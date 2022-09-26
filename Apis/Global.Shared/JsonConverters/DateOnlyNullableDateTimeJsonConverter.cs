using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Global.Shared.JsonConverters
{
    public class DateOnlyNullableDateTimeJsonConverter : JsonConverter<DateTime?>
    {
        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
                return null;
            return new DateOnlyDateTimeJsonConverter().Read(ref reader, typeToConvert, options);
        }

        public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
        {
            if (!value.HasValue)
                writer.WriteNullValue();
            new DateOnlyDateTimeJsonConverter().Write(writer, value!.Value, options);
        }
    }
}
