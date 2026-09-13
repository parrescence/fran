[← Back to index](index.md) · [← Back to page templates](page-templates.md)

# FaBlogIndexTemplate

`FaStandardShell` wrapped around a blog index / listing layout: headline, description, newsletter/subscribe action, highlighted featured post hero, category filter pills, responsive card grid, and pagination.

## Usage

```razor
@page "/blog"
@using Fran.Templates
@using Fran.Components

<FaBlogIndexTemplate BrandText="MyApp" BrandHref="/"
                     Title="Engineering &amp; Product Blog"
                     Description="Insights into modern Blazor architecture, frontend performance, and UI design.">
    <HeaderAction>
        <FaButton Variant="FaButtonVariant.Outline">Subscribe to RSS</FaButton>
    </HeaderAction>

    <CategoryBar>
        <FaBadge Variant="FaBadgeVariant.Primary">All</FaBadge>
        <FaBadge Variant="FaBadgeVariant.Neutral">Engineering</FaBadge>
        <FaBadge Variant="FaBadgeVariant.Neutral">Design</FaBadge>
    </CategoryBar>

    <FeaturedPost>
        <FaCard>
            <span class="fa-text-muted">Featured · Oct 14, 2026</span>
            <h3>Announcing Fran UI Component Suite v1.0</h3>
            <p>A ground-up rebuild of enterprise Blazor primitives with zero third-party dependencies.</p>
            <FaButton Variant="FaButtonVariant.Primary" Href="/blog/announcing-v1">Read article →</FaButton>
        </FaCard>
    </FeaturedPost>

    <ChildContent>
        <FaCard>
            <h4>Optimizing WebAssembly Startup</h4>
            <p class="fa-text-muted">How trimming and lazy loading cut bundle sizes by 60%.</p>
        </FaCard>
        <FaCard>
            <h4>Contrast Ratios and Theme Swapping</h4>
            <p class="fa-text-muted">Building dynamic WCAG AAA palettes using pure CSS variables.</p>
        </FaCard>
    </ChildContent>

    <PaginationContent>
        <FaPagination Page="1" PageCount="5" />
    </PaginationContent>
</FaBlogIndexTemplate>
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
| `Title` | `string` | blog title; defaults to "Blog" |
| `Description` | `string?` | blog description |
| `HeaderAction` | `RenderFragment?` | header action button (e.g. RSS / subscribe) |
| `FeaturedPost` | `RenderFragment?` | hero card for featured article |
| `CategoryBar` | `RenderFragment?` | category / tag filter chips |
| `ChildContent` | `RenderFragment?` | grid of blog post cards |
| `PaginationContent` | `RenderFragment?` | pagination controls at the bottom |
