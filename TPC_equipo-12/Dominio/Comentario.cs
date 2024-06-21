using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Comentario
    {
        int IDComentario {  get; set; }
        int? IDComentarioPadre { get; set; }
        Usuario UsuarioEmisor { get; set; }
        string TituloComentario { get; set; }
        string CuerpoComentario { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Estado { get; set; }
        int IDLeccion { get; set; }
        public List<Comentario> Respuestas { get; set; } = new List<Comentario>();

        public Comentario()
        {
            //FechaCreacion = DateTime.Now;
            //Estado = true; 
        }

        public Comentario(string tituloComentario, string cuerpoComentario, int IdLeccion, Usuario usuarioEmisor, DateTime fechaCreacion, int? idComentarioPadre = null)
        {
            IDComentarioPadre = idComentarioPadre;
            TituloComentario = tituloComentario;
            CuerpoComentario = cuerpoComentario;
            UsuarioEmisor = usuarioEmisor;
            FechaCreacion = DateTime.Now;
            IDLeccion = IdLeccion;
            Estado = true; 
        }

    }
}
