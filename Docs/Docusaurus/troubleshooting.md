---
id: troubleshooting
title: Troubleshooting
sidebar_label: Troubleshooting
sidebar_position: 90
---

## Common Issues

### Duplicate Type Errors
Cause: Accidental concatenation of multiple definitions into one file.  
Fix: Ensure each struct/class lives in its own file; remove duplicates. (Occurred earlier with `ValidationRunner`.)

### Zero / Near-Zero Quaternion
Error `quat.zero`: Input quaternion magnitude squared < 1e-8. Replace with identity or correct source calculations.

### Non-Monotonic Timestamps
Error `timestamps.nonMonotonic`: Data ordering issue—ensure acquisition clock or buffering preserves sequence.

### Missing Converters in IL2CPP
Add a link.xml entry preserving converter types once the set stabilizes.

### NaN / Infinity Components
Check upstream math (division by zero, invalid normalization). Filter or clamp before serialization.

## Diagnostic Strategy
| Symptom | Likely Cause | Action |
|---------|-------------|--------|
| NaNs in serialized JSON | Invalid quaternion/vector inputs | Run validators before serialize |
| Jitter between frames | Non-normalized rotation drift | (Future) enable normalization warning mode |
| Large JSON size | Redundant defaults / uncompressed output | Consider post-export compression (gzip/zstd) |

## Logging Policy (Planned)
Provide structured logs (code + severity) with aggregation counters to avoid flooding consoles.
