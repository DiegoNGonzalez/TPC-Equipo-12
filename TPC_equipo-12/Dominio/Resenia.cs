﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Resenia
    {
        public int IDResenia { get; set; }
        public int IDEstudiante { get; set; }
        public int Calificacion { get; set; }
        public string Comentario { get; set; }
        public DateTime FechaCreacion { get; set; }

    }
}
