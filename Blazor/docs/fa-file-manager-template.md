[← Back to index](index.md) · [← Back to page templates](page-templates.md)

# FaFileManagerTemplate

`FaSidebarShell` wrapped around a file manager / drive storage layout: title, storage quota indicator, folder breadcrumb navigation, search bar, upload / new folder action buttons, folder and file grid or table, and a file detail inspector side drawer.

## Usage

```razor
@page "/files"
@using Fran.Templates
@using Fran.Components

<FaFileManagerTemplate BrandText="MyApp" BrandHref="/"
                       Title="Files">
    <Sidebar>
        <nav class="fa-nav-tree">
            <a href="/files" class="active">My Drive</a>
            <a href="/files/shared">Shared with me</a>
            <a href="/files/trash">Trash</a>
        </nav>
    </Sidebar>

    <StorageQuotaContent>
        <span class="fa-text-muted fa-font-sm">45.2 GB of 100 GB used (45%)</span>
    </StorageQuotaContent>

    <BreadcrumbContent>
        <span>Drive &gt; Projects &gt; Q4 Assets</span>
    </BreadcrumbContent>

    <SearchContent>
        <FaInput Placeholder="Search files..." />
    </SearchContent>

    <SecondaryActions>
        <FaButton Variant="FaButtonVariant.Outline">New Folder</FaButton>
    </SecondaryActions>

    <PrimaryAction>
        <FaButton Variant="FaButtonVariant.Primary">+ Upload</FaButton>
    </PrimaryAction>

    <ChildContent>
        <div class="fa-grid fa-grid-cols-3 fa-gap-3">
            <FaCard>
                <h4>Brand_Guidelines_2026.pdf</h4>
                <p class="fa-text-muted">4.2 MB · Modified yesterday</p>
            </FaCard>
            <FaCard>
                <h4>Telemetry_Export.csv</h4>
                <p class="fa-text-muted">128 KB · Modified 2 hours ago</p>
            </FaCard>
        </div>
    </ChildContent>

    <InspectorContent>
        <h4>File Details</h4>
        <p><strong>Brand_Guidelines_2026.pdf</strong></p>
        <p class="fa-text-muted">Size: 4.2 MB<br />Type: PDF Document<br />Owner: Sarah Connor</p>
        <FaButton Variant="FaButtonVariant.Primary">Download</FaButton>
    </InspectorContent>
</FaFileManagerTemplate>
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
| `Title` | `string` | title; defaults to "Files" |
| `BreadcrumbContent` | `RenderFragment?` | folder path breadcrumbs |
| `StorageQuotaContent` | `RenderFragment?` | storage quota indicator |
| `SearchContent` | `RenderFragment?` | search files input |
| `PrimaryAction` | `RenderFragment?` | upload action button |
| `SecondaryActions` | `RenderFragment?` | new folder, view switcher buttons |
| `ChildContent` | `RenderFragment?` | files and folders grid/table |
| `InspectorContent` | `RenderFragment?` | file metadata inspector aside |
