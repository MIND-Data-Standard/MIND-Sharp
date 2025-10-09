using System.Collections.Generic; using MindFrame.Core.Primitives; using MindFrame.Core.Types;
namespace MindFrame.Validation
{
    public sealed class TransformValidator : IValidator<MindTransform>
    {
        private readonly QuaternionValidator _quat = new(); private readonly Vector3Validator _vec = new();
        public IEnumerable<ValidationIssue> Validate(MindTransform value, string? path = null)
        {
            foreach (var issue in _vec.Validate(value.Position, Append(path,"position"))) yield return issue;
            foreach (var issue in _vec.Validate(value.Scale, Append(path,"scale"))) yield return issue;
            if (value.Scale.X==0 || value.Scale.Y==0 || value.Scale.Z==0)
                yield return new ValidationIssue(ValidationSeverity.Warning, "scale.zeroComponent", "Scale contains a zero component", Append(path,"scale"));
            foreach (var issue in _quat.Validate(value.Rotation, Append(path,"rotation"))) yield return issue;
        }
        private static string Append(string? root,string child)=> string.IsNullOrEmpty(root)?child:root+"."+child;
    }
}
