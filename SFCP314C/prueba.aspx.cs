using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IBM.Data.DB2.iSeries;

namespace SFCP314C
{
    public partial class prueba : System.Web.UI.Page
    {
        String mensaje = string.Empty;
        string conexion = System.Configuration.ConfigurationManager.ConnectionStrings["connectStringDES"].ConnectionString;
        string almacen = "PT", tipo = "R", fecha_desde = "20120721", fecha_hasta = "20120721";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetData();
            }
        }

        private void GetData()
        {
            
            DataTable table = new DataTable();
            using (iDB2Connection conn = new iDB2Connection(conexion))
            {
                string sql =
                        " SELECT distinct ITH.THWRKC," +
                        "         ITH.THWRKC || ' ' || LWKL01.WDESC as WDESC" +
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
            GridView1.DataSource = table;
            GridView1.DataBind();
        }

        protected void FindTheGridAndBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;
            GridView nestedGridView = (GridView)e.Row.FindControl("GridViewNested");
            int ctro_fab = int.Parse(GridView1.DataKeys[e.Row.RowIndex].Value.ToString());
            DataTable table = new DataTable();
            using (iDB2Connection conn = new iDB2Connection(conexion))
            {
                string sql =
                    //" SELECT ITH.THWRKC," +
                    //"         ITH.THWRKC || ' ' || LWKL01.WDESC as WDESC," +
                    " SELECT         ITH.TPROD as Producto," +
                    "         IIML01.IDESC as Descripcion," +
                    " SUM(CASE WHEN (( ITH.THTIME between 223000 and 235959 ) OR ( ITH.THTIME between 0 and 55959 )) THEN (ITH.TQTY * IIML01.IMFLPF) ELSE 0 END  ) as Turno3," +
                    " SUM(CASE WHEN ( ITH.THTIME between 60000 and 142959 ) THEN (ITH.TQTY * IIML01.IMFLPF) ELSE 0 END  ) as Turno1," +
                    " SUM(CASE WHEN ( ITH.THTIME between 143000 and 222959 ) THEN (ITH.TQTY * IIML01.IMFLPF) ELSE 0  END  ) as Turno2" +
                        //Sumar los turnos
                        //" SUM((CASE WHEN  (( ITH.THTIME between 223000 and 235959 ) OR ( ITH.THTIME between 0 and 55959 )) THEN (ITH.TQTY * IIML01.IMFLPF)  END  ) + " +
                        //" (CASE WHEN  ( ITH.THTIME between 60000 and 142959 ) THEN (ITH.TQTY * IIML01.IMFLPF)  END  ) + " +
                        //" (CASE WHEN  ( ITH.THTIME between 143000 and 222959 ) THEN (ITH.TQTY * IIML01.IMFLPF)  END  )) as BDtotal" +
                        //                    "         SUM(ITH.TQTY * IIML01.IMFLPF) valor_std " +
                    "    FROM ITH," +
                    "         IIML01," +
                    "         LWKL01" +
                    "   WHERE ( ITH.TPROD = IIML01.IPROD ) and  " +
                    "         ( ITH.THWRKC = LWKL01.WWRKC ) and  " +
                        //"         ( ( ITH.THTIME between 223000 and 235959 ) OR  " +
                        //"         ( ITH.THTIME between 0 and 55959 ) )  AND " +
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
            nestedGridView.DataSource = table;
            nestedGridView.DataBind();
        }

        //Sumar el Grupo
        decimal DETTotal1 = 0;
        decimal DETTotal2 = 0;
        decimal DETTotal3 = 0;
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
                    DETTotal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "total"));
                }
                else if (e.Row.RowType == DataControlRowType.Footer)
                {
                    e.Row.Cells[0].Text = "Subtotal";
                    e.Row.Cells[1].Text = DETTotal3.ToString("N3");
                    e.Row.Cells[2].Text = DETTotal1.ToString("N3");
                    e.Row.Cells[3].Text = DETTotal2.ToString("N3");
                    e.Row.Cells[4].Text = DETTotal.ToString("N3");
                    e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
                    e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Font.Bold = true;
                    DETTotal1 = 0;
                    DETTotal2 = 0;
                    DETTotal3 = 0;
                    DETTotal = 0;
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