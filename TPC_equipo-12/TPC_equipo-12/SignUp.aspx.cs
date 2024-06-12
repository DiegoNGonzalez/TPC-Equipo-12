using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;

namespace TPC_equipo_12
{
    public partial class WebForm1 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dropGenero.Items.Add("Masculino");
                dropGenero.Items.Add("Femenino");
                dropGenero.Items.Add("No binario");
                dropGenero.Items.Add("No contesta");
            }
        }
        protected void btnSignUp_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario();
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            try
            {
                if (!ValidarFormulario())
                {
                    return;
                }
                else
                {
                    usuario.Nombre = InputNombres.Text;
                    usuario.Apellido = InputApellidos.Text;
                    usuario.DNI = Convert.ToInt32(InputDNI.Text);
                    usuario.Email = InputEmail.Text;
                    usuario.Contrasenia = InputPassword.Text;
                    usuario.EsProfesor = false;
                    if (dropGenero.SelectedValue == "Masculino")
                    {
                        usuario.Genero = "M";
                    }
                    else if (dropGenero.SelectedValue == "Femenino")
                    {
                        usuario.Genero = "F";
                    }
                    else if (dropGenero.SelectedValue == "No binario")
                    {
                        usuario.Genero = "x";
                    }
                    usuarioNegocio.AgregarUsuario(usuario);
                    Session["usuario"] = usuario;
                    //ScriptManager.RegisterStartupScript(this, typeof(Page), "Succes", "<script>showMessage('Registro exitoso, redirigiendo a Login!', 'info');</script>", false);
                    //Response.Redirect("~/LogIn.aspx", false);
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Success", @"<script>
                        showMessage('Registro exitoso, redirigiendo a Login!', 'info');
                        setTimeout(function() {
                        window.location.href = 'LogIn.aspx'; 
                        }, 3000); 
                        </script>", false);
                }

               
            }
            catch (Exception ex)
            {
                //Session["error"] = ex.Message;
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Info", @"<script>
                        showMessage('Verifique su información, El usuario ya esta registrado!', 'info');
                        setTimeout(function() {
                        window.location.href = 'LogIn.aspx'; 
                        }, 4000); 
                        </script>", false);
                //Response.Redirect("~/Error.aspx", false);
            }
        }
        protected bool ValidarFormulario()
        {
            if (InputNombres.Text == "" || InputApellidos.Text == "" || InputDNI.Text == "" || InputEmail.Text == "" || InputPassword.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "info", "<script>showMessage('Falta rellenar algun campo!', 'info');</script>", false);
                return false;
            }

            return true;
        }
    }
}