using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class ReinicioContrasenia
    {
        public int IDUsuario { get; set; }
        public string Token { get; set; }
        public DateTime FechaExpiracion { get; set; }
    }

}
