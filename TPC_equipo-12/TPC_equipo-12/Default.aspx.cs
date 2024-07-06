using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace TPC_equipo_12
{
    public partial class Default : System.Web.UI.Page
    {
        public List<Curso> listaCursos = new List<Curso>();
        public CursoNegocio cursoNegocio = new CursoNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                listaCursos = cursoNegocio.ListarCursos();
                listaCursos = cursoNegocio.ValidarCursoCompleto(listaCursos);
                listaCursos = cursoNegocio.ValidarCursosActivos(listaCursos);
                rptCursos.DataSource = listaCursos;
                rptCursos.DataBind();

                
            }
        }

      

        protected void LinkButtonDefault_Command(object sender, CommandEventArgs e)
        {
            int idCurso = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("VerDetalleCurso.aspx?idCurso=" + idCurso);
            
        }
    }
}