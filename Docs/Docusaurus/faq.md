---
id: faq
title: FAQ
sidebar_label: FAQ
sidebar_position: 95
---

### Why float timestamps instead of double?
Compactness and sufficient precision for many real-time XR / neuro-experiment timescales. Conversions to double / DateTimeOffset are supported.

### Why no backward compatibility layer yet?
Pre-alpha policy: reduce maintenance drag; finalize naming & patterns first.

### Will System.Text.Json be supported?
Likely as an optional backend once core stabilizes; current focus is feature completeness, not serializer abstraction.

### Why not auto-normalize quaternions now?
Normalization drift is not yet the dominant failure; a simple zero-magnitude guard delivers most safety. Auto-normalization will be optional later to avoid hiding upstream data quality issues.

### Are negative timestamps allowed?
Yes, flagged as warnings—useful for offsets or pre-session calibration windows.

### Can I add my own validators?
Yes—implement `IValidator<T>` and either use directly or register in your own catalog wrapper.

### How do I map to Unity types?
Use a companion integration (future MINDFlow adapters). Core deliberately avoids UnityEngine references.

### When will device / event records arrive?
They are Phase 1: needed before deeper validation & exporter expansion.

### How stable is the namespace?
`Packages.MindFrame.*` is provisional; expect a simplified root later.
