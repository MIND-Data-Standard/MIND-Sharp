using System.Collections.Generic; using MindFrame.Core.Primitives;
namespace MindFrame.Validation
{
    /// <summary>Validates sequence of timestamps is non-decreasing.</summary>
    public sealed class TimestampSequenceValidator : IValidator<IReadOnlyList<MindTimestamp>>
    {
        public IEnumerable<ValidationIssue> Validate(IReadOnlyList<MindTimestamp> value, string? path = null)
        {
            if (value == null){ yield return new ValidationIssue(ValidationSeverity.Error, "timestamps.null", "Timestamp sequence is null", path); yield break; }
            if (value.Count == 0) yield break; float prev = value[0].Value; for (int i=1;i<value.Count;i++){ float cur = value[i].Value; if (cur < prev) yield return new ValidationIssue(ValidationSeverity.Error, "timestamps.nonMonotonic", $"Timestamp at index {i} ({cur}) < previous ({prev})", IndexPath(path,i)); prev = cur; }
        }
        private static string IndexPath(string? root,int index)=> string.IsNullOrEmpty(root)?$"[{index}]":root+$"[{index}]";
    }
}
