---
id: serialization-json
title: JSON Serialization
sidebar_label: JSON
sidebar_position: 30
---

MindFrame uses a centralized JSON facade: `MindJson`.

## Goals
- Consistent camelCase output
- Deterministic inclusion of defaults when meaningful
- Ignore nulls to reduce payload size
- Strict: treat unknown members as errors to surface drift early

## Implementation Snapshot
```csharp
public static class MindJson {
    static MindJson() => DefaultSettings = CreateDefaultSettings();
    public static JsonSerializerSettings DefaultSettings { get; }
    public static string Serialize<T>(T value);
    public static T Deserialize<T>(string json);
}
```

### Timestamp Converter
Timestamps are serialized as floating seconds since Unix epoch (UTC). This is compact, stream-friendly, and still convertible to `DateTimeOffset`.

### Unity / Newtonsoft Note
Pre-alpha uses Unity's packaged Newtonsoft build. Future dual-backend option (System.Text.Json) is a potential enhancement; no tooling yet.

## Custom Converters (Planned)
| Concept | Purpose | Status |
|---------|---------|--------|
| Enum-as-strings enforcement | Stability & human readability | Planned |
| Compressed Quaternion (optional) | Bandwidth reduction | Planned |
| Flexible Timestamp Precision | Switch float<->double with define | Planned |

## Guidelines for Adding New Types
1. Keep public fields simple (avoid properties needing reflection overhead initially).
2. If external type mapping needed (e.g., UnityEngine types) do it **outside** core (MINDFlow adapters).
3. Add validation before adding complex serializers (fail early with human-readable messages).

## JSONL Export
See [JSONL Exporter](../exporters/jsonl.md) for record-per-line streaming format.
