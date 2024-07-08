using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace TPC_equipo_12
{
    public partial class EstudianteCursos : System.Web.UI.Page
    {

        public List<Curso> listaCursosInscriptos = new List<Curso>();
        public CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
        public List<CategoriaCurso> categorias = new List<CategoriaCurso>();
        protected void Page_Load(object sender, EventArgs e)
        {
            List<InscripcionACurso> listaInscripciones = new List<InscripcionACurso>();
            if (Session["estudiante"] == null)
            {
                Session["MensajeError"] = "No puede acceder a esa pestaña sin ser un estudiante.";
                Response.Redirect("../LogIn.aspx");
            }
            if (!IsPostBack)
            {
                EstudianteMasterPage master = (EstudianteMasterPage)Page.Master;
                master.VerificarMensaje();


                Estudiante estudiante = (Estudiante)Session["estudiante"];
                if(estudiante.Cursos.Count == 0)
                {
                    LabelNoHayCursos.Visible = true;
                    PanelCursosEstudiante.Visible = false;
                } else
                {
                    PanelTituloNoHayCursos.Visible = false;
                }
                rptCursos.DataSource = estudiante.Cursos;
                rptCursos.DataBind();

                cargarDropdownCategoria();
            }

        }

        protected void LinkButtonCurso_Command(object sender, CommandEventArgs e)
        {
            int idCurso = Convert.ToInt32(e.CommandArgument);
            Session.Add("IDCurso", idCurso);
            Response.Redirect("EstudianteUnidades.aspx");
        }

        protected void ButtonDesinscribirse_Command(object sender, CommandEventArgs e)
        {
            InscripcionNegocio inscripcionNegocio = new InscripcionNegocio(false);
            EstudianteNegocio estudianteNegocio = new EstudianteNegocio();
            Estudiante estudiante = (Estudiante)Session["estudiante"];
            int idCursoADesinscribir = Convert.ToInt32(e.CommandArgument);
            Curso cursoADesinscribir = estudiante.Cursos.Find(curso => curso.IDCurso == idCursoADesinscribir);
            try
            {
                inscripcionNegocio.EliminarInscripcion(estudiante.IDUsuario, idCursoADesinscribir);
                estudianteNegocio.Desuscribirse(estudiante.IDUsuario, idCursoADesinscribir);
                estudiante.Cursos.Remove(cursoADesinscribir);
                Session["estudiante"] = estudiante;
                Session["MensajeExito"] = "Desinscripcion exitosa!";
                Response.Redirect("EstudianteCursos.aspx", false);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", "<script>showMessage('Ocurrió un error al Desinscribirse.', 'error');</script>", false);
            }
        }
       

        protected void VerDetalleCurso_Command(object sender, CommandEventArgs e)
        {
            bool home = false;
            int idCurso = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("EstudianteVerDetalleCurso.aspx?idCurso=" + idCurso+"&home="+home);
        }

        protected void btnLimpiarFiltro_Click(object sender, EventArgs e)
        {
            ddlCategorias.SelectedIndex = 0;
            Estudiante estudiante = (Estudiante)Session["estudiante"];
            rptCursos.DataSource = estudiante.Cursos;
            rptCursos.DataBind();
            lblMensaje.Text = "";
            UpdatePanelCursos.Visible = true;
        }
        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            Estudiante estudiante = (Estudiante)Session["estudiante"];
            List<Curso> lista = estudiante.Cursos;
            int idCategoria = Convert.ToInt32(ddlCategorias.SelectedValue);
            if (idCategoria != 0)
            {
                List<Curso> listaFiltrada = lista.FindAll(x => x.Categoria.IDCategoria == idCategoria);
                if (listaFiltrada.Count == 0)
                {
                    lblMensaje.Text = "No se encontraron resultados";
                    UpdatePanelCursos.Visible = false;
                }
                else
                {
                    UpdatePanelCursos.Visible = true;
                    rptCursos.DataSource = listaFiltrada;
                    rptCursos.DataBind();
                    lblMensaje.Text = "";
                }
            }
            else
            {
                rptCursos.DataSource = lista;
                rptCursos.DataBind();
                lblMensaje.Text = "";
                UpdatePanelCursos.Visible = true;
            }
        }
        protected void cargarDropdownCategoria()
        {
            categoriaNegocio = new CategoriaNegocio();
            categorias = categoriaNegocio.ListarCategorias();
            ddlCategorias.DataSource = categorias;
            ddlCategorias.DataTextField = "Nombre";
            ddlCategorias.DataValueField = "IDCategoria";
            ddlCategorias.DataBind();
            ddlCategorias.Items.Insert(0, new ListItem("Todas las categorias", "0"));
        }
        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            Estudiante estudiante = (Estudiante)Session["estudiante"];
            rptCursos.DataSource = estudiante.Cursos;
            rptCursos.DataBind();
            lblMensaje.Text = "";
            UpdatePanelCursos.Visible = true;
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string busqueda = txtBuscar.Text;
            Estudiante estudiante = (Estudiante)Session["estudiante"];
            List<Curso> cursos = estudiante.Cursos;
            List<Curso> listaFiltrada = cursos.FindAll(x => x.Nombre.ToUpper().Contains(busqueda.ToUpper()) || x.Descripcion.ToUpper().Contains(busqueda.ToUpper()) || x.Categoria.Nombre.ToUpper().Contains(busqueda.ToUpper()));
            if (listaFiltrada.Count == 0)
            {
                lblMensaje.Text = "No se encontraron resultados";
                UpdatePanelCursos.Visible = false;
            }
            else
            {
                rptCursos.DataSource = listaFiltrada;
                rptCursos.DataBind();
                lblMensaje.Text = "";
                UpdatePanelCursos.Visible = true;

            }
        }
    }
}