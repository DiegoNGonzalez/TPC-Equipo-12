using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

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
                Session["MensajeError"] = "No puede acceder a esa pestaña sin ser profesor.";
                Response.Redirect("../LogIn.aspx");
            }
            if (!IsPostBack)
            {
                if (Session["MensajeExito"] != null)
                {
                    string msj = Session["MensajeExito"].ToString();
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Success", $@"showMessage('{msj}', 'success');", true);
                    Session["MensajeExito"] = null;
                }
                if (Session["MensajeError"] != null)
                {
                    string msj = Session["MensajeError"].ToString();
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $@"showMessage('{msj}', 'error');", true);
                    Session["MensajeError"] = null;
                }
                if (Session["MensajeInfo"] != null)
                {
                    string msj = Session["MensajeInfo"].ToString();
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Info", $@"showMessage('{msj}', 'info');", true);
                    Session["MensajeInfo"] = null;
                }

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


        protected void btnEstudiantesXCurso_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProfesorEstudiantesXCurso.aspx");
        }

        protected void ButtonModificarUnidadProf_Command(object sender, CommandEventArgs e)
        {
            int IdUnidad = Convert.ToInt32(e.CommandArgument);
            Session.Add("IDUnidadProfesor", IdUnidad);
            Response.Redirect("AgregarUnidades.aspx?IdUnidad=" + IdUnidad);

        }

    }
}