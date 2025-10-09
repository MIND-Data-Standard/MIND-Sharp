---
id: exporters-jsonl
title: JSONL Exporter
sidebar_label: JSONL
sidebar_position: 50
---

MindFrame includes a simple JSON Lines (JSONL) exporter for streaming-friendly, foundation-model oriented workflows.

## Rationale
- Line-delimited JSON is easy to concatenate, shard, and stream.
- Works with numerous tooling ecosystems (Unix, Python, ML pipelines).
- Amenable to incremental ingestion & tokenization.

## API
```csharp
public static class JsonlExporter {
  public static void WriteRecords<T>(IEnumerable<T> records, TextWriter writer);
  public static void WriteRecord<T>(T record, TextWriter writer);
}
```
Records are serialized via `MindJson` with the default settings.

## Usage
```csharp
using Packages.MindFrame.ModelIO;
using Packages.MindFrame.Serialization;

var transforms = new List<MindTransform> { /* ... */ };
using var file = File.CreateText("session.transforms.jsonl");
JsonlExporter.WriteRecords(transforms, file);
```

## Considerations
| Aspect | Note |
|--------|------|
| Ordering | Caller defines ordering; exporter is append-only. |
| Atomicity | No transaction semantics; partial files possible if aborted. |
| Compression | Use external tools (gzip, zstd) after generation. |
| Schema Evolution | Add version metadata at higher record layers (future). |

## Future Enhancements
| Feature | Purpose |
|---------|---------|
| Async streaming variant | Reduce blocking on large writes |
| Buffered batch writer | Fewer syscalls for high-frequency data |
| Partition naming helpers | Organized multi-file exports |
| Metadata preamble | Header line(s) for downstream context |

## Comparison
| Format | Pros | Cons |
|--------|------|------|
| JSONL | Human-readable, simple | Larger size, numeric text cost |
| Parquet (planned) | Columnar, compressible | Higher complexity, dependency weight |
| Arrow (planned) | Zero-copy analytics pipelines | Requires vectorized consumer |
