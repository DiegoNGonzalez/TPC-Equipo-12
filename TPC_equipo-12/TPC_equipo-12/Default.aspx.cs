using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace TPC_equipo_12
{
    public partial class Default : System.Web.UI.Page
    {
        public List<Curso> listaCursos = new List<Curso>();
        public CursoNegocio cursoNegocio = new CursoNegocio();
        public CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
        public List<CategoriaCurso> categorias = new List<CategoriaCurso>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                listaCursos = cursoNegocio.ListarCursos();
                listaCursos = cursoNegocio.ValidarCursoCompleto(listaCursos);
                listaCursos = cursoNegocio.ValidarCursosActivos(listaCursos);
                rptCursos.DataSource = listaCursos;
                rptCursos.DataBind();

                cargarDropdownCategoria();
                
            }
        }
        protected void LinkButtonDefault_Command(object sender, CommandEventArgs e)
        {
            int idCurso = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("VerDetalleCurso.aspx?idCurso=" + idCurso);
            
        }
        protected void btnLimpiarFiltro_Click(object sender, EventArgs e)
        {
            ddlCategorias.SelectedIndex = 0;
            listaCursos = cursoNegocio.ListarCursos();
            listaCursos = cursoNegocio.ValidarCursoCompleto(listaCursos);
            listaCursos = cursoNegocio.ValidarCursosActivos(listaCursos);
            rptCursos.DataSource = listaCursos;
            rptCursos.DataBind();
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
                    rptCursos.DataSource = listaFiltrada;
                    rptCursos.DataBind();
                    lblMensaje.Text = "";
                }
            }
            else
            {
                rptCursos.DataSource = listaCursos;
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
                rptCursos.DataSource = listaFiltrada;
                rptCursos.DataBind();
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
            rptCursos.DataSource = listaCursos;
            rptCursos.DataBind();
            lblMensaje.Text = "";
        }
    }
}