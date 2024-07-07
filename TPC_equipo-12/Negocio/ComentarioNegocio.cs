using AccesoDB;
using Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
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
        public void publicarComentario(int idComentarioPadre, int idLeccion, string cuerpoComentario, int IDUsuario, DateTime fechaCreacion)
        {
            try
            {
                datos.SetearConsulta("INSERT INTO Comentarios(IDComentarioPadre, IDleccion, CuerpoComentario, IDUsuarioEmisor, FechaCreacion, Estado) VALUES (@idComentarioPadre, @idLeccion, @cuerpoComentario, @idEmisor, @fechaCreacion, @estado)");
                datos.SetearParametro("@idComentarioPadre", idComentarioPadre);
                datos.SetearParametro("@idLeccion", idLeccion);
                datos.SetearParametro("@cuerpoComentario", cuerpoComentario);
                datos.SetearParametro("@idEmisor", IDUsuario);
                datos.SetearParametro("@fechaCreacion", fechaCreacion);
                //datos.SetearParametro("@idImagen", comentario.UsuarioEmisor.ImagenPerfil == null ? (object)DBNull.Value : comentario.UsuarioEmisor.ImagenPerfil.IDImagen);
                datos.SetearParametro("@estado", true);
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
        public void publicarComentario(Comentario comentario)
        {
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
            }
            finally
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
                }
                else
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
            }
            finally
            {
                datos.LimpiarParametros();
                datos.CerrarConexion();
            }
        }
        public List<Comentario> cargarComentarios(int idLeccion)
        {
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
                        ISNULL(i.URLIMG, 'perfil-0.jpg') AS ImagenPerfilURL
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
        public Comentario CargarComentarioPrincipal(int idComentarioPadre)
        {
            Comentario comentarioPrincipal = new Comentario();
            try
            {
                datos.SetearConsulta("SELECT C.IDComentario, C.IDUsuarioEmisor, U.Nombre, U.IDImagen, C.CuerpoComentario, C.FechaCreacion FROM Comentarios C INNER JOIN Usuarios U ON C.IDUsuarioEmisor = U.IDUsuario WHERE C.IDComentario = @IdComentarioPadre");

                datos.SetearParametro("@IdComentarioPadre", idComentarioPadre);
                datos.EjecutarLectura();
                datos.Lector.Read();
                comentarioPrincipal.IDComentario = (int)datos.Lector["IDComentario"];
                comentarioPrincipal.UsuarioEmisor = new Usuario();
                comentarioPrincipal.UsuarioEmisor.IDUsuario = (int)datos.Lector["IDUsuarioEmisor"];
                comentarioPrincipal.UsuarioEmisor.Nombre = (string)datos.Lector["Nombre"];
                comentarioPrincipal.CuerpoComentario = (string)datos.Lector["CuerpoComentario"];
                if (datos.Lector["IDImagen"] != DBNull.Value)
                {
                    comentarioPrincipal.UsuarioEmisor.ImagenPerfil = new Imagen();
                    comentarioPrincipal.UsuarioEmisor.ImagenPerfil.IDImagen = (int)datos.Lector["IDImagen"];
                }
                else
                {
                    comentarioPrincipal.UsuarioEmisor.ImagenPerfil = new Imagen();
                    comentarioPrincipal.UsuarioEmisor.ImagenPerfil.IDImagen = 0;
                }
                comentarioPrincipal.FechaCreacion = (DateTime)datos.Lector["FechaCreacion"];
                return comentarioPrincipal;
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
        public List<Comentario> cargarRespuestas(int idComentarioPadre)
        {
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
                        ISNULL(i.URLIMG, 'perfil-0.jpg') AS ImagenPerfilURL
                    FROM 
                        Comentarios c 
                    INNER JOIN 
                        Usuarios u ON c.IDUsuarioEmisor = u.IDUsuario
                    LEFT JOIN
                        Imagenes i ON u.IDImagen = i.IDImagenes
                    WHERE 
                        c.IDComentarioPadre = @IdComentarioPadre");
                datos.SetearParametro("@IdComentarioPadre", idComentarioPadre);
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
        //public void cargarComentarioPrincipalYrespuestas(int idComentarioPadre)
        //{
        //    Datos datos = new Datos();
        //    DataTable dtPadre = new DataTable();
        //    DataTable dtRespuestas = new DataTable();

        //    try
        //    {
        //        datos.SetearConsulta("SELECT C.IDComentario, C.IDUsuarioEmisor, U.Nombre, U.IDImagen, C.CuerpoComentario, C.FechaCreacion FROM Comentarios C INNER JOIN Usuarios U ON C.IDUsuarioEmisor = U.IDUsuario WHERE C.IDComentario = @IdComentarioPadre");
        //        datos.SetearParametro("@IdComentarioPadre", idComentarioPadre);
        //        datos.EjecutarLectura();
        //        dtPadre.Load(datos.Lector);
        //        datos.CerrarConexion();
        //        datos.LimpiarParametros();

                //datos.SetearConsulta("SELECT C.IDComentario, C.IDUsuarioEmisor, U.Nombre, U.IDImagen, C.CuerpoComentario, C.FechaCreacion FROM Comentarios C INNER JOIN Usuarios U ON C.IDUsuarioEmisor = U.IDUsuario WHERE C.IDComentarioPadre = @IdComentarioPadre");
                //datos.SetearParametro("@IdComentarioPadre", idComentarioPadre);
                //datos.EjecutarLectura();
                //dtRespuestas.Load(datos.Lector);
                //datos.CerrarConexion();

        //        rptComentarioPadre.DataSource = dtPadre;
        //        rptComentarioPadre.DataBind();

        //        rptRespuestas.DataSource = dtRespuestas;
        //        rptRespuestas.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        datos.CerrarConexion();
        //    }
        //}
    }
}

