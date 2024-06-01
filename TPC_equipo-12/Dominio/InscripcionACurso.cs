using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class InscripcionACurso
    {
        public int IDInscripcion { get; set; }
        public Usuario Usuario { get; set; }
        public Curso Curso { get; set; }
        public DateTime FechaInscripcion { get; set; }
        public bool Estado { get; set; }

      
    }
}
