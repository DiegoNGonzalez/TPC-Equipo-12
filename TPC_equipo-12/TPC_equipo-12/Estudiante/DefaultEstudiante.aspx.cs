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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["estudiante"] == null)
            {
                Session.Add("error", "Unicamente los estudiantes pueden acceder a esta pestaña.");
                Response.Redirect("../Error.aspx");

            }
            if (!IsPostBack)
            {
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

                }
                else
                {
                    throw new Exception("No se pudo inscribir al curso");
                }
            }
            catch (Exception ex)
            {

                ex.Message.ToString();
                Session.Add("error", "No se pudo inscribir al curso, ya estas inscripto u ocurrio un error");
                Response.Redirect("../Error.aspx");


            }
            
            
        }
    }
}