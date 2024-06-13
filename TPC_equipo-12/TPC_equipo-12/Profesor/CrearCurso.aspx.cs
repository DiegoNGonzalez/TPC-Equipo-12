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

        protected void ButtonCrearCurso_Click(object sender, EventArgs e)
        {
            Profesor profesor = (Profesor)Session["profesor"];
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
                curso.Unidades = new List<Unidad>();
                cursoNegocio.CrearCurso(curso);
                profesor.Cursos.Add(curso);
                Session.Add("profesor", profesor);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Success", @"<script>
                        showMessage('Curso creado exitosamente!', 'success');
                        setTimeout(function() {
                        window.location.href = 'ProfesorCursos.aspx'; 
                        }, 2000); 
                        </script>", false);
                //Response.Redirect("ProfesorCursos.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("../Error.aspx");
                throw ex;
                //ScriptManager.RegisterStartupScript(this, typeof(Page), "Info", @"<script>
                //        showMessage('Verifique su información, El usuario ya esta registrado!', 'info');
                //        setTimeout(function() {
                //        window.location.href = 'LogIn.aspx'; 
                //        }, 4000); 
                //        </script>", false); falta implementar validacion y luego hacer funcionar este script
            }
        }
    }
}