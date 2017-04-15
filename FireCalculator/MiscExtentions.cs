using System.Collections;
using System.Reflection;
using Newtonsoft.Json;

namespace FireCalculator {
    public static class MiscExtentions {
        private static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings {
            PreserveReferencesHandling = PreserveReferencesHandling.All,
            Formatting = Formatting.Indented,
            MissingMemberHandling = MissingMemberHandling.Error,
            TypeNameHandling = TypeNameHandling.All
        };

        public static string ToJson(this object obj) {
            return JsonConvert.SerializeObject(obj, JsonSettings);
        }

        public static T FromJson<T>(this string json) {
            return JsonConvert.DeserializeObject<T>(json, JsonSettings);
        }

        public static bool IsNullOrEmpty<T>(this T obj) {
            if (obj == null) {
                return true;
            }

            var type = typeof(T);
            if (type == typeof(string)) {
                return string.IsNullOrWhiteSpace(obj as string);
            }

            if (obj.GetType().IsAssignableFrom(typeof(IEnumerable))) {
                foreach (var _ in (IEnumerable)obj) {
                    if (!_.IsNullOrEmpty()) {
                        return false;
                    }
                }
            }

            return false;
        }
    }
}