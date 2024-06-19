using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_equipo_12
{
    public partial class EstudianteCursos : System.Web.UI.Page
    {

        public List<Curso> listaCursosInscriptos = new List<Curso>();
        protected void Page_Load(object sender, EventArgs e)
        {
            List<InscripcionACurso> listaInscripciones = new List<InscripcionACurso>();
            if (Session["estudiante"] == null)
            {
                Session["MensajeError"] = "No puede acceder a esa pestaña sin ser un estudiante.";
                Response.Redirect("../LogIn.aspx");
            }
            if (!IsPostBack)
            {
                EstudianteMasterPage master = (EstudianteMasterPage)Page.Master;
                master.VerificarMensaje();

                Estudiante estudiante = (Estudiante)Session["estudiante"];
                rptCursos.DataSource = estudiante.Cursos;
                rptCursos.DataBind();
            }

        }


        protected void LinkButtonCurso_Command(object sender, CommandEventArgs e)
        {
            int idCurso = Convert.ToInt32(e.CommandArgument);
            Session.Add("IDCurso", idCurso);
            Response.Redirect("EstudianteUnidades.aspx");
        }

        protected void ButtonDesuscribir_Command(object sender, CommandEventArgs e)
        {
            InscripcionNegocio inscripcionNegocio = new InscripcionNegocio();
            EstudianteNegocio estudianteNegocio = new EstudianteNegocio();
            Estudiante estudiante = (Estudiante)Session["estudiante"];
            int idCursoADesinscribir = Convert.ToInt32(e.CommandArgument);
            Curso cursoADesinscribir = estudiante.Cursos.Find(curso => curso.IDCurso == idCursoADesinscribir);
            try
            {
                inscripcionNegocio.EliminarInscripcion(estudiante.IDUsuario, idCursoADesinscribir);
                estudianteNegocio.Desuscribirse(estudiante.IDUsuario, idCursoADesinscribir);
                estudiante.Cursos.Remove(cursoADesinscribir);
                Session["estudiante"] = estudiante;
                Session["MensajeExito"] = "Desinscripcion exitosa!";
                Response.Redirect("EstudianteCursos.aspx", false);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", "<script>showMessage('Ocurrió un error al Desinscribirse.', 'error');</script>", false);
            }
        }
    }
}