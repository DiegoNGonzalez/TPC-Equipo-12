using AccesoDB;
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
            Seguridad seguridad = new Seguridad();
            EmailService emailService = new EmailService();
            string email = txtEmail.Text.Trim();
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            int IDUsuario = usuarioNegocio.ObtenerIDporEmail(email);
            if (IDUsuario != 0)
            {
                string token = Guid.NewGuid().ToString();
                seguridad.GuardarTokenEnBD(token, IDUsuario);
                emailService.EnviarEmailConToken(email, token);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "info", "<script>showMessage('Revise su correo!!', 'info');</script>", false);
            }
            else
            {
                Session["MensajeError"] = "No existe el usuario!";
                Response.Redirect("Login.aspx");
            }


        }
    
    }
}