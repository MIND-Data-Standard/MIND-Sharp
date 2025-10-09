---
id: dev-contributing
title: Contributing (Pre-Alpha)
sidebar_label: Contributing
sidebar_position: 81
---

## Philosophy
Keep PRs **narrow** (one conceptual change). Emphasis is on correctness, clarity, and future interoperability.

## Contribution Steps
1. **Discuss (Optional):** Open a discussion/issue if adding new categories (records, exporters).
2. **Fork / Branch:** Use a feature branch named `feature/<short-desc>`.
3. **Implement:** Follow coding standards and supply validator + doc updates if introducing new types.
4. **Self-Validate:** Run validators against any new composite test data.
5. **Document:** Update Docusaurus docs (primitives/types/validators) + CHANGELOG if user-facing.
6. **Submit PR:** Provide rationale, trade-offs, and any benchmarks.
7. **Review:** Expect feedback on naming, validation scope, and layering adherence.

## PR Checklist
- [ ] No legacy namespaces added
- [ ] Validators updated (if structural changes)
- [ ] Docs updated (relevant section)
- [ ] No duplicate symbol definitions
- [ ] CHANGELOG updated (if user-visible)

## Anti-Goals (Pre-Alpha)
- Backward compatibility layers
- Over-abstracted serialization switches
- Premature micro-optimizations without profiling evidence

## Communication
Decisions with cross-cutting impact (namespace, validation severity semantics) should be recorded in a session context file for continuity.
