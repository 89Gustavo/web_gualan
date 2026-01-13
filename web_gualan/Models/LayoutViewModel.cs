using web_gualan.Models;

namespace web_gualan.Models
{
    public class LayoutViewModel
    {
        public List<MenuRolDto> Menu { get; set; } = new();
        public string Usuario { get; set; } = string.Empty;
    }
}
