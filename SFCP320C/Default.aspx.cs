using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SFCP320C
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void img_salir_Click(object sender, ImageClickEventArgs e)
        {
            Response.Write(" <script> window.open('','_parent',''); window.close(); </script>");
        }

        protected void Selection_Change(object sender, EventArgs e)
        {
            ddl_cia_Selection_Change(null, null);
        }

        protected void ddl_cia_Selection_Change(object sender, EventArgs e)
        {
            Session["nbPais"] = ddl_pais.SelectedItem.ToString();
            Session["pais"] = ddl_pais.SelectedValue.ToString();
            Response.Redirect("SFCP320C.aspx");
        }
    }
}
