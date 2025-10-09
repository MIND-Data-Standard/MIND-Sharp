# AI Usage Policy (MindFrame C#)

Purpose: Document responsible, transparent usage of AI (LLM/GPT-style) assistance when contributing to this repository while protecting any private context.

## Scope
Applies to: code, documentation, test generation, refactors, and summarization activities performed with AI tooling.

## Guiding Principles
1. Human Accountability: All AI-assisted output must be reviewed, simplified where possible, and tested by a human contributor.
2. Minimal Exposure: Raw private session context or transcripts are never committed (kept only under `contexts/` or `ai-local/`, both ignored).
3. Reproducibility: Any durable AI-derived artifact committed to the repo must be reproducible via a script in `ai/scripts/`.
4. License Safety: Do not paste large passages from sources with incompatible licenses. Assume AI output may include fragments from its training; scrutinize unusual verbatim blocks.
5. Privacy & Secrets: Never include secrets, tokens, API keys, or proprietary third-party data in prompts that will be stored.

## Approved AI Uses
- Generating draft unit tests or benchmark harnesses (must pass & be idiomatic after review).
- Creating refactor proposals (final code must compile, maintain style, and include reasoning in the PR description).
- Summarizing architecture or module responsibilities (committed summaries go under `ai/summaries/`).
- Generating prompt templates (stored under `ai/prompts/`).

## Disallowed
- Committing raw conversation logs or full unfiltered model transcripts.
- Introducing speculative APIs or types that do not compile just to "reserve a name".
- Large-scale code generation (> ~300 lines net new) without prior design review or issue/discussion alignment.
- Relying on AI for cryptographic, security-critical, or licensing decisions without maintainer review.

## Attribution (Optional)
If a substantial chunk (> ~20 contiguous lines) of code remains largely unchanged from an AI draft, add a brief comment once near its start:
```csharp
// AI-assisted initial draft (human reviewed)
```
Do not over-attribute trivial changes.

## Workflow for Derived Artifacts
1. Place raw / bulky / exploratory context in `contexts/` (ignored) or `ai-local/` (ignored).
2. Run regeneration scripts (e.g., `ai/scripts/build-summaries.ps1`) to produce stable summaries.
3. Commit only the concise outputs in `ai/summaries/`.
4. CI may later verify summaries are current (planned).

## Prompt Templates
Reusable, sanitized prompt scaffolds live in `ai/prompts/`. They should:
- Avoid leaking private context.
- Explain constraints (framework versions, coding style expectations).
- Encourage minimal diffs for refactors.

## Security & Secret Handling
- Treat any accidental secret appearance as a security incident: rotate key + purge commit.
- Use a local `.git/hooks/pre-commit` (sample in `tools/hooks/`) to block adding ignored context paths.

## Review Checklist for AI-Assisted PRs
- [ ] All code compiles and tests pass.
- [ ] No raw transcripts or private context files included.
- [ ] License / attribution safe (no large verbatim unknown-origin blocks).
- [ ] Summary artifacts (if any) are reproducible (note script & params in PR description).
- [ ] No speculative dead code or unused abstractions.

## Future Enhancements (Planned)
- CI job to regenerate architecture summary & compare to committed version.
- Automated grep for forbidden sentinel strings (e.g., `PRIVATE_CONTEXT_ONLY`).
- Optional embedding index builder (stored locally only).

## Questions / Updates
Propose policy adjustments via a PR or discussion. Material policy changes require maintainer approval.

---
Last updated: 2025-10-09

