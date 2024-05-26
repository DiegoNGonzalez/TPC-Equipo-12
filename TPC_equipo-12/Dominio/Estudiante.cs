using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Estudiante : Usuario
    {
        public int IDEstudiante { get; set; }
        public Curso Curso { get; set; }

        
    }
}
