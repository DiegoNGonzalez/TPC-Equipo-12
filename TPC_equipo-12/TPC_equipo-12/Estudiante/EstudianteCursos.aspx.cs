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
        
        public List<Curso> listaCursosInscriptos = new List<Curso>();
        protected void Page_Load(object sender, EventArgs e)
        {
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