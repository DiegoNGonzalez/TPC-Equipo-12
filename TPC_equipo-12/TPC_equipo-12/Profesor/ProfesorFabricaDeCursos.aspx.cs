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
            Session["Home"] = false;
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

                Session["MensajeExito"] = "Curso dado de alta con exito!";
                Response.Redirect("ProfesorCursos.aspx", false);
            }
            else
            {
                Response.Redirect("ProfesorFabricaDeCursos.aspx", false);
            }
        }

        protected bool ValidarCurso(int idCurso)
        {
            string msj;
            Curso curso = cursoNegocio.BuscarCurso(idCurso);
            List<Unidad> listaUnidadesHabilitadas = new List<Unidad>();
            List<Leccion> listaLeccionesHabilitadas = new List<Leccion>();
            if (curso.Unidades.Count == 0 || curso.Unidades == null)
            {
                msj = "No puedes dar de alta este Curso, no tiene Unidades.";
                Session["MensajeError"] = msj;
                return false;
            }
            else
            {
                if (!ValidarUnidades(curso.Unidades, listaUnidadesHabilitadas))
                {
                    return false;
                }

                foreach (Unidad unidad in listaUnidadesHabilitadas)
                {
                    if (unidad.Lecciones.Count == 0 || unidad.Lecciones == null)
                    {
                        msj = "No puedes dar de alta este Curso, la Unidad N°" + unidad.NroUnidad + " no tiene Lecciones.";
                        Session["MensajeError"] = msj;
                        return false;
                    }
                    else
                    {
                        if (!ValidarLecciones(unidad.Lecciones, unidad.NroUnidad, listaLeccionesHabilitadas))
                        {
                            return false; 
                        }
                        foreach (Leccion leccion in listaLeccionesHabilitadas)
                        {
                            if (leccion.Materiales.Count == 0 || leccion.Materiales == null)
                            {
                                msj = "No puedes dar de alta este Curso, la Leccion N°" + leccion.NroLeccion + " de la Unidad N°"+unidad.NroUnidad+" no tiene Materiales.";
                                Session["MensajeError"] = msj;
                                return false;
                            }
                            else
                            {
                                if(!ValidarMateriales(leccion.Materiales, leccion.NroLeccion, unidad.NroUnidad))
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }
            }
            return true;
        }

        protected bool ValidarUnidades(List<Unidad> listaAValidar, List<Unidad> listaACargar)
        {
            int ContadorUnidadInhabilitado = 0;
            string msj;
            foreach (Unidad unidad in listaAValidar)
            {
                if (unidad.Estado == false)
                {
                    ContadorUnidadInhabilitado++;
                }
                else
                {
                    listaACargar.Add(unidad);
                }
            }
            if (listaAValidar.Count == ContadorUnidadInhabilitado)
            {
                msj = "No puedes dar de alta este Curso, todas las Unidades estan Deshabilitadas.";
                Session["MensajeError"] = msj;
                return false;
            }
            return true;
        }

        protected bool ValidarLecciones(List<Leccion> listaAValidar, int NroUnidad, List<Leccion> ListaACargar)
        {
            int ContadorLeccionInhabilitado = 0;
            string msj;
            List<Leccion> AuxLeccionesHabilitadas = new List<Leccion>();
            foreach (Leccion leccion in listaAValidar)
            {
                if (leccion.Estado == false)
                {
                    ContadorLeccionInhabilitado++;
                }
                else
                {
                    ListaACargar.Add(leccion);
                }
            }
            if (listaAValidar.Count == ContadorLeccionInhabilitado)
            {
                msj = "No puedes dar de alta este Curso, todas las Lecciones de la Unidad N°" + NroUnidad +" estan Deshabilitadas.";
                Session["MensajeError"] = msj;
                return false;
            }
            return true;
        }

        protected bool ValidarMateriales(List<MaterialLeccion> listaAValidar, int NroLeccion, int NroUnidad)
        {
            int ContadorMaterialInhabilitado = 0;
            string msj;
            foreach (MaterialLeccion material in listaAValidar)
            {
                if (material.Estado == false)
                {
                    ContadorMaterialInhabilitado++;
                }
            }
            if (listaAValidar.Count == ContadorMaterialInhabilitado)
            {
                msj = "No puedes dar de alta este Curso, todos los Materiales de la Leccion N°" + NroLeccion + " de la Unidad N°"+NroUnidad+" estan Deshabilitados.";
                Session["MensajeError"] = msj;
                return false;
            }
            return true;
        }
    }
}