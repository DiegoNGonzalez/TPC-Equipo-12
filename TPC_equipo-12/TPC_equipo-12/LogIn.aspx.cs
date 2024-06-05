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
                        profesor=usuarioNegocio.SetearProfesor(usuario.IDUsuario);
                        profesor.Cursos = listaCursos;
                        Session["profesor"] = profesor;
                        Response.Redirect("DefaultProfesor.aspx", false);
                    }
                    else
                    {
                        
                        Session["usuario"] = usuario;
                        Response.Redirect("DefaultEstudiante.aspx", false);
                    }
                }
                else
                {
                    Session["error"] = "Email o Contraseña incorrectos. Reingrese por favor.";
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