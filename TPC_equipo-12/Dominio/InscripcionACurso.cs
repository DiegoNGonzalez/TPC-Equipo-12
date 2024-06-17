using System;

namespace Dominio
{
    public class InscripcionACurso
    {
        public int IDInscripcion { get; set; }
        public Usuario Usuario { get; set; }
        public Curso Curso { get; set; }
        public DateTime FechaInscripcion { get; set; }
        public char Estado { get; set; }


    }
}
