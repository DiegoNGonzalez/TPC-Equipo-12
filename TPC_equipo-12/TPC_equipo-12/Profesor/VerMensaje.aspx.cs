using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI;

namespace TPC_equipo_12
{
    public partial class VerMensaje : System.Web.UI.Page
    {
        public MensajeUsuarioNegocio mensajeUsuarioNegocio = new MensajeUsuarioNegocio();
        public NotificacionNegocio notificacionNegocio = new NotificacionNegocio();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["profesor"] == null)
            {
                Session["MensajeError"] = "No puede acceder a esa pestaña sin ser profesor.";
                Response.Redirect("../LogIn.aspx");
            }
            if (!IsPostBack)
            {
                ProfesorMasterPage master = (ProfesorMasterPage)Page.Master;
                master.VerificarMensaje();
                int idMensaje = Convert.ToInt32(Request.QueryString["idMensaje"]);
                

                if (idMensaje != 0)
                {
                    MensajeUsuario mensaje= mensajeUsuarioNegocio.BuscarMensaje(idMensaje);
                    Session.Add("mensaje", mensaje);

                    lblAsunto.Text = $"<b>Asunto: {mensaje.Asunto}</b><br/>";
                    lblDe.Text = $"<b>{mensaje.UsuarioEmisor.Nombre} {mensaje.UsuarioEmisor.Apellido} ({mensaje.FechaHora.ToString()})</b><br/>";
                    lblMensaje.Text = $"<b>Mensaje: </b>{mensaje.Mensaje}<br/>";

                }
                else
                {
                    MensajeUsuario mensaje = (MensajeUsuario)Session["mensaje"];
                    idMensaje = mensaje.IDMensaje;

                    lblAsunto.Text = $"<b>Asunto: {mensaje.Asunto}</b><br/>";
                    lblDe.Text = $"<b>{mensaje.UsuarioEmisor.Nombre} {mensaje.UsuarioEmisor.Apellido} ({mensaje.FechaHora.ToString()})</b><br/>";
                    lblMensaje.Text = $"<b>Mensaje: </b>{mensaje.Mensaje}<br/>";
                }
               
                
                List<MensajeRespuesta> respuestas = mensajeUsuarioNegocio.ObtenerRespuestas(idMensaje);

                string htmlRespuestas = "";
                foreach (MensajeRespuesta respuesta in respuestas)
                {
                    
                    htmlRespuestas += $"<div class='respuesta'>";
                    htmlRespuestas += $"<b>{respuesta.UsuarioEmisor.Nombre} {respuesta.UsuarioEmisor.Apellido} ({respuesta.FechaHora}):</b><br/>";
                    htmlRespuestas += $"{respuesta.Texto}<br/>";
                    htmlRespuestas += "</div>";
                }

                
                ltlRespuestas.Text = htmlRespuestas;


                




            }
        }

        protected void btnResponder_Click(object sender, EventArgs e)
        {
            pnlResponder.Visible = true;
            this.btnResponder.Visible = false;
        }

        protected void btnEnviarRespuesta_Click(object sender, EventArgs e)
        {
            int idRespuesta= Request.QueryString["idRespuesta"] != null ? Convert.ToInt32(Request.QueryString["idRespuesta"]) : 0;
            MensajeRespuesta mensajeRespuesta = mensajeUsuarioNegocio.buscarRespuesta(idRespuesta);
            MensajeRespuesta mensaje = new MensajeRespuesta();
            MensajeUsuario aux = (MensajeUsuario)Session["mensaje"];
            Profesor profesor = (Profesor)Session["Profesor"];
           
            if(profesor.IDUsuario== aux.UsuarioEmisor.IDUsuario)
            {
                aux.UsuarioEmisor = aux.UsuarioReceptor;
            }

            if (ValidarCampos())
            {
                mensaje.IDMensajeOriginal = aux.IDMensaje;
                mensaje.UsuarioEmisor = profesor;
                mensaje.UsuarioReceptor = aux.UsuarioEmisor;
                mensaje.Texto = txtRespuesta.Text;
                mensaje.FechaHora = DateTime.Now;
                mensajeUsuarioNegocio.GuardarRespuesta(mensaje);
                int id = mensajeUsuarioNegocio.UltimoIDRespuesta();
                mensaje.IDRespuesta = id;
                notificacionNegocio.AgregarNotificacionXRespuesta(mensaje);
                Session["MensajeExito"] = "Mensaje enviado con éxito.";
                Response.Redirect("ProfesorMensajes.aspx");
            }
            else
            {
                Session["MensajeError"] = "Debe completar todos los campos.";
                Response.Redirect("ProfesorMensajes.aspx");
            }

        }
        protected bool ValidarCampos()
        {
            if (txtRespuesta.Text == "")
            {
                Session["MensajeError"] = "Debe completar todos los campos.";
                return false;
            }
            return true;
        }
        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProfesorMensajes.aspx", false);
        }
    }
}