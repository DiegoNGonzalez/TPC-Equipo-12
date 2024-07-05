using AccesoDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class CategoriaNegocio
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
        public string CategoriaNombreXIDCurso(int idcurso)
        {
            int IDCategoria = 0;
            try
            {
                Datos.SetearConsulta("SELECT IDCategoria from CategoriasXCurso WHERE IDCurso = @IDCurso");
                Datos.SetearParametro("@IDCurso", idcurso);
                Datos.EjecutarLectura();

                while (Datos.Lector.Read())
                {
                    IDCategoria = (int)Datos.Lector["IDCategoria"];
                }
                Datos.LimpiarParametros();
                Datos.CerrarConexion();

                if (IDCategoria != 0)
                {
                    Datos.SetearConsulta("SELECT Nombre FROM Categorias WHERE IDCategoria = @IDCategoria");
                    Datos.SetearParametro("@IDCategoria", IDCategoria);
                    Datos.EjecutarLectura();
                    while (Datos.Lector.Read())
                    {
                        return (string)Datos.Lector["Nombre"];
                    }
                }
                return "";
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

        public void AgregarCursoXCategoria(int idCurso, int idCategoria)
        {
            try
            {
                Datos.SetearConsulta("INSERT INTO CursosXCategorias (IDCurso, IDCategoria) VALUES (@IDCurso, @IDCategoria)");
                Datos.SetearParametro("@IDCurso", idCurso);
                Datos.SetearParametro("@IDCategoria", idCategoria);
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
        public bool ExisteCategoria(string nombreCategoria)
        {
            try
            {
                Datos.SetearConsulta("SELECT COUNT(*) FROM Categorias WHERE Nombre = @Nombre");
                Datos.SetearParametro("@Nombre", nombreCategoria);
                Datos.EjecutarLectura();

                if (Datos.Lector.Read() && (int)Datos.Lector[0] > 0)
                {
                    return true;
                }
                return false;
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

        public void ModificarCategoria(CategoriaCurso categoria)
        {
            try
            {
                Datos.SetearConsulta("UPDATE Categorias SET Nombre = @Nombre WHERE IDCategoria = @IDCategoria");
                Datos.SetearParametro("@Nombre", categoria.Nombre);
                Datos.SetearParametro("@IDCategoria", categoria.IDCategoria);
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

        public void EliminarCategoria(int idCategoria)
        {
            try
            {
                Datos.SetearConsulta("DELETE FROM Categorias WHERE IDCategoria = @IDCategoria");
                Datos.SetearParametro("@IDCategoria", idCategoria);
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
        public bool CategoriaAsociadaACurso(int idCategoria)
{
    try
    {
        Datos.SetearConsulta("SELECT COUNT(*) FROM CategoriasXCurso WHERE IDCategoria = @IDCategoria");
        Datos.SetearParametro("@IDCategoria", idCategoria);
        Datos.EjecutarLectura();

        if (Datos.Lector.Read() && (int)Datos.Lector[0] > 0)
        {
            return true; 
        }
        return false; 
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
