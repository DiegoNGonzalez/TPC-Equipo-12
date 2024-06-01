using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Estudiante : Usuario
    {
        
        public List<Curso> Cursos { get; set; }
        public int PorcentajeCompletado { get; set; }
        public List<Resenia> Resenias { get; set; }

        public List<Notificacion> Notificaciones { get; set; }

        public bool Estado { get; set; }

        

        //faltan props para calificaciones y certificaciones si luego llegamos con el desarrollo
    }
}
