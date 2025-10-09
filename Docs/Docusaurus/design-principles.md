---
id: design-principles
title: Design Principles
sidebar_label: Principles
sidebar_position: 70
---

Guiding principles shaping MindFrame’s evolution.

## 1. Explicit Over Implicit
Units (metres), frames (`MindSpace`), and temporal semantics (epoch seconds) are explicitly encoded.

## 2. Fail Fast, Not Loud Forever
Surface structural errors early. Avoid log spam for benign drift until strict modes are opted into.

## 3. Small Surface First
Stabilize core primitives; expand the schema breadth *after* validation + exporter foundations exist.

## 4. Portability
No UnityEngine dependency—portable to any .NET Standard 2.1 environment.

## 5. Interoperability Mindset
Design with future mapping to NWB, BIDS, OpenXR, LSL; avoid premature vendor-specific decisions.

## 6. Foundation-Model Readiness
Prefer formats and structures conducive to large-scale tokenization (flat, explicit, consistent naming).

## 7. Configuration Minimization (Early)
Avoid heavy configuration until behavior & constraints are proven in practice.

## 8. Performance Awareness
Use struct value types and field-based layouts; avoid heap churn and reflection until necessary.

## 9. Observability
Validation issues provide structured codes enabling downstream metrics & dashboards later.

## 10. Evolution > Backward Compatibility (Pre-Alpha)
Move quickly; prune or rename aggressively before stability promises are made.
