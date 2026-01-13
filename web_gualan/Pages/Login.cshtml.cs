using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using web_gualan.Helpers;
using web_gualan.Models;
using web_gualan.Services;

public class LoginModel : PageModel
{
    private readonly ApiService _api;
    public string Alert { get; set; } = "";

    public LoginModel(ApiService api)
    {
        _api = api;
    }

    public async Task<IActionResult> OnPost(string Usuario, string Clave)
    {
        var encrypted = CryptoHelper.Encrypt(Clave);

        var result = await _api.Login(new LoginRequestDto
        {
            nombreUsuario = Usuario,
            claveUsuario = encrypted
        });

        if (result == null || !result.success)
        {
            Alert = AlertHelper.Error("Credenciales incorrectas");
            return Page();
        }

        HttpContext.Session.SetString("usuario", result.nombreUsuario);
        HttpContext.Session.SetInt32("rol", result.codigoRol);

        return RedirectToPage("Index");
    }
}
