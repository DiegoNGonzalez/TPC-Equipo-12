using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using AccesoDB;

namespace Negocio
{
    public class MensajeUsuarioNegocio
    {
        private Datos datos;
        public MensajeUsuarioNegocio()
        {
            datos = new Datos();
        }

        public List<MensajeUsuario> listarMensajes(int IDUsuario)
        {
            List<MensajeUsuario> lista = new List<MensajeUsuario>();
            try
            {
                datos.SetearConsulta("select m.IDMensaje, m.Mensaje, m.FechaHora, m.IDEmisor, m.IDReceptor, m.Asunto, m.Leido from Mensajes m WHERE m.IDReceptor = @IDUsuario");
                datos.SetearParametro("@IDUsuario", IDUsuario);
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    MensajeUsuario aux = new MensajeUsuario();
                    aux.IDMensaje = (int)datos.Lector["IDMensaje"];
                    aux.Mensaje = (string)datos.Lector["Mensaje"];
                    aux.Asunto = (string)datos.Lector["Asunto"];
                    aux.FechaHora = (DateTime)datos.Lector["FechaHora"];
                    aux.Leido = (bool)datos.Lector["Leido"];
                    aux.UsuarioEmisor = new Usuario();
                    aux.UsuarioEmisor.IDUsuario = (int)datos.Lector["IDEmisor"];
                    aux.UsuarioReceptor = new Usuario();
                    aux.UsuarioReceptor.IDUsuario = (int)datos.Lector["IDReceptor"];
                    lista.Add(aux);
                }
                foreach(MensajeUsuario mensaje in lista)
                {
                    int idEmisor = mensaje.UsuarioEmisor.IDUsuario;
                    int idReceptor = mensaje.UsuarioReceptor.IDUsuario;
                    UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
                    mensaje.UsuarioEmisor = usuarioNegocio.buscarUsuario(idEmisor);
                    mensaje.UsuarioReceptor = usuarioNegocio.buscarUsuario(idReceptor);
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

        public void EnviarMensaje(MensajeUsuario mensaje)
        {
            try
            {
                datos.SetearConsulta("insert into Mensajes (Mensaje, FechaHora, IDEmisor, IDReceptor, Asunto) values (@Mensaje, @FechaHora, @IDEmisor, @IDReceptor, @Asunto)");
                datos.SetearParametro("@Mensaje", mensaje.Mensaje);
                datos.SetearParametro("@FechaHora", mensaje.FechaHora);
                datos.SetearParametro("@IDEmisor", mensaje.UsuarioEmisor.IDUsuario);
                datos.SetearParametro("@IDReceptor", mensaje.UsuarioReceptor.IDUsuario);
                datos.SetearParametro("@Asunto", mensaje.Asunto);
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
        public MensajeUsuario BuscarMensaje(int idMensaje)
        {
            MensajeUsuario mensaje = new MensajeUsuario();
            try
            {
                datos.SetearConsulta("select m.IDMensaje, m.Mensaje, m.FechaHora, m.IDEmisor, m.IDReceptor, m.Asunto from Mensajes m WHERE m.IDMensaje = @IDMensaje");
                datos.SetearParametro("@IDMensaje", idMensaje);
                datos.EjecutarLectura();
                if (datos.Lector.Read())
                {
                    mensaje.IDMensaje = (int)datos.Lector["IDMensaje"];
                    mensaje.Mensaje = (string)datos.Lector["Mensaje"];
                    mensaje.Asunto = (string)datos.Lector["Asunto"];
                    mensaje.FechaHora = (DateTime)datos.Lector["FechaHora"];
                    mensaje.UsuarioEmisor = new Usuario();
                    mensaje.UsuarioEmisor.IDUsuario = (int)datos.Lector["IDEmisor"];
                    mensaje.UsuarioReceptor = new Usuario();
                    mensaje.UsuarioReceptor.IDUsuario = (int)datos.Lector["IDReceptor"];
                }
                UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
                mensaje.UsuarioReceptor= usuarioNegocio.buscarUsuario(mensaje.UsuarioReceptor.IDUsuario);
                mensaje.UsuarioEmisor = usuarioNegocio.buscarUsuario(mensaje.UsuarioEmisor.IDUsuario);
                return mensaje;
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
    }
}