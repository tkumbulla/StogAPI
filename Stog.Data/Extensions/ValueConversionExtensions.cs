using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace Stog.Data.Extensions
{
    /// <summary>
    /// Extension methods for value conversion
    /// </summary>
    public static class ValueConversionExtensions
    {
        /// <summary>
        /// Serializing values to json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyBuilder"></param>
        /// <returns></returns>
        public static PropertyBuilder<T> HasJsonConversion<T>(this PropertyBuilder<T> propertyBuilder)
        {
            var jsonOptions = new JsonSerializerOptions
            {
                IgnoreNullValues = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true,
                Converters =
                {
                    new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
                }
            };

            var converter = new ValueConverter<T, string>(
                v => JsonSerializer.Serialize(v, typeof(T), jsonOptions),
                v => JsonSerializer.Deserialize<T>(v, jsonOptions));

            var comparer = new ValueComparer<T>(
                (l, r) => JsonSerializer.Serialize(l, typeof(T), jsonOptions) == JsonSerializer.Serialize(r, typeof(T), jsonOptions),
                v => v == null ? 0 : JsonSerializer.Serialize(v, typeof(T), jsonOptions).GetHashCode(),
                v => JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(v, typeof(T), jsonOptions), jsonOptions));

            propertyBuilder.HasConversion(converter);
            propertyBuilder.Metadata.SetValueConverter(converter);
            propertyBuilder.Metadata.SetValueComparer(comparer);

            return propertyBuilder;
        }
    }
}
