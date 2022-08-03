using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;


namespace SFCP320C
{
    public class General
    {
        public static void bindDDL<T>(DropDownList control, List<T> repo, params object[] fields)
        {
            control.DataSource = repo;
            control.DataTextField = fields[0].ToString();
            control.DataValueField = fields[1].ToString();
            control.DataBind();

            control.Items.Insert(0, "-- Seleccione la Instalación --");
            control.Items[0].Value = "";
            control.SelectedIndex = 0;
            control.Items[0].Attributes.Add("disabled", "disabled");
        }

        public static void bindDDL<T>(DropDownList control, IEnumerable<T> repo, params object[] fields)
        {
            control.DataSource = repo.ToList();
            control.DataTextField = fields[0].ToString();
            control.DataValueField = fields[1].ToString();
            control.DataBind();

            control.Items.Insert(0, "****** - TODOS LOS CENTROS DE TRABAJO");
            control.Items[0].Value = "TODOS";
            control.SelectedIndex = 0;
            control.Items[0].Attributes.Add("disabled", "disabled");
        }
    }
}