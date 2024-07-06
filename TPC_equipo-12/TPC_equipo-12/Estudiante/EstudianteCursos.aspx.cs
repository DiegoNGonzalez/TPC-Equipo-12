using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
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

        protected void ButtonDesinscribirse_Command(object sender, CommandEventArgs e)
        {
            InscripcionNegocio inscripcionNegocio = new InscripcionNegocio(false);
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
       

        protected void VerDetalleCurso_Command(object sender, CommandEventArgs e)
        {
            bool home = false;
            int idCurso = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("EstudianteVerDetalleCurso.aspx?idCurso=" + idCurso+"&home="+home);
        }

        protected void btnLimpiarFiltro_Click(object sender, EventArgs e)
        {

        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {

        }

        protected void chkFiltrar_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {

        }
    }
}