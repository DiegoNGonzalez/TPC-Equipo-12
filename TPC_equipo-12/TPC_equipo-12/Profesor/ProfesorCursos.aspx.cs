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
            if (Session["profesor"] == null)
            {
                Session.Add("error", "Unicamente el profesor puede acceder a esta pestaña.");
                Response.Redirect("../Error.aspx");
            }
            if (!IsPostBack)
            {
                profesor = (Profesor)Session["profesor"];
                Session.Add("listaCursosProfesor", profesor.Cursos);
                UpdatePanelCursos.Update();
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

        protected void ButtonEliminarCurso_Command(object sender, CommandEventArgs e)
        {
            Profesor profesor = (Profesor)Session["profesor"];
            int idCursoAEliminar = Convert.ToInt32(e.CommandArgument);
            Curso cursoAEliminar = profesor.Cursos.Find(curso => curso.IDCurso == idCursoAEliminar);

            if (cursoAEliminar != null)
            {
                profesor.Cursos.Remove(cursoAEliminar);
                Session["profesor"] = profesor;
                cursoNegocio.EliminarCurso(idCursoAEliminar);
                Response.Redirect("ProfesorCursos.aspx", false);
            }
            else
            {
                Session.Add("Error", "El curso no se encontró en la lista de cursos del profesor.");
                Response.Redirect("../Error.aspx");
            }

        }
    }
}