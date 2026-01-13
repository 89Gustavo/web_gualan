namespace web_gualan.Models
{
    public class MenuLayoutViewModel
    {
        public string Usuario { get; set; } = string.Empty;
        public List<MenuRolDto> Padres { get; set; } = new();
        public Dictionary<int, List<MenuRolDto>> Hijos { get; set; } = new();
    }
}
