# FaDevOpsAgreementCard

A standardized card component presenting a team's DevOps agreement, project delivery makeup (`FaProjectMethodology`), git branching strategy, quality gates checklist, environment tiers, and cloud secrets policy. Can be used standalone in documentation, onboarding pages, how-tos, or embedded in `FaCharterTemplate`.

## Usage

```razor
@using Fran.Components
@using Fran.Templates

<FaDevOpsAgreementCard Methodology="FaProjectMethodology.AgileScrum"
                       MethodologyDetails="2-week iterations · Daily standup · Sprint review & retro"
                       BranchingStrategy="GitHub Flow: main (production) + dev (integration) + feature/* (ephemeral branches)"
                       CiCdPipeline="GitHub Actions CI/CD with automated test suites and Azure deployment"
                       QualityGates="@(new[] { "100% unit & integration test pass rate", "Zero compiler warnings / linter errors", "Branch protection rules with mandatory review" })"
                       Environments="@(new[] { "Dev", "Staging", "Production" })"
                       SecretsManagement="Azure Key Vault + Managed Identities (Zero plaintext secrets in git)" />
```

## Parameters

| Parameter | Type | Default | Description |
|---|---|---|---|
| `Methodology` | `FaProjectMethodology` | `AgileScrum` | Project delivery form (Agile, Scrum, Waterfall, Kanban, Hybrid, etc.). |
| `MethodologyDetails` | `string` | — | Cadence and ceremony description. |
| `BranchingStrategy` | `string` | — | Git branching policy. |
| `CiCdPipeline` | `string` | — | Pipeline engine and automated workflow details. |
| `QualityGates` | `IReadOnlyList<string>` | Standard 4 gates | Merge gate criteria (tests, linting, code reviews). |
| `Environments` | `IReadOnlyList<string>` | `["Dev", "Staging", "Production"]` | Deployment tiers rendered as pills. |
| `SecretsManagement` | `string` | — | Security & secrets governance posture. |
| `CssClass` | `string?` | `null` | Additional CSS classes. |
| `ChildContent` | `RenderFragment?` | `null` | Optional custom clauses or team notes. |
