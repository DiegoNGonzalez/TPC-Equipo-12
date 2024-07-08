using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_equipo_12
{
    public partial class DefaultEstudiante : System.Web.UI.Page
    {
        public List<Curso> listaCursos = new List<Curso>();
        public CursoNegocio cursoNegocio = new CursoNegocio();
        public Estudiante EstudianteLogeado = new Estudiante();
        public List<Curso> listaCursosInscriptos = new List<Curso>();
        public InscripcionNegocio inscripcionNegocio = new InscripcionNegocio(false);
        public NotificacionNegocio notificacionNegocio = new NotificacionNegocio();
        public List<Curso> cursosNoInscriptos = new List<Curso>();
        public ProfesorNegocio profesorNegocio = new ProfesorNegocio();
        public CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
        public List<CategoriaCurso> categorias = new List<CategoriaCurso>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                EstudianteMasterPage master = (EstudianteMasterPage)Page.Master;
                master.VerificarMensaje();
                if (Session["estudiante"] == null)
                {
                    Session["MensajeError"] = "No puede acceder a esa pestaña sin ser un estudiante.";
                    Response.Redirect("../LogIn.aspx");
                }
                listaCursos = cursoNegocio.ListarCursos();
                EstudianteLogeado = (Estudiante)Session["estudiante"];

                EstaInscripto(listaCursos);

                EstudianteLogeado.Cursos = listaCursosInscriptos;
                Session["estudiante"] = EstudianteLogeado;
                Session.Add("listaCursosInscriptos", listaCursosInscriptos);

                listaCursos = cursoNegocio.ValidarCursoCompleto(listaCursos);
                listaCursos = cursoNegocio.ValidarCursosActivos(listaCursos);
                NoEstaInscripto(listaCursos);

                Session.Add("cursosNoInscriptos", cursosNoInscriptos);
                rptCursos.DataSource = cursosNoInscriptos;
                rptCursos.DataBind();
                
                cargarDropdownCategoria();
            }
        }

        protected void EstaInscripto(List<Curso> listCursos)
        {
            Estudiante EstudianteEnSession = (Estudiante)Session["estudiante"];
            List<int> auxIds = cursoNegocio.IDCursosXEstudiante(EstudianteEnSession.IDUsuario);
            foreach (int id in auxIds)
            {
                foreach (Curso curso in listCursos)
                {
                    if (curso.IDCurso == id)
                    {
                        listaCursosInscriptos.Add(curso);
                    }
                }
            }
        }

        protected void NoEstaInscripto(List<Curso> cursosActivos)
        {
            Estudiante EstudianteEnSession = (Estudiante)Session["estudiante"];
            List<int> auxIds = cursoNegocio.IDCursosXEstudiante(EstudianteEnSession.IDUsuario);

            foreach (Curso curso in cursosActivos)
            {
                if (!auxIds.Contains(curso.IDCurso))
                {
                    cursosNoInscriptos.Add(curso);
                }
            }
        }


        protected void btnInscribirse_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Control lblControl = button.NamingContainer.FindControl("lblIDCurso");
            Label label = (Label)lblControl;
            Estudiante estudianteAux = (Estudiante)Session["estudiante"];
            int idCurso = Convert.ToInt32(label.Text);
            Curso aux = cursoNegocio.BuscarCurso(idCurso);
            Profesor profesor = new Profesor();
            profesor = profesorNegocio.buscarProfesorXCurso(aux.IDCurso);
            InscripcionACurso inscripcionAuxiliar = new InscripcionACurso();
            inscripcionAuxiliar = inscripcionNegocio.BuscarInscripcionXCursoYEstudiante(idCurso, estudianteAux.IDUsuario);
            int idNotificacion = notificacionNegocio.buscarNotificacionXInscripcionXUsuario(inscripcionAuxiliar.IDInscripcion, profesor.IDUsuario);
            try
            {
                if (inscripcionAuxiliar.IDInscripcion != 0)
                {
                    if (inscripcionAuxiliar.Estado == 'P')
                    {
                        Session["MensajeError"]= "Ya existe una inscripcion pendiente de aprobación!";
                        Response.Redirect("DefaultEstudiante.aspx", false);
                    }
                    else
                    {
                        inscripcionNegocio.reinscribir(inscripcionAuxiliar.IDInscripcion);
                        notificacionNegocio.marcarComoNoLeidaYMensaje(idNotificacion, "Nueva Reinscripción");
                        Session["MensajeExito"] = "La Reinscripción enviada correctamente!";
                        Response.Redirect("DefaultEstudiante.aspx", false);

                    }

                }
                else
                {

                    bool seInscribio = inscripcionNegocio.Incripcion(estudianteAux, aux);
                    if (seInscribio)
                    {
                        int idInscripcion = inscripcionNegocio.UltimoIDInscripcion();
                        notificacionNegocio.AgregarNotificacionXInscripcion(idInscripcion, idCurso);
                        Session["MensajeExito"] = "La inscripción enviada correctamente!";
                        Response.Redirect("DefaultEstudiante.aspx", false);

                    }
                }

            }
            catch (Exception ex)
            {

                //ex.Message.ToString();
                //Session.Add("error", "No se pudo inscribir al curso, ya estas inscripto u ocurrio un error");
                //Response.Redirect("../Error.aspx");
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", "<script>showMessage('Ocurrió un error al enviar la inscripción.', 'error');</script>", false);


            }
            UpdatePanelCursos.Update();
        }

        protected void btnLimpiarFiltro_Click(object sender, EventArgs e)
        {
            ddlCategorias.SelectedIndex = 0;
            List<Curso> lista = (List<Curso>)Session["cursosNoInscriptos"];
            rptCursos.DataSource = lista;
            rptCursos.DataBind();
            lblMensaje.Text = "";
            UpdatePanelCursos.Visible = true;
        }
        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            List<Curso> cursos = (List<Curso>)Session["cursosNoInscriptos"];
            int idCategoria = Convert.ToInt32(ddlCategorias.SelectedValue);
            if (idCategoria != 0)
            {
                List<Curso> listaFiltrada = cursos.FindAll(x => x.Categoria.IDCategoria == idCategoria);
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
                rptCursos.DataSource = cursos;
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
        protected void LinkButtonEstudiante_Command(object sender, CommandEventArgs e)
        {
            bool home= true;
            int idCurso = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("EstudianteVerDetalleCurso.aspx?idCurso=" + idCurso +"&home="+home);

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string busqueda = txtBuscar.Text;
            List<Curso> cursosNoInscriptos = (List<Curso>)Session["cursosNoInscriptos"];
            List<Curso> listaFiltrada = cursosNoInscriptos.FindAll(x => x.Nombre.ToUpper().Contains(busqueda.ToUpper()) || x.Descripcion.ToUpper().Contains(busqueda.ToUpper()) || x.Categoria.Nombre.ToUpper().Contains(busqueda.ToUpper()));
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

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            List<Curso> lista = (List<Curso>)Session["cursosNoInscriptos"];
            rptCursos.DataSource = lista;
            rptCursos.DataBind();
            lblMensaje.Text = "";
            UpdatePanelCursos.Visible = true;
        }

        protected void rptCursos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Image imgCurso = (Image)e.Item.FindControl("ImagenCurso");
                var dataItem = (Curso)e.Item.DataItem;

                if (imgCurso != null && dataItem != null && dataItem.Imagen != null)
                {
                    imgCurso.ImageUrl = "~/Images/" + dataItem.Imagen.URL;
                }
            }
        }
    }
}