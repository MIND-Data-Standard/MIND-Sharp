---
id: validation-coverage
title: Validation Coverage Matrix
sidebar_label: Coverage
sidebar_position: 43
---

Tracks structural validators implemented for primitives and composite types.

| Type | Core Defined | Serialization Converter | Validator | Notes |
|------|--------------|-------------------------|-----------|-------|
| MindVector3 | ✅ | Default JSON | Vector3Validator | Finite components |
| MindQuaternion | ✅ | Default JSON | QuaternionValidator | Non-zero magnitude (drift warnings planned) |
| MindTimestamp | ✅ | TimestampConverter | TimestampValidator | Finite, negative → warning |
| MindPose | ✅ | Default JSON | PoseValidator | Position + rotation + space warning |
| MindTransform | ✅ | Default JSON | TransformValidator | Position/scale finite, zero scale warn |
| Timestamp seq (list) | n/a | n/a | TimestampSequenceValidator | Monotonic ordering |

## Planned Additions
| Future Validator | Purpose |
|------------------|---------|
| PoseSequenceValidator | Continuity (position/orientation deltas) |
| TransformSequenceValidator | Scale & rotation stability across frames |
| QuaternionDriftValidator | Normalization drift monitoring |
| VectorBoundsValidator | Domain-specific magnitude thresholds |

## Notes
- Using `ValidationCatalog` avoids per-call allocations.
- Magnitude squared checks avoid sqrt overhead.
- Future strict mode: tolerance-based quaternion normalization flags.

See also: [Structural Validators](./structural-validators.md) and [Quaternion Integrity](./quaternion-integrity.md).
