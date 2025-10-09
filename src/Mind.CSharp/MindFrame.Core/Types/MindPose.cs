using MindFrame.Core.Enums;
using MindFrame.Core.Primitives;
namespace MindFrame.Core.Types
{
    /// <summary>Position + rotation (no scale) with reference space.</summary>
    public struct MindPose
    {
        public MindVector3 Position;
        public MindQuaternion Rotation;
        public MindSpace Space;
        public MindPose(MindVector3 position, MindQuaternion rotation)
        { Position = position; Rotation = rotation; Space = MindSpace.World; }
        public MindPose(MindVector3 position, MindQuaternion rotation, MindSpace space)
        { Position = position; Rotation = rotation; Space = space; }
        public static MindPose Identity => new MindPose(new MindVector3(0,0,0), new MindQuaternion(1,0,0,0), MindSpace.World);
        public override string ToString() => $"MindPose(Pos=({Position.X},{Position.Y},{Position.Z}), Rot=({Rotation.W},{Rotation.X},{Rotation.Y},{Rotation.Z}), Space={Space})";
    }
}

