# 🧠 MindFrame (C# Core – Pre-Alpha)

> Structural layer for the MIND Data Standard: strongly-typed primitives, spatial types, and serialization utilities. Current development uses an interim namespace root `Packages.MindFrame.*`; a future simple rename to `MindFrame.*` may occur during pre-alpha without backward compatibility guarantees.

---

## ✨ Current Snapshot Features
- Core spatial + temporal primitives: `MindVector3`, `MindQuaternion`, `MindTimestamp`
- Composite spatial types: `MindPose`, `MindTransform`
- JSON facade (`MindJson`) with timestamp converter (Newtonsoft via Unity package)
- JSONL exporter (`JsonlExporter`) for line-delimited streaming / dataset prep
- Pure C# (.NET Standard 2.1) – **no** `UnityEngine` dependency
- Modular assemblies: Core, Serialization, ModelIO (Validation scaffold only)

> NOTE: Rich record types (participants, devices, events), validation logic, and interoperability mappings are not implemented yet.

---

## 📦 Assemblies
| Assembly | Purpose | Namespace Root (Interim) |
|----------|---------|--------------------------|
| MindFrame.Core | Primitives & basic composite types | `Packages.MindFrame.Core.*` |
| MindFrame.Serialization | JSON settings + converters | `Packages.MindFrame.Serialization` |
| MindFrame.ModelIO | Export helpers (JSONL) | `Packages.MindFrame.ModelIO` |
| MindFrame.Validation | (placeholder) | *(pending)* |

See `Docs/ARCHITECTURE.md` for details.

---

## 🔤 Namespaces (Development Notice)
The `Packages.MindFrame.*` prefix is a temporary convenience during early development. Backwards compatibility with interim namespaces will *not* be maintained; downstream code should be prepared to update if a simplified `MindFrame.*` prefix is introduced.

---

## 🧩 Design Principles
- Minimal, explicit data model; no implicit Unity types
- Serialization stability (camelCase JSON, explicit units, deterministic field order via types)
- Low overhead by default; deep validation will be opt-in
- Foundation-model friendly export formats (line-delimited JSON now; Parquet/Arrow planned)

---

## 🚧 Roadmap (Near-Term)
1. Structural validation primitives (null / range / enum / timestamp monotonicity)
2. Additional composite & record types (devices, events, participant, recording)
3. Interoperability mapping docs (NWB, BIDS, OpenXR, LSL)
4. Export pipelines: Parquet / Arrow + tokenization helpers
5. Performance benchmarks (Editor vs Mono vs IL2CPP) for serialization + allocations
6. Optional JsonSchema-based semantic validation (`MIND_SCHEMA_VALIDATION` symbol)

---

## 🛠 Usage (Current State)
```csharp
using Packages.MindFrame.Core.Primitives; // interim namespace
using Packages.MindFrame.Serialization;

var pose = Packages.MindFrame.Core.Types.MindPose.Identity;
string json = MindJson.Serialize(pose);
```

---

## 🧪 Testing & Validation
Validation layer is not yet implemented. Add your own assertions around value ranges, monotonic timestamps, and required fields until `MindFrame.Validation` ships.

---

## 🤝 Contributing (Early Phase)
Pre-alpha: architectural feedback and lightweight PRs (typos, docs, small primitives) welcome. Larger feature proposals: open a discussion first outlining schema alignment and interoperability impact.

Coordinate with the broader MIND ecosystem (Standard + Flow) for cross-repo concerns.

---

## 📄 License
Code: MIT (see root license for final text in repository).  
Specification materials (in the MIND-Standard repo) remain CC-BY-4.0.

---

## 📚 Related
| Layer | Repo (Concept) | Role |
|-------|----------------|------|
| Standard | MIND-Standard | Schemas & governance |
| Frame (this) | MindFrame (C#) | Structural model + serialization |
| Flow | MINDFlow-Unity | Runtime capture / streaming |

---

## 🗺️ Status Summary
MindFrame is in a structural bootstrap phase: primitives + basic serialization exist; validation and richer records are the immediate next steps.

---

## 🛠️ Building & Consuming DLLs (Experimental SDK Layout)

Root solution: `MindFrame-CSharp.sln` (aggregates all SDK-style projects under `src/Mind.CSharp/`).

Directory structure (SDK sources):
```
src/
  Mind.CSharp/
    MindFrame.Core/
    MindFrame.Serialization/
    MindFrame.Validation/
    MindFrame.ModelIO/
    MindFrame.Tests/
    Directory.Build.props
```

### Targets
All runtime libraries multi-target: `netstandard2.1; net8.0`.  
Tests target: `net8.0`.

### One-Time Prerequisites
- .NET SDK 8.x installed (`dotnet --version` should print 8.*)
- (Optional) NuGet API key for publishing.

### Quick Build
```bash
dotnet restore MindFrame-CSharp.sln
dotnet build MindFrame-CSharp.sln -c Release
```
Artifacts (DLLs) output to each project’s `bin/Release/<TFM>/`.

### Pack (Local NuGet Packages)
```bash
dotnet pack src/Mind.CSharp/MindFrame.Core/MindFrame.Core.csproj -c Release -o dist
dotnet pack src/Mind.CSharp/MindFrame.Serialization/MindFrame.Serialization.csproj -c Release -o dist
dotnet pack src/Mind.CSharp/MindFrame.Validation/MindFrame.Validation.csproj -c Release -o dist
dotnet pack src/Mind.CSharp/MindFrame.ModelIO/MindFrame.ModelIO.csproj -c Release -o dist
```
Result: `.nupkg` + `.snupkg` in `dist/` (create if absent).

### Consuming (Project Reference)
Add to a consumer `.csproj`:
```xml
<ItemGroup>
  <PackageReference Include="MindFrame.Core" Version="0.1.0" />
  <PackageReference Include="MindFrame.Serialization" Version="0.1.0" />
  <PackageReference Include="MindFrame.Validation" Version="0.1.0" />
  <PackageReference Include="MindFrame.ModelIO" Version="0.1.0" />
</ItemGroup>
```

### Namespaces (Migration Plan)
Current code still uses `Packages.MindFrame.*`. A phased migration to `MindFrame.*` will introduce parallel namespaces before marking the old ones `[Obsolete]`. Avoid hard‑coding assumptions; alias imports where practical:
```csharp
using CoreVec = Packages.MindFrame.Core.Primitives.MindVector3;
```

### Test Execution
```bash
dotnet test MindFrame-CSharp.sln -c Release --filter TestCategory!=Slow
```
(Tests are minimal bootstrap; more coverage incoming with temporal layer & validators.)

### Upcoming Build Enhancements
- CI workflow (GitHub Actions) for build + test + pack (`.github/workflows/ci.yml`).
- SourceLink already configured (see Directory.Build.props).
- Optional `MIND_COMPACT_JSON` / `MIND_LENIENT_JSON` feature symbols.

> NOTE: Unity package still sources original files; DLL consumption path is experimental until namespace migration completes.

---
