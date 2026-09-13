[← Back to index](index.md) · [← Back to page templates](page-templates.md)

# FaListViewTemplate

`FaSidebarShell` wrapped around an administrative or SaaS data list/table layout: a header row with entity title, count badge, primary and secondary actions, a search and filter toolbar, a primary table/grid container, and a pagination footer.

## Usage

```razor
@page "/customers"
@using Fran.Templates

<FaListViewTemplate BrandText="MyApp" BrandHref="/"
                    IsAuthenticated="true" UserDisplayName="Jane Doe"
                    Title="Customers"
                    Description="Manage your client directory and account statuses.">
    <Sidebar>
        <NavMenu />
    </Sidebar>

    <TitleBadge>
        <FaBadge Variant="FaBadgeVariant.Neutral">1,248 total</FaBadge>
    </TitleBadge>

    <PrimaryAction>
        <FaButton Variant="FaButtonVariant.Primary" Href="/customers/new">Add customer</FaButton>
    </PrimaryAction>

    <SecondaryActions>
        <FaButton Variant="FaButtonVariant.Secondary">Export</FaButton>
    </SecondaryActions>

    <SearchFilterBar>
        <FaInput TValue="string" Placeholder="Search customers..." @bind-Value="_search" />
        <FaSelect TValue="string" @bind-Value="_status">
            <option value="">All statuses</option>
            <option value="active">Active</option>
            <option value="lead">Lead</option>
        </FaSelect>
    </SearchFilterBar>

    <ChildContent>
        <FaTable TItem="Customer" Items="_customers">
            <!-- columns -->
        </FaTable>
    </ChildContent>

    <PaginationContent>
        <FaPagination CurrentPage="1" TotalPages="10" PageChanged="HandlePage" />
    </PaginationContent>
</FaListViewTemplate>

@code {
    private string _search = "";
    private string _status = "";
    private record Customer(string Name, string Email, string Status);
    private readonly List<Customer> _customers = [];

    private void HandlePage(int p) { /* page change */ }
}
```

## Getting the value

Pure layout — no bound value. Pass your table/grid into `ChildContent`, filter inputs into `SearchFilterBar`, buttons into `PrimaryAction` / `SecondaryActions`, and pager into `PaginationContent`.

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
| `Title` | `string` | entity title; defaults to "Items" |
| `TitleBadge` | `RenderFragment?` | badge or count tag next to the title |
| `Description` | `string?` | subtitle below title |
| `PrimaryAction` | `RenderFragment?` | main action button (e.g. "Create item") |
| `SecondaryActions` | `RenderFragment?` | secondary action buttons (e.g. Export, Import) |
| `SearchFilterBar` | `RenderFragment?` | search inputs, select filters, date pickers |
| `ChildContent` | `RenderFragment?` | the data table, grid, or item cards |
| `PaginationContent` | `RenderFragment?` | pagination controls or table summary |

[← Back to index](index.md) · [← Back to page templates](page-templates.md)
