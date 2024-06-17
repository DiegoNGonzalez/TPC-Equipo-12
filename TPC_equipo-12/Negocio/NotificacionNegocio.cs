using AccesoDB;
using Dominio;
using System;
using System.Collections.Generic;

namespace Negocio
{
    public class NotificacionNegocio
    {
        private Datos datos;
        public NotificacionNegocio()
        {
            datos = new Datos();
        }

        public List<Notificacion> listarNotificaciones(int IDUsuario)
        {
            List<Notificacion> lista = new List<Notificacion>();
            try
            {
                datos.SetearConsulta("select n.IDNotificacion, n.Mensaje, n.Tipo, n.Fecha, n.Leido, n.IDInscripcion, n.IDMensaje, n.IDRespuesta, nxu.IDUsuario from Notificaciones n inner join NotificacionesXUsuario nxu on n.IDNotificacion= nxu.IDNotificacion WHERE nxu.IDUsuario = @IDUsuario and n.Leido=0");
                datos.SetearParametro("@IDUsuario", IDUsuario);
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    Notificacion aux = new Notificacion();
                    aux.IDNotificacion = (int)datos.Lector["IDNotificacion"];
                    aux.MensajeNotificacion = (string)datos.Lector["Mensaje"];
                    aux.Tipo = (string)datos.Lector["Tipo"];
                    aux.Fecha = (DateTime)datos.Lector["Fecha"];
                    aux.Estado = (bool)datos.Lector["Leido"];
                    if (aux.Tipo == "Inscripcion")
                    {
                        aux.Inscripcion = new InscripcionACurso();
                        aux.Inscripcion.IDInscripcion = (int)datos.Lector["IDInscripcion"];
                    }
                    else if (aux.Tipo == "Mensaje")
                    {
                        aux.Mensaje = new MensajeUsuario();
                        aux.Mensaje.IDMensaje = (int)datos.Lector["IDMensaje"];
                    }
                    else
                    {
                        aux.MensajeRespuesta = new MensajeRespuesta();
                        aux.MensajeRespuesta.IDRespuesta = (int)datos.Lector["IDRespuesta"];
                    }
                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                datos.LimpiarParametros();
                datos.CerrarConexion();
            }
        }
        public void MarcarComoLeida(int idNotificacion)
        {
            try
            {
                datos.SetearConsulta("UPDATE Notificaciones SET Leido = 1 WHERE IDNotificacion = @IDNotificacion");
                datos.SetearParametro("@IDNotificacion", idNotificacion);
                datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.LimpiarParametros();
                datos.CerrarConexion();
            }
        }
        public void AgregarNotificacionXInscripcion(int idInscripcion)
        {
            try
            {
                datos.SetearConsulta("INSERT INTO Notificaciones (Mensaje, Tipo, Fecha, IDInscripcion) VALUES (@Mensaje, @Tipo, @Fecha, @IDInscripcion); SELECT SCOPE_IDENTITY();");
                datos.SetearParametro("@Mensaje", "Nueva inscripción");
                datos.SetearParametro("@Tipo", "Inscripcion");
                datos.SetearParametro("@Fecha", DateTime.Now);
                datos.SetearParametro("@IDInscripcion", idInscripcion);
                datos.EjecutarAccion();

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                datos.CerrarConexion();
            }
            try
            {
                int idNotificacion = UltimoID();
                datos.LimpiarParametros();
                datos.SetearConsulta("insert into NotificacionesXUsuario(IDNotificacion, IDUsuario) VALUES(@IDNotificacion, @IDUsuario)");
                datos.SetearParametro("@IDNotificacion", idNotificacion);
                datos.SetearParametro("@IDUsuario", 1);
                datos.EjecutarAccion();
                datos.LimpiarParametros();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
        public int UltimoID()
        {
            try
            {
                datos.SetearConsulta("SELECT TOP 1 IDNotificacion FROM Notificaciones ORDER BY IDNotificacion DESC");
                datos.EjecutarLectura();
                datos.Lector.Read();
                return (int)datos.Lector["IDNotificacion"];
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
        public void NotificacionRespuestaInscripcion(InscripcionACurso inscripcion, bool estado)
        {
            string mensaje = "Pendiente";
            try
            {
                if (estado)
                {
                    mensaje = "Inscripción aceptada";
                }
                else
                {
                    mensaje = "Inscripción rechazada";
                }
                datos.SetearConsulta("INSERT INTO Notificaciones (Mensaje, Tipo, Fecha, IDInscripcion) VALUES (@Mensaje, @Tipo, @Fecha, @IDInscripcion); SELECT SCOPE_IDENTITY();");
                datos.SetearParametro("@Mensaje", mensaje);
                datos.SetearParametro("@Tipo", "Inscripcion");
                datos.SetearParametro("@Fecha", DateTime.Now);
                datos.SetearParametro("@IDInscripcion", inscripcion.IDInscripcion);
                datos.EjecutarAccion();

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                datos.CerrarConexion();
            }
            try
            {
                int idNotificacion = UltimoID();
                datos.LimpiarParametros();
                datos.SetearConsulta("insert into NotificacionesXUsuario(IDNotificacion, IDUsuario) VALUES(@IDNotificacion, @IDUsuario)");
                datos.SetearParametro("@IDNotificacion", idNotificacion);
                datos.SetearParametro("@IDUsuario", inscripcion.Usuario.IDUsuario);
                datos.EjecutarAccion();
                datos.LimpiarParametros();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
        public void AgregarNotificacionXMensaje(MensajeUsuario mensaje)
        {
            try
            {
                datos.SetearConsulta("INSERT INTO Notificaciones (Mensaje, Tipo, Fecha, IDMensaje) VALUES (@Mensaje, @Tipo, @Fecha, @IDMensaje); SELECT SCOPE_IDENTITY();");
                datos.SetearParametro("@Mensaje", "Nuevo mensaje");
                datos.SetearParametro("@Tipo", "Mensaje");
                datos.SetearParametro("@Fecha", DateTime.Now);
                datos.SetearParametro("@IDMensaje", mensaje.IDMensaje);
                datos.EjecutarAccion();

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                datos.CerrarConexion();
            }
            try
            {
                int idNotificacion = UltimoID();
                datos.LimpiarParametros();
                datos.SetearConsulta("insert into NotificacionesXUsuario(IDNotificacion, IDUsuario) VALUES(@IDNotificacion, @IDUsuario)");
                datos.SetearParametro("@IDNotificacion", idNotificacion);
                datos.SetearParametro("@IDUsuario", mensaje.UsuarioReceptor.IDUsuario);
                datos.EjecutarAccion();
                datos.LimpiarParametros();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
        }
        public void AgregarNotificacionXRespuesta(MensajeRespuesta respuesta)
        {
            try
            {
                datos.SetearConsulta("INSERT INTO Notificaciones (Mensaje, Tipo, Fecha, IDRespuesta) VALUES (@Mensaje, @Tipo, @Fecha, @IDMensaje); SELECT SCOPE_IDENTITY();");
                datos.SetearParametro("@Mensaje", "Nueva respuesta");
                datos.SetearParametro("@Tipo", "Respuesta");
                datos.SetearParametro("@Fecha", DateTime.Now);
                datos.SetearParametro("@IDMensaje", respuesta.IDRespuesta);
                datos.EjecutarAccion();


            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                datos.CerrarConexion();
            }
            try
            {
                int idNotificacion = UltimoID();
                datos.LimpiarParametros();
                datos.SetearConsulta("insert into NotificacionesXUsuario(IDNotificacion, IDUsuario) VALUES(@IDNotificacion, @IDUsuario)");
                datos.SetearParametro("@IDNotificacion", idNotificacion);
                datos.SetearParametro("@IDUsuario", respuesta.UsuarioReceptor.IDUsuario);
                datos.EjecutarAccion();
                datos.LimpiarParametros();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }

        }
    }
}
