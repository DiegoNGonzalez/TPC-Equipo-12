﻿using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.IO;

namespace TPC_equipo_12
{
    public partial class CrearCurso : System.Web.UI.Page
    {
        public CursoNegocio cursoNegocio = new CursoNegocio();
        public string urlImagen { get; set; }
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
                cargarCategorias();
                ModificarCurso();

            }
        }

        //protected void TextBoxUrlImagen_TextChanged(object sender, EventArgs e)
        //{
        //    urlImagenCurso.ImageUrl = TextBoxUrlImagen.Text;
        //}

        protected void ButtonCrearCurso_Click(object sender, EventArgs e)
        {
            Profesor profesor = (Profesor)Session["profesor"];
            if (!validarForm())
            {
                Session["MensajeError"] = "Complete todos los campos!";
                Response.Redirect("CrearCurso.aspx", false);
                return;
            }

            try
            {
                Curso curso = new Curso();
                curso.Nombre = TextBoxNombreCurso.Text;
                curso.Descripcion = TextBoxDescripcionCurso.Text;
                curso.Duracion = Convert.ToInt32(TextBoxDuracionCurso.Text);
                curso.Completo = false;
                curso.Estreno = Convert.ToDateTime(TextBoxEstrenoCurso.Text);

                if (!ValidarFecha(curso.Estreno))
                {
                    Session["MensajeError"] = "La fecha de estreno no puede ser anterior a la fecha actual!";
                    Response.Redirect("CrearCurso.aspx", false);
                    return;
                }

                if (fileImagenCurso.PostedFile != null && fileImagenCurso.PostedFile.ContentLength > 0)
                {
                    string ruta = Server.MapPath("~/Images/");
                    string fileName = "curso-" + profesor.IDUsuario + "-" + Path.GetFileName(fileImagenCurso.PostedFile.FileName);
                    string fullPath = Path.Combine(ruta, fileName);

                    fileImagenCurso.PostedFile.SaveAs(fullPath);
                    curso.Imagen = new Imagen();
                    curso.Imagen.URL = fileName;
                }
                else if (Request.QueryString["idCurso"] != null)
                {
                    // Mantener la imagen existente
                    int idCurso = Convert.ToInt32(Request.QueryString["idCurso"]);
                    Curso cursoExistente = cursoNegocio.BuscarCurso(idCurso);
                    if (cursoExistente != null && cursoExistente.Imagen != null)
                    {
                        curso.Imagen = cursoExistente.Imagen;
                    }
                }
                else
                {
                    curso.Imagen = new Imagen();
                    curso.Imagen.URL = "curso-0.jpg";
                }

                curso.Categoria = new CategoriaCurso();
                curso.Categoria.IDCategoria = int.Parse(DropDownListCategoriaCurso.SelectedValue);
                curso.Categoria.Nombre = DropDownListCategoriaCurso.SelectedItem.Text;
                curso.Estado = true;
                curso.Unidades = new List<Unidad>();

                if (Request.QueryString["idCurso"] != null)
                {
                    curso.IDCurso = Convert.ToInt32(Request.QueryString["idCurso"]);
                    cursoNegocio.ModificarCurso(curso);
                    Session["MensajeExito"] = "Curso modificado con éxito!";
                    profesor.Cursos = cursoNegocio.ListarCursos();
                    profesor.Cursos = cursoNegocio.ValidarCursoCompleto(profesor.Cursos);
                    Session.Add("profesor", profesor);
                    Response.Redirect("ProfesorFabricaDeCursos.aspx", false);
                }
                else
                {
                    cursoNegocio.CrearCurso(curso, profesor.IDUsuario);
                    profesor.Cursos.Add(curso);
                    Session.Add("profesor", profesor);
                    Session["MensajeExito"] = "Curso creado con éxito!";
                    Response.Redirect("ProfesorFabricaDeCursos.aspx", false);
                }
            }
            catch (Exception ex)
            {
                Session.Add("Error", ex.ToString());
                Response.Redirect("../Error.aspx");
                throw ex;
            }
        }


        protected void ModificarCurso()
    {
        if (Request.QueryString["idCurso"] != null)
        {
            LabelTitulo.Text = "Modificar Curso";
            ButtonCrearCurso.Text = "Modificar Curso";
            int idCurso = Convert.ToInt32(Request.QueryString["idCurso"]);
            Curso curso = cursoNegocio.ListarCursos().Find(x => x.IDCurso == idCurso);
            TextBoxNombreCurso.Text = curso.Nombre;
            TextBoxDescripcionCurso.Text = curso.Descripcion;
            TextBoxDuracionCurso.Text = curso.Duracion.ToString();
            TextBoxEstrenoCurso.Text = curso.Estreno.ToString("yyyy-MM-dd");
            DropDownListCategoriaCurso.SelectedValue = curso.Categoria.IDCategoria.ToString();

            //TextBoxUrlImagen.Text = curso.Imagen.URL;
        }
    }

    protected void ButtonVolver_Click(object sender, EventArgs e)
    {
            if (Request.QueryString["idCurso"] != null)
            {
                   Response.Redirect("ProfesorFabricaDeCursos.aspx", false);
            }
            else{
                Response.Redirect("ProfesorCursos.aspx", false);

            }
    }
    private void cargarCategorias()
    {
        CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
        try
        {
            List<CategoriaCurso> listaCategorias = categoriaNegocio.ListarCategorias();
            DropDownListCategoriaCurso.DataSource = listaCategorias;
            DropDownListCategoriaCurso.DataTextField = "Nombre";
            DropDownListCategoriaCurso.DataValueField = "IDCategoria";
            DropDownListCategoriaCurso.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected bool validarForm()
    {
        if (TextBoxNombreCurso.Text == "" || TextBoxDescripcionCurso.Text == "" || TextBoxDuracionCurso.Text == "" || TextBoxEstrenoCurso.Text == "")
        {
            return false;
        }
        return true;
    }

    protected bool ValidarFecha(DateTime FechaAValidar)
    {
        DateTime Hoy = DateTime.Now;
        if (FechaAValidar < Hoy)
        {
            return false;
        }
        return true;
    }
}
}