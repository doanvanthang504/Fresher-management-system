using Application.Extensions;
using Global.Shared.Helpers;
using Global.Shared.JsonConverters;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.SeedData
{
    public static class DataInitializer
    {
        private static readonly Assembly assembly = typeof(DataInitializer).Assembly;

        /// <summary>
        /// Seeds a list of <typeparamref name="T"/> in <typeparamref name="T"/>s.json.
        /// <para>T is <typeparamref name="T"/>.</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>A list of <typeparamref name="T"/>.</returns>
        public static async Task<List<T>> SeedDataAsync<T>()
        {
            var fileName = $"Application.SeedData.Samples.{typeof(T).Name.ToPluralFormName()}.json";
            
            using var fileStream = assembly.GetManifestResourceStream(fileName)!;
            var jsonString = await fileStream.ReadAllTextAsync();

            var data = JsonSerializer.Deserialize<List<T>>(jsonString, ApplicationWideJsonConverter.DefaultSerializerOptions)!;
            return data;
        }
    }
}
