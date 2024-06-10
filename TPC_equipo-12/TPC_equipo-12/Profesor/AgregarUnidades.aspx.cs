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
    public partial class AgregarUnidades : System.Web.UI.Page
    {
        CursoNegocio cursoNegocio = new CursoNegocio();
        UnidadNegocio unidadNegocio = new UnidadNegocio();
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

        protected void ButtonCrearUnidades_Click(object sender, EventArgs e)
        {
            Profesor profesor = (Profesor)Session["profesor"];
            Curso curso = profesor.Cursos.Find(x => x.IDCurso == Convert.ToInt32(DropDownListCursos.SelectedValue));

            try
            {
                Unidad unidad = new Unidad();
                unidad.Nombre = TextBoxNombreUnidad.Text;
                unidad.Descripcion = TextBoxDescripcionUnidad.Text;
                unidad.NroUnidad = int.Parse(TextBoxNumeroUnidad.Text);
                curso.Unidades.Add(unidad);
                unidadNegocio.CrearUnidad(unidad, curso.IDCurso);
                Response.Redirect("CrearCurso.aspx", false);
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