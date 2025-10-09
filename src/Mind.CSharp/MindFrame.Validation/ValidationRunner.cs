using System.Collections.Generic;
namespace MindFrame.Validation
{
    public static class ValidationRunner
    {
        public static ValidationResult Run<T>(T value, params IValidator<T>[] validators)
        { var result = new ValidationResult(); if (validators==null) return result; foreach (var v in validators){ if (v==null) continue; foreach (var issue in v.Validate(value)) result.Add(issue);} return result; }
        public static ValidationResult RunAll<T>(T value, IEnumerable<IValidator<T>> validators)
        { var result = new ValidationResult(); if (validators==null) return result; foreach (var v in validators){ if (v==null) continue; foreach (var issue in v.Validate(value)) result.Add(issue);} return result; }
    }
}

