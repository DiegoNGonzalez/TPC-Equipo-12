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
    public partial class EstudianteCursos : System.Web.UI.Page
    {
        public List<Curso> listaCursos = new List<Curso>();
        public CursoNegocio cursoNegocio = new CursoNegocio();
        public Estudiante EstudianteLogeado = new Estudiante();
        public List<Curso> listaCursosInscriptos = new List<Curso>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                listaCursos = cursoNegocio.ListarCursos();
                EstaInscripto();
                Session.Add("listaCursosInscriptos", listaCursosInscriptos);
                rptCursos.DataSource = listaCursosInscriptos;
                rptCursos.DataBind();
            }

        }

        protected void EstaInscripto()
        {
            List<int> auxIds= cursoNegocio.IDCursosXEstudiante(1);
            foreach(int id in auxIds)
            {
                foreach(Curso curso in listaCursos)
                {
                    if(curso.IDCurso==id)
                    {
                        listaCursosInscriptos.Add(curso);
                    }
                }
            }


        }
    }
}