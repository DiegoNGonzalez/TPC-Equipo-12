using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI;

namespace TPC_equipo_12
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Master master = (Master)Page.Master;
                master.VerificarMensaje();

                ButtonErrorRegistro.Visible = false;
                if (Session["error"] != null)
                {
                    LabelErrorLogIn.Text = Session["error"].ToString();
                    LabelErrorRegistro.Text = "No sos estudiante? Registrate!";
                    ButtonErrorRegistro.Visible = true;
                    Session["error"] = null;
                }

                
            }
        }
        protected void ButtonErrorRegistro_Click(object sender, EventArgs e)
        {
            Response.Redirect("SignUp.aspx", true);
        }
        protected void ButtonLogIn_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario();
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            CursoNegocio cursoNegocio = new CursoNegocio();
            try
            {
                usuario.Email = InputEmailLogIn.Text;
                string Contrasenia = InputContraseñaLogIn.Text;
                if(usuarioNegocio.Logueo(usuario, Contrasenia))
                {
                    List<Curso> listaCursos = cursoNegocio.ListarCursos();
                    
                        if (usuario.EsProfesor)
                        {
                            Profesor profesor = new Profesor();
                            profesor = usuarioNegocio.SetearProfesor(usuario.IDUsuario);
                            profesor.Cursos = listaCursos;
                            Session["profesor"] = profesor;
                            Session["MensajeExito"] = "Bienvenido " + profesor.Nombre + " " + profesor.Apellido + "!";
                            Response.Redirect("/Profesor/DefaultProfesor.aspx", false);
                        }
                        else
                        {
                            Estudiante estudiante = new Estudiante();
                            estudiante = usuarioNegocio.SetearEstudiante(usuario.IDUsuario);
                            Session["estudiante"] = estudiante;
                            Session["MensajeExito"] = "Bienvenido " + estudiante.Nombre + " " + estudiante.Apellido + "!";
                            Response.Redirect("/Estudiante/DefaultEstudiante.aspx", false);
                        }
                    

                }
                else
                {
                    Session.Add("error", "Email o Contraseña incorrectos. Reingrese por favor.");
                    Response.Redirect("LogIn.aspx", false);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                Response.Redirect("LogIn.aspx");
            }
        }

    }
}