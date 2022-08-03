using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IBM.Data.DB2.iSeries;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace ACRP597
{
    public partial class EdoCtaClte : System.Web.UI.Page
    {
        funciones_generales func = new funciones_generales();
        string conexion, cliente;
        String mensaje = string.Empty;
        //public static int pais;
        public string cmdtext, pais, dir_clte, tel_clte, ruc_clte, fecha_hasta, filt, encab1, encab2, miclte;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["clte"] == null)
                {
                    Response.Redirect("~/ACRP598.aspx");
                }
                cliente = Session["clte"].ToString();
                lbl_clte.Text = "N° CLIENTE " + cliente;
                pais = Session["pais"].ToString();
                lbl_ncliente.Text = Session["nom_clte"].ToString();
                lbl_fecha.Text = "A LA FECHA " + Session["fecha"].ToString();
                filt = Session["filtro"].ToString();
                miclte = Session["customer"].ToString();
                //Mostrar direccion del Cliente
                direccion();
                llenar_grid();
            }
        }

        protected void direccion()
        {
            string colname;
            conexion = func.cadena_con(pais);
            iDB2Connection connDB2 = new iDB2Connection(conexion);

            cmdtext = "SELECT TRIM(cad1) || ' ' || TRIM(cad2) || ' ' || TRIM(cad3), CPHON, CMFTXC FROM RCM WHERE CCUST=" + @cliente;
            iDB2Command cmd = connDB2.CreateCommand();
            cmd.CommandText = cmdtext;

            DataSet ds2 = new DataSet();
            iDB2DataAdapter da2 = new iDB2DataAdapter("" + cmdtext, connDB2);
            connDB2.Open();
            da2.Fill(ds2);

            colname = ds2.Tables[0].Columns[0].ColumnName;
            dir_clte = ds2.Tables[0].Rows[0][colname].ToString();
            lbl_dir.Text = dir_clte.ToString();

            colname = ds2.Tables[0].Columns[1].ColumnName;
            tel_clte = ds2.Tables[0].Rows[0][colname].ToString();
            lbl_tel.Text = tel_clte.ToString();

            colname = ds2.Tables[0].Columns[2].ColumnName;
            ruc_clte = ds2.Tables[0].Rows[0][colname].ToString();
            lbl_ruc.Text = "R.U.C. CLIENTE " + ruc_clte.ToString();

            connDB2.Close();
        }

        protected void llenar_grid()
        {
            try
            {
                conexion = func.cadena_con(pais);
                iDB2Connection connDB2 = new iDB2Connection(conexion);

                fecha_hasta = func.convierte_fecha_400(Session["fecha"].ToString(), 2);

                /********** SE EVALUA PARA EL CASO DE CLIENTE NATURAL **********/
                if (filt == "1" && miclte=="N")
                {
                    gv_resumen.Visible = false;
                    gv_lista.Visible = true;
                    cmdtext = "SELECT RARL02.RDATE," +
                                    " ZPAL01.DATA AS RAZON," +
                                    " SUBSTR(RARL02.ARCAIN,15,8) AS DOCFIS," +
                                    " RARL02.RREF," +
                                    " RDDTE," +
                                    " CASE WHEN (RARL02.ARDTYP=1) THEN ('FC') WHEN (RARL02.ARDTYP=2) THEN ('ND') WHEN (RARL02.ARDTYP=3) THEN('NC') WHEN (RARL02.ARDTYP=0) THEN ('PG') ELSE ' ' END AS ARDTYP," +
                                    " RARL02.RINVC," +
                                    " RARL02.ARODPX," +
                                    " CASE WHEN (RARL02.ARODTP=1) THEN ('FC') WHEN (RARL02.ARODTP=2) THEN ('ND') WHEN (RARL02.ARODTP=3) THEN ('NC') ELSE ' ' END AS ARODTP," +
                                    " RARL02.RAMT," +
                                    " ' ' as RREM," +
                                    " RARL02.RREM " +
                                    "FROM RARL02, ZPAL01 " +
                                    "WHERE ( RARL02.RCUST = " + cliente + ") AND " +
                                    "(SUBSTR(ZPAL01.PKEY,1,3)='REA' ) and " +
                                    "(SUBSTR(ZPAL01.PKEY,4,5)=RARL02.RRESN ) and " +
                                    "(RARL02.RREF <> 'UNISSUED') and " + 
                        //"( RARL01.RSEQ = 0 ) AND " +
                                    "( RARL02.RDATE <= " + fecha_hasta + " )" +
                                    " ORDER BY RINVC, RDATE, RSEQ";

                    gv_lista.DataKeyNames = new string[] { "sistema" };

                    iDB2DataAdapter da = new iDB2DataAdapter("" + cmdtext, connDB2);
                    connDB2.Open();

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    System.Data.DataTable tabla = new DataTable();
                    DataColumn col = new DataColumn("fecha");
                    tabla.Columns.Add(col);

                    DataColumn col2 = new DataColumn("detall");
                    tabla.Columns.Add(col2);

                    DataColumn col3 = new DataColumn("fiscal");
                    tabla.Columns.Add(col3);

                    DataColumn col4 = new DataColumn("sistema");
                    tabla.Columns.Add(col4);

                    DataColumn col5 = new DataColumn("ref");
                    tabla.Columns.Add(col5);

                    DataColumn col6 = new DataColumn("prfo");
                    tabla.Columns.Add(col6);

                    DataColumn col7 = new DataColumn("tpo");
                    tabla.Columns.Add(col7);

                    DataColumn col8 = new DataColumn("vcto");
                    tabla.Columns.Add(col8);

                    DataColumn col9 = new DataColumn("tpd");
                    tabla.Columns.Add(col9);

                    DataColumn col10 = new DataColumn("monto");
                    tabla.Columns.Add(col10);

                    //DataColumn col7 = new DataColumn("remanente");
                    //tabla.Columns.Add(col7);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = tabla.NewRow();
                        Session["source_table"] = dt;

                        dr["fecha"] = func.convierte_fecha_400(dt.Rows[i]["RDATE"].ToString(), 1);
                        dr["detall"] = dt.Rows[i]["RAZON"].ToString();
                        dr["fiscal"] = dt.Rows[i]["DOCFIS"].ToString();
                        dr["sistema"] = dt.Rows[i]["RINVC"].ToString();
                        dr["prfo"] = dt.Rows[i]["ARODPX"].ToString();
                        dr["tpo"] = dt.Rows[i]["ARODTP"].ToString();
                        dr["vcto"] = func.convierte_fecha_400(dt.Rows[i]["RDDTE"].ToString(), 1);
                        dr["monto"] = (Decimal.Parse(dt.Rows[i]["RAMT"].ToString())).ToString("N2");
                        dr["tpd"] = dt.Rows[i]["ARDTYP"].ToString();
                        dr["ref"] = dt.Rows[i]["RREF"].ToString();
                        //dr["remanente"] = dt.Rows[i]["RREM"].ToString();

                        //ddl_tipo.Items.Insert(0, new ListItem("<--elija opcion-->", "0"));
                        tabla.Rows.Add(dr);

                        connDB2.Close();
                        gv_lista.DataSource = tabla;
                    }
                }
                if (filt == "0" && miclte =="N")
                {
                    gv_resumen.Visible = true;
                    gv_lista.Visible = false;
                    cmdtext = "SELECT RARL02.RRID," +
                                    " RARL02.RDATE," +
                                    " RARL02.RRESN," +
                                    " ZPAL01.DATA AS RAZON," +
                                    " CASE WHEN (RARL02.ARDTYP=1) THEN ('FC') WHEN (RARL02.ARDTYP=2) THEN ('ND') WHEN (RARL02.ARDTYP=3) THEN('NC') WHEN (RARL02.ARDTYP=0) THEN ('PG') ELSE ' ' END AS ARDTYP," +
                        //" RARL02.ARDTYP," +
                                    " RARL02.RINVC," +
                                    " SUBSTR(RARL02.ARCAIN,15,8) AS DOCFIS," +
                                    " RARL02.RSEQ," +
                                    " RARL02.RREF," +
                                    " RARL02.RDDTE," +
                                   "sum(RARL02.RAMT) as RAMT" +
                         " FROM RARL02, ZPAL01" +
                         " WHERE ( RARL02.RCUST = " + cliente + ") AND " +
                         " ( SUBSTR(ZPAL01.PKEY,1,3)='REA' ) and" +
                         " ( SUBSTR(ZPAL01.PKEY,4,5)=RARL02.RRESN ) and" +
                         " (RARL02.RREF <> 'UNISSUED') and " +
                        //( RARL02.RSEQ = 0 ) AND
                         "( RARL02.RDATE <= " + fecha_hasta + " )" +
                        " group by RARL02.RRID," +
                                 " RARL02.RDATE," +
                                 " RARL02.RRESN," +
                                 " RARL02.ARDTYP," +
                                 " ZPAL01.DATA," +
                                 " RARL02.ARCAIN," +
                                 " RARL02.RSEQ," +
                                 " RARL02.RINVC," +
                                 " RARL02.RDDTE," +
                                 " RARL02.Rref," +
                                 " RARL02.RAMT" +
                        " order by RARL02.rdate, RARL02.ARDTYP, RARL02.rinvc";

                    gv_resumen.DataKeyNames = new string[] { "sistema" };

                    iDB2DataAdapter da = new iDB2DataAdapter("" + cmdtext, connDB2);
                    connDB2.Open();

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    System.Data.DataTable tabla = new DataTable();
                    DataColumn col = new DataColumn("fecha");
                    tabla.Columns.Add(col);

                    DataColumn col2 = new DataColumn("detall");
                    tabla.Columns.Add(col2);

                    DataColumn col3 = new DataColumn("tpd");
                    tabla.Columns.Add(col3);

                    DataColumn col4 = new DataColumn("sistema");
                    tabla.Columns.Add(col4);

                    DataColumn col5 = new DataColumn("fiscal");
                    tabla.Columns.Add(col5);

                    DataColumn col6 = new DataColumn("ref");
                    tabla.Columns.Add(col6);

                    DataColumn col7 = new DataColumn("vcto");
                    tabla.Columns.Add(col7);

                    DataColumn col8 = new DataColumn("monto");
                    tabla.Columns.Add(col8);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = tabla.NewRow();
                        Session["source_table"] = dt;

                        dr["fecha"] = func.convierte_fecha_400(dt.Rows[i]["RDATE"].ToString(), 1);
                        dr["detall"] = dt.Rows[i]["RAZON"].ToString();
                        dr["fiscal"] = dt.Rows[i]["DOCFIS"].ToString();
                        dr["sistema"] = dt.Rows[i]["RINVC"].ToString();
                        dr["vcto"] = func.convierte_fecha_400(dt.Rows[i]["RDDTE"].ToString(), 1);
                        dr["monto"] = (Decimal.Parse(dt.Rows[i]["RAMT"].ToString())).ToString("N2");
                        dr["tpd"] = dt.Rows[i]["ARDTYP"].ToString();
                        dr["ref"] = dt.Rows[i]["RREF"].ToString();
                        //dr["remanente"] = dt.Rows[i]["RREM"].ToString();

                        //ddl_tipo.Items.Insert(0, new ListItem("<--elija opcion-->", "0"));
                        tabla.Rows.Add(dr);

                        connDB2.Close();
                        gv_resumen.DataSource = tabla;

                    }
                }

                /********** SE EVALUA PARA EL CASO DE CLIENTE CORPORATIVO **********/
                if (filt == "1" && miclte == "C")
                {
                    gv_resumen.Visible = false;
                    gv_lista.Visible = true;
                    cmdtext = "SELECT RARL02.RDATE," +
                                    " ZPAL01.DATA AS RAZON," +
                                    " SUBSTR(RARL02.ARCAIN,15,8) AS DOCFIS," +
                                    " RARL02.RREF," +
                                    " RDDTE," +
                                    " CASE WHEN (RARL02.ARDTYP=1) THEN ('FC') WHEN (RARL02.ARDTYP=2) THEN ('ND') WHEN (RARL02.ARDTYP=3) THEN('NC') WHEN (RARL02.ARDTYP=0) THEN ('PG') ELSE ' ' END AS ARDTYP," +
                                    " RARL02.RINVC," +
                                    " RARL02.ARODPX," +
                                    " CASE WHEN (RARL02.ARODTP=1) THEN ('FC') WHEN (RARL02.ARODTP=2) THEN ('ND') WHEN (RARL02.ARODTP=3) THEN ('NC') ELSE ' ' END AS ARODTP," +
                                    " RARL02.RAMT," +
                                    " ' ' as RREM," +
                                    " RARL02.RREM " +
                                    "FROM RARL02, ZPAL01 " +
                                    "WHERE ( RARL02.RCCUS = " + cliente + ") AND " +
                                    "(SUBSTR(ZPAL01.PKEY,1,3)='REA' ) and " +
                                    "(RARL02.RREF <> 'UNISSUED') and " +
                                    "(SUBSTR(ZPAL01.PKEY,4,5)=RARL02.RRESN ) and " +
                        //"( RARL01.RSEQ = 0 ) AND " +
                                    "( RARL02.RDATE <= " + fecha_hasta + " )" +
                                    " ORDER BY RINVC, RDATE, RSEQ";

                    gv_lista.DataKeyNames = new string[] { "sistema" };

                    iDB2DataAdapter da = new iDB2DataAdapter("" + cmdtext, connDB2);
                    connDB2.Open();

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    System.Data.DataTable tabla = new DataTable();
                    DataColumn col = new DataColumn("fecha");
                    tabla.Columns.Add(col);

                    DataColumn col2 = new DataColumn("detall");
                    tabla.Columns.Add(col2);

                    DataColumn col3 = new DataColumn("fiscal");
                    tabla.Columns.Add(col3);

                    DataColumn col4 = new DataColumn("sistema");
                    tabla.Columns.Add(col4);

                    DataColumn col5 = new DataColumn("ref");
                    tabla.Columns.Add(col5);

                    DataColumn col6 = new DataColumn("prfo");
                    tabla.Columns.Add(col6);

                    DataColumn col7 = new DataColumn("tpo");
                    tabla.Columns.Add(col7);

                    DataColumn col8 = new DataColumn("vcto");
                    tabla.Columns.Add(col8);

                    DataColumn col9 = new DataColumn("tpd");
                    tabla.Columns.Add(col9);

                    DataColumn col10 = new DataColumn("monto");
                    tabla.Columns.Add(col10);

                    //DataColumn col7 = new DataColumn("remanente");
                    //tabla.Columns.Add(col7);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = tabla.NewRow();
                        Session["source_table"] = dt;

                        dr["fecha"] = func.convierte_fecha_400(dt.Rows[i]["RDATE"].ToString(), 1);
                        dr["detall"] = dt.Rows[i]["RAZON"].ToString();
                        dr["fiscal"] = dt.Rows[i]["DOCFIS"].ToString();
                        dr["sistema"] = dt.Rows[i]["RINVC"].ToString();
                        dr["prfo"] = dt.Rows[i]["ARODPX"].ToString();
                        dr["tpo"] = dt.Rows[i]["ARODTP"].ToString();
                        dr["vcto"] = func.convierte_fecha_400(dt.Rows[i]["RDDTE"].ToString(), 1);
                        dr["monto"] = (Decimal.Parse(dt.Rows[i]["RAMT"].ToString())).ToString("N2");
                        dr["tpd"] = dt.Rows[i]["ARDTYP"].ToString();
                        dr["ref"] = dt.Rows[i]["RREF"].ToString();
                        //dr["remanente"] = dt.Rows[i]["RREM"].ToString();

                        //ddl_tipo.Items.Insert(0, new ListItem("<--elija opcion-->", "0"));
                        tabla.Rows.Add(dr);

                        connDB2.Close();
                        gv_lista.DataSource = tabla;

                    }
                }
                if (filt == "0" && miclte == "C")
                {
                    gv_resumen.Visible = true;
                    gv_lista.Visible = false;
                    cmdtext = "SELECT RARL02.RRID," +
                                    " RARL02.RDATE," +
                                    " RARL02.RRESN," +
                                    " ZPAL01.DATA AS RAZON," +
                                    " CASE WHEN (RARL02.ARDTYP=1) THEN ('FC') WHEN (RARL02.ARDTYP=2) THEN ('ND') WHEN (RARL02.ARDTYP=3) THEN('NC') WHEN (RARL02.ARDTYP=0) THEN ('PG') ELSE ' ' END AS ARDTYP," +
                        //" RARL02.ARDTYP," +
                                    " RARL02.RINVC," +
                                    " SUBSTR(RARL02.ARCAIN,15,8) AS DOCFIS," +
                                    " RARL02.RSEQ," +
                                    " RARL02.RREF," +
                                    " RARL02.RDDTE," +
                                   "sum(RARL02.RAMT) as RAMT" +
                         " FROM RARL02, ZPAL01" +
                         " WHERE ( RARL02.RCCUS = " + cliente + ") AND " +
                         " ( SUBSTR(ZPAL01.PKEY,1,3)='REA' ) and" +
                         " ( SUBSTR(ZPAL01.PKEY,4,5)=RARL02.RRESN ) and" +
                         " (RARL02.RREF <> 'UNISSUED') and " +
                        //( RARL02.RSEQ = 0 ) AND
                         "( RARL02.RDATE <= " + fecha_hasta + " )" +
                        " group by RARL02.RRID," +
                                 " RARL02.RDATE," +
                                 " RARL02.RRESN," +
                                 " RARL02.ARDTYP," +
                                 " ZPAL01.DATA," +
                                 " RARL02.ARCAIN," +
                                 " RARL02.RSEQ," +
                                 " RARL02.RINVC," +
                                 " RARL02.RDDTE," +
                                 " RARL02.Rref," +
                                 " RARL02.RAMT" +
                        " order by RARL02.rdate, RARL02.ARDTYP, RARL02.rinvc";

                    gv_resumen.DataKeyNames = new string[] { "sistema" };

                    iDB2DataAdapter da = new iDB2DataAdapter("" + cmdtext, connDB2);
                    connDB2.Open();

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    System.Data.DataTable tabla = new DataTable();
                    DataColumn col = new DataColumn("fecha");
                    tabla.Columns.Add(col);

                    DataColumn col2 = new DataColumn("detall");
                    tabla.Columns.Add(col2);

                    DataColumn col3 = new DataColumn("tpd");
                    tabla.Columns.Add(col3);

                    DataColumn col4 = new DataColumn("sistema");
                    tabla.Columns.Add(col4);

                    DataColumn col5 = new DataColumn("fiscal");
                    tabla.Columns.Add(col5);

                    DataColumn col6 = new DataColumn("ref");
                    tabla.Columns.Add(col6);

                    DataColumn col7 = new DataColumn("vcto");
                    tabla.Columns.Add(col7);

                    DataColumn col8 = new DataColumn("monto");
                    tabla.Columns.Add(col8);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = tabla.NewRow();
                        Session["source_table"] = dt;

                        dr["fecha"] = func.convierte_fecha_400(dt.Rows[i]["RDATE"].ToString(), 1);
                        dr["detall"] = dt.Rows[i]["RAZON"].ToString();
                        dr["fiscal"] = dt.Rows[i]["DOCFIS"].ToString();
                        dr["sistema"] = dt.Rows[i]["RINVC"].ToString();
                        dr["vcto"] = func.convierte_fecha_400(dt.Rows[i]["RDDTE"].ToString(), 1);
                        dr["monto"] = (Decimal.Parse(dt.Rows[i]["RAMT"].ToString())).ToString("N2");
                        dr["tpd"] = dt.Rows[i]["ARDTYP"].ToString();
                        dr["ref"] = dt.Rows[i]["RREF"].ToString();
                        //dr["remanente"] = dt.Rows[i]["RREM"].ToString();

                        //ddl_tipo.Items.Insert(0, new ListItem("<--elija opcion-->", "0"));
                        tabla.Rows.Add(dr);

                        connDB2.Close();
                        gv_resumen.DataSource = tabla;

                    }
                }


                //Copiar codigo para agrupar solo aplica a consulta detallada
                if (filt == "1")
                {
                    GridViewHelper helper = new GridViewHelper(this.gv_lista);
                    helper.RegisterGroup("sistema", true, true);
                    helper.RegisterGroup("prfo", true, true);
                    helper.GroupHeader += new GroupEvent(helper_GroupHeader);
                    helper.ApplyGroupSort();
                    //***********************************************************
                    //Totales por Grupo
                    helper.RegisterSummary("monto", "{0:###,###,###,###,###.##}", SummaryOperation.Sum, "sistema");
                    helper.GroupSummary += new GroupEvent(helper_Bug);

                    //Total General
                    helper.RegisterSummary("monto", "{0:###,###,###,###,###.##}", SummaryOperation.Sum);
                    helper.GeneralSummary += new FooterEvent(helper_GeneralSummary);
                }

                if (filt == "1")
                {
                    gv_lista.DataBind();
                }
                else
                {

                    gv_resumen.DataBind();
                }
            }

            catch (Exception ex)
            {
                mensaje = ex.Message;
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mensaje + "');", true);
            }
        }

        //void helper_GroupHeader(string groupName, object[] values, GridViewRow row)
        //{
        //    //throw new NotImplementedException();

        //}

        protected void gv_lista_sort(object sender, GridViewSortEventArgs e)
        {
        }

        protected void gv_resumen_sort(object sender, GridViewSortEventArgs e)
        {
        }

        /*Totalizar el GridView Resumido */
        decimal Total_Clte = 0;
        protected void gv_resumen_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int i = 0; i < gv_resumen.Rows.Count; i++)
            {
            }


            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Total_Clte += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "monto"));
                }
                else if (e.Row.RowType == DataControlRowType.Footer)
                {
                    e.Row.Cells[6].Text = "TOTAL";
                    e.Row.Cells[7].Text = Total_Clte.ToString("N2");
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

        private void helper_Bug(string groupName, object[] values, GridViewRow row)
        {
            if (groupName == null) return;


            row.Cells[0].Text = "SALDO DOCUMENTO:";
            row.Cells[0].HorizontalAlign = HorizontalAlign.Right;

            row.Font.Bold = true;
            row.BackColor = System.Drawing.ColorTranslator.FromHtml("#F5F6CE");

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
            if (groupName == "sistema")
            {
                row.Cells[0].Font.Bold = true;
                row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
                row.BackColor = System.Drawing.ColorTranslator.FromHtml("#B3D5F7");
                row.Cells[0].Text = string.Format("Doc Sistema:   {0}", values[0].ToString());
            }
            if (groupName == "prfo")
            {
                row.Cells[0].Font.Bold = true;
                row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
                row.BackColor = System.Drawing.ColorTranslator.FromHtml("#B3D5F7");
                row.Cells[0].Text = encab1 + " " + string.Format("Prefijo:   {0}", values[0].ToString());
            }
        }

        protected void img_pdf_Click(object sender, ImageClickEventArgs e)
        {
            
        }

        public void ExportToPdf(DataTable ExDataTable)
        {
            //Here set page size as A4        
            Document pdfDoc = new Document(PageSize.A4, 10, 10, 10, 10);
            try
            {
                PdfWriter.GetInstance(pdfDoc, System.Web.HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();
                //Set Font Properties for PDF File            
                Font fnt = FontFactory.GetFont("Times New Roman", 12);
                DataTable dt = ExDataTable;
                if (dt != null)
                {
                    PdfPTable PdfTable = new PdfPTable(dt.Columns.Count);
                    PdfPCell PdfPCell = null;
                    //Here we create PDF file tables                
                    for (int rows = 0; rows < dt.Rows.Count; rows++)
                    {
                        if (rows == 0)
                        {
                            for (int column = 0; column < dt.Columns.Count; column++)
                            {
                                PdfPCell = new PdfPCell(new Phrase(new Chunk(dt.Columns[column].ColumnName.ToString(), fnt)));
                                PdfTable.AddCell(PdfPCell);
                            }
                        }
                        for (int column = 0; column < dt.Columns.Count; column++)
                        {
                            PdfPCell = new PdfPCell(new Phrase(new Chunk(dt.Rows[rows][column].ToString(), fnt)));
                            PdfTable.AddCell(PdfPCell);
                        }
                    }
                    // Finally Add pdf table to the document                 
                    pdfDoc.Add(PdfTable);
                }
                pdfDoc.Close();
                Response.ContentType = "application/pdf";
                //Set default file Name as current datetime
                Response.AddHeader("content-disposition", "attachment; filename=" + DateTime.Now.ToString("yyyyMMdd") + ".pdf");
                System.Web.HttpContext.Current.Response.Write(pdfDoc); Response.Flush(); Response.End();
            }
            catch (Exception ex) { Response.Write(ex.ToString()); }

        }

        protected void gv_resumen_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}