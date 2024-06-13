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
                ModificarUnidad();
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
                if (Request.QueryString["idUnidad"] != null)
                {
                    unidad.IDUnidad = Convert.ToInt32(Request.QueryString["idUnidad"]);
                    unidadNegocio.ModificarUnidad(unidad);
                    Session["MensajeExito"] = "Unidad modificada con exito!";
                    Response.Redirect("ProfesorUnidades.aspx", false);
                }
                else
                {
                    curso.Unidades.Add(unidad);
                    unidadNegocio.CrearUnidad(unidad, curso.IDCurso);
                    Session["MensajeExito"] = "Unidad creada con exito!";
                    Response.Redirect("ProfesorUnidades.aspx", false);
                }
            }
            catch (Exception ex)
            {
                Session["MensajeError"] = ex.ToString();
                Response.Redirect("ProfesorUnidades.aspx", false);
            }
        }

        protected void ButtonVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProfesorUnidades.aspx", false);
        }

        protected void ModificarUnidad()
        {
            if (Request.QueryString["IdUnidad"] != null)
            {
                LabelAgregarUnidad.Text = "Modificar Unidad";
                ButtonCrearUnidades.Text = "Modificar Unidad";
                int idUnidad = Convert.ToInt32(Request.QueryString["IdUnidad"]);
                Unidad unidad = unidadNegocio.ListarUnidades((int)Session["IDCursoProfesor"]).Find(x => x.IDUnidad == idUnidad);
                TextBoxNombreUnidad.Text = unidad.Nombre;
                TextBoxDescripcionUnidad.Text = unidad.Descripcion;
                TextBoxNumeroUnidad.Text = unidad.NroUnidad.ToString();
                TextBoxNumeroUnidad.Enabled = false;
                unidad.IDUnidad = idUnidad;
            }
        }
    }
}