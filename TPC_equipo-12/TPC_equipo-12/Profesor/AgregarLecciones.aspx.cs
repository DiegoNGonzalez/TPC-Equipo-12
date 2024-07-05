using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Collections.Specialized.BitVector32;

namespace TPC_equipo_12
{
    public partial class AgregarLecciones : System.Web.UI.Page
    {
        UnidadNegocio unidadNegocio = new UnidadNegocio();
        CursoNegocio cursoNegocio = new CursoNegocio();
        LeccionNegocio leccionNegocio = new LeccionNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["profesor"] == null)
            {
                Session["MensajeError"] = "No puede acceder a esa pestaña sin ser profesor.";
                Response.Redirect("../LogIn.aspx");
            }

            if (!IsPostBack)
            {
                ModificarLeccion();
            }

        }

        protected void ButtonCrearLeccion_Click(object sender, EventArgs e)
        {
            Curso curso = cursoNegocio.BuscarCurso((int)Session["IDCursoProfesor"]);
            Unidad unidad = curso.Unidades.Find(x => x.IDUnidad == (int)Session["IDUnidadProfesor"]);
            if  (!ValidarFormulario())
            {
                return;
            }
            try
            {
                Leccion leccion = new Leccion();
                leccion.Materiales = new List<MaterialLeccion>();
                leccion.Nombre = TextBoxNombreLeccion.Text;
                leccion.Descripcion = TextBoxDescripcionLeccion.Text;
                leccion.NroLeccion = int.Parse(TextBoxNumeroLeccion.Text);
                if (Request.QueryString["idLeccion"] != null)
                {
                    leccion.IDLeccion = Convert.ToInt32(Request.QueryString["idLeccion"]);
                    leccionNegocio.ModificarLeccion(leccion);
                    Session["MensajeExito"] = "Leccion modificada con exito!";
                    Response.Redirect("ProfesorLecciones.aspx", false);
                }
                else
                {
                    if(unidad.Lecciones == null)
                    {
                        unidad.Lecciones = new List<Leccion>();
                    }
                    unidad.Lecciones.Add(leccion);
                    leccionNegocio.CrearLeccion(leccion, unidad.IDUnidad);
                    Session["MensajeExito"] = "Leccion creada con exito!";
                    Response.Redirect("ProfesorLecciones.aspx", false);
                }
            }
            catch (Exception ex)
            {
                Session["MensajeError"] = ex.ToString();
                Response.Redirect("ProfesorLecciones.aspx", false);
            }
        }

        protected void ModificarLeccion()
        {
            Leccion ultimaLeccion = new Leccion();
            Curso curso = cursoNegocio.BuscarCurso((int)Session["IDCursoProfesor"]);
            Unidad unidad = curso.Unidades.Find(x => x.IDUnidad == (int)Session["IDUnidadProfesor"]);
            ultimaLeccion = leccionNegocio.ListarLecciones(unidad.IDUnidad).Last();
            if (Request.QueryString["idLeccion"] != null)
            {
                LabelNombreLeccion.Text = "Modificar Leccion";
                ButtonCrearLeccion.Text = "Modificar Leccion";
                int idLeccion = Convert.ToInt32(Request.QueryString["idLeccion"]);
                Leccion leccion = leccionNegocio.ListarLecciones((int)Session["IDUnidadProfesor"]).Find(x => x.IDLeccion == idLeccion);
                TextBoxNombreLeccion.Text = leccion.Nombre;
                TextBoxDescripcionLeccion.Text = leccion.Descripcion;
                TextBoxNumeroLeccion.Text = leccion.NroLeccion.ToString();
                TextBoxNumeroLeccion.Enabled = false;
                leccion.IDLeccion = idLeccion;
            }
            else
            {
                TextBoxNumeroLeccion.Text = (ultimaLeccion.NroLeccion+1).ToString();
                TextBoxNumeroLeccion.Enabled = false;
            }
        }
        protected void ButtonVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProfesorLecciones.aspx", false);
        }
        protected bool ValidarFormulario()
        {
            if (TextBoxNombreLeccion.Text == "" || TextBoxDescripcionLeccion.Text == "" || TextBoxNumeroLeccion.Text == "" )
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "info", "<script>showMessage('Faltan campos por completar!', 'info');</script>", false);
                return false;
            }

            return true;
        }
    }
}