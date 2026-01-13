using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using web_gualan.Models;
using web_gualan.Services;

public class IndexModel : PageModel
{
    private readonly ApiService _api;

    public MenuLayoutViewModel LayoutData { get; set; } = new();

    public IndexModel(ApiService api)
    {
        _api = api;
    }

    public async Task<IActionResult> OnGet()
    {
        var usuario = HttpContext.Session.GetString("usuario");
        var rol = HttpContext.Session.GetInt32("rol");

        if (usuario == null || rol == null)
            return RedirectToPage("Login");

        var menu = await _api.GetMenu(rol.Value);

        LayoutData.Usuario = usuario;
        LayoutData.Padres = menu.Where(m => m.padre == 0).ToList();

        foreach (var padre in LayoutData.Padres)
        {
            LayoutData.Hijos[padre.codigoMenu] =
                menu.Where(m => m.padre == padre.codigoMenu).ToList();
        }

        ViewData["LayoutData"] = LayoutData;

        return Page();
    }
}
