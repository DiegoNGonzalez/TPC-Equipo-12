using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI;

namespace TPC_equipo_12
{
    public partial class WebForm1 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Master master = (Master)Page.Master;
                master.VerificarMensaje();

                dropGenero.Items.Add("Femenino");
                dropGenero.Items.Add("Masculino");
                dropGenero.Items.Add("No binario");

                if(chkProfesor.Checked)
                {
                    LblLicencia.Visible = true;
                    InputLicencia.Visible = true;
                }
            }
        }
        protected void btnSignUp_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario();
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            Profesor profesor = new Profesor();
            ProfesorNegocio auxNegocioProfesor = new ProfesorNegocio();
            int Licencia, UltimoID;
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
                    string Contrasenia = InputPassword.Text;
                    if (InputLicencia.Text == "")
                    {
                        Licencia = 0;
                    }
                    else
                    {

                        Licencia = Convert.ToInt32(InputLicencia.Text);
                    }
                    if (auxNegocioProfesor.VerificarLicencia(Licencia))
                    {
                        usuario.EsProfesor = true;

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
                        usuarioNegocio.AgregarUsuario(usuario, Contrasenia);
                        UltimoID = usuarioNegocio.UltimoIdUsuario();
                        //profesor = usuarioNegocio.SetearProfesor(UltimoID);
                        auxNegocioProfesor.InsertarProfesor(UltimoID);
                        auxNegocioProfesor.InsertarCursosProfesorDEMO();
                        Session["usuario"] = usuario;
                        Session["MensajeExito"] = "Profesor registrado con éxito!";

                        Response.Redirect("LogIn.aspx", false);
                    }
                    else
                    {
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
                        usuarioNegocio.AgregarUsuario(usuario, Contrasenia);
                        Session["usuario"] = usuario;
                        Session["MensajeExito"] = "Usuario registrado con éxito!";

                        Response.Redirect("LogIn.aspx", false);
                    }
                }


            }
            catch (Exception ex)
            {
                Session["MensajeError"] = "El Email ingresado ya esta asociado a una cuenta!";
                Response.Redirect("SignUp.aspx", false);
            }
        }
        protected bool ValidarFormulario()
        {
            if (InputNombres.Text == "" || InputApellidos.Text == "" || InputDNI.Text == "" || InputEmail.Text == "" || InputPassword.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, typeof(Page), "info", "<script>showMessage('Faltan campos por completar!', 'info');</script>", false);
                return false;
            }

            return true;
        }

        protected void chkProfesor_CheckedChanged(object sender, EventArgs e)
        {
            bool Check = chkProfesor.Checked;
            if (Check)
            {
                LblLicencia.Visible = true;
                InputLicencia.Visible = true;
            }
            else
            {
                LblLicencia.Visible = false;
                InputLicencia.Visible = false;
            }
        }
    }
}