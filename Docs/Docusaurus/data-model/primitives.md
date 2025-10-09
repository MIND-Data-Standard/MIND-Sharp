---
id: data-primitives
title: Primitives
sidebar_label: Primitives
sidebar_position: 20
---

Core primitives define the atomic numerical building blocks for higher-level structures.

## MindVector3
```csharp
public struct MindVector3 { public float X; public float Y; public float Z; }
```
**Semantics:** Metres in a right-handed coordinate system (frame context given by enclosing type's `Space`).  
**Validation:** Components must be finite (no NaN / ±Infinity).  
**Potential Extensions:** Unit tagging & coordinate frame transforms (future).

## MindQuaternion
```csharp
public struct MindQuaternion { public float W; public float X; public float Y; public float Z; }
```
Represents orientation. **Identity:** (1,0,0,0).  
**Critical Invariant:** Quaternion must not be the zero vector. See [Quaternion Integrity](../validation/quaternion-integrity.md).

## MindTimestamp
```csharp
public struct MindTimestamp { public float Value; } // seconds since Unix epoch
```
Float seconds (UTC epoch). Chosen for compactness in real-time streams; double precision conversions available via extension methods.  
**Validation:** Must be finite; negative allowed but flagged (can represent pre-session offsets).  
**Future:** High-resolution or monotonic clock variants.

## Enum: MindSpace
Reference frame classification for spatial primitives & composites.
```
World | Local | Stage | View | Device | Custom | Unknown
```
`Unknown` is discouraged—validators will emit a warning for ambiguous spatial context.

## Extension Methods
`MindTimestampExtensions` provides conversions to/from `DateTimeOffset`; all arithmetic is performed in seconds.

## General Validation Notes
| Primitive | Error Conditions | Warning Conditions |
|-----------|------------------|--------------------|
| Vector3 | NaN / Infinity component | (planned) excessive magnitude |
| Quaternion | Zero / near-zero magnitude | (planned) normalization drift |
| Timestamp | NaN / Infinity | Negative (advisory) |

See [Structural Validators](../validation/structural-validators.md) for implementation details.
