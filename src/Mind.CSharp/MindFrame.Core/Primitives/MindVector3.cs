namespace MindFrame.Core.Primitives
{
    /// <summary>Three-dimensional vector (X,Y,Z in metres).</summary>
    public struct MindVector3
    {
        public float X; public float Y; public float Z;
        public MindVector3(float x, float y, float z) { X = x; Y = y; Z = z; }
        public override string ToString() => $"MindVector3(X:{X}, Y:{Y}, Z:{Z})";
    }
}

