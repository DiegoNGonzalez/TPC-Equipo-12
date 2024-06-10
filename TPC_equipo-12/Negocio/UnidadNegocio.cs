using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using AccesoDB;

namespace Negocio
{
    public class UnidadNegocio
    {
        private Datos Datos;
        private LeccionNegocio LeccionesDeUnidad;
        public UnidadNegocio()
        {
            Datos = new Datos();
            LeccionesDeUnidad = new LeccionNegocio();
        }
        public List<Unidad> ListarUnidades(int IDCurso)
        {
            List<Unidad> lista = new List<Unidad>();
            try
            {
                Datos.SetearConsulta("select u.IDUnidad, u.Nombre, u.NroUnidad, u.Descripcion from Unidades u inner join UnidadesXCurso uxc on u.IDUnidad = uxc.IDUnidad Where uxc.IDCurso = @IDCurso");
                Datos.SetearParametro("@IDCurso", IDCurso);
                Datos.EjecutarLectura();
                while (Datos.Lector.Read())
                {
                    Unidad aux = new Unidad();
                    aux.IDUnidad = (int)Datos.Lector["IDUnidad"];
                    aux.Nombre = (string)Datos.Lector["Nombre"];
                    aux.NroUnidad = (int)Datos.Lector["NroUnidad"];
                    aux.Descripcion = (string)Datos.Lector["Descripcion"];
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
                Datos.SetearParametro("@NroUnidad", unidad.NroUnidad);
                Datos.SetearParametro("@Nombre", unidad.Nombre);
                Datos.SetearParametro("@Descripcion", unidad.Descripcion);
                Datos.EjecutarAccion();
                Datos.CerrarConexion();

                Datos.SetearConsulta("Select top(1) IDUnidad From Unidades order by IDUnidad desc");
                Datos.EjecutarLectura();
                if (Datos.Lector.Read())
                {
                    unidad.IDUnidad = Datos.Lector.GetInt32(0);
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

    }
}
