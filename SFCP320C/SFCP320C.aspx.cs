using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

namespace SFCP320C
{
    public partial class SFCP320C : System.Web.UI.Page
    {
        string seleccion_particion = ""; 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["nbPais"] == null)
                    etiquetaParticionSeleccionada.Text = "";
                else
                    etiquetaParticionSeleccionada.Text = Session["nbPais"].ToString();
                if (Session["pais"] == null)
                    seleccion_particion = "6";//Default Venezuela
                else
                    seleccion_particion = Session["pais"].ToString();
                switch (seleccion_particion)
                {
                    case "1":   /*COLOMBIA*/
                        General.bindDDL(CampoSeleccionInstalacion, new Datos(Datos.Conexiones.COLOMBIA).LlenarFacility(), "MFDESC", "MFFACL");
                        break;

                    case "2":  /*GUATEMALA*/
                        General.bindDDL(CampoSeleccionInstalacion, new Datos(Datos.Conexiones.GUATEMALA).LlenarFacility(), "MFDESC", "MFFACL");
                        break;

                    case "3":   /*PANAMA*/
                        General.bindDDL(CampoSeleccionInstalacion, new Datos(Datos.Conexiones.PANAMA).LlenarFacility(), "MFDESC", "MFFACL");
                        break;

                    case "4":  /*PERU*/
                        General.bindDDL(CampoSeleccionInstalacion, new Datos(Datos.Conexiones.PERU).LlenarFacility(), "MFDESC", "MFFACL");
                        break;

                    case "5":   /*TRINIDAD & TOBAGO*/
                        General.bindDDL(CampoSeleccionInstalacion, new Datos(Datos.Conexiones.TRINIDAD).LlenarFacility(), "MFDESC", "MFFACL");
                        break;

                    case "6":  /*VENEZUELA*/
                        General.bindDDL(CampoSeleccionInstalacion, new Datos(Datos.Conexiones.VENEZUELA).LlenarFacility(), "MFDESC", "MFFACL");
                        break;

                    case "7": /*prototitpo*/
                        General.bindDDL(CampoSeleccionInstalacion, new Datos(Datos.Conexiones.PROTOTIPO).LlenarFacility(), "MFDESC", "MFFACL");
                        break;
                }
            }
        }

        protected void img_salir_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        protected void CampoSeleccionInstalacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Session["pais"] == null)
                seleccion_particion = "6";//Default Venezuela
            else
                seleccion_particion = Session["pais"].ToString();
            switch (seleccion_particion)
            {
                case "1":   /*COLOMBIA*/
                    General.bindDDL(CampoSeleccionCentroDeTrabajo, new Datos(Datos.Conexiones.COLOMBIA).LlenarCtroWrk().Where(t => t.WFAC == CampoSeleccionInstalacion.SelectedValue), "WDESC", "WWRKC");
                    break;

                case "2":  /*GUATEMALA*/
                    General.bindDDL(CampoSeleccionCentroDeTrabajo, new Datos(Datos.Conexiones.GUATEMALA).LlenarCtroWrk().Where(t => t.WFAC == CampoSeleccionInstalacion.SelectedValue), "WDESC", "WWRKC");
                    break;

                case "3":   /*PANAMA*/
                    General.bindDDL(CampoSeleccionCentroDeTrabajo, new Datos(Datos.Conexiones.PANAMA).LlenarCtroWrk().Where(t => t.WFAC == CampoSeleccionInstalacion.SelectedValue), "WDESC", "WWRKC");
                    break;

                case "4":  /*PERU*/
                    General.bindDDL(CampoSeleccionCentroDeTrabajo, new Datos(Datos.Conexiones.PERU).LlenarCtroWrk().Where(t => t.WFAC == CampoSeleccionInstalacion.SelectedValue), "WDESC", "WWRKC");
                    break;

                case "5":   /*TRINIDAD & TOBAGO*/
                    General.bindDDL(CampoSeleccionCentroDeTrabajo, new Datos(Datos.Conexiones.TRINIDAD).LlenarCtroWrk().Where(t => t.WFAC == CampoSeleccionInstalacion.SelectedValue), "WDESC", "WWRKC");
                    break;

                case "6":  /*VENEZUELA*/
                    General.bindDDL(CampoSeleccionCentroDeTrabajo, new Datos(Datos.Conexiones.VENEZUELA).LlenarCtroWrk().Where(t => t.WFAC == CampoSeleccionInstalacion.SelectedValue), "WDESC", "WWRKC");
                    break;

                case "7": /*prototitpo*/
                    General.bindDDL(CampoSeleccionCentroDeTrabajo, new Datos(Datos.Conexiones.PROTOTIPO).LlenarCtroWrk().Where(t => t.WFAC == CampoSeleccionInstalacion.SelectedValue), "WDESC", "WWRKC");
                    break;
            }
        }
        
        protected void CampoSeleccionCentroDeTrabajo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int var = int.Parse(CampoSeleccionCentroDeTrabajo.SelectedValue);
            //General.bindDDL(CampoSeleccionCentroDeTrabajo, new Datos(Datos.Conexiones.PANAMA).LlenarCtroWrk().Where(t => t.WFAC == CampoSeleccionInstalacion.SelectedValue && t.WWRKC == var), "WDESC", "WWRKC");

            switch (CampoSeleccionCentroDeTrabajo.SelectedValue)
            {
                case "TODOS":
                    CampoCentroDeTrabajoDesde.Value = CampoRangoCentroDeTrabajoDesde.Text;
                    CampoCentroDeTrabajoHasta.Value = CampoRangoCentroDeTrabajoHasta.Text;
                    break;
                default:
                    CampoCentroDeTrabajoDesde.Value = CampoSeleccionCentroDeTrabajo.SelectedValue;
                    CampoCentroDeTrabajoHasta.Value = CampoSeleccionCentroDeTrabajo.SelectedValue;
                    break;
            }
        }

        protected void CampoSeleccionCentroDeTrabajo_DataBound(object sender, EventArgs e)
        {
            //ListItem li = new ListItem("****** - TODOS LOS CENTROS DE TRABAJO", "TODOS");
            //CampoSeleccionCentroDeTrabajo.Items.Insert(0, li);
            //CampoSeleccionCentroDeTrabajo_SelectedIndexChanged(sender, e);
        }

        protected void CampoRangoCentroDeTrabajo_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (CampoRangoCentroDeTrabajo.SelectedValue)
            {
                case "D":
                    CampoRangoCentroDeTrabajoDesde.Text = "430000";
                    CampoRangoCentroDeTrabajoHasta.Text = "439999";
                    PanelRangoCentroDeTrabajo.Visible = false;
                    break;
                case "R":
                    CampoRangoCentroDeTrabajoDesde.Text = "";
                    CampoRangoCentroDeTrabajoHasta.Text = "";
                    PanelRangoCentroDeTrabajo.Visible = true;
                    break;
                case "T":
                    CampoRangoCentroDeTrabajoDesde.Text = "0";
                    CampoRangoCentroDeTrabajoHasta.Text = "999999";
                    PanelRangoCentroDeTrabajo.Visible = false;
                    break;
            }
        }

        protected void BotonActualizarReporte_Click(object sender, EventArgs e)
        {
            int FechaDesde = int.Parse(campoFechaDesde.Text);
            int FechaHasta = int.Parse(campoFechaHasta.Text);
            int CtroWrkDesde = int.Parse(CampoRangoCentroDeTrabajoDesde.Text);
            int CtroWrkHasta = int.Parse(CampoRangoCentroDeTrabajoHasta.Text);
            string EstadoOrden = CampoEstadoOrden.SelectedValue;
            if (Session["pais"] == null)
                seleccion_particion = "6";//Default Venezuela
            else
                seleccion_particion = Session["pais"].ToString();
            switch (seleccion_particion)
            {
                case "1":   /*COLOMBIA*/
                    GridViewShinkrage.DataSource = new Datos(Datos.Conexiones.COLOMBIA).LlenarGV(FechaDesde,FechaHasta,CtroWrkDesde,CtroWrkHasta,EstadoOrden,seleccion_particion);
                    break;

                case "2":  /*GUATEMALA*/
                    GridViewShinkrage.DataSource = new Datos(Datos.Conexiones.GUATEMALA).LlenarGV(FechaDesde,FechaHasta,CtroWrkDesde,CtroWrkHasta,EstadoOrden,seleccion_particion);
                    break;

                case "3":   /*PANAMA*/
                    GridViewShinkrage.DataSource = new Datos(Datos.Conexiones.PANAMA).LlenarGV(FechaDesde,FechaHasta,CtroWrkDesde,CtroWrkHasta,EstadoOrden,seleccion_particion);
                    break;

                case "4":  /*PERU*/
                    GridViewShinkrage.DataSource = new Datos(Datos.Conexiones.PERU).LlenarGV(FechaDesde,FechaHasta,CtroWrkDesde,CtroWrkHasta,EstadoOrden,seleccion_particion);
                    break;

                case "5":   /*TRINIDAD & TOBAGO*/
                    GridViewShinkrage.DataSource = new Datos(Datos.Conexiones.TRINIDAD).LlenarGV(FechaDesde,FechaHasta,CtroWrkDesde,CtroWrkHasta,EstadoOrden,seleccion_particion);
                    break;

                case "6":  /*VENEZUELA*/
                    GridViewShinkrage.DataSource = new Datos(Datos.Conexiones.VENEZUELA).LlenarGV(FechaDesde,FechaHasta,CtroWrkDesde,CtroWrkHasta,EstadoOrden,seleccion_particion);
                    break;

                case "7": /*prototitpo*/
                    GridViewShinkrage.DataSource = new Datos(Datos.Conexiones.PROTOTIPO).LlenarGV(FechaDesde,FechaHasta,CtroWrkDesde,CtroWrkHasta,EstadoOrden,seleccion_particion);
                    break;
            }
            GridViewShinkrage.DataBind();
        }

        protected void GridViewShinkrage_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Decimal shinkrage = 0;
            Decimal validar = 0;
            Decimal diferencia = 0;
            Double porcentaje = 0;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[0].Text == "SO")
                {
                    e.Row.Cells[0].Text = "Abierta";
                    e.Row.Cells[0].ForeColor = Color.Green;
                }
                else
                {
                    e.Row.Cells[0].Text = "Cerrada";
                    e.Row.Cells[0].ForeColor = Color.Red;
                }
                validar = (Decimal)DataBinder.Eval(e.Row.DataItem, "CONSUMIDO");
                if (validar != 0)
                {
                    //shinkrage = (Decimal)DataBinder.Eval(e.Row.DataItem, "CONSUMIDO") - (Decimal)DataBinder.Eval(e.Row.DataItem, "PRODUCIDO") - (Decimal)DataBinder.Eval(e.Row.DataItem, "RECHAZADO");
                    //porcentaje = (Convert.ToDouble(shinkrage) / Convert.ToDouble((Decimal)DataBinder.Eval(e.Row.DataItem, "CONSUMIDO"))) * 100;
                    shinkrage = ((Decimal)DataBinder.Eval(e.Row.DataItem, "PRODUCIDO") + (Decimal)DataBinder.Eval(e.Row.DataItem, "RECHAZADO")) / (Decimal)DataBinder.Eval(e.Row.DataItem, "CONSUMIDO");
                    porcentaje = (1 - (Convert.ToDouble(shinkrage)))*100;
                    diferencia = (Decimal)DataBinder.Eval(e.Row.DataItem, "CONSUMIDO") - (Decimal)DataBinder.Eval(e.Row.DataItem, "PRODUCIDO") - (Decimal)DataBinder.Eval(e.Row.DataItem, "RECHAZADO");
                    e.Row.Cells[9].Text = diferencia.ToString("N1");
                    e.Row.Cells[10].Text = Convert.ToDouble(porcentaje).ToString("N2") + " %";
                }
                else
                {
                    porcentaje = 0;
                    e.Row.Cells[9].Text = "ALERT";
                    e.Row.Cells[9].ForeColor = Color.Red;
                    e.Row.Cells[10].Text = porcentaje.ToString("N2") + " %";
                }

            }
        }

        protected void GridViewShinkrage_DataBound(object sender, EventArgs e)
        {
            if (GridViewShinkrage.Rows.Count > 0)
            {
                Decimal consumido = 0;
                Decimal producido = 0;
                Decimal rechazo = 0;
                Decimal shinkrage = 0;
                Decimal porcentaje = 0;

                foreach (GridViewRow row in GridViewShinkrage.Rows)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        consumido += Decimal.Parse(row.Cells[6].Text);
                        producido += Decimal.Parse(row.Cells[7].Text);
                        rechazo += Decimal.Parse(row.Cells[8].Text);
                        shinkrage += Decimal.Parse(row.Cells[6].Text) - Decimal.Parse(row.Cells[7].Text) - Decimal.Parse(row.Cells[8].Text);
                    }
                }

                GridViewShinkrage.FooterRow.Cells[6].Text = consumido.ToString("N1");
                GridViewShinkrage.FooterRow.Cells[7].Text = producido.ToString("N1");
                GridViewShinkrage.FooterRow.Cells[8].Text = rechazo.ToString("N1");
                GridViewShinkrage.FooterRow.Cells[9].Text = shinkrage.ToString("N1");

                if (consumido != 0)
                {
                    porcentaje = (shinkrage / consumido) * 100;
                    GridViewShinkrage.FooterRow.Cells[10].Text = Convert.ToDouble(porcentaje).ToString("N2") + " %";
                }
                else
                {
                    GridViewShinkrage.FooterRow.Cells[9].Text = "ALERT";
                    porcentaje = 0;
                    GridViewShinkrage.FooterRow.Cells[10].Text = porcentaje.ToString("N2") + " %";
                }
            }

        }

        protected void CampoEstadoOrden_SelectedIndexChanged(object sender, EventArgs e)
        {
            //GridViewShinkrage.DataBind();
        }
    }
}