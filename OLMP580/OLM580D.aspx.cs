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

namespace OLMP580
{
    public partial class OLM580D : System.Web.UI.Page
    {
        funciones_generales func = new funciones_generales();
        String mensaje = string.Empty;
        //string conexion = System.Configuration.ConfigurationManager.ConnectionStrings["connectStringPAN"].ConnectionString;
        public static int param, li_pais, filas;
        string conexion, nro_pedido, nota_entrega, factura, fechafac;
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
                
            }
        }

        protected void img_salir_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        protected void gv_lista_SelectedIndexChanged(object sender, EventArgs e)
        {
            //GridViewRow row = gv_lista.SelectedRow;
            //nota_entrega = row.Cells[2].Text;
            //factura = row.Cells[3].Text;
            //string fecha_rec = row.Cells[0].Text;

            //lbl_notaentrega.Text = nota_entrega;
            //lbl_factura.Text = factura;
        }

        protected void gv_lista_Paginar(object sender, GridViewPageEventArgs e)
        {
            GridView gv_lista = (GridView)sender;
            gv_lista.PageIndex = e.NewPageIndex;
            btn_buscar_Click(null, null);
            gv_lista.DataBind();
            //ocultar();
        }

        protected void llenar_Grid()
        {
            try
            {
                conexion = func.cadena_con(lbl_pais.Text);
                iDB2Connection connDB2 = new iDB2Connection(conexion);
                cmdtext = "SELECT LHADDT, LHCARR, LHLOAD, LHCINV, LHSDTE, LHCUSN || ' ' || RCML01.CNME AS LHCUSN FROM LLHL03,RCML01 WHERE LHCUSN = RCML01.CCUST AND LHORDN =" + txt_pedido.Text + " ORDER BY LHORDN desc";

                gv_lista.DataKeyNames = new string[] { "nota_entrega" };

                iDB2DataAdapter da = new iDB2DataAdapter("" + cmdtext, connDB2);
                connDB2.Open();

                DataTable dt = new DataTable();
                da.Fill(dt);

                System.Data.DataTable tabla = new DataTable();
                DataColumn col = new DataColumn("fecha");
                tabla.Columns.Add(col);

                DataColumn col2 = new DataColumn("placa");
                tabla.Columns.Add(col2);

                DataColumn col3 = new DataColumn("nota_entrega");
                tabla.Columns.Add(col3);

                DataColumn col4 = new DataColumn("doc");
                tabla.Columns.Add(col4);

                DataColumn col5 = new DataColumn("fecfac");
                tabla.Columns.Add(col5);
               
                DataColumn col6 = new DataColumn("clte");
                tabla.Columns.Add(col6);

                filas = dt.Rows.Count;
                for (int i = 0; i < filas; i++)
                {
                    DataRow dr = tabla.NewRow();
                    //Session["source_table"] = dt;

                    dr["fecha"] = dt.Rows[i]["LHADDT"].ToString();
                    dr["placa"] = dt.Rows[i]["LHCARR"].ToString();
                    dr["nota_entrega"] = dt.Rows[i]["LHLOAD"].ToString();
                    dr["doc"] = dt.Rows[i]["LHCINV"].ToString();
                    dr["fecfac"] = dt.Rows[i]["LHSDTE"].ToString();
                    dr["clte"] = dt.Rows[i]["LHCUSN"].ToString();
                    
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

        protected void btn_buscar_Click(object sender, ImageClickEventArgs e)
        {
            if (txt_pedido.Text == "")
            {
                mensaje = "Por Favor Ingrese el N° del Pedido...";
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mensaje + "');", true);
                txt_pedido.Focus();
                return;
            }
            llenar_Grid();

        }

        protected void gv_lista_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gv_lista.EditIndex = e.NewEditIndex;
            llenar_Grid();
            
        }

        protected void gv_lista_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        { 
            gv_lista.EditIndex = -1;
            llenar_Grid();
        }

        protected void gv_lista_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = (GridViewRow)gv_lista.Rows[e.RowIndex];

            nro_pedido = txt_pedido.Text;
            TextBox txtfecha = (TextBox)row.Cells[0].FindControl("TextBox1");
            Label nota_entrega = (Label)row.Cells[2].FindControl("Label4");
            Label factura = (Label)row.Cells[3].FindControl("Label5");
            Label fechafac = (Label)row.Cells[4].FindControl("Label7");

            DateTime hoy = System.DateTime.Now;
            DateTime fecha = Convert.ToDateTime(txtfecha.Text);

            //Valida la Fecha
            if (fecha > hoy)
            {
                mensaje = "Fecha Ingresada No puede ser mayor a la fecha de hoy...Verifique";
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mensaje + "');", true);
                return;
            }
            txtfecha.Text  = func.convierte_fecha_400(txtfecha.Text, 2);
            if (Convert.ToInt32(txtfecha.Text) < Convert.ToInt32(fechafac.Text))
            {
                mensaje = "Fecha Ingresada No puede ser menor a la fecha de la Factura...Verifique";
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mensaje + "');", true);
                return;
            }

            iDB2Command query;
            Object res;

            conexion = func.cadena_con(lbl_pais.Text);
            ;
            iDB2Connection con = new iDB2Connection(conexion);
            con = new iDB2Connection(conexion);
            con.Open();

            query = new iDB2Command();
            query.CommandType = System.Data.CommandType.Text;

            //query.CommandText = "UPDATE LLHL03 SET LHADDT=" + txtfecha.Text + " WHERE LHORDN=" + nro_pedido + " AND LHLOAD=" + nota_entrega.Text + " AND LHCINV=" + factura.Text + " AND LHADDT=0";
            query.CommandText = "UPDATE LLHL03 SET LHADDT=" + txtfecha.Text + " WHERE LHORDN=" + nro_pedido + " AND LHLOAD=" + nota_entrega.Text + " AND LHCINV=" + factura.Text ;

            query.Connection = con;

            res = new Object();
            res = query.ExecuteScalar();

            if (!(res is DBNull))
            {
                mensaje = "Registro Guardado Satisfactoriamente...";
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mensaje + "');", true);
            }

            gv_lista.EditIndex = -1;
            llenar_Grid();
            con.Close();
            gv_lista.DataBind();
        }
         
    }
}