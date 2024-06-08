using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using AccesoDB;

namespace Negocio
{
    public class InscripcionNegocio
    {
        private Datos Datos;
        private List<InscripcionACurso> inscripciones;

        public InscripcionNegocio()
        {
            Datos = new Datos();
        }

        public bool Incripcion(Usuario usuario, Curso curso)
        {

            try
            {
                Datos.SetearConsulta("insert into Inscripciones (IDUsuario, IDCurso) values (@IDUsuario, @IDCurso)");
                Datos.SetearParametro("@IDUsuario", usuario.IDUsuario);
                Datos.SetearParametro("@IDCurso", curso.IDCurso);
                Datos.EjecutarAccion();
                return true;

            }
            catch (Exception ex)
            {

                throw ex ;

            }finally
            {
                Datos.CerrarConexion();
            }
            return false;
        }
    }
}
