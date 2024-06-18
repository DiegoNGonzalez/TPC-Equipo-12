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
                datos.SetearConsulta("UPDATE Usuarios SET Nombre = @nombre, Apellido = @apellido WHERE IDUsuario = @IDUsuario");
                datos.SetearParametro("@nombre", profesor.Nombre);
                datos.SetearParametro("@apellido", profesor.Apellido);
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
