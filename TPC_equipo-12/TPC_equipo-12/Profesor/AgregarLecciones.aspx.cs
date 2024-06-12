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
        LeccionNegocio leccionNegocio = new LeccionNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }

        }

        protected void ButtonCrearLeccion_Click(object sender, EventArgs e)
        {
            Profesor profesor = (Profesor)Session["profesor"];
            Curso curso = profesor.Cursos.Find(x => x.IDCurso == (int)Session["IDCursoProfesor"]);
            Unidad unidad = curso.Unidades.Find(x => x.IDUnidad == (int)Session["IDUnidadProfesor"]);
            try
            {
                Leccion leccion = new Leccion();
                leccion.Materiales = new List<MaterialLeccion>();
                leccion.Nombre = TextBoxNombreLeccion.Text;
                leccion.Descripcion = TextBoxDescripcionLeccion.Text;
                leccion.NroLeccion = int.Parse(TextBoxNumeroLeccion.Text);
                leccionNegocio.CrearLeccion(leccion, unidad.IDUnidad);
                unidad.Lecciones.Add(leccion);
                Session["MensajeExito"] = "Leccion creada con exito!";
                Response.Redirect("ProfesorLecciones.aspx", false);
            }
            catch (Exception ex)
            {
                Session["MensajeError"] = ex.ToString();
                Response.Redirect("ProfesorLecciones.aspx", false);
            }
        }

        
    }
}