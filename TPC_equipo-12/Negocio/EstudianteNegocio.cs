using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using AccesoDB;
using System.Collections;

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
            List <Estudiante> lista = new List<Estudiante>();

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
                    }else
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

    }
}
