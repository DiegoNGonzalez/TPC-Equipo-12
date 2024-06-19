using AccesoDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    internal class CategoriaNegocio
    {
        public Datos Datos;

        public CategoriaNegocio()
        {
            Datos = new Datos();
        }

        public List<CategoriaCurso> ListarCategorias()
        {
            List<CategoriaCurso> lista = new List<CategoriaCurso>();
            try
            {
                Datos.SetearConsulta("SELECT IDCategoria, Nombre FROM Categorias");
                Datos.EjecutarLectura();

                while (Datos.Lector.Read())
                {
                    CategoriaCurso aux = new CategoriaCurso();
                    aux.IDCategoria = (int)Datos.Lector["IDCategoria"];
                    aux.Nombre = (string)Datos.Lector["Nombre"];
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

        public void AgregarCategoria(CategoriaCurso categoria)
        {
            try
            {
                Datos.SetearConsulta("INSERT INTO Categorias (Nombre) VALUES (@Nombre)");
                Datos.SetearParametro("@Nombre", categoria.Nombre);
                Datos.EjecutarAccion();
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

    }
}
