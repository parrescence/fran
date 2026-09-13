[← Back to index](index.md) · [← Back to page templates](page-templates.md)

# FaAboutTemplate

`FaStandardShell` wrapped around an about / company introduction layout: a title and subtitle hero with optional media visual, key statistics/numbers grid, mission/story content, a team members grid, and a closing call-to-action banner.

## Usage

```razor
@page "/about"
@using Fran.Templates

<FaAboutTemplate BrandText="MyApp" BrandHref="/"
                 Title="We build tools for craftspeople"
                 Description="Founded in 2024, we're dedicated to helping small and medium teams build world-class products."
                 MissionTitle="Our Mission">
    <StatsContent>
        <div class="fa-stat-item">
            <h2>15,000+</h2>
            <p>Active workspaces</p>
        </div>
        <div class="fa-stat-item">
            <h2>99.99%</h2>
            <p>Platform uptime</p>
        </div>
        <div class="fa-stat-item">
            <h2>50+</h2>
            <p>Countries represented</p>
        </div>
    </StatsContent>

    <MissionContent>
        <p>We started with a single conviction: business software doesn't have to be sluggish or visually bloated. By focusing on fast rendering, accessible primitives, and thoughtful interactions, we empower builders everywhere.</p>
    </MissionContent>

    <TeamContent>
        <FaCard>
            <h4>Jane Doe</h4>
            <p class="fa-text-muted">CEO &amp; Co-founder</p>
        </FaCard>
        <FaCard>
            <h4>John Smith</h4>
            <p class="fa-text-muted">Head of Product</p>
        </FaCard>
    </TeamContent>

    <CtaContent>
        <h3>Ready to build together?</h3>
        <p>Explore our open positions or start your free trial today.</p>
        <div class="fa-flex fa-justify-center fa-gap-2 fa-mt-3">
            <FaButton Variant="FaButtonVariant.Primary" Href="/pricing">Get started</FaButton>
            <FaButton Variant="FaButtonVariant.Secondary" Href="/contact">Contact us</FaButton>
        </div>
    </CtaContent>
</FaAboutTemplate>
```

## Getting the value

Pure layout — no bound value. Fill in your content using the named slots (`StatsContent`, `MissionContent`, `TeamContent`, `CtaContent`, `HeroMedia`, `ChildContent`). Shell events behave identically to `FaStandardShell`.

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
| `Title` | `string` | hero title; defaults to "About our mission" |
| `Description` | `string?` | hero description/subheading |
| `HeroMedia` | `RenderFragment?` | optional hero image or video embed |
| `StatsContent` | `RenderFragment?` | key metrics / numbers grid |
| `MissionTitle` | `string?` | mission section heading; defaults to "Our Story" |
| `MissionContent` | `RenderFragment?` | mission / company narrative text |
| `TeamTitle` | `string?` | team section heading; defaults to "Meet our team" |
| `TeamDescription` | `string?` | team section description |
| `TeamContent` | `RenderFragment?` | team cards or portraits |
| `CtaContent` | `RenderFragment?` | bottom call-to-action banner |
| `ChildContent` | `RenderFragment?` | optional extra content |

[← Back to index](index.md) · [← Back to page templates](page-templates.md)
