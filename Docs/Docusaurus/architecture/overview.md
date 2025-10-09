---
id: architecture-overview
title: Architecture Overview
sidebar_label: Overview
sidebar_position: 10
---

MindFrame is intentionally minimal at this stage, organizing code into four assemblies.

## Assemblies
| Assembly | Responsibility | Depends On |
|----------|----------------|------------|
| MindFrame.Core | Primitives (Vector3, Quaternion, Timestamp), spatial composites (Pose, Transform), enums (Space) | (None) |
| MindFrame.Serialization | JSON facade (`MindJson`), converters (`TimestampConverter`) | Core + Newtonsoft (Unity pkg) |
| MindFrame.ModelIO | Export helpers (JSONL) | Core + Serialization |
| MindFrame.Validation | Structural validators (basic) | Core |

## Layering
```
Core <- Serialization
Core <- ModelIO
(Core, Serialization) <- Validation (optional future deep layer)
```

## Design Tenets
- **Purity:** Core has no UnityEngine dependency.
- **Explicitness:** Units (metres, seconds) and reference frames (World, Local, Device...) spelled out.
- **Simplicity First:** Early focus on the highest leverage primitives; postpone broad schema surface until validation is in place.
- **Performance Aware:** Avoid gratuitous allocations; structural validation is cheap; heavier semantic checks will be opt-in.

## Out of Scope (Currently)
- Device drivers, transport protocols, networking (MINDFlow’s domain).
- Code generation from JSON Schema (may be revisited once spec stabilizes).
- Backwards compatibility scaffolding (pre-alpha policy: move fast, break safely in isolation).

## Planned Enhancements
See [Roadmap](../roadmap/roadmap.md) for sequenced items (records, deeper validation, exporters, interoperability mapping).
