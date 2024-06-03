using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using AccesoDB;
using System.Collections;

namespace Negocio
{
    public class CursoNegocio
    {
		public Datos Datos;
		public CursoNegocio()
		{
            Datos = new Datos();
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
        public List<Curso> EstaInscripto()
        {
            List<Curso> lista = new List<Curso>();
            List<int>  idCursos = new List<int>();
            try
            {
                Datos.SetearConsulta("Select IDCurso From EstudiantesXCursos Where IDEstudiante Is Not Null");
                Datos.EjecutarLectura();
                while (Datos.Lector.Read())
                {
                    idCursos.Add((int)Datos.Lector["IDCurso"]);
                }
                Datos.CerrarConexion();
                foreach (int id in idCursos)
                {
                    Datos.SetearConsulta("select c.IDCurso, c.Nombre, c.Descripcion, c.Estreno, c.Duracion,c.IDImagen, i.IDImagenes, i.URLIMG from cursos c inner join Imagenes i on c.IDImagen= i.IDImagenes where c.IDCurso = @IDCurso");
                    Datos.SetearParametro("@IDCurso", id);
                    Datos.EjecutarLectura();
                    while (Datos.Lector.Read())
                    {
                        Curso aux = new Curso();
                        aux.IDCurso = (int)Datos.Lector["IDCurso"];
                        aux.Nombre = (string)Datos.Lector["Nombre"];
                        aux.Descripcion = (string)Datos.Lector["Descripcion"];
                        aux.Estreno = (DateTime)Datos.Lector["Estreno"];
                        aux.Duracion = (int)Datos.Lector["Duracion"];
                        aux.Imagen = new Imagen();
                        aux.Imagen.IDImagen = (int)Datos.Lector["IDImagenes"];
                        aux.Imagen.URL = (string)Datos.Lector["URLIMG"];
                        lista.Add(aux);
                    }
                    Datos.CerrarConexion();
                    Datos.LimpiarParametros();
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
