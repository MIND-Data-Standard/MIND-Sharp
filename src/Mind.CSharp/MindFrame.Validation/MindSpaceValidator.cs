using System.Collections.Generic; using MindFrame.Core.Enums;
namespace MindFrame.Validation
{
    public sealed class MindSpaceValidator : IValidator<MindSpace>
    {
        public IEnumerable<ValidationIssue> Validate(MindSpace value, string? path = null)
        {
            if (value == MindSpace.Unknown)
                yield return new ValidationIssue(ValidationSeverity.Warning, "space.unknown", "MindSpace is Unknown", path);
        }
    }
}
