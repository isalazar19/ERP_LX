using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IBM.Data.DB2.iSeries;

namespace CIEPR5
{
    public partial class CIEPR5 : System.Web.UI.Page
    {
        string tipo, conexion, ls_userfile;
        String cmdtext, Select;
        String mensaje = string.Empty;
        funciones_generales func = new funciones_generales();
        //string conexion = System.Configuration.ConfigurationManager.ConnectionStrings["connectStringPAN"].ConnectionString;
        public static int param, li_pais;
        public static string usuario, ctro_old, causa_old;
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
                llenar_tipo();
            }
        }

        protected void img_salir_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        protected void llenar_tipo()
        {
            try
            {
                conexion = func.cadena_con(lbl_pais.Text);
                iDB2Connection con = new iDB2Connection(conexion);
                string cmdtext = "SELECT TTYPE, TTYPE||'-'||TDESC AS mitipo FROM ITEL02 ORDER BY TTYPE";

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

                    this.ddl_tipo.DataSource = ds;
                    this.ddl_tipo.DataValueField = "TTYPE";
                    this.ddl_tipo.DataTextField = "mitipo";
                    this.ddl_tipo.DataBind();
                    //lbl_pfx.Text = ddl_pfx.SelectedValue.ToString();
                    //this.ddl_cia.Items.Insert(0, new ListItem("<--elija opcion-->", "0"));

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

        protected void ddl_tipo_Selection_Change(object sender, EventArgs e)
        {
            tipo = ddl_tipo.SelectedValue.ToString();
            gv_lista.DataBind();
            ddl_causa.DataBind();
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
                cmdtext = "SELECT * FROM " + ls_userfile + ".CIEPF92 WHERE TRAB92='" + tipo + "' ORDER BY TRAB92,CODC92";

                gv_lista.DataKeyNames = new string[] { "campo1","campo2","campo3" };

                iDB2DataAdapter da = new iDB2DataAdapter("" + cmdtext, con);
                con.Open();

                DataTable dt = new DataTable();
                da.Fill(dt);

                System.Data.DataTable tabla = new DataTable();
                DataColumn col = new DataColumn("campo1");
                tabla.Columns.Add(col);

                DataColumn col2 = new DataColumn("campo2");
                tabla.Columns.Add(col2);

                DataColumn col3 = new DataColumn("campo3");
                tabla.Columns.Add(col3);

                DataColumn col4 = new DataColumn("campo4");
                tabla.Columns.Add(col4);

                DataColumn col5 = new DataColumn("campo5");
                tabla.Columns.Add(col5);

                //DataColumn col6 = new DataColumn("und");
                //tabla.Columns.Add(col6);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = tabla.NewRow();

                    dr["campo1"] = dt.Rows[i]["TRAB92"].ToString();
                    dr["campo2"] = dt.Rows[i]["CODC92"].ToString();
                    dr["campo3"] = dt.Rows[i]["CTRA92"].ToString();
                    dr["campo4"] = dt.Rows[i]["COLU92"].ToString();
                    dr["campo5"] = dt.Rows[i]["TRAC92"].ToString();
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
            lbl_panel_datos.Text = "Datos p/Mant.Tabla Equivalencias";

            GridViewRow row = gv_lista.SelectedRow;

            tipo = ddl_tipo.SelectedValue.ToString();
            codigo = row.Cells[1].Text;
            txt_codigo.Text = codigo;
            ddl_causa.SelectedValue = row.Cells[2].Text;
            causa_old = row.Cells[2].Text;
            ddl_ctro.SelectedValue = row.Cells[3].Text;
            ctro_old = row.Cells[3].Text;
            txt_columna.Text = row.Cells[4].Text;
            txt_costo.Text = row.Cells[5].Text;
            //....

            lbl_codigo.Visible = true;
            txt_codigo.Visible = true;
            lbl_causa.Visible = true;
            ddl_causa.Visible = true;
            lbl_ctro.Visible = true;
            ddl_ctro.Visible = true;
            lbl_columna.Visible = true;
            txt_columna.Visible = true;
            lbl_costo.Visible = true;
            txt_costo.Visible = true;

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
            lbl_panel_datos.Text = "Datos p/Mant.Tabla Equivalencias";
            lbl_codigo.Visible = true;
            txt_codigo.Visible = true;
            lbl_causa.Visible = true;
            ddl_causa.Visible = true;
            lbl_ctro.Visible = true;
            ddl_ctro.Visible = true;
            lbl_columna.Visible = true;
            txt_columna.Visible = true;
            lbl_costo.Visible = true;
            txt_costo.Visible = true;

            txt_codigo.Enabled = true;
            ddl_causa.Enabled = true;
            ddl_ctro.Enabled = true;
            txt_columna.Enabled = true;
            txt_costo.Enabled = true;

            txt_codigo.Text = ddl_tipo.SelectedValue.ToString();
            ddl_causa.Items.Clear();
            ddl_ctro.Items.Clear();
            llenar_causa();
            llenar_centro_fab();
            b_incluir.Enabled = true;
            b_guardar.Enabled = true;
            lbl_param.Text = "1";
        }

        protected void llenar_causa()
        {
            try
            {
                conexion = func.cadena_con(lbl_pais.Text);
                iDB2Connection con = new iDB2Connection(conexion);
                string cmdtext = "SELECT SUBSTR(CHAR(PKEY), 7, 2) AS CODIGO, SUBSTR(CHAR(PKEY), 7, 2) ||' - '|| DATA AS midesc FROM ZPA " +
                                 "WHERE SUBSTR(CHAR(PKEY), 1, 4) = 'RCOD' AND SUBSTR(CHAR(PKEY), 5, 2) = '" + ddl_tipo.SelectedValue.ToString() + "'";  


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

                    this.ddl_causa.DataSource = ds;
                    this.ddl_causa.DataValueField = "CODIGO";
                    this.ddl_causa.DataTextField = "midesc";
                    this.ddl_causa.DataBind();
                    //lbl_pfx.Text = ddl_pfx.SelectedValue.ToString();
                    //this.ddl_cia.Items.Insert(0, new ListItem("<--elija opcion-->", "0"));
                    ddl_causa.Items.Insert(0, new ListItem("", "  "));

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
                Select.CommandText = "DELETE from " + ls_userfile + ".CIEPF92 " +
                                        " WHERE TRAB92='" + ddl_tipo.SelectedValue.ToString() + "'" +
                                        " AND CODC92='" + causa_old + "'" +
                                        " AND CTRA92=" + ctro_old;

                Select.Connection = con;

                res = Select.ExecuteNonQuery();
                if (res != 0)
                {
                    mensaje = "Registro Eliminado Satisfactoriamente...";
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mensaje + "');", true);
                    b_guardar.Enabled = false;
                    txt_codigo.Text = "";
                    ddl_causa.Items.Clear();
                    ddl_ctro.Items.Clear();
                    txt_columna.Text = "";
                    txt_costo.Text = "";
                }
                else
                {
                    mensaje = "Registro No Existe...";
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mensaje + "');", true);
                    b_guardar.Enabled = false;
                    txt_codigo.Text = "";
                    ddl_causa.Items.Clear();
                    ddl_ctro.Items.Clear();
                    txt_columna.Text = "";
                    txt_costo.Text = "";
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
                txt_columna.Text = "";
                txt_costo.Text = "";
                ddl_causa.Items.Clear();
                ddl_ctro.Items.Clear();
            }

        }

        protected void b_modificar_Click(object sender, EventArgs e)
        {
            ddl_causa.Enabled = true;
            ddl_ctro.Enabled = true;
            txt_columna.Enabled = true;
            txt_costo.Enabled = true;
            b_guardar.Enabled = true;
            ddl_causa.Items.Clear();
            ddl_ctro.Items.Clear();
            llenar_causa();
            llenar_centro_fab();
            lbl_param.Text = "2";
        }

        protected void b_guardar_Click(object sender, EventArgs e)
        {
            iDB2Command query;
            Object res;
            string codigo = txt_codigo.Text;

            conexion = func.cadena_con(lbl_pais.Text);
            ls_userfile = func.userfile(lbl_pais.Text);
            iDB2Connection con = new iDB2Connection(conexion);
            con = new iDB2Connection(conexion);
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
                query.CommandText = "INSERT INTO " + ls_userfile + ".CIEPF92 " +
                        "(TRAB92," +
                        "CODC92," +
                        "CTRA92," +
                        "COLU92," +
                        "TRAC92)" +
                        " VALUES('" + ddl_tipo.SelectedValue.ToString() + "','" +
                        ddl_causa.SelectedValue.ToString() + "'," +
                        ddl_ctro.SelectedValue.ToString() + "," +
                        txt_columna.Text + ",'" +
                        txt_costo.Text + "')";
            }
            else if (param == 2)
            {
                    query.CommandText = "UPDATE " + ls_userfile + ".CIEPF92 " +
                        "SET CTRA92='" + ddl_ctro.SelectedValue.ToString() + "'," +
                        " COLU92=" + txt_columna.Text + "," +
                        " TRAC92='" + txt_costo.Text + "'" +
                        " WHERE TRAB92='" + ddl_tipo.SelectedValue.ToString() + "'" +
                        " AND CODC92='" + causa_old + "'" +
                        " AND CTRA92=" + ctro_old;
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
                ddl_causa.Enabled = false;
                ddl_ctro.Enabled = false;
                txt_columna.Enabled = false;
                txt_costo.Enabled = false;
            }
            ocultar();
            btn_buscar_Click(null, null);
            con.Close();
            gv_lista.DataBind();
        }

        protected void ddl_causa_Selection_Change(object sender, EventArgs e)
        {
             
        }

        protected void llenar_centro_fab()
        {
            try
            {
                conexion = func.cadena_con(lbl_pais.Text);
                iDB2Connection con = new iDB2Connection(conexion);
                string cmdtext = "SELECT WWRKC, WWRKC||'-'||WDESC AS mictro FROM LWKL01 WHERE WLOAD=3 ";

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

                    this.ddl_ctro.DataSource = ds;
                    this.ddl_ctro.DataValueField = "WWRKC";
                    this.ddl_ctro.DataTextField = "mictro";
                    this.ddl_ctro.DataBind();
                    //lbl_pfx.Text = ddl_pfx.SelectedValue.ToString();
                    //this.ddl_cia.Items.Insert(0, new ListItem("<--elija opcion-->", "0"));
                    ddl_ctro.Items.Insert(0, new ListItem("", "0"));

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
            lbl_causa.Visible = false;
            ddl_causa.Visible = false;
            lbl_ctro.Visible = false;
            ddl_ctro.Visible = false;
            lbl_columna.Visible = false;
            txt_columna.Visible = false;
            lbl_costo.Visible = false;
            txt_costo.Visible = false;
        }

        protected void ddl_ctro_Selection_Change(object sender, EventArgs e)
        {

        }
    }
}