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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["estudiante"] == null)
            {
                Session["MensajeError"] = "No puede acceder a esa pestaña sin ser estudiante.";
                Response.Redirect("../LogIn.aspx");
            }
            if (!IsPostBack)
            {
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
                Estudiante estudiante = (Estudiante)Session["estudiante"];
                mensajes = mensajeUsuarioNegocio.listarMensajes("recibidos",estudiante.IDUsuario);
                if (mensajes.Count == 0)
                {
                    PnlMensaje.Visible = false;
                    LabelNoHayMensajes.Visible = true;
                }else
                {
                Session.Add("mensajes", mensajes);
                rptMensajes.DataSource = mensajes;
                rptMensajes.DataBind();
                }
            }
        }



        protected void btnVerMensaje_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int idMensaje = Convert.ToInt32(btn.CommandArgument);
            MensajeUsuario mensaje = mensajeUsuarioNegocio.BuscarMensaje(idMensaje);
            //mensajeUsuarioNegocio.MarcarComoLeido(mensaje);
            Session.Add("mensaje", mensaje);

            Response.Redirect("VerMensaje.aspx");
        }

        protected void btnVerMensaje_Command(object sender, CommandEventArgs e)
        {
            Button btn = (Button)sender;
            int idMensaje = Convert.ToInt32(btn.CommandArgument);
            MensajeUsuario mensaje = mensajeUsuarioNegocio.BuscarMensaje(idMensaje);
            //mensajeUsuarioNegocio.MarcarComoLeido(mensaje);
            Session.Add("mensaje", mensaje);
        }
    }
}
