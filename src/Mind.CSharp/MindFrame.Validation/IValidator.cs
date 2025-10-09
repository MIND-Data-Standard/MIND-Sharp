using System.Collections.Generic;
namespace MindFrame.Validation
{
    public interface IValidator<in T>
    {
        IEnumerable<ValidationIssue> Validate(T value, string? path = null);
    }
}
