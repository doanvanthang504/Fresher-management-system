using System.Text.Json;

namespace Global.Shared.JsonConverters
{
    public static class ApplicationWideJsonConverter
    {
        public static readonly JsonSerializerOptions DefaultSerializerOptions = new();

        static ApplicationWideJsonConverter()
        {
            DefaultSerializerOptions.Converters.Add(new DateOnlyDateTimeJsonConverter());
            DefaultSerializerOptions.Converters.Add(new DateOnlyNullableDateTimeJsonConverter());
            DefaultSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
            DefaultSerializerOptions.Converters.Add(new NullableDateOnlyJsonConverter());
            DefaultSerializerOptions.Converters.Add(new DateTimeOffsetJsonConverter());
            DefaultSerializerOptions.Converters.Add(new NullableDateTimeOffsetJsonConverter());
        }
    }
}
