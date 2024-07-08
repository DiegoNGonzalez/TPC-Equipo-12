using AccesoDB;
using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_equipo_12
{
    public partial class EstudiantePreguntas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ComentarioNegocio comentarioNegocio = new ComentarioNegocio();
            Comentario comentarioPrincipal = new Comentario();
            List<Comentario> respuestas = new List<Comentario>();
            if (!IsPostBack)
            {
                if (Session["estudiante"] == null)
                {
                    Session["MensajeError"] = "No puede acceder a esa pestaña sin ser un estudiante.";
                    Response.Redirect("../LogIn.aspx");
                }
                if (Session["IDComentarioPadre"] != null)
                {
                    int idComentarioPadre = Convert.ToInt32(Session["IDComentarioPadre"]);
                    comentarioPrincipal = comentarioNegocio.CargarComentarioPrincipal(idComentarioPadre);
                    if (comentarioPrincipal.UsuarioEmisor.ImagenPerfil.IDImagen != 0)
                        imgPerfilPadre.ImageUrl = "~/Images/perfil-" + comentarioPrincipal.UsuarioEmisor.IDUsuario.ToString() + ".jpg";
                    else
                        imgPerfilPadre.ImageUrl = "~/Images/perfil-0.jpg";

                    lblNombreYApellido.Text = comentarioPrincipal.UsuarioEmisor.Nombre +" "+ comentarioPrincipal.UsuarioEmisor.Apellido + ":";
                    lblCuerpoComentario.Text = comentarioPrincipal.CuerpoComentario;
                    lblFechaCreacion.Text = comentarioPrincipal.FechaCreacion.ToString("dd/MM/yyyy HH:mm");
                    respuestas = comentarioNegocio.cargarRespuestas(idComentarioPadre);

                    rptRespuestas.DataSource = respuestas;
                    rptRespuestas.DataBind();
                }
            }
        }

    
        protected void btnResponder_Click(object sender, EventArgs e)
        {
            if (Session["IDComentarioPadre"] != null)
            {
                int idComentarioPadre = Convert.ToInt32(Session["IDComentarioPadre"]);
                ComentarioNegocio comentarioNegocio = new ComentarioNegocio();
                int idLeccion = comentarioNegocio.BuscarIDLeccion(idComentarioPadre);
                Usuario usuarioActual = Session["estudiante"] != null ? (Usuario)Session["estudiante"] : (Usuario)Session["profesor"];
                string cuerpoRespuesta = txtRespuesta.Text;
                if (!string.IsNullOrEmpty(cuerpoRespuesta))
                {
                    comentarioNegocio.publicarComentario(idComentarioPadre, idLeccion, cuerpoRespuesta, usuarioActual.IDUsuario, DateTime.Now);
                    List<Comentario> respuestas = new List<Comentario>();
                    respuestas = comentarioNegocio.cargarRespuestas(idComentarioPadre);
                    rptRespuestas.DataSource = respuestas;
                    rptRespuestas.DataBind();
                    txtRespuesta.Text = "";
                }
                else
                {
                    return;
                }
            }
        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("EstudianteMateriales.aspx", false);
        }


    }
}