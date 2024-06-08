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
                usuario.Nombre = InputNombres.Text;
                usuario.Apellido = InputApellidos.Text;
                usuario.DNI = Convert.ToInt32(InputDNI.Text);
                usuario.Email = InputEmail.Text;
                usuario.Contrasenia = InputPassword.Text;
                usuario.EsProfesor = false;
                if (dropGenero.SelectedValue == "Masculino")
                {
                    usuario.Genero = "M";
                }else if (dropGenero.SelectedValue == "Femenino")
                {
                    usuario.Genero = "F";
                }else if (dropGenero.SelectedValue == "No binario")
                {
                    usuario.Genero = "x";
                }
                usuarioNegocio.AgregarUsuario(usuario);
                Session["usuario"] = usuario;
                Response.Redirect("~/LogIn.aspx", false);
            }
            catch (Exception ex)
            {
                Session["error"] = ex.Message;
                Response.Redirect("~/LogIn.aspx", false);
            }
        }
    }
}