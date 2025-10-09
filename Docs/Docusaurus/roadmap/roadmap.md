---
id: roadmap
title: Roadmap (Pre-Alpha)
sidebar_label: Roadmap
sidebar_position: 100
---

> Order may change as feedback and priorities evolve. No backward compatibility guarantees until post pre-alpha.

## Phase 0 (Done / In Progress)
- Primitives & spatial composites
- JSON facade + timestamp converter
- JSONL exporter
- Structural validators (primitives + spatial + timestamps)

## Phase 1 (Next)
- Record shells ([Participant](../records/participant.md), [Device](../records/device.md), [Event](../records/event.md), [Recording](../records/recording.md))
- Vector / quaternion drift & finite component enforcement (see [Quaternion Integrity](../validation/quaternion-integrity.md))
- Pose / Transform sequence continuity checks (future `PoseSequenceValidator`, `TransformSequenceValidator` – see [Validation Coverage](../validation/validation-coverage.md))
- CHANGELOG version tagging cadence

## Phase 2
- Deep semantic validation (optional schema layer)
- Interoperability mapping docs (NWB, BIDS, OpenXR, LSL)
- Performance benchmark harness (Editor vs IL2CPP vs Mono)

## Phase 3
- Parquet & Arrow exporters
- Tokenization helpers (chunking, alignment)
- Normalization & compression experiments (quaternion packing)

## Phase 4
- Configurable validation thresholds (profiles)
- Real-time streaming adapters (may land in MINDFlow)
- Optional System.Text.Json backend swap

## Stretch / Research
- Code generation from authoritative schemas
- Adaptive precision (dynamic float/double promotion)
- Semantic provenance tracking (data lineage attachments)

## Non-Goals (Now)
- Production-grade networking layer (belongs to MINDFlow)
- Backward compatibility shims for interim namespaces
