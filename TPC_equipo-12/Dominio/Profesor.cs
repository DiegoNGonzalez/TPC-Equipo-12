using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    internal class Profesor : Usuario
    {
        public int IDProfesor {  get; set; }
        public Curso Curso { get; set; }
    }
}
