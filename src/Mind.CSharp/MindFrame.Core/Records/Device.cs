using System.Collections.Generic;
namespace MindFrame.Core.Records
{
    public struct Device
    {
        public string Id;
        public Dictionary<string,string> Metadata;
        public Device(string id, Dictionary<string,string>? metadata = null)
        { Id = id; Metadata = metadata ?? new Dictionary<string,string>(); }
        public override string ToString() => $"Device(Id={Id}, Meta={Metadata?.Count ?? 0})";
    }
}
