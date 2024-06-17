using Dominio;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace TPC_equipo_12
{
    public partial class EstudianteCursos : System.Web.UI.Page
    {

        public List<Curso> listaCursosInscriptos = new List<Curso>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["estudiante"] == null)
            {
                Session["MensajeError"] = "No puede acceder a esa pestaña sin ser un estudiante.";
                Response.Redirect("../LogIn.aspx");
            }
            if (!IsPostBack)
            {
                listaCursosInscriptos = (List<Curso>)Session["listaCursosInscriptos"];
                rptCursos.DataSource = listaCursosInscriptos;
                rptCursos.DataBind();
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