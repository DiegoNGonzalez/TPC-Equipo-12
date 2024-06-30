using AccesoDB;
using Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;

namespace Negocio
{
    public class CursoNegocio
    {
        public Datos Datos;
        private UnidadNegocio UnidadesDeCurso;
        private CategoriaNegocio CategoriaNegocio;
        private EstudianteNegocio EstudianteNegocio;
        private InscripcionNegocio InscripcionNegocio;
        private LeccionNegocio LeccionNegocio;
        private MaterialNegocio MaterialNegocio;
        private ReseniaNegocio ReseniaNegocio;

        private ProfesorNegocio ProfesorNegocio;
        private NotificacionNegocio NotificacionNegocio;

        public CursoNegocio()
        {
            Datos = new Datos();
            UnidadesDeCurso = new UnidadNegocio();
            CategoriaNegocio = new CategoriaNegocio();
            EstudianteNegocio = new EstudianteNegocio();
            InscripcionNegocio = new InscripcionNegocio(true);
            LeccionNegocio = new LeccionNegocio();
            MaterialNegocio = new MaterialNegocio();
            ReseniaNegocio = new ReseniaNegocio();
            ProfesorNegocio = new ProfesorNegocio();
            NotificacionNegocio = new NotificacionNegocio();

        }
        public List<Curso> ListarCursos()
        {
            List<Curso> lista = new List<Curso>();
            //try
            //{
            //    Datos.SetearConsulta("select c.IDCurso, c.Nombre, c.Descripcion, c.Estreno, c.Duracion,c.IDImagen, i.IDImagenes, i.URLIMG from cursos c inner join Imagenes i on c.IDImagen= i.IDImagenes;");
            //    Datos.EjecutarLectura();
            //    while (Datos.Lector.Read())
            //    {
            //        Curso aux = new Curso();
            //        aux.IDCurso = Datos.Lector.GetInt32(0);
            //        aux.Nombre = (string)Datos.Lector["Nombre"];
            //        aux.Descripcion = (string)Datos.Lector["Descripcion"];
            //        aux.Estreno = (DateTime)Datos.Lector["Estreno"];
            //        aux.Duracion = (int)Datos.Lector["Duracion"];
            //        aux.Imagen = new Imagen();
            //        aux.Imagen.IDImagen = (int)Datos.Lector["IDImagenes"];
            //        aux.Imagen.URL = (string)Datos.Lector["URLIMG"];
            //        lista.Add(aux);
            //    }
            //    foreach (var item in lista)
            //    {
            //        item.Unidades = UnidadesDeCurso.ListarUnidades(item.IDCurso);
            //        item.Categoria = new CategoriaCurso();
            //        item.Categoria.Nombre = CategoriaNegocio.CategoriaNombreXIDCurso(item.IDCurso);
            //    }
            //    Datos.LimpiarParametros();
            //    return lista;
            //}
            //catch (Exception ex)
            //{

            //    throw ex;
            //}
            //finally
            //{
            //    Datos.CerrarConexion();
            //}
            try
            {
                Datos.SetearConsulta(@"
                        SELECT c.IDCurso, c.Nombre, c.Descripcion, c.Estreno, c.Duracion, c.Completo, 
                               c.IDImagen, i.IDImagenes, i.URLIMG, cx.IDCategoria, cat.Nombre as CategoriaNombre
                        FROM cursos c
                        INNER JOIN Imagenes i ON c.IDImagen = i.IDImagenes
                        LEFT JOIN CategoriasXCurso cx ON c.IDCurso = cx.IDCurso
                        LEFT JOIN Categorias cat ON cx.IDCategoria = cat.IDCategoria;
                    ");
                Datos.EjecutarLectura();
                while (Datos.Lector.Read())
                {
                    Curso aux = new Curso();
                    aux.IDCurso = (int)Datos.Lector["IDCurso"];
                    aux.Nombre = (String)Datos.Lector["Nombre"];
                    aux.Descripcion = (String)Datos.Lector["Descripcion"];
                    aux.Estreno = (DateTime)Datos.Lector["Estreno"];
                    aux.Duracion = (int)Datos.Lector["Duracion"];
                    aux.Completo = (bool)Datos.Lector["Completo"];
                    aux.Imagen = new Imagen
                    {
                        IDImagen = (int)Datos.Lector["IDImagen"],
                        URL = (String)Datos.Lector["URLIMG"]
                    };
                    aux.Categoria = new CategoriaCurso
                    {
                        IDCategoria = (int)Datos.Lector["IDCategoria"],
                        Nombre = (String)Datos.Lector["Nombre"]
                    };
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
                Datos.LimpiarParametros();
                Datos.CerrarConexion();
            }
        }
        public Curso BuscarCurso(int idCurso)
        {
            Curso aux = new Curso();
            try
            {
                Datos.SetearConsulta("select c.IDCurso, c.Nombre, c.Descripcion, c.Estreno, c.Duracion, C.Completo, c.IDImagen, i.IDImagenes, i.URLIMG from cursos c inner join Imagenes i on c.IDImagen= i.IDImagenes where c.IDCurso=@IDCurso");
                Datos.SetearParametro("@IDCurso", idCurso);
                Datos.EjecutarLectura();
                while (Datos.Lector.Read())
                {
                    aux.IDCurso = Datos.Lector.GetInt32(0);
                    aux.Nombre = (string)Datos.Lector["Nombre"];
                    aux.Descripcion = (string)Datos.Lector["Descripcion"];
                    aux.Estreno = (DateTime)Datos.Lector["Estreno"];
                    aux.Duracion = (int)Datos.Lector["Duracion"];
                    aux.Completo = (bool)Datos.Lector["Completo"];
                    aux.Unidades = new List<Unidad>();
                    aux.Categoria = new CategoriaCurso();
                    aux.Resenias = new List<Resenia>();
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
                aux.Unidades = UnidadesDeCurso.ListarUnidades(aux.IDCurso);
                aux.Categoria.Nombre = CategoriaNegocio.CategoriaNombreXIDCurso(aux.IDCurso);
                aux.Resenias = ReseniaNegocio.ListarReseniasXCurso(aux.IDCurso);
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

        public void CrearCurso(Curso curso, int idProfesor)
        {
            try
            {
                if (curso.Imagen.URL != "")
                {
                    Datos.SetearConsulta("insert into Imagenes (URLIMG) values (@URLIMG)");
                    Datos.SetearParametro("@URLIMG", curso.Imagen.URL);
                    Datos.EjecutarAccion();
                    Datos.LimpiarParametros();
                    Datos.CerrarConexion();

                }
                else
                {
                    Datos.SetearConsulta("insert into Imagenes (URLIMG) values (https://vilmanunez.com/wp-content/uploads/2016/04/VN-Como-crear-el-mejor-temario-de-tu-curso-online-Incluye-plantillas.png)");
                    Datos.EjecutarAccion();
                    Datos.LimpiarParametros();
                    Datos.CerrarConexion();
                }

                Datos.SetearConsulta("select IDImagenes from Imagenes where URLIMG=@URLIMG");
                Datos.SetearParametro("@URLIMG", curso.Imagen.URL);
                Datos.EjecutarLectura();
                while (Datos.Lector.Read())
                {
                    curso.Imagen.IDImagen = (int)Datos.Lector["IDImagenes"];
                }
                Datos.LimpiarParametros();
                Datos.CerrarConexion();

                Datos.SetearConsulta("insert into Cursos (Nombre, Descripcion, Duracion, Completo, Estreno, IDImagen) values (@Nombre, @Descripcion, @Duracion, @Completo, @Estreno, @IDImagen)");
                Datos.SetearParametro("@Nombre", curso.Nombre);
                Datos.SetearParametro("@Descripcion", curso.Descripcion);
                Datos.SetearParametro("@Duracion", curso.Duracion);
                Datos.SetearParametro("@Completo", curso.Completo);
                Datos.SetearParametro("@Estreno", curso.Estreno);
                Datos.SetearParametro("@IDImagen", curso.Imagen.IDImagen);
                Datos.EjecutarAccion();
                Datos.LimpiarParametros();
                Datos.CerrarConexion();

                Datos.SetearConsulta("Select top(1) IDCurso From Cursos order by IDCurso desc");
                Datos.EjecutarLectura();
                if (Datos.Lector.Read())
                {
                    curso.IDCurso = (int)Datos.Lector["IDCurso"];
                }
                Datos.LimpiarParametros();
                Datos.CerrarConexion();

                Datos.SetearConsulta("insert into ProfesorXCursos (IDCurso, IDProfesor) values (@IDCurso, @IDProfesor)");
                Datos.SetearParametro("@IDCurso", curso.IDCurso);
                Datos.SetearParametro("@IDProfesor", idProfesor);
                Datos.EjecutarAccion();
                Datos.LimpiarParametros();
                Datos.CerrarConexion();

                Datos.SetearConsulta("insert into CategoriasXCurso (IDCurso, IDCategoria) values (@IDCurso, @IDCategoria)");
                Datos.SetearParametro("@IDCurso", curso.IDCurso);
                Datos.SetearParametro("@IDCategoria", curso.Categoria.IDCategoria);
                Datos.EjecutarAccion();
                Datos.LimpiarParametros();
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
            List<int> idsInscripciones = new List<int>();
            List<int> idsNotificaciones = new List<int>();
            List<int> idsUnidades = new List<int>();
            List<int> idsLecciones = new List<int>();
            try
            {
                //Datos.SetearConsulta("delete from ProfesorXCursos where IDCurso = @IDCurso");
                //Datos.SetearParametro("@IDCurso", idCurso);
                //Datos.EjecutarAccion();
                //Datos.LimpiarParametros();
                //Datos.CerrarConexion();
                ProfesorNegocio.BorrarProfesorXCurso(idCurso);

                //Datos.SetearConsulta("delete from EstudiantesXCursos where IDCurso = @IDCurso");
                //Datos.SetearParametro("@IDCurso", idCurso);
                //Datos.EjecutarAccion();
                //Datos.LimpiarParametros();
                //Datos.CerrarConexion();
                EstudianteNegocio.BorrarEstudianteXcurso(idCurso);


                //Datos.SetearConsulta("delete from Inscripciones where IDCurso = @IDCurso");
                //Datos.SetearParametro("@IDCurso", idCurso);
                //Datos.EjecutarAccion();
                //Datos.LimpiarParametros();
                //Datos.CerrarConexion();
                idsInscripciones = InscripcionNegocio.listarIdsInscripcionXCurso(idCurso);
                foreach (int idInscripcion in idsInscripciones)
                {
                    idsNotificaciones = NotificacionNegocio.listarIdsNotificacionesXInscripcion(idInscripcion);
                }
                foreach (int idNotificacion in idsNotificaciones)
                {
                    NotificacionNegocio.BorrarNotificacionXUsuario(idNotificacion);
                    NotificacionNegocio.BorrarNotificacion(idNotificacion);
                }

                InscripcionNegocio.BorrarInscripcionXCurso(idCurso);

                //Datos.SetearConsulta("delete from CategoriasXCurso where IDCurso = @IDCurso");
                //Datos.SetearParametro("@IDCurso", idCurso);
                //Datos.EjecutarAccion();
                //Datos.LimpiarParametros();
                //Datos.CerrarConexion();
                CategoriaNegocio.BorrarCategoriaXCurso(idCurso);

                idsUnidades = UnidadesDeCurso.ListaIdUnidadXCurso(idCurso);
                foreach (int idUnidad in idsUnidades)
                {
                    idsLecciones = LeccionNegocio.ListarIdLeccionXUnidad(idUnidad);

                }
                foreach (int idLeccion in idsLecciones)
                {
                    LeccionNegocio.BorrarLeccionesXEstudiante(idLeccion);
                }

                foreach (int idUnidad in idsUnidades)
                {
                    UnidadesDeCurso.EliminarUnidad(idUnidad);
                }
                //Datos.SetearConsulta("Select IDUnidad from UnidadesXCurso where IDCurso = @IDCurso");
                //Datos.SetearParametro("@IDCurso", idCurso);
                //Datos.EjecutarLectura();
                //while (Datos.Lector.Read())
                //{
                //    Datos.SetearConsulta("Select IDLeccion from LeccionesXUnidad where IDUnidad = @IDUnidad");
                //    Datos.SetearParametro("@IDUnidad", (int)Datos.Lector["IDUnidad"]);
                //    Datos.EjecutarLectura();
                //    while (Datos.Lector.Read())
                //    {
                //        Datos.SetearConsulta("delete from LeccionesXEstudiante where IDLeccion = @IDLeccion");
                //        Datos.SetearParametro("@IDLeccion", (int)Datos.Lector["IDLeccion"]);
                //        Datos.EjecutarAccion();
                //    }
                //    UnidadesDeCurso.EliminarUnidad((int)Datos.Lector["IDUnidad"]);
                //}
                //Datos.LimpiarParametros();
                //Datos.CerrarConexion();

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
            try
            {
                // Obtener el ID de la imagen actual del curso
                Datos.SetearConsulta("SELECT IDImagen FROM Cursos WHERE IDCurso = @IDCurso");
                Datos.SetearParametro("@IDCurso", curso.IDCurso);
                Datos.EjecutarLectura();
                if (Datos.Lector.Read())
                {
                    curso.Imagen.IDImagen = (int)Datos.Lector["IDImagen"];
                }
                Datos.LimpiarParametros();
                Datos.CerrarConexion();

                // Actualizar la URL de la imagen si se ha proporcionado una nueva
                if (!string.IsNullOrEmpty(curso.Imagen.URL))
                {
                    Datos.SetearConsulta("UPDATE Imagenes SET URLIMG = @URLIMG WHERE IDImagenes = @IDImagenes");
                    Datos.SetearParametro("@URLIMG", curso.Imagen.URL);
                    Datos.SetearParametro("@IDImagenes", curso.Imagen.IDImagen);
                    Datos.EjecutarAccion();
                    Datos.LimpiarParametros();
                    Datos.CerrarConexion();
                }

                // Actualizar el curso
                Datos.SetearConsulta("UPDATE Cursos SET Nombre = @Nombre, Descripcion = @Descripcion, Duracion = @Duracion, Completo = @Completo, Estreno = @Estreno WHERE IDCurso = @IDCurso");
                Datos.SetearParametro("@Nombre", curso.Nombre);
                Datos.SetearParametro("@Descripcion", curso.Descripcion);
                Datos.SetearParametro("@Duracion", curso.Duracion);
                Datos.SetearParametro("@Completo", curso.Completo);
                Datos.SetearParametro("@Estreno", curso.Estreno);
                Datos.SetearParametro("@IDCurso", curso.IDCurso);
                Datos.EjecutarAccion();
                Datos.LimpiarParametros();
                Datos.CerrarConexion();

                // Actualizar la relación entre el curso y la categoría
                Datos.SetearConsulta("UPDATE CategoriasXCurso SET IDCategoria = @IDCategoria WHERE IDCurso = @IDCurso");
                Datos.SetearParametro("@IDCurso", curso.IDCurso);
                Datos.SetearParametro("@IDCategoria", curso.Categoria.IDCategoria);
                Datos.EjecutarAccion();
                Datos.LimpiarParametros();
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

        public List<Curso> ValidarCursoCompleto(List<Curso> listaAValidar)
        {
            List<Curso> listaValidada = new List<Curso>();
            foreach (Curso curso in listaAValidar)
            {
                if (curso.Completo)
                {
                    listaValidada.Add(curso);
                }
            }
            return listaValidada;
        }


    }
}
