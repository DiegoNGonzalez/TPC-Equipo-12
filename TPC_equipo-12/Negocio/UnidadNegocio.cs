using AccesoDB;
using Dominio;
using System;
using System.Collections.Generic;

namespace Negocio
{
    public class UnidadNegocio
    {
        private Datos Datos;
        private LeccionNegocio LeccionesDeUnidad;
        private MaterialNegocio MaterialesDeUnidad;
        public UnidadNegocio()
        {
            Datos = new Datos();
            LeccionesDeUnidad = new LeccionNegocio();
            MaterialesDeUnidad = new MaterialNegocio();
        }
        public List<Unidad> ListarUnidades(int IDCurso)
        {
            List<Unidad> lista = new List<Unidad>();
            try
            {
                Datos.SetearConsulta("select u.IDUnidad, u.Nombre, u.NroUnidad, u.Descripcion, uxc.Estado from Unidades u inner join UnidadesXCurso uxc on u.IDUnidad = uxc.IDUnidad Where uxc.IDCurso = @IDCurso");
                Datos.SetearParametro("@IDCurso", IDCurso);
                Datos.EjecutarLectura();
                while (Datos.Lector.Read())
                {
                    Unidad aux = new Unidad();
                    aux.IDUnidad = (int)Datos.Lector["IDUnidad"];
                    aux.Nombre = (string)Datos.Lector["Nombre"];
                    aux.NroUnidad = (int)Datos.Lector["NroUnidad"];
                    aux.Descripcion = (string)Datos.Lector["Descripcion"];
                    aux.Estado = (bool)Datos.Lector["Estado"];
                    lista.Add(aux);
                }
                foreach (var item in lista)
                {
                    item.Lecciones = LeccionesDeUnidad.ListarLecciones(item.IDUnidad);
                }
                Datos.LimpiarParametros();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                Datos.CerrarConexion();
            }
            return lista;
        }

        public void CrearUnidad(Unidad unidad, int IDCurso)
        {
            try
            {
                Datos.SetearConsulta("insert into Unidades (Nombre, NroUnidad, Descripcion) values (@Nombre, @NroUnidad, @Descripcion)");
                Datos.SetearParametro("@Nombre", unidad.Nombre);
                Datos.SetearParametro("@NroUnidad", unidad.NroUnidad);
                Datos.SetearParametro("@Descripcion", unidad.Descripcion);
                Datos.EjecutarAccion();
                Datos.CerrarConexion();

                Datos.SetearConsulta("Select top(1) IDUnidad From Unidades order by IDUnidad desc");
                Datos.EjecutarLectura();
                if (Datos.Lector.Read())
                {
                    unidad.IDUnidad = (int)Datos.Lector["IDUnidad"];
                }
                Datos.CerrarConexion();

                Datos.SetearConsulta("insert into UnidadesXCurso (IDUnidad, IDCurso) values (@IDUnidad, @IDCurso)");
                Datos.SetearParametro("@IDUnidad", unidad.IDUnidad);
                Datos.SetearParametro("@IDCurso", IDCurso);
                Datos.EjecutarAccion();
                Datos.LimpiarParametros();
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

        public void ModificarUnidad(Unidad unidad)
        {
            try
            {
                Datos.SetearConsulta("update Unidades set Nombre = @Nombre, NroUnidad = @NroUnidad, Descripcion = @Descripcion where IDUnidad = @IDUnidad");
                Datos.SetearParametro("@IDUnidad", unidad.IDUnidad);
                Datos.SetearParametro("@Nombre", unidad.Nombre);
                Datos.SetearParametro("@NroUnidad", unidad.NroUnidad);
                Datos.SetearParametro("@Descripcion", unidad.Descripcion);
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
        public List<int> ListaIdUnidadXCurso(int idCurso)
        {
            List<int> lista = new List<int>();
            try
            {
                Datos.SetearConsulta("select IDUnidad from UnidadesXCurso where IDCurso = @IDCurso");
                Datos.SetearParametro("@IDCurso", idCurso);
                Datos.EjecutarLectura();
                while (Datos.Lector.Read())
                {
                    lista.Add((int)Datos.Lector["IDUnidad"]);
                }
                return lista;
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

        public string visibilidadUnidad(int idUnidad)
        {
            UnidadNegocio unidadNegocio = new UnidadNegocio();
            string estadoUnidad;
            try
            {
                if (unidadNegocio.estadoUnidad(idUnidad))
                {
                    Datos.SetearConsulta("UPDATE UnidadesXCurso set Estado = 0 WHERE IDUnidad = @IDUnidad");
                    Datos.SetearParametro("@IDUnidad", idUnidad);
                    Datos.EjecutarAccion();
                    Datos.LimpiarParametros();
                    Datos.CerrarConexion();
                    estadoUnidad = "Deshabilitado";
                }
                else
                {
                    Datos.SetearConsulta("UPDATE UnidadesXCurso set Estado = 1 WHERE IDUnidad = @IDUnidad");
                    Datos.SetearParametro("@IDUnidad", idUnidad);
                    Datos.EjecutarAccion();
                    Datos.LimpiarParametros();
                    Datos.CerrarConexion();
                    estadoUnidad = "Habilitado";
                }
                return estadoUnidad;


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
        public bool estadoUnidad(int IdUnidad)
        {
            try
            {
                Datos.SetearConsulta("SELECT Estado FROM UnidadesXCurso WHERE IDUnidad = @IDUnidad");
                Datos.SetearParametro("@IDUnidad", IdUnidad);
                Datos.EjecutarLectura();

                if (Datos.Lector.Read())
                {
                    return Convert.ToBoolean(Datos.Lector["Estado"]);
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
                Datos.CerrarConexion();
            }
        }
    }
}
