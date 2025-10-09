using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using MindFrame.Core.Primitives;
namespace MindFrame.Serialization
{
    /// <summary>Centralized JSON settings + helper methods.</summary>
    public static class MindJson
    {
        static MindJson() => DefaultSettings = CreateDefaultSettings();
        public static JsonSerializerSettings DefaultSettings { get; private set; }
        public static JsonSerializerSettings CreateDefaultSettings()
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Include,
                MissingMemberHandling = MissingMemberHandling.Error,
                Formatting = Formatting.None,
                MetadataPropertyHandling = MetadataPropertyHandling.Ignore
            };
            settings.Converters.Add(new TimestampConverter());
            return settings;
        }
        public static string Serialize<T>(T value) => JsonConvert.SerializeObject(value, DefaultSettings);
        public static T Deserialize<T>(string json)
        {
            var result = JsonConvert.DeserializeObject<T>(json, DefaultSettings);
            if (result == null)
                throw new JsonSerializationException($"Deserialization produced null for type {typeof(T).FullName}");
            return result;
        }
    }
}
