namespace web_gualan.Models
{
    public class MenuRolDto
    {
        public int codigoMenu { get; set; }
        public int padre { get; set; }
        public string texto { get; set; } = string.Empty;
        public string href { get; set; } = string.Empty;
    }
}
