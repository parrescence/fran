[← Back to index](index.md) · [← Back to page templates](page-templates.md)

# FaChangelogTemplate

`FaStandardShell` wrapped around a release notes and product changelog layout: a header row with title, description, and action buttons (RSS subscribe, filter), an optional category filter bar, and a chronological timeline stream of product releases.

## Usage

```razor
@page "/changelog"
@using Fran.Templates

<FaChangelogTemplate BrandText="MyApp" BrandHref="/"
                     Title="Changelog"
                     Description="New features, fixes, and improvements shipped to our platform.">
    <HeaderActions>
        <FaButton Variant="FaButtonVariant.Secondary">Subscribe via RSS</FaButton>
    </HeaderActions>

    <FilterContent>
        <FaChip Label="All" Selected="true" />
        <FaChip Label="Features" />
        <FaChip Label="Fixes" />
        <FaChip Label="Performance" />
    </FilterContent>

    <ChildContent>
        <div class="changelog-entry">
            <div class="changelog-meta">
                <FaBadge Variant="FaBadgeVariant.Primary">v2.4.0</FaBadge>
                <span class="changelog-date">Sep 10, 2026</span>
            </div>
            <h3>Advanced Analytics &amp; CSV Exports</h3>
            <p>Added flexible date filtering and high-speed CSV streaming for large datasets.</p>
            <ul>
                <li>Added: Multi-column sorting in data tables</li>
                <li>Fixed: Date picker timezone offset issue in UTC+9</li>
                <li>Improved: 40% faster initial bundle load</li>
            </ul>
        </div>
    </ChildContent>
</FaChangelogTemplate>
```

## Getting the value

Pure layout — no bound value. Pass your action buttons into `HeaderActions`, filter chips into `FilterContent`, and release entries into `ChildContent`.

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
| `Title` | `string` | changelog headline; defaults to "Changelog" |
| `Description` | `string?` | subheading text below title |
| `HeaderActions` | `RenderFragment?` | action buttons (RSS, Subscribe, etc.) |
| `FilterContent` | `RenderFragment?` | category pills or version filters |
| `ChildContent` | `RenderFragment?` | chronological release entries |

[← Back to index](index.md) · [← Back to page templates](page-templates.md)
