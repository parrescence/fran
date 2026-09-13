[← Back to index](index.md) · [← Back to page templates](page-templates.md)

# FaBlogPostTemplate

`FaStandardShell` wrapped around a single-article blog post layout: back navigation link, article title, subtitle, author details, publication date and read time, hero cover image, structured prose body, author bio card, social sharing buttons, and related articles.

## Usage

```razor
@page "/blog/building-blazor-apps"
@using Fran.Templates
@using Fran.Components

<FaBlogPostTemplate BrandText="MyApp" BrandHref="/"
                    BackHref="/blog"
                    BackText="← Back to Engineering Blog"
                    Title="Building Scalable Blazor Applications"
                    Subtitle="Architectural patterns, memory management, and clean code principles."
                    PublishedDate="October 14, 2026"
                    ReadTime="8 min read">
    <CategoryBadge>
        <FaBadge Variant="FaBadgeVariant.Primary">Architecture</FaBadge>
    </CategoryBadge>

    <AuthorContent>
        <FaAvatar Name="Alex Mercer" />
        <span>Alex Mercer</span>
    </AuthorContent>

    <ChildContent>
        <p>Blazor WebAssembly gives .NET developers the ability to execute C# code client-side inside the browser. In this deep dive, we explore state container lifecycles, virtualized data tables, and high-frequency UI updates.</p>
        <h2>Component Boundaries</h2>
        <p>Keeping UI components small and decoupled is essential for minimal re-render churn.</p>
    </ChildContent>

    <ShareContent>
        <span>Share article:</span>
        <FaButton Variant="FaButtonVariant.Outline">Copy Link</FaButton>
    </ShareContent>

    <AuthorBio>
        <h4>About Alex Mercer</h4>
        <p class="fa-text-muted">Alex is a Principal Engineer specializing in high-performance web systems and frontend architectures.</p>
    </AuthorBio>

    <RelatedPosts>
        <FaCard>
            <h4>Memory Profiling in WebAssembly</h4>
            <a href="/blog/memory-profiling">Read more →</a>
        </FaCard>
    </RelatedPosts>
</FaBlogPostTemplate>
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
| `BackHref` | `string?` | back navigation link URL |
| `BackText` | `string` | back navigation link label; defaults to "← Back to Blog" |
| `BackAction` | `RenderFragment?` | custom back action slot |
| `Title` | `string` | article title |
| `Subtitle` | `string?` | article subtitle |
| `PublishedDate` | `string?` | formatted publication date |
| `ReadTime` | `string?` | estimated read time |
| `CategoryBadge` | `RenderFragment?` | category / topic badge |
| `AuthorContent` | `RenderFragment?` | author avatar and name |
| `CoverImageUrl` | `string?` | hero image URL |
| `CoverImageAlt` | `string?` | hero image alt text |
| `ChildContent` | `RenderFragment?` | article body markdown or markup |
| `ShareContent` | `RenderFragment?` | sharing and bookmark actions |
| `AuthorBio` | `RenderFragment?` | author biography box |
| `RelatedPosts` | `RenderFragment?` | recommended / related articles grid |
