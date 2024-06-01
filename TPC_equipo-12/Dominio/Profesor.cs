using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    internal class Profesor : Usuario
    {
        
        public List<Curso> Cursos { get; set; }
        public List<Notificacion> Notificaciones { get; set; }
        //faltan props para calificaciones y certificaciones si luego llegamos con el desarrollo

    }
}
