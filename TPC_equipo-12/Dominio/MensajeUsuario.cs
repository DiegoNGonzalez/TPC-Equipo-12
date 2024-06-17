using System;

namespace Dominio
{
    public class MensajeUsuario
    {
        public int IDMensaje { get; set; }
        public Usuario UsuarioEmisor { get; set; }
        public Usuario UsuarioReceptor { get; set; }
        public string Asunto { get; set; }
        public string Mensaje { get; set; }
        public DateTime FechaHora { get; set; }
        public bool Leido { get; set; }
    }
}
