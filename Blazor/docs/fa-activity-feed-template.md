[← Back to index](index.md) · [← Back to page templates](page-templates.md)

# FaActivityFeedTemplate

`FaSidebarShell` wrapped around an activity feed / audit log layout: title, audit scope description, export action, quick summary statistics counters, a filter toolbar, a chronological event stream, and pagination.

## Usage

```razor
@page "/activity"
@using Fran.Templates
@using Fran.Components

<FaActivityFeedTemplate BrandText="MyApp" BrandHref="/"
                        Title="Activity &amp; Audit Trail"
                        Description="Chronological log of workspace operations, member logins, and system permissions.">
    <Sidebar>
        <nav class="fa-nav-tree">
            <a href="/settings">General</a>
            <a href="/billing">Billing</a>
            <a href="/activity" class="active">Audit Log</a>
        </nav>
    </Sidebar>

    <HeaderAction>
        <FaButton Variant="FaButtonVariant.Outline">Export CSV</FaButton>
    </HeaderAction>

    <SummaryStats>
        <FaCard>
            <h4>1,420</h4>
            <p class="fa-text-muted">Events this week</p>
        </FaCard>
        <FaCard>
            <h4>0</h4>
            <p class="fa-text-muted">Security alerts</p>
        </FaCard>
    </SummaryStats>

    <FilterToolbar>
        <FaInput Placeholder="Filter by user, action, or IP..." />
    </FilterToolbar>

    <ChildContent>
        <div class="fa-activity-item">
            <FaAvatar Name="Sarah Connor" />
            <div>
                <p><strong>Sarah Connor</strong> updated role for <strong>Kyle Reese</strong> to <em>Admin</em></p>
                <time class="fa-text-muted">10 minutes ago</time>
            </div>
        </div>
        <div class="fa-activity-item">
            <FaAvatar Name="System" />
            <div>
                <p><strong>System</strong> automatically renewed SSL certificates</p>
                <time class="fa-text-muted">1 hour ago</time>
            </div>
        </div>
    </ChildContent>

    <PaginationContent>
        <FaPagination Page="1" PageCount="12" />
    </PaginationContent>
</FaActivityFeedTemplate>
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
| `Sidebar` | `RenderFragment?` | navigation links in the side rail |
| `FooterContent` | `RenderFragment?` | overrides default footer |
| `HeaderPosition` / `FooterPosition` / `SidebarPosition` | `FaNavPosition` | `Standard` (default) \| `Sticky` \| `Floating` |
| `SidebarCollapsible` | `bool` | enables mobile drawer collapse |
| `ContainScroll` | `bool` | confines scroll to content pane |
| `Title` | `string` | page title; defaults to "Activity Feed" |
| `Description` | `string?` | scope description |
| `HeaderAction` | `RenderFragment?` | export action button |
| `SummaryStats` | `RenderFragment?` | quick metric summary cards |
| `FilterToolbar` | `RenderFragment?` | filter inputs and selects |
| `ChildContent` | `RenderFragment?` | timeline stream of events |
| `PaginationContent` | `RenderFragment?` | pagination controls |
