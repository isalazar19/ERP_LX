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
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;

namespace ACRP598
{
    public partial class ACRP598 : System.Web.UI.Page
    {
        funciones_generales func = new funciones_generales();
        String mensaje = string.Empty;
        //string conexion = System.Configuration.ConfigurationManager.ConnectionStrings["connectStringPAN"].ConnectionString;
        public static int param, li_pais, filas;
        string ls_userfile, conexion, fecha_hasta, cliente, nom_clte, dir, tel, usuario, filt, miclte;
        public String cmdtext, cia;

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
                btn_buscar_Click(null, null);
            }
        }

        protected void img_salir_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        protected void btn_buscar_Click(object sender, ImageClickEventArgs e)
        {
            llenar_grid();
        }

        protected void gv_lista_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gv_lista.SelectedRow;

            cliente = row.Cells[1].Text;
            nom_clte = row.Cells[2].Text;
            Session["clte"] = cliente;
            Session["nom_clte"] = nom_clte;
            Session["pais"] = lbl_pais.Text;
            Session["fecha"] = txt_fecha1.Text;
            if (rb_detalle.Checked) filt = "1";
            if (rb_resumida.Checked) filt = "0";
            if (rb_natural.Checked) miclte = "N";
            if (rb_corporativo.Checked) miclte = "C";
            Session["filtro"] = filt;
            Session["customer"] = miclte;
            btn_buscar_Click(null, null);
            Response.Write("<script type='text/javascript'>window.open('EdoCtaClte.aspx','cal','width=1000,height=700,left=50,top=10,scrollbars=yes');</script>");
            //Response.Redirect("EdoCtaClte.aspx?="+cliente+"?="+nom_clte);
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

                fecha_hasta = func.convierte_fecha_400(txt_fecha1.Text, 2);

                /* QUERY PARA TRAER LOS DATOS POR CLIENTE NATURAL */
                if (rb_base.Checked && rb_natural.Checked) /*Ejecuta el QUERY para la moneda LOCAL*/
                {
                    cmdtext = "SELECT DISTINCT RARL02.RCUST, RCM.CNME, SUM(RAMT) AS RAMT FROM RARL02, RCM WHERE ( RARL02.RCUST = RCM.CCUST  and RARL02.RDATE <= " + fecha_hasta + ") GROUP BY RARL02.RCUST, RCM.CNME ORDER BY RARL02.RCUST ASC";
                }

                if (rb_transaccion.Checked && rb_natural.Checked)  /* Ejecuta el QUERY para la moneda EXTRANJERA  */
                {
                    cmdtext = "SELECT DISTINCT RARL02.RCUST, RCM.CNME, SUM(RAMT) AS RAMT FROM RARL02, RCM WHERE ( RARL02.RCUST = RCM.CCUST and RARL02.RDATE <= " + fecha_hasta + ") GROUP BY RARL02.RCUST, RCM.CNME ORDER BY RARL02.RCUST ASC";
                }
                /* QUERY PARA TRAER LOS DATOS POR CLIENTE CORPORATIVO */
                if (rb_base.Checked && rb_corporativo.Checked) /*Ejecuta el QUERY para la moneda LOCAL*/
                {
                    cmdtext = "SELECT DISTINCT RARL02.RCCUS, RCM.CNME, SUM(RAMT) as RAMT FROM RARL02, RCM WHERE ( RARL02.RCCUS = RCM.CCCUS  and RARL02.RCCUS = RCM.CCUST and RARL02.RDATE <= " + fecha_hasta + ") GROUP BY RARL02.RCCUS, RCM.CNME ORDER BY RARL02.RCCUS ASC";
                }
                if (rb_transaccion.Checked && rb_corporativo.Checked) /* Ejecuta el QUERY para la moneda EXTRANJERA  */
                {
                    cmdtext = "SELECT DISTINCT RARL02.RCCUS, RCM.CNME, SUM(RAMT) as RAMT FROM RARL02, RCM WHERE ( RARL02.RCCUS = RCM.CCCUS  and RARL02.RCCUS = RCM.CCUST and RARL02.RDATE <= " + fecha_hasta + ") GROUP BY RARL02.RCCUS, RCM.CNME ORDER BY RARL02.RCCUS ASC";
                }
                
                gv_lista.DataKeyNames = new string[] { "clte" };

                iDB2DataAdapter da = new iDB2DataAdapter("" + cmdtext, connDB2);
                connDB2.Open();

                DataTable dt = new DataTable();
                da.Fill(dt);

                System.Data.DataTable tabla = new DataTable();
                DataColumn col = new DataColumn("clte");
                tabla.Columns.Add(col);

                DataColumn col2 = new DataColumn("nom_clte");
                tabla.Columns.Add(col2);

                DataColumn col3 = new DataColumn("saldo");
                tabla.Columns.Add(col3);

                //DataColumn col4 = new DataColumn("dire");
                //tabla.Columns.Add(col4);

                //DataColumn col5 = new DataColumn("tele");
                //tabla.Columns.Add(col5);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = tabla.NewRow();
                    Session["source_table"] = dt;

                    if (rb_natural.Checked)
                    {
                        dr["clte"] = dt.Rows[i]["RCUST"].ToString();
                    }
                    else
                    {
                        dr["clte"] = dt.Rows[i]["RCCUS"].ToString();
                    }
                    dr["nom_clte"] = dt.Rows[i]["CNME"].ToString();
                    dr["saldo"] = dt.Rows[i]["RAMT"].ToString();
                    //dr["dire"] = dt.Rows[i]["DIR"].ToString();
                    //dr["tele"] = dt.Rows[i]["CPHON"].ToString();

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

        decimal Total = 0;
        protected void gv_lista_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Total += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "saldo"));
                }
                else if (e.Row.RowType == DataControlRowType.Footer)
                {
                    e.Row.Cells[2].Text = "TOTAL ---->";
                    e.Row.Cells[3].Text = Total.ToString("N2");
                    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Left;
                    e.Row.Font.Bold = true;
                    Total = 0;
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mensaje + "');", true);
            }
        }

        protected void rb_detalle_CheckedChanged(object sender, EventArgs e)
        {
            btn_buscar_Click(null, null);
        }

        protected void rb_resumida_CheckedChanged(object sender, EventArgs e)
        {
            btn_buscar_Click(null, null);
        }

        protected void rb_natural_CheckedChanged(object sender, EventArgs e)
        {
            btn_buscar_Click(null, null);
        }

        protected void rb_corporativo_CheckedChanged(object sender, EventArgs e)
        {
            btn_buscar_Click(null, null);
        }

        protected void txt_fecha1_TextChanged(object sender, EventArgs e)
        {
            gv_lista.DataBind();
            btn_buscar_Click(null, null);
        }

    }
}