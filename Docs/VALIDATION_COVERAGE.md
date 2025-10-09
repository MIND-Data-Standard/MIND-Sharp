# Validation Coverage (Pre-Alpha)

This matrix tracks which primitives/types have structural validation helpers.

| Type | Core Defined | Serialization Specific Converter | Validation Implemented | Notes |
|------|--------------|----------------------------------|------------------------|-------|
| MindVector3 | Yes | Default JSON (fields) | Vector3Validator | Checks finite components |
| MindQuaternion | Yes | Default JSON | QuaternionValidator | Checks non-zero magnitude |
| MindTimestamp | Yes | TimestampConverter | TimestampValidator | Finite & non-negative (warn) |
| MindPose | Yes | Default JSON | PoseValidator | Delegates to vector + quaternion; warns Unknown space |
| MindTransform | Yes | Default JSON | TransformValidator | Scale zero-component warning, quaternion check |
| Timestamp sequence (list) | n/a | n/a | TimestampSequenceValidator | Monotonic check |

## Pending / Potential Future Validators
- Vector magnitude range constraints (application-specific)
- Quaternion normalization tolerance warning
- Spatial continuity (delta thresholds) for pose/transform sequences

## Serialization Notes
- Current primitives use public fields (X,Y,Z,W,Value). Newtonsoft CamelCase resolver does not lowercase single-letter field names. If JSON spec requires lowercase (x,y,z,w), add explicit `[JsonProperty]` attributes or custom contract resolver in a future pass.

## ModelIO
- Generic JSONL exporter works for all current types; no type-specific exporters required yet.
