using System.Collections.Generic; using MindFrame.Core.Primitives;
namespace MindFrame.Validation
{
    public sealed class TimestampValidator : IValidator<MindTimestamp>
    {
        public IEnumerable<ValidationIssue> Validate(MindTimestamp value, string? path = null)
        {
            if (float.IsNaN(value.Value) || float.IsInfinity(value.Value))
                yield return new ValidationIssue(ValidationSeverity.Error, "timestamp.invalid", "Timestamp value is not finite", path);
            else if (value.Value < 0)
                yield return new ValidationIssue(ValidationSeverity.Warning, "timestamp.negative", "Timestamp value is negative", path);
        }
    }
}
