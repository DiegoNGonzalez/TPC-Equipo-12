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

                ButtonErrorRegistro.Visible = false;
                if (Session["error"] != null)
                {
                    LabelErrorLogIn.Text = Session["error"].ToString();
                    LabelErrorRegistro.Text = "No sos estudiante? Registrate!";
                    ButtonErrorRegistro.Visible = true;
                    Session["error"] = null;
                }

                if (Session["MensajeExito"] != null)
                {
                    string msj = Session["MensajeExito"].ToString();
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Success", $@"showMessage('{msj}', 'success');", true);
                    Session["MensajeExito"] = null;
                }
                if (Session["MensajeError"] != null)
                {
                    string msj = Session["MensajeError"].ToString();
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Error", $@"showMessage('{msj}', 'error');", true);
                    Session["MensajeError"] = null;
                }
                if (Session["MensajeInfo"] != null)
                {
                    string msj = Session["MensajeInfo"].ToString();
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Info", $@"showMessage('{msj}', 'info');", true);
                    Session["MensajeInfo"] = null;
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
            try
            {
                usuario.Email = InputEmailLogIn.Text;
                usuario.Contrasenia = InputContraseñaLogIn.Text;
                usuarioNegocio.Logueo(usuario);
                List<Curso> listaCursos = new List<Curso>();
                listaCursos = (List<Curso>)Session["listaCursos"];
                if (usuario.IDUsuario != 0)
                {
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

        protected void ButtonLogInFastProf_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario();
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            try
            {
                usuario.Email = "hola@maxiprograma.com";
                usuario.Contrasenia = "contraseniaSegura123";
                usuarioNegocio.Logueo(usuario);
                List<Curso> listaCursos = new List<Curso>();
                listaCursos = (List<Curso>)Session["listaCursos"];
                Profesor profesor = new Profesor();
                profesor = usuarioNegocio.SetearProfesor(usuario.IDUsuario);
                profesor.Cursos = listaCursos;
                Session["profesor"] = profesor;
                Response.Redirect("~/Profesor/DefaultProfesor.aspx", false);
            }
            catch (Exception ex)
            {
                ex.ToString();
                Response.Redirect("LogIn.aspx");
            }
        }

        protected void ButtonLogInFastEst_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario();
            UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
            try
            {
                usuario.Email = "tomas.caceres2@alumnos.frgp.utn.edu.ar";
                usuario.Contrasenia = "contraseniaSegura789";
                usuarioNegocio.Logueo(usuario);
                List<Curso> listaCursos = new List<Curso>();
                listaCursos = (List<Curso>)Session["listaCursos"];
                Estudiante estudiante = new Estudiante();
                estudiante = usuarioNegocio.SetearEstudiante(usuario.IDUsuario);
                Session["estudiante"] = estudiante;
                Response.Redirect("~/Estudiante/DefaultEstudiante.aspx", false);
            }
            catch (Exception ex)
            {
                ex.ToString();
                Response.Redirect("LogIn.aspx");
            }
        }

    }
}