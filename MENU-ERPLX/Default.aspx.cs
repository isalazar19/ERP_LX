using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace MENU_ERPLX
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            

            
            // GRUPOS DE MENÚ ERP EXTRA LX

            // definición de tabla
            DataTable dt = new DataTable();

            // definición de columnas
            DataColumn dcMENU   = new DataColumn("MENU",   typeof(string));
            DataColumn dcTITULO = new DataColumn("TITULO", typeof(string));
            DataColumn dcURL    = new DataColumn("URL",    typeof(string));

            dt.Columns.AddRange(new DataColumn[] { dcMENU, dcTITULO, dcURL });

            // registro de filas
            // UR1
            dt.Rows.Add(new object[] { "UR1", "GERENCIA",
                                       "1" });
            // UR2
            dt.Rows.Add(new object[] { "UR2", "DISTRIBUCION Y VENTAS",
                                       "2" });
            // UR3
            dt.Rows.Add(new object[] { "UR3", "PLANIFICACION DE PRODUCCION",
                                       "3" });
            // UR4
            dt.Rows.Add(new object[] { "UR4", "LOGISTICA DE DESPACHO",
                                       "4" });
            // UR5
            dt.Rows.Add(new object[] { "UR5", "COSTOS",
                                       "5" });
            // UR6
            dt.Rows.Add(new object[] { "UR6", "LOGISTICA DE PRODUCTOS TERMINADOS",
                                       "6" });
            // UR7
            dt.Rows.Add(new object[] { "UR7", "COMPRAS",
                                       "7" });
            // UR8
            dt.Rows.Add(new object[] { "UR8", "CONTABILIDAD",
                                       "8" });
            // UR9
            dt.Rows.Add(new object[] { "UR9", "PRODUCCION",
                                       "9" });
            // UR10
            dt.Rows.Add(new object[] { "UR10", "CREDITO Y COBRANZAS",
                                       "10" });
            // UR12
            dt.Rows.Add(new object[] { "UR12", "PRESUPUESTO",
                                       "12" });
            // UR13
            dt.Rows.Add(new object[] { "UR13", "LOGISTICA",
                                       "13" });
            // UR14
            dt.Rows.Add(new object[] { "UR14", "CONTROL DE CALIDAD",
                                       "14" });


            // asignación de datos
            GridViewGruposDeMenu.DataSource = dt;
            GridViewGruposDeMenu.DataBind();

        }
    }
}
