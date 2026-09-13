[← Back to index](index.md) · [← Back to page templates](page-templates.md)

# FaTutorialTemplate

`FaStandardShell` wrapped around a how-to / tutorial layout: breadcrumb trail, difficulty level and read time tags, tutorial headline and description, author info, quick-jump step aside navigation, numbered step body cards, and previous/next tutorial footer links.

## Usage

```razor
@page "/tutorials/building-dashboards"
@using Fran.Templates
@using Fran.Components

<FaTutorialTemplate BrandText="MyApp" BrandHref="/"
                    Title="Building your first realtime dashboard"
                    Description="A step-by-step guide to streaming live metrics using WebSockets and Fran data grids."
                    Difficulty="Intermediate"
                    ReadTime="12 min read">
    <BreadcrumbContent>
        <span>Tutorials &gt; Blazor &gt; Data Visualization</span>
    </BreadcrumbContent>

    <CategoryBadge>
        <FaBadge Variant="FaBadgeVariant.Primary">WebAssembly</FaBadge>
    </CategoryBadge>

    <AuthorContent>
        <FaAvatar Name="Alex Mercer" />
        <div>
            <strong>Alex Mercer</strong>
            <div class="fa-text-muted fa-font-sm">Frontend Architect</div>
        </div>
    </AuthorContent>

    <StepJumpList>
        <ol class="fa-list-steps">
            <li><a href="#step-1">1. Configure Project</a></li>
            <li><a href="#step-2">2. Stream Data</a></li>
            <li><a href="#step-3">3. Bind FaGrid</a></li>
        </ol>
    </StepJumpList>

    <ChildContent>
        <section id="step-1" class="fa-mb-4">
            <h3>Step 1: Configure Project</h3>
            <p>Install the required packages and register the service bus.</p>
        </section>
        <section id="step-2" class="fa-mb-4">
            <h3>Step 2: Stream Data</h3>
            <p>Listen to events emitted by the telemetry channel.</p>
        </section>
        <section id="step-3" class="fa-mb-4">
            <h3>Step 3: Bind FaGrid</h3>
            <p>Connect the live stream directly to the reactive data grid.</p>
        </section>
    </ChildContent>

    <PrevNextNav>
        <FaButton Variant="FaButtonVariant.Outline" Href="/tutorials/intro">← Previous: Intro</FaButton>
        <FaButton Variant="FaButtonVariant.Primary" Href="/tutorials/auth">Next: Auth &amp; Roles →</FaButton>
    </PrevNextNav>
</FaTutorialTemplate>
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
| `Title` | `string` | tutorial headline; defaults to "Tutorial" |
| `Description` | `string?` | introductory summary |
| `Difficulty` | `string?` | difficulty label, e.g. "Beginner", "Intermediate" |
| `ReadTime` | `string?` | estimated read time, e.g. "12 min read" |
| `BreadcrumbContent` | `RenderFragment?` | breadcrumbs above the title |
| `CategoryBadge` | `RenderFragment?` | topic badge pill |
| `AuthorContent` | `RenderFragment?` | author credit slot |
| `StepJumpList` | `RenderFragment?` | sticky aside step navigation |
| `ChildContent` | `RenderFragment?` | main tutorial steps and code examples |
| `PrevNextNav` | `RenderFragment?` | previous/next navigation footer |
