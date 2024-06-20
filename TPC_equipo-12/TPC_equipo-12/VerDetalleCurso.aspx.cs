using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace TPC_equipo_12
{
    public partial class VerDetalleCurso : System.Web.UI.Page
    {
        public Curso curso = new Curso();
        public CursoNegocio cursoNegocio = new CursoNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int idCurso = Convert.ToInt32(Request.QueryString["idCurso"]);
                curso = cursoNegocio.BuscarCurso(idCurso);
                List <Curso> cursos = new List<Curso>();
                cursos.Add(curso);
                RepeaterVerDetalleCurso.DataSource = cursos;
                RepeaterVerDetalleCurso.DataBind();

            }
        }

        protected void RepeaterVerDetalleCurso_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Curso curso1 = new Curso();
                curso1 = (Curso)e.Item.DataItem;
                Repeater Repeaterunidades = (Repeater)e.Item.FindControl("RepeaterUnidades");
                Repeaterunidades.DataSource = curso.Unidades;
                Repeaterunidades.DataBind();
            }
        }
    }
}