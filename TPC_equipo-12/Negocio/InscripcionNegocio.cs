using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using AccesoDB;

namespace Negocio
{
    public class InscripcionNegocio
    {
        private Datos Datos;
        private List<InscripcionACurso> inscripciones;

        public InscripcionNegocio()
        {
            Datos = new Datos();
        }

    }
}
