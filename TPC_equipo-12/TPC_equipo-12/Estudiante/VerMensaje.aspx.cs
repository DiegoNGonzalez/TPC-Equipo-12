using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI;

namespace TPC_equipo_12
{
    public partial class VerMensaje1 : System.Web.UI.Page
    {
        public MensajeUsuarioNegocio mensajeUsuarioNegocio = new MensajeUsuarioNegocio();
        public NotificacionNegocio notificacionNegocio = new NotificacionNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["estudiante"] == null)
            {
                Session["MensajeError"] = "No puede acceder a esa pestaña sin ser estudiante.";
                Response.Redirect("../LogIn.aspx");
            }
            if (!IsPostBack)
            {
                EstudianteMasterPage master = (EstudianteMasterPage)Page.Master;
                master.VerificarMensaje();

                MensajeUsuario mensaje = (MensajeUsuario)Session["mensaje"];
                int idMensaje = mensaje.IDMensaje;
                List<MensajeRespuesta> respuestas = mensajeUsuarioNegocio.ObtenerRespuestas(idMensaje);

                
                string htmlRespuestas = "";
                foreach (MensajeRespuesta respuesta in respuestas)
                {
                    
                    htmlRespuestas += $"<div class='respuesta'>";
                    htmlRespuestas += $"<b>{respuesta.UsuarioEmisor.Nombre} {respuesta.UsuarioEmisor.Apellido} ({respuesta.FechaHora}):</b><br/>";
                    htmlRespuestas += $"{respuesta.Texto}<br/>";
                    htmlRespuestas += "</div><hr/>";
                }

                
                ltlRespuestas.Text = htmlRespuestas;

                
                lblAsunto.Text = mensaje.Asunto;
                lblFecha.Text = mensaje.FechaHora.ToString();
                lblDe.Text = mensaje.UsuarioEmisor.Nombre + " " + mensaje.UsuarioEmisor.Apellido;
                lblMensaje.Text = mensaje.Mensaje;




            }
        }
        protected void btnResponder_Click(object sender, EventArgs e)
        {
            pnlResponder.Visible = true;
            this.btnResponder.Visible = false;
        }

        protected void btnEnviarRespuesta_Click(object sender, EventArgs e)
        {
            MensajeRespuesta mensaje = new MensajeRespuesta();
            MensajeUsuario aux = (MensajeUsuario)Session["mensaje"];
            Estudiante estudiante = (Estudiante)Session["estudiante"];
            MensajeUsuarioNegocio mensajeNegocio = new MensajeUsuarioNegocio();
            if (ValidarCampos())
            {
                mensaje.IDMensajeOriginal = aux.IDMensaje;
                mensaje.UsuarioEmisor = estudiante;
                mensaje.UsuarioReceptor = aux.UsuarioEmisor;
                mensaje.Texto = txtRespuesta.Text;
                mensaje.FechaHora = DateTime.Now;
                mensajeNegocio.GuardarRespuesta(mensaje);
                int id = mensajeNegocio.UltimoIDRespuesta();
                mensaje.IDRespuesta = id;
                notificacionNegocio.AgregarNotificacionXRespuesta(mensaje);
                Session["MensajeExito"] = "Mensaje enviado con éxito.";
                Response.Redirect("EstudianteMensajes.aspx");
            }
            else
            {
                Session["MensajeError"] = "Debe completar todos los campos.";
                Response.Redirect("EstudianteMensajes.aspx");
            }
           
        }
        private bool ValidarCampos()
        {
            if (txtRespuesta.Text == "")
            {
                return false;
            }
            return true;
        }
    }
}