using Newtonsoft.Json;

namespace SportsStore.Web
{
    public static class Util
    {
        public static string ToJson(this object value, bool format = true)
        {
            Formatting formatting = format ? Formatting.Indented : Formatting.None;
            return JsonConvert.SerializeObject(value, formatting);
        }

        public static bool IsExpired(this DateTime value) => DateTime.Now.CompareTo(value) > 0;
    }
}
