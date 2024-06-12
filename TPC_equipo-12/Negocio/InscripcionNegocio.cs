using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using AccesoDB;
using System.Security.Cryptography.X509Certificates;
using System.Collections;

namespace Negocio
{
    public class InscripcionNegocio
    {
        private Datos Datos;
        private CursoNegocio cursoNegocio;
        private UsuarioNegocio usuarioNegocio;
        private EstudianteNegocio estudianteNegocio;
        

        public InscripcionNegocio()
        {
            Datos = new Datos();
            cursoNegocio = new CursoNegocio();
            usuarioNegocio = new UsuarioNegocio();
            estudianteNegocio = new EstudianteNegocio();
        }
        public List<InscripcionACurso> listarInscripciones()
        {
            List <InscripcionACurso> lista = new List<InscripcionACurso>();
            try
            {
                Datos.SetearConsulta("select i.IdInscripcion,i. IdUsuario, i.IdCurso,i.FechaInscripcion, u.Nombre, u.Apellido, c.Nombre as NombreCurso, i.Estado from Inscripciones i inner join Usuarios u on i.IDusuario= u.IDUsuario INNER JOIN Cursos c on i.IDCurso= c.IDCurso where i.Estado= 'P' order by i.FechaInscripcion desc");
                Datos.EjecutarLectura();
                while (Datos.Lector.Read())
                {
                    InscripcionACurso aux = new InscripcionACurso();
                    aux.IDInscripcion = Datos.Lector.GetInt32(0);
                    aux.Curso = new Curso();
                    aux.Curso= cursoNegocio.BuscarCurso((int)Datos.Lector["IdCurso"]);
                    aux.Usuario=new Usuario();
                    aux.Usuario = usuarioNegocio.buscarUsuario((int)Datos.Lector["IdUsuario"]);
                    aux.Estado = Convert.ToChar(Datos.Lector["Estado"]);
                    aux.FechaInscripcion = (DateTime)Datos.Lector["FechaInscripcion"];
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
        
        public bool Incripcion(Usuario usuario, Curso curso)
        {

            try
            {
                if(EstaInscripto(usuario.IDUsuario, curso.IDCurso))
                {
                    throw new Exception("El estudiante ya esta inscripto en el curso");
                }
                else
                {

                    Datos.SetearConsulta("insert into Inscripciones (IDUsuario, IDCurso, FechaInscripcion) values (@IDUsuario, @IDCurso, @FechaInscripcion)");
                    Datos.SetearParametro("@IDUsuario", usuario.IDUsuario);
                    Datos.SetearParametro("@IDCurso", curso.IDCurso);
                    Datos.SetearParametro("@FechaInscripcion", DateTime.Now);
                    Datos.EjecutarAccion();
                    return true;
                }

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
        public void ConfirmarInscripcion(InscripcionACurso inscripcion)
        {
            Usuario aux = inscripcion.Usuario;
            Curso curso = inscripcion.Curso;
            try
            {
                if (EstaInscripto(aux.IDUsuario, curso.IDCurso))
                {
                    throw new Exception("El estudiante ya esta inscripto en el curso");

                }
                else
                {
                    if (estudianteNegocio.EsEstudiante(aux.IDUsuario))
                    {

                        estudianteNegocio.CargarEstudianteEnCurso(aux.IDUsuario, curso.IDCurso);
                    }
                    else
                    {
                        estudianteNegocio.Agregar(aux);
                        estudianteNegocio.CargarEstudianteEnCurso(aux.IDUsuario, curso.IDCurso);
                    }
                    Datos.LimpiarParametros();
                    ModificarEstadoInscripcion(inscripcion.IDInscripcion);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
        public InscripcionACurso BuscarInscripcion(int idInscripcion)
        {
            InscripcionACurso aux = new InscripcionACurso();

            try
            {
                Datos.SetearConsulta("select i.IdInscripcion,i. IdUsuario, i.IdCurso,i.FechaInscripcion, u.Nombre, u.Apellido, c.Nombre as NombreCurso, i.Estado from Inscripciones i inner join Usuarios u on i.IDusuario= u.IDUsuario INNER JOIN Cursos c on i.IDCurso= c.IDCurso where i.IdInscripcion = @IDInscripcion" );
                Datos.SetearParametro("@IDInscripcion", idInscripcion);
                Datos.EjecutarLectura();
                while (Datos.Lector.Read())
                {
                    aux.IDInscripcion = Datos.Lector.GetInt32(0);
                    aux.Curso = new Curso();
                    aux.Curso = cursoNegocio.BuscarCurso((int)Datos.Lector["IdCurso"]);
                    aux.Usuario = new Usuario();
                    aux.Usuario = usuarioNegocio.buscarUsuario((int)Datos.Lector["IdUsuario"]);
                    aux.Estado = Convert.ToChar(Datos.Lector["Estado"]);
                    aux.FechaInscripcion = (DateTime)Datos.Lector["FechaInscripcion"];
                    
                }
                return aux;
            }
            catch (Exception)
            {

                throw;
            }finally
            {
                Datos.CerrarConexion();
            }
        }
        public void ModificarEstadoInscripcion(int idInscripcion)
        {
            try
            {
                Datos.SetearConsulta("update Inscripciones set Estado= 'A' where IdInscripcion= @IDInscripcion");
                Datos.SetearParametro("@IDInscripcion", idInscripcion);
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
        public bool EstaInscripto(int idEstudiante, int idCurso)
        {
            try
            {
                Datos.SetearConsulta("select * from EstudiantesXCursos where IDEstudiante = @IdEstudiante and IDCurso = @IdCurso");
                Datos.SetearParametro("@IdEstudiante", idEstudiante);
                Datos.SetearParametro("@IdCurso", idCurso);
                Datos.EjecutarLectura();
                if (Datos.Lector.Read())
                {
                    return true;

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
                Datos.LimpiarParametros();
                Datos.CerrarConexion();
            }
        }
        public List<InscripcionACurso> listarInscripcionesXEstudiante(int idEstudiante)
        {
            List<InscripcionACurso> lista = new List<InscripcionACurso>();
            try
            {
                Datos.SetearConsulta("select i.IdInscripcion,i. IdUsuario, i.IdCurso,i.FechaInscripcion, u.Nombre, u.Apellido, c.Nombre as NombreCurso, i.Estado from Inscripciones i inner join Usuarios u on i.IDusuario= u.IDUsuario INNER JOIN Cursos c on i.IDCurso= c.IDCurso where i.IDUsuario=@IdEstudiante order by i.FechaInscripcion desc");
                Datos.SetearParametro("@IdEstudiante", idEstudiante);
                Datos.EjecutarLectura();
                while (Datos.Lector.Read())
                {
                    InscripcionACurso aux = new InscripcionACurso();
                    aux.IDInscripcion = Datos.Lector.GetInt32(0);
                    aux.Curso = new Curso();
                    aux.Curso = cursoNegocio.BuscarCurso((int)Datos.Lector["IdCurso"]);
                    aux.Usuario = new Usuario();
                    aux.Usuario = usuarioNegocio.buscarUsuario((int)Datos.Lector["IdUsuario"]);
                    aux.Estado =Convert.ToChar(Datos.Lector["Estado"]);
                    aux.FechaInscripcion = (DateTime)Datos.Lector["FechaInscripcion"];
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
