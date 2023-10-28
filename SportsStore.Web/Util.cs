using Newtonsoft.Json;
using System.Text.Json.Serialization;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace SportsStore.Web
{
    public static class Util
    {
        public static string ToJson(this object value, bool format = true)
        {
            Formatting formatting = format ? Formatting.Indented : Formatting.None;
            return JsonConvert.SerializeObject(value, formatting, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }

        public static T? FromJson<T>(this string? json)
        {
            if (json == null)
                return default;

            return JsonConvert.DeserializeObject<T>(json);
        }

        public static bool IsExpired(this DateTime value) => DateTime.Now.CompareTo(value) > 0;


        public static string ToJsonMs<T>(this T log) => JsonSerializer.Serialize(log, _applicationInsightsJsonSettings);

        private static readonly JsonSerializerOptions _applicationInsightsJsonSettings = new()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            Converters = { new JsonStringEnumConverter() },
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };

    }
}
