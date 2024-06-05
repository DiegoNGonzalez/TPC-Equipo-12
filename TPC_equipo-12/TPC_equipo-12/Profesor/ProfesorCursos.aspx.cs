using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;

namespace TPC_equipo_12.Profesor
{
    public partial class ProfesorCursos : System.Web.UI.Page
    {
        public List<Curso> listaCursos = new List<Curso>();
        public CursoNegocio cursoNegocio = new CursoNegocio();
        public Profesor profesor = new Profesor();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                profesor = (Profesor)Session["profesor"];
                Session.Add("listaCursosProfesor", profesor.Cursos);
                rptProfesorCursos.DataSource = profesor.Cursos;
                rptProfesorCursos.DataBind();
            }
        }
    }
}