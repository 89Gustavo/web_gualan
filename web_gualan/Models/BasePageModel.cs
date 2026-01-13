using Microsoft.AspNetCore.Mvc.RazorPages;
using web_gualan.Models;
using web_gualan.Services;

namespace web_gualan.Pages
{
    public abstract class BasePageModel : PageModel
    {
        protected readonly ApiService _api;

        public BasePageModel(ApiService api)
        {
            _api = api;
        }

        protected async Task LoadLayoutAsync()
        {
            var usuario = HttpContext.Session.GetString("usuario");
            var rol = HttpContext.Session.GetInt32("rol");

            if (string.IsNullOrEmpty(usuario) || rol == null)
                return;

            var menu = await _api.GetMenu(rol.Value);

            var layout = new MenuLayoutViewModel
            {
                Usuario = usuario,
                Padres = menu.Where(m => m.padre == 0).ToList()
            };

            foreach (var padre in layout.Padres)
            {
                layout.Hijos[padre.codigoMenu] =
                    menu.Where(m => m.padre == padre.codigoMenu).ToList();
            }

            ViewData["LayoutData"] = layout;
        }
    }
}
