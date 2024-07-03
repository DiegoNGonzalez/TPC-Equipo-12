using AccesoDB;
using Dominio;
using System;
using System.Collections.Generic;

namespace Negocio
{
    public class LeccionNegocio
    {
        private Datos datos;
        private MaterialNegocio MaterialesDeLeccion;
        public LeccionNegocio()
        {
            datos = new Datos();
            MaterialesDeLeccion = new MaterialNegocio();
        }
        public List<Leccion> ListarLecciones(int IDUnidad)
        {
            List<Leccion> lista = new List<Leccion>();
            try
            {
                datos.SetearConsulta("select l.Idleccion, l.nombre, l.nroLeccion, l.Descripcion, lxu.Estado from Lecciones l inner JOIN LeccionesXUnidades lxu on lxu.IDLeccion = l.IDLeccion inner join Unidades u on u.IDUnidad = lxu.IDUnidad Where u.IDUnidad = @IDUnidad");
                datos.SetearParametro("@IDUnidad", IDUnidad);
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    Leccion aux = new Leccion();
                    aux.IDLeccion = (int)datos.Lector["Idleccion"];
                    aux.NroLeccion = (int)datos.Lector["nroLeccion"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Estado = (bool)datos.Lector["Estado"];
                    lista.Add(aux);
                }
                foreach (var item in lista)
                {
                    item.Materiales = MaterialesDeLeccion.ListarMateriales(item.IDLeccion);
                }
                datos.LimpiarParametros();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                datos.CerrarConexion();
            }
            return lista;
        }

        public void CrearLeccion(Leccion leccion, int IDUnidad)
        {
            try
            {
                datos.SetearConsulta("insert into Lecciones (Nombre, NroLeccion, Descripcion) values (@Nombre, @NroLeccion, @Descripcion)");
                datos.SetearParametro("@Nombre", leccion.Nombre);
                datos.SetearParametro("@NroLeccion", leccion.NroLeccion);
                datos.SetearParametro("@Descripcion", leccion.Descripcion);
                datos.EjecutarAccion();
                datos.CerrarConexion();

                datos.SetearConsulta("Select top(1) IDLeccion From Lecciones order by IDLeccion desc");
                datos.EjecutarLectura();
                if (datos.Lector.Read())
                {
                    leccion.IDLeccion = (int)datos.Lector["IDLeccion"];
                }
                datos.CerrarConexion();

                datos.SetearConsulta("insert into LeccionesXUnidades (IDLeccion, IDUnidad) values (@IDLeccion, @IDUnidad)");
                datos.SetearParametro("@IDLeccion", leccion.IDLeccion);
                datos.SetearParametro("@IDUnidad", IDUnidad);
                datos.EjecutarAccion();
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

        public void EliminarLeccion(int IDLeccion)
        {
            int idMaterial = 0;
            try
            {
                datos.SetearConsulta("delete from LeccionesXUnidades where IDLeccion = @IDLeccion");
                datos.SetearParametro("@IDLeccion", IDLeccion);
                datos.EjecutarAccion();
                datos.LimpiarParametros();
                datos.CerrarConexion();

                datos.SetearConsulta("Select IDMaterial from MaterialesXLecciones where IDLeccion = @IDLeccion");
                datos.SetearParametro("@IDLeccion", IDLeccion);
                datos.EjecutarLectura();
                if (datos.Lector.Read())
                {
                    idMaterial = (int)datos.Lector["IDMaterial"];
                    MaterialesDeLeccion.EliminarMaterial(idMaterial);
                }
                datos.LimpiarParametros();
                datos.CerrarConexion();

                datos.SetearConsulta("delete from Lecciones where IDLeccion = @IDLeccion");
                datos.SetearParametro("@IDLeccion", IDLeccion);
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

        public void ModificarLeccion(Leccion leccion)
        {
            try
            {
                datos.SetearConsulta("update Lecciones set Nombre = @Nombre, NroLeccion = @NroLeccion, Descripcion = @Descripcion where IDLeccion = @IDLeccion");
                datos.SetearParametro("@Nombre", leccion.Nombre);
                datos.SetearParametro("@NroLeccion", leccion.NroLeccion);
                datos.SetearParametro("@Descripcion", leccion.Descripcion);
                datos.SetearParametro("@IDLeccion", leccion.IDLeccion);
                datos.EjecutarAccion();
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

        public List<int> ListarIdLeccionXUnidad(int idUnidad)
        {
            List<int> lista = new List<int>();
            try
            {
                datos.SetearConsulta("Select IDLeccion from LeccionesXUnidades where IDUnidad = @IDUnidad");
                datos.SetearParametro("@IDUnidad", idUnidad);
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    lista.Add((int)datos.Lector["IDLeccion"]);
                }
                return lista;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                datos.LimpiarParametros();
                datos.CerrarConexion();
            }
        }
        public void BorrarLeccionesXEstudiante(int idLeccion)
        {
            try
            {
                datos.SetearConsulta("delete from LeccionesXEstudiantes where IDLeccion = @IDLeccion");
                datos.SetearParametro("@IDLeccion", idLeccion);
                datos.EjecutarAccion();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                datos.LimpiarParametros();
                datos.CerrarConexion();
            }
        }
        public Leccion BuscarLeccion(int idLeccion)
        {
            Leccion aux = new Leccion();
            try
            {
                datos.SetearConsulta("select * from Lecciones where IDLeccion = @IDLeccion");
                datos.SetearParametro("@IDLeccion", idLeccion);
                datos.EjecutarLectura();
                if (datos.Lector.Read())
                {
                    aux.IDLeccion = (int)datos.Lector["IDLeccion"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.NroLeccion = (int)datos.Lector["NroLeccion"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                }
                return aux;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                datos.LimpiarParametros();
                datos.CerrarConexion();
            }
        }

        public void visibilidadLeccion(int IDLeccion)
        {
            LeccionNegocio leccionNegocio = new LeccionNegocio();
            try
            {
                if (leccionNegocio.estadoLeccion(IDLeccion))
                {
                    datos.SetearConsulta("UPDATE LeccionesXUnidades set Estado = 0 WHERE IDLeccion = @IDLeccion");
                    datos.SetearParametro("@IDLeccion", IDLeccion);
                    datos.EjecutarAccion();
                    datos.LimpiarParametros();
                    datos.CerrarConexion();
                }
                else
                {
                    datos.SetearConsulta("UPDATE LeccionesXUnidades set Estado = 1 WHERE IDLeccion = @IDLeccion");
                    datos.SetearParametro("@IDLeccion", IDLeccion);
                    datos.EjecutarAccion();
                    datos.LimpiarParametros();
                    datos.CerrarConexion();
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

        private bool estadoLeccion(int iDLeccion)
        {
            try
            {
                datos.SetearConsulta("SELECT Estado FROM LeccionesXUnidades WHERE IDLeccion = @iDLeccion");
                datos.SetearParametro("@iDLeccion", iDLeccion);
                datos.EjecutarLectura();

                if (datos.Lector.Read())
                {
                    return Convert.ToBoolean(datos.Lector["Estado"]);
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
                datos.CerrarConexion();
            }
        }
    }
}
