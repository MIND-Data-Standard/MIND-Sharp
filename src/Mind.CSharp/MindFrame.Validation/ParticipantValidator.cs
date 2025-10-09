using System.Collections.Generic; using MindFrame.Core.Records;
namespace MindFrame.Validation
{
    public sealed class ParticipantValidator : IValidator<Participant>
    {
        public IEnumerable<ValidationIssue> Validate(Participant value, string? path = null)
        {
            if (string.IsNullOrWhiteSpace(value.Id))
                yield return new ValidationIssue(ValidationSeverity.Error, "PARTICIPANT_ID_EMPTY", "Participant Id must be non-empty", path);
            if (value.Metadata != null)
            {
                foreach (var kv in value.Metadata)
                    if (string.IsNullOrEmpty(kv.Key))
                        yield return new ValidationIssue(ValidationSeverity.Warning, "PARTICIPANT_META_KEY_EMPTY", "Metadata key is empty", path);
            }
        }
    }
}
