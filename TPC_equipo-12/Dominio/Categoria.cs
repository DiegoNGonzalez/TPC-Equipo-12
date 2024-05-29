using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class CategoriaCurso
    {
        public int IDCategoria { get; set; }
        public string Nombre { get; set; }
        public List<Curso> CursosXCategoria { get; set; } 
    }
}
