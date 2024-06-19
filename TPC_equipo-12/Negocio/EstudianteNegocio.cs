using AccesoDB;
using Dominio;
using System;
using System.Collections.Generic;

namespace Negocio
{
    public class EstudianteNegocio
    {
        private Datos Datos;
        public EstudianteNegocio()
        {
            Datos = new Datos();
        }
        public List<Estudiante> ListarEstudiantes()
        {
            List<Estudiante> lista = new List<Estudiante>();

            try
            {
                Datos.SetearConsulta("select u.Nombre, u.Apellido, u.DNI, u.Genero, u.Email, u.Contrasenia, u.EsProfesor, i.IDImagenes, i.URLIMG,  e.IDEstudiante, e.Estado  from usuarios u inner join Estudiantes e on u.IDUsuario=e.IDEstudiante left JOIN Imagenes i on u.IDImagen= i.IDImagenes");
                Datos.EjecutarLectura();
                while (Datos.Lector.Read())
                {
                    Estudiante aux = new Estudiante();
                    aux.IDUsuario = Datos.Lector.GetInt32(9);
                    aux.Nombre = (string)Datos.Lector["Nombre"];
                    aux.Apellido = (string)Datos.Lector["Apellido"];
                    aux.DNI = (int)Datos.Lector["DNI"];
                    aux.Genero = (string)Datos.Lector["Genero"];
                    aux.Email = (string)Datos.Lector["Email"];
                    aux.Contrasenia = (string)Datos.Lector["Contrasenia"];
                    aux.EsProfesor = (bool)Datos.Lector["EsProfesor"];
                    if (Datos.Lector["IDImagenes"] != DBNull.Value)
                    {
                        aux.ImagenPerfil = new Imagen();
                        aux.ImagenPerfil.IDImagen = (int)Datos.Lector["IDImagenes"];
                        aux.ImagenPerfil.URL = (string)Datos.Lector["URLIMG"];
                    }
                    else
                    {
                        aux.ImagenPerfil = new Imagen();
                        aux.ImagenPerfil.IDImagen = 0;
                        aux.ImagenPerfil.URL = "https://www.abc.com.py/resizer/1J9J9Q1";
                    }

                    aux.Estado = (bool)Datos.Lector["Estado"];
                    lista.Add(aux);
                }
                return lista;
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
        public void Agregar(Usuario usuario)
        {
            try
            {
                Datos.SetearConsulta("insert into Estudiantes(IDEstudiante, Estado) values(@IdEstudiante, @Estado)");
                Datos.SetearParametro("@IdEstudiante", usuario.IDUsuario);
                Datos.SetearParametro("@Estado", true);
                Datos.EjecutarAccion();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                Datos.CerrarConexion();
            }
        }
        public void CargarEstudianteEnCurso(int idUsuario, int idCurso)
        {
            try
            {
                if (EstaInscripto(idUsuario, idCurso))
                {
                    throw new Exception("El estudiante ya esta inscripto en el curso");
                }
                else
                {
                    Datos.SetearConsulta("insert into EstudiantesXCursos (IDEstudiante, IDCurso) values (@IDEstudiante, @IDCurso)");
                    Datos.SetearParametro("@IDEstudiante", idUsuario);
                    Datos.SetearParametro("@IDCurso", idCurso);
                    Datos.EjecutarAccion();
                    Datos.LimpiarParametros();


                }
            }
            catch (Exception ex)
            {

                throw ex;

            }
            finally
            {
                Datos.CerrarConexion();

            }

        }
        public void ModificarEstado(Estudiante estudiante)
        {
            try
            {
                Datos.SetearConsulta("update Estudiantes set Estado = @Estado where IDEstudiante = @IdEstudiante");
                Datos.SetearParametro("@IdEstudiante", estudiante.IDUsuario);
                Datos.SetearParametro("@Estado", estudiante.Estado);
                Datos.EjecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                Datos.CerrarConexion();
            }
        }

        public bool EsEstudiante(int idEstudiante)
        {
            try
            {
                Datos.SetearConsulta("select * from Estudiantes where IDEstudiante = @IdEstudiante");
                Datos.SetearParametro("@IdEstudiante", idEstudiante);
                Datos.EjecutarLectura();
                if (Datos.Lector.Read())
                {
                    return true;

                }
                else
                {
                    return false;
                }
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
        public bool EstaInscripto(int idEstudiante, int idCurso)
        {
            try
            {
                Datos.SetearConsulta("select * from EstudiantesXCursos where IDEstudiante = @IdEstudiante and IDCurso = @IdCurso");
                Datos.SetearParametro("@IdEstudiante", idEstudiante);
                Datos.SetearParametro("@IdCurso", idCurso);
                Datos.EjecutarLectura();
                if (Datos.Lector.Read())
                {
                    return true;
                }
                else
                {
                    return false;
                }
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

        public void actualizar(Estudiante estudiante)
        {
            Datos datos = new Datos();
            try
            {
                datos.SetearConsulta("UPDATE Usuarios SET Nombre = @nombre, Apellido = @apellido, DNI = @dni, Genero = @genero WHERE IDUsuario = @IDUsuario");
                datos.SetearParametro("@nombre", estudiante.Nombre);
                datos.SetearParametro("@apellido", estudiante.Apellido);
                datos.SetearParametro("@dni", estudiante.DNI);
                datos.SetearParametro("@genero", (object) estudiante.Genero ?? DBNull.Value);
                datos.SetearParametro("@IDUsuario", estudiante.IDUsuario);
                datos.EjecutarAccion();
                datos.LimpiarParametros();
                datos.CerrarConexion();

                datos.SetearConsulta("SELECT IDImagen FROM Usuarios WHERE IDUsuario = @IDUsuario");
                datos.SetearParametro("@IDUsuario", estudiante.IDUsuario);
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
                    datos.SetearParametro("@imagen", estudiante.ImagenPerfil.URL);
                    datos.SetearParametro("@IDImagenes", idImagen);
                    datos.EjecutarAccion();
                }
                else
                {
                    // Insertar una nueva imagen y obtener el nuevo ID
                    datos.LimpiarParametros();
                    datos.SetearConsulta("INSERT INTO Imagenes (URLIMG) OUTPUT INSERTED.IDImagenes VALUES (@imagen)");
                    datos.SetearParametro("@imagen", estudiante.ImagenPerfil.URL);
                    int nuevoIDImagen = datos.ejecutarAccionScalar();
                    datos.CerrarConexion();
                    // Actualizar el IDImagen del usuario con el nuevo IDImagen de la imagen insertada
                    datos.LimpiarParametros();
                    datos.SetearConsulta("UPDATE Usuarios SET IDImagen = @IDImagen WHERE IDUsuario = @IDUsuario");
                    datos.SetearParametro("@IDImagen", nuevoIDImagen);
                    datos.SetearParametro("@IDUsuario", estudiante.IDUsuario);
                    datos.EjecutarAccion();
                }

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
        public void Desuscribirse(int idEstudiante, int idCurso)
        {
            try
            {
                //falta validar que unicamente elimine las lecciones de ese curso.

                Datos.SetearConsulta("delete from LeccionesXEstudiantes where IDEstudiante = @IdEstudiante");
                Datos.SetearParametro("@IdEstudiante", idEstudiante);
                Datos.EjecutarAccion();
                Datos.LimpiarParametros();
                Datos.CerrarConexion();


                Datos.SetearConsulta("delete from EstudiantesXCursos where IDEstudiante = @IdEstudiante and IDCurso = @IdCurso");
                Datos.SetearParametro("@IdEstudiante", idEstudiante);
                Datos.SetearParametro("@IdCurso", idCurso);
                Datos.EjecutarAccion();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Datos.CerrarConexion();
            }
        }
    }
}
