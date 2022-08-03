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

namespace SFCP108
{
    public partial class SFCP108 : System.Web.UI.Page
    {
        funciones_generales func = new funciones_generales();
        String mensaje = string.Empty;
        //string conexion = System.Configuration.ConfigurationManager.ConnectionStrings["connectStringPAN"].ConnectionString;
        public static int param, li_pais, filas = 0;
        string conexion, ls_userfile;
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
                mpe.Show();
            }

        }

        protected void img_salir_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            //mensaje = "Ejecutar UPDATE al FSO...";
            //ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mensaje + "');", true);
            //txt_orden.Attributes.Add("onkeydown", "if( event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + btnOK.UniqueID + "').click();return false;}} else {return true}; ");
            mpe.Hide();
            desbloquear_orden();

            //img_salir_Click(null, null);
        }

        protected void desbloquear_orden()
        {
            iDB2Command query;
            int res;
            int orden = func.convierte_string_int(txt_orden.Text);

            conexion = func.cadena_con(lbl_pais.Text);
            ls_userfile = func.userfile(lbl_pais.Text);
            iDB2Connection con = new iDB2Connection(conexion);
            con = new iDB2Connection(conexion);
            con.Open();

            query = new iDB2Command();
            query.CommandType = System.Data.CommandType.Text;

            query.CommandText = "UPDATE FSO SET SEINUS='0' WHERE SORD=" + orden + " AND SEINUS='1'";

            query.Connection = con;

            //res = new Object();
            res = query.ExecuteNonQuery();

            if (res != 0)
            //if (!(res is DBNull))
            {
                mensaje = "N° de Orden [" + orden + "] Desbloqueado Satisfactoriamente...";
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mensaje + "');", true);
                txt_orden.Text = "";
                mpe.Show();
            }
            else
            {
                mensaje = "N° Orden Ingresado No Existe o No hay registros en uso...";
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mensaje + "');", true);
                txt_orden.Text = "";
                mpe.Show();
            }
            con.Close();
        }
 
    }
}