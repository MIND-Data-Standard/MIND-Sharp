---
id: structural-validators
title: Structural Validators
sidebar_label: Structural Validators
sidebar_position: 42
---

This page outlines implementation details and usage patterns for the current structural validators.

Refer to the matrix in [Validation Coverage](./validation-coverage.md) for implemented vs planned validators.

## Catalog
`ValidationCatalog` centralizes singletons to avoid churn:
```csharp
ValidationCatalog.Vector3
ValidationCatalog.Quaternion
ValidationCatalog.Pose
ValidationCatalog.Transform
ValidationCatalog.Timestamp
ValidationCatalog.TimestampSequence
```

## Running Validation
```csharp
var result = ValidationRunner.Run(transform, ValidationCatalog.Transform);
if (result.HasErrors) { /* handle */ }
```
Multiple validators can be passed; issues accumulate:
```csharp
var result = ValidationRunner.Run(pose, ValidationCatalog.Pose, ValidationCatalog.TimestampSequence /* etc */);
```

## Issue Shape
```csharp
struct ValidationIssue {
  ValidationSeverity Severity; // Info|Warning|Error
  string Code;                 // e.g. "quat.zero"
  string Message;              // human description
  string Path;                 // logical path (e.g. "rotation" or "timestamps[5]")
}
```

Severity semantics:
- **Error:** Data structurally invalid (must fix).
- **Warning:** Potentially lossy / ambiguous but usable.
- **Info:** Optional diagnostics (future extension).

## Current Codes (Examples)
| Code | Severity | Meaning |
|------|----------|---------|
| quat.zero | Error | Quaternion magnitude ~ 0 |
| scale.zeroComponent | Warning | Scale contains a zero component |
| timestamps.nonMonotonic | Error | Sequence decreased |
| timestamps.null | Error | Sequence reference was null |
| timestamp.invalid | Error | NaN / ∞ timestamp |
| timestamp.negative | Warning | Negative timestamp (allowed but flagged) |
| vector3.component.invalid | Error | Vector3 had non-finite component |
| pose.space.unknown | Warning | Unknown space used |

## Extending
To add a validator:
1. Implement `IValidator<T>`.
2. Return `IEnumerable<ValidationIssue>` (yield as you go for streaming friendliness).
3. Register (optionally) in `ValidationCatalog`.
4. Add code & message doc entry.

## Performance Notes
- Validators avoid allocations except materializing issues list.
- Using `yield return` streams issues; consumer collects only when iterated.
- Magnitude squared is used instead of `Math.Sqrt`.

## Future Enhancements
| Idea | Benefit |
|------|--------|
| Strict vs Lenient modes | Tailor validation cost & strictness |
| Batch Pose / Transform continuity checks | Detect jumps / jitter |
| Configurable thresholds (json/yaml) | Domain-specific tuning |
| Auto-normalization helpers | Clean inputs automatically |

## Example: Custom Validator
```csharp
public sealed class PositiveScaleValidator : IValidator<MindTransform>
{
    public IEnumerable<ValidationIssue> Validate(MindTransform value, string path = null)
    {
        if (value.Scale.X <= 0 || value.Scale.Y <= 0 || value.Scale.Z <= 0)
            yield return new ValidationIssue(ValidationSeverity.Warning, "scale.nonPositive", "Non-positive scale component", path);
    }
}
```
Add it as needed:
```csharp
var result = ValidationRunner.Run(t, new TransformValidator(), new PositiveScaleValidator());
```
