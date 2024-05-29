using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Estudiante : Usuario
    {
        public int IDEstudiante { get; set; }
        public List<Curso> Cursos { get; set; }
        public int PorcentajeCompletado { get; set; }
        public List<Resenia> Resenias { get; set; }
        public List<InscripcionACurso> Inscripciones { get; set; }

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
