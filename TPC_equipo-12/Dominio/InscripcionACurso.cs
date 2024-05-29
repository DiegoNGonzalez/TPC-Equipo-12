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
        public int IDEstudiante { get; set; }
        public int IDCurso { get; set; }
        public DateTime FechaInscripcion { get; set; }

        public Estudiante Estudiante
        {
            get => default;
            set
            {
            }
        }
    }
}
