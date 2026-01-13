using Microsoft.AspNetCore.Mvc;
using web_gualan.Pages;
using web_gualan.Services;
using web_gualan.Helpers;
using web_gualan.Models;
using System.Net.Http.Headers;
using System.Text.Json;

public class DatosCtasInternas : BasePageModel
{
    [BindProperty]
    public IFormFile ArchivoCsv { get; set; } = default!;

    // Propiedad para mostrar la respuesta del API
    public CsvResponseDto? ApiResponse { get; set; }

    private readonly ApiService _api;

    public DatosCtasInternas(ApiService api) : base(api)
    {
        _api = api;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var usuario = HttpContext.Session.GetString("usuario");
        if (usuario == null)
            return RedirectToPage("/Login");

        await LoadLayoutAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var usuario = HttpContext.Session.GetString("usuario");
        if (usuario == null)
            return RedirectToPage("/Login");

        await LoadLayoutAsync();

        if (ArchivoCsv == null || ArchivoCsv.Length == 0)
        {
            TempData["Error"] = "Debe seleccionar un archivo CSV";
            return Page();
        }

        var extension = Path.GetExtension(ArchivoCsv.FileName).ToLower();
        if (extension != ".csv")
        {
            TempData["Error"] = "El archivo debe ser formato CSV";
            return Page();
        }

        if (!ValidarArchivo.ValidarNombre(ArchivoCsv.FileName, "DatosCtasInternas"))
        {
            TempData["Error"] = "El nombre del archivo debe contener 'DatosCtasInternas'";
            return Page();
        }

        try
        {
            using var content = new MultipartFormDataContent();
            using var stream = ArchivoCsv.OpenReadStream();

            // ✅ Nombre del campo "archivo"
            content.Add(new StreamContent(stream), "archivo", ArchivoCsv.FileName);

            // ✅ Enviar usuario en query string
            //var endpoint = $"api/csv/cargarColocacion?usuarioBitacora={usuario}";
            //var endpoint = $"api/csv/cargarColocacionBatch?usuarioBitacora={usuario}";
            var endpoint = $"api/csv/cargarCtasInternasMasivoLimpio?usuarioBitacora={usuario}";
            var response = await _api.PostCsvAsync(endpoint, content);

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Error al enviar el archivo al servidor";
                return Page();
            }

            // Leer la respuesta JSON del API
            var json = await response.Content.ReadAsStringAsync();
            ApiResponse = JsonSerializer.Deserialize<CsvResponseDto>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (ApiResponse != null && ApiResponse.Success)
            {
                //TempData["Success"] = $"Archivo {ApiResponse.Archivo} cargado correctamente. " +
                //$"tipoCarga: {ApiResponse.TipoCarga}, " +
                //$"peso: {ApiResponse.Peso}, " +
                //$"mensaje: {ApiResponse.Mensaje}, " +
                //$"Tiempo: {ApiResponse.TiempoSegundos} s";
                TempData["Success"] = new Dictionary<string, string>
                {
                    ["Archivo"] = ApiResponse.Archivo,
                    ["Tipo de carga"] = ApiResponse.TipoCarga,
                    ["Peso"] = ApiResponse.Peso,
                    ["Mensaje"] = ApiResponse.Mensaje,
                    ["Tiempo"] = $"{ApiResponse.TiempoSegundos} s"
                };
                //TempData["Success"] = new List<string>
                //{
                //    $"Archivo: {ApiResponse.Archivo}",
                //    $"Tipo de carga: {ApiResponse.TipoCarga}",
                //    $"Peso: {ApiResponse.Peso}",
                //    $"Mensaje: {ApiResponse.Mensaje}",
                //    $"Tiempo: {ApiResponse.TiempoSegundos} s"
                //};
            }
            else
            {
                TempData["Error"] = "El archivo no se pudo procesar correctamente.";
            }
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Ocurrió un error: {ex.Message}";
            return Page();
        }

        return RedirectToPage();
    }

}
