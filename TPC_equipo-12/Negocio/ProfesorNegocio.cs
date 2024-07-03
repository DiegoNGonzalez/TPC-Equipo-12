using AccesoDB;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ProfesorNegocio
    {
        private Datos Datos;
        public ProfesorNegocio()
        {
            Datos = new Datos();
        }

        public void actualizar(Profesor profesor)
        {
            Datos datos = new Datos();
            try
            {
                datos.SetearConsulta("UPDATE Usuarios SET Nombre = @nombre, Apellido = @apellido, DNI = @dni, Genero = @genero WHERE IDUsuario = @IDUsuario");
                datos.SetearParametro("@nombre", profesor.Nombre);
                datos.SetearParametro("@apellido", profesor.Apellido);
                datos.SetearParametro("@dni", profesor.DNI);
                datos.SetearParametro("@genero", (object)profesor.Genero ?? DBNull.Value);
                datos.SetearParametro("@IDUsuario", profesor.IDUsuario);
                datos.EjecutarAccion();
                datos.LimpiarParametros();
                datos.CerrarConexion();

                datos.SetearConsulta("SELECT IDImagen FROM Usuarios WHERE IDUsuario = @IDUsuario");
                datos.SetearParametro("@IDUsuario", profesor.IDUsuario);
                datos.EjecutarLectura();

                int idImagen = 0;
                if (datos.Lector.Read() && datos.Lector["IDImagen"] != DBNull.Value)
                {
                    idImagen = (int)datos.Lector["IDImagen"];
                }
                datos.CerrarConexion(); 

                if (idImagen != 0)
                {
                    // Actualizar la URL de la imagen existente
                    datos.LimpiarParametros();
                    datos.SetearConsulta("UPDATE Imagenes SET URLIMG = @imagen WHERE IDImagenes = @IDImagenes");
                    datos.SetearParametro("@imagen", profesor.ImagenPerfil.URL);
                    datos.SetearParametro("@IDImagenes", idImagen);
                    datos.EjecutarAccion();
                }
                else
                {
                    // Insertar una nueva imagen y obtener el nuevo ID
                    datos.LimpiarParametros();
                    if (!string.IsNullOrEmpty(profesor.ImagenPerfil.URL))
                    {
                        datos.SetearConsulta("INSERT INTO Imagenes (URLIMG) OUTPUT INSERTED.IDImagenes VALUES (@imagen)");
                        datos.SetearParametro("@imagen", profesor.ImagenPerfil.URL);
                        int nuevoIDImagen = datos.ejecutarAccionScalar();
                        datos.CerrarConexion();
                        // Actualizar el IDImagen del usuario con el nuevo IDImagen de la imagen insertada
                        datos.LimpiarParametros();
                        datos.SetearConsulta("UPDATE Usuarios SET IDImagen = @IDImagen WHERE IDUsuario = @IDUsuario");
                        datos.SetearParametro("@IDImagen", nuevoIDImagen);
                        datos.SetearParametro("@IDUsuario", profesor.IDUsuario);
                        datos.EjecutarAccion();
                    }
                    
                }

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
        public void BorrarProfesorXCurso(int idCurso)
        {
            try
            {
                Datos.SetearConsulta("delete from ProfesorXCursos where IDCurso = @IDCurso");
                Datos.SetearParametro("@IDCurso", idCurso);
                Datos.EjecutarAccion();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                Datos.LimpiarParametros();
                Datos.CerrarConexion();
            }
            
        }
        public Profesor buscarProfesorxLeccion(int idLeccion)
        {
            Profesor profesor = new Profesor();
            try
            {
                Datos.SetearConsulta("select u.IDUsuario, u.Nombre, u.Apellido, u.DNI, u.Genero, u.Email, u.IDImagen, i.URLIMG from Usuarios u inner join Imagenes i on u.IDImagen = i.IDImagenes inner JOIN ProfesorXCursos pc on u.IDUsuario= pc.IDProfesor inner JOIN Cursos c on c.IDCurso = pc.IDCurso INNER JOIN UnidadesXCurso uc on c.IDCurso=uc.IDCurso INNER JOIN LeccionesXUnidades lxc on lxc.IDUnidad = uc.IDUnidad INNER JOIN Lecciones l on l.IDLeccion = lxc.IDLeccion where l.IDLeccion = @IDLeccion");
                Datos.SetearParametro("@IDLeccion", idLeccion);
                Datos.EjecutarLectura();
                if (Datos.Lector.Read())
                {
                    profesor.IDUsuario = (int)Datos.Lector["IDUsuario"];
                    profesor.Nombre = (string)Datos.Lector["Nombre"];
                    profesor.Apellido = (string)Datos.Lector["Apellido"];
                    profesor.DNI = (int)Datos.Lector["DNI"];
                    profesor.Genero = (string)Datos.Lector["Genero"];
                    profesor.Email = (string)Datos.Lector["Email"];
                    profesor.ImagenPerfil = new Imagen();
                    profesor.ImagenPerfil.IDImagen = (int)Datos.Lector["IDImagen"];
                    profesor.ImagenPerfil.URL = (string)Datos.Lector["URLIMG"];
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                Datos.LimpiarParametros();
                Datos.CerrarConexion();
            }
            return profesor;
        }
    }
    
}
