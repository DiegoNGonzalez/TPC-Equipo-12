using AccesoDB;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ReinicioContraseniaNegocio
    {
        Datos datos = new Datos();
        public ReinicioContraseniaNegocio()
        {
            datos = new Datos();
        }

        public void GuardarTokenEnBD(ReinicioContrasenia reinicioContrasenia)
        {
            try
            {
                datos.SetearConsulta("SELECT COUNT(*) FROM ReiniciosContrasenias WHERE IDUsuario = @IDUsuario");
                datos.SetearParametro("@IDUsuario", reinicioContrasenia.IDUsuario);
                int tokenExiste = datos.ejecutarAccionScalar();
                datos.LimpiarParametros();
                datos.CerrarConexion();
                if (tokenExiste > 0)
                {
                    datos.SetearConsulta("UPDATE ReiniciosContrasenias SET Token = @Token, FechaExpiracion = DATEADD(MINUTE, 30, GETDATE()) WHERE IDUsuario = @IDUsuario");
                    datos.SetearParametro("@Token", reinicioContrasenia.Token);
                    datos.SetearParametro("@IDUsuario", reinicioContrasenia.IDUsuario);
                    datos.EjecutarAccion();
                }
                else
                {
                    datos.SetearConsulta("INSERT INTO ReiniciosContrasenias (IDUsuario, Token, FechaExpiracion) VALUES (@IDUsuario, @Token, DATEADD(MINUTE, 30, GETDATE()))");
                    datos.SetearParametro("@IDUsuario", reinicioContrasenia.IDUsuario);
                    datos.SetearParametro("@Token", reinicioContrasenia.Token);
                    datos.EjecutarAccion();
                }
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
                return tokenValido;
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
        public void ActualizarFechaExp(int IDUsuario)
        {
            try
            {
                datos.SetearConsulta("UPDATE ReiniciosContrasenias SET FechaExpiracion = GETDATE() WHERE IDUsuario = @IDUsuario");
                datos.SetearParametro("@IDUsuario", IDUsuario);
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
