using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.GridView;
using System.Data;
using IBM.Data.DB2.iSeries;

namespace CSTP20
{
    public partial class CSTP20 : System.Web.UI.Page
    {
        string tipo, conexion, ls_userfile;
        String cmdtext, Select;
        String mensaje = string.Empty;
        funciones_generales func = new funciones_generales();
        //string conexion = System.Configuration.ConfigurationManager.ConnectionStrings["connectStringPAN"].ConnectionString;
        public static int param, li_pais;
        public static string usuario, almacen, clase, clase_old;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["pais"] == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                lbl_pais.Text = Session["pais"].ToString();
                lbl_cia.Text = " - " + Session["cia"].ToString();
                lbl_panel.Text = "Selección de Parametro";
                llenar_almacen();
                llenar_clase();
            }
        }

        protected void img_salir_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        protected void ddl_tipo_Selection_Change(object sender, EventArgs e)
        {
            gv_lista.DataBind();
            ddl_clase.DataBind();
            ocultar();
        }

        protected void btn_buscar_Click(object sender, ImageClickEventArgs e)
        {
            tipo = ddl_tipo.SelectedValue.ToString();
            llenar_grid();
        }

        protected void llenar_grid()
        {
            try
            {
                conexion = func.cadena_con(lbl_pais.Text);
                iDB2Connection con = new iDB2Connection(conexion);

                //*Segun el Tipo de Insumo se ejecutan los siguientes Queries */
                //ejecutar_SQL(1);
                ls_userfile = func.userfile(lbl_pais.Text);
                cmdtext = "SELECT tipo20,clas20, icdes" +
                          " FROM " + ls_userfile + ".CSTPF20, IICL01 " +
                          "WHERE " + ls_userfile + ".CSTPF20.CLAS20 = IICL01.ICLAS AND " +
                          "TIPO20='" + ddl_almacen.SelectedValue.ToString() + "' AND " +
                          "IDEN15='" + ddl_tipo.SelectedValue.ToString() + "'" +
                          "ORDER BY TIPO20,CLAS20";

                gv_lista.DataKeyNames = new string[] { "tipo", "clase" };

                iDB2DataAdapter da = new iDB2DataAdapter("" + cmdtext, con);
                con.Open();

                DataTable dt = new DataTable();
                da.Fill(dt);

                System.Data.DataTable tabla = new DataTable();
                DataColumn col = new DataColumn("tipo");
                tabla.Columns.Add(col);

                DataColumn col2 = new DataColumn("clase");
                tabla.Columns.Add(col2);

                DataColumn col3 = new DataColumn("descripcion");
                tabla.Columns.Add(col3);

                //DataColumn col6 = new DataColumn("und");
                //tabla.Columns.Add(col6);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = tabla.NewRow();

                    dr["tipo"] = dt.Rows[i]["TIPO20"].ToString();
                    dr["clase"] = dt.Rows[i]["CLAS20"].ToString();
                    dr["descripcion"] = dt.Rows[i]["ICDES"].ToString();
                    //dr["und"] = dt.Rows[i]["RIUM03"].ToString();

                    //ddl_tipo.Items.Insert(0, new ListItem("<--elija opcion-->", "0"));
                    tabla.Rows.Add(dr);
                }

                con.Close();
                gv_lista.DataSource = tabla;
                gv_lista.DataBind();
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mensaje + "');", true);
            }
            b_incluir.Visible = true;
            b_modificar.Visible = true;
            b_eliminar.Visible = true;
            b_guardar.Visible = true;
        }

        protected void gv_lista_SelectedIndexChanged(object sender, EventArgs e)
        {
            string codigo;
            panel1.Visible = true;
            lbl_panel_datos.Text = "Datos p/Mant.Tabla Tipo Producto X Clas";

            GridViewRow row = gv_lista.SelectedRow;

            codigo = ddl_tipo.SelectedValue.ToString();
            txt_codigo.Text = codigo;
            txt_tipo.Text = ddl_almacen.SelectedValue.ToString();
            //row.Cells[1].Text;
            //txt_tipo.Text = row.Cells[2].Text;
            ddl_clase.SelectedValue = row.Cells[2].Text;
            clase_old = row.Cells[2].Text;

            lbl_codigo.Visible = true;
            txt_codigo.Visible = true;
            lbl_tipo2.Visible = true;
            txt_tipo.Visible = true;
            lbl_clase.Visible = true;
            ddl_clase.Visible = true;

            b_modificar.Enabled = true;
            b_eliminar.Enabled = true;
            b_guardar.Enabled = true;
        }

        protected void gv_lista_Paginar(object sender, GridViewPageEventArgs e)
        {
            GridView gv_lista = (GridView)sender;
            gv_lista.PageIndex = e.NewPageIndex;
            btn_buscar_Click(null, null);
            gv_lista.DataBind();
            ocultar();
        }

        protected void b_incluir_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            lbl_panel_datos.Text = "Datos p/Mant.Tabla Tipo Producto X Clas";
            lbl_codigo.Visible = true;
            txt_codigo.Visible = true;
            lbl_tipo2.Visible = true;
            txt_tipo.Visible = true;
            lbl_clase.Visible = true;
            ddl_clase.Visible = true;

            txt_codigo.Text = ddl_tipo.SelectedValue.ToString();
            txt_tipo.Text = ddl_almacen.SelectedValue.ToString();
            ddl_clase.Enabled = true;
            ddl_clase.Items.Clear();
            llenar_clase();
            b_incluir.Enabled = true;
            b_guardar.Enabled = true;
            lbl_param.Text = "1";
        }

        protected void b_eliminar_Click(object sender, EventArgs e)
        {
            iDB2Command Select;
            int res;

            try
            {
                conexion = func.cadena_con(lbl_pais.Text);
                ls_userfile = func.userfile(lbl_pais.Text);
                iDB2Connection con = new iDB2Connection(conexion);
                con = new iDB2Connection(conexion);
                con.Open();

                Select = new iDB2Command();
                Select.CommandType = System.Data.CommandType.Text;

                tipo = ddl_tipo.SelectedValue.ToString();
                /*Segun el Tipo de Insumo se ejecutan los siguientes Queries */
                Select.CommandText = "DELETE from " + ls_userfile + ".CSTPF20 " +
                                        " WHERE TIPO20='" + txt_tipo.Text + "'" +
                                        " AND CLAS20='" + clase_old + "'";

                Select.Connection = con;

                res = Select.ExecuteNonQuery();
                if (res != 0)
                {
                    mensaje = "Registro Eliminado Satisfactoriamente...";
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mensaje + "');", true);
                    b_guardar.Enabled = false;
                    txt_codigo.Text = "";
                    txt_tipo.Text = "";
                    ddl_clase.Items.Clear();
                    txt_tipo.Text = "";
                }
                else
                {
                    mensaje = "Registro No Existe...";
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mensaje + "');", true);
                    b_guardar.Enabled = false;
                    txt_codigo.Text = "";
                    ddl_clase.Items.Clear();
                    txt_tipo.Text = "";
                }
                ocultar();
                btn_buscar_Click(null, null);
                con.Close();
                gv_lista.DataBind();
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mensaje + "');", true);
                b_guardar.Enabled = false;
                txt_codigo.Text = "";
                txt_tipo.Text = "";
                ddl_clase.Items.Clear();
            }

        }

        protected void b_modificar_Click(object sender, EventArgs e)
        {
            ddl_clase.Enabled = true;
            ddl_clase.Items.Clear();
            llenar_clase();
            lbl_param.Text = "2";
        }

        protected void b_guardar_Click(object sender, EventArgs e)
        {
            try
            {
                iDB2Command query;
                Object res;
                string codigo = txt_codigo.Text;

                conexion = func.cadena_con(lbl_pais.Text);
                ls_userfile = func.userfile(lbl_pais.Text);
                iDB2Connection con = new iDB2Connection(conexion);
                con = new iDB2Connection(conexion);

                ////Validar 
                //string col = "tipo";

                //if (Buscar(txt_tipo.Text, ddl_clase.SelectedValue.ToString(), col, gv_lista ))
                //{
                //    //lblMensaje.Text = "Ya se encuentra estos registros";
                //}
                
               

                con.Open();

                query = new iDB2Command();
                query.CommandType = System.Data.CommandType.Text;
                param = Convert.ToInt32(lbl_param.Text);
                //li_pais = func.convierte_string_int(Session["pais"].ToString());
                //usuario = HttpContext.Current.User.Identity.Name;
                //longitud_cadena = usuario.Length;
                //usuario = func.login_Descomposicion(usuario);

                //string und = ddl_und.SelectedValue.ToString();
                //und = und.Substring(0, 2);

                if (param == 1)
                {
                    query.CommandText = "INSERT INTO " + ls_userfile + ".CSTPF20 " +
                            "(IDEN15," +
                            "TIPO20," +
                            "CLAS20)" +
                            " VALUES('" + ddl_tipo.SelectedValue.ToString() + "','" +
                            ddl_almacen.SelectedValue.ToString() + "','" +
                            ddl_clase.SelectedValue.ToString() + "')";
                }
                else if (param == 2)
                {
                    query.CommandText = "UPDATE " + ls_userfile + ".CSTPF20 " +
                        "SET CLAS20='" + ddl_clase.SelectedValue.ToString() + "'" +
                        " WHERE TIPO20='" + txt_tipo.Text + "'" +
                        " AND CLAS20='" + clase_old + "'";
                }
                query.Connection = con;

                res = new Object();
                res = query.ExecuteScalar();

                if (!(res is DBNull))
                {
                    mensaje = "Registro Guardado Satisfactoriamente...";
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mensaje + "');", true);
                    b_guardar.Enabled = false;
                    txt_codigo.Enabled = false;
                    ddl_clase.Enabled = false;
                    txt_tipo.Enabled = false;
                }
                ocultar();
                btn_buscar_Click(null, null);
                con.Close();
                gv_lista.DataBind();
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mensaje + "');", true);
                b_guardar.Enabled = false;
                txt_codigo.Text = "";
                txt_tipo.Text = "";
                ddl_clase.Items.Clear();
            }
        }

        protected void ddl_almacen_Selection_Change(object sender, EventArgs e)
        {
            almacen = ddl_almacen.SelectedValue.ToString();
            gv_lista.DataBind();
            ocultar();
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

        protected void ddl_clase_Selection_Change(object sender, EventArgs e)
        {
        }

        protected void llenar_clase()
        {

            try
            {
                conexion = func.cadena_con(lbl_pais.Text);
                iDB2Connection con = new iDB2Connection(conexion);
                string cmdtext = "SELECT ICLAS, ICLAS||'-'||ICDES AS miclase FROM IICL01 ORDER BY ICLAS";

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

                    this.ddl_clase.DataSource = ds;
                    this.ddl_clase.DataValueField = "ICLAS";
                    this.ddl_clase.DataTextField = "miclase";
                    this.ddl_clase.DataBind();
                    //lbl_pfx.Text = ddl_pfx.SelectedValue.ToString();
                    //this.ddl_cia.Items.Insert(0, new ListItem("<--elija opcion-->", "0"));
                    this.ddl_clase.Items.Insert(0, new ListItem("", " "));

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

        protected void ocultar()
        {
            panel1.Visible = false;
            lbl_codigo.Visible = false;
            txt_codigo.Visible = false;
            lbl_tipo2.Visible = false;
            txt_tipo.Visible = false;
            lbl_clase.Visible = false;
            ddl_clase.Visible = false;
        }

        //bool Buscar(string TextoABuscar, string buscar, string Columna, GridView grid)
        //{
        //    bool encontrado = false;
        //    if (TextoABuscar == string.Empty) return false;
        //    if (grid.Rows.Count == 0) return false;
        //    //grid.ClearSelection();
        //    if (Columna == string.Empty)
        //    {
        //        foreach (GridViewRow row in grid.Rows)
        //        {
        //            //foreach (GridViewCell cell in row.Cells)
        //                if (cell.Value == TextoABuscar && cell.Values == buscar)
        //                {
        //                    //row.Selected = true;
        //                    return true;
        //                }
        //        }
        //    }
        //    else
        //    {
        //        foreach (GridViewRow row in grid.Rows)
        //        {

        //            if (row.Cells[Columna].Value == TextoABuscar)
        //            {
        //                row.Selected = true;
        //                return true;
        //            }
        //        }
        //    }
        //    return encontrado;
        //}

    }

}