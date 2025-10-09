using System.Collections.Generic;
using MindFrame.Core.Primitives;
namespace MindFrame.Core.Records
{
    public struct Recording
    {
        public string Id;
        public MindTimestamp Start;
        public MindTimestamp End;
        public Dictionary<string,string> Metadata;
        public Recording(string id, MindTimestamp start, MindTimestamp end, Dictionary<string,string>? metadata = null)
        { Id = id; Start = start; End = end; Metadata = metadata ?? new Dictionary<string,string>(); }
        public float DurationSeconds => End.Value - Start.Value;
        public override string ToString() => $"Recording(Id={Id}, Start={Start.Value}, End={End.Value}, Dur={DurationSeconds:F3})";
    }
}
