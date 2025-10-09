namespace MindFrame.Core.Primitives
{
    /// <summary>Timestamp in seconds since Unix epoch.</summary>
    public struct MindTimestamp
    {
        public float Value;
        public MindTimestamp(float value) { Value = value; }
        public override string ToString() => $"MindTimestamp({Value})";
    }
}

