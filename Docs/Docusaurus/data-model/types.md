---
id: data-types
title: Composite Types
sidebar_label: Composite Types
sidebar_position: 21
---

## MindPose
```csharp
public struct MindPose {
  public MindVector3 Position;
  public MindQuaternion Rotation;
  public MindSpace Space;
}
```
A rotation + translation (no scale). Identity uses (0,0,0) position and identity quaternion.

## MindTransform
```csharp
public struct MindTransform {
  public MindVector3 Position;
  public MindQuaternion Rotation;
  public MindVector3 Scale;
  public MindSpace Space;
}
```
Adds uniform or non-uniform scale to pose semantics. Scale components of 0 trigger a warning (degenerate). Future: optional enforcement of positive scale.

## Planned Future Composites
| Type | Purpose | Status |
|------|---------|--------|
| MindDevice | Device metadata (model, manufacturer, capabilities) | Planned |
| MindParticipant | Participant/session metadata | Planned |
| MindEvent | Time-stamped semantic or experimental event | Planned |
| MindRecording | Aggregate session dataset header | Planned |

## Validation Summary
| Type | Validation | Notes |
|------|-----------|-------|
| MindPose | Vector + quaternion + space warning | Unknown space flagged |
| MindTransform | Position + scale finite; zero scale warning; quaternion | Scale normalization policy TBD |

See [Structural Validators](../validation/structural-validators.md).
