using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using AccesoDB;

namespace Negocio
{
    public class ReseniaNegocio
    {
        private Datos Datos;
        private EstudianteNegocio estudianteNegocio;
        public ReseniaNegocio()
        {
            Datos = new Datos();
            estudianteNegocio = new EstudianteNegocio();
        }
        public List<Resenia> ListarResenias()
        {
            List<Resenia> lista = new List<Resenia>();
            try
            {
                Datos.SetearConsulta("select r.IDResenia, r.IDEstudiante, r.Resenia, r.Calificacion, r.Fecha, rxc.IDCurso from Resenias r INNER JOIN ReseniasXCurso rxc on r.IDResenia= rxc.IDResenia");
                Datos.EjecutarLectura();
                while (Datos.Lector.Read())
                {
                    Resenia aux = new Resenia();
                    aux.IDResenia = (int)Datos.Lector["IDResenia"];
                    aux.IDCurso = (int)Datos.Lector["IDCurso"];
                    aux.Calificacion = (int)Datos.Lector["Calificacion"];
                    aux.Comentario = (string)Datos.Lector["Resenia"];
                    aux.FechaCreacion = (DateTime)Datos.Lector["Fecha"];
                    aux.Estudiante = new Estudiante();
                    aux.Estudiante.IDUsuario = (int)Datos.Lector["IDEstudiante"];
                    lista.Add(aux);
                }
                foreach (Resenia resenia in lista)
                {
                    resenia.Estudiante = estudianteNegocio.BuscarEstudiante(resenia.Estudiante.IDUsuario);
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
        public void CrearResenia(Resenia resenia)
        {
            try
            {
                Datos.SetearConsulta("insert into Resenias (IDEstudiante, Resenia, Calificacion, Fecha) values (@IDEstudiante, @Resenia, @Calificacion, @Fecha)");
                Datos.SetearParametro("@IDEstudiante", resenia.Estudiante.IDUsuario);
                Datos.SetearParametro("@Resenia", resenia.Comentario);
                Datos.SetearParametro("@Calificacion", resenia.Calificacion);
                Datos.SetearParametro("@Fecha", DateTime.Now);
                Datos.EjecutarAccion();
                Datos.CerrarConexion();

                resenia.IDResenia = UltimoID();

                Datos.SetearConsulta("insert into ReseniasXCurso (IDResenia, IDCurso) values (@IDResenia, @IDCurso)");
                Datos.SetearParametro("@IDResenia", resenia.IDResenia);
                Datos.SetearParametro("@IDCurso", resenia.IDCurso);
                Datos.EjecutarAccion();
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
        public int UltimoID()
        {
            int IdResenia = 0;
            try
            {
                Datos.SetearConsulta("Select top(1) IDResenia From Resenias order by IDResenia desc");
                Datos.EjecutarLectura();
                if (Datos.Lector.Read())
                {
                    IdResenia = (int)Datos.Lector["IDResenia"];
                }
                return IdResenia;
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
        public void ModificarResenia(Resenia resenia)
        {
            try
            {
                Datos.SetearConsulta("update Resenias set Resenia = @Resenia, Calificacion = @Calificacion where IDResenia = @IDResenia");
                Datos.SetearParametro("@Resenia", resenia.Comentario);
                Datos.SetearParametro("@Calificacion", resenia.Calificacion);
                Datos.SetearParametro("@IDResenia", resenia.IDResenia);
                Datos.EjecutarAccion();
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
        public void EliminarResenia(int idResenia)
        {
            try
            {
                Datos.SetearConsulta("delete from ReseniasXCurso where IDResenia = @IDResenia");
                Datos.SetearParametro("@IDResenia", idResenia);
                Datos.EjecutarAccion();
                Datos.LimpiarParametros();
                Datos.CerrarConexion();

                Datos.SetearConsulta("delete from Resenias where IDResenia = @IDResenia");
                Datos.SetearParametro("@IDResenia", idResenia);
                Datos.EjecutarAccion();

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
        public bool ExisteReseniaUsuarioXCurso(int idEstudiante, int idCurso)
        {
            int idResenia = 0;
            try
            {
                Datos.SetearConsulta("select r.IDResenia from Resenias r INNER JOIN ReseniasXCurso rxc on r.IDResenia= rxc.IDResenia where r.IDEstudiante = @IDEstudiante and rxc.IDCurso = @IDCurso");
                Datos.SetearParametro("@IDEstudiante", idEstudiante);
                Datos.SetearParametro("@IDCurso", idCurso);
                Datos.EjecutarLectura();
                if (Datos.Lector.Read())
                {
                    idResenia = (int)Datos.Lector["IDResenia"];
                }

                if (idResenia != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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
        public List<Resenia> ListarReseniasXCurso(int IDCurso)
        {
            List<Resenia> lista = new List<Resenia>();
            try
            {
                Datos.SetearConsulta("select r.IDResenia, r.IDEstudiante, r.Resenia, r.Calificacion, r.Fecha from Resenias r INNER JOIN ReseniasXCurso rxc on r.IDResenia= rxc.IDResenia where rxc.IDCurso = @IDCurso");
                Datos.SetearParametro("@IDCurso", IDCurso);
                Datos.EjecutarLectura();
                while (Datos.Lector.Read())
                {
                    Resenia aux = new Resenia();
                    aux.IDResenia = (int)Datos.Lector["IDResenia"];
                    aux.Calificacion = (int)Datos.Lector["Calificacion"];
                    aux.Comentario = (string)Datos.Lector["Resenia"];
                    aux.FechaCreacion = (DateTime)Datos.Lector["Fecha"];
                    aux.IDCurso = IDCurso;
                    aux.Estudiante = new Estudiante();
                    aux.Estudiante.IDUsuario = (int)Datos.Lector["IDEstudiante"];
                    lista.Add(aux);
                }
                foreach (Resenia resenia in lista)
                {
                    resenia.Estudiante = estudianteNegocio.BuscarEstudiante(resenia.Estudiante.IDUsuario);
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
    }

}
