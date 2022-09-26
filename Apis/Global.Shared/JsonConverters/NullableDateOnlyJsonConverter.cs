using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Global.Shared.JsonConverters
{
    public class NullableDateOnlyJsonConverter : JsonConverter<DateOnly?>
    {
        public override DateOnly? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
                return null;
            return new DateOnlyJsonConverter().Read(ref reader, typeToConvert, options);
        }

        public override void Write(Utf8JsonWriter writer, DateOnly? value, JsonSerializerOptions options)
        {
            if (!value.HasValue)
                writer.WriteNullValue();
            new DateOnlyJsonConverter().Write(writer, value!.Value, options);
        }
    }
}
