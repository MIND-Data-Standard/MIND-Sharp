using System.Collections.Generic; using MindFrame.Core.Records;
namespace MindFrame.Validation
{
    public sealed class EventValidator : IValidator<Event>
    {
        public IEnumerable<ValidationIssue> Validate(Event value, string? path = null)
        {
            if (string.IsNullOrWhiteSpace(value.Id))
                yield return new ValidationIssue(ValidationSeverity.Error, "EVENT_ID_EMPTY", "Event Id must be non-empty", path);
            if (value.Type == null)
                yield return new ValidationIssue(ValidationSeverity.Warning, "EVENT_TYPE_NULL", "Event Type is null (classification missing)", path);
        }
    }
}
