---
id: dev-coding-standards
title: Coding Standards
sidebar_label: Coding Standards
sidebar_position: 80
---

## Language / Target
- C# (up to 9 features allowed; .NET Standard 2.1 compatibility)
- Avoid UnityEngine dependencies in core assemblies.

## Naming
| Element | Style | Example |
|---------|-------|---------|
| Types | PascalCase | MindVector3 |
| Fields (public struct) | PascalCase (immutable later) | X, Y, Z |
| Private fields | _camelCase | _quatValidator |
| Constants | PascalCase or ALL_CAPS (retain consistency) | MinMagnitudeSq |
| Namespaces | Provisional `Packages.MindFrame.*` | Packages.MindFrame.Core.Primitives |

## Struct Design
- Use public fields initially (low friction, no hidden logic).
- Provide Identity static members for canonical zero / unit constructs.

## Validation Overlay
- Return `IEnumerable<ValidationIssue>` via `yield return` for streaming friendliness.
- Severity semantics: Error = must fix, Warning = advisory, Info = diagnostic.

## Serialization
- CamelCase JSON fields via global resolver.
- Avoid custom per-type converters unless semantically required.

## Documentation
- XML doc comments for any public API likely to persist beyond pre-alpha.
- Side-effect / performance caveats belong in `<remarks>`.

## Testing (Planned)
- Structural validation tests for each primitive & composite.
- Round-trip JSON asserts for all publicly exposed types.

## Performance Guidelines
- Prefer magnitude squared over magnitude when only comparisons needed.
- Defer allocations; reuse validators (see ValidationCatalog).

## Future Evolution
A style phase may introduce: readonly ref structs, property-based immutability, and analyzer-driven consistency once the surface stabilizes.
