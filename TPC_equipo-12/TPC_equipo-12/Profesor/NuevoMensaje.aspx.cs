using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;


namespace TPC_equipo_12
{
    public partial class NuevoMensaje : System.Web.UI.Page
    {
        public List<Usuario> usuarios = new List<Usuario>();
        public UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
        public MensajeUsuarioNegocio mensajeNegocio = new MensajeUsuarioNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["profesor"] == null)
            {
                Session["MensajeError"] = "No puede acceder a esa pestaña sin ser profesor.";
                Response.Redirect("../LogIn.aspx");
            }
            if (!IsPostBack)
            {
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
                ddlDestinatario.DataSource = usuarios;
                ddlDestinatario.DataTextField ="NombreCompleto";
                ddlDestinatario.DataValueField = "IDUsuario";
                ddlDestinatario.DataBind();
            }
        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            MensajeUsuario mensaje = new MensajeUsuario();
            Profesor profesor = (Profesor)Session["profesor"];
            mensaje.UsuarioEmisor = profesor;
            mensaje.UsuarioReceptor = usuarioNegocio.buscarUsuario(Convert.ToInt32(ddlDestinatario.SelectedValue));
            mensaje.Asunto = txtAsunto.Text;
            mensaje.Mensaje = txtMensaje.Text;
            mensaje.FechaHora = DateTime.Now;
            mensajeNegocio.EnviarMensaje(mensaje);
            Session["MensajeExito"] = "Mensaje enviado con éxito.";
            Response.Redirect("ProfesorMensajes.aspx");


        }
    }
}