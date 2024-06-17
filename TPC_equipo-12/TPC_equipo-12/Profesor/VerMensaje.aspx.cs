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

                MensajeUsuario mensaje = (MensajeUsuario)Session["mensaje"];
                int idMensaje = mensaje.IDMensaje;
                // 2. Cargar las respuestas
                List<MensajeRespuesta> respuestas = mensajeUsuarioNegocio.ObtenerRespuestas(idMensaje);

                // 3. Construir el HTML para las respuestas
                string htmlRespuestas = "";
                foreach (MensajeRespuesta respuesta in respuestas)
                {
                    // Aquí personaliza el formato HTML como desees
                    htmlRespuestas += $"<div class='respuesta'>";
                    htmlRespuestas += $"<b>{respuesta.UsuarioEmisor.Nombre} {respuesta.UsuarioEmisor.Apellido} ({respuesta.FechaHora}):</b><br/>";
                    htmlRespuestas += $"{respuesta.Texto}<br/>";
                    htmlRespuestas += "</div><hr/>";
                }

                // Asignar el HTML al Literal
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
            Profesor profesor = (Profesor)Session["profesor"];
            MensajeUsuarioNegocio mensajeNegocio = new MensajeUsuarioNegocio();
            if (!ValidarCampos())
            {
                Session["MensajeError"] = "Debe completar todos los campos.";
                Response.Redirect("VerMensaje.aspx");            
            }
            else
            {
                mensaje.IDMensajeOriginal = aux.IDMensaje;
                mensaje.UsuarioEmisor = profesor;
                mensaje.Texto = txtRespuesta.Text;
                mensaje.FechaHora = DateTime.Now;
                mensajeNegocio.GuardarRespuesta(mensaje);
                Session["MensajeExito"] = "Mensaje enviado con éxito.";
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
    }
}