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
    public partial class ProfesorUnidades : System.Web.UI.Page
    {
        public List<Unidad> listaUnidades = new List<Unidad>();
        public UnidadNegocio unidadNegocio = new UnidadNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["profesor"] == null)
            {
                Session.Add("error", "Unicamente el profesor puede acceder a esta pestaña.");
                Response.Redirect("../Error.aspx");
            }
            if (!IsPostBack)
            {
                listaUnidades = unidadNegocio.ListarUnidades((int)Session["IDCursoProfesor"]);
                Session.Add("ListaUnidadesProfesor", listaUnidades);
                rptUnidadesProf.DataSource = listaUnidades;
                rptUnidadesProf.DataBind();
            }

        }

        protected void ButtonVerLeccionesProf_Command(object sender, CommandEventArgs e)
        {
            int IdUnidad = Convert.ToInt32(e.CommandArgument);
            Session.Add("IDUnidadProfesor", IdUnidad);
            Response.Redirect("ProfesorLecciones.aspx");
        }

        protected void ButtonBackCursosProf_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProfesorCursos.aspx");
        }

        protected void ButtonCrearUnidadProf_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarUnidades.aspx");
        }

        protected void ButtonEliminarUnidadProf_Click(object sender, EventArgs e)
        {
            Response.Redirect("EliminarUnidad.aspx");
        }
    }
}