using System.Collections.Generic; using MindFrame.Core.Primitives; using MindFrame.Core.Types; using MindFrame.Core.Enums;
namespace MindFrame.Validation
{
    public sealed class PoseValidator : IValidator<MindPose>
    {
        private readonly Vector3Validator _vec = new(); private readonly QuaternionValidator _quat = new();
        public IEnumerable<ValidationIssue> Validate(MindPose value, string? path = null)
        {
            foreach (var v in _vec.Validate(value.Position, Append(path,"position"))) yield return v;
            foreach (var q in _quat.Validate(value.Rotation, Append(path,"rotation"))) yield return q;
            if (value.Space == MindSpace.Unknown)
                yield return new ValidationIssue(ValidationSeverity.Warning, "pose.space.unknown", "Pose space is Unknown", Append(path,"space"));
        }
        private static string Append(string? root,string child)=> string.IsNullOrEmpty(root)?child:root+"."+child;
    }
}
