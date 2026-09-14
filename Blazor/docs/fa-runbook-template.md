# FaRunbookTemplate

Operational Runbook & Playbook template designed for SRE and engineering teams. Features system dependency tables, on-call escalation matrices, severity tier definitions & response SLAs, diagnostic CLI commands, common failure mode recovery recipes, zero-downtime rollback procedures, and post-mortem guidelines — paired with a sticky bookmark rail (`FaBookmarkNav`) for rapid incident response navigation.

## Usage

```razor
@using Fran.Templates

<FaRunbookTemplate BrandText="Operations"
                   BrandHref="/"
                   SystemName="Vince Payment Engine"
                   SystemDescription="Critical transaction settlement and ledger balancing service."
                   ServiceTier="Tier 1 — Mission Critical"
                   SlaTarget="99.95% Availability (< 21.9 min downtime / month)"
                   PrimaryOnCall="SRE Primary (PagerDuty Schedule)"
                   SecondaryOnCall="Platform Engineering Lead"
                   WarRoomChannel="#incident-war-room (Slack) / Bridge"
                   StatusPageUrl="https://status.parrescence.com" />
```

## Standard Sections

1. `01 System overview & topology` (`#overview`) — Architecture, hosting providers, dependency criticality tiers.
2. `02 On-call & escalation matrix` (`#escalation`) — Escalation tiers, notification schedules, and SLA response windows.
3. `03 Severity tiers & SLAs` (`#severity`) — SEV-1 through SEV-4 definitions, response time targets, and update cadences.
4. `04 Health checks & telemetry` (`#health`) — Synthetic probe curl snippets and health monitoring queries.
5. `05 Diagnostic commands & triage` (`#diagnostics`) — Live log tailing and Application Insights KQL queries.
6. `06 Common failures & recovery` (`#recovery`) — Actionable troubleshooting playbooks for rate-limiting, DB throttling, and token validation.
7. `07 Deployment & rollback` (`#rollback`) — Zero-downtime slot swapping and commit revert procedures.
8. `08 Post-mortem guidelines` (`#post-mortem`) — 5 Whys analysis, blameless timeline reconstruction, and corrective action item tracking.

## Parameters

| Parameter | Type | Default | Description |
|---|---|---|---|
| `SystemName` | `string` | `"Core API & Service Engine"` | Service or component title. |
| `SystemDescription` | `string` | — | Short overview of operational purpose and guarantees. |
| `ServiceTier` | `string` | `"Tier 1 — Mission Critical"` | Criticality classification tier. |
| `SlaTarget` | `string` | `"99.95% Availability"` | Uptime and availability guarantee. |
| `PrimaryOnCall` | `string` | — | Primary on-call engineer or PagerDuty schedule. |
| `SecondaryOnCall` | `string` | — | Secondary escalation engineer or platform lead. |
| `WarRoomChannel` | `string` | — | Incident communication war room or bridge. |
| `StatusPageUrl` | `string?` | `null` | Optional external status dashboard URL. |
| `BookmarkItems` | `IReadOnlyList<FaBookmarkItem>?` | Default 8 sections | Custom bookmark items for the left rail. |
| `ActiveSectionId` | `string?` | `null` | Active bookmark section anchor ID. |
| `ClosingFootnote` | `string?` | `null` | Operational closing footnote. |
