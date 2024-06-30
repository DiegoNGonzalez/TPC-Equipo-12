using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace TPC_equipo_12
{
    public partial class ProfesorFabricaDeCursos : System.Web.UI.Page
    {
        public List<Curso> listaCursos = new List<Curso>();
        public CursoNegocio cursoNegocio = new CursoNegocio();
        public Profesor profesor = new Profesor();
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

                List<Curso> listaCursosAux = new List<Curso>();
                listaCursosAux = cursoNegocio.ListarCursos();
                listaCursosAux = cursoNegocio.ValidarCursoIncompleto(listaCursosAux);
                rptProfesorCursos.DataSource = listaCursosAux;
                rptProfesorCursos.DataBind();
                MostrarCategoria();
            }
        }

        protected void LinkButtonCursoProf_Command(object sender, CommandEventArgs e)
        {
            int idCurso = Convert.ToInt32(e.CommandArgument);
            Session.Add("IDCursoProfesor", idCurso);
            Response.Redirect("ProfesorUnidades.aspx");
        }

        protected void ButtonEliminarCurso_Command(object sender, CommandEventArgs e)
        {
            Profesor profesor = (Profesor)Session["profesor"];
            int idCursoAEliminar = Convert.ToInt32(e.CommandArgument);
            Curso cursoAEliminar = profesor.Cursos.Find(curso => curso.IDCurso == idCursoAEliminar);

            if (cursoAEliminar != null)
            {
                try
                {
                    profesor.Cursos.Remove(cursoAEliminar);
                    Session["profesor"] = profesor;
                    cursoNegocio.EliminarCurso(idCursoAEliminar);
                    Session["MensajeExito"] = "Curso eliminado con exito!";
                    Response.Redirect("ProfesorCursos.aspx", false);
                }
                catch (Exception ex)
                {
                    Session.Add("Error", ex.ToString());
                    Response.Redirect("../Error.aspx");
                }
            }
            else
            {
                Session.Add("Error", "El curso no se encontró en la lista de cursos del profesor.");
                Response.Redirect("../Error.aspx");
            }
        }

        protected void ButtonModificarCurso_Command(object sender, CommandEventArgs e)
        {
            int idCurso = Convert.ToInt32(e.CommandArgument);
            Session.Add("IDCursoProfesor", idCurso);
            Response.Redirect("CrearCurso.aspx?IdCurso=" + idCurso);
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
                    }
                    else
                    {
                        lblCategoria.Text = "Sin categoria";
                    }
                }
            }
        }

        protected void ButtonAltaCurso_Command(object sender, CommandEventArgs e)
        {
            int idCurso = Convert.ToInt32(e.CommandArgument);
            if (ValidarCurso(idCurso))
            {
            cursoNegocio.DarDeAltaCurso(idCurso);
            // Una vez que doy de alta el curso, actualizo al listado de cursos que tiene el profesor.
            Profesor profesor = (Profesor)Session["profesor"];
            profesor.Cursos = cursoNegocio.ListarCursos();
            profesor.Cursos = cursoNegocio.ValidarCursoCompleto(profesor.Cursos);

            Session["MensajeExito"] = "Curso dado de alta con exito!";
            Response.Redirect("ProfesorFabricaDeCursos.aspx", false);
            } else
            {
            Response.Redirect("ProfesorFabricaDeCursos.aspx", false);
            }
        }

        protected bool ValidarCurso(int idCurso)
        {
            string msj;
            Curso curso = cursoNegocio.BuscarCurso(idCurso);
            if (curso.Unidades.Count == 0 || curso.Unidades == null)
            {
                msj = "No puedes dar de alta este Curso, no tiene Unidades.";
                Session["MensajeError"] = msj;
                return false;
            }
            else
            {
                foreach (Unidad unidad in curso.Unidades)
                {
                    if (unidad.Lecciones.Count == 0 || unidad.Lecciones == null)
                    {
                        msj = "No puedes dar de alta este Curso, la Unidad N°" + unidad.NroUnidad + " no tiene Lecciones.";
                        Session["MensajeError"] = msj;
                        return false;
                    }
                    else
                    {
                        foreach (Leccion leccion in unidad.Lecciones)
                        {
                            if (leccion.Materiales.Count == 0 || leccion.Materiales == null)
                            {
                                msj = "No puedes dar de alta este Curso, la Leccion N°" + leccion.NroLeccion + " no tiene Materiales.";
                                Session["MensajeError"] = msj;
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }
    }
}