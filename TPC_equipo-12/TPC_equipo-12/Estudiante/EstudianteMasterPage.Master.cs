using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI;

namespace TPC_equipo_12
{
    public partial class EstudianteMasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Estudiante estudiante = (Estudiante)Session["estudiante"];

                if (Request.QueryString["accion"] == "redirigir")
                {
                    MarcarNotificacionComoLeida();
                    RedirigirSegunTipo();
                }
                CargarNotificaciones(estudiante.IDUsuario);

                if (estudiante != null && estudiante.ImagenPerfil.URL != null)
                {
                    lblNombreEstudiante.Text = estudiante.Nombre;
                    imgPerfil.ImageUrl = "~/Images/" + estudiante.ImagenPerfil.URL;
                }
                else
                {
                    lblNombreEstudiante.Text = estudiante.Nombre;
                    imgPerfil.ImageUrl = "https://static.vecteezy.com/system/resources/thumbnails/008/442/086/small/illustration-of-human-icon-user-symbol-icon-modern-design-on-blank-background-free-vector.jpg";
                }
            }
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session["estudiante"] = null;
            Response.Redirect("../Default.aspx");
        }
        public void CargarNotificaciones(int userID)
        {
            NotificacionNegocio notificacionNegocio = new NotificacionNegocio();
            List<Notificacion> notificaciones = notificacionNegocio.listarNotificaciones(userID, "Usuario");

            if (notificaciones != null && notificaciones.Count > 0)
            {
                notificationCount.InnerText = notificaciones.Count.ToString();
                foreach (var notificacion in notificaciones)
                {
                    string urlRedireccion = "";
                    if (notificacion.Tipo == "Inscripcion")
                    {
                        urlRedireccion = $"DefaultEstudiante.aspx?accion=redirigir&id={notificacion.IDNotificacion}&tipo=Inscripcion";
                    }
                    else if (notificacion.Tipo == "Mensaje")
                    {
                        urlRedireccion = $"DefaultEstudiante.aspx?accion=redirigir&id={notificacion.IDNotificacion}&tipo=Mensaje&idMensaje={notificacion.Mensaje.IDMensaje}";
                    }
                    else if(notificacion.Tipo=="Respuesta")
                    {
                        urlRedireccion = $"DefaultEstudiante.aspx?accion=redirigir&id={notificacion.IDNotificacion}&tipo=Mensaje&idMensaje={notificacion.MensajeRespuesta.IDRespuesta}";
                    }else if (notificacion.Tipo == "Comentario")
                    {
                        ComentarioNegocio comentarioNegocio = new ComentarioNegocio();
                        notificacion.ComentarioLeccion = comentarioNegocio.buscarComentario(notificacion.ComentarioLeccion.IDComentario);

                        urlRedireccion = $"DefaultEstudiante.aspx?accion=redirigir&id={notificacion.IDNotificacion}&tipo=Comentario&idLeccion={notificacion.ComentarioLeccion.Leccion.IDLeccion}";
                    }else if (notificacion.Tipo == "Deshabilitado")
                    {
                        urlRedireccion = $"DefaultEstudiante.aspx?accion=redirigir&id={notificacion.IDNotificacion}&tipo=Deshabilitado";
                    }


                    notificationList.InnerHtml += $"<a class='dropdown-item' href='{urlRedireccion}'>{notificacion.MensajeNotificacion} - {notificacion.Fecha.ToString("dd/MM/yyyy")}</a>";
                }
            }
            else
            {
                notificationList.InnerHtml += "<a class='dropdown-item' href='#'>No hay notificaciones</a>";
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
                Response.Redirect("EstudianteInscripciones.aspx");
            }
            else if (tipo == "Mensaje")
            {
                int idMensaje = Convert.ToInt32(Request.QueryString["idMensaje"]);
                Response.Redirect($"EstudianteMensajes.aspx");
            }else if(tipo == "Deshabilitado")
            {
                Response.Redirect("EstudianteCursos.aspx");
            }
            else
            {
                int idRespuesta= Convert.ToInt32(Request.QueryString["idRespuesta"]);
                Response.Redirect("EstudianteMensajes.aspx");
            }
        }
        public void VerificarMensaje()
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
        }

    }
}
