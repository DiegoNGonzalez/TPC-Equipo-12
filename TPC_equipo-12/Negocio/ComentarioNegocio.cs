using AccesoDB;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ComentarioNegocio
    {
        private Datos datos;

        public ComentarioNegocio()
        {
            datos = new Datos();
        }
        public void publicarComentario(Comentario comentario)
        {
            Datos datos = new Datos();
            try
            {
                datos.SetearConsulta("INSERT INTO Comentarios(IDleccion, CuerpoComentario, IDUsuarioEmisor, FechaCreacion, Estado) VALUES (@idLeccion, @cuerpoComentario, @idEmisor, @fechaCreacion, @estado)");
                datos.SetearParametro("@idLeccion", comentario.Leccion.IDLeccion);
                datos.SetearParametro("@cuerpoComentario", comentario.CuerpoComentario);
                datos.SetearParametro("@idEmisor", comentario.UsuarioEmisor.IDUsuario);
                datos.SetearParametro("@fechaCreacion", comentario.FechaCreacion);
                //datos.SetearParametro("@idImagen", comentario.UsuarioEmisor.ImagenPerfil == null ? (object)DBNull.Value : comentario.UsuarioEmisor.ImagenPerfil.IDImagen);
                datos.SetearParametro("@estado", comentario.Estado);
                datos.EjecutarAccion();
                datos.LimpiarParametros();
            }
            catch (Exception ex)
            {

                throw ex;
            }finally
            {
                datos.CerrarConexion();
            }

        }
        public int ultimoID()
        {
            
            try
            {
                datos.SetearConsulta("SELECT TOP 1 IDComentario FROM Comentarios ORDER BY IDComentario DESC");
                datos.EjecutarLectura();
                datos.Lector.Read();
                return (int)datos.Lector["IDComentario"];
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
        public Comentario buscarComentario(int idComentario)
        {
            try
            {
                datos.SetearConsulta("SELECT IDComentario, IDComentarioPadre, IDUsuarioEmisor, CuerpoComentario, FechaCreacion, Estado, IDLeccion FROM Comentarios WHERE IDComentario = @idComentario");
                datos.SetearParametro("@idComentario", idComentario);
                datos.EjecutarLectura();
                datos.Lector.Read();
                
                Comentario comentario = new Comentario();
                comentario.IDComentario = (int)datos.Lector["IDComentario"];
                if (datos.Lector["IDComentarioPadre"] != DBNull.Value)
                {
                    comentario.IDComentarioPadre = (int)datos.Lector["IDComentarioPadre"];
                }else
                {
                    comentario.IDComentarioPadre = null;
                }
                comentario.UsuarioEmisor = new Usuario();
                comentario.UsuarioEmisor.IDUsuario = (int)datos.Lector["IDUsuarioEmisor"];
                comentario.CuerpoComentario = (string)datos.Lector["CuerpoComentario"];
                comentario.FechaCreacion = (DateTime)datos.Lector["FechaCreacion"];
                comentario.Estado = (bool)datos.Lector["Estado"];
                comentario.Leccion = new Leccion();
                comentario.Leccion.IDLeccion = (int)datos.Lector["IDLeccion"];
                return comentario;

            }
            catch (Exception)
            {

                throw;
            }finally
            {
                datos.LimpiarParametros();
                datos.CerrarConexion();
            }
        }
        public List<Comentario> cargarComentarios(int idLeccion)
        {
            Datos datos = new Datos();
            List<Comentario> listaComentarios = new List<Comentario>();
            try
            {
                datos.SetearConsulta(@"
                    SELECT 
                        c.IDComentario, 
                        c.CuerpoComentario,
                        c.IDUsuarioEmisor,
                        u.Nombre AS Nombre,
                        c.FechaCreacion,
                        ISNULL(i.URLIMG, 'perfil-1.jpg') AS ImagenPerfilURL
                    FROM 
                        Comentarios c 
                    INNER JOIN 
                        Usuarios u ON c.IDUsuarioEmisor = u.IDUsuario
                    LEFT JOIN
                        Imagenes i ON u.IDImagen = i.IDImagenes
                    WHERE 
                        c.IDComentarioPadre IS NULL and c.IDLeccion = @IDLecion");
                datos.SetearParametro("@IDLecion", idLeccion);
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    Comentario comentario = new Comentario();
                    comentario.IDComentario = (int)datos.Lector["IDComentario"];
                    comentario.CuerpoComentario = (string)datos.Lector["CuerpoComentario"];
                    comentario.UsuarioEmisor = new Usuario();
                    comentario.UsuarioEmisor.IDUsuario = (int)datos.Lector["IDUsuarioEmisor"];
                    comentario.UsuarioEmisor.Nombre = (string)datos.Lector["Nombre"];
                    if (datos.Lector["ImagenPerfilURL"] != DBNull.Value)
                    {
                        comentario.UsuarioEmisor.ImagenPerfil = new Imagen();
                        comentario.UsuarioEmisor.ImagenPerfil.URL = (string)datos.Lector["ImagenPerfilURL"];
                    }
                    else
                    {
                        comentario.UsuarioEmisor.ImagenPerfil = new Imagen();
                        comentario.UsuarioEmisor.ImagenPerfil.URL = "perfil-0.jpg";
                    }
                    comentario.FechaCreacion = (DateTime)datos.Lector["FechaCreacion"];
                    listaComentarios.Add(comentario);
                }
                return listaComentarios;


                
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
    
