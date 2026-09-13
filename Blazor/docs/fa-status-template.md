[← Back to index](index.md) · [← Back to page templates](page-templates.md)

# FaStatusTemplate

`FaStandardShell` wrapped around a system status &amp; uptime layout: title, last updated timestamp, subscribe to updates action, prominent overall system status banner with status dot, service component health statuses, historical 90-day uptime metrics, and an incident resolution log.

## Usage

```razor
@page "/status"
@using Fran.Templates
@using Fran.Components

<FaStatusTemplate BrandText="MyApp" BrandHref="/"
                  Title="System Status"
                  OverallStatus="All Systems Operational"
                  OverallStatusVariant="FaBadgeVariant.Success"
                  LastUpdated="Refreshed 30 seconds ago">
    <HeaderAction>
        <FaButton Variant="FaButtonVariant.Outline">Subscribe to Updates</FaButton>
    </HeaderAction>

    <ServicesContent>
        <FaCard class="fa-mb-2">
            <div class="fa-flex fa-justify-between fa-items-center">
                <span>API Gateway (US-East)</span>
                <FaBadge Variant="FaBadgeVariant.Success">Operational</FaBadge>
            </div>
        </FaCard>
        <FaCard class="fa-mb-2">
            <div class="fa-flex fa-justify-between fa-items-center">
                <span>Database Cluster</span>
                <FaBadge Variant="FaBadgeVariant.Success">Operational</FaBadge>
            </div>
        </FaCard>
        <FaCard class="fa-mb-2">
            <div class="fa-flex fa-justify-between fa-items-center">
                <span>Notification Engine</span>
                <FaBadge Variant="FaBadgeVariant.Success">Operational</FaBadge>
            </div>
        </FaCard>
    </ServicesContent>

    <UptimeMetricsContent>
        <FaCard>
            <div class="fa-flex fa-justify-between">
                <span>Overall Uptime (Last 90 Days)</span>
                <strong>99.98%</strong>
            </div>
        </FaCard>
    </UptimeMetricsContent>

    <PastIncidentsContent>
        <FaCard>
            <h4>September 28 — Scheduled Database Maintenance</h4>
            <p class="fa-text-muted">Completed cleanly with zero client downtime during the maintenance window.</p>
        </FaCard>
    </PastIncidentsContent>
</FaStatusTemplate>
```

## Parameters

| Parameter | Type | Notes |
|---|---|---|
| `BrandText` | `string` | **required** |
| `BrandHref` | `string` | |
| `BrandIconUrl` | `string?` | optional logo shown left of `BrandText` |
| `IsAuthenticated` | `bool` | |
| `UserDisplayName` / `UserImageUrl` / `UserEmail` | `string?` | |
| `UseAvatarForm` | `bool` | when true, renders `FaAvatarForm` in topbar |
| `ShowUserNameInHeader` | `bool` | when true with avatar form, shows user name |
| `AccountHref` | `string?` | account page link |
| `OnAccountClick` / `OnLogin` / `OnLogout` | `EventCallback` | |
| `FooterContent` | `RenderFragment?` | overrides default footer |
| `HeaderPosition` / `FooterPosition` | `FaNavPosition` | `Standard` (default) \| `Sticky` \| `Floating` |
| `Title` | `string` | status title; defaults to "System Status" |
| `OverallStatus` | `string` | overall summary text (e.g. "All Systems Operational") |
| `OverallStatusVariant` | `FaBadgeVariant` | `Success`, `Danger`, `Primary`, `Neutral` |
| `LastUpdated` | `string?` | timestamp string |
| `HeaderAction` | `RenderFragment?` | subscribe to updates action button |
| `ServicesContent` | `RenderFragment?` | service &amp; API component list |
| `UptimeMetricsContent` | `RenderFragment?` | 90-day uptime bars and metrics |
| `PastIncidentsContent` | `RenderFragment?` | past incident log |
| `ChildContent` | `RenderFragment?` | optional extra content |
