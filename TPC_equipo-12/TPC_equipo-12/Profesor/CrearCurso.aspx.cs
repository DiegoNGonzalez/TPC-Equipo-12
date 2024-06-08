using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_equipo_12
{
    public partial class CrearCurso : System.Web.UI.Page
    {
        public string urlImagen { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void TextBoxUrlImagen_TextChanged(object sender, EventArgs e)
        {
                urlImagenCurso.ImageUrl = TextBoxUrlImagen.Text;
        }

        protected void ButtonAgregarUnidades_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarUnidades.aspx");
        }
    }
}