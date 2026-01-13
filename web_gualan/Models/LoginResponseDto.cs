namespace web_gualan.Models
{
    public class LoginResponseDto
    {
        public bool success { get; set; }
        public string nombreUsuario { get; set; } = string.Empty;
        public int codigoRol { get; set; }
    }
}
