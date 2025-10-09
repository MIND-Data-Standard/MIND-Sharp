namespace MindFrame.Core.Primitives
{
    /// <summary>Quaternion rotation (W,X,Y,Z).</summary>
    public struct MindQuaternion
    {
        public float W; public float X; public float Y; public float Z;
        public MindQuaternion(float w, float x, float y, float z) { W = w; X = x; Y = y; Z = z; }
        public override string ToString() => $"MindQuaternion(W:{W}, X:{X}, Y:{Y}, Z:{Z})";
    }
}

