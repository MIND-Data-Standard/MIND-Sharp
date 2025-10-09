# MindFrame Architecture (Pre-Alpha Core)

## Assemblies (Current)
- MindFrame.Core – Primitives & basic spatial composites (`Packages.MindFrame.Core.*`)
- MindFrame.Serialization – JSON facade + converters (`Packages.MindFrame.Serialization`)
- MindFrame.ModelIO – Export helpers (JSONL) (`Packages.MindFrame.ModelIO`)
- MindFrame.Validation – Placeholder (no validators yet)

> The `Packages.MindFrame.*` prefix is purely provisional during early development; no compatibility guarantees.

## Layering
```
Core <- Serialization
Core <- ModelIO
(Core, Serialization) <- Validation (future optional)
```

## Implemented Snapshot
| Area | Status |
|------|--------|
| Primitives (Vector3, Quaternion, Timestamp) | Done |
| Spatial types (Pose, Transform) | Done |
| JSON facade + timestamp converter | Done |
| JSONL exporter | Done |
| Validation | Not implemented |
| Record / device / event types | Not implemented |

## Immediate Focus
1. Structural validation (null, range, enum, timestamp monotonicity)
2. Record types (participant, device, event, recording)
3. Interoperability mapping outline (NWB, BIDS, OpenXR, LSL)
4. Export pipeline extensions (Parquet, Arrow) & tokenization helpers
5. Performance benchmark harness (serialization + allocations)

## Principles
- Pure .NET Standard 2.1 (no UnityEngine)
- Minimal allocations; opt-in heavier validation
- Explicit units & reference frames
- Foundation-model friendly dataset formats

## Risks (Current Scope)
| Risk | Mitigation |
|------|------------|
| Schema drift | Introduce schema sync script (planned) |
| Hidden perf regressions | Establish benchmarks early |
| IL2CPP stripping converters | Provide link.xml template |

## Summary
Early structural pieces are present; validation and richer domain modeling are next. Migration/backwards compatibility work intentionally deferred until after core feature set matures.
