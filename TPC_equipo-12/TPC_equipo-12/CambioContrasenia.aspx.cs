using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_equipo_12
{
    public partial class CambioContraseña : System.Web.UI.Page
    {
        Seguridad seguridad = new Seguridad();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                if (Request.QueryString["token"] != null)
                {
                    string token = Request.QueryString["token"];

                    bool tokenValido = seguridad.ValidarToken(token);

                    if (!tokenValido)
                    {
                        Session["MensajeError"] = "Link invalido!";
                        Response.Redirect("LogIn.aspx");
                    }
                }
                else
                {
                    
                    Response.Redirect("LogIn.aspx");
                }
            }
        }

        protected void btnActualizarContraseña_Click(object sender, EventArgs e)
        {
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();   
            string token = Request.QueryString["token"];
            if (!ValidarFormulario())
            {
                return;
            }
            if (token != null && seguridad.ValidarToken(token))
            {
                string nuevaContrasenia = txtNuevaContraseña.Text.Trim();
                string confirmarContrasenia = txtConfirmarContraseña.Text.Trim();

                int IDusuario = seguridad.ObtenerIdUsuarioPorToken(token);
                if (IDusuario != 0)
                {
                    bool cambioExitoso = usuarioNegocio.CambiarContraseñaEnBaseDeDatos(IDusuario, nuevaContrasenia);
                    if (cambioExitoso)
                    {
                        
                        Session["MensajeExito"] = "¡Contraseña cambiada con éxito!";
                        Response.Redirect("Login.aspx");
                    }
                    else
                    {
                        Session["MensajeError"] = "Error al cambiar la contraseña!";
                        Response.Redirect("Login.aspx");
                    }
                }
                else
                {
                    Session["MensajeError"] = "Usuario no encontrado!";
                    Response.Redirect("Login.aspx");
                }
            }
            else
            {
                Session["MensajeError"] = "No es posible Ingresar!";
                Response.Redirect("Login.aspx");
            }

        }

        private bool ValidarFormulario()
        {
            if (txtNuevaContraseña.Text == "" || txtConfirmarContraseña.Text == "" )
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "info", "<script>showMessage('Faltan campos por completar!', 'info');</script>", false);
                return false;
            }

            return true;
        }
    }
}