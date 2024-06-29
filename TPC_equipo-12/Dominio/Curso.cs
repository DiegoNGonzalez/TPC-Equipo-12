using System;
using System.Collections.Generic;

namespace Dominio
{
    public class Curso
    {
        public int IDCurso { get; set; }
        //public int IdProfesor { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Duracion { get; set; }
        public bool Completo { get; set; }
        public DateTime Estreno { get; set; }
        public List<Unidad> Unidades { get; set; }
        public DateTime FechaEstreno { get; set; }
        public List<Resenia> Resenias { get; set; }
        public CategoriaCurso Categoria { get; set; }
        public Imagen Imagen { get; set; }



    }
}
