using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TPC_equipo_12
{
    public partial class EstudianteMensajes : System.Web.UI.Page
    {
        public List<MensajeUsuario> mensajes = new List<MensajeUsuario>();
        public MensajeUsuarioNegocio mensajeUsuarioNegocio = new MensajeUsuarioNegocio();
        public List<MensajeUsuario> mensajesEnviados = new List<MensajeUsuario>();
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

                Estudiante estudiante = (Estudiante)Session["estudiante"];
                mensajes = mensajeUsuarioNegocio.listarMensajes("recibidos", estudiante.IDUsuario);
                if (mensajes.Count == 0)
                {
                    PnlMensaje.Visible = false;
                    LabelNoHayMensajes.Visible = true;
                }
                else
                {
                    Session.Add("mensajes", mensajes);
                    rptMensajes.DataSource = mensajes;
                    rptMensajes.DataBind();
                }
                mensajesEnviados = mensajeUsuarioNegocio.listarMensajes("enviados", estudiante.IDUsuario);
                if (mensajesEnviados.Count == 0)
                {
                    PanelMensajesEnviados.Visible = false;
                    LabelNoHayMensajesEnviados.Visible = true;
                }
                else
                {
                    Session.Add("mensajesEnviados", mensajesEnviados);
                    rptMensajesEnviados.DataSource = mensajesEnviados;
                    rptMensajesEnviados.DataBind();
                }
            }
        }



        protected void btnVerMensaje_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int idMensaje = Convert.ToInt32(btn.CommandArgument);
            MensajeUsuario mensaje = mensajeUsuarioNegocio.BuscarMensaje(idMensaje);
            mensajeUsuarioNegocio.MarcarComoLeido(mensaje.IDMensaje);
            Session.Add("mensaje", mensaje);

            Response.Redirect("VerMensaje.aspx");
        }

        protected void btnVerMensaje_Command(object sender, CommandEventArgs e)
        {
            Button btn = (Button)sender;
            int idMensaje = Convert.ToInt32(btn.CommandArgument);
            MensajeUsuario mensaje = mensajeUsuarioNegocio.BuscarMensaje(idMensaje);
            mensajeUsuarioNegocio.MarcarComoLeido(mensaje.IDMensaje);
            Session.Add("mensaje", mensaje);
        }

        protected void btnNuevoMensaje_Click(object sender, EventArgs e)
        {
            Response.Redirect("NuevoMensaje.aspx");
        }
        protected void btnVerMensajeEnviado_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int idMensaje = Convert.ToInt32(btn.CommandArgument);
            MensajeUsuario mensaje = mensajeUsuarioNegocio.BuscarMensaje(idMensaje);
            mensajeUsuarioNegocio.MarcarComoLeido(mensaje.IDMensaje);
            Session.Add("mensaje", mensaje);
            Response.Redirect("VerMensaje.aspx");
        }

        protected void btnVerMensajeEnviado_Command(object sender, CommandEventArgs e)
        {
            Button btn = (Button)sender;
            int idMensaje = Convert.ToInt32(btn.CommandArgument);
            MensajeUsuario mensaje = mensajeUsuarioNegocio.BuscarMensaje(idMensaje);
            mensajeUsuarioNegocio.MarcarComoLeido(mensaje.IDMensaje);
            Session.Add("mensaje", mensaje);
        }

       

    }
}
