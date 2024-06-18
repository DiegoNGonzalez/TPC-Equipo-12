using System.Collections.Generic;

namespace Dominio
{
    public class Profesor : Usuario
    {

        public List<Curso> Cursos { get; set; }
        public List<Notificacion> Notificaciones { get; set; }
        //faltan props para calificaciones y certificaciones si luego llegamos con el desarrollo
       
    }
}
