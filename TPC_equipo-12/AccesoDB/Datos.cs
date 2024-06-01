using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace AccesoDB
{
    public class Datos
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;

        public SqlDataReader Lector
        {
            get { return lector; }
        }
        public Datos()
        {
            conexion = new SqlConnection("data source=.\\SQLEXPRESS; initial catalog=DB_DESPEGA; integrated security=true;");
            comando = new SqlCommand();
        }
        public void SetearConsulta(string consulta)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }

        public void EjecutarLectura()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                lector = comando.ExecuteReader();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public void EjecutarAccion()
        {
            comando.Connection = conexion;
            try
            {

                conexion.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                comando.Parameters.Clear();
            }
        }

        public void CerrarConexion()
        {
            if (lector != null)
                lector.Close();
            conexion.Close();
        }

        public void SetearParametro(string Nombre, object Valor)
        {
            comando.Parameters.AddWithValue(Nombre, Valor);

        }
    }
}
