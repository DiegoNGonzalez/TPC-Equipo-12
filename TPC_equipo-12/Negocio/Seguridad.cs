using AccesoDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class Seguridad
    {
        Datos datos = new Datos();
        public Seguridad()
        {
            datos = new Datos();
        }

        public void GuardarTokenEnBD(string token, int IDUsuario)
        {
            try
            {
                datos.SetearConsulta("SELECT COUNT(*) FROM ReiniciosContrasenias WHERE IDUsuario = @IDUsuario");
                datos.SetearParametro("@IDUsuario", IDUsuario);
                int tokenExiste = datos.ejecutarAccionScalar();
                datos.LimpiarParametros();
                datos.CerrarConexion();
                if (tokenExiste > 0)
                {
                    // Si ya existe un token para este usuario, actualiza el token y la fecha de expiración
                    datos.SetearConsulta("UPDATE ReiniciosContrasenias SET Token = @Token, FechaExpiracion = DATEADD(MINUTE, 30, GETDATE()) WHERE IDUsuario = @IDUsuario");
                    datos.SetearParametro("@Token", token);
                    datos.SetearParametro("@IDUsuario", IDUsuario);
                    datos.EjecutarAccion();
                }
                else
                {
                    // Si no existe un token para este usuario, inserta uno nuevo
                    datos.SetearConsulta("INSERT INTO ReiniciosContrasenias (IDUsuario, Token, FechaExpiracion) VALUES (@IDUsuario, @Token, DATEADD(MINUTE, 30, GETDATE()))");
                    datos.SetearParametro("@IDUsuario", IDUsuario);
                    datos.SetearParametro("@Token", token);
                    datos.EjecutarAccion();
                }
                datos.LimpiarParametros();
                datos.CerrarConexion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public int ObtenerIdUsuarioPorToken(string token)
        {
            int userId = 0;
            try
            {
                string consulta = "SELECT IDUsuario FROM ReiniciosContrasenias WHERE Token = @Token AND FechaExpiracion > GETDATE();";
                datos.SetearConsulta(consulta);
                datos.SetearParametro("@Token", token);
                datos.EjecutarLectura();

                if (datos.Lector.Read())
                {
                    userId = Convert.ToInt32(datos.Lector["IDUsuario"]);
                }
                return userId;
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

        public bool ValidarToken(string token)
        {
            bool tokenValido = false;
            try
            {

                string consulta = "SELECT FechaExpiracion FROM ReiniciosContrasenias WHERE Token = @Token";
                datos.SetearConsulta(consulta);
                datos.SetearParametro("@Token", token);

                datos.EjecutarLectura();

                if (datos.Lector.Read())
                {
                    DateTime fechaExpiracion = Convert.ToDateTime(datos.Lector["FechaExpiracion"]);

                    if (fechaExpiracion > DateTime.Now)
                    {
                        tokenValido = true;
                    }
                }

                datos.LimpiarParametros();
                datos.CerrarConexion();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return tokenValido;
        }
    }
}
