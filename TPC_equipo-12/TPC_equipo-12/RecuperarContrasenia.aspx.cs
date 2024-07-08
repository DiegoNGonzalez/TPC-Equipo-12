using AccesoDB;
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
    public partial class RecuperarContrasenia : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {

            ReinicioContraseniaNegocio seguridad = new ReinicioContraseniaNegocio();
            ReinicioContrasenia reinicioContrasenia = new ReinicioContrasenia();
            EmailService emailService = new EmailService();
            string email = txtEmail.Text.Trim();
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            try
            {
                if (!ValidarFormulario())
                {
                    return;
                }
                reinicioContrasenia.IDUsuario = usuarioNegocio.ObtenerIDporEmail(email);
                if (reinicioContrasenia.IDUsuario != 0)
                {
                    string token = Guid.NewGuid().ToString();
                    reinicioContrasenia.Token = token;
                    seguridad.GuardarTokenEnBD(reinicioContrasenia);
                    emailService.EnviarEmailConToken(email, token);
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "info", "<script>showMessage('Revise su correo!!', 'info');</script>", false);
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    Session["MensajeError"] = "No existe el usuario!";
                    Response.Redirect("Login.aspx");
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                Response.Redirect("LogIn.aspx");
            }
            


        }

        protected bool ValidarFormulario()
        {
            if (txtEmail.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "info", "<script>showMessage('Faltan campos por completar!', 'info');</script>", false);
                return false;
            }

            return true;
        }

    }
}