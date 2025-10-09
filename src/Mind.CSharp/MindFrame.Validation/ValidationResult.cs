using System.Collections.Generic; using System.Linq;
namespace MindFrame.Validation
{
    public sealed class ValidationResult
    {
        private readonly List<ValidationIssue> _issues = new();
        public IReadOnlyList<ValidationIssue> Issues => _issues;
        public bool HasErrors => _issues.Any(i => i.Severity == ValidationSeverity.Error);
        public bool HasWarnings => _issues.Any(i => i.Severity == ValidationSeverity.Warning);
        public void Add(ValidationIssue issue) => _issues.Add(issue);
        public void AddRange(IEnumerable<ValidationIssue> issues) { if (issues==null) return; _issues.AddRange(issues); }
    }
}

