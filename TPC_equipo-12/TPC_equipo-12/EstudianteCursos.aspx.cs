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
        public List<Curso> listaCursosInscriptos = new List<Curso>();
        public CursoNegocio cursoNegocio = new CursoNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                listaCursosInscriptos = cursoNegocio.EstaInscripto();
                Session.Add("listaCursos", listaCursosInscriptos);
                rptCursos.DataSource = listaCursosInscriptos;
                rptCursos.DataBind();
            }

        }
    }
}