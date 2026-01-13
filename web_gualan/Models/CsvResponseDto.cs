using System.Text.Json;

namespace web_gualan.Models
{
    public class CsvResponseDto
    {
        public bool Success { get; set; }
        public string Archivo { get; set; }
        public string TipoCarga { get; set; }
        public double TiempoSegundos { get; set; } // 🔹 doble, no string
        public string Peso { get; set; }
        public string Mensaje { get; set; }
        public int RegistrosInsertados { get; set; }

        public JsonElement Errores { get; set; } // usar JsonElement para leer cualquier tipo
    }
}
