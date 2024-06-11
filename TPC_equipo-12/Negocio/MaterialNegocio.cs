using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using AccesoDB;
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
                Datos.SetearConsulta("select m.IDMaterial, m.Nombre, m.TipoMaterial, m.URLMaterial, m.Descripcion from Materiales m inner join materialesxlecciones mxl on mxl.IDMaterial = m.IDmaterial inner join lecciones l on mxl.Idleccion = l.IDLeccion Where l.IDLeccion = @idLeccion");
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
                Datos.SetearConsulta("insert into Materiales (Nombre, TipoMaterial, URLMaterial, Descripcion) values (@Nombre, @TipoMaterial, @URLMaterial, @Descripcion)");
                Datos.SetearParametro("@Nombre", material.Nombre);
                Datos.SetearParametro("@TipoMaterial", material.TipoMaterial);
                Datos.SetearParametro("@URLMaterial", material.URL);
                Datos.SetearParametro("@Descripcion", material.Descripcion);
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
    }
}
