﻿using Dominio;
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

                profesor = (Profesor)Session["profesor"];
                Session.Add("listaCursosProfesor", profesor.Cursos);
                profesor.Cursos = cursoNegocio.ValidarCursoCompleto(profesor.Cursos);
                rptProfesorCursos.DataSource = profesor.Cursos;
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
                    } else
                    {
                        lblCategoria.Text = "Sin categoria";
                    }
                }
            }
        }
    }
}