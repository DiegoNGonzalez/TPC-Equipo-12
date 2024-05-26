using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Curso
    {
        public int IDCurso { get; set; }
        public int IdProfesor { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Duracion { get; set; }
        public string RequisitosPrevios { get; set; }
        public string Categoria { get; set; }
        public string Etiquetas {  get; set; } 
        public Modulo Modulo { get; set; }
        public DateTime FechaInscripcion { get; set; }
        public Reseña Reseña { get; set; }
    }
}
