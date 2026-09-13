[← Back to index](index.md) · [← Back to page templates](page-templates.md)

# FaDocsTemplate

`FaSidebarShell` wrapped around an API reference or developer documentation layout: a hierarchy sidebar navigation tree, a breadcrumb and search top bar, an article header with title, method/version badge, and description, a main documentation body slot, an optional "On this page" table of contents right rail, and bottom previous/next page navigation links.

## Usage

```razor
@page "/docs/auth"
@using Fran.Templates

<FaDocsTemplate BrandText="API Docs" BrandHref="/docs"
                Title="Authentication & Tokens"
                Description="Learn how to authenticate requests to the API using Bearer tokens.">
    <Sidebar>
        <div class="docs-nav-tree">
            <h5>Getting Started</h5>
            <a href="/docs/overview">Overview</a>
            <a href="/docs/quickstart">Quickstart</a>
            <h5>Core Concepts</h5>
            <a href="/docs/auth" class="active">Authentication</a>
            <a href="/docs/pagination">Pagination</a>
            <h5>Endpoints</h5>
            <a href="/docs/users">Users</a>
            <a href="/docs/orders">Orders</a>
        </div>
    </Sidebar>

    <BreadcrumbContent>
        <span class="fa-text-muted">Docs › Core Concepts ›</span> <strong>Authentication</strong>
    </BreadcrumbContent>

    <Badge>
        <FaBadge Variant="FaBadgeVariant.Primary">v2.1</FaBadge>
    </Badge>

    <ChildContent>
        <h2 id="bearer-tokens">Bearer Tokens</h2>
        <p>All endpoints require a valid API key passed in the <code>Authorization</code> header:</p>
        <FaCodeBlock Language="FaCodeLanguage.Bash"
                     Code="curl -H 'Authorization: Bearer sec_xyz123' https://api.example.com/v1/profile" />
    </ChildContent>

    <TableOfContents>
        <a href="#bearer-tokens">Bearer Tokens</a>
        <a href="#expirations">Token Expirations</a>
        <a href="#rate-limits">Rate Limits</a>
    </TableOfContents>

    <PrevNextNav>
        <a href="/docs/quickstart">← Quickstart</a>
        <a href="/docs/pagination">Pagination →</a>
    </PrevNextNav>
</FaDocsTemplate>
```

## Getting the value

Pure layout — no bound value. Pass your nav hierarchy into `Sidebar`, breadcrumbs into `BreadcrumbContent`, quick search into `SearchContent`, doc body into `ChildContent`, and in-page anchor links into `TableOfContents`.

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
| `Sidebar` | `RenderFragment?` | docs hierarchy sidebar nav |
| `FooterContent` | `RenderFragment?` | overrides default footer |
| `HeaderPosition` / `FooterPosition` / `SidebarPosition` | `FaNavPosition` | `Standard` (default) \| `Sticky` \| `Floating` |
| `SidebarCollapsible` | `bool` | collapsible sidebar flag (defaults to `true`) |
| `ContainScroll` | `bool` | contained scroll flag |
| `BreadcrumbContent` | `RenderFragment?` | breadcrumb path trail above the title |
| `SearchContent` | `RenderFragment?` | search input or command palette trigger |
| `Title` | `string` | article heading; defaults to "Documentation" |
| `Badge` | `RenderFragment?` | version pill or HTTP method badge |
| `Description` | `string?` | lead text or summary below title |
| `TableOfContents` | `RenderFragment?` | right aside in-page navigation links |
| `PrevNextNav` | `RenderFragment?` | previous/next article links at bottom |
| `ChildContent` | `RenderFragment?` | main documentation body |

[← Back to index](index.md) · [← Back to page templates](page-templates.md)
