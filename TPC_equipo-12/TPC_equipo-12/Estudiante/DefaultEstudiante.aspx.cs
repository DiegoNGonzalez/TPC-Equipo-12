using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
                EstudianteLogeado.Cursos=listaCursosInscriptos;
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

        }
    }
}