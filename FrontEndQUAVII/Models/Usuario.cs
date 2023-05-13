namespace FrontEndQUAVII.Models
{
    public class Usuarios
    {
        public int IdUsuario { get; set; }
        public string? Nombres { get; set; }
        public string? ApPaterno { get; set; }
        public string? ApMaterno { get; set; }
        public string? Correo { get; set; }
        public string? password { get; set; }
        public string? Usuario { get; set; }
        public int IdGrupoReparto { get; set; }
        public int Estado { get; set; }

        // nombre de grupo reparto
        public string? NombreGrupo { get; set; }
    }
}
