using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
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
                Session.Add("listaCursos", listaCursos);
                rptCursos.DataSource = listaCursos;
                rptCursos.DataBind();

                MostrarCategoria();
            }
        }

        private void MostrarCategoria()
        {
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            foreach (RepeaterItem item in rptCursos.Items)
            {
                HiddenField hiddenFieldIDCurso = (HiddenField)item.FindControl("HiddenFieldIDCurso");
                Label lblCategoria = (Label)item.FindControl("LabelCategoriaCurso");

                if (hiddenFieldIDCurso != null && lblCategoria != null)
                {

                    int idCurso = int.Parse(hiddenFieldIDCurso.Value);
                    if (categoriaNegocio.CategoriaNombreXIDCurso(idCurso) != "")
                    {
                        lblCategoria.Text = categoriaNegocio.CategoriaNombreXIDCurso(idCurso);
                    }
                    else
                    {
                        lblCategoria.Text = "Sin categoria";
                    }
                }
            }
        }
    }
}