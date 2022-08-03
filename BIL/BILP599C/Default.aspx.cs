using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BILP599C
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // rango de fechas por defecto (período actual)
            if (campoPeriodoDesde.Text == "")
            {
                DateTime hoy = DateTime.Now;

                campoPeriodoDesde.Text = hoy.ToString("dd/MM/yyyy");
            }
            if (campoPeriodoHasta.Text == "")
            {
                DateTime hoy = DateTime.Now;

                campoPeriodoHasta.Text = hoy.ToString("dd/MM/yyyy");
            }

            // AJUSTE DE CONNECTION STRINGS SEGUN LA SELECCION DE PAÍS
            if (campoSeleccionDeParticion.SelectedItem.Value.ToString() != "-1")
            {
                // connection string a utilizar
                string cs = System.Configuration.ConfigurationManager.ConnectionStrings[campoSeleccionDeParticion.SelectedItem.Value.ToString()].ToString();

                // asignación del CS a los DataSource
                //// lista de compañías
                SqlDataSourceListaCompanias.ConnectionString = cs;
                //// lista de almacenes
                SqlDataSourceListaAlmacenes.ConnectionString = cs;
            }
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

        // SELECCION DE PAIS
        protected void campoSeleccionDeParticion_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Ocultar el panel de partición
            panelSeleccionDeParticion.Visible = false;

            // mostrar el panel de parámetros
            panelParametros.Enabled = true;
            panelParametros.Visible = true;
            // establecer la etiqueta que muestra la partición
            etiquetaParticionSeleccionada.Text = campoSeleccionDeParticion.SelectedItem.Text.ToString();

            // panel de parámetros
            campoSeleccionDeCompania.SelectedIndex = -1;
            campoSeleccionAlmacen.SelectedIndex = -1;

            // repoblación de campos de selección
            campoSeleccionDeCompania.DataBind();
            campoSeleccionAlmacen.DataBind();
        }

        // CAMBIO DE PAÍS
        protected void botonCambiarParticion_Click(object sender, EventArgs e)
        {
            // mostrar el panel de partición
            panelSeleccionDeParticion.Visible = true;
            //// resetear la selección de partición
            campoSeleccionDeParticion.ClearSelection();

            // panel de parametros
            panelParametros.Enabled = false;
            panelParametros.Visible = false;
            //// establecer la etiqueta que muestra la Partición
            etiquetaParticionSeleccionada.Text = "";
            //// selección de compañía
            campoSeleccionDeCompania.ClearSelection();
            //// periodo desde
            campoPeriodoDesde.Text = "";
            //// periodo hasta
            campoPeriodoHasta.Text = "";
            //// spool destino
            campoSpoolDestino.Text = "";

            // botón de ejecución
            botonEjecutarProceso.Enabled = true;
            botonEjecutarProceso.Visible = true;
            // botón de ejecutar otro
            botonEjecutarOtro.Enabled = false;
            botonEjecutarOtro.Visible = false;
        }

        // construccion del campo de seleccion de compañía
        protected void campoSeleccionDeCompania_DataBound(object sender, EventArgs e)
        {
            // ajuste de etiquetas
            if (campoSeleccionDeCompania.Items.Count > 0)
            {
                for (int i = 0; i < campoSeleccionDeCompania.Items.Count; i++)
                {
                    campoSeleccionDeCompania.Items[i].Text = campoSeleccionDeCompania.Items[i].Value + " - " + campoSeleccionDeCompania.Items[i].Text;
                }
            }
        }

        // ejecucion de un comando en el AS400
        protected void comandoAS400(string comandoParam)
        {

            // DEFINICIONES PREVIAS

            // sistema
            //// definición
            cwbx.AS400System as400 = new cwbx.AS400System();

            //// sistema a aconectarse
            //// ajustes segun la conexion
            switch (campoSeleccionDeParticion.SelectedItem.Value.ToString())
            {
                case "DNetDesarrollErpDE":
                    as400.Define("APPN.DESARROL");
                    as400.IPAddress = "10.0.1.60";
                    break;
                case "DNetPrototipoErpPA":
                    as400.Define("APPN.CENTROA");
                    as400.IPAddress = "10.0.103.2";
                    break;
                default:
                    as400.Define("APPN.DESARROL");
                    as400.IPAddress = "10.0.1.60";
                    break;
            }

            //// usuario de conexion al sistema
            as400.UserID = "ADONET";
            as400.Password = "ADONET01";


            // PROCESO

            // levantar la conexión
            as400.Connect(cwbx.cwbcoServiceEnum.cwbcoServiceRemoteCmd);

            // chequear la conexión
            if (as400.IsConnected(cwbx.cwbcoServiceEnum.cwbcoServiceRemoteCmd) == 1)
            {

                // reportar mensaje
                ReportarMensaje("SISTEMA CONECTADO");

                // definición de comando
                cwbx.Command comando = new cwbx.Command();
                comando.system = as400;

                // ejecución del comando
                try
                {
                    // ejecución
                    comando.Run(comandoParam);

                    // reportar mensaje
                    ReportarMensaje("COMANDO ENVIADO");

                    // sin errores
                    if (comando.Errors.Count == 0)
                    {
                        // reportar mensaje
                        ReportarMensaje("COMANDO EJECUTADO EXITOSAMENTE");

                        // botón de ejecución
                        botonEjecutarProceso.Enabled = false;
                        botonEjecutarProceso.Visible = false;
                        // botón de ejecutar otro
                        botonEjecutarOtro.Enabled = true;
                        botonEjecutarOtro.Visible = true;

                    }
                }
                catch (Exception)
                {
                    if (comando.Errors.Count > 0)
                    {
                        foreach (cwbx.Error error in comando.Errors)
                        {
                            // reportar advertencia
                            ReportarAdvertencia("ERROR EN LA EJECUCIÓN DEL COMANDO: " + error.Text);
                        }
                    }
                }

            }

            // desconexión
            as400.Disconnect(cwbx.cwbcoServiceEnum.cwbcoServiceAll);

        }

        // ejecutar el proceso
        protected void botonEjecutarProceso_Click(object sender, EventArgs e)
        {

            // ajustes previos de parámetros
            //// spool destino
            campoSpoolDestino.Text = campoSpoolDestino.Text.ToUpper();


            // PROCESO

            // libreria por defecto
            string libreria = "V82BPCSUSF";

            // ajustes segun la conexion
            switch (campoSeleccionDeParticion.SelectedItem.Value.ToString())
            {
                case "DNetDesarrollErpDE":
                    // libreria
                    libreria = "V82BPCSUSF";
                    break;
                case "DNetPrototipoErpPA":
                    // libreria
                    libreria = "PANLXUSRF";
                    break;
            }

            // parametros
            //// periodo ini
            string periodoIni = campoPeriodoDesde.Text.Substring(6, 4) + campoPeriodoDesde.Text.Substring(3, 2) + campoPeriodoDesde.Text.Substring(0, 2);
            //// periodo fin
            string periodoFin = campoPeriodoHasta.Text.Substring(6, 4) + campoPeriodoHasta.Text.Substring(3, 2) + campoPeriodoHasta.Text.Substring(0, 2);

            //// spool destino
            string spool = campoSpoolDestino.Text;

            // construccion del comando
            string comando = String.Format("SBMJOB CMD(CALL PGM({0}/BILP599C) PARM('{1}' '{2}' '{3}' '{4}' '{5}')) JOB(BILP599C) JOBQ(*LIBL/QBATCH) USER({6})", libreria, campoSeleccionDeCompania.SelectedValue, periodoIni, periodoFin, campoSeleccionAlmacen.SelectedValue.Trim(), CampoReporteAGenerar.SelectedValue, spool);


            // reportar mensaje
            //ReportarMensaje(String.Format("COMANDO A EJECUTAR: {0}", comando));


            // EJECUCION
            comandoAS400(comando);

        }

    }
}
