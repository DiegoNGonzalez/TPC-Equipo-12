using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    internal class Profesor : Usuario
    {
        public int IDProfesor {  get; set; }
        public List<Curso> Cursos { get; set; }

        public Curso Curso
        {
            get => default;
            set
            {
            }
        }
        //faltan props para calificaciones y certificaciones si luego llegamos con el desarrollo

    }
}
