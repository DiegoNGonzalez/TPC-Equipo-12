using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_equipo_12
{
    public partial class EstudianteCambiarContrasenia : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["estudiante"] == null)
            {
                Session["MensajeError"] = "No puede acceder a esa pestaña sin ser estudiante.";
                Response.Redirect("../LogIn.aspx");
            }
        }

        protected void btnActualizarContrasena_Click(object sender, EventArgs e)
        {
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            Usuario usuario = new Usuario();
            usuario = (Usuario)Session["estudiante"];
            string contraseniaActual = txtContraseniaActual.Text;
            string contraseniaNueva = txtNuevaContrasenia.Text;
            if (!ValidarFormulario())
            {
                return;
            }
            if (usuarioNegocio.Logueo(usuario, contraseniaActual))
            {
                if (usuarioNegocio.CambiarContraseñaEnBaseDeDatos(usuario.IDUsuario, contraseniaNueva))
                {

                    Session["MensajeExito"] = "¡Contraseña cambiada con éxito!";
                    Response.Redirect("DefaultEstudiante.aspx");
                }
                else
                {
                    Session["MensajeError"] = "Error al cambiar la contraseña!";
                    Response.Redirect("DefaultEstudiante.aspx");
                }
            }
            else
            {
                lblError.Visible = true;
                return;
            }


        }
        protected bool ValidarFormulario()
        {
            if (txtContraseniaActual.Text == "" || txtNuevaContrasenia.Text == "" || txtConfirmarContrasenia.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "info", "<script>showMessage('Faltan campos por completar!', 'info');</script>", false);
                return false;
            }

            return true;
        }
    }
}
