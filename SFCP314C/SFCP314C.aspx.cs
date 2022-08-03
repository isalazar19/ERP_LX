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
//using iTextSharp.text;
//using iTextSharp.text.pdf;


namespace ERP_LX
{
    public partial class SFCP314C : System.Web.UI.Page
    {
        funciones_generales func = new funciones_generales();
        String mensaje = string.Empty;
        //string conexion = System.Configuration.ConfigurationManager.ConnectionStrings["connectStringPAN"].ConnectionString;
        public static int param, li_pais, filas;
        string conexion, tipo, almacen, undidades, fecha_desde, fecha_hasta, tipo_consulta, usuario, ctro_fab;
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
                txt_fecha1.Text = Convert.ToString(DateTime.Today);
                txt_fecha2.Text = Convert.ToString(DateTime.Today);
                btn_buscar.Visible = false;
                btn_buscar_Click(null, null);
            }
        }

        protected void img_salir_Click(object sender, ImageClickEventArgs e)
        {
            //Response.Write(" <script> window.open('','_parent',''); window.close(); </script>"); 
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
                    ddl_alm.SelectedIndex = 8;
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
            gv_lista.DataBind();
            btn_buscar_Click(null, null);
        }

        protected void Image2_Click(object sender, ImageClickEventArgs e)
        {
            gv_lista.DataBind();
            btn_buscar_Click(null, null);
        }

        protected void ddl_whs_Selection_Changed(object sender, EventArgs e)
        {
            lbl_titulo_consulta.Text = "";
            tipo = ddl_alm.SelectedValue.ToString();
            gv_lista.DataBind();
            gv_detalle.DataBind();
            rb_resumido.Checked = true;
            btn_buscar_Click(null, null);
        }

        protected void ddl_tipo_Selection_Change(object sender, EventArgs e)
        {
            lbl_titulo_consulta.Text = "";
            tipo = ddl_tipo.SelectedValue.ToString();
            gv_lista.DataBind();
            gv_detalle.DataBind();
            rb_resumido.Checked = true;
            btn_buscar_Click(null, null);
        }

        protected void ddl_und_Selection_Change(object sender, EventArgs e)
        {
            lbl_titulo_consulta.Text = "";
            undidades = ddl_und.SelectedValue.ToString();
            gv_lista.DataBind();
            gv_detalle.DataBind();
            rb_resumido.Checked = true;
            rb_detalle.Checked = false;
            btn_buscar_Click(null, null);
        }

        protected void btn_buscar_Click(object sender, ImageClickEventArgs e)
        {
            gv_lista.Visible = true;
            gv_detalle.Visible = false;
            Label1.Visible = false;
            ImageButton1.Visible = false;
            fecha_desde = txt_fecha1.Text;
            fecha_desde = func.convierte_fecha_400(fecha_desde, 2);
            fecha_hasta = txt_fecha2.Text;
            fecha_hasta = func.convierte_fecha_400(fecha_hasta, 2);
            almacen = ddl_alm.SelectedValue.ToString();
            tipo = ddl_tipo.SelectedValue.ToString();
            undidades = ddl_und.SelectedValue.ToString();

            if (rb_resumido.Checked)
            {
                gv_lista.Visible = true;
                gv_detalle.Visible = false;
                tipo_consulta = "R";
                llenar_grid_general();
            }
            if (rb_detalle.Checked)
            {
                gv_lista.Visible = false;
                gv_detalle.Visible = true;
                tipo_consulta = "D";
                GetData();
            }

            //llenar_grid_general();
        }

        protected void gv_lista_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            //gv_lista.Rows[e.NewSelectedIndex]

            //gv_lista.SelectedIndex

            //e.NewSelectedIndex
            gv_lista.Visible = false;
            gv_detalle.Visible = true;
            fecha_desde = txt_fecha1.Text;
            fecha_desde = func.convierte_fecha_400(fecha_desde, 2);
            fecha_hasta = txt_fecha2.Text;
            fecha_hasta = func.convierte_fecha_400(fecha_hasta, 2);
            almacen = ddl_alm.SelectedValue.ToString();
            tipo = ddl_tipo.SelectedValue.ToString();
            undidades = ddl_und.SelectedValue.ToString();
            //Capturar el Centro Fabricacion
            GridViewRow row = gv_lista.SelectedRow;
            ctro_fab = row.Cells[1].Text;
            lbl_titulo_consulta.Text = ctro_fab;
            ctro_fab = ctro_fab.Substring(0, 6);
            //llenar_grid_detalle(); */
        }

        protected void gv_lista_Paginar(object sender, GridViewPageEventArgs e)
        {
            GridView gv_lista = (GridView)sender;
            gv_lista.PageIndex = e.NewPageIndex;
            btn_buscar_Click(null, null);
            gv_lista.DataBind();
            //ocultar();
        }

        protected void llenar_grid_general()
        {
            try
            {
                conexion = func.cadena_con(lbl_pais.Text);
                iDB2Connection connDB2 = new iDB2Connection(conexion);
                if (ddl_und.SelectedValue.ToString() == "S") /* Unidades Standar */
                {
                    cmdtext =
                        " SELECT ITH.THWRKC," +
                        "         ITH.THWRKC || ' ' || LWKL01.WDESC as WDESC," +
                        //"         ITH.TPROD," +
                        //"         IIML01.IDESC," +
                        //Sumar los turnos
                        " SUM(CASE WHEN (ITH.THMFGR =3) THEN (ITH.TQTY * IIML01.IMFLPF) ELSE 0 END  ) as BDturno3," +
                        " SUM(CASE WHEN (ITH.THMFGR =1) THEN (ITH.TQTY * IIML01.IMFLPF) ELSE 0 END  ) as BDturno1," +
                        " SUM(CASE WHEN (ITH.THMFGR =2) THEN (ITH.TQTY * IIML01.IMFLPF) ELSE 0  END  ) as BDturno2," +
                        " SUM(CASE WHEN (ITH.THMFGR ='') THEN (ITH.TQTY * IIML01.IMFLPF) ELSE 0  END  ) as BDturno0" +
                        "    FROM ITH," +
                        "         IIML01," +
                        "         LWKL01" +
                        "   WHERE ( ITH.TPROD = IIML01.IPROD ) and  " +
                        "         ( ITH.THWRKC = LWKL01.WWRKC ) and  " +
                        //"         ( ( ITH.THTIME between 223000 and 235959 ) OR  " +
                        //"         ( ITH.THTIME between 0 and 55959 ) )  AND " +
                        "         ( ( ITH.TWHS = '" + @almacen + "' ) and  " +
                        "         ( ITH.TTYPE = '" + @tipo + "' ) and  " +
                        "         ( ITH.TTDTE between " + @fecha_desde + " and " + @fecha_hasta + " ) ) " +
                        "GROUP BY ITH.THWRKC,   " +
                        "         LWKL01.WDESC   " +
                        //"         ITH.TPROD,   " +
                        //"         IIML01.IDESC   " +
                        "ORDER BY ITH.THWRKC ASC ";
                }
                else if (ddl_und.SelectedValue.ToString() == "P") /* Propias */
                {
                    cmdtext =
                        " SELECT ITH.THWRKC," +
                        "         ITH.THWRKC || ' ' || LWKL01.WDESC as WDESC," +
                        //"         ITH.TPROD," +
                        //"         IIML01.IDESC," +
                        //Sumar los turnos
                        " SUM(CASE WHEN (ITH.THMFGR =3) THEN (ITH.TQTY * 1) ELSE 0 END  ) as BDturno3," +
                        " SUM(CASE WHEN (ITH.THMFGR =1) THEN (ITH.TQTY * 1) ELSE 0 END  ) as BDturno1," +
                        " SUM(CASE WHEN (ITH.THMFGR =2) THEN (ITH.TQTY * 1) ELSE 0  END  ) as BDturno2," +
                        " SUM(CASE WHEN (ITH.THMFGR ='') THEN (ITH.TQTY * 1) ELSE 0  END  ) as BDturno0" +
                        "    FROM ITH," +
                        "         IIML01," +
                        "         LWKL01" +
                        "   WHERE ( ITH.TPROD = IIML01.IPROD ) and  " +
                        "         ( ITH.THWRKC = LWKL01.WWRKC ) and  " +
                        //"         ( ( ITH.THTIME between 223000 and 235959 ) OR  " +
                        //"         ( ITH.THTIME between 0 and 55959 ) )  AND " +
                        "         ( ( ITH.TWHS = '" + @almacen + "' ) and  " +
                        "         ( ITH.TTYPE = '" + @tipo + "' ) and  " +
                        "         ( ITH.TTDTE between " + @fecha_desde + " and " + @fecha_hasta + " ) ) " +
                        "GROUP BY ITH.THWRKC,   " +
                        "         LWKL01.WDESC   " +
                        //"         ITH.TPROD,   " +
                        //"         IIML01.IDESC   " +
                        "ORDER BY ITH.THWRKC ASC ";
                }
                else if (ddl_und.SelectedValue.ToString() == "U") /* Unificadas */
                {
                    cmdtext =
                        " SELECT ITH.THWRKC," +
                        "         ITH.THWRKC || ' ' || LWKL01.WDESC as WDESC," +
                        //"         ITH.TPROD," +
                        //"         IIML01.IDESC," +
                        //Sumar los turnos
                        " SUM(CASE WHEN (ITH.THMFGR =3) THEN (ITH.TQTY * IIML01.IMAXP) ELSE 0 END  ) as BDturno3," +
                        " SUM(CASE WHEN (ITH.THMFGR =1) THEN (ITH.TQTY * IIML01.IMAXP) ELSE 0 END  ) as BDturno1," +
                        " SUM(CASE WHEN (ITH.THMFGR =2) THEN (ITH.TQTY * IIML01.IMAXP) ELSE 0  END  ) as BDturno2," +
                        " SUM(CASE WHEN (ITH.THMFGR ='') THEN (ITH.TQTY * IIML01.IMAXP) ELSE 0  END  ) as BDturno0" +
                        "    FROM ITH," +
                        "         IIML01," +
                        "         LWKL01" +
                        "   WHERE ( ITH.TPROD = IIML01.IPROD ) and  " +
                        "         ( ITH.THWRKC = LWKL01.WWRKC ) and  " +
                        //"         ( ( ITH.THTIME between 223000 and 235959 ) OR  " +
                        //"         ( ITH.THTIME between 0 and 55959 ) )  AND " +
                        "         ( ( ITH.TWHS = '" + @almacen + "' ) and  " +
                        "         ( ITH.TTYPE = '" + @tipo + "' ) and  " +
                        "         ( ITH.TTDTE between " + @fecha_desde + " and " + @fecha_hasta + " ) ) " +
                        "GROUP BY ITH.THWRKC,   " +
                        "         LWKL01.WDESC   " +
                        //"         ITH.TPROD,   " +
                        //"         IIML01.IDESC   " +
                        "ORDER BY ITH.THWRKC ASC ";
                }
                gv_lista.DataKeyNames = new string[] { "ctro" };

                iDB2DataAdapter da = new iDB2DataAdapter("" + cmdtext, connDB2);
                connDB2.Open();

                DataTable dt = new DataTable();
                da.Fill(dt);

                System.Data.DataTable tabla = new DataTable();
                DataColumn col = new DataColumn("ctro");
                tabla.Columns.Add(col);

                DataColumn col2 = new DataColumn("turno3");
                tabla.Columns.Add(col2);

                DataColumn col3 = new DataColumn("turno1");
                tabla.Columns.Add(col3);

                DataColumn col4 = new DataColumn("turno2");
                tabla.Columns.Add(col4);

                DataColumn col5 = new DataColumn("turno0");
                tabla.Columns.Add(col5);

                DataColumn col6 = new DataColumn("total");
                tabla.Columns.Add(col6);

                filas = dt.Rows.Count;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = tabla.NewRow();
                    Session["source_table"] = dt;

                    dr["ctro"] = dt.Rows[i]["WDESC"].ToString();
                    dr["turno3"] = (Decimal.Parse(dt.Rows[i]["BDturno3"].ToString())).ToString("N3");
                    dr["turno1"] = (Decimal.Parse(dt.Rows[i]["BDturno1"].ToString())).ToString("N3");
                    dr["turno2"] = (Decimal.Parse(dt.Rows[i]["BDturno2"].ToString())).ToString("N3");
                    dr["turno0"] = (Decimal.Parse(dt.Rows[i]["BDturno0"].ToString())).ToString("N3");

                    dr["total"] = (Decimal.Parse(dr["turno3"].ToString()) + Decimal.Parse(dr["turno1"].ToString()) + Decimal.Parse(dr["turno2"].ToString()) + Decimal.Parse(dr["turno0"].ToString())).ToString("N3");
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
        decimal Total1 = 0;
        decimal Total2 = 0;
        decimal Total3 = 0;
        decimal Total0 = 0;
        decimal Total = 0;
        protected void gv_lista_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Total1 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "turno1"));
                    Total2 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "turno2"));
                    Total3 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "turno3"));
                    Total0 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "turno0"));
                    Total += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "total"));
                }
                else if (e.Row.RowType == DataControlRowType.Footer)
                {
                    e.Row.Cells[0].Text = "TOTAL";
                    e.Row.Cells[1].Text = Total3.ToString("N3");
                    e.Row.Cells[2].Text = Total1.ToString("N3");
                    e.Row.Cells[3].Text = Total2.ToString("N3");
                    e.Row.Cells[4].Text = Total0.ToString("N3");
                    e.Row.Cells[5].Text = Total.ToString("N3");
                    e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
                    e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Font.Bold = true;
                    Total1 = 0;
                    Total2 = 0;
                    Total3 = 0;
                    Total0 = 0;
                    Total = 0;
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mensaje + "');", true);
            }
        }

        protected void rb_resumido_CheckedChanged(object sender, EventArgs e)
        {
            gv_lista.DataBind();
            gv_detalle.DataBind();
            btn_buscar_Click(null, null);
        }

        protected void rb_detalle_CheckedChanged(object sender, EventArgs e)
        {
            gv_detalle.DataBind();
            gv_detalle.DataBind();
            btn_buscar_Click(null, null);
        }

        private void GetData()
        {
            conexion = func.cadena_con(lbl_pais.Text);
            DataTable table = new DataTable();
            using (iDB2Connection conn = new iDB2Connection(conexion))
            {
                string sql =
                        " SELECT distinct ITH.THWRKC," +
                    //"         ITH.THWRKC || ' ' || LWKL01.WDESC as WDESC" +
                        "         LWKL01.WDESC as WDESC" +
                        "    FROM ITH," +
                        "         LWKL01" +
                        " where        ( ITH.THWRKC = LWKL01.WWRKC ) and  " +
                        "         ( ( ITH.TWHS = '" + @almacen + "' ) and  " +
                        "         ( ITH.TTYPE = '" + @tipo + "' ) and  " +
                        "         ( ITH.TTDTE between " + @fecha_desde + " and " + @fecha_hasta + " ) ) " +
                        "GROUP BY ITH.THWRKC,   " +
                        "         LWKL01.WDESC   " +
                        "ORDER BY ITH.THWRKC ASC ";


                using (iDB2Command cmd = new iDB2Command(sql, conn))
                {
                    using (iDB2DataAdapter ad = new iDB2DataAdapter(cmd))
                    {
                        ad.Fill(table);
                    }
                }
            }
            gv_detalle.DataSource = table;
            gv_detalle.DataBind();
        }

        protected void FindTheGridAndBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;
            GridView nestedGridView = (GridView)e.Row.FindControl("GridViewNested");
            int ctro_fab = int.Parse(gv_detalle.DataKeys[e.Row.RowIndex].Value.ToString());
            DataTable table = new DataTable();
            using (iDB2Connection conn = new iDB2Connection(conexion))
            {
                if (ddl_und.SelectedValue.ToString() == "S") /* Unidades Standar */
                {
                    string sql =
                        " SELECT         ITH.TPROD as Producto," +
                        "         IIML01.IDESC as Descripcion," +
                        " SUM(CASE WHEN (ITH.THMFGR = 3) THEN (ITH.TQTY * IIML01.IMFLPF) ELSE 0 END  ) as Turno3," +
                        " SUM(CASE WHEN (ITH.THMFGR = 1) THEN (ITH.TQTY * IIML01.IMFLPF) ELSE 0 END  ) as Turno1," +
                        " SUM(CASE WHEN (ITH.THMFGR = 2) THEN (ITH.TQTY * IIML01.IMFLPF) ELSE 0  END  ) as Turno2," +
                        " SUM(CASE WHEN (ITH.THMFGR = '') THEN (ITH.TQTY * IIML01.IMFLPF) ELSE 0  END  ) as Turno0," +
                        " SUM(CASE WHEN (ITH.THMFGR = 3) THEN (ITH.TQTY * IIML01.IMFLPF) ELSE 0 END  )+SUM(CASE WHEN ( ITH.THMFGR = 1 ) THEN (ITH.TQTY * IIML01.IMFLPF) ELSE 0 END  )+SUM(CASE WHEN ( ITH.THMFGR = 2 ) THEN (ITH.TQTY * IIML01.IMFLPF) ELSE 0  END  )+SUM(CASE WHEN ( ITH.THMFGR = '' ) THEN (ITH.TQTY * IIML01.IMFLPF) ELSE 0 END ) AS Total" +
                        "    FROM ITH," +
                        "         IIML01," +
                        "         LWKL01" +
                        "   WHERE ( ITH.TPROD = IIML01.IPROD ) and  " +
                        "         ( ITH.THWRKC = LWKL01.WWRKC ) and  " +
                        "         ( ( ITH.TWHS = '" + @almacen + "' ) and  " +
                        "         ( ITH.TTYPE = '" + @tipo + "' ) and  " +
                        "         ( ith.thwrkc =" + @ctro_fab + ") and " +
                        "         ( ITH.TTDTE between " + @fecha_desde + " and " + @fecha_hasta + " ) ) " +
                        "GROUP BY ITH.THWRKC,   " +
                        "         LWKL01.WDESC,   " +
                        "         ITH.TPROD,   " +
                        "         IIML01.IDESC   " +
                        "ORDER BY ITH.THWRKC ASC ";

                    using (iDB2Command cmd = new iDB2Command(sql, conn))
                    {
                        using (iDB2DataAdapter ad = new iDB2DataAdapter(cmd))
                        {
                            cmd.Parameters.Add("@ctro_fab", ctro_fab);
                            ad.Fill(table);
                        }
                    }
                }
                else if (ddl_und.SelectedValue.ToString() == "P") /* Propias */
                {
                    string sql =
                        //" SELECT         ITH.TPROD as Producto," +
                        //"         IIML01.IDESC as Descripcion," +
                        " SELECT         ITH.TPROD as Producto," +
                        "         IIML01.IDESC as Descripcion," +
                        " SUM(CASE WHEN (ITH.THMFGR = 3) THEN (ITH.TQTY * 1) ELSE 0 END  ) as Turno3," +
                        " SUM(CASE WHEN (ITH.THMFGR = 1) THEN (ITH.TQTY * 1) ELSE 0 END  ) as Turno1," +
                        " SUM(CASE WHEN (ITH.THMFGR = 2) THEN (ITH.TQTY * 1) ELSE 0  END  ) as Turno2," +
                        " SUM(CASE WHEN (ITH.THMFGR = '') THEN (ITH.TQTY * 1) ELSE 0  END  ) as Turno0," +
                        " SUM(CASE WHEN (ITH.THMFGR = 3) THEN (ITH.TQTY * 1) ELSE 0 END  )+SUM(CASE WHEN ( ITH.THMFGR = 1 ) THEN (ITH.TQTY * 1) ELSE 0 END  )+SUM(CASE WHEN ( ITH.THMFGR = 2 ) THEN (ITH.TQTY * 1) ELSE 0  END  )+SUM(CASE WHEN ( ITH.THMFGR = '' ) THEN (ITH.TQTY * 1) ELSE 0 END ) AS Total" +
                        "    FROM ITH," +
                        "         IIML01," +
                        "         LWKL01" +
                        "   WHERE ( ITH.TPROD = IIML01.IPROD ) and  " +
                        "         ( ITH.THWRKC = LWKL01.WWRKC ) and  " +
                        "         ( ( ITH.TWHS = '" + @almacen + "' ) and  " +
                        "         ( ITH.TTYPE = '" + @tipo + "' ) and  " +
                        "         ( ith.thwrkc =" + @ctro_fab + ") and " +
                        "         ( ITH.TTDTE between " + @fecha_desde + " and " + @fecha_hasta + " ) ) " +
                        "GROUP BY ITH.THWRKC,   " +
                        "         LWKL01.WDESC,   " +
                        "         ITH.TPROD,   " +
                        "         IIML01.IDESC   " +
                        "ORDER BY ITH.THWRKC ASC ";

                    using (iDB2Command cmd = new iDB2Command(sql, conn))
                    {
                        using (iDB2DataAdapter ad = new iDB2DataAdapter(cmd))
                        {
                            cmd.Parameters.Add("@ctro_fab", ctro_fab);
                            ad.Fill(table);
                        }
                    }
                }
                else if (ddl_und.SelectedValue.ToString() == "U") /* Unificadas */
                {
                    string sql =
                        //" SELECT         ITH.TPROD as Producto," +
                        //"         IIML01.IDESC as Descripcion," +
                        " SELECT         ITH.TPROD as Producto," +
                        "         IIML01.IDESC as Descripcion," +
                        " SUM(CASE WHEN (ITH.THMFGR = 3) THEN (ITH.TQTY * IIML01.IMAXP) ELSE 0 END  ) as Turno3," +
                        " SUM(CASE WHEN (ITH.THMFGR = 1) THEN (ITH.TQTY * IIML01.IMAXP) ELSE 0 END  ) as Turno1," +
                        " SUM(CASE WHEN (ITH.THMFGR = 2) THEN (ITH.TQTY * IIML01.IMAXP) ELSE 0  END  ) as Turno2," +
                        " SUM(CASE WHEN (ITH.THMFGR = '') THEN (ITH.TQTY * IIML01.IMAXP) ELSE 0  END  ) as Turno0," +
                        " SUM(CASE WHEN (ITH.THMFGR = 3) THEN (ITH.TQTY * IIML01.IMAXP) ELSE 0 END  )+SUM(CASE WHEN ( ITH.THMFGR = 1 ) THEN (ITH.TQTY * IIML01.IMAXP) ELSE 0 END  )+SUM(CASE WHEN ( ITH.THMFGR = 2 ) THEN (ITH.TQTY * IIML01.IMAXP) ELSE 0  END  )+SUM(CASE WHEN ( ITH.THMFGR = '' ) THEN (ITH.TQTY * IIML01.IMAXP) ELSE 0 END ) AS Total" +
                        "    FROM ITH," +
                        "         IIML01," +
                        "         LWKL01" +
                        "   WHERE ( ITH.TPROD = IIML01.IPROD ) and  " +
                        "         ( ITH.THWRKC = LWKL01.WWRKC ) and  " +
                        "         ( ( ITH.TWHS = '" + @almacen + "' ) and  " +
                        "         ( ITH.TTYPE = '" + @tipo + "' ) and  " +
                        "         ( ith.thwrkc =" + @ctro_fab + ") and " +
                        "         ( ITH.TTDTE between " + @fecha_desde + " and " + @fecha_hasta + " ) ) " +
                        "GROUP BY ITH.THWRKC,   " +
                        "         LWKL01.WDESC,   " +
                        "         ITH.TPROD,   " +
                        "         IIML01.IDESC   " +
                        "ORDER BY ITH.THWRKC ASC ";

                    using (iDB2Command cmd = new iDB2Command(sql, conn))
                    {
                        using (iDB2DataAdapter ad = new iDB2DataAdapter(cmd))
                        {
                            cmd.Parameters.Add("@ctro_fab", ctro_fab);
                            ad.Fill(table);
                        }
                    }
                }
            }
            nestedGridView.DataSource = table;
            nestedGridView.DataBind();
        }

        //Sumar el Grupo
        decimal DETTotal1 = 0;
        decimal DETTotal2 = 0;
        decimal DETTotal3 = 0;
        decimal DETTotal0 = 0;
        decimal DETTotal = 0;
        protected void gv_GVN_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DETTotal1 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "turno1"));
                    DETTotal2 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "turno2"));
                    DETTotal3 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "turno3"));
                    DETTotal0 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "turno0"));
                    DETTotal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "total"));
                }
                else if (e.Row.RowType == DataControlRowType.Footer)
                {
                    e.Row.Cells[1].Text = "Subtotal";
                    e.Row.Cells[2].Text = DETTotal3.ToString("N3");
                    e.Row.Cells[3].Text = DETTotal1.ToString("N3");
                    e.Row.Cells[4].Text = DETTotal2.ToString("N3");
                    e.Row.Cells[5].Text = DETTotal0.ToString("N3");
                    e.Row.Cells[6].Text = DETTotal.ToString("N3");
                    e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Left;
                    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Font.Bold = true;
                    DETTotal1 = 0;
                    DETTotal2 = 0;
                    DETTotal3 = 0;
                    DETTotal0 = 0;
                    DETTotal = 0;
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mensaje + "');", true);
            }
        }



        //protected void llenar_grid_detalle()
        //{
        //    Label1.Visible = true;
        //    ImageButton1.Visible = true;

        //    try
        //    {
        //        conexion = func.cadena_con(lbl_pais.Text);
        //        iDB2Connection connDB2 = new iDB2Connection(conexion);
        //        if (ddl_und.SelectedValue.ToString() == "S") /* Unidades Standar */
        //        {
        //             cmdtext =
        //                " SELECT ITH.THWRKC," +
        //                "         ITH.THWRKC || ' ' || LWKL01.WDESC as WDESC," +
        //                "         ITH.TPROD," +
        //                "         TRIM(ITH.TPROD) || ' ' || IIML01.IDESC as IDESC," +
        //                " SUM(CASE WHEN (( ITH.THTIME between 223000 and 235959 ) OR ( ITH.THTIME between 0 and 55959 )) THEN (ITH.TQTY * IIML01.IMFLPF) ELSE 0 END  ) as BDturno3," +
        //                " SUM(CASE WHEN ( ITH.THTIME between 60000 and 142959 ) THEN (ITH.TQTY * IIML01.IMFLPF) ELSE 0 END  ) as BDturno1," +
        //                " SUM(CASE WHEN ( ITH.THTIME between 143000 and 222959 ) THEN (ITH.TQTY * IIML01.IMFLPF) ELSE 0  END  ) as BDturno2" +
        //                "    FROM ITH," +
        //                "         IIML01," +
        //                "         LWKL01" +
        //                "   WHERE ( ITH.TPROD = IIML01.IPROD ) and  " +
        //                "         ( ITH.THWRKC = LWKL01.WWRKC ) and  " +
        //                "         ( ( ITH.TWHS = '" + @almacen + "' ) and  " +
        //                "         ( ITH.TTYPE = '" + @tipo + "' ) and  " +
        //                "         ( ITH.THWRKC = '"+ @ctro_fab +"' ) and " +
        //                "         ( ITH.TTDTE between " + @fecha_desde + " and " + @fecha_hasta + " ) ) " +
        //                "GROUP BY ITH.THWRKC,   " +
        //                "         LWKL01.WDESC,  " +
        //                "         ITH.TPROD,   " +
        //                "         IIML01.IDESC   " +
        //                "ORDER BY ITH.THWRKC,ITH.TPROD ASC ";
        //        }
        //        else if (ddl_und.SelectedValue.ToString() == "P") /* Propias */
        //        {
        //            cmdtext =
        //                " SELECT ITH.THWRKC," +
        //                "         ITH.THWRKC || ' ' || LWKL01.WDESC as WDESC," +
        //                "         ITH.TPROD," +
        //                "         TRIM(ITH.TPROD) || ' ' || IIML01.IDESC as IDESC," +
        //                " SUM(CASE WHEN (( ITH.THTIME between 223000 and 235959 ) OR ( ITH.THTIME between 0 and 55959 )) THEN (ITH.TQTY * 1) ELSE 0 END  ) as BDturno3DET," +
        //                " SUM(CASE WHEN ( ITH.THTIME between 60000 and 142959 ) THEN (ITH.TQTY * 1) ELSE 0 END  ) as BDturno1DET," +
        //                " SUM(CASE WHEN ( ITH.THTIME between 143000 and 222959 ) THEN (ITH.TQTY * 1) ELSE 0  END  ) as BDturno2DET" +
        //                "    FROM ITH," +
        //                "         IIML01," +
        //                "         LWKL01" +
        //                "   WHERE ( ITH.TPROD = IIML01.IPROD ) and  " +
        //                "         ( ITH.THWRKC = LWKL01.WWRKC ) and  " +
        //                "         ( ( ITH.TWHS = '" + @almacen + "' ) and  " +
        //                "         ( ITH.TTYPE = '" + @tipo + "' ) and  " +
        //                "         ( ITH.THWRKC = '"+ @ctro_fab +"' ) and " +
        //                "         ( ITH.TTDTE between " + @fecha_desde + " and " + @fecha_hasta + " ) ) " +
        //                "GROUP BY ITH.THWRKC,   " +
        //                "         LWKL01.WDESC,  " +
        //                "         ITH.TPROD,   " +
        //                "         IIML01.IDESC   " +
        //                "ORDER BY ITH.THWRKC,ITH.TPROD ASC ";
        //        }
        //        else if (ddl_und.SelectedValue.ToString() == "U") /* Unificadas */
        //        {
        //            cmdtext =
        //               " SELECT ITH.THWRKC," +
        //               "         ITH.THWRKC || ' ' || LWKL01.WDESC as WDESC," +
        //               "         ITH.TPROD," +
        //               "         TRIM(ITH.TPROD) || ' ' || IIML01.IDESC as IDESC," +
        //               " SUM(CASE WHEN (( ITH.THTIME between 223000 and 235959 ) OR ( ITH.THTIME between 0 and 55959 )) THEN (ITH.TQTY * IIML01.IMAXP) ELSE 0 END  ) as BDturno3," +
        //               " SUM(CASE WHEN ( ITH.THTIME between 60000 and 142959 ) THEN (ITH.TQTY * IIML01.IMBUPL) ELSE 0 END  ) as BDturno1," +
        //               " SUM(CASE WHEN ( ITH.THTIME between 143000 and 222959 ) THEN (ITH.TQTY * IIML01.IMBUPL) ELSE 0  END  ) as BDturno2" +
        //               "    FROM ITH," +
        //               "         IIML01," +
        //               "         LWKL01" +
        //               "   WHERE ( ITH.TPROD = IIML01.IPROD ) and  " +
        //               "         ( ITH.THWRKC = LWKL01.WWRKC ) and  " +
        //               "         ( ( ITH.TWHS = '" + @almacen + "' ) and  " +
        //               "         ( ITH.TTYPE = '" + @tipo + "' ) and  " +
        //               "         ( ITH.THWRKC = '" + @ctro_fab + "' ) and " +
        //               "         ( ITH.TTDTE between " + @fecha_desde + " and " + @fecha_hasta + " ) ) " +
        //               "GROUP BY ITH.THWRKC,   " +
        //               "         LWKL01.WDESC,  " +
        //               "         ITH.TPROD,   " +
        //               "         IIML01.IDESC   " +
        //               "ORDER BY ITH.THWRKC,ITH.TPROD ASC ";
        //        }
        //        gv_detalle.DataKeyNames = new string[] { "prod" };

        //        iDB2DataAdapter da = new iDB2DataAdapter("" + cmdtext, connDB2);
        //        connDB2.Open();

        //        DataTable dt = new DataTable();
        //        da.Fill(dt);

        //        System.Data.DataTable tabla = new DataTable();
        //        DataColumn col = new DataColumn("prod");
        //        tabla.Columns.Add(col);

        //        DataColumn col2 = new DataColumn("DETturno3");
        //        tabla.Columns.Add(col2);

        //        DataColumn col3 = new DataColumn("DETturno1");
        //        tabla.Columns.Add(col3);

        //        DataColumn col4 = new DataColumn("DETturno2");
        //        tabla.Columns.Add(col4);

        //        DataColumn col5 = new DataColumn("DETtotal");
        //        tabla.Columns.Add(col5);

        //        filas = dt.Rows.Count;
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            DataRow dr = tabla.NewRow();

        //            dr["prod"] = dt.Rows[i]["IDESC"].ToString();
        //            dr["DETturno3"] = (Decimal.Parse(dt.Rows[i]["BDturno3DET"].ToString())).ToString("N3");
        //            dr["DETturno1"] = (Decimal.Parse(dt.Rows[i]["BDturno1DET"].ToString())).ToString("N3");
        //            dr["DETturno2"] = (Decimal.Parse(dt.Rows[i]["BDturno2DET"].ToString())).ToString("N3");

        //            dr["DETtotal"] = (Decimal.Parse(dr["DETturno3"].ToString()) + Decimal.Parse(dr["DETturno1"].ToString()) + Decimal.Parse(dr["DETturno2"].ToString())).ToString("N3");
        //            tabla.Rows.Add(dr);
        //        }


        //        connDB2.Close();
        //        gv_detalle.DataSource = tabla;
        //        gv_detalle.DataBind();

        //    }
        //    catch (Exception ex)
        //    {
        //        mensaje = ex.Message;
        //        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mensaje + "');", true);
        //    }
        //}

        //protected void gv_detalle_Paginar(object sender, GridViewPageEventArgs e)
        //{
        //    GridView gv_detalle = (GridView)sender;
        //    gv_detalle.PageIndex = e.NewPageIndex;
        //    //btn_buscar_Click(null, null);
        //    gv_detalle.DataBind();
        //    //ocultar();
        //}

        //decimal DETTotal1 = 0;
        //decimal DETTotal2 = 0;
        //decimal DETTotal3 = 0;
        //decimal DETTotal = 0;
        //protected void gv_detalle_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    try
        //    {
        //        if (e.Row.RowType == DataControlRowType.DataRow)
        //        {
        //            DETTotal1 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DETturno1"));
        //            DETTotal2 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DETturno2"));
        //            DETTotal3 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DETturno3"));
        //            DETTotal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "DETtotal"));
        //        }
        //        else if (e.Row.RowType == DataControlRowType.Footer)
        //        {
        //            e.Row.Cells[0].Text = "TOTAL";
        //            e.Row.Cells[1].Text = DETTotal3.ToString("N3");
        //            e.Row.Cells[2].Text = DETTotal1.ToString("N3");
        //            e.Row.Cells[3].Text = DETTotal2.ToString("N3");
        //            e.Row.Cells[4].Text = DETTotal.ToString("N3");
        //            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
        //            e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
        //            e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
        //            e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
        //            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
        //            e.Row.Font.Bold = true;
        //            DETTotal1 = 0;
        //            DETTotal2 = 0;
        //            DETTotal3 = 0;
        //            DETTotal = 0;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        mensaje = ex.Message;
        //        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mensaje + "');", true);
        //    }
        //} 


        protected void img_regresar_Click(object sender, ImageClickEventArgs e)
        {
            //gv_detalle.Visible = false;
            //gv_lista.Visible = true;
            //Label1.Visible = false;
            //ImageButton1.Visible = false;
            //lbl_titulo_consulta.Text = "";
            //convertir_excel();
        }

        protected void txt_fecha1_TextChanged(object sender, EventArgs e)
        {
            gv_lista.DataBind();
            btn_buscar_Click(null, null);
        }

        protected void txt_fecha2_TextChanged(object sender, EventArgs e)
        {
            gv_lista.DataBind();
            btn_buscar_Click(null, null);
        }


        public void ExportToPdf(DataTable ExDataTable)
        {
            ////Here set page size as A4        
            //Document pdfDoc = new Document(PageSize.A4, 10, 10, 10, 10);
            //try
            //{
            //    PdfWriter.GetInstance(pdfDoc, System.Web.HttpContext.Current.Response.OutputStream);
            //    pdfDoc.Open();
            //    //Set Font Properties for PDF File            
            //    Font fnt = FontFactory.GetFont("Times New Roman", 12);
            //    DataTable dt = ExDataTable;
            //    if (dt != null)
            //    {
            //        PdfPTable PdfTable = new PdfPTable(dt.Columns.Count);
            //        PdfPCell PdfPCell = null;
            //        //Here we create PDF file tables                
            //        for (int rows = 0; rows < dt.Rows.Count; rows++)
            //        {
            //            if (rows == 0)
            //            {
            //                for (int column = 0; column < dt.Columns.Count; column++)
            //                {
            //                    PdfPCell = new PdfPCell(new Phrase(new Chunk(dt.Columns[column].ColumnName.ToString(), fnt)));
            //                    PdfTable.AddCell(PdfPCell);
            //                }
            //            }
            //            for (int column = 0; column < dt.Columns.Count; column++)
            //            {
            //                PdfPCell = new PdfPCell(new Phrase(new Chunk(dt.Rows[rows][column].ToString(), fnt)));
            //                PdfTable.AddCell(PdfPCell);
            //            }
            //        }
            //        // Finally Add pdf table to the document                 
            //        pdfDoc.Add(PdfTable);
            //    }
            //    pdfDoc.Close();
            //    Response.ContentType = "application/pdf";
            //    //Set default file Name as current datetime
            //    Response.AddHeader("content-disposition", "attachment; filename=" + DateTime.Now.ToString("yyyyMMdd") + ".pdf");
            //    System.Web.HttpContext.Current.Response.Write(pdfDoc); Response.Flush(); Response.End();
            //}
            //catch (Exception ex) { Response.Write(ex.ToString()); }

        }

        protected void img_pdf_Click(object sender, ImageClickEventArgs e)
        {

        }

        //protected void convertir_excel()
        //{
//            try
//            {
//                StringBuilder sb = new StringBuilder();
//                StringWriter sw = new StringWriter(sb);
//                HtmlTextWriter htw = new HtmlTextWriter(sw);
//                Page page = new Page();
//                HtmlForm form = new HtmlForm();

//                if (rb_resumido.Checked)
//                {
//                    gv_lista.AllowPaging = false;
//                    gv_lista.DataBind();
//                    gv_lista.EnableViewState = false;
//                    page.EnableEventValidation = false;
//                }
//                if (rb_detalle.Checked)
//                {
//                    gv_detalle.AllowPaging = false;
//                    gv_detalle.DataBind();
//                    gv_detalle.EnableViewState = false;
//                    page.EnableEventValidation = false;
//                }
//                page.DesignerInitialize();
//                page.Controls.Add(form);
//                if (rb_resumido.Checked)
//                {
//                    form.Controls.Add(gv_lista);
//                }
//                if (rb_detalle.Checked)
//                {
//                    form.Controls.Add(gv_detalle);
//                }
//                page.RenderControl(htw);

//                Response.Clear();
//                Response.Buffer = true;
//                Response.ContentType = "application/vnd.ms-excel";
//                Response.AddHeader("Content-Disposition", "attachment;filename=InformeDeDatos.xls");
//                Response.Charset = "UTF-8";

//                Response.Cache.SetCacheability(HttpCacheability.NoCache);
//                Response.ContentEncoding = System.Text.Encoding.Default;

//                Response.Write(sb.ToString());
//                Response.End();

//                if (rb_resumido.Checked)
//                {
//                    gv_lista.AllowPaging = true;
//                    gv_lista.DataBind();
//                }
//                if (rb_detalle.Checked)
//                {
//                    gv_detalle.AllowPaging = true;
//                    gv_detalle.DataBind();
//                }
//            }
            
//            catch (Exception ex)
//            {

////                // valida que las fila no excedan el tamañoa maximo de una hoja 

////                string script = @"<script language = ""JavaScript""> 
////                            alert('Filtre mas la informacion por que se sobrepaso de los 65.536 registros que se pueden exportar a excel');
////                             </script>";
////                ClientScript.RegisterStartupScript(typeof(Page), "Alerta", script);

//            }
//        }

//        public override void VerifyRenderingInServerForm(Control control)
//        {

//        }

    }
}