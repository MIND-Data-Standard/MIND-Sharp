namespace MindFrame.Validation
{
    public struct ValidationIssue
    {
        public ValidationSeverity Severity; public string Code; public string Message; public string? Path; // Path nullable
        public ValidationIssue(ValidationSeverity severity, string code, string message, string? path = null)
        { Severity = severity; Code = code; Message = message; Path = path; }
        public override string ToString() => $"[{Severity}] {Code}: {Message}{(string.IsNullOrEmpty(Path)?"":" (@"+Path+")")}";
    }
}
