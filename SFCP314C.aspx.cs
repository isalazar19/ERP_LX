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

namespace ERP_LX
{
    public partial class SFCP314C : System.Web.UI.Page
    {
        funciones_generales func = new funciones_generales();
        String mensaje = string.Empty;
        string conexion = System.Configuration.ConfigurationManager.ConnectionStrings["connectStringPAN"].ConnectionString;
        string tipo, almacen, undidades, fecha_desde, fecha_hasta, tipo_consulta;

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
                llenar_ddlalm();
            }
        }

        protected void img_salir_Click(object sender, ImageClickEventArgs e)
        {
            Response.Write(" <script> window.open('','_parent',''); window.close(); </script>"); 
        }

        protected void llenar_ddlalm()
        {
            try
            {
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

        }

        protected void ddl_whs_Selection_Changed(object sender, EventArgs e)
        {
            tipo = ddl_alm.SelectedValue.ToString();
            gv_lista.DataBind();
        }

        protected void ddl_tipo_Selection_Change(object sender, EventArgs e)
        {
            tipo = ddl_tipo.SelectedValue.ToString();
            gv_lista.DataBind();
        }

        protected void ddl_und_Selection_Change(object sender, EventArgs e)
        {
            undidades = ddl_und.SelectedValue.ToString();
            gv_lista.DataBind();
        }

        protected void btn_buscar_Click(object sender, ImageClickEventArgs e)
        {
            fecha_desde = txt_fecha1.Text;
            fecha_desde = func.convierte_fecha_400(fecha_desde, 2);
            fecha_hasta = txt_fecha2.Text;
            fecha_hasta = func.convierte_fecha_400(fecha_hasta, 2);
            almacen = ddl_alm.SelectedValue.ToString();
            tipo = ddl_tipo.SelectedValue.ToString();
            undidades = ddl_und.SelectedValue.ToString();
            if(rb_resumido.Checked)
                tipo_consulta="R";
                llenar_grid_resumen();
            if (rb_detalle.Checked)
                tipo_consulta = "D";
                //llenar_grid_detalle();
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

        protected void llenar_grid_resumen()
        {
            try
            {
                iDB2Connection connDB2 = new iDB2Connection(conexion);
                String cmdtext = 
                    " SELECT ITH.THWRKC," +
                    "         ITH.THWRKC || ' ' || LWKL01.WDESC as WDESC," +
                    "         ITH.TPROD," +
                    "         IIML01.IDESC," +
                    " SUM(CASE WHEN  (( ITH.THTIME between 223000 and 235959 ) OR ( ITH.THTIME between 0 and 55959 )) THEN (ITH.TQTY * IIML01.IMFLPF) ELSE 0 END  ) as BDturno3," +
                    " SUM(CASE WHEN  ( ITH.THTIME between 60000 and 142959 ) THEN (ITH.TQTY * IIML01.IMFLPF) ELSE 0 END  ) as BDturno1," +
                    " SUM(CASE WHEN  ( ITH.THTIME between 143000 and 222959 ) THEN (ITH.TQTY * IIML01.IMFLPF) ELSE 0  END  ) as BDturno2" +
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
                    "         ( ( ITH.TWHS = '" + @almacen  + "' ) and  " +
                    "         ( ITH.TTYPE = '" + @tipo + "' ) and  " +
                    "         ( ITH.TTDTE between " + @fecha_desde + " and " + @fecha_hasta +" ) ) " +
                    "GROUP BY ITH.THWRKC,   " +
                    "         LWKL01.WDESC,   " +
                    "         ITH.TPROD,   " +
                    "         IIML01.IDESC   " +
                    "ORDER BY ITH.THWRKC ASC ";
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

                DataColumn col5 = new DataColumn("total");
                tabla.Columns.Add(col5);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = tabla.NewRow();

                    dr["ctro"] = dt.Rows[i]["WDESC"].ToString();
                    dr["turno3"] = (Decimal.Parse(dt.Rows[i]["BDturno3"].ToString())).ToString("N3");
                    dr["turno1"] = (Decimal.Parse(dt.Rows[i]["BDturno1"].ToString())).ToString("N3");
                    dr["turno2"] = (Decimal.Parse(dt.Rows[i]["BDturno2"].ToString())).ToString("N3");

                    dr["total"] = (Decimal.Parse(dr["turno3"].ToString()) + Decimal.Parse(dr["turno1"].ToString()) + Decimal.Parse(dr["turno2"].ToString())).ToString("N3");

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

        protected void rb_resumido_CheckedChanged(object sender, EventArgs e)
        {
            gv_lista.DataBind();
        }

        protected void rb_detalle_CheckedChanged(object sender, EventArgs e)
        {
            gv_lista.DataBind();
        }
        
    }
}