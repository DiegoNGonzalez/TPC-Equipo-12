using AccesoDB;
using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ComentarioNegocio
    {
        private Datos datos;

        public ComentarioNegocio() 
        {
            datos = new Datos();
        }

        public void publicarComentario(Usuario emisor, string tituloComentario, int idLeccion,  string comentario)
        {
            try
            {
                Comentario primerComentario = new Comentario(tituloComentario, comentario, idLeccion, emisor);
                datos.SetearConsulta("INSERT INTO Comentarios(IDLeccion, TituloComentario, CuerpoComentario, IDUsuarioEmisor, FechaCreacion, Estado) VALUES (@iDLeccion, @tituloComentario, @cuerpoComentario, @idEmisor, @fechaCreacion, @estado");
                //@iDLeccion, @tituloComentario, @cuerpoComentario, @idEmisor, @fechaCreacion, @estado
                datos.SetearParametro("@iDleccion", primerComentario.IDLeccion);
                datos.SetearParametro("@tituloComentario", primerComentario.TituloComentario);
                datos.SetearParametro("@cuerpoComentario", primerComentario.CuerpoComentario);
                datos.SetearParametro("@idEmisor", primerComentario.UsuarioEmisor.IDUsuario);
                datos.SetearParametro("@fechaCreacion", primerComentario.FechaCreacion);
                datos.SetearParametro("@estado", primerComentario.Estado);
                datos.EjecutarAccion();
                datos.LimpiarParametros();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
