# AI Scaffold

This folder contains curated, reproducible AI assistance assets (policy, prompts, scripts, and small summaries). Keep any raw or private GPT session data outside this folder under `contexts/` (gitignored) or `ai-local/` (gitignored).

## Why this exists
- Be transparent about AI use without leaking private context.
- Keep committed artifacts small, text-only, and reproducible.
- Provide prompt templates and scripts that anyone can re-run locally.

## Layout
```
ai/
  README.md                   (this file)
  prompts/                    (sanitized reusable prompt templates)
    refactor-template.txt
    test-generation-template.txt
  scripts/                    (regeneration scripts & docs)
    build-summaries.ps1
    build-summaries.md
  summaries/                  (tracked, small derived artifacts)
    .keep                     (placeholder; real JSON appears after running scripts)
```

Private (ignored) locations you can use:
- `contexts/` – your raw GPT sessions, context packs, scratch notes (never committed)
- `ai-local/` – any local caches or experimental outputs (never committed)

## Workflow
1. Keep raw/sensitive context in `contexts/` (ignored) — not in `ai/`.
2. Run the summary script to generate small, shareable artifacts in `ai/summaries/`.
3. Review diffs and commit only meaningful changes.

### Generate summaries (Windows, cmd.exe)
```cmd
powershell -NoProfile -ExecutionPolicy Bypass -File ai\scripts\build-summaries.ps1 -Session s1
```
Outputs:
- `ai/summaries/architecture-projects.json` – all `*.csproj` + target frameworks
- `ai/summaries/namespace-distribution.json` – count of source files per namespace

## Conventions
- Do commit: small JSON/MD summaries under `ai/summaries/` that are reproducible from repo state.
- Do not commit: raw transcripts, large context dumps, embeddings, secrets.
- Prompt templates live in `ai/prompts/` and should avoid leaking private details.

## Safety
- `.gitignore` excludes `contexts/`, `ai-local/`, `*.gpt-context.json`, `*.ai-context.json`.
- Optional pre-commit hook: copy `tools/hooks/pre-commit.sample.sh` to `.git/hooks/pre-commit` to block accidental context commits.

## See also
- `AI_USAGE.md` – policy, review checklist, and best practices.
- `ai/scripts/build-summaries.md` – script usage details and extension ideas.

---
Last updated: 2025-10-09

