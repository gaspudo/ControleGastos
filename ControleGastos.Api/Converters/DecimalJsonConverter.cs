using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ControleGastos.Api.Converters
{
    // criei essa classe para apresentar os dados decimais sem digitos desnecessarios
    public class DecimalJsonConverter : JsonConverter<decimal>
    {
        public override decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.GetDecimal();
        }

        public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options)
        {
            // fixando para 2 casas decimais
            writer.WriteRawValue(value.ToString("F2", CultureInfo.InvariantCulture));
        }
    }
}