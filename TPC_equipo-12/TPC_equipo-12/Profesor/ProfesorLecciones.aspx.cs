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
    public partial class ProfesorLecciones : System.Web.UI.Page
    {
        public List<Leccion> listaLecciones = new List<Leccion>();
        public LeccionNegocio leccionNegocio = new LeccionNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["profesor"] == null)
            {
                Session.Add("error", "Unicamente el profesor puede acceder a esta pestaña.");
                Response.Redirect("../Error.aspx");
            }
            if (!IsPostBack)
            {
                listaLecciones = leccionNegocio.ListarLecciones((int)Session["IDUnidadProfesor"]);
                Session.Add("ListaLeccionesProfesor", listaLecciones);
                rptLeccionesProf.DataSource = listaLecciones;
                rptLeccionesProf.DataBind();
            }
        }

        protected void ButtonBackUnidadProf_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProfesorUnidades.aspx");
        }

        protected void ButtonVerMaterialesProf_Command(object sender, CommandEventArgs e)
        {
            int IdLeccion = Convert.ToInt32(e.CommandArgument);
            Session.Add("IDLeccionProfesor", IdLeccion);
            Response.Redirect("ProfesorMateriales.aspx");
        }
    }
}