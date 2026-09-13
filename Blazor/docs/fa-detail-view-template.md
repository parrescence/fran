[← Back to index](index.md) · [← Back to page templates](page-templates.md)

# FaDetailViewTemplate

`FaSidebarShell` wrapped around a record detail or profile view layout: a back link or breadcrumbs trail, a record header row with title, status badge, subtitle, and action buttons, an optional navigation tabs strip (`FaTabs`), and a two-column or stacked layout with a main content body and an optional side summary card/panel.

## Usage

```razor
@page "/customers/123"
@using Fran.Templates

<FaDetailViewTemplate BrandText="MyApp" BrandHref="/"
                      IsAuthenticated="true" UserDisplayName="Jane Doe"
                      BackHref="/customers" BackText="Back to customers"
                      Title="Acme Corp"
                      Subtitle="Created on Sep 12, 2026 · ID: cust_123">
    <Sidebar>
        <NavMenu />
    </Sidebar>

    <StatusBadge>
        <FaBadge Variant="FaBadgeVariant.Success">Active</FaBadge>
    </StatusBadge>

    <Actions>
        <FaButton Variant="FaButtonVariant.Secondary">Edit</FaButton>
        <FaButton Variant="FaButtonVariant.Danger">Delete</FaButton>
    </Actions>

    <Tabs>
        <FaTabs TValue="string" @bind-Value="_currentTab" Tabs="_tabs" />
    </Tabs>

    <ChildContent>
        <FaCard>
            <h3>Account Details</h3>
            <p>Primary contact: John Doe (john@acme.com)</p>
        </FaCard>
    </ChildContent>

    <AsideContent>
        <FaCard>
            <h4>Billing Summary</h4>
            <p>Plan: Enterprise</p>
            <p>MRR: $1,200</p>
        </FaCard>
    </AsideContent>
</FaDetailViewTemplate>

@code {
    private string _currentTab = "overview";
    private readonly (string Value, string Label)[] _tabs =
    [
        ("overview", "Overview"),
        ("activity", "Activity"),
        ("invoices", "Invoices")
    ];
}
```

## Getting the value

Pure layout — no bound value. Pass your main record content into `ChildContent`, secondary summary info into `SideContent`, tabs into `TabsContent`, and header buttons into `Actions`.

## Parameters

| Parameter | Type | Notes |
|---|---|---|
| `BrandText` | `string` | **required** |
| `BrandHref` | `string` | |
| `BrandIconUrl` | `string?` | optional logo shown left of `BrandText` |
| `IsAuthenticated` | `bool` | |
| `UserDisplayName` / `UserImageUrl` / `UserEmail` | `string?` | |
| `UseAvatarForm` | `bool` | when true, renders `FaAvatarForm` in the topbar |
| `ShowUserNameInHeader` | `bool` | when `UseAvatarForm` is true, shows the user's name next to the avatar |
| `AccountHref` | `string?` | URL for account navigation |
| `OnAccountClick` / `OnLogin` / `OnLogout` | `EventCallback` | |
| `Sidebar` | `RenderFragment?` | main app navigation menu |
| `FooterContent` | `RenderFragment?` | overrides default footer |
| `HeaderPosition` / `FooterPosition` / `SidebarPosition` | `FaNavPosition` | `Standard` (default) \| `Sticky` \| `Floating` |
| `SidebarCollapsible` | `bool` | whether sidebar can collapse to icon rail |
| `ContainScroll` | `bool` | whether scroll is contained inside main viewport |
| `BackHref` | `string?` | return URL (e.g. "/items") |
| `BackText` | `string` | back link text; defaults to "Back" |
| `BreadcrumbContent` | `RenderFragment?` | breadcrumbs trail (overrides `BackHref` if provided) |
| `Title` | `string` | record or entity title; defaults to "Detail" |
| `StatusBadge` | `RenderFragment?` | status badge next to title |
| `Subtitle` | `string?` | metadata or subtitle text below title |
| `Actions` | `RenderFragment?` | action buttons on the right side of the header |
| `Tabs` | `RenderFragment?` | navigation tabs strip |
| `ChildContent` | `RenderFragment?` | primary detail body content |
| `AsideContent` | `RenderFragment?` | optional side summary panel or metadata card |

[← Back to index](index.md) · [← Back to page templates](page-templates.md)
