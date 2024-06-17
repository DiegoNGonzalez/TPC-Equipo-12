using System;
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
            conexion = new SqlConnection("data source=.\\SQLEXPRESS; initial catalog=DB_DESPEGAv2; integrated security=true;");
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

        }
        public int ejecutarAccionScalar()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                return int.Parse(comando.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {

                throw ex;
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
            if (Valor == null)
            {
                comando.Parameters.AddWithValue(Nombre, DBNull.Value);
            }
            else
            {
                comando.Parameters.AddWithValue(Nombre, Valor);

            }

        }
        public void LimpiarParametros()
        {
            if (comando != null && comando.Parameters != null)
            {
                comando.Parameters.Clear();
            }
        }

    }
}
