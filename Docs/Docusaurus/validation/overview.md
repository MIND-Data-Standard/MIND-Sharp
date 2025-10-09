---
id: validation-overview
title: Validation Overview
sidebar_label: Overview
sidebar_position: 40
---

Validation in MindFrame (pre-alpha) focuses on *structural integrity* rather than deep semantic schema conformity.

## Rationale
Early detection of malformed numeric data prevents downstream explosion in transforms, interpolation, and analytics. Cheap, focused checks yield disproportionate stability gains.

## Current Validators
| Validator | Target | Key Checks |
|-----------|--------|-----------|
| Vector3Validator | MindVector3 | Finite components |
| QuaternionValidator | MindQuaternion | Non-zero magnitude (drift planned) |
| PoseValidator | MindPose | Vector + quaternion + space warning |
| TransformValidator | MindTransform | Position/scale finite, zero scale warn, quaternion |
| TimestampValidator | MindTimestamp | Finite, negative warn |
| TimestampSequenceValidator | IReadOnlyList<MindTimestamp> | Monotonic non-decreasing |

A central `ValidationCatalog` exposes reusable instances.

```csharp
var result = ValidationRunner.Run(pose, ValidationCatalog.Pose);
if (result.HasErrors) { /* handle */ }
```

## Future Layers
| Layer | Scope | Example |
|-------|-------|---------|
| Structural (current) | Numeric sanity | NaN / zero quaternion |
| Semantic | Cross-field coherence | Timestamp ordering across records |
| Schema-driven | Spec alignment | Required fields, enum domains |
| Streaming Live | Real-time warnings | Frame-to-frame continuity |

See detailed quaternion reasoning in [Quaternion Integrity](./quaternion-integrity.md) and matrix in [Validation Coverage](./validation-coverage.md).
