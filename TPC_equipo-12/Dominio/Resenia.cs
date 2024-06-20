using System;

namespace Dominio
{
    public class Resenia
    {
        public int IDResenia { get; set; }
        public Estudiante Estudiante { get; set; }
        public int IDCurso { get; set; }
        public int Calificacion { get; set; }
        public string Comentario { get; set; }
        public DateTime FechaCreacion { get; set; }

    }
}
