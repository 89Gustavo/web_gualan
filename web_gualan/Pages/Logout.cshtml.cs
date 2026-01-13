using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using web_gualan.Services;

public class LogoutModel : PageModel
{
    private readonly UrlService _urlService;

    public LogoutModel(UrlService urlService)
    {
        _urlService = urlService;
    }

    public IActionResult OnGet()
    {
        // Limpia toda la sesión
        HttpContext.Session.Clear();

        // Mensaje para la siguiente página
        TempData["Success"] = "Sesión cerrada correctamente";

        // Redirige respetando IIS y PathBase
        return Redirect(_urlService.Build("Login"));
    }
}
