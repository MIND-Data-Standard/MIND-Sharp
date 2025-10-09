using MindFrame.Core.Enums;
using MindFrame.Core.Primitives;
namespace MindFrame.Core.Types
{
    /// <summary>Position + rotation + scale with reference space.</summary>
    public struct MindTransform
    {
        public MindVector3 Position;
        public MindQuaternion Rotation;
        public MindVector3 Scale;
        public MindSpace Space;
        public MindTransform(MindVector3 position, MindQuaternion rotation, MindVector3 scale)
        { Position = position; Rotation = rotation; Scale = scale; Space = MindSpace.World; }
        public MindTransform(MindVector3 position, MindQuaternion rotation, MindVector3 scale, MindSpace space)
        { Position = position; Rotation = rotation; Scale = scale; Space = space; }
        public static MindTransform Identity => new MindTransform(new MindVector3(0,0,0), new MindQuaternion(1,0,0,0), new MindVector3(1,1,1), MindSpace.World);
    }
}

