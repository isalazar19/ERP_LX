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

namespace ACRP393
{
    public partial class ACRP393 : System.Web.UI.Page
    {
        funciones_generales func = new funciones_generales();
        String mensaje = string.Empty;
        //string conexion = System.Configuration.ConfigurationManager.ConnectionStrings["connectStringPAN"].ConnectionString;
        public static int param, li_pais, filas;
        string ls_userfile, conexion, fecha_hasta, cliente, nom_clte, dir, tel, usuario, filt, miclte, num_doc, tipo_doc;
        decimal ldc_saldo_mes, ldc_saldo_30_dias, ldc_saldo1_30, ldc_saldo31_60, ldc_saldo61_90, ldc_saldo91;
        public String cmdtext, cia, encab1, vendedor;

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
                llenar_vndr();
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
            ejecutar_SQL();
            if (cmdtext != null)
            {
                gv_lista.DataBind();
                if (rb_resumida.Checked)
                {
                    gv_lista.DataBind();
                    llenar_grid();
                }
                else
                {
                    gv_detalle.DataBind();
                    llenar_grid_detalle();
                }
            }
            else
            {
                mensaje = "No hay nada que listar...Verifique su selección";
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mensaje + "');", true);
            }

        }

        protected void gv_lista_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gv_lista_Paginar(object sender, GridViewPageEventArgs e)
        {
            GridView gv_lista = (GridView)sender;
            gv_lista.PageIndex = e.NewPageIndex;
            btn_buscar_Click(null, null);
            gv_lista.DataBind();
            //ocultar();
        }

        protected void ejecutar_SQL()
        {
            fecha_hasta = func.convierte_fecha_400(txt_fecha1.Text, 2);
            vendedor = ddl_vndr.SelectedValue.ToString();

            /*****************************  CASO CONSULTA RESUMIDA  **********************************************************************/
            /* QUERY PARA TRAER LOS DATOS POR CLIENTE NATURAL */
            if (rb_base.Checked && rb_natural.Checked && rb_vndNO.Checked && rb_resumida.Checked) /*Ejecuta el QUERY para la moneda LOCAL*/
            {
                cmdtext = "SELECT DISTINCT RARL01.RCUST, RCM.CNME, SUM(RAMT) AS RAMT, 0 as column1, 0 as column2 FROM RARL01, RCM WHERE ( RARL01.RCUST = RCM.CCUST  and RARL01.RDATE <= " + fecha_hasta + ") GROUP BY RARL01.RCUST, RCM.CNME ORDER BY RARL01.RCUST ASC";
            }

            if (rb_transaccion.Checked && rb_natural.Checked && rb_vndNO.Checked && rb_resumida.Checked)  /* Ejecuta el QUERY para la moneda EXTRANJERA  */
            {
                cmdtext = "SELECT DISTINCT RARL01.RCUST, RCM.CNME, SUM(RAMT) AS RAMT, 0 as column1, 0 as column2 FROM RARL01, RCM WHERE ( RARL01.RCUST = RCM.CCUST and RARL01.RDATE <= " + fecha_hasta + ") GROUP BY RARL01.RCUST, RCM.CNME ORDER BY RARL01.RCUST ASC";
            }
            /* QUERY PARA TRAER LOS DATOS POR CLIENTE CORPORATIVO */
            if (rb_base.Checked && rb_corporativo.Checked && rb_vndNO.Checked && rb_resumida.Checked) /*Ejecuta el QUERY para la moneda LOCAL*/
            {
                cmdtext = "SELECT DISTINCT RARL01.RCCUS, RCM.CNME, SUM(RAMT) as RAMT FROM RARL01, RCM WHERE ( RARL01.RCCUS = RCM.CCCUS  and RARL01.RCCUS = RCM.CCUST and RARL01.RDATE <= " + fecha_hasta + ") GROUP BY RARL01.RCCUS, RCM.CNME ORDER BY RARL01.RCCUS ASC";
                //cmdtext = "SELECT DISTINCT RARL01.RCCUS, RCM.CNME, SUM(RAMT) as RAMT FROM RARL01, RCM WHERE ( RARL01.RCCUS = RCM.CCCUS  and RARL01.RCCUS = RCM.CCUST and RARL01.RDATE <= " + fecha_hasta + ") and rccus=5010349 GROUP BY RARL01.RCCUS, RCM.CNME ORDER BY RARL01.RCCUS ASC";
            }
            if (rb_transaccion.Checked && rb_corporativo.Checked && rb_vndNO.Checked && rb_resumida.Checked) /* Ejecuta el QUERY para la moneda EXTRANJERA  */
            {
                cmdtext = "SELECT DISTINCT RARL01.RCCUS, RCM.CNME, SUM(RAMT) as RAMT FROM RARL01, RCM WHERE ( RARL01.RCCUS = RCM.CCCUS  and RARL01.RCCUS = RCM.CCUST and RARL01.RDATE <= " + fecha_hasta + ") GROUP BY RARL01.RCCUS, RCM.CNME ORDER BY RARL01.RCCUS ASC";
            }
            /*****************************  CASO CONSULTA DETALLADA  **********************************************************************/
            // POR VENDEDOR
            if (rb_base.Checked && rb_natural.Checked && rb_vndSI.Checked && rb_detalle.Checked)
            {
                cmdtext = "SELECT RSAL|| ' ' || SNAME as vendedorBD, RCUST || ' ' || CNME as clienteBD, ARDPFX, RRID, RINVC, RDATE, RDDTE, RAMT FROM RARL01, SSML01, RCML01 WHERE (RARL01.RSAL = SSML01.SSAL) AND (RARL01.RCUST = RCML01.CCUST) AND RARL01.RDATE <= " + fecha_hasta + " AND RRID='RI' AND CTYPE NOT IN ('11','12','CIA') AND RARL01.RSAL=" + vendedor + " ORDER BY RSAL,RCUST,RINVC";
            }
            if (rb_base.Checked && rb_corporativo.Checked && rb_vndSI.Checked && rb_detalle.Checked)
            {
                cmdtext = "SELECT RSAL|| ' ' || SNAME as vendedorBD, CTYPE, RCCUS || ' ' || CNME as clienteBD, RRID, RINVC, ARDPFX, RDATE, RDDTE, RAMT FROM RARL01, SSML01, RCML01 WHERE (RARL01.RSAL = SSML01.SSAL) AND (RARL01.RCCUS = RCML01.CCCUS) AND (RARL01.RCCUS = RCML01.CCUST) AND RARL01.RDATE <= " + fecha_hasta + " AND CTYPE NOT IN ('11','12','CIA') AND RARL01.RSAL=" + vendedor + " ORDER BY RSAL,RCCUS,RINVC";
                //cmdtext = "SELECT RSAL|| ' ' || SNAME as vendedorBD, CTYPE, RCCUS || ' ' || CNME as clienteBD, RRID, RINVC, ARDPFX, RDATE, RDDTE, RAMT FROM RARL01, SSML01, RCML01 WHERE (RARL01.RSAL = SSML01.SSAL) AND (RARL01.RCCUS = RCML01.CCCUS) AND (RARL01.RCCUS = RCML01.CCUST) AND RARL01.RDATE <= " + fecha_hasta + " AND CTYPE NOT IN ('11','12','CIA') AND RARL01.RSAL=" + vendedor + "and rccus=5010349 ORDER BY RSAL,RCCUS,RINVC";
            }
            /* POR CLIENTE */
            if (rb_base.Checked && rb_natural.Checked && rb_vndNO.Checked && rb_detalle.Checked)
            {
                cmdtext = "SELECT RSAL|| ' ' || SNAME as vendedorBD, RCUST || ' ' || CNME as clienteBD, ARDPFX, RRID, RINVC, RDATE, RDDTE, RAMT FROM RARL01, SSML01, RCML01 WHERE (RARL01.RSAL = SSML01.SSAL) AND (RARL01.RCUST = RCML01.CCUST) AND RARL01.RDATE <= " + fecha_hasta + " AND CTYPE NOT IN ('11','12','CIA') ORDER BY RSAL,RCUST,RINVC";
            }
            if (rb_base.Checked && rb_corporativo.Checked && rb_vndNO.Checked && rb_detalle.Checked)
            {
                cmdtext = "SELECT RSAL|| ' ' || SNAME as vendedorBD, CTYPE, RCCUS || ' ' || CNME as clienteBD, RRID, RINVC, ARDPFX, RDATE, RDDTE, RAMT FROM RARL01, SSML01, RCML01 WHERE (RARL01.RSAL = SSML01.SSAL) AND (RARL01.RCCUS = RCML01.CCCUS) AND (RARL01.RCCUS = RCML01.CCUST) AND RARL01.RDATE <= " + fecha_hasta + " AND CTYPE NOT IN ('11','12','CIA') ORDER BY RSAL,RCCUS,RINVC";
                //cmdtext = "SELECT RSAL|| ' ' || SNAME as vendedorBD, CTYPE, RCCUS || ' ' || CNME as clienteBD, RRID, RINVC, ARDPFX, RDATE, RDDTE, RAMT FROM RARL01, SSML01, RCML01 WHERE (RARL01.RSAL = SSML01.SSAL) AND (RARL01.RCCUS = RCML01.CCCUS) AND (RARL01.RCCUS = RCML01.CCUST) AND RARL01.RDATE <= " + fecha_hasta + " AND CTYPE NOT IN ('11','12','CIA') and rccus=5010349 ORDER BY RSAL,RCCUS,RINVC";
            }
        }

        protected void llenar_grid()
        {
            try
            {
                conexion = func.cadena_con(lbl_pais.Text);
                iDB2Connection connDB2 = new iDB2Connection(conexion);

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

                DataColumn col4 = new DataColumn("saldomes");
                tabla.Columns.Add(col4);

                DataColumn col5 = new DataColumn("saldo30dia");
                tabla.Columns.Add(col5);

                DataColumn col6 = new DataColumn("saldo1_30");
                tabla.Columns.Add(col6);

                DataColumn col7 = new DataColumn("saldo31_60");
                tabla.Columns.Add(col7);

                DataColumn col8 = new DataColumn("saldo61_90");
                tabla.Columns.Add(col8);

                DataColumn col9 = new DataColumn("saldo91");
                tabla.Columns.Add(col9);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = tabla.NewRow();
                    Session["source_table"] = dt;

                    if (rb_natural.Checked)
                    {
                        dr["clte"] = dt.Rows[i]["RCUST"].ToString();
                        cliente = dt.Rows[i]["RCUST"].ToString();
                        //dr["saldomes"] = dt.Rows[i]["column1"].ToString();
                        //dr["saldo30dia"] = dt.Rows[i]["column2"].ToString();
                    }
                    else
                    {
                        dr["clte"] = dt.Rows[i]["RCCUS"].ToString();
                        cliente = dt.Rows[i]["RCCUS"].ToString();
                    }
                    dr["nom_clte"] = dt.Rows[i]["CNME"].ToString();
                    dr["saldo"] = dt.Rows[i]["RAMT"].ToString();
                    //Buscar el Saldo para la Columna Por Vencer en El Mes
                    ldc_saldo_mes = busca_saldo1(cliente, "", "");
                    dr["saldomes"] = ldc_saldo_mes.ToString();
                    //Buscar el Saldo para la Columna Por Vencer en El Mes > 30 dias
                    ldc_saldo_30_dias = 0; //busca_saldo2(cliente,"");
                    dr["saldo30dia"] = ldc_saldo_30_dias.ToString();
                    //Buscar el Saldo para la Columna Montos Vencidos hasta 30 dias
                    ldc_saldo1_30 = busca_saldo3(cliente, "", "");
                    dr["saldo1_30"] = ldc_saldo1_30.ToString();
                    //Buscar el Saldo para la Columna Montos Vencidos hasta 60 dias
                    ldc_saldo31_60 = busca_saldo4(cliente, "", "");
                    dr["saldo31_60"] = ldc_saldo31_60.ToString();
                    //Buscar el Saldo para la Columna Montos Vencidos hasta 90 dias
                    ldc_saldo61_90 = busca_saldo5(cliente, "", "");
                    dr["saldo61_90"] = ldc_saldo61_90.ToString();
                    //Buscar el Saldo para la Columna Montos Vencidos >90 dias
                    ldc_saldo91 = busca_saldo6(cliente, "", "");
                    dr["saldo91"] = ldc_saldo91.ToString();

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

        decimal Total = 0;  //Suma el Total de la columna Saldo a la Fecha
        decimal Total1 = 0; //Suma el Total de la columna En el Mes
        decimal Total2 = 0; //Suma el Total de la columna Mas 30 dias
        decimal Total3 = 0; //Suma el Total de la columna 1-30 dias
        decimal Total4 = 0; //Suma el Total de la columna 31-60 dias
        decimal Total5 = 0; //Suma el Total de la columna 61-90 dias
        decimal Total6 = 0; //Suma el Total de la columna > 90 dias
        protected void gv_lista_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Total += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "saldo"));
                    Total1 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "saldomes"));
                    Total2 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "saldo30dia"));
                    Total3 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "saldo1_30"));
                    Total4 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "saldo31_60"));
                    Total5 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "saldo61_90"));
                    Total6 += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "saldo91"));
                }
                else if (e.Row.RowType == DataControlRowType.Footer)
                {
                    e.Row.Cells[1].Text = "TOTAL ---->";
                    e.Row.Cells[2].Text = Total.ToString("N2");
                    e.Row.Cells[3].Text = Total1.ToString("N2");
                    e.Row.Cells[4].Text = Total2.ToString("N2");
                    e.Row.Cells[5].Text = Total3.ToString("N2");
                    e.Row.Cells[6].Text = Total4.ToString("N2");
                    e.Row.Cells[7].Text = Total5.ToString("N2");
                    e.Row.Cells[8].Text = Total6.ToString("N2");
                    e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Font.Bold = true;
                    Total = 0;
                    Total1 = 0;
                    Total2 = 0;
                    Total3 = 0;
                    Total4 = 0;
                    Total5 = 0;
                    Total6 = 0;
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
            gv_detalle.DataBind();
            gv_lista.DataBind();
            //btn_buscar_Click(null, null);
        }

        protected void rb_resumida_CheckedChanged(object sender, EventArgs e)
        {
            gv_lista.DataBind();
            //btn_buscar_Click(null, null);
        }

        protected void rb_natural_CheckedChanged(object sender, EventArgs e)
        {
            gv_lista.DataBind();
            gv_detalle.DataBind();
            //btn_buscar_Click(null, null);
        }

        protected void rb_corporativo_CheckedChanged(object sender, EventArgs e)
        {
            gv_lista.DataBind();
            gv_detalle.DataBind();
            //btn_buscar_Click(null, null);
        }

        protected void txt_fecha1_TextChanged(object sender, EventArgs e)
        {
            gv_lista.DataBind();
            //btn_buscar_Click(null, null);
        }

        public decimal busca_saldo1(string parametro, string parametro2, string parametro3)
        {
            try
            {
                String fecha_doc, fecha_vcto, mes_reporte, mes_doc, mes_vcto;
                DateTime fecha1, fecha2;
                iDB2Connection con2 = new iDB2Connection(conexion);
                //string Select = "SELECT DISTINCT RARL01.RCCUS, RARL01.RCUST, RCM.CNME, RARL01.RSEQ, RARL01.RINVC, SUM(RAMT) as RAMT, RARL01.RDATE, " +
                //    "RARL01.RDDTE, RARL01.RRID FROM RARL01, RCM " +
                //    "WHERE ( RARL01.RCCUS = RCM.CCCUS  and RARL01.RCCUS = RCM.CCUST and RARL01.RDATE <= " + fecha_hasta + ") AND RARL01.RCCUS = " + parametro + " " +
                //    "GROUP BY RARL01.RCCUS, RCM.CNME, RARL01.RDATE, RARL01.RRID, RARL01.RDATE, RARL01.RSEQ , RARL01.RCUST, RARL01.RINVC " +
                //    "ORDER BY RARL01.RCCUS, RARL01.RINVC, RARL01.RSEQ ASC";
                string Select;
                if (rb_resumida.Checked)
                {
                    Select = "SELECT DISTINCT RARL01.RCCUS, RARL01.RCUST,  SUM(RAMT) as RAMT, RARL01.RDATE, " +
                        "RARL01.RDDTE FROM RARL01, RCM " +
                        "WHERE ( RARL01.RCCUS = RCM.CCCUS  and RARL01.RCCUS = RCM.CCUST and RARL01.RDATE <= " + fecha_hasta + ") AND RARL01.RCCUS = " + parametro + " " +
                        "GROUP BY RARL01.RCCUS, RARL01.RDATE, RARL01.RDDTE,  RARL01.RCUST " +
                        "ORDER BY RARL01.RCCUS ASC";
                }
                else
                {
                    Select = "SELECT DISTINCT RARL01.RCCUS, RARL01.RCUST,  SUM(RAMT) as RAMT, RARL01.RDATE, " +
                        "RARL01.RDDTE FROM RARL01, RCM " +
                        "WHERE ( RARL01.RCCUS = RCM.CCCUS  and RARL01.RCCUS = RCM.CCUST and RARL01.RDATE <= " + fecha_hasta + ") AND RARL01.RCCUS = " + parametro + " AND RARL01.RINVC=" + parametro2 + " AND RARL01.RRID='" + parametro3 + "' " +
                        "GROUP BY RARL01.RCCUS, RARL01.RDATE, RARL01.RDDTE,  RARL01.RCUST " +
                        "ORDER BY RARL01.RCCUS ASC";
                }

                iDB2DataAdapter da2 = new iDB2DataAdapter("" + Select, con2);
                con2.Open();

                DataTable dt2 = new DataTable();
                da2.Fill(dt2);

                System.Data.DataTable tabla2 = new DataTable();
                DataColumn col = new DataColumn("clte");
                tabla2.Columns.Add(col);

                DataColumn col2 = new DataColumn("clten");
                tabla2.Columns.Add(col2);

                DataColumn col3 = new DataColumn("monto");
                tabla2.Columns.Add(col3);

                DataColumn col4 = new DataColumn("fechadoc");
                tabla2.Columns.Add(col4);

                DataColumn col5 = new DataColumn("fechavcto");
                tabla2.Columns.Add(col5);

                ldc_saldo_mes = 0;
                ldc_saldo_30_dias = 0;
                ldc_saldo1_30 = 0;
                ldc_saldo31_60 = 0;
                ldc_saldo61_90 = 0;
                ldc_saldo91 = 0;
                for (int ii = 0; ii < dt2.Rows.Count; ii++)
                {
                    fecha_doc = dt2.Rows[ii]["RDATE"].ToString();
                    fecha_vcto = dt2.Rows[ii]["RDDTE"].ToString();
                    //Preguntar si el mes de la fecha del documento esta dentro del mes que se pide el reporte
                    mes_reporte = fecha_hasta.Substring(4, 2);
                    mes_doc = fecha_doc.Substring(4, 2);
                    mes_vcto = fecha_vcto.Substring(4, 2);
                    if (mes_reporte == mes_vcto || Convert.ToDateTime(func.convierte_fecha_400(fecha_vcto, 1)) > Convert.ToDateTime(func.convierte_fecha_400(fecha_hasta, 1)))
                    //if (mes_reporte == mes_doc && Convert.ToInt32(mes_vcto) > Convert.ToInt32(mes_doc))
                    //if (mes_reporte == mes_doc && (Convert.ToDateTime(func.convierte_fecha_400(fecha_doc, 1)) < Convert.ToDateTime(func.convierte_fecha_400(fecha_hasta, 1)) && Convert.ToInt32(mes_vcto) > Convert.ToInt32(mes_doc)))
                    {
                        //Calculo del los dias
                        fecha1 = Convert.ToDateTime(func.convierte_fecha_400(fecha_hasta, 1));
                        //fecha1 = Convert.ToDateTime(func.convierte_fecha_400(fecha_doc, 1));
                        fecha2 = Convert.ToDateTime(func.convierte_fecha_400(fecha_vcto, 1));
                        int dias = func.CalculateDays(fecha2, fecha1);
                        if (dias < 0)
                        //if (dias <= 30)
                        {
                            //Acumula los montos
                            ldc_saldo_mes += Convert.ToDecimal(dt2.Rows[ii]["RAMT"].ToString());
                        }
                    }
                }
                con2.Close();
                gv_saldo.DataSource = tabla2;
                gv_saldo.DataBind();
            }

            catch (Exception ex)
            {
                mensaje = ex.Message;
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mensaje + "');", true);
            }
            return ldc_saldo_mes;
        }

        public decimal busca_saldo2(string parametro, string parametro2, string parametro3)
        {
            try
            {
                String fecha_doc, fecha_vcto, mes_reporte, mes_doc, mes_vcto;
                DateTime fecha1, fecha2;
                iDB2Connection con2 = new iDB2Connection(conexion);
                string Select;
                if (rb_resumida.Checked)
                {
                    Select = "SELECT DISTINCT RARL01.RCCUS, RARL01.RCUST,  SUM(RAMT) as RAMT, RARL01.RDATE, " +
                        "RARL01.RDDTE FROM RARL01, RCM " +
                        "WHERE ( RARL01.RCCUS = RCM.CCCUS  and RARL01.RCCUS = RCM.CCUST and RARL01.RDATE <= " + fecha_hasta + ") AND RARL01.RCCUS = " + parametro + " " +
                        "GROUP BY RARL01.RCCUS, RARL01.RDATE, RARL01.RDDTE,  RARL01.RCUST " +
                        "ORDER BY RARL01.RCCUS ASC";
                }
                else
                {
                    Select = "SELECT DISTINCT RARL01.RCCUS, RARL01.RCUST,  SUM(RAMT) as RAMT, RARL01.RDATE, " +
                        "RARL01.RDDTE FROM RARL01, RCM " +
                        "WHERE ( RARL01.RCCUS = RCM.CCCUS  and RARL01.RCCUS = RCM.CCUST and RARL01.RDATE <= " + fecha_hasta + ") AND RARL01.RCCUS = " + parametro + " AND RARL01.RINVC=" + parametro2 + " AND RARL01.RRID='" + parametro3 + "' " +
                        "GROUP BY RARL01.RCCUS, RARL01.RDATE, RARL01.RDDTE,  RARL01.RCUST " +
                        "ORDER BY RARL01.RCCUS ASC";
                }
                iDB2DataAdapter da2 = new iDB2DataAdapter("" + Select, con2);
                con2.Open();

                DataTable dt2 = new DataTable();
                da2.Fill(dt2);

                System.Data.DataTable tabla2 = new DataTable();
                DataColumn col = new DataColumn("clte");
                tabla2.Columns.Add(col);

                DataColumn col2 = new DataColumn("clten");
                tabla2.Columns.Add(col2);

                DataColumn col3 = new DataColumn("monto");
                tabla2.Columns.Add(col3);

                DataColumn col4 = new DataColumn("fechadoc");
                tabla2.Columns.Add(col4);

                DataColumn col5 = new DataColumn("fechavcto");
                tabla2.Columns.Add(col5);

                ldc_saldo_mes = 0;
                ldc_saldo_30_dias = 0;
                ldc_saldo1_30 = 0;
                ldc_saldo31_60 = 0;
                ldc_saldo61_90 = 0;
                ldc_saldo91 = 0;
                for (int ii = 0; ii < dt2.Rows.Count; ii++)
                {
                    fecha_doc = dt2.Rows[ii]["RDATE"].ToString();
                    fecha_vcto = dt2.Rows[ii]["RDDTE"].ToString();
                    //Preguntar si el mes de la fecha del documento esta dentro del mes que se pide el reporte y el doc tiene mas de 30 dias por vencerse
                    mes_reporte = fecha_hasta.Substring(4, 2);
                    mes_doc = fecha_doc.Substring(4, 2);
                    mes_vcto = fecha_vcto.Substring(4, 2);
                    if (Convert.ToDateTime(func.convierte_fecha_400(fecha_vcto, 1)) > Convert.ToDateTime(func.convierte_fecha_400(fecha_hasta, 1)))
                    //if (mes_reporte == mes_doc && Convert.ToInt32(mes_vcto) > Convert.ToInt32(mes_reporte))
                    {
                        //Calculo del los dias
                        fecha1 = Convert.ToDateTime(func.convierte_fecha_400(fecha_hasta, 1));
                        //fecha1 = Convert.ToDateTime(func.convierte_fecha_400(fecha_doc, 1));
                        fecha2 = Convert.ToDateTime(func.convierte_fecha_400(fecha_vcto, 1));
                        int dias = func.CalculateDays(fecha1, fecha2);
                        if (dias > 30)
                        {
                            //Acumula los montos
                            ldc_saldo_30_dias += Convert.ToDecimal(dt2.Rows[ii]["RAMT"].ToString());
                        }
                    }
                }
                con2.Close();
                gv_saldo.DataSource = tabla2;
                gv_saldo.DataBind();
            }

            catch (Exception ex)
            {
                mensaje = ex.Message;
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mensaje + "');", true);
            }

            return ldc_saldo_30_dias;
        }

        public decimal busca_saldo3(string parametro, string parametro2, string parametro3)
        {
            try
            {
                String fecha_doc, fecha_vcto, mes_reporte, mes_doc, mes_vcto;
                DateTime fecha1, fecha2;
                iDB2Connection con2 = new iDB2Connection(conexion);
                string Select;
                if (rb_resumida.Checked)
                {
                    Select = "SELECT DISTINCT RARL01.RCCUS, RARL01.RCUST,  SUM(RAMT) as RAMT, RARL01.RDATE, " +
                        "RARL01.RDDTE FROM RARL01, RCM " +
                        "WHERE ( RARL01.RCCUS = RCM.CCCUS  and RARL01.RCCUS = RCM.CCUST and RARL01.RDATE <= " + fecha_hasta + ") AND RARL01.RCCUS = " + parametro + " " +
                        "GROUP BY RARL01.RCCUS, RARL01.RDATE, RARL01.RDDTE,  RARL01.RCUST " +
                        "ORDER BY RARL01.RCCUS ASC";
                }
                else
                {
                    Select = "SELECT DISTINCT RARL01.RCCUS, RARL01.RCUST,  SUM(RAMT) as RAMT, RARL01.RDATE, " +
                        "RARL01.RDDTE FROM RARL01, RCM " +
                        "WHERE ( RARL01.RCCUS = RCM.CCCUS  and RARL01.RCCUS = RCM.CCUST and RARL01.RDATE <= " + fecha_hasta + ") AND RARL01.RCCUS = " + parametro + " AND RARL01.RINVC=" + parametro2 + " AND RARL01.RRID='" + parametro3 + "' " +
                        "GROUP BY RARL01.RCCUS, RARL01.RDATE, RARL01.RDDTE,  RARL01.RCUST " +
                        "ORDER BY RARL01.RCCUS ASC";
                }
                iDB2DataAdapter da3 = new iDB2DataAdapter("" + Select, con2);
                con2.Open();

                DataTable dt3 = new DataTable();
                da3.Fill(dt3);

                System.Data.DataTable tabla3 = new DataTable();
                DataColumn col = new DataColumn("clte");
                tabla3.Columns.Add(col);

                DataColumn col2 = new DataColumn("clten");
                tabla3.Columns.Add(col2);

                DataColumn col3 = new DataColumn("monto");
                tabla3.Columns.Add(col3);

                DataColumn col4 = new DataColumn("fechadoc");
                tabla3.Columns.Add(col4);

                DataColumn col5 = new DataColumn("fechavcto");
                tabla3.Columns.Add(col5);

                ldc_saldo_mes = 0;
                ldc_saldo_30_dias = 0;
                ldc_saldo1_30 = 0;
                ldc_saldo31_60 = 0;
                ldc_saldo61_90 = 0;
                ldc_saldo91 = 0;
                for (int iii = 0; iii < dt3.Rows.Count; iii++)
                {
                    fecha_doc = dt3.Rows[iii]["RDATE"].ToString();
                    fecha_vcto = dt3.Rows[iii]["RDDTE"].ToString();
                    //Preguntar si el mes de la fecha del documento esta dentro del mes que se pide el reporte y el doc tiene mas de 30 dias por vencerse
                    mes_reporte = fecha_hasta.Substring(4, 2);
                    mes_doc = fecha_doc.Substring(4, 2);
                    mes_vcto = fecha_vcto.Substring(4, 2);
                    if (Convert.ToDateTime(func.convierte_fecha_400(fecha_vcto, 1)) < Convert.ToDateTime(func.convierte_fecha_400(fecha_hasta, 1)))
                    //if (Convert.ToInt32(mes_vcto) < Convert.ToInt32(mes_reporte))
                    //if (Convert.ToDateTime(func.convierte_fecha_400(fecha_vcto, 1)) > Convert.ToDateTime(func.convierte_fecha_400(fecha_hasta, 1)))
                    {
                        //Calculo del los dias
                        fecha1 = Convert.ToDateTime(func.convierte_fecha_400(fecha_hasta, 1));
                        //fecha1 = Convert.ToDateTime(func.convierte_fecha_400(fecha_vcto, 1));
                        fecha2 = Convert.ToDateTime(func.convierte_fecha_400(fecha_vcto, 1));
                        int dias = func.CalculateDays(fecha2, fecha1);
                        if (dias >= 1 && dias <= 30)
                        {
                            //Acumula los montos
                            ldc_saldo1_30 += Convert.ToDecimal(dt3.Rows[iii]["RAMT"].ToString());
                        }
                    }
                }
                con2.Close();
                gv_saldo.DataSource = tabla3;
                gv_saldo.DataBind();
            }

            catch (Exception ex)
            {
                mensaje = ex.Message;
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mensaje + "');", true);
            }
            return ldc_saldo1_30;
        }

        public decimal busca_saldo4(string parametro, string parametro2, string parametro3)
        {
            try
            {
                String fecha_doc, fecha_vcto, mes_reporte, mes_doc, mes_vcto;
                DateTime fecha1, fecha2;
                iDB2Connection con2 = new iDB2Connection(conexion);
                string Select;
                if (rb_resumida.Checked)
                {
                    Select = "SELECT DISTINCT RARL01.RCCUS, RARL01.RCUST,  SUM(RAMT) as RAMT, RARL01.RDATE, " +
                        "RARL01.RDDTE FROM RARL01, RCM " +
                        "WHERE ( RARL01.RCCUS = RCM.CCCUS  and RARL01.RCCUS = RCM.CCUST and RARL01.RDATE <= " + fecha_hasta + ") AND RARL01.RCCUS = " + parametro + " " +
                        "GROUP BY RARL01.RCCUS, RARL01.RDATE, RARL01.RDDTE,  RARL01.RCUST " +
                        "ORDER BY RARL01.RCCUS ASC";
                }
                else
                {
                    Select = "SELECT DISTINCT RARL01.RCCUS, RARL01.RCUST,  SUM(RAMT) as RAMT, RARL01.RDATE, " +
                        "RARL01.RDDTE FROM RARL01, RCM " +
                        "WHERE ( RARL01.RCCUS = RCM.CCCUS  and RARL01.RCCUS = RCM.CCUST and RARL01.RDATE <= " + fecha_hasta + ") AND RARL01.RCCUS = " + parametro + " AND RARL01.RINVC=" + parametro2 + " AND RARL01.RRID='" + parametro3 + "' " +
                        "GROUP BY RARL01.RCCUS, RARL01.RDATE, RARL01.RDDTE,  RARL01.RCUST " +
                        "ORDER BY RARL01.RCCUS ASC";
                }
                iDB2DataAdapter da4 = new iDB2DataAdapter("" + Select, con2);
                con2.Open();

                DataTable dt4 = new DataTable();
                da4.Fill(dt4);

                System.Data.DataTable tabla4 = new DataTable();
                DataColumn col = new DataColumn("clte");
                tabla4.Columns.Add(col);

                DataColumn col2 = new DataColumn("clten");
                tabla4.Columns.Add(col2);

                DataColumn col3 = new DataColumn("monto");
                tabla4.Columns.Add(col3);

                DataColumn col4 = new DataColumn("fechadoc");
                tabla4.Columns.Add(col4);

                DataColumn col5 = new DataColumn("fechavcto");
                tabla4.Columns.Add(col5);

                ldc_saldo_mes = 0;
                ldc_saldo_30_dias = 0;
                ldc_saldo1_30 = 0;
                ldc_saldo31_60 = 0;
                ldc_saldo61_90 = 0;
                ldc_saldo91 = 0;
                for (int i4 = 0; i4 < dt4.Rows.Count; i4++)
                {
                    fecha_doc = dt4.Rows[i4]["RDATE"].ToString();
                    fecha_vcto = dt4.Rows[i4]["RDDTE"].ToString();
                    //Preguntar si el mes de la fecha del documento esta dentro del mes que se pide el reporte y el doc tiene mas de 30 dias por vencerse
                    mes_reporte = fecha_hasta.Substring(4, 2);
                    mes_doc = fecha_doc.Substring(4, 2);
                    mes_vcto = fecha_vcto.Substring(4, 2);
                    if (Convert.ToDateTime(func.convierte_fecha_400(fecha_vcto, 1)) < Convert.ToDateTime(func.convierte_fecha_400(fecha_hasta, 1)))
                    //if (Convert.ToInt32(mes_vcto) < Convert.ToInt32(mes_reporte))
                    {
                        //Calculo del los dias
                        fecha1 = Convert.ToDateTime(func.convierte_fecha_400(fecha_hasta, 1));
                        //fecha1 = Convert.ToDateTime(func.convierte_fecha_400(fecha_vcto, 1));
                        fecha2 = Convert.ToDateTime(func.convierte_fecha_400(fecha_vcto, 1));
                        int dias = func.CalculateDays(fecha2, fecha1);
                        if (dias >= 31 && dias <= 60) //&& Convert.ToInt32(mes_vcto) < Convert.ToInt32(mes_reporte))
                        {
                            //Acumula los montos
                            ldc_saldo31_60 += Convert.ToDecimal(dt4.Rows[i4]["RAMT"].ToString());
                        }
                    }
                }
                con2.Close();
                gv_saldo.DataSource = tabla4;
                gv_saldo.DataBind();
            }

            catch (Exception ex)
            {
                mensaje = ex.Message;
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mensaje + "');", true);
            }
            return ldc_saldo31_60;
        }

        public decimal busca_saldo5(string parametro, string parametro2, string parametro3)
        {
            try
            {
                String fecha_doc, fecha_vcto, mes_reporte, mes_doc, mes_vcto;
                DateTime fecha1, fecha2;
                iDB2Connection con2 = new iDB2Connection(conexion);
                string Select;
                if (rb_resumida.Checked)
                {
                    Select = "SELECT DISTINCT RARL01.RCCUS, RARL01.RCUST,  SUM(RAMT) as RAMT, RARL01.RDATE, " +
                        "RARL01.RDDTE FROM RARL01, RCM " +
                        "WHERE ( RARL01.RCCUS = RCM.CCCUS  and RARL01.RCCUS = RCM.CCUST and RARL01.RDATE <= " + fecha_hasta + ") AND RARL01.RCCUS = " + parametro + " " +
                        "GROUP BY RARL01.RCCUS, RARL01.RDATE, RARL01.RDDTE,  RARL01.RCUST " +
                        "ORDER BY RARL01.RCCUS ASC";
                }
                else
                {
                    Select = "SELECT DISTINCT RARL01.RCCUS, RARL01.RCUST,  SUM(RAMT) as RAMT, RARL01.RDATE, " +
                        "RARL01.RDDTE FROM RARL01, RCM " +
                        "WHERE ( RARL01.RCCUS = RCM.CCCUS  and RARL01.RCCUS = RCM.CCUST and RARL01.RDATE <= " + fecha_hasta + ") AND RARL01.RCCUS = " + parametro + " AND RARL01.RINVC=" + parametro2 + " AND RARL01.RRID='" + parametro3 + "' " +
                        "GROUP BY RARL01.RCCUS, RARL01.RDATE, RARL01.RDDTE,  RARL01.RCUST " +
                        "ORDER BY RARL01.RCCUS ASC";
                }
                iDB2DataAdapter da5 = new iDB2DataAdapter("" + Select, con2);
                con2.Open();

                DataTable dt5 = new DataTable();
                da5.Fill(dt5);

                System.Data.DataTable tabla5 = new DataTable();
                DataColumn col = new DataColumn("clte");
                tabla5.Columns.Add(col);

                DataColumn col2 = new DataColumn("clten");
                tabla5.Columns.Add(col2);

                DataColumn col3 = new DataColumn("monto");
                tabla5.Columns.Add(col3);

                DataColumn col4 = new DataColumn("fechadoc");
                tabla5.Columns.Add(col4);

                DataColumn col5 = new DataColumn("fechavcto");
                tabla5.Columns.Add(col5);

                ldc_saldo_mes = 0;
                ldc_saldo_30_dias = 0;
                ldc_saldo1_30 = 0;
                ldc_saldo31_60 = 0;
                ldc_saldo61_90 = 0;
                ldc_saldo91 = 0;
                for (int i5 = 0; i5 < dt5.Rows.Count; i5++)
                {
                    fecha_doc = dt5.Rows[i5]["RDATE"].ToString();
                    fecha_vcto = dt5.Rows[i5]["RDDTE"].ToString();
                    //Preguntar si el mes de la fecha del documento esta dentro del mes que se pide el reporte y el doc tiene mas de 30 dias por vencerse
                    mes_reporte = fecha_hasta.Substring(4, 2);
                    mes_doc = fecha_doc.Substring(4, 2);
                    mes_vcto = fecha_vcto.Substring(4, 2);
                    if (Convert.ToDateTime(func.convierte_fecha_400(fecha_vcto, 1)) < Convert.ToDateTime(func.convierte_fecha_400(fecha_hasta, 1)))
                    //if (Convert.ToInt32(mes_vcto) < Convert.ToInt32(mes_reporte))
                    {
                        //Calculo del los dias
                        fecha1 = Convert.ToDateTime(func.convierte_fecha_400(fecha_hasta, 1));
                        //fecha1 = Convert.ToDateTime(func.convierte_fecha_400(fecha_vcto, 1));
                        fecha2 = Convert.ToDateTime(func.convierte_fecha_400(fecha_vcto, 1));
                        int dias = func.CalculateDays(fecha2, fecha1);
                        if (dias >= 61 && dias <= 90) //&& Convert.ToInt32(mes_vcto) < Convert.ToInt32(mes_reporte))
                        {
                            //Acumula los montos
                            ldc_saldo61_90 += Convert.ToDecimal(dt5.Rows[i5]["RAMT"].ToString());
                        }
                    }
                }
                con2.Close();
                gv_saldo.DataSource = tabla5;
                gv_saldo.DataBind();
            }

            catch (Exception ex)
            {
                mensaje = ex.Message;
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mensaje + "');", true);
            }
            return ldc_saldo61_90;
        }

        public decimal busca_saldo6(string parametro, string parametro2, string parametro3)
        {
            try
            {
                String fecha_doc, fecha_vcto, mes_reporte, mes_doc, mes_vcto;
                DateTime fecha1, fecha2;
                iDB2Connection con2 = new iDB2Connection(conexion);
                string Select;
                if (rb_resumida.Checked)
                {
                    Select = "SELECT DISTINCT RARL01.RCCUS, RARL01.RCUST,  SUM(RAMT) as RAMT, RARL01.RDATE, " +
                        "RARL01.RDDTE FROM RARL01, RCM " +
                        "WHERE ( RARL01.RCCUS = RCM.CCCUS  and RARL01.RCCUS = RCM.CCUST and RARL01.RDATE <= " + fecha_hasta + ") AND RARL01.RCCUS = " + parametro + " " +
                        "GROUP BY RARL01.RCCUS, RARL01.RDATE, RARL01.RDDTE,  RARL01.RCUST " +
                        "ORDER BY RARL01.RCCUS ASC";
                }
                else
                {
                    Select = "SELECT RARL01.RCCUS, RARL01.RCUST,  SUM(RAMT) as RAMT, RARL01.RDATE, " +
                        "RARL01.RDDTE FROM RARL01, RCM " +
                        "WHERE ( RARL01.RCCUS = RCM.CCCUS  and RARL01.RCCUS = RCM.CCUST and RARL01.RDATE <= " + fecha_hasta + ") AND RARL01.RCCUS = " + parametro + " AND RARL01.RINVC=" + parametro2 + " AND RARL01.RRID='" + parametro3 + "' " +
                        "GROUP BY RARL01.RCCUS, RARL01.RDATE, RARL01.RDDTE,  RARL01.RCUST " +
                        "ORDER BY RARL01.RCCUS ASC";
                }
                iDB2DataAdapter da6 = new iDB2DataAdapter("" + Select, con2);
                con2.Open();

                DataTable dt6 = new DataTable();
                da6.Fill(dt6);

                System.Data.DataTable tabla6 = new DataTable();
                DataColumn col = new DataColumn("clte");
                tabla6.Columns.Add(col);

                DataColumn col2 = new DataColumn("clten");
                tabla6.Columns.Add(col2);

                DataColumn col3 = new DataColumn("monto");
                tabla6.Columns.Add(col3);

                DataColumn col4 = new DataColumn("fechadoc");
                tabla6.Columns.Add(col4);

                DataColumn col5 = new DataColumn("fechavcto");
                tabla6.Columns.Add(col5);

                ldc_saldo_mes = 0;
                ldc_saldo_30_dias = 0;
                ldc_saldo1_30 = 0;
                ldc_saldo31_60 = 0;
                ldc_saldo61_90 = 0;
                ldc_saldo91 = 0;
                for (int i6 = 0; i6 < dt6.Rows.Count; i6++)
                {
                    fecha_doc = dt6.Rows[i6]["RDATE"].ToString();
                    fecha_vcto = dt6.Rows[i6]["RDDTE"].ToString();
                    //Preguntar si el mes de la fecha del documento esta dentro del mes que se pide el reporte y el doc tiene mas de 30 dias por vencerse
                    mes_reporte = fecha_hasta.Substring(4, 2);
                    mes_doc = fecha_doc.Substring(4, 2);
                    mes_vcto = fecha_vcto.Substring(4, 2);
                    if (Convert.ToDateTime(func.convierte_fecha_400(fecha_vcto, 1)) < Convert.ToDateTime(func.convierte_fecha_400(fecha_hasta, 1)))
                    //if (Convert.ToInt32(mes_vcto) < Convert.ToInt32(mes_reporte))
                    {
                        //Calculo del los dias
                        fecha1 = Convert.ToDateTime(func.convierte_fecha_400(fecha_hasta, 1));
                        //fecha1 = Convert.ToDateTime(func.convierte_fecha_400(fecha_vcto, 1));
                        fecha2 = Convert.ToDateTime(func.convierte_fecha_400(fecha_vcto, 1));
                        int dias = func.CalculateDays(fecha2, fecha1);
                        if (dias >= 91) //&& Convert.ToInt32(mes_vcto) < Convert.ToInt32(mes_reporte))
                        {
                            //Acumula los montos
                            ldc_saldo91 += Convert.ToDecimal(dt6.Rows[i6]["RAMT"].ToString());
                        }
                    }
                }
                con2.Close();
                gv_saldo.DataSource = tabla6;
                gv_saldo.DataBind();
            }

            catch (Exception ex)
            {
                mensaje = ex.Message;
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mensaje + "');", true);
            }
            return ldc_saldo91;
        }

        protected void img_excel_Click(object sender, ImageClickEventArgs e)
        {
            if (rb_detalle.Checked == true)
            {
                GridViewExportUtil.Export("Devoluciones hasta_" + fecha_hasta + ".xls", this.gv_detalle);
            }
            if (rb_resumida.Checked == true)
            {
                GridViewExportUtil.Export("Devoluciones hasta_" + fecha_hasta + ".xls", this.gv_lista);
            }
        }

        //protected void chk_vnd_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (chk_vnd.Checked == true)
        //    {
        //        //gv_lista.DataBind();
        //        gv_lista.Visible = false;
        //        //btn_buscar_Click(null, null);
        //        llenar_grid_detalle();
        //    }
        //}

        protected void rb_vndSI_CheckedChanged(object sender, EventArgs e)
        {
            lbl_vndr.Visible = true;
            ddl_vndr.Visible = true;
            gv_detalle.DataBind();
            gv_lista.DataBind();
        }

        protected void rb_vndNO_CheckedChanged(object sender, EventArgs e)
        {
            lbl_vndr.Visible = false;
            ddl_vndr.Visible = false;
            gv_detalle.DataBind();
            gv_lista.DataBind();
        }

        protected void llenar_grid_detalle()
        {
            try
            {
                conexion = func.cadena_con(lbl_pais.Text);
                iDB2Connection connDB2 = new iDB2Connection(conexion);

                gv_detalle.DataKeyNames = new string[] { "clte" };

                iDB2DataAdapter da = new iDB2DataAdapter("" + cmdtext, connDB2);
                connDB2.Open();

                DataTable dt = new DataTable();
                da.Fill(dt);

                System.Data.DataTable tabla = new DataTable();
                DataColumn col = new DataColumn("vndr");
                tabla.Columns.Add(col);

                DataColumn col1 = new DataColumn("tipo_doc");
                tabla.Columns.Add(col1);

                DataColumn col2 = new DataColumn("num_doc");
                tabla.Columns.Add(col2);

                DataColumn col3 = new DataColumn("pref_doc");
                tabla.Columns.Add(col3);

                DataColumn col4 = new DataColumn("fecha_doc");
                tabla.Columns.Add(col4);

                DataColumn col5 = new DataColumn("fecha_vcto");
                tabla.Columns.Add(col5);

                DataColumn col6 = new DataColumn("clte");
                tabla.Columns.Add(col6);

                //DataColumn col7 = new DataColumn("monto_doc");
                //tabla.Columns.Add(col7);

                DataColumn col8 = new DataColumn("saldo");
                tabla.Columns.Add(col8);

                DataColumn col9 = new DataColumn("saldomes");
                tabla.Columns.Add(col9);

                DataColumn col10 = new DataColumn("saldo30dia");
                tabla.Columns.Add(col10);

                DataColumn col11 = new DataColumn("saldo1_30");
                tabla.Columns.Add(col11);

                DataColumn col12 = new DataColumn("saldo31_60");
                tabla.Columns.Add(col12);

                DataColumn col13 = new DataColumn("saldo61_90");
                tabla.Columns.Add(col13);

                DataColumn col14 = new DataColumn("saldo91");
                tabla.Columns.Add(col14);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = tabla.NewRow();
                    Session["source_table"] = dt;

                    dr["vndr"] = dt.Rows[i]["vendedorBD"].ToString();
                    dr["tipo_doc"] = dt.Rows[i]["RRID"].ToString();
                    tipo_doc = dt.Rows[i]["RRID"].ToString();
                    dr["num_doc"] = dt.Rows[i]["RINVC"].ToString();
                    num_doc = dt.Rows[i]["RINVC"].ToString();
                    dr["pref_doc"] = dt.Rows[i]["ARDPFX"].ToString();
                    dr["fecha_doc"] = func.convierte_fecha_400(dt.Rows[i]["RDATE"].ToString(), 1);
                    dr["fecha_vcto"] = func.convierte_fecha_400(dt.Rows[i]["RDDTE"].ToString(), 1);
                    dr["clte"] = dt.Rows[i]["clienteBD"].ToString();
                    cliente = dt.Rows[i]["clienteBD"].ToString();
                    cliente = cliente.Substring(0, 7);
                    //dr["saldo"] = dt.Rows[i]["RAMT"].ToString();

                    ldc_saldo_mes = busca_saldo1(cliente, num_doc, tipo_doc);
                    dr["saldomes"] = ldc_saldo_mes.ToString();
                    //Buscar el Saldo para la Columna Por Vencer en El Mes > 30 dias
                    ldc_saldo_30_dias = busca_saldo2(cliente, num_doc, tipo_doc);
                    dr["saldo30dia"] = ldc_saldo_30_dias.ToString();
                    //Buscar el Saldo para la Columna Montos Vencidos hasta 30 dias
                    ldc_saldo1_30 = busca_saldo3(cliente, num_doc, tipo_doc);
                    dr["saldo1_30"] = ldc_saldo1_30.ToString();
                    //Buscar el Saldo para la Columna Montos Vencidos hasta 60 dias
                    ldc_saldo31_60 = busca_saldo4(cliente, num_doc, tipo_doc);
                    dr["saldo31_60"] = ldc_saldo31_60.ToString();
                    //Buscar el Saldo para la Columna Montos Vencidos hasta 90 dias
                    ldc_saldo61_90 = busca_saldo5(cliente, num_doc, tipo_doc);
                    dr["saldo61_90"] = ldc_saldo61_90.ToString();
                    //Buscar el Saldo para la Columna Montos Vencidos >90 dias
                    ldc_saldo91 = busca_saldo6(cliente, num_doc, tipo_doc);
                    dr["saldo91"] = ldc_saldo91.ToString();

                    dr["saldo"] = (Decimal.Parse(dr["saldomes"].ToString()) + Decimal.Parse(dr["saldo30dia"].ToString()) + Decimal.Parse(dr["saldo1_30"].ToString()) + Decimal.Parse(dr["saldo31_60"].ToString()) + Decimal.Parse(dr["saldo61_90"].ToString()) + Decimal.Parse(dr["saldo91"].ToString())).ToString("N2");

                    //dr["saldo30dia"] = "0";
                    //dr["saldo1_30"] = "0";
                    //dr["saldo31_60"] = "0";
                    //dr["saldo61_90"] = "0";
                    //dr["saldo91"] = "0";
                    tabla.Rows.Add(dr);
                }

                connDB2.Close();
                gv_detalle.DataSource = tabla;

                GridViewHelper helper = new GridViewHelper(this.gv_detalle);
                helper.RegisterGroup("vndr", true, true);
                helper.RegisterGroup("clte", true, true);
                helper.RegisterGroup("num_doc", true, true);
                helper.GroupHeader += new GroupEvent(helper_GroupHeader);
                helper.ApplyGroupSort();
                //***********************************************************
                //Totales por Grupo
                if (rb_vndSI.Checked)
                {
                    helper.RegisterSummary("saldo", "{0:###,###,###,###,###.##}", SummaryOperation.Sum, "vndr");
                    helper.GroupSummary += new GroupEvent(helper_Bug);
                    helper.RegisterSummary("saldo", "{0:###,###,###,###,###.##}", SummaryOperation.Sum, "clte");
                    helper.GroupSummary += new GroupEvent(helper_Bug2);
                    helper.RegisterSummary("saldo", "{0:###,###,###,###,###.##}", SummaryOperation.Sum, "num_doc");
                    helper.GroupSummary += new GroupEvent(helper_Bug3);
                }
                else
                {
                    helper.RegisterSummary("saldo", "{0:###,###,###,###,###.##}", SummaryOperation.Sum, "clte");
                    helper.GroupSummary += new GroupEvent(helper_Bug);
                }

                //Total General
                helper.RegisterSummary("saldo", "{0:###,###,###,###,###.##}", SummaryOperation.Sum);
                helper.GeneralSummary += new FooterEvent(helper_GeneralSummary);

                gv_detalle.DataBind();
            }

            catch (Exception ex)
            {
                mensaje = ex.Message;
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mensaje + "');", true);
            }
        }

        protected void gv_detalle_sort(object sender, GridViewSortEventArgs e)
        {
        }

        private void helper_Bug(string groupName, object[] values, GridViewRow row)
        {
            if (groupName == null) return;

            if (groupName == "vndr")
            {
                row.Cells[0].Text = "Total Vendedor:";
                row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                row.Font.Bold = true;
                row.BackColor = System.Drawing.ColorTranslator.FromHtml("#F5F6CE");
            }

            if (groupName == "clte")
            {
                row.Cells[0].Text = "Total Cliente:";
                row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                row.Font.Bold = true;
                //row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                row.BackColor = System.Drawing.ColorTranslator.FromHtml("#5D7B9D");
            }

        }

        private void helper_Bug2(string groupName, object[] values, GridViewRow row)
        {
            if (groupName == null) return;

            if (groupName == "clte")
            {
                row.Cells[0].Text = "Total Cliente:";
                row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                row.Font.Bold = true;
                //row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF"); /*color blanco*/
                //row.BackColor = System.Drawing.ColorTranslator.FromHtml("#5D7B9D");#B3D5F7
                row.BackColor = System.Drawing.ColorTranslator.FromHtml("#B3D5F7");
            }

        }

        private void helper_Bug3(string groupName, object[] values, GridViewRow row)
        {
            if (groupName == null) return;

            if (groupName == "num_doc")
            {
                row.Cells[0].Text = "Total Doc:";
                row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
                row.Font.Bold = true;
                //row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF"); /*color blanco*/
                //row.BackColor = System.Drawing.ColorTranslator.FromHtml("#5D7B9D");#B3D5F7
                row.BackColor = System.Drawing.ColorTranslator.FromHtml("#B3D5F7");
            }

        }
        private void helper_GeneralSummary(GridViewRow row)
        {
            row.Cells[0].Text = "TOTAL GENERAL:";
            row.Cells[0].HorizontalAlign = HorizontalAlign.Right;

            row.Font.Bold = true;
            row.ForeColor = System.Drawing.Color.White;
            row.BackColor = System.Drawing.ColorTranslator.FromHtml("#5D7B9D");
        }

        private void helper_GroupHeader(string groupName, object[] values, GridViewRow row)
        {
            if (groupName == "vndr")
            {
                row.Cells[0].Font.Bold = true;
                row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
                //row.BackColor = System.Drawing.ColorTranslator.FromHtml("#B3D5F7");#F5F6CE
                row.BackColor = System.Drawing.ColorTranslator.FromHtml("#F5F6CE");
                row.Cells[0].Text = string.Format("Vendedor:   {0}", values[0].ToString());
            }
            if (groupName == "clte")
            {
                row.Cells[0].Font.Bold = true;
                row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
                row.BackColor = System.Drawing.ColorTranslator.FromHtml("#B3D5F7");
                row.Cells[0].Text = encab1 + " " + string.Format("Cliente:   {0}", values[0].ToString());
            }

            if (groupName == "num_doc")
            {
                row.Cells[0].Font.Bold = true;
                row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
                row.BackColor = System.Drawing.ColorTranslator.FromHtml("#B3D5F7");
                row.Cells[0].Text = encab1 + " " + string.Format("Docs:   {0}", values[0].ToString());
            }
        }

        protected void llenar_vndr()
        {
            try
            {
                conexion = func.cadena_con(lbl_pais.Text);
                iDB2Connection con = new iDB2Connection(conexion);
                string cmdtext = " SELECT SSAL,SSAL||'-'||SNAME AS mivend FROM SSML01 WHERE SMCOMP=" + cia + " ORDER BY SSAL ";

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

                    this.ddl_vndr.DataSource = ds;
                    this.ddl_vndr.DataValueField = "SSAL";
                    this.ddl_vndr.DataTextField = "mivend";
                    this.ddl_vndr.DataBind();
                    //lbl_pfx.Text = ddl_pfx.SelectedValue.ToString();
                    //this.ddl_cia.Items.Insert(0, new ListItem("<--elija opcion-->", "0"));
                    this.ddl_vndr.Items.Insert(0, new ListItem("", " "));
                    ddl_vndr.SelectedIndex = 1;
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

        protected void Selection_Change_vndr(object sender, EventArgs e)
        {
            vendedor = ddl_vndr.SelectedValue.ToString();
        }
    }

}