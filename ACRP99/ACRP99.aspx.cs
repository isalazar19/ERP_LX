using System;
using System.Globalization;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IBM.Data.DB2.iSeries;
using cwbx;

namespace ACRP99
{
    public partial class ACRP99 : System.Web.UI.Page
    {
        funciones_generales func = new funciones_generales();
        String mensaje = string.Empty;
        //string conexion = System.Configuration.ConfigurationManager.ConnectionStrings["connectStringPAN"].ConnectionString;
        public static int param, li_pais, filas;
        string conexion, almacen, pedido, nota, cantidad, nro_placa, usuario;
        public String cmdtext;

        protected void Page_Load(object sender, EventArgs e)
        {
            string idioma;
            idioma = System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
            switch (idioma)
            {
                case "es": // Sets the CurrentCulture property to VEN Spanish.
                    Thread.CurrentThread.CurrentCulture = new CultureInfo("es-VE");
                    break;

                case "en": // Sets the CurrentCulture property to U.S. English.
                    Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                    break;
            }

            if (!IsPostBack)
            {
                if (Session["pais"] == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                lbl_pais.Text = Session["pais"].ToString();
                lbl_cia.Text = " - " + Session["cia"].ToString();
                llenar_ddlalm();
            }
        }

        protected void img_salir_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        protected void llenar_ddlalm()
        {
            try
            {
                conexion = func.cadena_con(lbl_pais.Text);
                iDB2Connection con = new iDB2Connection(conexion);
                string cmdtext = "SELECT LWHS, LWHS||'-'||LDESC AS mialm FROM IWML01 ORDER BY LWHS";

                iDB2Command cmd = con.CreateCommand();
                cmd.CommandText = cmdtext;

                DataSet ds = new DataSet();
                iDB2DataAdapter da = new iDB2DataAdapter(cmd);

                this.Title = con.DataSource;
                if (con.State.ToString() == "Closed")
                {
                    con.Open();
                    da.Fill(ds);


                    //txt_mensaje.Text = ds.Tables[0].Rows[1]["CMPNAM"].ToString();

                    this.ddl_alm.DataSource = ds;
                    this.ddl_alm.DataValueField = "LWHS";
                    this.ddl_alm.DataTextField = "mialm";
                    this.ddl_alm.DataBind();
                    //lbl_pfx.Text = ddl_pfx.SelectedValue.ToString();
                    //this.ddl_cia.Items.Insert(0, new ListItem("<--elija opcion-->", "0"));
                    this.ddl_alm.Items.Insert(0, new ListItem("", " "));

                    //txt_mensaje.Text = ddl_cia.SelectedValue = ds.Tables[0].Rows[0]["CMPNAM"].ToString();

                    da.Dispose();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mensaje + "');", true);
            }
        }

        protected void ddl_whs_Selection_Changed(object sender, EventArgs e)
        {

        }

        protected void btn_buscar_Click(object sender, ImageClickEventArgs e)
        {
            almacen = ddl_alm.SelectedValue.ToString();
            llenar_grid();
        }

        protected void gv_lista_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl_parametros.Text = "";
            GridViewRow row = gv_lista.SelectedRow;

            almacen = row.Cells[0].Text;
            pedido = row.Cells[1].Text;
            nota = row.Cells[2].Text;
            cantidad = row.Cells[3].ToString();
            nro_placa = row.Cells[4].Text;
            lbl_nota.Text = nota;
            lbl_pedi.Text = pedido;
            //Activar la Ventana Popup Modal
            mpe.Show();
            llenar_ddlplaca(null, null);

        }

        protected void gv_lista_Paginar(object sender, GridViewPageEventArgs e)
        {
            GridView gv_lista = (GridView)sender;
            gv_lista.PageIndex = e.NewPageIndex;
            btn_buscar_Click(null, null);
            gv_lista.DataBind();
            //ocultar();
        }

        protected void llenar_grid()
        {
            try
            {
                conexion = func.cadena_con(lbl_pais.Text);
                iDB2Connection connDB2 = new iDB2Connection(conexion);
                cmdtext = "SELECT PPWHS," +
                          " PPDREF," +
                          " PPLSTN," +
                          " sum(PPQTY5) AS PPQTY5," +
                          " PPCARR " +
                          "FROM IPP " +
                          //"WHERE PPID='PP' AND PPTYPE=1 AND PPWHS='" + almacen + "' AND PPPSPR=1 AND PPCARR=' ' GROUP BY PPWHS, PPDREF, PPLSTN, PPCARR ORDER BY PPLSTN DESC";
                          "WHERE PPID='PP' AND PPTYPE=1 AND PPWHS='" + almacen + "' AND PPPSPR=1 GROUP BY PPWHS, PPDREF, PPLSTN, PPCARR ORDER BY PPLSTN DESC";

                gv_lista.DataKeyNames = new string[] { "alm", "ped", "nota_carga" };

                iDB2DataAdapter da = new iDB2DataAdapter("" + cmdtext, connDB2);
                connDB2.Open();

                DataTable dt = new DataTable();
                da.Fill(dt);

                System.Data.DataTable tabla = new DataTable();
                DataColumn col = new DataColumn("alm");
                tabla.Columns.Add(col);

                DataColumn col2 = new DataColumn("ped");
                tabla.Columns.Add(col2);

                DataColumn col3 = new DataColumn("nota_carga");
                tabla.Columns.Add(col3);

                DataColumn col4 = new DataColumn("cant");
                tabla.Columns.Add(col4);

                DataColumn col5 = new DataColumn("placa");
                tabla.Columns.Add(col5);

                //DataColumn col6 = new DataColumn("und");
                //tabla.Columns.Add(col6);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = tabla.NewRow();

                    dr["alm"] = dt.Rows[i]["PPWHS"].ToString();
                    dr["ped"] = dt.Rows[i]["PPDREF"].ToString();
                    dr["nota_carga"] = dt.Rows[i]["PPLSTN"].ToString();
                    dr["cant"] = dt.Rows[i]["PPQTY5"].ToString();
                    dr["placa"] = dt.Rows[i]["PPCARR"].ToString();
                    //dr["und"] = dt.Rows[i]["RIUM03"].ToString();

                    //ddl_tipo.Items.Insert(0, new ListItem("<--elija opcion-->", "0"));
                    tabla.Rows.Add(dr);
                }

                connDB2.Close();
                gv_lista.DataSource = tabla;
                gv_lista.DataBind();

            }

            catch (Exception ex)
            {
                mensaje = ex.Message;
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mensaje + "');", true);
            }
        }

        protected void ddl_placa_Selection_Change(object sender, EventArgs e)
        {
            string cadena_parametros, ls_userfile;

            conexion = func.cadena_con(lbl_pais.Text);
            ls_userfile = func.userfile(lbl_pais.Text);

            nro_placa = ddl_placa.SelectedValue.ToString().Trim();
            almacen = ddl_alm.SelectedValue.ToString();
            nota = lbl_nota.Text;
            pedido = lbl_pedi.Text;
            cadena_parametros = nro_placa + "," + almacen + "," + nota + "," + pedido;
            usuario = txt_user.Text;
            lbl_parametros.Text = cadena_parametros;
            //
            CALL_RPG(conexion, usuario, ls_userfile, "ORDP550B", nro_placa, almacen, nota, pedido);
            //mensaje = "Enviando Parametros al Spool...";
            //ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mensaje + "');", true);
            lbl_parametros.Visible = false;
        }

        protected void llenar_ddlplaca(object sender, EventArgs e)
        {
            try
            {
                string ls_userfile;
                conexion = func.cadena_con(lbl_pais.Text);
                ls_userfile = func.userfile(lbl_pais.Text);
                iDB2Connection connDB2 = new iDB2Connection(conexion);

                string cmdtext = "SELECT " + ls_userfile + ".LEML99.EMEQID," + ls_userfile + ".LEML99.EMEQID||'-'|| " + ls_userfile + ".LEML99.EMDESC AS miplaca FROM " + ls_userfile + ".LEML99 ORDER BY " + ls_userfile + ".LEML99.EMEQID";
                iDB2Command cmd = connDB2.CreateCommand();
                cmd.CommandText = cmdtext;

                DataSet ds = new DataSet();
                iDB2DataAdapter da = new iDB2DataAdapter(cmd);

                this.Title = connDB2.DataSource;
                if (connDB2.State.ToString() == "Closed")
                {
                    connDB2.Open();
                    da.Fill(ds);

                    //txt_mensaje.Text = ds.Tables[0].Rows[1]["CMPNAM"].ToString();

                    this.ddl_placa.DataSource = ds;
                    this.ddl_placa.DataValueField = "EMEQID";
                    this.ddl_placa.DataTextField = "miplaca";
                    this.ddl_placa.DataBind();
                    //lbl_pfx.Text = ddl_pfx.SelectedValue.ToString();
                    //this.ddl_cia.Items.Insert(0, new ListItem("<--elija opcion-->", "0"));
                    this.ddl_placa.Items.Insert(0, new ListItem("", " "));

                    //txt_mensaje.Text = ddl_cia.SelectedValue = ds.Tables[0].Rows[0]["CMPNAM"].ToString();

                    da.Dispose();
                    connDB2.Close();
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mensaje + "');", true);
            }
        }

        protected void CALL_RPG(string particion, string usuario_spool, string userfile, string programa_RPG, string arg_placa, string arg_almacen, string arg_nota, string arg_pedido)
        {
            // parametros
            //string comando = String.Format("SBMJOB CMD(CALL PGM({0}/CSTP507C) PARM('{1}' '{2}' '1' '1' '{3}' '{4}')) JOB(CSTP507C) USER({5})", libreria, ini, fin, instalacion, compania, spool);
            string comando = String.Format("SBMJOB CMD(CALL PGM(" + userfile + "/" + programa_RPG + ") PARM('" + arg_placa + "' '" + arg_almacen + "' '" + arg_nota + "' '" + arg_pedido + "')) JOB(" + programa_RPG + ") JOBQ(*LIBL/QBATCH) USER(" + usuario_spool + ")");
            
            // reportar mensaje
            // ReportarMensaje(String.Format("COMANDO A EJECUTAR: {0}", comando));


            // EJECUCION
            comandoAS400(comando, particion);
           
        }

        protected void comandoAS400(string comandoParam, string arg_particion)
        {
            string ls_particion, mensaje;
            cwbx.AS400System as400 = new cwbx.AS400System();

            ls_particion = arg_particion.Substring(11, 12); //Para que tome del Web.config APPN.CENTROA
            as400.Define(ls_particion);

            as400.UserID = "ADONET";
            as400.Password = "ADONET01";

            // levantar la conexión
            as400.Connect(cwbx.cwbcoServiceEnum.cwbcoServiceRemoteCmd);

            // chequear la conexión
            if (as400.IsConnected(cwbx.cwbcoServiceEnum.cwbcoServiceRemoteCmd) == 1)
            {
                // reportar mensaje
                mensaje = "SISTEMA CONECTADO...";
                //ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mensaje + "');", true);
                // definición de comando
                cwbx.Command comando = new cwbx.Command();
                comando.system = as400;

                // ejecución del comando
                try
                {
                    // ejecución
                    comando.Run(comandoParam);

                    // reportar mensaje
                    mensaje = "COMANDO ENVIADO";

                    // sin errores
                    if (comando.Errors.Count == 0)
                    {
                        // reportar mensaje
                        mensaje = "COMANDO EJECUTADO EXITOSAMENTE";
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mensaje + "');", true);

                    }

                }

                catch (Exception)
                {
                    if (comando.Errors.Count > 0)
                    {
                        foreach (cwbx.Error error in comando.Errors)
                        {
                            // reportar advertencia
                            mensaje = "ERROR EN LA EJECUCIÓN DEL COMANDO: " + error.Text;
                            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mensaje + "');", true);
                        }
                    }

                }
            }

            // desconexión
            as400.Disconnect(cwbx.cwbcoServiceEnum.cwbcoServiceAll);

        }


    }
}