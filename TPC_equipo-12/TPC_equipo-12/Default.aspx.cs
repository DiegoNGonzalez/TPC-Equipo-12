using Dominio;
using Negocio;
using System;
using System.Collections.Generic;


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
                Session.Add("listaCursos", listaCursos);
                rptCursos.DataSource = listaCursos;
                rptCursos.DataBind();

            }
        }
    }
}