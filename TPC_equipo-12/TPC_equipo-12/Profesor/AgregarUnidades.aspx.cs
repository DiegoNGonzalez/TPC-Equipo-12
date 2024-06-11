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

            }

        }

        protected void ButtonCrearUnidades_Click(object sender, EventArgs e)
        {
            Profesor profesor = (Profesor)Session["profesor"];
            Curso curso = profesor.Cursos.Find(x => x.IDCurso == (int)Session["IDCursoProfesor"]);

            try
            {
                Unidad unidad = new Unidad();
                unidad.Lecciones = new List<Leccion>();
                unidad.Nombre = TextBoxNombreUnidad.Text;
                unidad.Descripcion = TextBoxDescripcionUnidad.Text;
                unidad.NroUnidad = int.Parse(TextBoxNumeroUnidad.Text);
                curso.Unidades.Add(unidad);
                unidadNegocio.CrearUnidad(unidad, curso.IDCurso);
                Response.Redirect("ProfesorUnidades.aspx", false);
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