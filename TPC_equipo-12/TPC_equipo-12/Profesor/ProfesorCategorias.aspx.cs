using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_equipo_12
{
    public partial class ProfesorCategorias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["profesor"] == null)
            {
                Session["MensajeError"] = "No puede acceder a esa pestaña sin ser profesor.";
                Response.Redirect("../LogIn.aspx");
            }
            if (!IsPostBack)
            {
                cargarCategorias();
            }

        }

        private void cargarCategorias()
        {
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            try
            {
                List<CategoriaCurso> listaCategorias = categoriaNegocio.ListarCategorias();
                dropCategorias.DataSource = listaCategorias;
                dropCategorias.DataTextField = "Nombre";
                dropCategorias.DataValueField = "IDCategoria";
                dropCategorias.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            string nuevaCategoria = txtNuevaCategoria.Text.Trim();
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            if (!string.IsNullOrEmpty(nuevaCategoria))
            {
                try
                {
                    CategoriaCurso categoria = new CategoriaCurso();
                    categoria.Nombre = nuevaCategoria;
                    if (!(categoriaNegocio.ExisteCategoria(nuevaCategoria)))
                    {
                        categoriaNegocio.AgregarCategoria(categoria);
                        lblNotificacion.Text = "Se ha agregado la categoría correctamente.";
                        lblNotificacion.ForeColor = System.Drawing.Color.Green;
                        lblNotificacion.Visible = true;
                    }
                    else
                    {
                        lblNotificacion.Text = "Ya existe una categoría con ese nombre, ingrese otro por favor..";
                        lblNotificacion.Visible = true;
                    }
                    cargarCategorias();
                    txtNuevaCategoria.Text = "";
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                lblNotificacion.Text = "Por favor, ingrese el nombre de la nueva categoría.";
                lblNotificacion.Visible = true;
            }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            string categoriaActual = dropCategorias.SelectedValue;
            string nuevoNombre = txtNuevaCategoria.Text.Trim();
            CategoriaNegocio categoriaNegocio  = new CategoriaNegocio();

            if (!string.IsNullOrEmpty(nuevoNombre))
            {
                try
                {
                    CategoriaCurso categoria = new CategoriaCurso();
                    categoria.IDCategoria = int.Parse(categoriaActual);
                    categoria.Nombre = nuevoNombre;
                    categoriaNegocio.ModificarCategoria(categoria);
                    cargarCategorias();
                    lblNotificacion.Text = "Se ha modificado la categoría correctamente.";
                    lblNotificacion.ForeColor = System.Drawing.Color.Green;
                    lblNotificacion.Visible = true;
                    txtNuevaCategoria.Text = "";


                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                lblNotificacion.Text = "Por favor, ingrese el nombre de la nueva categoría.";
                lblNotificacion.Visible = true;
            }
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            lblNotificacion.Visible = false;
            lblNotificacion.ForeColor = System.Drawing.Color.Red;
            string categoriaSeleccionada = dropCategorias.SelectedValue;
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            try
            {
                int idCategoria = int.Parse(categoriaSeleccionada);
                if (categoriaNegocio.CategoriaAsociadaACurso(idCategoria))
                {
                    lblNotificacion.Text = "La categoría está asociada a un curso y no se puede eliminar.";
                    lblNotificacion.Visible = true;
                    return;
                }

                categoriaNegocio.EliminarCategoria(idCategoria);
                cargarCategorias();
                lblNotificacion.Text = "Se ha eliminado la categoría correctamente.";
                lblNotificacion.ForeColor = System.Drawing.Color.Green;
                lblNotificacion.Visible = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
