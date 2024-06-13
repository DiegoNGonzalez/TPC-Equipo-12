using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace TPC_equipo_12
{
    public partial class ProfesorMasterPage : System.Web.UI.MasterPage
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["profesor"] == null)
            {
                Session.Add("error", "Unicamente el profesor puede acceder a esta pestaña.");
                Response.Redirect("Error.aspx");
            }
            
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session["profesor"] = null;
            Response.Redirect("../Default.aspx");
        }
        public void CargarNotificaciones(int userID)
        {
            NotificacionNegocio notificacionNegocio = new NotificacionNegocio();
            List<Notificacion> notificaciones = notificacionNegocio.listarNotificaciones(userID);

            if (notificaciones != null && notificaciones.Count > 0)
            {
                notificationCount.InnerText = notificaciones.Count.ToString();
                foreach (var notificacion in notificaciones)
                {
                    string urlRedireccion = "";
                    if (notificacion.Tipo == "Inscripcion")
                    {
                        urlRedireccion = $"DefaultProfesor.aspx?accion=redirigir&id={notificacion.IDNotificacion}&tipo=Inscripcion";
                    }
                    else
                    {
                        urlRedireccion = $"DefaultProfesor.aspx?accion=redirigir&id={notificacion.IDNotificacion}&tipo=Mensaje&idMensaje={notificacion.Mensaje.IDMensaje}";
                    }

                    notificationList.InnerHtml += $"<a class='dropdown-item' href='{urlRedireccion}'>{notificacion.MensajeNotificacion} - {notificacion.Fecha.ToString("dd/MM/yyyy")}</a>";
                }
            }
            else
            {
                notificationList.InnerHtml += "<a class='dropdown-item' href='#'>No hay notificaciones</a>";
            }
        }


    }
}