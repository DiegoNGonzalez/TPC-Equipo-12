﻿using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace TPC_equipo_12
{
    public partial class AgregarMateriales : System.Web.UI.Page
    {
        UnidadNegocio unidadNegocio = new UnidadNegocio();
        CursoNegocio cursoNegocio = new CursoNegocio();
        MaterialNegocio MaterialNegocio = new MaterialNegocio();
        LeccionNegocio LeccionNegocio = new LeccionNegocio();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["profesor"] == null)
            {
                Session["MensajeError"] = "No puede acceder a esa pestaña sin ser profesor.";
                Response.Redirect("../LogIn.aspx");
            }
            if (!IsPostBack)
            {
                ModificarMaterial();
            }
        }


        protected void ButtonCrearMaterial_Click(object sender, EventArgs e)
        {
            if (!ValidarFormulario())
            {
                return;
            }
            Curso curso = cursoNegocio.BuscarCurso((int)Session["IDCursoProfesor"]);
            Unidad unidad = curso.Unidades.Find(x => x.IDUnidad == (int)Session["IDUnidadProfesor"]);
            Leccion leccion = unidad.Lecciones.Find(x => x.IDLeccion == (int)Session["IDLeccionProfesor"]);
            try
            {
                MaterialLeccion material = new MaterialLeccion();
                material.Nombre = TextBoxNombreMaterial.Text;
                material.Descripcion = TextBoxDescripcionMaterial.Text;
                material.NroMaterial = int.Parse(TextBoxNumeroMaterial.Text);
                material.TipoMaterial = DropDownListTipoMaterial.SelectedValue;
                material.URL = TextBoxURLMaterial.Text;
                if (Request.QueryString["idMaterial"] != null)
                {
                    material.IDMaterial = Convert.ToInt32(Request.QueryString["idMaterial"]);
                    MaterialNegocio.ModificarMaterial(material);
                    Session["MensajeExito"] = "Material modificado con exito!";
                    Response.Redirect("ProfesorMateriales.aspx", false);
                }
                else
                {
                    if (leccion.Materiales == null)
                    {
                        leccion.Materiales = new List<MaterialLeccion>();
                    }
                    leccion.Materiales.Add(material);
                    MaterialNegocio.CrearMaterial(material, leccion.IDLeccion);
                    Session["MensajeExito"] = "Material creado con exito!";
                    Response.Redirect("ProfesorMateriales.aspx", false);
                }
            }
            catch (Exception ex)
            {
                Session["MensajeError"] = ex.ToString();
                Response.Redirect("ProfesorMateriales.aspx", false);
            }
        }

        protected void ModificarMaterial()
        {
            MaterialLeccion ultimoMaterial = new MaterialLeccion();
            ultimoMaterial = MaterialNegocio.ListarMateriales((int)Session["IDLeccionProfesor"]).Count != 0 ? MaterialNegocio.ListarMateriales((int)Session["IDLeccionProfesor"]).Last() : null;
            if (Request.QueryString["idMaterial"] != null)
            {
                LabelAgregarMaterial.Text = "Modificar Material";
                ButtonCrearMaterial.Text = "Modificar Material";
                int idMaterial = Convert.ToInt32(Request.QueryString["idMaterial"]);
                MaterialLeccion material = MaterialNegocio.ListarMateriales((int)Session["IDLeccionProfesor"]).Find(x => x.IDMaterial == idMaterial);
                TextBoxNombreMaterial.Text = material.Nombre;
                TextBoxDescripcionMaterial.Text = material.Descripcion;
                TextBoxNumeroMaterial.Text = material.NroMaterial.ToString();
                TextBoxNumeroMaterial.Enabled = false;
                DropDownListTipoMaterial.SelectedValue = material.TipoMaterial;
                TextBoxURLMaterial.Text = material.URL;
                material.IDMaterial = idMaterial;

            }
            else
            {
                if (ultimoMaterial != null)
                {
                    TextBoxNumeroMaterial.Text = (ultimoMaterial.NroMaterial + 1).ToString();
                    TextBoxNumeroMaterial.Enabled = false;
                }
                else
                {
                    TextBoxNumeroMaterial.Text = "1";
                    TextBoxNumeroMaterial.Enabled = false;
                }
            }
        }

        protected void ButtonVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProfesorMateriales.aspx", false);
        }
        protected bool ValidarFormulario()
        {
            if (TextBoxNombreMaterial.Text == "" || TextBoxDescripcionMaterial.Text == "" || TextBoxNumeroMaterial.Text == "" || TextBoxURLMaterial.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "info", "<script>showMessage('Faltan campos por completar!', 'info');</script>", false);
                return false;
            }

            return true;
        }
    }
}