using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using IBM.Data.DB2.iSeries;


namespace ERP_LX
{
    public partial class ORDP101 : System.Web.UI.Page
    {
        string tipo,conexion,ls_userfile;
        String cmdtext, Select;
        String mensaje = string.Empty;
        funciones_generales func = new funciones_generales();
        //string conexion = System.Configuration.ConfigurationManager.ConnectionStrings["connectStringPAN"].ConnectionString;
        public static int param, li_pais;
        public static string usuario, ctro_old;
                
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
                llenar_und();
                llenar_art();
                llenar_centro_fab();
            }
        }

        protected void ejecutar_SQL(int param)
        {
            ls_userfile = func.userfile(lbl_pais.Text);
            switch (param)
            {
                case 1:
                    /*Segun el Tipo de Insumo se ejecutan los siguientes Queries */
                    switch (tipo)
                    {
                        case "IN": /* INSUMOS */
                        case "ST": /* STRETCH */
                        case "CA": /* CAPACIDAD */
                            cmdtext = "SELECT " + ls_userfile + ".ORDPF003.RCDI03 || ' ' || " + ls_userfile +".ORDPF003.RDIN03 AS myinsumo, " +
                                     ls_userfile +".ORDPF003.RIDR03," +
                                     ls_userfile +".ORDPF003.RITE03," +
                                     " IIML01.IDESC, " +
                                     ls_userfile +".ORDPF003.RCEN03, " +
                                     ls_userfile+".ORDPF003.RINS03, " +
                                     ls_userfile +".ORDPF003.RIUM03 " +
                                     "FROM " + ls_userfile +".ORDPF003, IIML01 " +
                                    "WHERE ( " + ls_userfile + ".ORDPF003.RITE03 = IIML01.IPROD ) and  " +
                                    "      ( ( " + ls_userfile + ".ORDPF003.RIDR03 = '" + ddl_tipo.SelectedValue.ToString() + "' ) ) ORDER BY " + ls_userfile + ".ORDPF003.RITE03";

                            break;

                        case "PT": /* PALETAS */
                            cmdtext = "SELECT " + ls_userfile +".ORDPF003.RCDI03 || ' ' || " + ls_userfile + ".ORDPF003.RDIN03 AS myinsumo, " +
                                      ls_userfile + ".ORDPF003.RIDR03," +
                                      ls_userfile + ".ORDPF003.RITE03," +
                                      " ' ' AS IDESC," +
                                      ls_userfile + ".ORDPF003.RCEN03," +
                                      ls_userfile + ".ORDPF003.RINS03, " +
                                      ls_userfile + ".ORDPF003.RIUM03 " +
                                      "FROM " + ls_userfile + ".ORDPF003 " +
                                     "WHERE " + ls_userfile + ".ORDPF003.RIDR03 = '" + ddl_tipo.SelectedValue.ToString() + "' ORDER BY " + ls_userfile + ".ORDPF003.RITE03";

                            break;
                    }
                    break;

                case 2:
                    /*Segun el Tipo de Insumo se ejecutan los siguientes Queries */
                    switch (tipo)
                    {
                        case "IN":
                        case "ST":
                        case "CA":
                        Select = "SELECT RIUM03,RMIN03,RMAX03,RPRO03,RQTY03 from " + ls_userfile + ".ORDPF003 " +
                                              " WHERE RITE03='" + ddl_art.SelectedValue.ToString() + "'" +
                                              " AND RIDR03='" + ddl_tipo.SelectedValue.ToString() + "'" +
                                              " AND RCEN03='" + ddl_ctro.SelectedValue.ToString() + "'" +
                                              " AND RCDI03=" + txt_codigo.Text;
                            break;

                        case "PT":
                            Select = "SELECT RIUM03,RMIN03,RMAX03,RPRO03,RQTY03 from " + ls_userfile + ".ORDPF003 " +
                                              " WHERE RIDR03='" + ddl_tipo.SelectedValue.ToString() + "'" +
                                              " AND RCDI03=" + txt_codigo.Text ;
                            break;
                    }
                    break;

                case 3:
                    break;
            }
        }

        protected void img_salir_Click(object sender, ImageClickEventArgs e)
        {
            //Response.Write(" <script> window.open('','_parent',''); window.close(); </script>");
            Response.Redirect("~/Default.aspx");
        }

        protected void llenar_und()
        {
            try
            {
                conexion = func.cadena_con(lbl_pais.Text);
                iDB2Connection con = new iDB2Connection(conexion);
                string cmdtext = "SELECT CCCODE, CCCODE||'-'||CCDESC AS miund FROM ZCCL01 WHERE CCTABL='UNITMEAS' ";

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

                    this.ddl_und.DataSource = ds;
                    this.ddl_und.DataValueField = "CCCODE";
                    this.ddl_und.DataTextField = "miund";
                    this.ddl_und.DataBind();
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

        protected void llenar_art()
        {
            try
            {
                conexion = func.cadena_con(lbl_pais.Text);
                iDB2Connection con = new iDB2Connection(conexion);
                string cmdtext = "SELECT IPROD, IPROD||'-'||IDESC AS miprod FROM IIML01 ORDER BY IPROD";

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

                    this.ddl_art.DataSource = ds;
                    this.ddl_art.DataValueField = "IPROD";
                    this.ddl_art.DataTextField = "miprod";
                    this.ddl_art.DataBind();
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

        protected void b_incluir_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            lbl_panel_datos.Text = "Datos p/Mant.Tabla Estandar de Peso Caja";
            lbl_codigo.Visible = true;
            txt_codigo.Visible = true;
            lbl_descripcion.Visible = true;
            txt_descripcion.Visible = true;
            lbl_und.Visible = true;
            ddl_und.Visible = true;
            lbl_articulo.Visible = true;
            ddl_art.Visible = true;
            lbl_ctro.Visible = true;
            ddl_ctro.Visible = true;
            lbl_peso_std.Visible = true;
            txt_peso_std.Visible = true;
            lbl_peso_min.Visible = true;
            txt_peso_min.Visible = true;
            lbl_peso_max.Visible = true;
            txt_peso_max.Visible = true;
            lbl_peso_prom.Visible = true;
            txt_peso_prom.Visible = true;
            lbl_prod_teor.Visible = true;
            txt_prod_teor.Visible = true;

            txt_codigo.Enabled = true;
            txt_descripcion.Enabled = true;
            ddl_und.Enabled = true;
            ddl_art.Enabled = true;
            ddl_ctro.Enabled = true;
            txt_peso_std.Enabled = true;
            txt_peso_min.Enabled = true;
            txt_peso_max.Enabled = true;
            txt_peso_prom.Enabled = true;
            txt_prod_teor.Enabled = true;

            txt_codigo.Text = "";
            string tipo = ddl_tipo.SelectedValue.ToString();
            switch (tipo)
            {
                case "IN":
                    txt_descripcion.Text = "INSUMO";
                    break;

                case "PT":
                    txt_descripcion.Text = "PALETA";
                    break;
            }
            ddl_ctro.Items.Clear();
            ddl_art.Items.Clear();
            ddl_und.Items.Clear();
            //ddl_ctro.Items.Insert(0, new ListItem("", ""));
            //ddl_art.Items.Insert(0, new ListItem("", "               "));
            ddl_ctro.Items.Insert(0, new ListItem("", "0"));
            ddl_art.Items.Insert(0, new ListItem("", "               "));

            txt_peso_std.Text = "";
            txt_peso_min.Text = "";
            txt_peso_max.Text = "";
            txt_peso_prom.Text = "";
            txt_prod_teor.Text = "";

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
                switch (tipo)
                {
                    case "IN":
                    case "ST":
                    case "CA":
                        Select.CommandText = "DELETE from " + ls_userfile + ".ORDPF003 " +
                                             " WHERE RITE03='" + ddl_art.SelectedValue.ToString() + "'" +
                                             " AND RIDR03='" + ddl_tipo.SelectedValue.ToString() + "'" +
                                             " AND RCEN03='" + ddl_ctro.SelectedValue.ToString() + "'" +
                                             " AND RCDI03=" + txt_codigo.Text;

                        break;

                    case "PT":
                        Select.CommandText = "DELETE from " + ls_userfile + ".ORDPF003 " +
                                             " WHERE RIDR03='" + ddl_tipo.SelectedValue.ToString() + "'" +
                                             " AND RCDI03=" + txt_codigo.Text;
                        break;

                }
                Select.Connection = con;

                res = Select.ExecuteNonQuery();
                if (res != 0)
                {
                    mensaje = "Registro Eliminado Satisfactoriamente...";
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mensaje + "');", true);
                    b_guardar.Enabled = false;
                    txt_codigo.Text = "";
                    txt_descripcion.Text = "";
                }
                else
                {
                    mensaje = "Registro No Existe...";
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mensaje + "');", true);
                    b_guardar.Enabled = false;
                    txt_codigo.Text = "";
                    txt_descripcion.Text = "";
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
                txt_descripcion.Text = "";
            }
        }

        protected void b_modificar_Click(object sender, EventArgs e)
        {
            txt_descripcion.Enabled = true;
            ddl_und.Enabled = true;
            txt_peso_max.Enabled = true;
            txt_peso_min.Enabled = true;
            txt_peso_prom.Enabled = true;
            txt_peso_std.Enabled = true;
            txt_prod_teor.Enabled = true;

            tipo = ddl_tipo.SelectedValue.ToString();
            switch (tipo)
            {
                case "IN":
                    ddl_art.Enabled = true;
                    ddl_ctro.Enabled = true;
                    break;

                case "ST":
                    ddl_art.Enabled = true;
                    break;

                case "PT":
                    break;

                case "CA":
                    ddl_art.Enabled = true;
                    ddl_ctro.Enabled = true;
                    break;
            }
            b_guardar.Enabled = true;
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

            string und = ddl_und.SelectedValue.ToString();

            und = und.Substring(0, 2);

            if (param == 1)
            {
                query.CommandText = "INSERT INTO " + ls_userfile + ".ORDPF003 " + 
                        "(RIDR03;" +
                        "RCDI03;" +
                        "RDIN03;" +
                        "RIUM03;" +
                        "RITE03;" +
                        "RCEN03;" +
                        "RINS03;" +
                        "RMIN03;" +
                        "RMAX03;" +
                        "RPRO03;" +
                        "RQTY03)" +
                        " VALUES('" + ddl_tipo.SelectedValue.ToString() + "';" +
                        txt_codigo.Text + ";'" +
                        txt_descripcion.Text + "';'" +
                        und + "';'" +
                        ddl_art.SelectedValue.ToString() + "';" +
                        ddl_ctro.SelectedValue.ToString() + ";" +
                        txt_peso_std.Text + ";" +
                        txt_peso_min.Text + ";" +
                        txt_peso_max.Text + ";" +
                        txt_peso_prom.Text + ";" +
                        txt_prod_teor.Text + ")";
            }
            else if (param == 2)
            {
                tipo = ddl_tipo.SelectedValue.ToString();
                if (tipo=="IN")
                {
                    query.CommandText = "UPDATE " + ls_userfile + ".ORDPF003 " +
                        "SET RDIN03='" + txt_descripcion.Text + "';" +
                        " RIUM03='" + und + "';" +
                        " RITE03='" + ddl_art.SelectedValue.ToString() + "';" +
                        " RCEN03='" + ddl_ctro.SelectedValue.ToString() + "';" +
                        " RINS03='" + txt_peso_std.Text + "';" +
                        " RMIN03='" + txt_peso_min.Text + "';" +
                        " RMAX03='" + txt_peso_max.Text + "';" +
                        " RPRO03='" + txt_peso_prom.Text + "';" +
                        " RQTY03='" + txt_prod_teor.Text + "'" +
                        " WHERE RITE03='" + ddl_art.SelectedValue.ToString() + "'" +
                        " AND RIDR03='" + ddl_tipo.SelectedValue.ToString() + "'" +
                        //" AND RCEN03='" + ddl_ctro.SelectedValue.ToString() + "'" +
                        " AND RCEN03='" + ctro_old + "'" +
                        " AND RCDI03=" + txt_codigo.Text;
                }
                else if (tipo == "PT")
                {
                    query.CommandText = "UPDATE " + ls_userfile + ".ORDPF003 " +
                        "SET RDIN03='" + txt_descripcion.Text + "';" +
                        " RIUM03='" + und + "';" +
                        //" RITE03='" + ddl_art.SelectedValue.ToString() + "'," + no va
                        //" RCEN03=" + ddl_ctro.SelectedValue.ToString() + "," + no va
                        " RINS03='" + txt_peso_std.Text + "';" +
                        " RMIN03='" + txt_peso_min.Text + "';" +
                        " RMAX03='" + txt_peso_max.Text + "';" +
                        " RPRO03='" + txt_peso_prom.Text + "';" +
                        " RQTY03='" + txt_prod_teor.Text + "'" +
                        " WHERE RIDR03='" + ddl_tipo.SelectedValue.ToString() + "'" +
                        " AND RCDI03=" + txt_codigo.Text;
                }
                else if (tipo == "ST")
                {
                    query.CommandText = "UPDATE " + ls_userfile + ".ORDPF003 " +
                        "SET RDIN03='" + txt_descripcion.Text + "';" +
                        " RIUM03='" + und + "';" +
                        " RITE03='" + ddl_art.SelectedValue.ToString() + "';" +
                        //" RCEN03=" + ddl_ctro.SelectedValue.ToString() + "," + no va
                        " RINS03='" + txt_peso_std.Text + "';" +
                        " RMIN03='" + txt_peso_min.Text + "';" +
                        " RMAX03='" + txt_peso_max.Text + "';" +
                        " RPRO03='" + txt_peso_prom.Text + "';" +
                        " RQTY03='" + txt_prod_teor.Text + "'" +
                        " WHERE RITE03='" + ddl_art.SelectedValue.ToString() + "'" +
                        " AND RIDR03='" + ddl_tipo.SelectedValue.ToString() + "'" +
                        //" AND RCEN03='" + ddl_ctro.SelectedValue.ToString() + "'" +
                        " AND RCEN03='" + ctro_old + "'" +
                        " AND RCDI03=" + txt_codigo.Text;
                }
                else if (tipo == "CA")
                {
                    query.CommandText = "UPDATE " + ls_userfile + ".ORDPF003 " +
                        "SET RDIN03='" + txt_descripcion.Text + "';" +
                        " RIUM03='" + und + "';" +
                        " RITE03='" + ddl_art.SelectedValue.ToString() + "';" +
                        " RCEN03='" + ddl_ctro.SelectedValue.ToString() + "';" +
                        " RINS03=" + txt_peso_std.Text + ";" +
                        " RMIN03=" + txt_peso_min.Text + ";" +
                        " RMAX03=" + txt_peso_max.Text + ";" +
                        " RPRO03=" + txt_peso_prom.Text + ";" +
                        " RQTY03='" + txt_prod_teor.Text + "'" +
                        " WHERE RITE03='" + ddl_art.SelectedValue.ToString() + "'" +
                        " AND RIDR03='" + ddl_tipo.SelectedValue.ToString() + "'" +
                        //" AND RCEN03='" + ddl_ctro.SelectedValue.ToString() + "'" +
                        " AND RCEN03='" + ctro_old + "'" +
                        " AND RCDI03=" + txt_codigo.Text;
                }
            }
            query.CommandText = query.CommandText.Replace(",", ".");
            query.CommandText = query.CommandText.Replace(";", ",");
            query.Connection = con;

            res = new Object();
            res = query.ExecuteScalar();

            if (!(res is DBNull))
            {
                mensaje = "Registro Guardado Satisfactoriamente...";
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mensaje + "');", true);
                b_guardar.Enabled = false;
                txt_codigo.Enabled = false;
                txt_descripcion.Enabled = false;
                ddl_und.Enabled = false;
                ddl_ctro.Enabled = false;
                ddl_art.Enabled = false;
                txt_peso_std.Enabled = false;
                txt_peso_min.Enabled = false;
                txt_peso_max.Enabled = false;
                txt_peso_prom.Enabled = false;
                txt_prod_teor.Enabled = false;
            }
            ocultar();
            btn_buscar_Click(null, null);
            con.Close();
            gv_lista.DataBind();

        }

        protected void ddl_tipo_Selection_Change(object sender, EventArgs e)
        {
            tipo = ddl_tipo.SelectedValue.ToString();
            gv_lista.DataBind();
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
                ejecutar_SQL(1);
                
                gv_lista.DataKeyNames = new string[] { "codigo" };

                iDB2DataAdapter da = new iDB2DataAdapter("" + cmdtext, con);
                con.Open();

                DataTable dt = new DataTable();
                da.Fill(dt);

                System.Data.DataTable tabla = new DataTable();
                DataColumn col = new DataColumn("codigo");
                tabla.Columns.Add(col);

                DataColumn col2 = new DataColumn("articulo");
                tabla.Columns.Add(col2);

                DataColumn col3 = new DataColumn("nombre");
                tabla.Columns.Add(col3);

                DataColumn col4 = new DataColumn("ctro");
                tabla.Columns.Add(col4);

                DataColumn col5 = new DataColumn("peso");
                tabla.Columns.Add(col5);

                //DataColumn col6 = new DataColumn("und");
                //tabla.Columns.Add(col6);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = tabla.NewRow();

                    dr["codigo"] = dt.Rows[i]["myinsumo"].ToString();
                    dr["articulo"] = dt.Rows[i]["RITE03"].ToString();
                    dr["nombre"] = dt.Rows[i]["IDESC"].ToString();
                    dr["ctro"] = dt.Rows[i]["RCEN03"].ToString();
                    dr["peso"] = dt.Rows[i]["RINS03"].ToString();
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
            lbl_panel_datos.Text = "Datos p/Mant.Tabla Estandar de Peso Caja";
            lbl_codigo.Visible = true;
            txt_codigo.Visible = true;
            lbl_descripcion.Visible = true;
            txt_descripcion.Visible = true;
            lbl_und.Visible = true;
            ddl_und.Visible = true;
            lbl_articulo.Visible = true;
            ddl_art.Visible = true;
            lbl_ctro.Visible = true;
            ddl_ctro.Visible = true;
            lbl_peso_std.Visible = true;
            txt_peso_std.Visible = true;
            lbl_peso_min.Visible = true;
            txt_peso_min.Visible = true;
            lbl_peso_max.Visible = true;
            txt_peso_max.Visible = true;
            lbl_peso_prom.Visible = true;
            txt_peso_prom.Visible = true;
            lbl_prod_teor.Visible = true;
            txt_prod_teor.Visible = true;

            GridViewRow row = gv_lista.SelectedRow;

            tipo = ddl_tipo.SelectedValue.ToString();

            codigo = row.Cells[1].Text;
            txt_codigo.Text = codigo.Substring(0, 2);
            txt_descripcion.Text = codigo.Substring(2, 50);
            switch (tipo)
            {
                case "IN":
                    ddl_art.SelectedValue = row.Cells[2].Text;
                    ddl_ctro.Items.Insert(0, new ListItem("", "0"));
                    ddl_ctro.SelectedValue = row.Cells[4].Text;
                    ctro_old = row.Cells[4].Text;
                    ddl_ctro.Items.Insert(0, new ListItem("", ""));
                    ddl_art.Items.Insert(0, new ListItem("", "               "));
                    break;

                case "ST":
                    ddl_art.SelectedValue = row.Cells[2].Text;
                    ddl_ctro.Items.Insert(0, new ListItem("", "0"));
                    ddl_ctro.SelectedValue = row.Cells[4].Text;
                    ctro_old = row.Cells[4].Text;
                    break;
                    
                case "PT":
                    string campo1;
                    campo1 = row.Cells[2].Text;
                    ddl_art.Items.Insert(0, new ListItem("", campo1));
                    ddl_art.SelectedValue = row.Cells[2].Text;
                    ddl_ctro.Items.Insert(0, new ListItem("", "0"));
                    ddl_ctro.SelectedValue = row.Cells[4].Text;
                    ctro_old = row.Cells[4].Text;
                    break;
               case "CA":
                    ddl_art.SelectedValue = row.Cells[2].Text;
                    ddl_ctro.SelectedValue = row.Cells[4].Text;
                    ddl_ctro.Items.Insert(0, new ListItem("", ""));
                    ddl_art.Items.Insert(0, new ListItem("", "               "));
                    ctro_old = row.Cells[4].Text;
                    break;
            }
            txt_peso_std.Text = row.Cells[5].Text;

            //colocar los campos de las columnas ocultas en el GridView
            datos_adicionales();


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

        protected void ocultar()
        {
            panel1.Visible = false;
            lbl_codigo.Visible = false;
            txt_codigo.Visible = false;
            lbl_descripcion.Visible = false;
            txt_descripcion.Visible = false;
            lbl_und.Visible = false;
            ddl_und.Visible = false;
            lbl_articulo.Visible = false;
            ddl_art.Visible = false;
            lbl_ctro.Visible = false;
            ddl_ctro.Visible = false;
            lbl_peso_std.Visible = false;
            txt_peso_std.Visible = false;
            lbl_peso_min.Visible = false;
            txt_peso_min.Visible = false;
            lbl_peso_max.Visible = false;
            txt_peso_max.Visible = false;
            lbl_peso_prom.Visible = false;
            txt_peso_prom.Visible = false;
            lbl_prod_teor.Visible = false;
            txt_prod_teor.Visible = false;
        }

        protected void ddl_und_Selection_Change(object sender, EventArgs e)
        {
            switch (ddl_tipo.SelectedValue.ToString())
            {
                case "IN":
                case "ST":
                case "CA":
                    llenar_art();
                    break;
                case "PT":
                    ddl_ctro.Items.Insert(0, new ListItem("", "0"));
                    ddl_art.Items.Insert(0, new ListItem("", "               "));
                    break;
            }
        }

        protected void ddl_articulo_Selection_Change(object sender, EventArgs e)
        {
            switch (ddl_tipo.SelectedValue.ToString())
            {
                case "IN":
                case "ST":
                case "CA":
                    llenar_centro_fab();
                    break;
                case "PT":
                    ddl_ctro.Items.Insert(0, new ListItem("", "0"));
                    ddl_art.Items.Insert(0, new ListItem("", "               "));
                    break;
            }
        }

        protected void datos_adicionales()
        {
            conexion = func.cadena_con(lbl_pais.Text);
            iDB2Connection con2 = new iDB2Connection(conexion);

            ejecutar_SQL(2);
            iDB2Command cmd2 = con2.CreateCommand();
            cmd2.CommandText = Select;

            DataSet ds2 = new DataSet();
            iDB2DataAdapter da2 = new iDB2DataAdapter("" + Select, con2);
            con2.Open();
            da2.Fill(ds2);

            String colname, und_med, peso_minimo, peso_maximo, peso_promedio, prod_teor, spacios="             ";
            colname = ds2.Tables[0].Columns[0].ColumnName;
            und_med = ds2.Tables[0].Rows[0][colname].ToString() + spacios ;
            ddl_und.SelectedValue = und_med;


            colname = ds2.Tables[0].Columns[1].ColumnName;
            peso_minimo = ds2.Tables[0].Rows[0][colname].ToString();
            txt_peso_min.Text = peso_minimo.ToString();

            colname = ds2.Tables[0].Columns[2].ColumnName;
            peso_maximo = ds2.Tables[0].Rows[0][colname].ToString();
            txt_peso_max.Text = peso_maximo.ToString();

            colname = ds2.Tables[0].Columns[3].ColumnName;
            peso_promedio = ds2.Tables[0].Rows[0][colname].ToString();
            txt_peso_prom.Text = peso_promedio.ToString();

            colname = ds2.Tables[0].Columns[4].ColumnName;
            prod_teor = ds2.Tables[0].Rows[0][colname].ToString(); 
            txt_prod_teor.Text = prod_teor.ToString();

            con2.Close();

        }

        protected void ddl_ctro_Selection_Change(object sender, EventArgs e)
        {

        }

        protected void txt_codigo_TextChanged(object sender, EventArgs e)
        {
            //Llenar nuevamente los DropDownLists
            llenar_und();
            
        }

        protected void Selection_Change(object sender, EventArgs e)
        {
            
        }
    }
}