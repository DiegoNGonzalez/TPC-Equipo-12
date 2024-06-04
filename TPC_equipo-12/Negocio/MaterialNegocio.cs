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
                Datos.SetearConsulta("select m.IDMaterial, m.Nombre, m.TipoMaterial, m.URLMaterial from Materiales m inner join materialesxlecciones mxl on mxl.IDMaterial = m.IDmaterial inner join lecciones l on mxl.Idleccion = l.IDLeccion Where l.IDLeccion = @idLeccion");
                Datos.SetearParametro("@IDLeccion", idLeccion);
                Datos.EjecutarLectura();
                while (Datos.Lector.Read())
                {
                    MaterialLeccion aux = new MaterialLeccion();
                    aux.IDMaterial = Datos.Lector.GetInt32(0);
                    aux.Nombre = (string)Datos.Lector["Nombre"];
                    aux.TipoMaterial = (string)Datos.Lector["TipoMaterial"];
                    aux.URL = (string)Datos.Lector["URL"];
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
                Datos.CerrarConexion();
            }
        }
    }
}
