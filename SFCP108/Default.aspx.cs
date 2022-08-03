using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IBM.Data.DB2.iSeries;

namespace SFCP108
{
    public partial class _Default : System.Web.UI.Page
    {
        String mensaje = string.Empty;
        string conexion;
        public static int param, li_pais;
        public static string cia, usuario;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void img_salir_Click(object sender, ImageClickEventArgs e)
        {
            Response.Write(" <script> window.open('','_parent',''); window.close(); </script>");
        }

        protected void Selection_Change(object sender, EventArgs e)
        {
            int li_pais;
            li_pais = ddl_pais.SelectedIndex;

            switch (li_pais)
            {
                case 1:   /*COLOMBIA*/
                    Session["pais"] = "COLOMBIA";
                    conexion = System.Configuration.ConfigurationManager.ConnectionStrings["connectStringCO"].ConnectionString;
                    break;

                case 2:  /*GUATEMALA*/
                    Session["pais"] = "GUATEMALA";
                    conexion = System.Configuration.ConfigurationManager.ConnectionStrings["connectStringGU"].ConnectionString;
                    break;

                case 3:   /*PANAMA*/
                    Session["pais"] = "PANAMA";
                    conexion = System.Configuration.ConfigurationManager.ConnectionStrings["connectStringPAN"].ConnectionString;
                    break;

                case 4:  /*PERU*/
                    Session["pais"] = "PERU";
                    conexion = System.Configuration.ConfigurationManager.ConnectionStrings["connectStringPE"].ConnectionString;
                    break;

                case 5:   /*TRINIDAD & TOBAGO*/
                    Session["pais"] = "TRINIDAD & TOBAGO";
                    conexion = System.Configuration.ConfigurationManager.ConnectionStrings["connectStringTT"].ConnectionString;
                    break;

                case 6:  /*VENEZUELA*/
                    Session["pais"] = "VENEZUELA";
                    conexion = System.Configuration.ConfigurationManager.ConnectionStrings["connectStringVE"].ConnectionString;
                    break;

                case 7: /*PROTOTIPO¨*/
                    Session["pais"] = "PROTOTIPO";
                    conexion = System.Configuration.ConfigurationManager.ConnectionStrings["connectStringDES"].ConnectionString;
                    break;

            }
            Session.Timeout = 500;
            lbl_cia.Visible = true;
            ddl_cia.Visible = true;
            ddl_cia.DataBind();
            llenar_ddlcia();
            ddl_cia_Selection_Change(null, null);
        }

        protected void ddl_cia_Selection_Change(object sender, EventArgs e)
        {
            Session["cia"] = ddl_cia.SelectedValue.ToString();
            Response.Redirect("SFCP108.aspx");
        }

        protected void llenar_ddlcia()
        {
            //Llenar DropDown de Cia
            try
            {
                iDB2Connection con = new iDB2Connection(conexion);
                string cmdtext = " SELECT CMPNY,CMPNY||'-'||CMPNAM AS micomp FROM RCOL01 ORDER BY CMPNY ";

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

                    this.ddl_cia.DataSource = ds;
                    this.ddl_cia.DataValueField = "CMPNY";
                    this.ddl_cia.DataTextField = "micomp";
                    this.ddl_cia.DataBind();
                    //lbl_pfx.Text = ddl_pfx.SelectedValue.ToString();
                    //this.ddl_cia.Items.Insert(0, new ListItem("<--elija opcion-->", "0"));
                    this.ddl_cia.Items.Insert(0, new ListItem("", " "));
                    ddl_cia.SelectedIndex = 1;
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
    }
}
