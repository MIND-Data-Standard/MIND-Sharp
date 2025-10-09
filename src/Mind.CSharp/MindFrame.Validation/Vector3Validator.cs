using System.Collections.Generic; using MindFrame.Core.Primitives;
namespace MindFrame.Validation
{
    public sealed class Vector3Validator : IValidator<MindVector3>
    {
        public IEnumerable<ValidationIssue> Validate(MindVector3 value, string? path = null)
        {
            if (!IsFinite(value.X)) yield return Issue("vector3.component.invalid","X component is not finite",path,"x");
            if (!IsFinite(value.Y)) yield return Issue("vector3.component.invalid","Y component is not finite",path,"y");
            if (!IsFinite(value.Z)) yield return Issue("vector3.component.invalid","Z component is not finite",path,"z");
        }
        private static bool IsFinite(float f) => !float.IsNaN(f) && !float.IsInfinity(f);
        private static ValidationIssue Issue(string code,string msg,string? root,string child)=> new ValidationIssue(ValidationSeverity.Error, code, msg, Append(root,child));
        private static string Append(string? root,string child)=> string.IsNullOrEmpty(root)?child:root+"."+child;
    }
}
