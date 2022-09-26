using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Global.Shared.JsonConverters
{
    public class NullableDateTimeOffsetJsonConverter : JsonConverter<DateTimeOffset?>
    {
        public override DateTimeOffset? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
                return null;
            return new DateTimeOffsetJsonConverter().Read(ref reader, typeToConvert, options);
        }

        public override void Write(Utf8JsonWriter writer, DateTimeOffset? value, JsonSerializerOptions options)
        {
            if (!value.HasValue)
                writer.WriteNullValue();
            new DateTimeOffsetJsonConverter().Write(writer, value!.Value, options);
        }
    }
}
