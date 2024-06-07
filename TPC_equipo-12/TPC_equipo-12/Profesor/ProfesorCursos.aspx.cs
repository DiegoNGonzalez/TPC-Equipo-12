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

        protected void LinkButtonCursoProf_Command(object sender, CommandEventArgs e)
        {
            int idCurso = Convert.ToInt32(e.CommandArgument);
            Session.Add("IDCursoProfesor", idCurso);
            Response.Redirect("ProfesorUnidades.aspx");
        }
    }
}