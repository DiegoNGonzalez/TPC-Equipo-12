using AccesoDB;
using Dominio;
using System;
using System.Collections.Generic;
using System.Reflection;
namespace Negocio
{
    public class MaterialNegocio
    {
        private Datos Datos;
        public MaterialNegocio()
        {
            Datos = new Datos();
        }
        public List<MaterialLeccion> ListarMateriales(int idLeccion)
        {
            List<MaterialLeccion> lista = new List<MaterialLeccion>();
            try
            {
                Datos.SetearConsulta("SELECT m.IDMaterial, m.Nombre, m.TipoMaterial, m.URLMaterial, m.Descripcion, m.NroMaterial, mxl.Estado FROM Materiales m INNER JOIN MaterialesXLecciones mxl ON mxl.IDMaterial = m.IDMaterial INNER JOIN Lecciones l ON mxl.IDLeccion = l.IDLeccion WHERE l.IDLeccion = @idLeccion");
                Datos.SetearParametro("@idLeccion", idLeccion);
                Datos.EjecutarLectura();
                while (Datos.Lector.Read())
                {
                    MaterialLeccion aux = new MaterialLeccion();
                    aux.IDMaterial = (int)Datos.Lector["IDMaterial"];
                    aux.Nombre = (string)Datos.Lector["Nombre"];
                    aux.TipoMaterial = (string)Datos.Lector["TipoMaterial"];
                    aux.URL = (string)Datos.Lector["URLMaterial"];
                    aux.Descripcion = (string)Datos.Lector["Descripcion"];
                    aux.NroMaterial = (int)Datos.Lector["NroMaterial"];
                    aux.Estado = (bool)Datos.Lector["Estado"];
                    lista.Add(aux);
                }
                Datos.LimpiarParametros();
                return lista;
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

        public void CrearMaterial(MaterialLeccion material, int idLeccion)
        {
            try
            {
                Datos.SetearConsulta("insert into Materiales (Nombre, TipoMaterial, URLMaterial, Descripcion, NroMaterial) values (@Nombre, @TipoMaterial, @URLMaterial, @Descripcion, @NroMaterial)");
                Datos.SetearParametro("@Nombre", material.Nombre);
                Datos.SetearParametro("@TipoMaterial", material.TipoMaterial);
                Datos.SetearParametro("@URLMaterial", material.URL);
                Datos.SetearParametro("@Descripcion", material.Descripcion);
                Datos.SetearParametro("@NroMaterial", material.NroMaterial);
                Datos.EjecutarAccion();
                Datos.CerrarConexion();

                Datos.SetearConsulta("Select top(1) IDMaterial From Materiales order by IDMaterial desc");
                Datos.EjecutarLectura();
                if (Datos.Lector.Read())
                {
                    material.IDMaterial = (int)Datos.Lector["IDMaterial"];
                }
                Datos.CerrarConexion();

                Datos.SetearConsulta("insert into MaterialesXLecciones (IDMaterial, IDLeccion) values (@IDMaterial, @IDLeccion)");
                Datos.SetearParametro("@IDMaterial", material.IDMaterial);
                Datos.SetearParametro("@IDLeccion", idLeccion);
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
        public void ModificarMaterial(MaterialLeccion material)
        {
            try
            {
                Datos.SetearConsulta("update Materiales set Nombre = @Nombre, TipoMaterial = @TipoMaterial, URLMaterial = @URLMaterial, Descripcion = @Descripcion, NroMaterial = @NroMaterial where IDMaterial = @IDMaterial");
                Datos.SetearParametro("@IDMaterial", material.IDMaterial);
                Datos.SetearParametro("@Nombre", material.Nombre);
                Datos.SetearParametro("@TipoMaterial", material.TipoMaterial);
                Datos.SetearParametro("@URLMaterial", material.URL);
                Datos.SetearParametro("@Descripcion", material.Descripcion);
                Datos.SetearParametro("@NroMaterial", material.NroMaterial);
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

        public string visibilidadMaterial(int idMaterial)
        {
            MaterialNegocio materialNegocio = new MaterialNegocio();
            string estadoMaterial;
            try
            {
                if (materialNegocio.estadoMaterial(idMaterial))
                {
                    Datos.SetearConsulta("UPDATE MaterialesXLecciones set Estado = 0 WHERE IDMaterial = @IDMaterial");
                    Datos.SetearParametro("@IDMaterial", idMaterial);
                    Datos.EjecutarAccion();
                    Datos.LimpiarParametros();
                    Datos.CerrarConexion();
                    estadoMaterial = "Deshabilitado";
                }else
                {
                    Datos.SetearConsulta("UPDATE MaterialesXLecciones set Estado = 1 WHERE IDMaterial = @IDMaterial");
                    Datos.SetearParametro("@IDMaterial", idMaterial);
                    Datos.EjecutarAccion();
                    Datos.LimpiarParametros();
                    Datos.CerrarConexion();
                    estadoMaterial = "Habilitado";
                }
                return estadoMaterial;


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
        public bool estadoMaterial(int idMaterial)
        {
            try
            {
                Datos.SetearConsulta("SELECT Estado FROM MaterialesXLecciones WHERE IDMaterial = @idMaterial");
                Datos.SetearParametro("@idMaterial", idMaterial);
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
