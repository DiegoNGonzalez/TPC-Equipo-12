using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Usuario
    {
        public int IDUsuario { get; set; }
        public int DNI { get; set; }
        public string Genero { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; } 
        public string Contrasenia { get; set;}

        public bool EsProfesor { get; set; }
        public Imagen ImagenPerfil { get; set; }
    }
}
