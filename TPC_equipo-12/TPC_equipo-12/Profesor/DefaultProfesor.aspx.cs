using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;
using System.Web.UI.HtmlControls;

namespace TPC_equipo_12
{
    public partial class DefaultProfesor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["profesor"] == null)
            {
                Session.Add("error", "Unicamente el profesor puede acceder a esta pestaña.");
                Response.Redirect("../Error.aspx");
            }
            if (!IsPostBack)
            {
                Profesor profesor = (Profesor)Session["profesor"];
                ProfesorMasterPage master = (ProfesorMasterPage)this.Master;
                master.CargarNotificaciones(profesor.IDUsuario);

                if (Request.QueryString["accion"] == "redirigir")
                {
                    MarcarNotificacionComoLeida();
                    RedirigirSegunTipo();
                }
            }
        }
        private void MarcarNotificacionComoLeida()
        {
            int idNotificacion = Convert.ToInt32(Request.QueryString["id"]);
            NotificacionNegocio notificacionNegocio = new NotificacionNegocio();
            notificacionNegocio.MarcarComoLeida(idNotificacion);
        }

        private void RedirigirSegunTipo()
        {
            string tipo = Request.QueryString["tipo"];
            if (tipo == "Inscripcion")
            {
                Response.Redirect("Inscripciones.aspx");
            }
            else if (tipo == "Mensaje")
            {
                int idMensaje = Convert.ToInt32(Request.QueryString["idMensaje"]);
                Response.Redirect($"MensajeDetalle.aspx?id={idMensaje}");
            }
        }

    }
}