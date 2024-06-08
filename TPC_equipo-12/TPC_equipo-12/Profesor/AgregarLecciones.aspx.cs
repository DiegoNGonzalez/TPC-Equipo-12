using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_equipo_12
{
    public partial class AgregarLecciones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonAgregarMateriales_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarMateriales.aspx");
        }

        protected void ButtonHechoLecciones_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarUnidades.aspx");
        }
    }
}