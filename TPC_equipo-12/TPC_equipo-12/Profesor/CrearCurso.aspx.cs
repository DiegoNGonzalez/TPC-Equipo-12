using Dominio;
using Negocio;
using System;
using System.Collections.Generic;

namespace TPC_equipo_12
{
    public partial class CrearCurso : System.Web.UI.Page
    {
        public CursoNegocio cursoNegocio = new CursoNegocio();
        public string urlImagen { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["profesor"] == null)
            {
                Session["MensajeError"] = "No puede acceder a esa pestaña sin ser profesor.";
                Response.Redirect("../LogIn.aspx");
            }
            if (!IsPostBack)
            {
                cargarCategorias();
                ModificarCurso();

            }
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
                Curso curso = new Curso();
                curso.Nombre = TextBoxNombreCurso.Text;
                curso.Descripcion = TextBoxDescripcionCurso.Text;
                curso.Duracion = Convert.ToInt32(TextBoxDuracionCurso.Text);
                curso.Completo = false;
                curso.Estreno = Convert.ToDateTime(TextBoxEstrenoCurso.Text);
                curso.Categoria = new CategoriaCurso();
                curso.Categoria.IDCategoria = int.Parse(DropDownListCategoriaCurso.SelectedValue);
                curso.Categoria.Nombre = DropDownListCategoriaCurso.SelectedItem.Text;
                curso.Imagen = new Imagen();
                curso.Imagen.URL = TextBoxUrlImagen.Text;
                curso.Unidades = new List<Unidad>();

                if (Request.QueryString["idCurso"] != null)
                {
                    curso.IDCurso = Convert.ToInt32(Request.QueryString["idCurso"]);
                    cursoNegocio.ModificarCurso(curso);
                    Session["MensajeExito"] = "Curso modificado con exito!";
                    profesor.Cursos = cursoNegocio.ListarCursos();
                    profesor.Cursos = cursoNegocio.ValidarCursoCompleto(profesor.Cursos);
                    Session.Add("profesor", profesor);
                    Response.Redirect("ProfesorCursos.aspx", false);
                }
                else
                {
                    cursoNegocio.CrearCurso(curso, profesor.IDUsuario);
                    profesor.Cursos.Add(curso);
                    Session.Add("profesor", profesor);
                    Session["MensajeExito"] = "Curso creado con exito!";
                    Response.Redirect("ProfesorCursos.aspx", false);
                }

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

        protected void ModificarCurso()
        {
            if (Request.QueryString["idCurso"] != null)
            {
                LabelTitulo.Text = "Modificar Curso";
                ButtonCrearCurso.Text = "Modificar Curso";
                int idCurso = Convert.ToInt32(Request.QueryString["idCurso"]);
                Curso curso = cursoNegocio.ListarCursos().Find(x => x.IDCurso == idCurso);
                TextBoxNombreCurso.Text = curso.Nombre;
                TextBoxDescripcionCurso.Text = curso.Descripcion;
                TextBoxDuracionCurso.Text = curso.Duracion.ToString();
                TextBoxEstrenoCurso.Text = curso.Estreno.ToString("yyyy-MM-dd");
                DropDownListCategoriaCurso.SelectedValue = curso.Categoria.IDCategoria.ToString();
                TextBoxUrlImagen.Text = curso.Imagen.URL;
            }
        }

        protected void ButtonVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProfesorCursos.aspx", false);
        }
        private void cargarCategorias()
        {
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            try
            {
                List<CategoriaCurso> listaCategorias = categoriaNegocio.ListarCategorias();
                DropDownListCategoriaCurso.DataSource = listaCategorias;
                DropDownListCategoriaCurso.DataTextField = "Nombre";
                DropDownListCategoriaCurso.DataValueField = "IDCategoria";
                DropDownListCategoriaCurso.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}