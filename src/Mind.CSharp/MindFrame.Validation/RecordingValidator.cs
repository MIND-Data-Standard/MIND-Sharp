using System.Collections.Generic; using MindFrame.Core.Records;
namespace MindFrame.Validation
{
    public sealed class RecordingValidator : IValidator<Recording>
    {
        public IEnumerable<ValidationIssue> Validate(Recording value, string? path = null)
        {
            if (string.IsNullOrWhiteSpace(value.Id))
                yield return new ValidationIssue(ValidationSeverity.Error, "RECORDING_ID_EMPTY", "Recording Id must be non-empty", path);
            if (float.IsNaN(value.Start.Value) || float.IsNaN(value.End.Value))
                yield return new ValidationIssue(ValidationSeverity.Error, "RECORDING_TS_NAN", "Start/End timestamp cannot be NaN", path);
            if (value.End.Value < value.Start.Value)
                yield return new ValidationIssue(ValidationSeverity.Error, "RECORDING_RANGE_INVALID", "End timestamp precedes Start", path);
            else if (value.End.Value == value.Start.Value)
                yield return new ValidationIssue(ValidationSeverity.Warning, "RECORDING_ZERO_DURATION", "Recording has zero duration", path);
        }
    }
}
