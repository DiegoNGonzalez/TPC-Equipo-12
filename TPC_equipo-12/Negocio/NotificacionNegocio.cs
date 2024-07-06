using AccesoDB;
using Dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Negocio
{
    public class NotificacionNegocio
    {
        private Datos datos;
        public NotificacionNegocio()
        {
            datos = new Datos();
        }

        public List<Notificacion> listarNotificaciones(int IDUsuarioODeInscripcion, string UsuarioOInscripcion)
        {
            List<Notificacion> lista = new List<Notificacion>();
            try
            {
                if (UsuarioOInscripcion == "Usuario")
                {
                    datos.SetearConsulta("select n.IDNotificacion, n.Mensaje, n.Tipo, n.Fecha, n.Leido, n.IDInscripcion, n.IDMensaje, n.IDRespuesta,n.IDComentario, nxu.IDUsuario from Notificaciones n inner join NotificacionesXUsuario nxu on n.IDNotificacion= nxu.IDNotificacion WHERE nxu.IDUsuario = @IDUsuario and n.Leido=0");
                    datos.SetearParametro("@IDUsuario", IDUsuarioODeInscripcion);

                }
                else if (UsuarioOInscripcion == "Inscripcion")
                {
                    datos.SetearConsulta("select n.IDNotificacion, n.Mensaje, n.Tipo, n.Fecha, n.Leido, n.IDInscripcion, n.IDMensaje, n.IDRespuesta, nxu.IDUsuario from Notificaciones n inner join NotificacionesXUsuario nxu on n.IDNotificacion= nxu.IDNotificacion WHERE n.IDInscripcion = @IDInscripcion");
                    datos.SetearParametro("@IDInscripcion", IDUsuarioODeInscripcion);
                }
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    Notificacion aux = new Notificacion();
                    aux.IDNotificacion = (int)datos.Lector["IDNotificacion"];
                    aux.MensajeNotificacion = (string)datos.Lector["Mensaje"];
                    aux.Tipo = (string)datos.Lector["Tipo"];
                    aux.Fecha = (DateTime)datos.Lector["Fecha"];
                    aux.Estado = (bool)datos.Lector["Leido"];
                    if (aux.Tipo == "Inscripcion" || aux.Tipo == "Deshabilitado")
                    {
                        aux.Inscripcion = new InscripcionACurso();
                        aux.Inscripcion.IDInscripcion = (int)datos.Lector["IDInscripcion"];
                    }
                    else if (aux.Tipo == "Mensaje")
                    {
                        aux.Mensaje = new MensajeUsuario();
                        aux.Mensaje.IDMensaje = (int)datos.Lector["IDMensaje"];
                    }
                    else if (aux.Tipo == "Respuesta")
                    {
                        aux.MensajeRespuesta = new MensajeRespuesta();
                        aux.MensajeRespuesta.IDRespuesta = (int)datos.Lector["IDRespuesta"];
                    }
                    else
                    {
                        aux.ComentarioLeccion = new Comentario();
                        aux.ComentarioLeccion.IDComentario = (int)datos.Lector["IDComentario"];
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
        public void AgregarNotificacionXInscripcion(int idInscripcion, int idCurso)
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
                datos.LimpiarParametros();
                datos.CerrarConexion();
            }
            try
            {
                int idUsuario = 0;
                datos.SetearConsulta("Select IDProfesor from ProfesorXCursos where IDCurso = @IDCurso");
                datos.SetearParametro("@IDCurso", idCurso);
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    idUsuario = (int)datos.Lector["IDProfesor"];
                }
                datos.LimpiarParametros();
                datos.CerrarConexion();

                int idNotificacion = UltimoID();
                datos.SetearConsulta("insert into NotificacionesXUsuario(IDNotificacion, IDUsuario) VALUES(@IDNotificacion, @IDUsuario)");
                datos.SetearParametro("@IDNotificacion", idNotificacion);
                datos.SetearParametro("@IDUsuario", idUsuario);
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
                datos.LimpiarParametros();
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
        public void AgregarNotificacionxComentario(Comentario ComentarioLeccion)
        {
            try
            {
                datos.SetearConsulta("insert into notificaciones (Mensaje, Tipo, Fecha, IDComentario) VALUES (@Mensaje, @Tipo, @Fecha, @IDComentario); SELECT SCOPE_IDENTITY();");
                datos.SetearParametro("@Mensaje", "Nuevo comentario");
                datos.SetearParametro("@Tipo", "Comentario");
                datos.SetearParametro("@Fecha", DateTime.Now);
                datos.SetearParametro("@IDComentario", ComentarioLeccion.IDComentario);
                datos.EjecutarAccion();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.CerrarConexion();
            }
            ProfesorNegocio aux = new ProfesorNegocio();
            Profesor aux2 = aux.buscarProfesorxLeccion(ComentarioLeccion.Leccion.IDLeccion);

            try
            {
                int idNotificacion = UltimoID();
                datos.LimpiarParametros();
                datos.SetearConsulta("insert into NotificacionesXUsuario(IDNotificacion, IDUsuario) VALUES(@IDNotificacion, @IDUsuario)");
                datos.SetearParametro("@IDNotificacion", idNotificacion);
                datos.SetearParametro("@IDUsuario", aux2.IDUsuario);
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
        public void BorrarNotificacionXUsuario(int iDNotificacion)
        {
            try
            {
                datos.SetearConsulta("delete from NotificacionesXUsuario where IDNotificacion=@IDNotificacion");
                datos.SetearParametro("@IDNotificacion", iDNotificacion);
                datos.EjecutarAccion();

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
        public void BorrarNotificacion(int iDNotificacion)
        {
            try
            {
                datos.SetearConsulta("delete from Notificaciones where IDNotificacion=@IDNotificacion");
                datos.SetearParametro("@IDNotificacion", iDNotificacion);
                datos.EjecutarAccion();

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
        public List<int> listarIdsNotificacionesXInscripcion(int idInscripcion)
        {
            List<int> lista = new List<int>();
            try
            {
                datos.SetearConsulta("select IDNotificacion from Notificaciones where IDInscripcion = @IDInscripcion");
                datos.SetearParametro("@IDInscripcion", idInscripcion);
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    lista.Add((int)datos.Lector["IDNotificacion"]);
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
        public void notificacionXCursoDeshabilitado(int idCurso)
        {
            List<InscripcionACurso> estudiantes = new List<InscripcionACurso>();
            InscripcionNegocio aux = new InscripcionNegocio(false);
            estudiantes = aux.listarInscripcionesXCurso(idCurso);
            foreach (InscripcionACurso item in estudiantes)
            {
                try
                {
                    datos.SetearConsulta("INSERT INTO Notificaciones (Mensaje, Tipo, Fecha, IDInscripcion) VALUES (@Mensaje, @Tipo, @Fecha, @IDInscripcion); SELECT SCOPE_IDENTITY();");
                    datos.SetearParametro("@Mensaje", "Curso deshabilitado");
                    datos.SetearParametro("@Tipo", "Deshabilitado");
                    datos.SetearParametro("@Fecha", DateTime.Now);
                    datos.SetearParametro("@IDInscripcion", item.IDInscripcion);
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
                    datos.SetearParametro("@IDUsuario", item.Usuario.IDUsuario);
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
        public void reactivarNotificacionXInscripcion(int idInscripcion)
        {
            try
            {
                datos.SetearConsulta("UPDATE Notificaciones SET Leido = 0 WHERE IDInscripcion = @IDInscripcion");
                datos.SetearParametro("@IDInscripcion", idInscripcion);
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
        public int buscarNotificacionXInscripcionXUsuario(int idInscripcion, int idUsuario)
        {
            int idNotificacion = 0;
            try
            {
                datos.SetearConsulta("select * from Notificaciones n inner join notificacionesXusuario nxu on n.IdNotificacion= nxu.IDNotificacion where IDInscripcion = @IDInscripcion and nxu.IDUsuario=@idUsuario");
                datos.SetearParametro("@IDInscripcion", idInscripcion);
                datos.SetearParametro("@idUsuario", idUsuario);
                datos.EjecutarLectura();
                if (datos.Lector.Read())
                {
                    idNotificacion = (int)datos.Lector["IDNotificacion"];
                }
                return idNotificacion;
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
        public void marcarComoNoLeidaYMensaje(int idNotificacion, string mensaje)
        {
            try
            {
                datos.SetearConsulta("UPDATE Notificaciones SET Leido = 0, mensaje=@Mensaje WHERE IDNotificacion = @IDNotificacion");
                datos.SetearParametro("@IDNotificacion", idNotificacion);
                datos.SetearParametro("@Mensaje", mensaje);
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
    }
}
