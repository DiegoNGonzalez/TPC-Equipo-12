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
        //public string Contrasenia { get; set; }
        public string ContraseniaHash { get; set; } 
        public string ContraseniaSalt { get; set; } 
        public TipoUsuario TipoUsuario { get; set; }
        public bool EsProfesor { get; set; }
        public Imagen ImagenPerfil { get; set; }

        public Usuario() { }
        public Usuario(string email, string contraseniaHash,string contraseniaSatl, bool profesor)
        {
            Email = email;
            ContraseniaHash = contraseniaHash;
            ContraseniaSalt = contraseniaSatl;
            TipoUsuario = profesor ? TipoUsuario.Profesor : TipoUsuario.Estudiante;
        }
        public string NombreCompleto => $"{Nombre} {Apellido}";
    }

}
