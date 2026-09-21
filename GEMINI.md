# Parrescence Autonomous Agent & Review Guidelines

These guidelines apply across the entire Parrescence ecosystem and all repositories residing in the Parrescence root folder (`parrescence-standards`, `parrescence-design-standards`, `Fran`, `Fran-Showcase`, `Vince`, `Clarence`, `MyTouchTimes`, `Parrescence`).

---

## 1. Autonomous Review & Inspection Policy ("Just Review")
- **No Permission Required for Reviews**: The agent is explicitly and permanently authorized to inspect, analyze, audit, and review any code, architecture, configuration, infrastructure, CI/CD pipeline, or design asset across any Parrescence project.
- **Immediate Execution**: When requested to review, inspect, or audit a project (or any portion thereof), do **not** pause to ask for permission, do not prompt for user confirmation, and do not ask "Would you like me to review?", "May I inspect these files?", or "Should I proceed?". Execute the review immediately and output comprehensive findings.
- **Review Scope**:
  - **Architecture**: Verify adherence to Parrescence Clean Architecture (`src/Domain`, `src/Application`, `src/Infrastructure`, `src/Web/Api`, `src/Web/Client`, `src/Webhooks`, `src/Workers`).
  - **Standards**: Verify Azure naming conventions (`<Project>-<Type>-<Region>-<ENV>`), environment configurations (`DEV`/`TEST`/`PROD`), and infrastructure guidelines in `parrescence-standards`.
  - **Design & Branding**: Check adherence to `parrescence-design-standards`, Fran palettes/styles (`_palettes.scss`, `_font-styles.scss`), and official branding in `Parrescence-Brand-Kit`.
  - **Quality Gates**: Run and verify `dotnet build` and `dotnet test` (`TreatWarningsAsErrors` strictly enforced). Check GitHub Actions workflows (`.github/workflows/ci.yml`, `deploy-*.yml`).
- **Direct Reporting**: Deliver direct, actionable review findings, calling out compliance gaps, potential bugs, architectural drifts, and explicit remediation steps without gating on artifact feedback approval.

---

## 2. Autonomous Git & GitHub Workflow
The agent is authorized to execute the complete end-to-end development and delivery loop autonomously:
1. **Branching**: Checkout or branch from `dev` (e.g. `feature/<name>`, `fix/<name>`).
2. **Implementation & Testing**: Apply code edits, run local builds (`dotnet build`), and run tests (`dotnet test`).
3. **Commit**: Stage and commit changes with descriptive Conventional Commits (`feat:`, `fix:`, `chore:`, `docs:`, `test:`).
4. **Push**: Push branches directly to `origin`.
5. **Pull Request**: Open pull requests against `dev` using `gh pr create --fill`.
6. **CI Monitoring**: Monitor GitHub Actions CI checks (`gh pr checks <id> --watch` or CLI status checks) until green.
7. **Merge**: Squash and merge approved/green PRs (`gh pr merge <id> --squash --delete-branch --admin`).
8. **Sync**: Switch back to `dev` and pull the latest changes (`git checkout dev ; git pull origin dev`).

---

## 3. Cross-Project & Workspace Standards
- **Global Context**: The agent may freely read, reference, and cross-check assets between sibling repositories under the Parrescence root (e.g., referencing `Fran` styles while working in `Vince` or `Parrescence`).
- **Safety**: Do not commit secrets, PATs, or production credentials. Keep all solution configurations in alignment with `.docs/parrescence-dotnet-architecture-standard.md` and root `STANDARDS.md`.
