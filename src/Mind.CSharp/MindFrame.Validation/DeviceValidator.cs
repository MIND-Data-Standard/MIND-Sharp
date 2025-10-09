using System.Collections.Generic; using MindFrame.Core.Records;
namespace MindFrame.Validation
{
    public sealed class DeviceValidator : IValidator<Device>
    {
        public IEnumerable<ValidationIssue> Validate(Device value, string? path = null)
        {
            if (string.IsNullOrWhiteSpace(value.Id))
                yield return new ValidationIssue(ValidationSeverity.Error, "DEVICE_ID_EMPTY", "Device Id must be non-empty", path);
            if (value.Metadata != null)
            {
                foreach (var kv in value.Metadata)
                    if (string.IsNullOrEmpty(kv.Key))
                        yield return new ValidationIssue(ValidationSeverity.Warning, "DEVICE_META_KEY_EMPTY", "Metadata key is empty", path);
            }
        }
    }
}
