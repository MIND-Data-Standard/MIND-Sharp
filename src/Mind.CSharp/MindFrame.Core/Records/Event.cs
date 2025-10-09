using System.Collections.Generic;
using MindFrame.Core.Primitives;
namespace MindFrame.Core.Records
{
    public struct Event
    {
        public string Id;
        public string? Type; // made nullable
        public MindTimestamp Timestamp;
        public Dictionary<string,string> Metadata;
        public Event(string id, MindTimestamp timestamp, string? type = null, Dictionary<string,string>? metadata = null)
        { Id = id; Timestamp = timestamp; Type = type; Metadata = metadata ?? new Dictionary<string,string>(); }
        public override string ToString() => $"Event(Id={Id}, Type={Type}, Ts={Timestamp.Value}, Meta={Metadata?.Count ?? 0})";
    }
}
