using Global.Shared.Commons;
using Global.Shared.Extensions;
using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Global.Shared.JsonConverters
{
    public class DateTimeOffsetJsonConverter : JsonConverter<DateTimeOffset>
    {
        public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString()!;

            if (value.IsInDateFormat())
            {
                return DateTimeOffset.ParseExact(
                    value,
                    Constant.DATE_TIME_FORMAT_MMddyyyy,
                    CultureInfo.InvariantCulture.DateTimeFormat,
                    DateTimeStyles.AdjustToUniversal);
            }
            return DateTimeOffset.Parse(value);
        }

        public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
