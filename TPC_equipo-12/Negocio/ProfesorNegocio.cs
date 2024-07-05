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

        public Profesor buscarProfesorXCurso(int iDCurso)
        {
            Profesor aux = new Profesor();
            try
            {
                Datos.SetearConsulta("select p.IDProfesor from ProfesorXCursos p where IDCurso=@iDCurso");
                Datos.SetearParametro("@iDCurso", iDCurso);
                Datos.EjecutarLectura();
                if (Datos.Lector.Read())
                {
                    aux.IDUsuario = (int)Datos.Lector["IDProfesor"];
                }
                return aux;
            }
            catch (Exception ex)
            {

                throw ex;
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
                Datos.SetearConsulta("select u.IDUsuario, u.Nombre, u.Apellido, u.DNI, u.Genero, u.Email, u.IDImagen, i.URLIMG from Usuarios u inner JOIN ProfesorXCursos pc on u.IDUsuario= pc.IDProfesor inner JOIN Cursos c on c.IDCurso = pc.IDCurso INNER JOIN UnidadesXCurso uc on c.IDCurso=uc.IDCurso INNER JOIN LeccionesXUnidades lxc on lxc.IDUnidad = uc.IDUnidad INNER JOIN Lecciones l on l.IDLeccion = lxc.IDLeccion left join Imagenes i on u.IDImagen = i.IDImagenes where l.IDLeccion = @IDLeccion");
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
                    if (Datos.Lector["IDImagen"] != DBNull.Value)
                    {
                        profesor.ImagenPerfil.IDImagen = (int)Datos.Lector["IDImagen"];
                        profesor.ImagenPerfil.URL = (string)Datos.Lector["URLIMG"];

                    }
                    else
                    {
                        profesor.ImagenPerfil.IDImagen = 0;
                        profesor.ImagenPerfil.URL = "https://static.vecteezy.com/system/resources/thumbnails/008/442/086/small/illustration-of-human-icon-user-symbol-icon-modern-design-on-blank-background-free-vector.jpg";
                    }
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

        public int ObtenerLicencia()
        {
            int licencia = 0;
            try
            {
                Datos.SetearConsulta("Select * From LicenciaProfesor");
                Datos.EjecutarLectura();
                while(Datos.Lector.Read())
                {
                    licencia = (int)Datos.Lector["Licencia"];
                }
                return licencia;
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

        public bool VerificarLicencia(int licencia)
        {
            try
            {
                Datos.SetearConsulta("Select * From LicenciaProfesor");
                Datos.EjecutarLectura();
                while (Datos.Lector.Read())
                {
                    if (licencia == (int)Datos.Lector["Licencia"])
                    {
                        return true;
                    }
                }
                return false;
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

        public void InsertarProfesor(int IdUsuario)
        {
            try
            {
                Datos.SetearConsulta("insert into Profesor (IDProfesor) values (@IDUsuario)");
                Datos.SetearParametro("@IDUsuario", IdUsuario);
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

        public void InsertarCursosProfesorDEMO()
        {
            try
            {
                Datos.SetearConsulta("INSERT INTO ProfesorXCursos(IDProfesor, IDCurso) VALUES(1, 1)");
                Datos.EjecutarAccion();
                Datos.CerrarConexion();

                Datos.SetearConsulta("INSERT INTO ProfesorXCursos(IDProfesor, IDCurso) VALUES(1, 2)");
                Datos.EjecutarAccion();
                Datos.CerrarConexion();

                Datos.SetearConsulta("INSERT INTO ProfesorXCursos(IDProfesor, IDCurso) VALUES(1, 3)");
                Datos.EjecutarAccion();
                Datos.CerrarConexion();

                Datos.SetearConsulta("INSERT INTO ProfesorXCursos(IDProfesor, IDCurso) VALUES(1, 4)");
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
    }
    
}
