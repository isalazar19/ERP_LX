using System;
using System.Globalization;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;
using System.Data;
using IBM.Data.DB2.iSeries;

namespace ACRP122C
{
    public partial class ACRP122C : System.Web.UI.Page
    {
        string tipo, conexion, ls_userfile;
        String cmdtext, Select;
        String mensaje = string.Empty;
        funciones_generales func = new funciones_generales();
        public static int param, li_pais;
        public static string usuario, almacen;
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
                //lbl_panel.Text = "Selección de Parametro";
                llenar_almacen();
                btn_buscar_Click(null, null);
            }
        }

        protected void img_salir_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        protected void llenar_almacen()
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

                    this.ddl_almacen.DataSource = ds;
                    this.ddl_almacen.DataValueField = "LWHS";
                    this.ddl_almacen.DataTextField = "mialm";
                    this.ddl_almacen.DataBind();
                    //lbl_pfx.Text = ddl_pfx.SelectedValue.ToString();
                    //this.ddl_cia.Items.Insert(0, new ListItem("<--elija opcion-->", "0"));
                    this.ddl_almacen.Items.Insert(0, new ListItem("", " "));
                    this.ddl_almacen.SelectedIndex = 9;

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

        protected void ddl_almacen_Selection_Change(object sender, EventArgs e)
        {
            almacen = ddl_almacen.SelectedValue.ToString();
            btn_buscar_Click(null, null);
            gv_lista.DataBind();
            //ocultar();
        }

        protected void btn_buscar_Click(object sender, ImageClickEventArgs e)
        {
            almacen = ddl_almacen.SelectedValue.ToString();
            //ViewState["sortOrder"] = "";
            //llenar_grid("", "");
            llenar_grid();
        }

        protected void gv_lista_Paginar(object sender, GridViewPageEventArgs e)
        {
            GridView gv_lista = (GridView)sender;
            gv_lista.PageIndex = e.NewPageIndex;
            btn_buscar_Click(null, null);
            gv_lista.DataBind();
            //ocultar();
        }

        protected void gv_lista_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = gv_lista.SelectedRow;
            TextBox cod_causa = (TextBox)row.Cells[0].FindControl("TextBox9");
        }

        protected void gv_lista_sorting(object sender, GridViewSortEventArgs e)
        {
            //llenar_grid(e.SortExpression, sortOrder);
        }

        public string sortOrder
        {
            get
            {
                if (ViewState["sortOrder"].ToString() == "desc")
                {
                    ViewState["sortOrder"] = "asc";
                }
                else
                {
                    ViewState["sortOrder"] = "desc";
                }

                return ViewState["sortOrder"].ToString();
            }
            set
            {
                ViewState["sortOrder"] = value;
            }
        }
        ////Set & get sort state in viewstate
        //private string GVSortDirection
        //{
        //    get { return ViewState["SortDirection"] as string ?? "DESC"; }
        //    set { ViewState["SortDirection"] = value; }
        //}

        //private string GetSortDirection()
        //{
        //    switch (GVSortDirection)
        //    {
        //        //If previous ssort direction if ascending order then assign new direction as descending order
        //        case "ASC":
        //            GVSortDirection = "DESC";
        //            break;
        //        //If previous ssort direction if ascending order then assign new direction as ascending order
        //        case "DESC":
        //            GVSortDirection = "ASC";
        //            break;
        //    }
        //    return GVSortDirection;
        //}


        //protected void llenar_grid(string sortExp, string sortDir)
        protected void llenar_grid()
        {
            try
            {
                conexion = func.cadena_con(lbl_pais.Text);
                iDB2Connection con = new iDB2Connection(conexion);

                ls_userfile = func.userfile(lbl_pais.Text);
                cmdtext = "SELECT * FROM " + ls_userfile + ".RARPFRM WHERE RCOMP=" + Session["cia"].ToString() + " AND ARDPFX='" + almacen + "' ORDER BY RDATE DESC"; // AND TRES=''";

                gv_lista.DataKeyNames = new string[] { "cliente", "doc", "nc" };

                iDB2DataAdapter da = new iDB2DataAdapter("" + cmdtext, con);
                con.Open();

                DataTable dt = new DataTable();
                da.Fill(dt);

                System.Data.DataTable tabla = new DataTable();
                DataColumn col = new DataColumn("cliente");
                tabla.Columns.Add(col);

                DataColumn col2 = new DataColumn("doc");
                tabla.Columns.Add(col2);

                DataColumn col3 = new DataColumn("fecha");
                tabla.Columns.Add(col3);

                DataColumn col4 = new DataColumn("vndr");
                tabla.Columns.Add(col4);

                DataColumn col5 = new DataColumn("alm");
                tabla.Columns.Add(col5);

                DataColumn col6 = new DataColumn("nc");
                tabla.Columns.Add(col6);

                DataColumn col7 = new DataColumn("cant");
                tabla.Columns.Add(col7);

                DataColumn col8 = new DataColumn("num_placa");
                tabla.Columns.Add(col8);

                DataColumn col9 = new DataColumn("cod_causa");
                tabla.Columns.Add(col9);

                DataColumn col10 = new DataColumn("pedido");
                tabla.Columns.Add(col10);

                DataColumn col11 = new DataColumn("local");
                tabla.Columns.Add(col11);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Session["dt"] = dt;
                    DataRow dr = tabla.NewRow();

                    dr["cliente"] = dt.Rows[i]["RCUST"].ToString();
                    dr["doc"] = dt.Rows[i]["RINVC"].ToString();
                    dr["fecha"] = dt.Rows[i]["RDATE"].ToString();
                    dr["vndr"] = dt.Rows[i]["RSAL"].ToString();
                    dr["alm"] = dt.Rows[i]["ARDPFX"].ToString();
                    dr["nc"] = dt.Rows[i]["ARDOCN"].ToString();
                    dr["cant"] = dt.Rows[i]["SIQTY"].ToString();
                    dr["num_placa"] = dt.Rows[i]["SICARR"].ToString();
                    dr["cod_causa"] = dt.Rows[i]["TRES"].ToString();
                    dr["pedido"] = dt.Rows[i]["ARSORD"].ToString();
                    dr["local"] = dt.Rows[i]["LOCAL"].ToString();

                    //ddl_tipo.Items.Insert(0, new ListItem("<--elija opcion-->", "0"));
                    tabla.Rows.Add(dr);
                }
                /*
                DataSet myDataSet = new DataSet();
                da.Fill(myDataSet);
                DataView myDataView = new DataView();
                myDataView = myDataSet.Tables[0].DefaultView;
                if (sortExp != string.Empty)
                {
                    myDataView.Sort = string.Format("{0} {1}", sortExp, sortDir);
                }
                gv_lista.DataSource = myDataView;

                */
                con.Close();
                gv_lista.DataSource = tabla;
                gv_lista.DataBind();
            }

            catch (Exception ex)
            {
                mensaje = ex.Message;
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mensaje + "');", true);
            }
        }

        protected void gv_lista_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gv_lista.EditIndex = e.NewEditIndex;
            GridViewRow row = (GridViewRow)gv_lista.Rows[e.NewEditIndex];
            Label valor = (Label)row.Cells[0].FindControl("Label9");

            if(valor.Text!="01" || valor.Text=="")
            {
                e.Cancel = true;
                mensaje = "El Código de Causa se modificará si no tiene valor o es 01...Verifique";
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mensaje + "');", true);
                return;
            }
            llenar_grid();
        }

        protected void gv_lista_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gv_lista.EditIndex = -1;
            llenar_grid();
        }

        protected void gv_lista_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = (GridViewRow)gv_lista.Rows[e.RowIndex];

            TextBox cod_causa = (TextBox)row.Cells[0].FindControl("TextBox9");
            Label nc = (Label)row.Cells[1].FindControl("Label15");

            if(cod_causa.Text==null || cod_causa.Text == "")
            {
                mensaje = "Debe Ingresar el Código de Causa...Verifique";
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mensaje + "');", true);
                return;
            }
            
            //Quitar espacios en blanco de una cadena
            cod_causa.Text = func.remover_blancos(cod_causa.Text);

            iDB2Command query;
            Object res;

            conexion = func.cadena_con(lbl_pais.Text);
            ;
            iDB2Connection con = new iDB2Connection(conexion);
            con = new iDB2Connection(conexion);
            con.Open();

            query = new iDB2Command();
            query.CommandType = System.Data.CommandType.Text;
            ls_userfile = func.userfile(lbl_pais.Text);

            query.CommandText = "UPDATE " + ls_userfile + ".RARPFRM SET TRES='" + cod_causa.Text + "' WHERE RCOMP=" + Session["cia"].ToString() + " AND ARDPFX='" + almacen + "' AND ARDOCN=" + nc.Text;

            query.Connection = con;

            res = new Object();
            res = query.ExecuteScalar();

            if (!(res is DBNull))
            {
                mensaje = "Registro Guardado Satisfactoriamente...";
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mensaje + "');", true);
            }

            gv_lista.EditIndex = -1;
            llenar_grid();
            con.Close();
            gv_lista.DataBind();
        }

        protected void gv_lista_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
        }
    }
}