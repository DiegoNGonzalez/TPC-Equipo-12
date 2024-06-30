using System;

namespace Dominio
{
    public class Notificacion
    {
        public int IDNotificacion { get; set; }
        public string MensajeNotificacion { get; set; }
        public string Tipo { get; set; }
        public InscripcionACurso Inscripcion { get; set; }
        public MensajeUsuario Mensaje { get; set; }
        public MensajeRespuesta MensajeRespuesta { get; set; }
        public Comentario ComentarioLeccion { get; set; }
        public DateTime Fecha { get; set; }
        public bool Estado { get; set; }


    }
}
