[← Back to index](index.md) · [← Back to page templates](page-templates.md)

# FaRoadmapTemplate

`FaStandardShell` wrapped around a public or team product roadmap: title, description, header action button (such as "Suggest a feature"), filter toolbar, and a 3-column Kanban board (Planned, In Progress, Completed).

## Usage

```razor
@page "/roadmap"
@using Fran.Templates
@using Fran.Components

<FaRoadmapTemplate BrandText="MyApp" BrandHref="/"
                   Title="Product Roadmap"
                   Description="Follow our upcoming releases and vote on planned capabilities.">
    <HeaderAction>
        <FaButton Variant="FaButtonVariant.Primary">Suggest a Feature</FaButton>
    </HeaderAction>

    <FilterBar>
        <FaBadge Variant="FaBadgeVariant.Primary">All Areas</FaBadge>
        <FaBadge Variant="FaBadgeVariant.Neutral">Core App</FaBadge>
        <FaBadge Variant="FaBadgeVariant.Neutral">Integrations</FaBadge>
    </FilterBar>

    <PlannedColumn>
        <FaCard>
            <h4>Audit Logging v2</h4>
            <p class="fa-text-muted">Export system telemetry to Datadog and Splunk.</p>
        </FaCard>
    </PlannedColumn>

    <InProgressColumn>
        <FaCard>
            <h4>Mobile Companion App</h4>
            <p class="fa-text-muted">Native iOS and Android clients for offline inspection.</p>
        </FaCard>
    </InProgressColumn>

    <CompletedColumn>
        <FaCard>
            <h4>Dark Mode Engine</h4>
            <p class="fa-text-muted">Contrast-compliant system palette switching.</p>
        </FaCard>
    </CompletedColumn>
</FaRoadmapTemplate>
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
| `Title` | `string` | roadmap title; defaults to "Roadmap" |
| `Description` | `string?` | description / intro |
| `HeaderAction` | `RenderFragment?` | header action button (e.g. feature request button) |
| `FilterBar` | `RenderFragment?` | category or status filter chips |
| `PlannedColumn` | `RenderFragment?` | items planned for future sprints |
| `InProgressColumn` | `RenderFragment?` | items actively being engineered |
| `CompletedColumn` | `RenderFragment?` | recently shipped items |
| `ChildContent` | `RenderFragment?` | optional custom timeline or board content |
