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
    public partial class CrearCurso : System.Web.UI.Page
    {
        public string urlImagen { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void TextBoxUrlImagen_TextChanged(object sender, EventArgs e)
        {
                urlImagenCurso.ImageUrl = TextBoxUrlImagen.Text;
        }

        protected void ButtonCrearUnidad_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarUnidades.aspx");
        }

        protected void ButtonCrearLeccion_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarLecciones.aspx");
        }

        protected void ButtonCrearMaterial_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarMateriales.aspx");
        }

        protected void ButtonCrearCurso_Click(object sender, EventArgs e)
        {
            try 
            {
                CursoNegocio cursoNegocio = new CursoNegocio();
                Curso curso = new Curso();
                curso.Nombre = TextBoxNombreCurso.Text;
                curso.Descripcion = TextBoxDescripcionCurso.Text;
                curso.Duracion = Convert.ToInt32(TextBoxDuracionCurso.Text);
                curso.Estreno = Convert.ToDateTime(TextBoxEstrenoCurso.Text);
                curso.Imagen = new Imagen();
                curso.Imagen.URL = TextBoxUrlImagen.Text;
                cursoNegocio.CrearCurso(curso);
                //Me traigo al profesor de la session, le cargo el ultimo curso agregado y lo cargo de nuevo a la session.
                Profesor profesor = (Profesor)Session["profesor"];
                profesor.Cursos.Add(curso);
                Session.Add("profesor", profesor);
                Response.Redirect("ProfesorCursos.aspx", false);
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