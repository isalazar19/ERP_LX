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


namespace ACRP135
{
    public partial class ACRP135 : System.Web.UI.Page
    {
        funciones_generales func = new funciones_generales();
        String mensaje = string.Empty;
        //string conexion = System.Configuration.ConfigurationManager.ConnectionStrings["connectStringPAN"].ConnectionString;
        public static int param, li_pais, filas;
        string ls_userfile, conexion, fecha_desde, fecha_hasta, vend_desde, vend_hasta, prefijo, usuario;
        public String cmdtext, cia;

        protected void Page_Load(object sender, EventArgs e)
        {
            string idioma, nueva_fecha;
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
                llenar_ddlpfx();
                fecha_desde = Convert.ToString(DateTime.Today);
                nueva_fecha = "01" + "/" + fecha_desde.Substring(3, 2) + "/" + fecha_desde.Substring(6, 4);
                txt_fecha1.Text = nueva_fecha;
                txt_fecha2.Text = Convert.ToString(DateTime.Today);

                //btn_buscar.Visible = false;
                //btn_buscar_Click(null, null);
            }
            
        }

        protected void img_salir_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        protected void llenar_ddlpfx()
        {
            try
            {
                conexion = func.cadena_con(lbl_pais.Text);
                iDB2Connection con = new iDB2Connection(conexion);
                string cmdtext = "SELECT DSDPFX, DSDPFX||'-'||DSDESC AS mipref FROM RDSL01 WHERE DSSSID = 'R' AND DSCMPY=" + @cia ;

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

                    this.ddl_pfx.DataSource = ds;
                    this.ddl_pfx.DataValueField = "DSDPFX";
                    this.ddl_pfx.DataTextField = "mipref";
                    this.ddl_pfx.DataBind();
                    //lbl_pfx.Text = ddl_pfx.SelectedValue.ToString();
                    //this.ddl_cia.Items.Insert(0, new ListItem("<--elija opcion-->", "0"));
                    this.ddl_pfx.Items.Insert(0, new ListItem("", " "));
                    ddl_pfx.SelectedIndex = 5;
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

        protected void Selection_Change_pfx(object sender, EventArgs e)
        {
            prefijo = ddl_pfx.SelectedValue.ToString();
        }

        protected void btn_buscar_Click(object sender, ImageClickEventArgs e)
        {
            prefijo = ddl_pfx.SelectedValue.ToString();
            llenar_grid();
        }

        //protected void gv_lista_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //}

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
                ls_userfile = func.userfile(lbl_pais.Text);
                iDB2Connection connDB2 = new iDB2Connection(conexion);
                
                fecha_desde = func.convierte_fecha_400(txt_fecha1.Text, 2);
                fecha_hasta = func.convierte_fecha_400(txt_fecha2.Text, 2);
                if(prefijo=="")
                {
                    prefijo="%";
                }
                vend_desde = txt_vend1.Text;
                if (vend_desde == "")
                {
                    vend_desde = "000000";
                }
                vend_hasta = txt_vend2.Text;
                if (vend_hasta == "")
                {
                    vend_hasta = "999999";
                }

                cmdtext = 
                        "SELECT " + ls_userfile + ".RARL5RM.RDATE," +   
                                 ls_userfile + ".RARL5RM.ARDOCN," +   
                                 ls_userfile + ".RARL5RM.RINVC," +   
                                 ls_userfile + ".RARL5RM.RCUST," +   
                                 "RCML01.CNME," +   
                                 ls_userfile + ".RARL5RM.RSAL," +   
                                 ls_userfile + ".RARL5RM.LOCAL," +   
                                 ls_userfile + ".RARL5RM.SIQTY," +   
                                 ls_userfile + ".RARL5RM.SICARR," +   
                                 ls_userfile + ".RARL5RM.TRES," +
                                 "ZPAL01.DATA AS RAZON " +
                            "FROM " + ls_userfile + ".RARL5RM, " +   
                                 "RCML01, ZPAL01 " + 
                           "WHERE ( " + ls_userfile + ".RARL5RM.RCUST = RCML01.CCUST ) and " + 
                                 "( ( " + ls_userfile + ".RARL5RM.RDATE between " + fecha_desde + " AND " + fecha_hasta + " ) AND " + 
                                 "( " + ls_userfile + ".RARL5RM.ARDPFX LIKE  '" + prefijo + "' ) AND " +
                                 "( " + ls_userfile + ".RARL5RM.RSAL between " + vend_desde + " AND " + vend_hasta + " ) and " +
                                 "( SUBSTR(ZPAL01.PKEY,1,6)='RCODRM' ) and " +
                                 "( SUBSTR(ZPAL01.PKEY,7,2)=" + ls_userfile + ".RARL5RM.TRES ) ) " +   
                        "ORDER BY " + ls_userfile + ".RARL5RM.RCUST ASC," +   
                                 ls_userfile + ".RARL5RM.RDATE ASC," +
                                 ls_userfile + ".RARL5RM.LOCAL ASC   ";

                gv_lista.DataKeyNames = new string[] { "fecha", "nota_cred", "factura", "clte" };

                iDB2DataAdapter da = new iDB2DataAdapter("" + cmdtext, connDB2);
                connDB2.Open();

                DataTable dt = new DataTable();
                da.Fill(dt);

                System.Data.DataTable tabla = new DataTable();
                DataColumn col = new DataColumn("fecha");
                tabla.Columns.Add(col);

                DataColumn col2 = new DataColumn("nota_cred");
                tabla.Columns.Add(col2);

                DataColumn col3 = new DataColumn("factura");
                tabla.Columns.Add(col3);

                DataColumn col4 = new DataColumn("clte");
                tabla.Columns.Add(col4);

                DataColumn col5 = new DataColumn("nom_clte");
                tabla.Columns.Add(col5);

                DataColumn col6 = new DataColumn("vend");
                tabla.Columns.Add(col6);

                DataColumn col7 = new DataColumn("local");
                tabla.Columns.Add(col7);

                DataColumn col8 = new DataColumn("cant");
                tabla.Columns.Add(col8);

                DataColumn col9 = new DataColumn("placa");
                tabla.Columns.Add(col9);

                DataColumn col10 = new DataColumn("causa");
                tabla.Columns.Add(col10);

                DataColumn col11 = new DataColumn("desc_razon");
                tabla.Columns.Add(col11);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = tabla.NewRow();
                    Session["source_table"] = dt;

                    dr["fecha"] = dt.Rows[i]["RDATE"].ToString();
                    dr["nota_cred"] = dt.Rows[i]["ARDOCN"].ToString();
                    dr["factura"] = dt.Rows[i]["RINVC"].ToString();
                    dr["clte"] = dt.Rows[i]["RCUST"].ToString();
                    dr["nom_clte"] = dt.Rows[i]["CNME"].ToString();
                    dr["vend"] = dt.Rows[i]["RSAL"].ToString();
                    dr["local"] = dt.Rows[i]["LOCAL"].ToString();
                    dr["cant"] = dt.Rows[i]["SIQTY"].ToString();
                    dr["placa"] = dt.Rows[i]["SICARR"].ToString();
                    dr["causa"] = dt.Rows[i]["TRES"].ToString();
                    dr["desc_razon"] = dt.Rows[i]["RAZON"].ToString();
                    
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

        /*Totalizar el GridView Resumido */
        decimal Total_Clte = 0;
        protected void gv_lista_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int i = 0; i < gv_lista.Rows.Count; i++)
            {
            }


            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Total_Clte += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "cant"));
                }
                else if (e.Row.RowType == DataControlRowType.Footer)
                {
                    e.Row.Cells[6].Text = "TOTAL";
                    e.Row.Cells[7].Text = Total_Clte.ToString("N3");
                    e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Left;
                    e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Center;
                    e.Row.Font.Bold = true;
                    Total_Clte = 0;
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mensaje + "');", true);
            }
        }

        protected void OnRowCreated(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.Header)
                //foreach (TableCell headerCell in e.Row.Cells)
                    //headerCell.Text = "Company Name";
        }

        protected void img_excel_Click(object sender, ImageClickEventArgs e)
        {
            GridViewExportUtil.Export("Devoluciones hasta_" + DateTime.Now.ToString("ddMMyyyy") + ".xls", this.gv_lista); 
        }
    }
}