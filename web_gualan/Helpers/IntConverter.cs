using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace web_gualan.Helpers
{
    public class IntConverter : JsonConverter<int?>
    {
        public override int? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Number)
                return reader.GetInt32();

            if (reader.TokenType == JsonTokenType.String && int.TryParse(reader.GetString(), out var value))
                return value;

            return null; // si es null o inválido
        }

        public override void Write(Utf8JsonWriter writer, int? value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value ?? 0);
        }
    }
}
