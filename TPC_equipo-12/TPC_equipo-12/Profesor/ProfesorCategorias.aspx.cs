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
                        lblNotificacion.Text = "Ya existe una Categoria con ese Nombre, ingrese otro por favor..";
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
    }
}
