using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.Globalization;
using System.Threading;

namespace BILP500PA
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            // rango de fechas por defecto (día de hoy)
            campoFechaDesde.Text = campoFechaDesde.Text == "" ? DateTime.Now.ToString("yyyyMMdd") : campoFechaDesde.Text;
            campoFechaHasta.Text = campoFechaHasta.Text == "" ? DateTime.Now.ToString("yyyyMMdd") : campoFechaHasta.Text;

            // culture info de PA
            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-PA");

        }


        // REPORTAR MENSAJES Y ADVERTENCIAS DEL PROGRAMA
        //// reportar mensaje
        protected void ReportarMensaje(string mensaje)
        {
            listaMensajesDelPrograma.Items.Add(mensaje);
            etiquetaMensajesDelPrograma.Visible = true;
        }
        //// reportar mensaje
        protected void ReportarAdvertencia(string error)
        {
            listaAdvertenciasDelPrograma.Items.Add(error);
            etiquetaAdvertenciasDelPrograma.Visible = true;
        }


        protected void GridViewPrefijosDeDocumento_DataBound(object sender, EventArgs e)
        {

            // ASOCIACIÓN PROGRAMÁTICA DE LA INTERFAZ FISCAL !!! (IMPORTANTE)
            // Este bloque de impresión asocia un prefijo de documentos
            for (int i = 0; i < GridViewPrefijosDeDocumento.Rows.Count; i++)
            {
                switch (GridViewPrefijosDeDocumento.Rows[i].Cells[1].Text)
                {
                    //case "AA":
                    //    GridViewPrefijosDeDocumento.Rows[i].Cells[3].Text = "IFHS210000162";
                    //    break;
                    case "PT":
                        GridViewPrefijosDeDocumento.Rows[i].Cells[3].Text = "1FHS210000559";
                        break; 
                    default:
                        GridViewPrefijosDeDocumento.Rows[i].Cells[3].Text = "-";
                        GridViewPrefijosDeDocumento.Rows[i].Cells[0].Controls[0].Visible = false;
                        break;
                }
            }

        }


        // generar y mostrar la pantalla consolidado
        protected void GenerarPantallaDocumentos()
        {
            
            // detectar la interfaz fiscal
            InterfazFiscalDetectarInterfaz();

            // activar y mostrar el panel de documentos (vista principal)
            PanelDocumentos.Enabled = true;
            PanelDocumentos.Visible = true;

            // desactivar y ocultar paneles a no utilizar
            PanelDocumentoSeleccionado.Enabled = false;
            PanelDocumentoSeleccionado.Visible = false;

            // data view de la pantalla
            DataView dv = (DataView)SqlDataSourceDocumentos.Select(DataSourceSelectArguments.Empty);

            // columnas adicionales (formateadas)
            dv.Table.Columns.Add("FECHAE", typeof(String)); // fecha de emisión
            dv.Table.Columns.Add("TIPOD", typeof(String)); // tipo de documento
            // dv.Table.Columns.Add("DOCORG", typeof(String)); // # documento original
            dv.Table.Columns.Add("DOCASO", typeof(String)); // # documento asociado
            dv.Table.Columns.Add("CODCLIENTE", typeof(String)); // codigo del cliente (facturación)
            dv.Table.Columns.Add("NOMCLIENTE", typeof(String)); // nombre del cliente
            dv.Table.Columns.Add("IDFISCAL", typeof(String)); // idfiscal

            // recorrer toda la data y alimentar la GV
            for (int i = 0; i < dv.Table.Rows.Count; i++)
            {

                // fecha de emisión
                dv.Table.Rows[i]["FECHAE"] = new DateTime(
                    Int32.Parse(String.Format(dv.Table.Rows[i]["SIINVD"].ToString()).Substring(0, 4)),
                    Int32.Parse(String.Format(dv.Table.Rows[i]["SIINVD"].ToString()).Substring(4, 2)),
                    Int32.Parse(String.Format(dv.Table.Rows[i]["SIINVD"].ToString()).Substring(6, 2))).ToString("dd/MM/yyyy");

                // tipo de documento
                switch (dv.Table.Rows[i]["IHDTYP"].ToString())
                {
                    case "1":
                        dv.Table.Rows[i]["TIPOD"] = "FC";
                        break;
                    case "2":
                        dv.Table.Rows[i]["TIPOD"] = "ND";
                        break;
                    case "3":
                        dv.Table.Rows[i]["TIPOD"] = "NC";
                        break;
                    default:
                        dv.Table.Rows[i]["TIPOD"] = "";
                        break;
                }

                // # documento original
                // dv.Table.Rows[i]["DOCORG"] = dv.Table.Rows[i]["IHDPFX"] + "-" + dv.Table.Rows[i]["IHDOCN"];

                // # documento asociado
                if (String.Format(dv.Table.Rows[i]["IHDPFX"] + "-" + dv.Table.Rows[i]["IHDOCN"]).ToString() != String.Format(dv.Table.Rows[i]["ARODPX"] + "-" + dv.Table.Rows[i]["SIINVN"]).ToString())
                {
                    string docAsoTipo;
                    switch (dv.Table.Rows[i]["ARODTP"].ToString())
                    {
                        case "1":
                            docAsoTipo = "1 FC";
                            break;
                        case "2":
                            docAsoTipo = "2 ND";
                            break;
                        case "3":
                            docAsoTipo = "3 NC";
                            break;
                        default:
                            docAsoTipo = "";
                            break;
                    }
                    dv.Table.Rows[i]["DOCASO"] = docAsoTipo + " " + dv.Table.Rows[i]["ARODPX"] + " " + dv.Table.Rows[i]["SIINVN"];
                }

                // cliente
                //// cliente asociado con cliente de facturación diferente
                if (dv.Table.Rows[i]["CCUST_CLIENTE"].ToString() != dv.Table.Rows[i]["CCUST_FACTURACION"].ToString())
                {
                    dv.Table.Rows[i]["CODCLIENTE"] = dv.Table.Rows[i]["CCUST_FACTURACION"];
                    dv.Table.Rows[i]["NOMCLIENTE"] = dv.Table.Rows[i]["CCUST_CLIENTE"] + " - " + dv.Table.Rows[i]["CNME_CLIENTE"] +
                        " facturar a: " + dv.Table.Rows[i]["CNME_FACTURACION"];
                }
                //// cliente de facturación
                else
                {
                    dv.Table.Rows[i]["CODCLIENTE"] = dv.Table.Rows[i]["CCUST_FACTURACION"];
                    dv.Table.Rows[i]["NOMCLIENTE"] = dv.Table.Rows[i]["CNME_FACTURACION"];
                }

                dv.Table.Rows[i]["IDFISCAL"] = "-";

                // CORRELATIVO FISCAL
                if (dv.Table.Rows[i]["ARCAIN"].ToString().Trim() != "")
                {
                    dv.Table.Rows[i]["IDFISCAL"] = dv.Table.Rows[i]["ARCAIN"].ToString();
                }

            }

            // alimentar la GV
            GridViewDocumentos.DataSource = dv;
            GridViewDocumentos.DataBind();

            // funciones y comandos
            // decoración y color
            for (int i = 0; i < GridViewDocumentos.Rows.Count; i++)
            {
            }

        }
        //// reconstrucción de las líneas y botones de comando
        protected void GridViewDocumentos_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowIndex > -1) {


                // Referencia a los controles
                var botonComando1 = (ImageButton)e.Row.Cells[8].FindControl("botonComando1");
                botonComando1.CommandName = "Comando1";
                botonComando1.CommandArgument = e.Row.RowIndex.ToString();
                var botonComando2 = (ImageButton)e.Row.Cells[8].FindControl("botonComando2");
                botonComando2.CommandName = "Comando2";
                botonComando2.CommandArgument = e.Row.RowIndex.ToString();
                var botonComando3 = (ImageButton)e.Row.Cells[8].FindControl("botonComando3");
                botonComando3.CommandName = "Comando3";
                botonComando3.CommandArgument = e.Row.RowIndex.ToString();
                var botonComando4 = (ImageButton)e.Row.Cells[8].FindControl("botonComando4");
                botonComando4.CommandName = "Comando4";
                botonComando4.CommandArgument = e.Row.RowIndex.ToString();


                // Comando # 1
                // ENVIAR a Interfaz Fiscal

                // verifica si el documento ya ha sido enviado a la interfaz fiscal
                //// registrado
                if (e.Row.Cells[10].Text.ToString() != "-")
                {
                    botonComando1.Visible = false;
                }
                else
                {
                    // verifica la interfaz fiscal
                    //// disponible
                    if (Boolean.Parse(estadoInterfazFiscal.Value) == true)
                    {
                        // detecta si ya se han enviado los datos a la interfaz fiscal para avisar al usuario en la interfaz
                        if (InterfazFiscalDetectarRecepcionDocumento(e.Row.Cells[2].Text.ToString(), e.Row.Cells[4].Text.ToString(), e.Row.Cells[5].Text.ToString())) {
                            botonComando1.ImageUrl = "~/images/up-off.gif";
                        }
                    }
                    //// NO disponible
                    else
                    {
                        botonComando1.ImageUrl = "~/images/up-off.gif";
                        botonComando1.Visible = false;
                    }
                }

                // Comando #2
                // RECIBIR ID Fiscal

                botonComando2.Enabled = false;
                botonComando2.Visible = false;

                // verifica si el documento ya ha sido enviado a la interfaz fiscal
                //// detectado
                if (e.Row.Cells[10].Text.ToString() != "-")
                {
                    botonComando2.ImageUrl = "~/images/dn-off.gif";
                    botonComando2.Enabled = false;
                    botonComando2.Visible = false;

                    botonComando1.ImageUrl = "~/images/up-off.gif";
                    botonComando1.Enabled = false;
                    botonComando1.Visible = false;

                    botonComando3.ImageUrl = "~/images/edit-off.gif";
                    botonComando3.Enabled = false;
                    botonComando3.Visible = false;
                }
                //// no detectado
                else
                {
                    if (InterfazFiscalDetectarSalidaDocumento(e.Row.Cells[2].Text.ToString(), e.Row.Cells[4].Text.ToString(), e.Row.Cells[5].Text.ToString()))
                    {
                        botonComando2.ImageUrl = "~/images/dn.gif";
                        botonComando2.Visible = true;
                        botonComando2.Enabled = true;

                        botonComando1.ImageUrl = "~/images/up-off.gif";
                        botonComando1.Enabled = false;
                        botonComando1.Visible = false;

                        botonComando3.ImageUrl = "~/images/edit.gif";
                        botonComando3.Enabled = true;
                        botonComando3.Visible = true;
                    }
                }


                // Comando #3
                // REGISTRO MANUAL DE ID Fiscal

                // detecta si ya se han enviado los datos a la interfaz fiscal para avisar al usuario en la interfaz
                if (InterfazFiscalDetectarRecepcionDocumento(e.Row.Cells[2].Text.ToString(), e.Row.Cells[4].Text.ToString(), e.Row.Cells[5].Text.ToString()))
                {
                    if (InterfazFiscalDetectarSalidaDocumento(e.Row.Cells[2].Text.ToString(), e.Row.Cells[4].Text.ToString(), e.Row.Cells[5].Text.ToString()))
                    {
                        botonComando3.ImageUrl = "~/images/edit-off.gif";
                        botonComando3.Enabled = false;
                        botonComando3.Visible = false;
                    }
                    else
                    {
                        botonComando3.ImageUrl = "~/images/edit.gif";
                        botonComando3.Enabled = true;
                        botonComando3.Visible = true;
                    }
                }


                // Comando #4
                // VER DOCUMENTO Proforma



                // EXCEPCIONES

                // verifica si el documento tipo 3 NC puede ser procesado por la interfaz fiscal
                // las notas de crédito sin documento asociado no pueden procesarse por la interfaz fiscal
                if (e.Row.Cells[2].Text.ToString() == "3")
                {
                    if (e.Row.Cells[9].Text.ToString() == "-")
                    {
                        botonComando1.ImageUrl = "~/images/up-off.gif";
                        botonComando1.Enabled = false;
                        botonComando1.Visible = false;

                        botonComando2.ImageUrl = "~/images/dn-off.gif";
                        botonComando2.Enabled = false;
                        botonComando2.Visible = false;

                        botonComando3.ImageUrl = "~/images/edit-off.gif";
                        botonComando3.Enabled = false;
                        botonComando3.Visible = false;
                    }
                }


            }

        }


        // ver/actualizar lista de documentos
        protected void botonVerListaDocumentos_Click(object sender, EventArgs e)
        {
            // oculta el panel de documento seleccionado
            PanelDocumentoSeleccionado.Enabled = false;
            PanelDocumentoSeleccionado.Visible = false;

            // muestra el panel de documentos
            PanelDocumentos.Visible = true;
            botonActualizarDocumentos.Visible = true;
        }


        // BOTONES DE COMANDOS

        // COMANDO 1 "ENVIAR A INTERFAZ FISCAL" - Desencadenante
        protected void botonComando1_Click(object sender, ImageClickEventArgs e)
        {
            if (InterfazFiscalSeleccionarDocumento(int.Parse(((ImageButton)sender).CommandArgument.ToString())))
            {
                // canalización según el tipo de documento
                switch (docSelTipo.Value.ToString())
                {
                    case "1": // 1 - FACTURAS
                        botonComandoInterfazFiscal_Click(sender, e);
                        break;
                    case "2": // 2 - NOTAS DE DEBITO (No soportadas)
                        ReportarAdvertencia(String.Format("La interfaz fiscal no soporta ND para {0} {1}", docSelPrefijo.Value.ToString(), docSelNumero.Value.ToString()));
                        break;
                    case "3": // 3 - NOTAS DE CREDITO
                        botonComandoInterfazFiscal_Click(sender, e);
                        break;
                    default:
                        ReportarAdvertencia(String.Format("Tipo de documento no reconocido por la interfaz fiscal"));
                        break;
                }
            }

            GenerarPantallaDocumentos();

            InterfazFiscalBorrarDocumentoSeleccionado();

        }
        // COMANDO 2 "RECIBIR ID Fiscal" - Desencadenante
        protected void botonComando2_Click(object sender, ImageClickEventArgs e)
        {
            if (InterfazFiscalSeleccionarDocumento(int.Parse(((ImageButton)sender).CommandArgument.ToString())))
            {

                string interfazFiscal = InterfazFiscalUbicacion();
                try
                {

                    // string archivoDestino = interfazFiscal + "\\SALIDA\\" + docSelTipo.Value + "_" + docSelPrefijo.Value + "-" + docSelNumero.Value + ".txt_S.txt";
                    string archivoDestino = interfazFiscal + "\\SALIDA\\" + docSelTipo.Value + "_" + docSelPrefijo.Value + "-" + docSelNumero.Value + ".txt_S";

                    StreamReader tr = new StreamReader(archivoDestino);

                    string trAllFile = tr.ReadToEnd();
                    string[] trLines = trAllFile.Split(new char[] {'\n'});
                    
                    // FIRMA FISCAL
                    // INTERFAZ FISCAL + "-" + CONSECUTIVO FISCAL DEL DOCUMENTO
                    string firmaFiscal = trLines[0].Trim() + "-" + trLines[1].Trim();

                    if (trLines[0].Trim() != etiquetaInterfazFiscal.Text)
                    {
                        ReportarAdvertencia(String.Format("EL SERIAL DE LA IMPRESORA FISCAL EN LA RESPUESTA DE LA INTERFAZ FISCAL NO COINCIDE CON EL SERIAL ASOCIADO AL PREFIJO. '{0}' Y DEBIÓ SER '{1}'", trLines[0].Trim(), etiquetaInterfazFiscal.Text));
                        firmaFiscal = "-";
                    }

                    if (trLines[1].Trim().Length != 8)
                    {
                        ReportarAdvertencia(String.Format("EL CORRELATIVO EN LA RESPUESTA DE LA INTERFAZ FISCAL NO TRAJO LA LONGITUD ESPERADA DE 8 DIGITOS. LONGITUD ENTREGADA '{0}' Y DEBIÓ SER '8'", trLines[1].Length.ToString()));
                        firmaFiscal = "-";
                    }

                    // TIEMPO FISCAL
                    // DIA FISCAL + HORA FISCAL DDMMYYYYHHMMSS
                    string tiempoFiscal = trLines[2].Trim() + trLines[3].Trim();

                    if (firmaFiscal != "-")
                    {

                        try
                        {

                            InterfazFiscalIdicarDocumentoSeleccionado();

                            OdbcConnection rarCon = new OdbcConnection(System.Configuration.ConfigurationManager.ConnectionStrings["BILP500PAcsWriter"].ToString());

                            OdbcCommand rarComm = new OdbcCommand("UPDATE RAR SET ARCAIN = ?, ARHNDL = ? WHERE (RCOMP = ?) AND (ARDTYP = ?) AND (ARDPFX = ?) AND (ARDOCN = ?)", rarCon);

                            rarComm.Parameters.AddWithValue("?", firmaFiscal);
                            rarComm.Parameters.AddWithValue("?", tiempoFiscal);
                            rarComm.Parameters.AddWithValue("?", Decimal.Parse(campoSeleccionDeCompania.SelectedValue));
                            rarComm.Parameters.AddWithValue("?", Decimal.Parse(docSelTipo.Value));
                            rarComm.Parameters.AddWithValue("?", docSelPrefijo.Value);
                            rarComm.Parameters.AddWithValue("?", Decimal.Parse(docSelNumero.Value));

                            rarCon.Open();
                            rarComm.ExecuteNonQuery();
                            rarCon.Close();                        

                        }
                        catch (Exception ex)
                        {
                            // ReportarAdvertencia(String.Format("RECIBIENDO ID FISCAL {0}", ex.Message));
                        }

                        ReportarMensaje(String.Format("Se recibio correctamente el ID Fiscal {0} para el documento tipo {1} {2}-{3}", firmaFiscal, docSelTipo.Value, docSelPrefijo.Value, docSelNumero.Value));

                    }

                    tr.Close();

                }
                catch (Exception ex)
                {
                    ReportarAdvertencia(String.Format("SISTEMA DE ARCHIVOS DE LA INTERFAZ FISCAL {0}", "", ex.Message));
                }

            }

            GenerarPantallaDocumentos();

            InterfazFiscalBorrarDocumentoSeleccionado();

        }
        // COMANDO 3 " REGISTRO MANUAL de ID Fiscal" - Desencadenante
        protected void botonComando3_Click(object sender, ImageClickEventArgs e)
        {
            botonComandoInterfazFiscal_Click(sender, e);
            InterfazFiscalBorrarDocumentoSeleccionado();
        }
        // COMANDO 4 "VER PROFORMA" - Desencadenante
        protected void botonComando4_Click(object sender, ImageClickEventArgs e)
        {
            botonComandoInterfazFiscal_Click(sender, e);
            InterfazFiscalBorrarDocumentoSeleccionado();
        }

        // COMANDO DE LA INTERFAZ FISCAL - Acción Desencadenada
        protected void botonComandoInterfazFiscal_Click(object sender, ImageClickEventArgs e)
        {

            // ((ImageButton)sender).CommandName.ToString()
            // Comando1
            // Comando2
            // Comando4
            // Comando5

            if (InterfazFiscalSeleccionarDocumento(int.Parse(((ImageButton)sender).CommandArgument.ToString())))
            {


                // lineas del archivo a generar
                List<string> lineasArchivo = new List<string>();

                // interruptor para la generación de archivo
                bool generarArchivo = false;
                bool generarArchivoAdvertencias = false;

                // manejo de paneles
                switch (((ImageButton)sender).CommandName.ToString())
                {
                    case "Comando4":
                        // panel de la lista de documentos
                        PanelDocumentos.Visible = false;
                        botonActualizarDocumentos.Visible = false;
                        // panel del documento seleccionado
                        PanelDocumentoSeleccionado.Enabled = true;
                        PanelDocumentoSeleccionado.Visible = true;

                        break;
                    default:
                        break;
                }

                // manejo de indicadores
                switch (((ImageButton)sender).CommandName.ToString())
                {
                    case "Comando1":
                    case "Comando4":
                        // documento seleccionado
                        InterfazFiscalIdicarDocumentoSeleccionado();
                        break;
                    default:
                        break;
                }

                // generacion de archivo
                switch (((ImageButton)sender).CommandName.ToString())
                {
                    case "Comando1":
                        generarArchivo = true;
                        break;
                    default:
                        break;
                }


                /////////////////////
                // MANEJO DE DATOS //
                /////////////////////

                // data view del cliente de facturacion
                DataView dvCliente = (DataView)SqlDataSourceDocSelCliente.Select(DataSourceSelectArguments.Empty);

                // data view del país de facturación
                docSelClientePais.Value = dvCliente.Table.Rows[0]["CCOUN"].ToString().Trim();
                DataView dvPais = (DataView)SqlDataSourceDocSelClientePais.Select(DataSourceSelectArguments.Empty);

                // data view de encabezado
                DataView dvEncabezado = (DataView)SqlDataSourceDocSelEncabezado.Select(DataSourceSelectArguments.Empty);

                // data view de notas

                docSelPedido.Value = dvEncabezado.Table.Rows[0]["SIORD"].ToString();

                DataView dvNotas = (DataView)SqlDataSourceDocSelNotas.Select(DataSourceSelectArguments.Empty);

                // data view del documento asociado (padre)
                docSelTipoAsociado.Value = dvEncabezado.Table.Rows[0]["ARODTP"].ToString();
                docSelPrefijoAsociado.Value = dvEncabezado.Table.Rows[0]["ARODPX"].ToString();
                docSelNumeroAsociado.Value = dvEncabezado.Table.Rows[0]["SIINVN"].ToString();
                DataView dvEncabezadoAsociado = (DataView)SqlDataSourceDocSelEncabezadoAsociado.Select(DataSourceSelectArguments.Empty);

                switch (dvEncabezado.Table.Rows[0]["IHDTYP"].ToString())
                {
                    case "2":
                    case "3":
                        if (dvEncabezadoAsociado.Table.Rows[0]["ARCAIN"].ToString() == "                      ")
                        {
                            ReportarAdvertencia(String.Format("COMANDO NO PERMITIDO. No se puede enviar el documento tipo {0} numero {1} a la interfaz fiscal. La impresora fiscal aún no ha impreso el documento original asociado tipo {2} numero {3}", dvEncabezadoAsociado.Table.Rows[0]["IHDTYP"].ToString(), dvEncabezadoAsociado.Table.Rows[0]["IHDPFX"].ToString() + "-" + dvEncabezadoAsociado.Table.Rows[0]["IHDOCN"].ToString(), dvEncabezadoAsociado.Table.Rows[0]["IHDTYP"].ToString(), dvEncabezadoAsociado.Table.Rows[0]["IHDPFX"].ToString() + "-" + dvEncabezadoAsociado.Table.Rows[0]["IHDOCN"].ToString()));
                            generarArchivoAdvertencias = true;
                        }
                    break;
                }

                // ENCABEZADO DEL DOCUMENTO
                ///////////////////////////

                // LÍNEA # 1 ----------
                // TIPO DE DOCUMENTO
                string dsl1 = "";

                switch (dvEncabezado.Table.Rows[0]["IHDTYP"].ToString())
                {
                    case "1": // 1 - FACTURAS
                        dsl1 = "FACTURA";
                        LabelDSTipoDocumento.Text = "FACTURA";
                        break;
                    case "2": // 2 - NOTAS DE DEBITO (No soportadas)
                        dsl1 = "";
                        LabelDSTipoDocumento.Text = "---NO SOPORTADO---";
                        break;
                    case "3": // 3 - NOTAS DE CREDITO
                        dsl1 = "NOTA CREDITO";
                        LabelDSTipoDocumento.Text = "NOTA CREDITO";
                        break;
                    default:
                        dsl1 = "";
                        LabelDSTipoDocumento.Text = "---NO DEFINIDO---";
                        break;
                }

                lineasArchivo.Add(dsl1);

                // LÍNEA # 2 ----------
                // NO. FACTURA DEL SISTEMA
                string dsl2 = "";

                dsl2 = dvEncabezado.Table.Rows[0]["IHDPFX"].ToString() + "-" + dvEncabezado.Table.Rows[0]["IHDOCN"].ToString();

                lineasArchivo.Add(dsl2);

                // LÍNEA # 3 ----------
                // NÚMERO DE IMPRESIÓN FISCAL
                string dsl3 = "";

                // documento procesado por la interfaz fiscal
                if (dvEncabezado.Table.Rows[0]["ARCAIN"].ToString() != "                      ")
                {

                    string dsl3TInterfaz = String.Format(dvEncabezado.Table.Rows[0]["ARCAIN"].ToString()).Substring(0, 13);
                    string dsl3TCorrelativo = String.Format(dvEncabezado.Table.Rows[0]["ARCAIN"].ToString()).Substring(14, 8);

                    DateTime dsl3TTiempoFiscal = new DateTime(Int32.Parse(String.Format(dvEncabezado.Table.Rows[0]["ARHNDL"].ToString()).Substring(4, 4)),
                        Int32.Parse(String.Format(dvEncabezado.Table.Rows[0]["ARHNDL"].ToString()).Substring(2, 2)),
                        Int32.Parse(String.Format(dvEncabezado.Table.Rows[0]["ARHNDL"].ToString()).Substring(0, 2)),
                        Int32.Parse(String.Format(dvEncabezado.Table.Rows[0]["ARHNDL"].ToString()).Substring(8, 2)),
                        Int32.Parse(String.Format(dvEncabezado.Table.Rows[0]["ARHNDL"].ToString()).Substring(10, 2)),
                        Int32.Parse(String.Format(dvEncabezado.Table.Rows[0]["ARHNDL"].ToString()).Substring(12, 2)));

                    LabelDSNumeroFiscal.Text = dsl3TInterfaz + "-" + dsl3TCorrelativo;
                    LabelDSFechaHoraFiscal.Text = dsl3TTiempoFiscal.ToString("dd-MM-yyyy hh:mm");

                }
                // documento no procesado por la interfaz fiscal
                else
                {
                    dsl3 = "";
                    LabelDSNumeroFiscal.Text = "DOCUMENTO NO PROCESADO POR LA INTERFAZ FISCAL";
                    LabelDSFechaHoraFiscal.Text = "DOCUMENTO NO PROCESADO POR LA INTERFAZ FISCAL";
                }

                // cadena del número fiscal del documento asociado
                if (dvEncabezadoAsociado.Table.Rows[0]["ARCAIN"].ToString() != "                      ")
                {
                    switch (dvEncabezado.Table.Rows[0]["IHDTYP"].ToString())
                    {
                        case "3": // 3 - NOTAS DE CREDITO

                            string dsl3TInterfazAsociado = String.Format(dvEncabezadoAsociado.Table.Rows[0]["ARCAIN"].ToString()).Substring(0, 13);
                            string dsl3TCorrelativoAsociado = String.Format(dvEncabezadoAsociado.Table.Rows[0]["ARCAIN"].ToString()).Substring(14, 8);

                            DateTime dsl3TTiempoFiscalAsociado = new DateTime(Int32.Parse(String.Format(dvEncabezadoAsociado.Table.Rows[0]["ARHNDL"].ToString()).Substring(4, 4)),
                            Int32.Parse(String.Format(dvEncabezadoAsociado.Table.Rows[0]["ARHNDL"].ToString()).Substring(2, 2)),
                            Int32.Parse(String.Format(dvEncabezadoAsociado.Table.Rows[0]["ARHNDL"].ToString()).Substring(0, 2)),
                            Int32.Parse(String.Format(dvEncabezadoAsociado.Table.Rows[0]["ARHNDL"].ToString()).Substring(8, 2)),
                            Int32.Parse(String.Format(dvEncabezadoAsociado.Table.Rows[0]["ARHNDL"].ToString()).Substring(10, 2)),
                            Int32.Parse(String.Format(dvEncabezadoAsociado.Table.Rows[0]["ARHNDL"].ToString()).Substring(12, 2)));

                            dsl3 = dsl3TCorrelativoAsociado + dsl3TInterfazAsociado + dsl3TTiempoFiscalAsociado.ToString("ddMMyyyyhhmmss");

                            break;
                        default:
                            dsl3 = "";
                            break;
                    }
                }
                else
                {
                    dsl3 = "";
                }

                lineasArchivo.Add(dsl3);

                // LÍNEA # 4 ----------
                // CONDICIÓN DE PAGO
                string dsl4 = "";

                if (dvEncabezado.Table.Rows[0]["RIDTE"].ToString() == dvEncabezado.Table.Rows[0]["RDDTE"].ToString())
                {
                    dsl4 = "CONTADO";
                }
                else
                {
                    dsl4 = "CREDITO";
                }

                lineasArchivo.Add(dsl4);

                // LÍNEA # 5 ----------
                // RUC DEL CLIENTE
                string dsl5 = "";

                //dsl5 = dvCliente.Table.Rows[0]["CMFTXC"].ToString();
                dsl5 = dvCliente.Table.Rows[0]["CMUF01"].ToString();
                LabelIDFiscalCliente.Text = dsl5;

                lineasArchivo.Add(dsl5);

                // LÍNEA # 6 ----------
                // NOMBRE DEL CLIENTE
                string dsl6 = "";

                dsl6 = dvCliente.Table.Rows[0]["CNME"].ToString().Substring(0, 50);
                LabelNombreCliente.Text = dsl6;

                lineasArchivo.Add(dsl6);

                // LÍNEA # 7 ----------
                // DIRECCION DEL CLIENTE
                string dsl7 = "";

                dsl7 = dvCliente.Table.Rows[0]["CAD1"].ToString().Substring(0, 50);
                LabelDireccionCliente.Text = dsl7;

                lineasArchivo.Add(dsl7);

                // LÍNEA # 8 ----------
                // TELÉFONO DEL CLIENTE
                string dsl8 = "";

                dsl8 = dvCliente.Table.Rows[0]["CPHON"].ToString().Substring(0, 25);
                LabelTelefonoCliente.Text = dsl8;

                lineasArchivo.Add(dsl8);

                // LINEAS ADICIONALES DE ENCABEZADO DEL DOCUMENTO
                /////////////////////////////////////////////////

                string dsl9 = "";

                // identificación del cliente
                dsl9 = String.Format("Cta: {0} Cliente: {1}", dvCliente.Table.Rows[0]["CCUST"].ToString(), dvCliente.Table.Rows[0]["CNME"].ToString().Substring(0, 50)).Trim();
                // identificación del pedido
                string dsl9t = "";
                dsl9t = String.Format("Pedido: {0}  OC: {1}", dvEncabezado.Table.Rows[0]["SIORD"].ToString().Trim(), dvEncabezado.Table.Rows[0]["SICPO"].ToString().Trim());
                // combinación
                dsl9 = String.Format("{0} {1}", dsl9, dsl9t.ToString().PadLeft(120 - 1 - dsl9.Length, ' '));

                LabelEncabezadoL1.Text = dsl9;

                string dsl10 = "";

                string dsl10t = "";
                string dsl10to = "";

                dsl10t = dvCliente.Table.Rows[0]["CAD1"].ToString().Trim();
                //dsl10to += (dsl10to.Length + dsl10t.Length + 1) <= 132 ? " " + dsl10t : dsl10t.Substring(0, 132 - 1 - dsl10to.Length - dsl10t.Length);
                dsl10to += (dsl10to.Length + dsl10t.Length + 1) <= 132 ? " " + dsl10t : dsl10t;
                dsl10t = dvCliente.Table.Rows[0]["CAD2"].ToString().Trim();
                //dsl10to += (dsl10to.Length + dsl10t.Length + 1) <= 132 ? " " + dsl10t : dsl10t.Substring(0, 132 - 1 - dsl10to.Length - dsl10t.Length);
                dsl10to += (dsl10to.Length + dsl10t.Length + 1) <= 132 ? " " + dsl10t : dsl10t;
                // dsl10t = dvCliente.Table.Rows[0]["CAD3"].ToString().Trim();
                // dsl10to += (dsl10to.Length + dsl10t.Length + 1) <= 132 ? " " + dsl10t : dsl10t.Substring(0, 132 - 1 - dsl10to.Length - dsl10t.Length);

                dsl10 = String.Format("Direccion: {0} Pais: {1}", dsl10to, dvPais.Table.Rows[0]["CCDESC"].ToString().Trim());
                LabelEncabezadoL2.Text = dsl10;

                string dsl11 = "";

                // telefono ruc
                //dsl11 = String.Format("Telefonos: {0} Ruc: {1}", dvCliente.Table.Rows[0]["CPHON"].ToString().Substring(0, 25).Trim(), dvCliente.Table.Rows[0]["CMFTXC"].ToString().Trim());
                dsl11 = String.Format("Telefonos: {0} Ruc: {1}", dvCliente.Table.Rows[0]["CPHON"].ToString().Substring(0, 25).Trim(), dvCliente.Table.Rows[0]["CMUF01"].ToString().Trim());
                // almacen, cond pago y vendedor
                string dsl11t = "";
                dsl11t = String.Format("Alm: {0}  Cond.Pago: {1} Vendedor: {2}", dvEncabezado.Table.Rows[0]["SIWHSE"].ToString().Trim(), dvEncabezado.Table.Rows[0]["SITDES"].ToString().Trim(), dvEncabezado.Table.Rows[0]["SISAL"].ToString().Trim());
                // combinación
                dsl11 = String.Format("{0} {1}", dsl11, dsl11t.ToString().PadLeft(120 - 1 - dsl11.Length, ' '));


                
                LabelEncabezadoL3.Text = dsl11;

                lineasArchivo.Add(dsl9);
                lineasArchivo.Add(dsl10);
                lineasArchivo.Add(dsl11);

                // LINEAS ADICIONALES DEL PIE DEL DOCUMENTO
                ///////////////////////////////////////////

                string dsl12 = "";

                dsl12 = "Pto.Envio:";
                dsl12 += " " + dvEncabezado.Table.Rows[0]["SINAM"].ToString().Trim();

                LabelPieL1.Text = dsl12;

                string dsl13 = "";

                dsl13 = "Dir.Envio:";
                dsl13 += " " + dvEncabezado.Table.Rows[0]["SIAD1"].ToString().Trim();
                dsl13 += " " + dvEncabezado.Table.Rows[0]["SIAD2"].ToString().Trim();

                LabelPieL2.Text = dsl13;

                string dsl14 = "";

                dsl14 = dvNotas.Table.Rows.Count > 0 ? "Observaciones:" : "";
                dsl14 += dvNotas.Table.Rows.Count >= 1 ? " " + dvNotas.Table.Rows[0]["SNDESC"].ToString().Trim() : "";
                dsl14 += dvNotas.Table.Rows.Count >= 2 ? " " + dvNotas.Table.Rows[1]["SNDESC"].ToString().Trim() : "";

                LabelPieL3.Text = dsl14;

                lineasArchivo.Add(dsl12);
                lineasArchivo.Add(dsl13);
                lineasArchivo.Add(dsl14);

                // LÍNEA # 15 ----------
                // DESCUENTO GLOBAL
                double dsl15 = 0;

                lineasArchivo.Add(dsl15.ToString("##0.00"));

                // LINEAS DE MEDIOS DE PAGO
                ///////////////////////////

                lineasArchivo.Add("01");
                lineasArchivo.Add("");
                lineasArchivo.Add("02");
                lineasArchivo.Add("");
                lineasArchivo.Add("03");
                lineasArchivo.Add("");
                lineasArchivo.Add("04");
                lineasArchivo.Add("");
                lineasArchivo.Add("05");
                lineasArchivo.Add("");

                // LINEAS DEL DOCUMENTO
                ///////////////////////

                // data view de líneas
                DataView dvLineas = (DataView)SqlDataSourceDocSelLineas.Select(DataSourceSelectArguments.Empty);

                // data view de impuestos
                DataView dvImpuestos = (DataView)SqlDataSourceLibroImpuestos.Select(DataSourceSelectArguments.Empty);

                // LÍNEAS # 26 27 28 29 30 31 32 33 34 ----------
                // LÍNEAS DEL DOCUMENTO

                // tabla temporal de líneas
                DataTable dtLineasTemp = new DataTable();
                //// columnas
                dtLineasTemp.Columns.Add("CODIGO", typeof(String));
                dtLineasTemp.Columns.Add("DESCRIPCION", typeof(String));
                dtLineasTemp.Columns.Add("IMPUESTO", typeof(Decimal));
                dtLineasTemp.Columns.Add("IMPUESTO_TASA", typeof(Decimal));
                dtLineasTemp.Columns.Add("BASE", typeof(Decimal));
                dtLineasTemp.Columns.Add("CANTIDAD", typeof(Decimal));
                dtLineasTemp.Columns.Add("SUBTOTAL", typeof(Decimal));
                dtLineasTemp.Columns.Add("DESCUENTO", typeof(Decimal));
                dtLineasTemp.Columns.Add("DESCRIPCION1", typeof(String));
                dtLineasTemp.Columns.Add("DESCRIPCION2", typeof(String));
                dtLineasTemp.Columns.Add("DESCRIPCION3", typeof(String));

                // acumuladores
                decimal dtAcumuladorBase = 0;
                decimal dtAcumuladorImpuesto = 0;
                decimal dtAcumuladorSubTotal = 0;
                decimal dtAcumuladorTotal = 0;

                // recorrido por las líneas
                // IMPORTANTE: HAY LÍNEAS QUE PUEDEN NO TRAER DATOS DEL IIM
                for (int i = 0; i < dvLineas.Table.Rows.Count; i++)
                {
                    
                    //// CODIGO
                    string dtLineasTempCODIGO = dvLineas.Table.Rows[i]["ILPROD"].ToString().Substring(0, 25);
                    //// DESCRIPCION
                    string dtLineasTempDESCRIPCION = !(dvLineas.Table.Rows[i]["IDESC"] is System.DBNull) ? dvLineas.Table.Rows[i]["IDESC"].ToString().Substring(0, 50) : "DESC. N/D";
                    //// IMPUESTO
                    decimal dtLineasTempIMPUESTO = Decimal.Parse(dvLineas.Table.Rows[i]["ILTA01"].ToString());
                    dtAcumuladorImpuesto += dtLineasTempIMPUESTO;
                    //// IMPUESTO_TASA
                    decimal dtLineasTempIMPUESTO_TASA = 0; // Excento por defecto
                    for (int imp = 0; imp < dvImpuestos.Table.Rows.Count; imp++) // busca en el libro de impuestos
                    {
                        if (dvLineas.Table.Rows[i]["ILTR01"].ToString() == dvImpuestos.Table.Rows[imp]["RCRTCD"].ToString())
                        {
                            dtLineasTempIMPUESTO_TASA = Decimal.Parse(dvImpuestos.Table.Rows[imp]["RCNRTE"].ToString());
                            break;
                        }
                        imp++;
                    }
                    //// BASE
                    decimal dtLineasTempBASE = Decimal.Parse(dvLineas.Table.Rows[i]["ILNET"].ToString());
                    // dtLineasTempBASE = Decimal.Round(dtLineasTempBASE, 2); // REDONDEO DE LA BASE - NO APLICA EN EL MODELO PAPISA
                    dtAcumuladorBase += dtLineasTempBASE * Decimal.Parse(dvLineas.Table.Rows[i]["ILQTY"].ToString()); // base * cantidad en el acumulador
                    dtAcumuladorSubTotal += dtLineasTempBASE * Decimal.Parse(dvLineas.Table.Rows[i]["ILQTY"].ToString()); // base * cantidad en el acumulador;
                    //// CANTIDAD
                    decimal dtLineasTempCANTIDAD = Decimal.Parse(dvLineas.Table.Rows[i]["ILQTY"].ToString());
                    //// SUBTOTAL
                    decimal dtLineasTempSUBTOTAL = dtLineasTempBASE * Decimal.Parse(dvLineas.Table.Rows[i]["ILQTY"].ToString()); // base * cantidad en el acumulador

                    // inserta la línea
                    dtLineasTemp.Rows.Add(
                        dtLineasTempCODIGO,
                        dtLineasTempDESCRIPCION,
                        dtLineasTempIMPUESTO,
                        dtLineasTempIMPUESTO_TASA,
                        dtLineasTempBASE,
                        dtLineasTempCANTIDAD,
                        dtLineasTempSUBTOTAL,
                        0,
                        "",
                        "",
                        ""
                        );

                    lineasArchivo.Add(dtLineasTempCODIGO);
                    lineasArchivo.Add(dtLineasTempDESCRIPCION);
                    lineasArchivo.Add(dtLineasTempIMPUESTO_TASA.ToString("##0.00"));
                    lineasArchivo.Add(dtLineasTempBASE.ToString("#########0.0000"));

                    switch (dvEncabezado.Table.Rows[0]["IHDTYP"].ToString())
                    {
                        case "1":
                            lineasArchivo.Add(dtLineasTempCANTIDAD.ToString("######0.00"));
                            break;
                        case "3": // la interfaz fiscal no soporta valores en negativo
                            dtLineasTempCANTIDAD *= -1;
                            lineasArchivo.Add(dtLineasTempCANTIDAD.ToString("######0.00"));
                            break;
                        default:
                            lineasArchivo.Add(dtLineasTempCANTIDAD.ToString("######0.00"));
                            break;
                    }

                    lineasArchivo.Add(0.ToString("##0.00"));
                    lineasArchivo.Add("");
                    lineasArchivo.Add("");
                    lineasArchivo.Add("");

                }

                // sub procesos
                switch (((ImageButton)sender).CommandName.ToString())
                {
                    case "Comando4":
                        // acumuladores
                        //// base imponible
                        LabelDSSubtotaBaseImponible.Text = dtAcumuladorBase.ToString("N2");
                        //// impuesto
                        LabelDSSubtotaImpuestos.Text = dtAcumuladorImpuesto.ToString("N2");
                        //// subtotal
                        LabelDSSubtotalItems.Text = dtAcumuladorSubTotal.ToString("N2");
                        //// total
                        dtAcumuladorTotal = dtAcumuladorBase + dtAcumuladorImpuesto;
                        LabelDSTotal.Text = dtAcumuladorTotal.ToString("N2");

                        // alimentar la vista de las líneas
                        GridViewDSLineas.DataSource = dtLineasTemp;
                        GridViewDSLineas.DataBind();
                        break;
                }


                ////////////////////////////////////////////////////
                // GENERACIÓN DEL ARCHIVO PARA LA INTERFAZ FISCAL //
                ////////////////////////////////////////////////////

                switch (((ImageButton)sender).CommandName.ToString())
                {
                    case "Comando1":
                        if (generarArchivo == true && generarArchivoAdvertencias == false)
                        {
                            try
                            {

                                string archivoDestino = InterfazFiscalUbicacion() + "\\RECEPCION\\" + docSelTipo.Value.ToString() + "_" + docSelPrefijo.Value.ToString() + "-" + docSelNumero.Value.ToString() + ".txt";

                                TextWriter tw = new StreamWriter(archivoDestino);

                                for (int i = 0; i < lineasArchivo.Count; i++)
                                {
                                    tw.WriteLine(lineasArchivo[i].ToString());
                                }

                                tw.Close();

                                ReportarMensaje("SE HA ENVIADO CORRECTAMENTE EL DOCUMENTO A LA INTERFAZ FISCAL");

                            }
                            catch (Exception ex)
                            {
                                ReportarAdvertencia(String.Format("SISTEMA DE ARCHIVOS DE LA INTERFAZ FISCAL '{0}' {1}", "", ex.Message));
                            }
                        }
                    break;
                }

                // advertencias en la generación de datos para la interfaz fiscal
                if (generarArchivo == true && generarArchivoAdvertencias == true)
                {
                    ReportarAdvertencia("Se generaron advertencias durante la generación de los datos para la interfaz fiscal");
                }


            }

        }


        // selección de prefijo de documento (pantalla principal)
        protected void GridViewPrefijosDeDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {

            // oculta el panel de parámetros
            PanelParametros.Visible = false;

            // muestra el panel de parámetros seleccionados
            PanelParametrosSeleccionados.Visible = true;

            // etiquetas de parámetros seleccionados
            etiquetaCompania.Text = campoSeleccionDeCompania.SelectedItem.ToString();
            etiquetaFechaDesde.Text = new DateTime(Int32.Parse(campoFechaDesde.Text.Substring(0, 4)), Int32.Parse(campoFechaDesde.Text.Substring(4, 2)), Int32.Parse(campoFechaDesde.Text.Substring(6, 2))).ToString("dd/MM/yyyy");
            etiquetaFechaHasta.Text = new DateTime(Int32.Parse(campoFechaHasta.Text.Substring(0, 4)), Int32.Parse(campoFechaHasta.Text.Substring(4, 2)), Int32.Parse(campoFechaHasta.Text.Substring(6, 2))).ToString("dd/MM/yyyy");
            etiquetaPrefijo.Text = GridViewPrefijosDeDocumento.Rows[GridViewPrefijosDeDocumento.SelectedIndex].Cells[1].Text + " - " + GridViewPrefijosDeDocumento.Rows[GridViewPrefijosDeDocumento.SelectedIndex].Cells[2].Text;
            etiquetaInterfazFiscal.Text = GridViewPrefijosDeDocumento.Rows[GridViewPrefijosDeDocumento.SelectedIndex].Cells[3].Text;

            // fijación de parámetros
            campoPrefijoDocumento.Value = GridViewPrefijosDeDocumento.Rows[GridViewPrefijosDeDocumento.SelectedIndex].Cells[1].Text;


            // alimentar la vista de documentos
            GenerarPantallaDocumentos();

        }


        // FUNCIONES DE LA INTERFAZ FISCAL

        // seleccionar un documento de la vista
        /// <summary>
        /// Selecciona y establece los parámetros del documento al cual se va a operar
        /// </summary>
        /// <param name="indice">Identificador de la fila en la GridView donde reciden los parámetros del documento al cual se va a operar</param>
        /// <returns></returns>
        protected bool InterfazFiscalSeleccionarDocumento(int indice)
        {
            try
            {
                // estado previo
                // bool gvde = GridViewDocumentos.Enabled;
                bool gvdv = GridViewDocumentos.Visible;

                // estado temporal
                GridViewDocumentos.Enabled = true;
                GridViewDocumentos.Visible = true;
                
                // fijación de parámetros
                //// tipo de documento
                docSelTipo.Value    = GridViewDocumentos.Rows[indice].Cells[2].Text.ToString();
                //// prefijo del documento
                docSelPrefijo.Value = GridViewDocumentos.Rows[indice].Cells[4].Text.ToString();
                //// numero del documento
                docSelNumero.Value  = GridViewDocumentos.Rows[indice].Cells[5].Text.ToString();
                //// cliente del documento
                docSelCliente.Value = GridViewDocumentos.Rows[indice].Cells[6].Text.ToString();

                // selecciona la fila en el GridView
                GridViewDocumentos.SelectedIndex = indice;

                // estado original
                // GridViewDocumentos.Enabled = gvde;
                GridViewDocumentos.Visible = gvdv;

                // muestra un mensaje del documento seleccionado
                // ReportarMensaje(String.Format("DOCUMENTO SELECCIONADO {0} {1} {2}", docSelTipo.Value, docSelPrefijo.Value, docSelNumero.Value));

                return true;
            }
            catch (Exception e)
            {
                ReportarAdvertencia(String.Format("PROBLEMAS CON LA SELECCION DEL DOCUMENTO {0}", e.Message));
                return false;
            }
        }
        protected bool InterfazFiscalSeleccionarDocumento(string pDocSelTipo, string pDocSelPrefijo, string pDocSelNumero, string pDocSelCliente)
        {
            try
            {
                // fijación de parámetros
                //// tipo de documento
                docSelTipo.Value    = pDocSelTipo;
                //// prefijo del documento
                docSelPrefijo.Value = pDocSelPrefijo;
                //// numero del documento
                docSelNumero.Value  = pDocSelNumero;
                //// cliente del documento
                docSelCliente.Value = pDocSelCliente;

                return true;
            }
            catch (Exception e)
            {
                ReportarAdvertencia(String.Format("PROBLEMAS CON LA SELECCION DEL DOCUMENTO {0}", e.Message));
                return false;
            }
        }
        // quitar el documento seleccionado
        protected void InterfazFiscalBorrarDocumentoSeleccionado()
        {
            docSelTipo.Value = "";
            docSelPrefijo.Value = "";
            docSelNumero.Value = "";
            docSelCliente.Value = "";
        }
        // indicar por pantalla el documento seleccionado
        protected void InterfazFiscalIdicarDocumentoSeleccionado()
        {
            ReportarMensaje(String.Format("DOCUMENTO SELECCIONADO {0} {1}-{2}", docSelTipo.Value, docSelPrefijo.Value, docSelNumero.Value));
        }
        
        // ubicación de la interfaz fiscal
        /// <summary>
        /// Devuelve la ubicación del sistema de archivos de la interfaz fiscal
        /// </summary>
        /// <returns>Devuelve la ubicación del sistema de archivos de la interfaz fiscal</returns>
        protected string InterfazFiscalUbicacion()
        {
            BILP500PA.Properties.Settings ps = new BILP500PA.Properties.Settings();
            // verifica con el código de la interfaz fiscal seleccionada por prefijo
            switch (etiquetaInterfazFiscal.Text)
            {
                //case "IFHS210000162":
                //    return "\\\\PA-PRUEBASDGI\\IFHS210000162";
                //    break;
                case "1FHS210000559":
                    return ps.INTERFAZ_1FHS210000559;
                    // return "\\\\PA-FACTURACION\\1FHS210000559";
                    // break;
                default:
                    return "";
                    // break;
            }
        }

        // detecta la operatividad del sistema de archivos de la interfaz fiscal
        /// <summary>
        /// Detecta la operatividad del sistema de archivos de la interfaz fiscal
        /// </summary>
        /// <returns></returns>
        protected bool InterfazFiscalDetectarInterfaz()
        {
            string interfazFiscal = InterfazFiscalUbicacion();
            try
            {
                DirectoryInfo di = new DirectoryInfo(interfazFiscal);
                FileInfo[] rgFiles = di.GetFiles("*.*");

                etiquetaInterfazFiscal.ForeColor = Color.Green;
                estadoInterfazFiscal.Value = "true";
                botonActualizarEstadoInterfazFiscal.Visible = false;

                return true;
            }
            catch (Exception e)
            {
                ReportarAdvertencia(String.Format("PROBLEMAS DE COMUNICACIÓN CON EL SISTEMA DE ARCHIVOS DE LA INTERFAZ FISCAL {0} {1}", e.Message, interfazFiscal));

                etiquetaInterfazFiscal.ForeColor = Color.Red;
                estadoInterfazFiscal.Value = "false";
                botonActualizarEstadoInterfazFiscal.Visible = true;
                
                return false;
            }
        }

        // verifica si un documento ha sido enviado al sistema de archivos de la interfaz fiscal
        protected bool InterfazFiscalDetectarRecepcionDocumento(string pDocSelTipo, string pDocSelPrefijo, string pDocSelNumero)
        {
            string interfazFiscal = InterfazFiscalUbicacion();
            try
            {
                string archivoDestino = interfazFiscal + "\\RECEPCION\\" + pDocSelTipo + "_" + pDocSelPrefijo + "-" + pDocSelNumero + ".txt";

                StreamReader tr = new StreamReader(archivoDestino);

                //string trAllFile = tr.ReadToEnd();
                //string[] trLines = trAllFile.Split(new char[] {'\n'});

                tr.Close();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        // verifica si un documento ha sido procesado por la interfaz fiscal
        protected bool InterfazFiscalDetectarSalidaDocumento(string pDocSelTipo, string pDocSelPrefijo, string pDocSelNumero)
        {
            string interfazFiscal = InterfazFiscalUbicacion();
            try
            {
                // string archivoDestino = interfazFiscal + "\\SALIDA\\" + pDocSelTipo + "_" + pDocSelPrefijo + "-" + pDocSelNumero + ".txt_S.txt";
                string archivoDestino = interfazFiscal + "\\SALIDA\\" + pDocSelTipo + "_" + pDocSelPrefijo + "-" + pDocSelNumero + ".txt_S";
                
                StreamReader tr = new StreamReader(archivoDestino);

                string trAllFile = tr.ReadToEnd();
                string[] trLines = trAllFile.Split(new char[] { '\n' });

                // FIRMA FISCAL
                // INTERFAZ FISCAL + "-" + CONSECUTIVO FISCAL DEL DOCUMENTO
                string firmaFiscal = trLines[0].Trim() + "-" + trLines[1].Trim();

                tr.Close();

                if (firmaFiscal != "-")
                {
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            catch (Exception e)
            {
                return false;
            }
        }


        // cambiar los parámetros de selección de documentos
        protected void botonCambiarParametros_Click(object sender, EventArgs e)
        {

            // muestra el panel de parámetros
            PanelParametros.Visible = true;

            // etiquetas de parámetros seleccionados
            etiquetaPrefijo.Text        = "";
            etiquetaInterfazFiscal.Text = "";

            // oculta el panel de parámetros seleccionados
            PanelParametrosSeleccionados.Visible = false;
            
            // desactiva y oculta el panel de documentos (vista principal)
            PanelDocumentos.Enabled = false;
            PanelDocumentos.Visible = false;

            // desactiva y oculta el panel de documento seleccionado
            PanelDocumentoSeleccionado.Enabled = false;
            PanelDocumentoSeleccionado.Visible = false;

            // fijación de parámetros
            campoPrefijoDocumento.Value = "";
            estadoInterfazFiscal.Value = "false";

        }
        // cambio de selección de tipo de documento (desde - hasta)
        protected void campoSeleccionTipoDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (campoSeleccionTipoDocumento.SelectedValue)
            {
                case "1":
                    campoTipoDocumentoDesde.Value = "1";
                    campoTipoDocumentoHasta.Value = "1";
                    break;
                case "2":
                    campoTipoDocumentoDesde.Value = "2";
                    campoTipoDocumentoHasta.Value = "2";
                    break;
                case "3":
                    campoTipoDocumentoDesde.Value = "3";
                    campoTipoDocumentoHasta.Value = "3";
                    break;
                default:
                    campoTipoDocumentoDesde.Value = "1";
                    campoTipoDocumentoHasta.Value = "3";
                    break;
            }

            // quitar la selección de documento
            GridViewDocumentos.SelectedIndex = -1;

            // regenerar la pantalla de documentos
            GenerarPantallaDocumentos();

        }
        // 
        protected void botonActualizarDocumentos_Click(object sender, EventArgs e)
        {
            GenerarPantallaDocumentos();
        }
        // verificar el estado de la interfaz fiscal
        protected void botonActualizarEstadoInterfazFiscal_Click(object sender, ImageClickEventArgs e)
        {
            GenerarPantallaDocumentos();
            // InterfazFiscalDetectarInterfaz();
        }

    }
}
