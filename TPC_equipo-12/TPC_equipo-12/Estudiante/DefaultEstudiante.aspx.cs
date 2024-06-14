using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace TPC_equipo_12
{
    public partial class DefaultEstudiante : System.Web.UI.Page
    {
        public List<Curso> listaCursos = new List<Curso>();
        public CursoNegocio cursoNegocio = new CursoNegocio();
        public Estudiante EstudianteLogeado = new Estudiante();
        public List<Curso> listaCursosInscriptos = new List<Curso>();
        public InscripcionNegocio inscripcionNegocio = new InscripcionNegocio();
        public NotificacionNegocio notificacionNegocio = new NotificacionNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["estudiante"] == null)
            {
                Session["MensajeError"] = "No puede acceder a esa pestaña sin ser un estudiante.";
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

                listaCursos = cursoNegocio.ListarCursos();
                EstudianteLogeado = (Estudiante)Session["estudiante"];
                EstaInscripto();
                EstudianteLogeado.Cursos = listaCursosInscriptos;
                Session["estudiante"] = EstudianteLogeado;
                Session.Add("listaCursosInscriptos", listaCursosInscriptos);
                rptCursos.DataSource = listaCursos;
                rptCursos.DataBind();


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
                    int idInscripcion= inscripcionNegocio.UltimoIDInscripcion();
                    notificacionNegocio.AgregarNotificacionXInscripcion(idInscripcion);
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
    }
}