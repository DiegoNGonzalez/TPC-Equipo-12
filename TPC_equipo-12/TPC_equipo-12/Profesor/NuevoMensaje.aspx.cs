using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI;


namespace TPC_equipo_12
{
    public partial class NuevoMensaje : System.Web.UI.Page
    {
        public List<Usuario> usuarios = new List<Usuario>();
        public UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
        public MensajeUsuarioNegocio mensajeNegocio = new MensajeUsuarioNegocio();
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

                Profesor profesor = (Profesor)Session["profesor"];
                usuarios = usuarioNegocio.ListarUsuarios();
                foreach (Usuario usuario in usuarios)
                {
                    if (usuario.IDUsuario == profesor.IDUsuario)
                    {
                        usuarios.Remove(usuario);
                        break;
                    }
                }
                Session.Add("usuarios", usuarios);
                if (usuarios.Count == 0)
                {
                    Session["MensajeError"] = "No hay usuarios disponibles para enviar mensajes.";
                    Response.Redirect("ProfesorMensajes.aspx");
                }
                else
                {

                    ddlDestinatario.DataSource = usuarios;
                    ddlDestinatario.DataTextField = "NombreCompleto";
                    ddlDestinatario.DataValueField = "IDUsuario";
                    ddlDestinatario.DataBind();
                }
            }
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            MensajeUsuario mensaje = new MensajeUsuario();
            Profesor profesor = (Profesor)Session["profesor"];
            if (!ValidarCampos())
            {
                Session["MensajeError"] = "Debe completar todos los campos.";
                Response.Redirect("NuevoMensaje.aspx");
            }
            else
            {
                mensaje.UsuarioEmisor = profesor;
                mensaje.UsuarioReceptor = usuarioNegocio.buscarUsuario(Convert.ToInt32(ddlDestinatario.SelectedValue));
                mensaje.Asunto = txtAsunto.Text;
                mensaje.Mensaje = txtMensaje.Text;
                mensaje.FechaHora = DateTime.Now;
                mensajeNegocio.EnviarMensaje(mensaje);
                int id = mensajeNegocio.UltimoIDMensaje();
                mensaje.IDMensaje = id;
                notificacionNegocio.AgregarNotificacionXMensaje(mensaje);
                Session["MensajeExito"] = "Mensaje enviado con éxito.";
                Response.Redirect("ProfesorMensajes.aspx");

            }


        }
        protected bool ValidarCampos()
        {
            if (txtMensaje.Text == "")
            {
                Session["MensajeError"] = "Debe completar todos los campos.";
                return false;
            }
            return true;
        }
    }
}