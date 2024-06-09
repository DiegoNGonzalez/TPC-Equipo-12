using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace TPC_equipo_12
{
    public partial class AgregarLecciones : System.Web.UI.Page
    {
        UnidadNegocio unidadNegocio = new UnidadNegocio();
        CursoNegocio cursoNegocio = new CursoNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDropDownCurso();
            }

        }

        public void CargarDropDownCurso()
        {
            Profesor profesor = (Profesor)Session["profesor"];
            if (profesor != null && profesor.Cursos != null && profesor.Cursos.Count > 0)
            {
                DropDownListCursos.DataSource = profesor.Cursos;
                DropDownListCursos.DataValueField = "IDCurso";
                DropDownListCursos.DataTextField = "Nombre";
                DropDownListCursos.DataBind();
            }
            else
            {
                DropDownListCursos.Items.Clear();
                DropDownListCursos.Items.Add(new ListItem("No hay cursos disponibles", ""));
            }
        }

        protected void ButtonAgregarMateriales_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarMateriales.aspx");
        }

        protected void ButtonHechoLecciones_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarUnidades.aspx");
        }

        protected void DropDownListCursos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Profesor profesor = (Profesor)Session["profesor"];
                int idcurso = int.Parse(DropDownListCursos.SelectedItem.Value);
                Curso curso = profesor.Cursos.Find(x => x.IDCurso == idcurso);
                if (curso != null)
                {
                    if (curso.Unidades != null && curso.Unidades.Count > 0)
                    {
                        DropDownListUnidades.DataSource = unidadNegocio.ListarUnidades(int.Parse(DropDownListCursos.SelectedItem.Value));
                        DropDownListUnidades.DataValueField = "IDUnidad";
                        DropDownListUnidades.DataTextField = "Nombre";
                        DropDownListUnidades.DataBind();
                    }
                    else
                    {
                        DropDownListUnidades.Items.Clear();
                        DropDownListUnidades.Items.Add(new ListItem("No hay unidades disponibles", ""));
                    }

                } else
                {
                    DropDownListUnidades.Items.Clear();
                    DropDownListUnidades.Items.Add(new ListItem("Curso no encontrado", ""));
                }
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("../Error.aspx");
                throw ex;
            }
        }
    }
}