using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_equipo_12
{
    public partial class ProfesorCursos : System.Web.UI.Page
    {
        public List<Curso> listaCursos = new List<Curso>();
        public CursoNegocio cursoNegocio = new CursoNegocio();
        public Profesor profesor = new Profesor();
        public NotificacionNegocio notif = new NotificacionNegocio();
        public CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
        public List<CategoriaCurso> categorias = new List<CategoriaCurso>();
        public List<CategoriaCurso> categoriasInactivo = new List<CategoriaCurso>();
        public List<Curso> cursosInactivos = new List<Curso>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["profesor"] == null)
            {
                Session["MensajeError"] = "No puede acceder a esa pestaña sin ser profesor.";
                Response.Redirect("../LogIn.aspx");
            }
            if (!IsPostBack)
            {
                ProfesorMasterPage master = (ProfesorMasterPage)Page.Master;
                master.VerificarMensaje();

                //profesor = (Profesor)Session["profesor"];
                //Session.Add("listaCursosProfesor", profesor.Cursos);
                listaCursos = cursoNegocio.ListarCursos();
                listaCursos = cursoNegocio.ValidarCursoCompleto(listaCursos);
                listaCursos = cursoNegocio.ValidarCursosActivos(listaCursos);
                rptProfesorCursos.DataSource = listaCursos;
                rptProfesorCursos.DataBind();
                

                cursosInactivos = cursoNegocio.ListarCursos();
                cursosInactivos = cursoNegocio.ValidarCursoCompleto(cursosInactivos);
                cursosInactivos = cursoNegocio.ValidarCursosInactivos(cursosInactivos);
                RepeaterCursosInactivos.DataSource = cursosInactivos;
                RepeaterCursosInactivos.DataBind();
                
                cargarDropdownCategoria();
                cargarDropdownCategoriaInactivo();
            }
        }
        protected void LinkButtonCursoProf_Command(object sender, CommandEventArgs e)
        {
            int idCurso = Convert.ToInt32(e.CommandArgument);
            Session.Add("IDCursoProfesor", idCurso);
            Session["Home"] = true;
            Response.Redirect("ProfesorUnidades.aspx");
        }
        protected void ButtonDeshabilitarCurso_Command(object sender, CommandEventArgs e)
        {
            int idCursoADeshabilitar = Convert.ToInt32(e.CommandArgument);

            if (idCursoADeshabilitar != 0)
            {
                try
                {
                    cursoNegocio.DeshabilitarCurso(idCursoADeshabilitar);
                    notif.notificacionXCursoDeshabilitado(idCursoADeshabilitar);
                    Session["MensajeExito"] = "Curso deshabilitado correctamente.";
                    Response.Redirect("ProfesorCursos.aspx", false);
                }
                catch (Exception ex)
                {
                    Session["MensajeError"] = "Ocurrio un error al intentar Deshabilitar el curso.";
                    Response.Redirect("ProfesorCursos.aspx", false);
                }
            }
        }

       

        protected void ButtonHabilitar_Command(object sender, CommandEventArgs e)
        {
            int idCursoAHabilitar = Convert.ToInt32(e.CommandArgument);

            try
            {
                cursoNegocio.HabilitarCurso(idCursoAHabilitar);
                Session["MensajeExito"] = "Curso Habilitado correctamente.";
                Response.Redirect("ProfesorCursos.aspx", false);
            }
            catch (Exception ex)
            {
                Session["MensajeError"] = "Ocurrio un error al intentar Habilitar el curso.";
                Response.Redirect("ProfesorCursos.aspx", false);
            }
        }

        protected void ButtonFabrica_Command(object sender, CommandEventArgs e)
        {
            int idCurso = Convert.ToInt32(e.CommandArgument);
            try
            {
                cursoNegocio.MarcarIncompletoCurso(idCurso);
                Session["MensajeExito"] = "Curso enviado a la fabrica de cursos!";
                Response.Redirect("ProfesorFabricaDeCursos.aspx", false);

            }
            catch (Exception ex)
            {
                Session["MensajeError"] = "Ocurrio un error al intentar enviar el curso a la fabrica.";
                Response.Redirect("ProfesorCursos.aspx", false);
            }
        }

        protected void ButtonResenias_Command(object sender, CommandEventArgs e)
        {
            int idCurso = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("VerResenias.aspx?idCurso=" + idCurso,false);
        }

        protected void btnLimpiarFiltro_Click(object sender, EventArgs e)
        {
            ddlCategorias.SelectedIndex = 0;
            listaCursos = cursoNegocio.ListarCursos();
            listaCursos = cursoNegocio.ValidarCursoCompleto(listaCursos);
            listaCursos = cursoNegocio.ValidarCursosActivos(listaCursos);
            rptProfesorCursos.DataSource = listaCursos;
            rptProfesorCursos.DataBind();
            lblMensaje.Text = "";
            UpdatePanelCursos.Visible = true;
        }
        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            listaCursos = cursoNegocio.ListarCursos();
            listaCursos = cursoNegocio.ValidarCursoCompleto(listaCursos);
            listaCursos = cursoNegocio.ValidarCursosActivos(listaCursos);
            int idCategoria = Convert.ToInt32(ddlCategorias.SelectedValue);
            if (idCategoria != 0)
            {
                List<Curso> listaFiltrada = listaCursos.FindAll(x => x.Categoria.IDCategoria == idCategoria);
                if (listaFiltrada.Count == 0)
                {
                    lblMensaje.Text = "No se encontraron resultados";
                    UpdatePanelCursos.Visible = false;
                }
                else
                {
                    UpdatePanelCursos.Visible = true;
                    rptProfesorCursos.DataSource = listaFiltrada;
                    rptProfesorCursos.DataBind();
                    lblMensaje.Text = "";
                }
            }
            else
            {
                rptProfesorCursos.DataSource = listaCursos;
                rptProfesorCursos.DataBind();
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
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string busqueda = txtBuscar.Text;
            listaCursos = cursoNegocio.ListarCursos();
            listaCursos = cursoNegocio.ValidarCursoCompleto(listaCursos);
            listaCursos = cursoNegocio.ValidarCursosActivos(listaCursos);
            List<Curso> listaFiltrada = listaCursos.FindAll(x => x.Nombre.ToUpper().Contains(busqueda.ToUpper()) || x.Descripcion.ToUpper().Contains(busqueda.ToUpper()) || x.Categoria.Nombre.ToUpper().Contains(busqueda.ToUpper()));
            if (listaFiltrada.Count == 0)
            {
                lblMensaje.Text = "No se encontraron resultados";
                UpdatePanelCursos.Visible = false;
            }
            else
            {
                UpdatePanelCursos.Visible = true;
                rptProfesorCursos.DataSource = listaFiltrada;
                rptProfesorCursos.DataBind();
                lblMensaje.Text = "";

            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            UpdatePanelCursos.Visible = true;
            txtBuscar.Text = "";
            listaCursos = cursoNegocio.ListarCursos();
            listaCursos = cursoNegocio.ValidarCursoCompleto(listaCursos);
            listaCursos = cursoNegocio.ValidarCursosActivos(listaCursos);
            rptProfesorCursos.DataSource = listaCursos;
            rptProfesorCursos.DataBind();
            lblMensaje.Text = "";
        }
        //EMPIEZAN BUSCADORES INACTIVOS.

        protected void cargarDropdownCategoriaInactivo()
        {
            categoriaNegocio = new CategoriaNegocio();
            categoriasInactivo = categoriaNegocio.ListarCategorias();
            ddlInactivo.DataSource = categoriasInactivo;
            ddlInactivo.DataTextField = "Nombre";
            ddlInactivo.DataValueField = "IDCategoria";
            ddlInactivo.DataBind();
            ddlInactivo.Items.Insert(0, new ListItem("Todas las categorias", "0"));
        }
        protected void btnLimpiarFiltroInactivo_Click(object sender, EventArgs e)
        {
            ddlInactivo.SelectedIndex = 0;
            cursosInactivos = cursoNegocio.ListarCursos();
            cursosInactivos = cursoNegocio.ValidarCursoCompleto(cursosInactivos);
            cursosInactivos = cursoNegocio.ValidarCursosInactivos(cursosInactivos);
            RepeaterCursosInactivos.DataSource = cursosInactivos;
            RepeaterCursosInactivos.DataBind();
            lblMensajeInactivo.Text = "";
            UpdatePanelCursosInactivos.Visible = true;
        }
        protected void btnFiltrarInactivo_Click(object sender, EventArgs e)
        {
            cursosInactivos = cursoNegocio.ListarCursos();
            cursosInactivos = cursoNegocio.ValidarCursoCompleto(cursosInactivos);
            cursosInactivos = cursoNegocio.ValidarCursosInactivos(cursosInactivos);
            int idCategoria = Convert.ToInt32(ddlInactivo.SelectedValue);
            if (idCategoria != 0)
            {
                List<Curso> listaFiltrada = cursosInactivos.FindAll(x => x.Categoria.IDCategoria == idCategoria);
                if (listaFiltrada.Count == 0)
                {
                    lblMensajeInactivo.Text = "No se encontraron resultados";
                    UpdatePanelCursosInactivos.Visible = false;
                }
                else
                {
                    UpdatePanelCursosInactivos.Visible = true;
                    RepeaterCursosInactivos.DataSource = listaFiltrada;
                    RepeaterCursosInactivos.DataBind();
                    lblMensajeInactivo.Text = "";
                }
            }
            else
            {
                RepeaterCursosInactivos.DataSource = cursosInactivos;
                RepeaterCursosInactivos.DataBind();
                lblMensajeInactivo.Text = "";
                UpdatePanelCursosInactivos.Visible = true;
            }
        }
        protected void btnBuscarInactivo_Click(object sender, EventArgs e)
        {
            string busqueda = TextBoxInactivo.Text;
            cursosInactivos = cursoNegocio.ListarCursos();
            cursosInactivos = cursoNegocio.ValidarCursoCompleto(cursosInactivos);
            cursosInactivos = cursoNegocio.ValidarCursosInactivos(cursosInactivos);
            List<Curso> listaFiltrada = cursosInactivos.FindAll(x => x.Nombre.ToUpper().Contains(busqueda.ToUpper()) || x.Descripcion.ToUpper().Contains(busqueda.ToUpper()) || x.Categoria.Nombre.ToUpper().Contains(busqueda.ToUpper()));
            if (listaFiltrada.Count == 0)
            {
                lblMensajeInactivo.Text = "No se encontraron resultados";
                UpdatePanelCursosInactivos.Visible = false;
            }
            else
            {
                UpdatePanelCursosInactivos.Visible = true;
                RepeaterCursosInactivos.DataSource = listaFiltrada;
                RepeaterCursosInactivos.DataBind();
                lblMensajeInactivo.Text = "";

            }
        }

        protected void btnLimpiarInactivo_Click(object sender, EventArgs e)
        {
            UpdatePanelCursosInactivos.Visible = true;
            TextBoxInactivo.Text = "";
            cursosInactivos = cursoNegocio.ListarCursos();
            cursosInactivos = cursoNegocio.ValidarCursoCompleto(cursosInactivos);
            cursosInactivos = cursoNegocio.ValidarCursosInactivos(cursosInactivos);
            RepeaterCursosInactivos.DataSource = cursosInactivos;
            RepeaterCursosInactivos.DataBind();
            lblMensajeInactivo.Text = "";
        }
    }
}