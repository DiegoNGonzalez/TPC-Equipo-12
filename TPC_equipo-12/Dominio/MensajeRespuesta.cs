using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class MensajeRespuesta
    {
        public int IDRespuesta { get; set; }
        public int IDMensajeOriginal { get; set; }
        public string Texto { get; set; }
        public DateTime FechaHora { get; set; }
        public Usuario UsuarioEmisor { get; set; }
    }
}
