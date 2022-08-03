using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IBM.Data.DB2.iSeries;

namespace ACRP597
{
    public partial class EdoCtaClte : System.Web.UI.Page
    {
        funciones_generales func = new funciones_generales();
        string conexion, cliente;
        String mensaje = string.Empty;
        //public static int pais;
        public string cmdtext, pais, dir_clte, tel_clte, ruc_clte, fecha_hasta;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["clte"] == null)
                {
                    Response.Redirect("~/ACRP597.aspx");
                }
                cliente = Session["clte"].ToString();
                lbl_clte.Text = "N° CLIENTE " + cliente;
                pais = Session["pais"].ToString();
                lbl_ncliente.Text = Session["nom_clte"].ToString();
                lbl_fecha.Text = "FECHA " + Session["fecha"].ToString();
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

                cmdtext = "SELECT RARL01.RDATE," +
                                " ZPAL01.DATA AS RAZON," +
                                " SUBSTR(RARL01.ARCAIN,15,8) AS DOCFIS," +
                                " CASE WHEN (RARL01.ARDTYP=1) THEN ('FC') WHEN (RARL01.ARDTYP=2) THEN ('ND') WHEN (RARL01.ARDTYP=3) THEN('NC') WHEN (RARL01.ARDTYP=0) THEN ('PG') ELSE ' ' END AS ARDTYP," +
                                " RARL01.RINVC," +
                                " RARL01.ARODPX," +
                                " RARL01.ARODTP," +
                                " RARL01.RAMT," +
                                " ' ' as RREM," +
                                " RARL01.RREM " +
                                "FROM RARL01, ZPAL01 " +
                                "WHERE ( RARL01.RCUST = " + cliente  + ") AND "+
                                "(SUBSTR(ZPAL01.PKEY,1,3)='REA' ) and " +
                                "(SUBSTR(ZPAL01.PKEY,4,5)=RARL01.RRESN ) and " +
                                //"( RARL01.RSEQ = 0 ) AND " +
                                "( RARL01.RDATE <= " + fecha_hasta +" )" +
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

                DataColumn col5 = new DataColumn("prfo");
                tabla.Columns.Add(col5);

                DataColumn col6 = new DataColumn("tpo");
                tabla.Columns.Add(col6);

                DataColumn col7 = new DataColumn("tpd");
                tabla.Columns.Add(col7);

                DataColumn col8 = new DataColumn("monto");
                tabla.Columns.Add(col8);

                //DataColumn col7 = new DataColumn("remanente");
                //tabla.Columns.Add(col7);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = tabla.NewRow();
                    Session["source_table"] = dt;

                    dr["fecha"] = dt.Rows[i]["RDATE"].ToString();
                    dr["detall"] = dt.Rows[i]["RAZON"].ToString();
                    dr["fiscal"] = dt.Rows[i]["DOCFIS"].ToString();
                    dr["sistema"] = dt.Rows[i]["RINVC"].ToString();
                    dr["prfo"] = dt.Rows[i]["ARODPX"].ToString();
                    dr["tpo"] = dt.Rows[i]["ARODTP"].ToString();
                    dr["monto"] = (Decimal.Parse(dt.Rows[i]["RAMT"].ToString())).ToString("N2");
                    dr["tpd"] = dt.Rows[i]["ARDTYP"].ToString();
                    //dr["remanente"] = dt.Rows[i]["RREM"].ToString();

                    //ddl_tipo.Items.Insert(0, new ListItem("<--elija opcion-->", "0"));
                    tabla.Rows.Add(dr);
                }

                connDB2.Close();
                gv_lista.DataSource = tabla;
                //Copiar codigo para agrupar
                GridViewHelper helper = new GridViewHelper(this.gv_lista);
                helper.RegisterGroup("sistema", true, true);
                helper.RegisterGroup("prfo", true, true);
                //helper.RegisterGroup("tpo", true, true);
                helper.GroupHeader += new GroupEvent(helper_GroupHeader);
                helper.ApplyGroupSort();
                //***********************************************************
                //Totales por Grupo
                helper.RegisterSummary("monto", "{0:###,###,###,###,###.##}", SummaryOperation.Sum, "sistema");
                helper.GroupSummary += new GroupEvent(helper_Bug);
                //helper.RegisterSummary("monto", "{0:###,###,###,###,###.##}", SummaryOperation.Sum, "prfo");
                //helper.GroupSummary += new GroupEvent(helper_Bug);
                //helper.RegisterSummary("monto", "{0:###,###,###,###,###.##}", SummaryOperation.Sum, "tpo");
                //helper.GroupSummary += new GroupEvent(helper_Bug);

                //Total General
                helper.RegisterSummary("monto", "{0:###,###,###,###,###.##}", SummaryOperation.Sum);
                helper.GeneralSummary += new FooterEvent(helper_GeneralSummary);
                gv_lista.DataBind();
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
            //if (groupName == "prfo")
            //{
            //    row.Cells[0].Font.Bold = true;
            //    row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
            //    row.BackColor = System.Drawing.ColorTranslator.FromHtml("#B3D5F7");
            //    row.Cells[0].Text = string.Format("Pref Orig:   {0}", values[0].ToString());
            //}
            //if (groupName == "tpo")
            //{
            //    row.Cells[0].Font.Bold = true;
            //    row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
            //    row.BackColor = System.Drawing.ColorTranslator.FromHtml("#B3D5F7");
            //    row.Cells[0].Text = string.Format("Tipo Doc Orig:   {0}", values[0].ToString());
            //}
        }

    }
}