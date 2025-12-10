# DEVPS-001-Use-Conventional-Commits

Date: 6 December 2025

Status: Accepted

## Status

Accepted

## Context

We want to maintain a clean, consistent, and machine-readable Git history for the URL Shortener project.

Key reasons:

1.**Automation**: Conventional Commit messages enable automatic changelog generation and semantic versioning in the future.

2.**Collaboration**: Enforcing a shared standard helps all contributors write meaningful commit messages.

3.**Quality gates**: Prevents accidental commits with vague messages like “fix stuff” or “update code”.

## Options considered

1.**Free-form commit messages**: No enforcement, relies on developer discipline.
2.**Git hooks with custom scripts**: DIY validation, more maintenance.
3.**Husky + Commitlint + Conventional Commits**: Industry standard, supported ecosystem, integrates with CI.

## Decision

We will adopt Conventional Commits as the commit message format, enforced locally with Husky and Commitlint, and validated remotely in CI.

1.**Local enforcement**: Husky runs a commit-msg Git hook. Commitlint checks the commit message against the Conventional Commits spec.

2.**Remote enforcement**: A GitHub Action (commitlint-github-action) validates all commits in PRs. This ensures that no commit can bypass validation, even if local hooks are disabled.

## Consequences

### Positive

1. Commit history is structured and readable.
2.We can later adopt semantic-release to automate versioning and changelogs.
3.Developers receive fast feedback locally (via Husky hooks).
3.CI provides an additional safety net.

### Negative

1.Developers need Node.js installed to run Husky + Commitlint locally.
2.Small learning curve for new contributors unfamiliar with Conventional Commits.
