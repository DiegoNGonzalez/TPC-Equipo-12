using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class MaterialLeccion
    {
        public int IDMaterial { get; set; }
        public string Nombre { get; set; }
        public string TipoMaterial { get; set; }
        public string URL { get; set; }
        public string Descripcion { get; set; }

        public int NroMaterial { get; set; }    
    }
}
