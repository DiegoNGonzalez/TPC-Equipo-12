using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace TPC_equipo_12
{
    public partial class ProfesorEstudiantesXCurso : System.Web.UI.Page
    {
        public List<InscripcionACurso> inscripciones = new List<InscripcionACurso>();
        public InscripcionNegocio inscripcionNegocio = new InscripcionNegocio(false);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ProfesorMasterPage master = (ProfesorMasterPage)Page.Master;
                master.VerificarMensaje();
                if (Session["profesor"] == null)
                {
                    Session["MensajeError"] = "No puede acceder a esa pestaña sin ser profesor.";
                    Response.Redirect("../LogIn.aspx");
                }
                inscripciones = inscripcionNegocio.listarInscripcionesXCurso((int)Session["IDCursoProfesor"]);
                if (inscripciones.Count == 0)
                {
                    lblNoInscripciones.Visible = true;
                    pnlTablaInscripciones.Visible = false;
                }
                else
                {
                    Session.Add("inscripciones", inscripciones);
                    rptInscripciones.DataSource = inscripciones;
                    rptInscripciones.DataBind();

                }
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProfesorUnidades.aspx");
        }

        protected void btnCancelarInscripcion_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int idInscripcion = Convert.ToInt32(btn.CommandArgument);
            InscripcionACurso aux = inscripcionNegocio.BuscarInscripcion(idInscripcion);
            inscripcionNegocio.RechazarInscripcion(aux.IDInscripcion, 'C');
            Usuario usuario = aux.Usuario;
            NotificacionNegocio notificacionNegocio = new NotificacionNegocio();
            int existeNotif = notificacionNegocio.buscarNotificacionXInscripcionXUsuario(aux.IDInscripcion, aux.Usuario.IDUsuario);
            if (existeNotif != 0)
            {
                notificacionNegocio.marcarComoNoLeidaYMensaje(existeNotif, "Inscripción cancelada x Profesor, contactelo o reinscribase");
                EstudianteNegocio estudianteNegocio = new EstudianteNegocio();

                estudianteNegocio.Desuscribirse(usuario.IDUsuario, aux.Curso.IDCurso);
                Session["MensajeInfo"] = "Inscripción cancelada.";
                Response.Redirect("ProfesorEstudiantesXCurso.aspx", false);
            }
            //inscripciones = inscripcionNegocio.listarInscripcionesXCurso((int)Session["IDCursoProfesor"]);
            //rptInscripciones.DataSource = inscripciones;
            //rptInscripciones.DataBind();

        }
    }
}