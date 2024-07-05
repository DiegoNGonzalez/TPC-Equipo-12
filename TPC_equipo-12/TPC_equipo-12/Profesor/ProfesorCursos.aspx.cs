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
                MostrarCategoria();

                List<Curso> cursosInactivos = new List<Curso>();
                cursosInactivos = cursoNegocio.ListarCursos();
                cursosInactivos = cursoNegocio.ValidarCursoCompleto(cursosInactivos);
                cursosInactivos = cursoNegocio.ValidarCursosInactivos(cursosInactivos);
                RepeaterCursosInactivos.DataSource = cursosInactivos;
                RepeaterCursosInactivos.DataBind();
                MostrarCategoria();
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

        private void MostrarCategoria()
        {
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            foreach (RepeaterItem item in rptProfesorCursos.Items)
            {
                HiddenField hiddenFieldIDCurso = (HiddenField)item.FindControl("HiddenFieldIDCurso");
                Label lblCategoria = (Label)item.FindControl("LabelCategoriaCurso");

                if (hiddenFieldIDCurso != null && lblCategoria != null)
                {
                    int idCurso = int.Parse(hiddenFieldIDCurso.Value);
                    if (categoriaNegocio.CategoriaNombreXIDCurso(idCurso) != "")
                    {
                    lblCategoria.Text = categoriaNegocio.CategoriaNombreXIDCurso(idCurso);
                    } else
                    {
                        lblCategoria.Text = "Sin categoria";
                    }
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
    }
}