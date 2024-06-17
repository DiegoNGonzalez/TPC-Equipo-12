using System.Collections.Generic;

namespace Dominio
{
    public class CategoriaCurso
    {
        public int IDCategoria { get; set; }
        public string Nombre { get; set; }
        public List<Curso> CursosXCategoria { get; set; }
    }
}
