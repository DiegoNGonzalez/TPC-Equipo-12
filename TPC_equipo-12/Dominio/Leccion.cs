using System.Collections.Generic;

namespace Dominio
{
    public class Leccion
    {
        public int IDLeccion { get; set; }
        public int NroLeccion { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public List<MaterialLeccion> Materiales { get; set; }
        public bool Completada { get; set; }
    }
}
