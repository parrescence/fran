# FaCharterTemplate

A comprehensive project charter layout embodying the Parrescence project charter standard. Features a cover banner with brand plate, kicker, headline, tagline, and metadata pills, alongside a sticky left-hand table of contents bookmark rail (`FaBookmarkNav`) and numbered sections (`01 Brand identity`, `02 Project makeup & methodology`, `03 DevOps agreement & CI/CD`, `04 Architecture & data model`, `05 Design system & tokens`, `06 Surface & page templates`, `07 Roadmap`, `08 Open decisions`, `09 References`) concluding with a living document footnote.

## Usage

```razor
@using Fran.Templates

<FaCharterTemplate BrandText="Vince"
                   BrandHref="/"
                   ProjectName="Vince"
                   Headline="Every dollar, touched once,"
                   HeadlineAccent="owned by the household that spent it."
                   Subtitle="Vince is the shared ledger a family actually keeps up with — who paid, what it was for, and whether it fit the budget."
                   Tagline="\"Your money's new best friend.\" — official Vince tagline"
                   Methodology="FaProjectMethodology.AgileScrum"
                   MethodologyDetails="2-week iterations · Daily standups · Sprint demo & retro"
                   DevOpsBranching="GitHub Flow: main (production) + dev (integration) + feature/* (ephemeral branches)"
                   DevOpsCiCd="GitHub Actions CI/CD with automated test suites and Azure deployment" />
```

## Project Methodology Forms

Supported project delivery forms (`FaProjectMethodology`):
- `FaProjectMethodology.AgileScrum` — Time-boxed sprints, backlog refinement, sprint review, retro.
- `FaProjectMethodology.AgileKanban` — Continuous pull system, WIP limits, cycle-time tracking.
- `FaProjectMethodology.Waterfall` — Phased gated delivery (Discovery, Architecture, Build, QA, Release).
- `FaProjectMethodology.Scrumban` — Iterative sprints with WIP limits and visual pull boards.
- `FaProjectMethodology.ExtremeProgramming` — Continuous automated testing, TDD, pair programming, daily releases.
- `FaProjectMethodology.ShapeUp` — Six-week cycles with two-week cool-downs and shaped pitches.
- `FaProjectMethodology.Hybrid` — Upfront architectural and compliance discovery with agile sprint releases.

## Parameters

| Parameter | Type | Default | Description |
|---|---|---|---|
| `ProjectName` | `string` | `"Project Charter"` | Project name. |
| `ProjectKicker` | `string` | `"Charter & Design System · Prepared for Parrescence"` | Kicker above headline. |
| `Headline` | `string` | — | Primary mission statement or banner headline. |
| `HeadlineAccent` | `string?` | `null` | Highlighted accent text appended to the headline. |
| `Subtitle` | `string` | — | Descriptive paragraph below the headline. |
| `Tagline` | `string?` | `null` | Optional brand tagline or quote. |
| `BrandPlateLogo` | `RenderFragment?` | `null` | Wordmark or brand logo mark rendered in the cover plate. |
| `BrandPlateLabel` | `string?` | `"Official Brand Kit"` | Label beside the brand mark in the cover plate. |
| `MetaPills` | `IReadOnlyList<string>?` | `null` | Technology and environment pills. |
| `Methodology` | `FaProjectMethodology` | `AgileScrum` | Project delivery framework. |
| `MethodologyDetails` | `string` | — | Cadence, sprint length, and ceremony structure. |
| `DevOpsBranching` | `string` | — | Git branch protection and PR workflow. |
| `DevOpsCiCd` | `string` | — | Continuous integration and deployment pipeline description. |
| `QualityGates` | `IReadOnlyList<string>?` | `null` | Automated quality criteria required for merge. |
| `Environments` | `IReadOnlyList<string>?` | `null` | Deployment environment tiers (e.g. Dev, Staging, Prod). |
| `BookmarkItems` | `IReadOnlyList<FaBookmarkItem>?` | Default 9 sections | Custom bookmark items for the left sticky rail. |
| `ActiveSectionId` | `string?` | `null` | Currently active bookmark target ID. |
| `ClosingFootnote` | `string?` | `null` | Living document footnote text. |
