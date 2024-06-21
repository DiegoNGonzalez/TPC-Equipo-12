using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Comentario
    {
        public int IDComentario {  get; set; }
        public int? IDComentarioPadre { get; set; }
        public Usuario UsuarioEmisor { get; set; }
        public string CuerpoComentario { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Estado { get; set; }
        public int IDLeccion { get; set; }
        public List<Comentario> Respuestas { get; set; } = new List<Comentario>();

        public Comentario()
        {
            FechaCreacion = DateTime.Now;
            Estado = true; 
        }

        public Comentario(string cuerpoComentario, int idLeccion, Usuario usuarioEmisor)
        {
            CuerpoComentario = cuerpoComentario;
            IDLeccion = idLeccion;
            UsuarioEmisor = usuarioEmisor;
            FechaCreacion = DateTime.Now;
            Estado = true; 
        }

    }
}
