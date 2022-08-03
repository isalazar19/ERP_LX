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

namespace ORDP58
{
    public partial class ORDP58 : System.Web.UI.Page
    {
        funciones_generales func = new funciones_generales();
        String mensaje = string.Empty;
        //string conexion = System.Configuration.ConfigurationManager.ConnectionStrings["connectStringPAN"].ConnectionString;
        public static int param, li_pais, filas;
        string conexion, almacen, deposito, tipoClte, vendedor, producto_desde, producto_hasta, fecha_desde, fecha_hasta, usuario;
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
                llenar_ddldep();
                llenar_ddlalm();
                llenar_tipoClte();
                llenar_vendedor();
                llenar_producto1();
                llenar_producto2();
                //Activar la Ventana Popup Modal
                mpe.Show();
                imgOK.OnClientClick = String.Format("Aceptar(‘{0}’,'{1}’)", imgOK.UniqueID, "");
            }
        }

        protected void img_salir_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        protected void llenar_ddldep()
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

                    this.ddl_dep.DataSource = ds;
                    this.ddl_dep.DataValueField = "LWHS";
                    this.ddl_dep.DataTextField = "mialm";
                    this.ddl_dep.DataBind();
                    //lbl_pfx.Text = ddl_pfx.SelectedValue.ToString();
                    //this.ddl_cia.Items.Insert(0, new ListItem("<--elija opcion-->", "0"));
                    this.ddl_dep.Items.Insert(0, new ListItem("", " "));

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

        protected void llenar_tipoClte()
        {
            //SELECT CTCSTP, CTDESC FROM v82bpcsf/rct WHERE CTCOMP = 10 
            try
            {
                conexion = func.cadena_con(lbl_pais.Text);
                iDB2Connection con = new iDB2Connection(conexion);
                string cmdtext = "SELECT CTCSTP, CTCSTP||'-'||CTDESC AS mitipoClte FROM RCTL01 WHERE CTCOMP = " + Session["cia"].ToString();

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

                    this.ddl_tipoClte.DataSource = ds;
                    this.ddl_tipoClte.DataValueField = "CTCSTP";
                    this.ddl_tipoClte.DataTextField = "mitipoClte";
                    this.ddl_tipoClte.DataBind();
                    //lbl_pfx.Text = ddl_pfx.SelectedValue.ToString();
                    //this.ddl_cia.Items.Insert(0, new ListItem("<--elija opcion-->", "0"));
                    this.ddl_tipoClte.Items.Insert(0, new ListItem("", " "));

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

        protected void llenar_vendedor()
        {
            /*
         SELECT  SSM.SSAL , 
              SSM.SNAME ,
              SSM.SMCOMP
         FROM SSM
          WHERE ( SSM.SMCOMP = :cia )   
             * */
            try
            {
                conexion = func.cadena_con(lbl_pais.Text);
                iDB2Connection con = new iDB2Connection(conexion);
                string cmdtext = "SELECT SSAL, SSAL||'-'||SNAME AS mivend FROM SSML01 WHERE SMCOMP = " + Session["cia"].ToString() + " ORDER BY SSAL";

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

                    this.ddl_vend.DataSource = ds;
                    this.ddl_vend.DataValueField = "SSAL";
                    this.ddl_vend.DataTextField = "mivend";
                    this.ddl_vend.DataBind();
                    //lbl_pfx.Text = ddl_pfx.SelectedValue.ToString();
                    //this.ddl_cia.Items.Insert(0, new ListItem("<--elija opcion-->", "0"));
                    this.ddl_vend.Items.Insert(0, new ListItem("", " "));

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

        protected void llenar_producto1()
        {
            try
            {
                conexion = func.cadena_con(lbl_pais.Text);
                iDB2Connection con = new iDB2Connection(conexion);
                string cmdtext = "SELECT IPROD, IPROD||'-'||IDESC AS miprod FROM IIML01 ORDER BY IPROD";

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

                    this.ddl_prod1.DataSource = ds;
                    this.ddl_prod1.DataValueField = "IPROD";
                    this.ddl_prod1.DataTextField = "miprod";
                    this.ddl_prod1.DataBind();
                    //lbl_pfx.Text = ddl_pfx.SelectedValue.ToString();
                    //this.ddl_cia.Items.Insert(0, new ListItem("<--elija opcion-->", "0"));
                    this.ddl_prod1.Items.Insert(0, new ListItem("", " "));

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

        protected void llenar_producto2()
        {
            try
            {
                conexion = func.cadena_con(lbl_pais.Text);
                iDB2Connection con = new iDB2Connection(conexion);
                string cmdtext = "SELECT IPROD, IPROD||'-'||IDESC AS miprod FROM IIML01 ORDER BY IPROD";

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

                    this.ddl_prod2.DataSource = ds;
                    this.ddl_prod2.DataValueField = "IPROD";
                    this.ddl_prod2.DataTextField = "miprod";
                    this.ddl_prod2.DataBind();
                    //lbl_pfx.Text = ddl_pfx.SelectedValue.ToString();
                    //this.ddl_cia.Items.Insert(0, new ListItem("<--elija opcion-->", "0"));
                    this.ddl_prod2.Items.Insert(0, new ListItem("", " "));

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

        protected void Image1_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void ddl_dep_Selection_Change(object sender, EventArgs e)
        {
            deposito = ddl_dep.SelectedValue.ToString();
        }

        protected void ddl_alm_Selection_Change(object sender, EventArgs e)
        {
            almacen = ddl_alm.SelectedValue.ToString();
        }

        protected void ddl_tipo_Clte_Selection_Change(object sender, EventArgs e)
        {
            tipoClte = ddl_tipoClte.SelectedValue.ToString();
        }

        protected void ddl_vend_Selection_Change(object sender, EventArgs e)
        {
            vendedor = ddl_vend.SelectedValue.ToString();
        }

        protected void ddl_prod1_Selection_Change(object sender, EventArgs e)
        {
            producto_desde = ddl_prod1.SelectedValue.ToString();
        }

        protected void ddl_prod2_SelectedIndexChanged(object sender, EventArgs e)
        {
            producto_hasta = ddl_prod2.SelectedValue.ToString();
        }

        protected void btnGrid_Click(object sender, EventArgs e)
        {
            //Convertir la fecha a Formato AS400
            fecha_desde = txt_fecha1.Text;
            if (fecha_desde != null && fecha_desde != "")
                fecha_desde = func.convierte_fecha_400(fecha_desde, 2);
            fecha_hasta = txt_fecha2.Text;
            if (fecha_hasta != null && fecha_hasta != "")
                fecha_hasta = func.convierte_fecha_400(fecha_hasta, 2);
            //lbl_parametros.Text = fecha_desde + "|" + fecha_hasta + "|" + deposito + "|" + almacen + "|" + tipoClte + "|" + vendedor;
        }

        protected void btnCncl_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

    }
}