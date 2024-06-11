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
    public partial class AgregarMateriales : System.Web.UI.Page
    {
        UnidadNegocio unidadNegocio = new UnidadNegocio();
        CursoNegocio cursoNegocio = new CursoNegocio();
        MaterialNegocio MaterialNegocio = new MaterialNegocio();
        LeccionNegocio LeccionNegocio = new LeccionNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDropDownCurso();
                CargarUnidadesYLeccionesDelPrimerCurso();
            }
        }

        private void CargarDropDownCurso()
        {
            Profesor profesor = (Profesor)Session["profesor"];
            if (profesor != null && profesor.Cursos != null && profesor.Cursos.Count > 0)
            {
                DropDownListCursos.DataSource = profesor.Cursos;
                DropDownListCursos.DataValueField = "IDCurso";
                DropDownListCursos.DataTextField = "Nombre";
                DropDownListCursos.DataBind();

                DropDownListCursos.SelectedIndex = 0;
            }
            else
            {
                DropDownListCursos.Items.Clear();
                DropDownListCursos.Items.Add(new ListItem("No hay cursos disponibles", ""));
            }
        }

        private void CargarUnidadesYLeccionesDelPrimerCurso()
        {
            if (DropDownListCursos.Items.Count > 0)
            {
                int idPrimerCurso = int.Parse(DropDownListCursos.Items[0].Value);
                CargarUnidades(idPrimerCurso);
            }
        }

        private void CargarUnidades(int idCurso)
        {
            List<Unidad> unidades = unidadNegocio.ListarUnidades(idCurso);
            if (unidades != null && unidades.Count > 0)
            {
                DropDownListUnidades.DataSource = unidades;
                DropDownListUnidades.DataValueField = "IDUnidad";
                DropDownListUnidades.DataTextField = "Nombre";
                DropDownListUnidades.DataBind();

                DropDownListUnidades.SelectedIndex = 0;

                int idUnidadSeleccionada = int.Parse(DropDownListUnidades.SelectedValue);
                CargarLecciones(idUnidadSeleccionada);
            }
            else
            {
                DropDownListUnidades.Items.Clear();
                DropDownListUnidades.Items.Add(new ListItem("No hay unidades disponibles", ""));

                DropDownListLecciones.Items.Clear();
                DropDownListLecciones.Items.Add(new ListItem("No hay lecciones disponibles", ""));
            }
        }

        private void CargarLecciones(int idUnidad)
        {
            if (DropDownListUnidades.Items[0].Text == "No hay unidades disponibles")
            {
                DropDownListLecciones.Items.Clear();
                DropDownListLecciones.Items.Add(new ListItem("No hay unidades seleccionadas", ""));
            }
            else
            {
                List<Leccion> lecciones = LeccionNegocio.ListarLecciones(idUnidad);
                if (lecciones != null && lecciones.Count > 0)
                {
                    DropDownListLecciones.DataSource = lecciones;
                    DropDownListLecciones.DataValueField = "IDLeccion";
                    DropDownListLecciones.DataTextField = "Nombre";
                    DropDownListLecciones.DataBind();
                }
                else
                {
                    DropDownListLecciones.Items.Clear();
                    DropDownListLecciones.Items.Add(new ListItem("No hay lecciones disponibles para esta unidad", ""));
                }
            }
        }

        protected void DropDownListCursos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idCursoSeleccionado = int.Parse(DropDownListCursos.SelectedValue);
            CargarUnidades(idCursoSeleccionado);
        }
        protected void DropDownListUnidades_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idUnidadSeleccionada = int.Parse(DropDownListUnidades.SelectedValue);
            CargarLecciones(idUnidadSeleccionada);
        }
        protected void ButtonCrearMaterial_Click(object sender, EventArgs e)
        {
            Profesor profesor = (Profesor)Session["profesor"];
            Curso curso = profesor.Cursos.Find(x => x.IDCurso == Convert.ToInt32(DropDownListCursos.SelectedValue));
            Unidad unidad = curso.Unidades.Find(x => x.IDUnidad == Convert.ToInt32(DropDownListUnidades.SelectedValue));
            Leccion leccion = unidad.Lecciones.Find(x => x.IDLeccion == Convert.ToInt32(DropDownListLecciones.SelectedValue));
            try
            {
                MaterialLeccion material = new MaterialLeccion();
                material.Nombre = TextBoxNombreMaterial.Text;
                material.Descripcion = TextBoxDescripcionMaterial.Text;
                material.TipoMaterial = DropDownListTipoMaterial.SelectedValue;
                material.URL = TextBoxURLMaterial.Text;
                MaterialNegocio.CrearMaterial(material, leccion.IDLeccion);
                leccion.Materiales.Add(material);
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