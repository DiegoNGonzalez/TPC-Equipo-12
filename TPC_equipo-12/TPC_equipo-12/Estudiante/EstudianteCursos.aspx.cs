using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;
using System.Configuration;

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
            Usuario usuarioensession = (Usuario)Session["usuario"];
            List<int> auxIds= cursoNegocio.IDCursosXEstudiante(usuarioensession.IDUsuario);
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
        protected void LinkButtonCurso_Command(object sender, CommandEventArgs e)
        {
            int idCurso = Convert.ToInt32(e.CommandArgument);
            Session.Add("IDCurso", idCurso);
            Response.Redirect("EstudianteUnidades.aspx");
        }

    }
}