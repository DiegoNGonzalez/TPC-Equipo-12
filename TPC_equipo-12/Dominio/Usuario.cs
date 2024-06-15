using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public enum TipoUsuario
    {
        Estudiante = 0,
        Profesor = 1
    }
    public class Usuario
    {
        public int IDUsuario { get; set; }
        public int DNI { get; set; }
        public string Genero { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Contrasenia { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
        public bool EsProfesor { get; set; }
        public Imagen ImagenPerfil { get; set; }

        public Usuario() { }
        public Usuario(string email, string contrasenia, bool profesor)
        {
            Email = email;
            Contrasenia = contrasenia;
            TipoUsuario = profesor ? TipoUsuario.Profesor : TipoUsuario.Estudiante;
        }
        public string NombreCompleto => $"{Nombre} {Apellido}";
    }

}
