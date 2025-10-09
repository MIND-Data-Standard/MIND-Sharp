using Newtonsoft.Json;
using System;
using MindFrame.Core.Primitives;
namespace MindFrame.Serialization
{
    public sealed class TimestampConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => objectType == typeof(MindTimestamp);
        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.Float && reader.TokenType != JsonToken.Integer)
                throw new JsonSerializationException("Expected JSON number for MindTimestamp");
            float seconds = Convert.ToSingle(reader.Value);
            return new MindTimestamp(seconds);
        }
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            if (value is MindTimestamp ts) writer.WriteValue(ts.Value); else writer.WriteNull();
        }
    }
}
