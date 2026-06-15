using System.Text.Json;
using System.Text.Json.Serialization;

namespace MyApp.Endpoints.Abstractions.ApiErrors
{
    public sealed class ErrorCodeConverter : JsonConverter<ErrorCode>
    {
        public override ErrorCode? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new JsonException("Not supported");
        }

        public override void Write(Utf8JsonWriter writer, ErrorCode value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.Code);
        }
    }
}
