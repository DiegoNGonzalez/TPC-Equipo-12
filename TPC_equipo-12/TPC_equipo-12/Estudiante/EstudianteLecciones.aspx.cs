using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;

namespace TPC_equipo_12
{
    public partial class EstudianteLecciones : System.Web.UI.Page
    {
        public List<Leccion> listaLecciones = new List<Leccion>();
        public LeccionNegocio leccionNegocio = new LeccionNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                listaLecciones = leccionNegocio.ListarLecciones((int)Session["IDUnidad"]);
                Session.Add("ListaLecciones", listaLecciones);
                rptLecciones.DataSource = listaLecciones;
                rptLecciones.DataBind();
            }

        }
    }
}