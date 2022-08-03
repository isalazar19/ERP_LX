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

namespace SYSPC199
{
    public partial class SYSPC199 : System.Web.UI.Page
    {
        funciones_generales func = new funciones_generales();
        String mensaje = string.Empty;
        //string conexion = System.Configuration.ConfigurationManager.ConnectionStrings["connectStringPAN"].ConnectionString;
        public static int param, li_pais, filas;
        string conexion, fecha_desde, fecha_hasta, usuario;
        public String cmdtext, cia, ls_userfile;

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
                cia = Session["cia"].ToString();
                txt_fecha1.Text = Convert.ToString(DateTime.Today);
                txt_fecha2.Text = Convert.ToString(DateTime.Today);
            }
        }

        protected void img_salir_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        protected void txt_fecha1_TextChanged(object sender, EventArgs e)
        {
            fecha_desde = func.convierte_fecha_400(txt_fecha1.Text, 2);
            fecha_hasta = func.convierte_fecha_400(txt_fecha2.Text, 2);
        }

        protected void txt_fecha2_TextChanged(object sender, EventArgs e)
        {
            fecha_desde = func.convierte_fecha_400(txt_fecha1.Text, 2);
            fecha_hasta = func.convierte_fecha_400(txt_fecha2.Text, 2);
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            conexion = func.cadena_con(lbl_pais.Text);
            ls_userfile = func.userfile(lbl_pais.Text);

            cia = Session["cia"].ToString();
            fecha_desde = func.convierte_fecha_400(txt_fecha1.Text, 2);
            fecha_hasta = func.convierte_fecha_400(txt_fecha2.Text, 2);
            usuario = txt_user.Text;

            CALL_RPG(conexion, usuario, ls_userfile, "SYSPC199", cia, fecha_desde, fecha_hasta);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        protected void CALL_RPG(string particion, string usuario_spool, string userfile, string programa_RPG, string arg_cia, string arg_fecha1, string arg_fecha2)
        {
            // parametros
            string comando = String.Format("SBMJOB CMD(CALL PGM(" + userfile + "/" + programa_RPG + ") PARM('" + arg_cia + "' '" + arg_fecha1 + "' '" + arg_fecha2 + "')) JOB(" + programa_RPG + ") JOBQ(*LIBL/QBATCH) USER(" + usuario_spool + ")");
            //string comando = String.Format("CALL PGM(" + userfile + "/" + programa_RPG + ") PARM('" + arg_cia + "' '" + arg_fecha1 + "' '" + arg_fecha2 + "') JOB(" + programa_RPG + ") JOBQ(*LIBL/QBATCH) USER(" + usuario_spool + ")");

            // reportar mensaje
            // ReportarMensaje(String.Format("COMANDO A EJECUTAR: {0}", comando));


            // EJECUCION
            comandoAS400(comando, particion);

        }

        protected void comandoAS400(string comandoParam, string arg_particion)
        {
            string ls_particion, mensaje;
            cwbx.AS400System as400 = new cwbx.AS400System();
            
            ls_particion = arg_particion.Substring(11, 12);
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
                        mensaje = "VERIFIQUE EN SU SPOOL EL REPORTE GENERADO...";
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