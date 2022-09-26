using Global.Shared.Commons;
using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Global.Shared.JsonConverters
{
    public class DateOnlyDateTimeJsonConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString()!;

            var isValidIso8601DateTimeString = DateTime.TryParse(
                    value, out DateTime result
                );

            if (isValidIso8601DateTimeString)
                return result;
            else
                return DateTime.ParseExact(
                    reader.GetString()!,
                    Constant.DATE_TIME_FORMAT_MMddyyyy,
                    CultureInfo.InvariantCulture);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(
                value.ToString(Constant.DATE_TIME_FORMAT_MMddyyyy, CultureInfo.InvariantCulture));
        }
    }
}
