using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_equipo_12
{
    public partial class EstudianteVerDetalleCurso : System.Web.UI.Page
    {
        public Curso curso = new Curso();
        public CursoNegocio cursoNegocio = new CursoNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["estudiante"] == null)
            {
                Session["MensajeError"] = "No puede acceder a esa pestaña sin ser un estudiante.";
                Response.Redirect("../LogIn.aspx");
            }
            if (!IsPostBack)
            {
                int idCurso = Convert.ToInt32(Request.QueryString["idCurso"]);
                curso = cursoNegocio.BuscarCurso(idCurso);
                List<Curso> cursos = new List<Curso>();
                cursos.Add(curso);
                RepeaterVerDetalleCurso.DataSource = cursos;
                RepeaterVerDetalleCurso.DataBind();

            }
        }
        protected void RepeaterVerDetalleCurso_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater Repeaterunidades = (Repeater)e.Item.FindControl("RepeaterUnidades");
                Label NoHayUnidades = (Label)e.Item.FindControl("LabelNoHayUnidades");
                if (curso.Unidades.Count > 0)
                {
                    Repeaterunidades.DataSource = curso.Unidades;
                    Repeaterunidades.DataBind();
                }
                else
                {
                    NoHayUnidades.Visible = true;
                    NoHayUnidades.Text = "No hay unidades en este curso.";
                }
            }
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater RepeaterResenias = (Repeater)e.Item.FindControl("RepeaterResenias");
                Label NohayResenias = (Label)e.Item.FindControl("LabelNoHayResenias");
                if (curso.Resenias.Count > 0)
                {
                    RepeaterResenias.DataSource = curso.Resenias;
                    RepeaterResenias.DataBind();
                }
                else
                {
                    NohayResenias.Visible = true;
                    NohayResenias.Text = "No hay reseñas de este curso.";
                }
            }
        }

        protected void volverCursos_Click(object sender, EventArgs e)
        {
            bool home = Convert.ToBoolean(Request.QueryString["home"]);
            if (home)
            {
                
                Response.Redirect("DefaultEstudiante.aspx", false);
            }
            else
            {
                Response.Redirect("EstudianteCursos.aspx", false);

            }
        }
    }
}