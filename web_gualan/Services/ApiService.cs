using System.Text;
using System.Text.Json;
using web_gualan.Models;

namespace web_gualan.Services
{
    public class ApiService
    {
        private readonly HttpClient _http;

        public ApiService(HttpClient http)
        {
            _http = http;
            _http.BaseAddress = new Uri("http://localhost/api_gualan/");
        }

        public async Task<LoginResponseDto?> Login(LoginRequestDto data)
        {
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _http.PostAsync("api/auth/login", content);

            if (!response.IsSuccessStatusCode) return null;

            var result = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<LoginResponseDto>(result);
        }

        public async Task<List<MenuRolDto>> GetMenu(int codigoRol)
        {
            var response = await _http.GetAsync($"api/menu/menuRol/{codigoRol}");
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<MenuRolDto>>(json)!;
        }
        // Enviar CSV al API
        public async Task<HttpResponseMessage> PostCsvAsync(string endpoint, MultipartFormDataContent content)
        {
            return await _http.PostAsync(endpoint, content);
        }
    }
}
