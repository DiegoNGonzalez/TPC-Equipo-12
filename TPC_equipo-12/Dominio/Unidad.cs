using System.Collections.Generic;

namespace Dominio
{
    public class Unidad
    {
        public int IDUnidad { get; set; }
        public int NroUnidad { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public List<Leccion> Lecciones { get; set; }
        public bool Completada { get; set; }


    }
}
