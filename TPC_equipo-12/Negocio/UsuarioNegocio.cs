using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using AccesoDB;

namespace Negocio
{
    public class UsuarioNegocio
    {
        private Datos Datos;
        public UsuarioNegocio()
        {
            Datos = new Datos();
        }
        public List<Usuario> ListarUsuarios()
        {
            List<Usuario> lista = new List<Usuario>();
            try
            {
                Datos.SetearConsulta("select u.IDUsuario, u.Nombre, u.Apellido, u.Email, u.Contrasenia, u.DNI, u.Genero, EsProfesor, i.IDImagenes, i.URLIMG from Usuarios u left join Imagenes i on u.IDImagen = i.IDImagenes");
                Datos.EjecutarLectura();
                while (Datos.Lector.Read())
                {
                    Usuario aux = new Usuario();
                    aux.IDUsuario = Datos.Lector.GetInt32(0);
                    aux.Nombre = (string)Datos.Lector["Nombre"];
                    aux.Apellido = (string)Datos.Lector["Apellido"];
                    aux.Email = (string)Datos.Lector["Email"];
                    aux.Contrasenia = (string)Datos.Lector["Clave"];
                    aux.DNI = (int)Datos.Lector["DNI"];
                    aux.Genero = (string)Datos.Lector["Genero"];
                    aux.EsProfesor = (bool)Datos.Lector["EsProfesor"];
                    aux.ImagenPerfil = new Imagen();
                    aux.ImagenPerfil.IDImagen = (int)Datos.Lector["IDImagenes"];
                    aux.ImagenPerfil.URL = (string)Datos.Lector["URLIMG"];

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
        public int UltimoIdUsuario()
        {
            try
            {
                Datos.SetearConsulta("select top 1 IDUsuario from Usuarios order by IDUsuario desc");
                Datos.EjecutarLectura();
                Datos.Lector.Read();
                return Datos.Lector.GetInt32(0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int UltimoIdImagen()
        {
            try
            {
                Datos.SetearConsulta("select top 1 IDImagenes from Imagenes order by IDImagenes desc");
                Datos.EjecutarLectura();
                Datos.Lector.Read();
                return Datos.Lector.GetInt32(0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AgregarUsuario(Usuario usuario)
        {
            try
            {
                Datos.SetearConsulta("insert into Imagenes (URLIMG) values(@URLIMG)");
                Datos.SetearParametro("@URLIMG", usuario.ImagenPerfil.URL);
                Datos.EjecutarAccion();
                int aux = UltimoIdImagen();
                Datos.SetearConsulta("insert into Usuarios (Nombre, Apellido, Email, Clave, DNI, Genero, EsProfesor, IDImagen) values (@Nombre, @Apellido, @Email, @Clave, @DNI, @Genero, @EsProfesor, @IDImagen)");
                Datos.SetearParametro("@Nombre", usuario.Nombre);
                Datos.SetearParametro("@Apellido", usuario.Apellido);
                Datos.SetearParametro("@Email", usuario.Email);
                Datos.SetearParametro("@Clave", usuario.Contrasenia);
                Datos.SetearParametro("@DNI", usuario.DNI);
                Datos.SetearParametro("@Genero", usuario.Genero);
                Datos.SetearParametro("@EsProfesor", usuario.EsProfesor);
                Datos.SetearParametro("@IDImagen", aux);
                Datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void ModificarUsuario(Usuario usuario)
        {
            try
            {
                Datos.SetearConsulta("update Usuarios set Nombre = @Nombre, Apellido = @Apellido, Email = @Email, Clave = @Clave, DNI = @DNI, Genero = @Genero, EsProfesor = @EsProfesor where IDUsuario = @IDUsuario");
                Datos.SetearParametro("@Nombre", usuario.Nombre);
                Datos.SetearParametro("@Apellido", usuario.Apellido);
                Datos.SetearParametro("@Email", usuario.Email);
                Datos.SetearParametro("@Clave", usuario.Contrasenia);
                Datos.SetearParametro("@DNI", usuario.DNI);
                Datos.SetearParametro("@Genero", usuario.Genero);
                Datos.SetearParametro("@EsProfesor", usuario.EsProfesor);
                Datos.SetearParametro("@IDUsuario", usuario.IDUsuario);
                Datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void EliminarUsuario(Usuario usuario)
        {
            try
            {
                Datos.SetearConsulta("delete from Imagenes where IDImagenes = @IDImagenes");
                Datos.SetearParametro("@IDImagenes", usuario.ImagenPerfil.IDImagen);
                Datos.EjecutarAccion();
                if (usuario.EsProfesor)
                {
                    Datos.SetearConsulta("delete from Profesores where IDProfesor = @IDProfesor");
                    Datos.SetearParametro("@IDProfesor", usuario.IDUsuario);
                    Datos.EjecutarAccion();
                }
                else
                {
                    Datos.SetearConsulta("delete from Estudiantes where IDEstudiante = @IDEstudiante");
                    Datos.SetearParametro("@IDEstudiante", usuario.IDUsuario);
                    Datos.EjecutarAccion();
                }

                Datos.SetearConsulta("delete from Usuarios where IDUsuario = @IDUsuario");
                Datos.SetearParametro("@IDUsuario", usuario.IDUsuario);
                Datos.EjecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
