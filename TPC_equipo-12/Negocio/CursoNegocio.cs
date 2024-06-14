using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using AccesoDB;
using System.Collections;
using System.Diagnostics.Eventing.Reader;

namespace Negocio
{
    public class CursoNegocio
    {
        public Datos Datos;
        private UnidadNegocio UnidadesDeCurso;
        public CursoNegocio()
        {
            Datos = new Datos();
            UnidadesDeCurso = new UnidadNegocio();
        }
        public List<Curso> ListarCursos()
        {
            List<Curso> lista = new List<Curso>();
            try
            {
                Datos.SetearConsulta("select c.IDCurso, c.Nombre, c.Descripcion, c.Estreno, c.Duracion,c.IDImagen, i.IDImagenes, i.URLIMG from cursos c inner join Imagenes i on c.IDImagen= i.IDImagenes;");
                Datos.EjecutarLectura();
                while (Datos.Lector.Read())
                {
                    Curso aux = new Curso();
                    aux.IDCurso = Datos.Lector.GetInt32(0);
                    aux.Nombre = (string)Datos.Lector["Nombre"];
                    aux.Descripcion = (string)Datos.Lector["Descripcion"];
                    aux.Estreno = (DateTime)Datos.Lector["Estreno"];
                    aux.Duracion = (int)Datos.Lector["Duracion"];
                    aux.Imagen = new Imagen();
                    aux.Imagen.IDImagen = (int)Datos.Lector["IDImagenes"];
                    aux.Imagen.URL = (string)Datos.Lector["URLIMG"];
                    lista.Add(aux);
                }
                foreach (var item in lista)
                {
                    item.Unidades = UnidadesDeCurso.ListarUnidades(item.IDCurso);
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
        public List<int> IDCursosXEstudiante(int idEstudiante)
        {

            List<int> idCursos = new List<int>();
            try
            {
                Datos.SetearConsulta("Select IDCurso From EstudiantesXCursos Where IDEstudiante=@IDEstudiante");
                Datos.SetearParametro("@IDEstudiante", idEstudiante);
                Datos.EjecutarLectura();
                while (Datos.Lector.Read())
                {
                    idCursos.Add((int)Datos.Lector["IDCurso"]);
                }
                Datos.CerrarConexion();

                return idCursos;
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
        public Curso BuscarCurso(int idCurso)
        {
            Curso aux = new Curso();
            try
            {
                Datos.SetearConsulta("select c.IDCurso, c.Nombre, c.Descripcion, c.Estreno, c.Duracion,c.IDImagen, i.IDImagenes, i.URLIMG from cursos c inner join Imagenes i on c.IDImagen= i.IDImagenes where c.IDCurso=@IDCurso");
                Datos.SetearParametro("@IDCurso", idCurso);
                Datos.EjecutarLectura();
                while (Datos.Lector.Read())
                {
                    aux.IDCurso = Datos.Lector.GetInt32(0);
                    aux.Nombre = (string)Datos.Lector["Nombre"];
                    aux.Descripcion = (string)Datos.Lector["Descripcion"];
                    aux.Estreno = (DateTime)Datos.Lector["Estreno"];
                    aux.Duracion = (int)Datos.Lector["Duracion"];
                    aux.Imagen = new Imagen();
                    if (Datos.Lector["IDImagenes"] != DBNull.Value)
                    {
                        aux.Imagen.IDImagen = (int)Datos.Lector["IDImagenes"];
                        aux.Imagen.URL = (string)Datos.Lector["URLIMG"];
                    }
                    else
                    {
                        aux.Imagen.IDImagen = 0;
                    }

                }
                Datos.LimpiarParametros();
                return aux;
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

        public void CrearCurso(Curso curso)
        {
            try
            {
                if (curso.Imagen.URL != "")
                {
                    Datos.SetearConsulta("insert into Imagenes (URLIMG) values (@URLIMG)");
                    Datos.SetearParametro("@URLIMG", curso.Imagen.URL);
                    Datos.EjecutarAccion();
                    Datos.CerrarConexion();
                }
                else
                {
                    Datos.SetearConsulta("insert into Imagenes (URLIMG) values (https://vilmanunez.com/wp-content/uploads/2016/04/VN-Como-crear-el-mejor-temario-de-tu-curso-online-Incluye-plantillas.png)");
                    Datos.EjecutarAccion();
                    Datos.CerrarConexion();
                }

                Datos.SetearConsulta("select IDImagenes from Imagenes where URLIMG=@URLIMG");
                Datos.SetearParametro("@URLIMG", curso.Imagen.URL);
                Datos.EjecutarLectura();
                while (Datos.Lector.Read())
                {
                    curso.Imagen.IDImagen = (int)Datos.Lector["IDImagenes"];
                }
                Datos.CerrarConexion();

                Datos.SetearConsulta("insert into Cursos (Nombre, Descripcion, Duracion, Estreno, IDImagen) values (@Nombre, @Descripcion, @Duracion, @Estreno, @IDImagen)");
                Datos.SetearParametro("@Nombre", curso.Nombre);
                Datos.SetearParametro("@Descripcion", curso.Descripcion);
                Datos.SetearParametro("@Duracion", curso.Duracion);
                Datos.SetearParametro("@Estreno", curso.Estreno);
                Datos.SetearParametro("@IDImagen", curso.Imagen.IDImagen);
                Datos.EjecutarAccion();
                Datos.CerrarConexion();

                Datos.SetearConsulta("Select top(1) IDCurso From Cursos order by IDCurso desc");
                Datos.EjecutarLectura();
                if (Datos.Lector.Read())
                {
                    curso.IDCurso = (int)Datos.Lector["IDCurso"];
                }
                Datos.CerrarConexion();
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

        public void EliminarCurso(int idCurso)
        {
            try
            {
                Datos.SetearConsulta("delete from ProfesorXCursos where IDCurso = @IDCurso");
                Datos.SetearParametro("@IDCurso", idCurso);
                Datos.EjecutarAccion();
                Datos.LimpiarParametros();
                Datos.CerrarConexion();

                Datos.SetearConsulta("delete from EstudiantesXCursos where IDCurso = @IDCurso");
                Datos.SetearParametro("@IDCurso", idCurso);
                Datos.EjecutarAccion();
                Datos.LimpiarParametros();
                Datos.CerrarConexion();

                Datos.SetearConsulta("delete from Inscripciones where IDCurso = @IDCurso");
                Datos.SetearParametro("@IDCurso", idCurso);
                Datos.EjecutarAccion();
                Datos.LimpiarParametros();
                Datos.CerrarConexion();

                Datos.SetearConsulta("delete from CategoriasXCurso where IDCurso = @IDCurso");
                Datos.SetearParametro("@IDCurso", idCurso);
                Datos.EjecutarAccion();
                Datos.LimpiarParametros();
                Datos.CerrarConexion();

                Datos.SetearConsulta("Select IDUnidad from UnidadesXCurso where IDCurso = @IDCurso");
                Datos.SetearParametro("@IDCurso", idCurso);
                Datos.EjecutarLectura();
                while (Datos.Lector.Read())
                {
                    Datos.SetearConsulta("Select IDLeccion from LeccionesXUnidad where IDUnidad = @IDUnidad");
                    Datos.SetearParametro("@IDUnidad", (int)Datos.Lector["IDUnidad"]);
                    Datos.EjecutarLectura();
                    while (Datos.Lector.Read())
                    {
                            Datos.SetearConsulta("delete from LeccionesXEstudiante where IDLeccion = @IDLeccion");
                            Datos.SetearParametro("@IDLeccion", (int)Datos.Lector["IDLeccion"]);
                            Datos.EjecutarAccion();
                    }
                    UnidadesDeCurso.EliminarUnidad((int)Datos.Lector["IDUnidad"]);
                }
                Datos.LimpiarParametros();
                Datos.CerrarConexion();

                Datos.SetearConsulta("delete from Cursos where IDCurso = @IDCurso");
                Datos.SetearParametro("@IDCurso", idCurso);
                Datos.EjecutarAccion();
                Datos.CerrarConexion();
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

        public void ModificarCurso(Curso curso)
        {
            int IDImagen = 0;
            try
            {
                Datos.SetearConsulta("Select IDImagen from Cursos where IDCurso = @IDCurso");
                Datos.SetearParametro("@IDCurso", curso.IDCurso);
                Datos.EjecutarLectura();
                if (Datos.Lector.Read())
                {
                    IDImagen = (int)Datos.Lector["IDImagen"];
                }
                Datos.LimpiarParametros();
                Datos.CerrarConexion();

                if (IDImagen != 0)
                {
                    try
                    {
                        if (curso.Imagen.URL != "")
                        {
                            Datos.SetearConsulta("update Imagenes set URLIMG = @URLIMG where IDImagenes = @IDImagenes");
                            Datos.SetearParametro("@URLIMG", curso.Imagen.URL);
                            Datos.SetearParametro("@IDImagenes", IDImagen);
                            Datos.EjecutarAccion();
                            Datos.CerrarConexion();
                        }
                        else
                        {
                            Datos.SetearConsulta("insert into Imagenes (URLIMG) values (https://vilmanunez.com/wp-content/uploads/2016/04/VN-Como-crear-el-mejor-temario-de-tu-curso-online-Incluye-plantillas.png)");
                            Datos.EjecutarAccion();
                            Datos.CerrarConexion();
                        }

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    Datos.SetearConsulta("update Cursos set Nombre = @Nombre, Descripcion = @Descripcion, Duracion = @Duracion, Estreno = @Estreno where IDCurso = @IDCurso");
                    Datos.SetearParametro("@Nombre", curso.Nombre);
                    Datos.SetearParametro("@Descripcion", curso.Descripcion);
                    Datos.SetearParametro("@Duracion", curso.Duracion);
                    Datos.SetearParametro("@Estreno", curso.Estreno);
                    Datos.SetearParametro("@IDCurso", curso.IDCurso);
                    Datos.EjecutarAccion();
                    Datos.CerrarConexion();
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
