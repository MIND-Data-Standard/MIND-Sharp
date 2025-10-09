using System.Collections.Generic;
namespace MindFrame.Core.Records
{
    public struct Participant
    {
        public string Id;
        public Dictionary<string,string> Metadata;
        public Participant(string id, Dictionary<string,string>? metadata = null)
        { Id = id; Metadata = metadata ?? new Dictionary<string,string>(); }
        public override string ToString() => $"Participant(Id={Id}, Meta={Metadata?.Count ?? 0})";
    }
}
