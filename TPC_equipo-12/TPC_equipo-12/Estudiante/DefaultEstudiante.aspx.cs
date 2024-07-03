using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_equipo_12
{
    public partial class DefaultEstudiante : System.Web.UI.Page
    {
        public List<Curso> listaCursos = new List<Curso>();
        public CursoNegocio cursoNegocio = new CursoNegocio();
        public Estudiante EstudianteLogeado = new Estudiante();
        public List<Curso> listaCursosInscriptos = new List<Curso>();
        public InscripcionNegocio inscripcionNegocio = new InscripcionNegocio(false);
        public NotificacionNegocio notificacionNegocio = new NotificacionNegocio();
        public List<Curso> cursosNoInscriptos = new List<Curso>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["estudiante"] == null)
            {
                Session["MensajeError"] = "No puede acceder a esa pestaña sin ser un estudiante.";
                Response.Redirect("../LogIn.aspx");
            }
            if (!IsPostBack)
            {
                EstudianteMasterPage master = (EstudianteMasterPage)Page.Master;
                master.VerificarMensaje();
                listaCursos = cursoNegocio.ListarCursos();
                listaCursos = cursoNegocio.ValidarCursoCompleto(listaCursos);
                listaCursos = cursoNegocio.ValidarCursosActivos(listaCursos);
                EstudianteLogeado = (Estudiante)Session["estudiante"];
                EstaInscripto();
                EstudianteLogeado.Cursos = listaCursosInscriptos;
                Session["estudiante"] = EstudianteLogeado;
                Session.Add("listaCursosInscriptos", listaCursosInscriptos);
                NoEstaInscripto();
                rptCursos.DataSource = cursosNoInscriptos;
                rptCursos.DataBind();
                MostrarCategoria();

            }
        }
        protected void EstaInscripto()
        {
            Estudiante EstudianteEnSession = (Estudiante)Session["estudiante"];
            List<int> auxIds = cursoNegocio.IDCursosXEstudiante(EstudianteEnSession.IDUsuario);
            foreach (int id in auxIds)
            {
                foreach (Curso curso in listaCursos)
                {
                    if (curso.IDCurso == id)
                    {
                        listaCursosInscriptos.Add(curso);
                    }
                }
            }


        }
        protected void NoEstaInscripto()
        {
            Estudiante EstudianteEnSession = (Estudiante)Session["estudiante"];
            List<int> auxIds = cursoNegocio.IDCursosXEstudiante(EstudianteEnSession.IDUsuario);
            foreach (Curso curso in listaCursos)
            {
                if (!auxIds.Contains(curso.IDCurso))
                {
                    cursosNoInscriptos.Add(curso);
                }
            }
        }

        protected void btnInscribirse_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Control lblControl = button.NamingContainer.FindControl("lblIDCurso");
            Label label = (Label)lblControl;
            int idCurso = Convert.ToInt32(label.Text);
            Curso aux = cursoNegocio.BuscarCurso(idCurso);
            try
            {
                bool seInscribio = inscripcionNegocio.Incripcion((Usuario)Session["estudiante"], aux);
                if (seInscribio)
                {
                    int idInscripcion = inscripcionNegocio.UltimoIDInscripcion();
                    notificacionNegocio.AgregarNotificacionXInscripcion(idInscripcion,idCurso);
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Success", "<script>showMessage('La inscripción se envió correctamente!', 'success');</script>", false);

                }

            }
            catch (Exception ex)
            {

                //ex.Message.ToString();
                //Session.Add("error", "No se pudo inscribir al curso, ya estas inscripto u ocurrio un error");
                //Response.Redirect("../Error.aspx");
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", "<script>showMessage('Ocurrió un error al enviar la inscripción.', 'error');</script>", false);


            }
        }
        private void MostrarCategoria()
        {
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            foreach (RepeaterItem item in rptCursos.Items)
            {
                HiddenField hiddenFieldIDCurso = (HiddenField)item.FindControl("HiddenFieldIDCurso");
                Label lblCategoria = (Label)item.FindControl("LabelCategoriaCurso");

                if (hiddenFieldIDCurso != null && lblCategoria != null)
                {
                    int idCurso = int.Parse(hiddenFieldIDCurso.Value);
                    if (categoriaNegocio.CategoriaNombreXIDCurso(idCurso) != "")
                    {
                        lblCategoria.Text = categoriaNegocio.CategoriaNombreXIDCurso(idCurso);
                    }
                    else
                    {
                        lblCategoria.Text = "Sin categoria";
                    }
                }
            }
        }

        protected void LinkButtonEstudiante_Command(object sender, CommandEventArgs e)
        {
            int idCurso = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("EstudianteVerDetalleCurso.aspx?idCurso=" + idCurso);

        }
    }
}