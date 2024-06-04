using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using AccesoDB;

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
                datos.SetearConsulta("select l.Idleccion, l.nombre, l.nroLeccion, l.Descripcion from Lecciones l inner JOIN LeccionesXUnidades lxu on lxu.IDLeccion = l.IDLeccion inner join Unidades u on @IDUnidad = lxu.IDUnidad");
                datos.SetearParametro("@IDUnidad", IDUnidad);
                datos.EjecutarLectura();
                while (datos.Lector.Read())
                {
                    Leccion aux = new Leccion();
                    aux.IDLeccion = datos.Lector.GetInt32(0);
                    aux.NroLeccion = datos.Lector.GetInt32(1);
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    lista.Add(aux);
                }
                foreach (var item in lista)
                {
                    item.Materiales = MaterialesDeLeccion.ListarMateriales(item.IDLeccion);
                }
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
    }
}
