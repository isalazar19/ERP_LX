using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MENU_ERPLX
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {   
            
            // MENÚ DE NAVEGACIÓN DE LA APLICACIÓN

            // UR1 GERENCIA
            MenuItem mi1 = new MenuItem("UR1", "UR1", "", "UR.aspx?id=1", "_self");
            NavigationMenu.Items.Add(mi1);

            // UR2 DISTRIBUCION Y VENTAS
            MenuItem mi2 = new MenuItem("UR2", "UR2", "", "UR.aspx?id=2", "_self");
            NavigationMenu.Items.Add(mi2);

            // UR3 PLANIFICACION DE PRODUCCION
            MenuItem mi3 = new MenuItem("UR3", "UR3", "", "UR.aspx?id=3", "_self");
            NavigationMenu.Items.Add(mi3);

            // UR4 LOGISTICA DE DESPACHO
            MenuItem mi4 = new MenuItem("UR4", "UR4", "", "UR.aspx?id=4", "_self");
            NavigationMenu.Items.Add(mi4);

            // UR5 COSTOS
            MenuItem mi5 = new MenuItem("UR5", "UR5", "", "UR.aspx?id=5", "_self");
            NavigationMenu.Items.Add(mi5);

            // UR6 LOGISTICA DE PRODUCTOS TERMINADOS
            MenuItem mi6 = new MenuItem("UR6", "UR6", "", "UR.aspx?id=6", "_self");
            NavigationMenu.Items.Add(mi6);

            // UR7 COMPRAS
            MenuItem mi7 = new MenuItem("UR7", "UR7", "", "UR.aspx?id=7", "_self");
            NavigationMenu.Items.Add(mi7);

            // UR8 CONTABILIDAD
            MenuItem mi8 = new MenuItem("UR8", "UR8", "", "UR.aspx?id=8", "_self");
            NavigationMenu.Items.Add(mi8);

            // UR9 PRODUCCION
            MenuItem mi9 = new MenuItem("UR9", "UR9", "", "UR.aspx?id=9", "_self");
            NavigationMenu.Items.Add(mi9);

            // UR10 LOGISTICA DE PRODUCTOS TERMINADOS
            MenuItem mi10 = new MenuItem("UR10", "UR10", "", "UR.aspx?id=10", "_self");
            NavigationMenu.Items.Add(mi10);

            // UR12 PRESUPUESTO
            MenuItem mi12 = new MenuItem("UR12", "UR12", "", "UR.aspx?id=12", "_self");
            NavigationMenu.Items.Add(mi12);

            // UR13 LOGISTICA
            MenuItem mi13 = new MenuItem("UR13", "UR13", "", "UR.aspx?id=13", "_self");
            NavigationMenu.Items.Add(mi13);

            // UR14 CONTROL DE CALIDAD
            MenuItem mi14 = new MenuItem("UR14", "UR14", "", "UR.aspx?id=14", "_self");
            NavigationMenu.Items.Add(mi14);

        }
    }
}
