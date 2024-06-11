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
            }
        }

        protected void ButtonCrearMaterial_Click(object sender, EventArgs e)
        {
            Profesor profesor = (Profesor)Session["profesor"];
            Curso curso = profesor.Cursos.Find(x => x.IDCurso == (int)Session["IDCursoProfesor"]);
            Unidad unidad = curso.Unidades.Find(x => x.IDUnidad == (int)Session["IDUnidadProfesor"]);
            Leccion leccion = unidad.Lecciones.Find(x => x.IDLeccion == (int)Session["IDLeccionProfesor"]);
            try
            {
                MaterialLeccion material = new MaterialLeccion();
                material.Nombre = TextBoxNombreMaterial.Text;
                material.Descripcion = TextBoxDescripcionMaterial.Text;
                material.TipoMaterial = DropDownListTipoMaterial.SelectedValue;
                material.URL = TextBoxURLMaterial.Text;
                MaterialNegocio.CrearMaterial(material, leccion.IDLeccion);
                leccion.Materiales.Add(material);
                Response.Redirect("ProfesorMateriales.aspx", false);
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