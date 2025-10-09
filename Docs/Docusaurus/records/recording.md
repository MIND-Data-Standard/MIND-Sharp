---
id: record-recording
title: Recording Record (Planned)
sidebar_label: Recording
sidebar_position: 63
---

Top-level session / acquisition container metadata.

## Proposed Fields (Draft)
| Field | Type | Purpose |
|-------|------|---------|
| id | string | Recording/session identifier |
| participantId | string | Links to participant record |
| startTimestamp | MindTimestamp | Session start |
| endTimestamp | MindTimestamp? | Session end |
| devices | DeviceRef[] | Device inventory snapshot |
| notes | string? | Free-form annotations |
| attributes | map<string,string> | Extensible key/value metadata |

## Validation (Future)
- startTimestamp <= endTimestamp (if end defined)
- Participant + referenced devices must exist

## Interop
- NWB file-level metadata
- BIDS dataset/session JSON sidecars
---
id: record-event
title: Event Record (Planned)
sidebar_label: Event
sidebar_position: 62
---

Timestamped semantic or experimental event marker.

## Proposed Fields (Draft)
| Field | Type | Purpose |
|-------|------|---------|
| timestamp | MindTimestamp | Event time |
| code | string | Short machine-readable tag |
| label | string? | Human readable description |
| payload | object/map | Arbitrary structured metadata |

## Validation (Future)
- Monotonic ordering within streams
- Required: `timestamp`, `code`

## Interop
- BIDS events.tsv
- NWB TimeSeries annotations
---
id: record-device
title: Device Record (Planned)
sidebar_label: Device
sidebar_position: 61
---

Describes a hardware or virtual sensor/device contributing data.

## Proposed Fields (Draft)
| Field | Type | Purpose |
|-------|------|---------|
| id | string | Unique device identifier |
| model | string | Device model name |
| manufacturer | string | Vendor or maker |
| modality | string | e.g. imu, camera, eeg, hand-tracker |
| sampleRateHz | float? | Nominal sampling rate |
| attributes | map<string,string> | Additional extensible metadata |

## Validation (Future)
- `id`, `modality` required
- sampleRateHz > 0 if present

## Interop
- BIDS: `*_channels.tsv` + device JSON sidecars
- NWB: `Device` entity mapping
---
id: record-participant
title: Participant Record (Planned)
sidebar_label: Participant
sidebar_position: 60
---

> Planned structure capturing subject / participant level metadata.

## Proposed Fields (Draft)
| Field | Type | Purpose |
|-------|------|---------|
| id | string | Stable participant identifier |
| demographic | object | Optional age / handedness / etc. (extensible) |
| attributes | map<string,string> | Arbitrary tagged metadata |

## Validation (Future)
- Required: `id`
- Optional: Controlled vocab for demographic subfields

## Interoperability Notes
- Map to BIDS `participants.tsv`
- Link to NWB subject fields
---
id: architecture-data-flow
title: Data Flow Diagram
sidebar_label: Data Flow
sidebar_position: 11
---

High-level conceptual flow (current + planned components):

```
[ Sensors / Runtime Sources ]
           |
           v
    (Future) MINDFlow Capture Layer
           |
           v
   MindFrame Primitives & Types
           |
           v
   Validation (Structural -> Semantic*)
           |
           v
   Serialization (MindJson / Converters)
           |
           |----> JSON (debug / logs)
           |----> JSONL (stream / dataset)
           |----> (Planned) Parquet / Arrow
           |
           v
   Downstream Pipelines (Analytics, ML, Tokenization)
```
*Semantic, schema-driven & streaming validators planned.

## Notes
- MindFrame is agnostic to transport; MINDFlow handles real-time networking.
- Exporter layer will expand with columnar formats when schema breadth increases.
---
id: getting-started
title: Getting Started
sidebar_label: Getting Started
sidebar_position: 1
---

A minimal end‑to‑end example: create a transform, validate it, serialize it, and export a JSONL stream.

## 1. Create Data
```csharp
using Packages.MindFrame.Core.Primitives;
using Packages.MindFrame.Core.Types;
using Packages.MindFrame.Core.Enums;

var pose = new MindPose(
    new MindVector3(0.2f, 1.0f, -0.4f),
    new MindQuaternion(1,0,0,0),
    MindSpace.World);
```

## 2. Validate (Structural)
```csharp
using Packages.MindFrame.Validation;
var validation = ValidationRunner.Run(pose, ValidationCatalog.Pose);
if (validation.HasErrors)
{
    foreach (var issue in validation.Issues)
        UnityEngine.Debug.LogError(issue); // or Console.WriteLine
}
```

## 3. Serialize to JSON
```csharp
using Packages.MindFrame.Serialization;
string json = MindJson.Serialize(pose);
```

## 4. Export JSON Lines
```csharp
using System.IO;
using Packages.MindFrame.ModelIO;
var poses = new [] { pose, MindPose.Identity };
using var writer = File.CreateText("session.poses.jsonl");
JsonlExporter.WriteRecords(poses, writer);
```

## 5. Timestamp Handling
```csharp
var nowTs = new MindTimestamp((float)(System.DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()/1000.0));
```
Convert back:
```csharp
var dto = nowTs.ToDateTimeOffset();
```

## 6. Common Pitfalls
| Issue | Cause | Fix |
|-------|------|-----|
| `quat.zero` | Zero / near-zero quaternion | Use identity (1,0,0,0) or recompute rotation |
| `vector3.component.invalid` | NaN/∞ component | Sanitize upstream math |
| Monotonic timestamp error | Out-of-order acquisition | Buffer & sort or ensure single clock source |

## 7. What Next?
- Extend types: implement record shells (Participant, Device)
- Add semantic validation when available
- Explore [JSONL Exporter](./exporters/jsonl.md) details

## 8. Quick Checklist
- [ ] Pose validates
- [ ] JSON produced
- [ ] JSONL file produced
- [ ] No structural errors
---
id: intro
title: MindFrame C# (Pre-Alpha)
sidebar_label: Introduction
sidebar_position: 0
---

**Status:** Pre-alpha. APIs and namespaces may change without notice. No backward compatibility guarantees.

MindFrame (C#) provides the structural layer for the MIND ecosystem: strongly typed primitives, spatial composites, serialization/utilities, and early validation for multimodal behavioral / neural data.

> MINDFlow handles *how data moves*; MindFrame defines *what the data is*.

## Key Goals
- Explicit primitives (units, frames, timing)
- Low-overhead, deterministic serialization
- Early structural validation (semantic later)
- Foundation-model friendly export paths

## Current Snapshot
| Area | Implemented |
|------|-------------|
| Primitives (Vector3, Quaternion, Timestamp) | ✅ |
| Spatial Types (Pose, Transform) | ✅ |
| JSON Facade / Timestamp Converter | ✅ |
| JSONL Exporter | ✅ |
| Structural Validators | ✅ (basic) |
| Record / Event / Device Types | ⏳ Planned |
| Deep Schema Validation | ⏳ Planned |
| Parquet / Arrow Exporters | ⏳ Planned |

## Start Here
Read [Getting Started](./getting-started.md) for a minimal end‑to‑end code sample.

## Architecture & Roadmap
- [Architecture Overview](./architecture/overview.md)
- [Data Flow Diagram](./architecture/data-flow.md)
- [Roadmap](./roadmap/roadmap.md)

## Validation Focus
See [Validation Overview](./validation/overview.md) and detailed [Quaternion Integrity](./validation/quaternion-integrity.md).

## Design Principles
Captured in [Design Principles](./design-principles.md).

