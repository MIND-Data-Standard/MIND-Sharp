using System.Collections.Generic; using MindFrame.Core.Primitives;
namespace MindFrame.Validation
{
    public sealed class QuaternionValidator : IValidator<MindQuaternion>
    {
        private const float MinMagnitudeSq = 1e-8f;
        public IEnumerable<ValidationIssue> Validate(MindQuaternion value, string? path = null)
        {
            float magSq = value.W*value.W + value.X*value.X + value.Y*value.Y + value.Z*value.Z;
            if (magSq < MinMagnitudeSq)
                yield return new ValidationIssue(ValidationSeverity.Error, "quat.zero", "Quaternion magnitude is zero (invalid rotation)", path);
        }
    }
}
