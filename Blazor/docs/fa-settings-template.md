[← Back to index](index.md) · [← Back to page templates](page-templates.md)

# FaSettingsTemplate

`FaSidebarShell` wrapped around an application settings layout: a page header with title and description, and a two-column body combining a category sub-navigation sidebar (Account, Security, Team, Billing) and a structured settings card panel with header, content body, and action footer.

## Usage

```razor
@page "/settings"
@using Fran.Templates

<FaSettingsTemplate BrandText="MyApp" BrandHref="/"
                    IsAuthenticated="true" UserDisplayName="Jane Doe"
                    Title="Settings"
                    Description="Manage your personal preferences and workspace configuration."
                    SectionTitle="General Settings"
                    SectionDescription="Update your basic account profile details.">
    <Sidebar>
        <NavMenu />
    </Sidebar>

    <SettingsNav>
        <a href="/settings" class="active">General</a>
        <a href="/settings/security">Security</a>
        <a href="/settings/notifications">Notifications</a>
        <a href="/settings/billing">Billing</a>
    </SettingsNav>

    <ChildContent>
        <FaInput TValue="string" Label="Workspace name" @bind-Value="_workspace" />
        <FaInput TValue="string" Label="Support email" @bind-Value="_email" />
    </ChildContent>

    <SectionFooter>
        <FaButton Variant="FaButtonVariant.Primary" OnClick="SaveSettings">Save changes</FaButton>
        <FaButton Variant="FaButtonVariant.Secondary" OnClick="Reset">Cancel</FaButton>
    </SectionFooter>
</FaSettingsTemplate>

@code {
    private string _workspace = "Acme Inc";
    private string _email = "admin@acme.com";

    private void SaveSettings() { /* save */ }
    private void Reset() { /* reset */ }
}
```

## Getting the value

Pure layout — no bound value. Pass your sub-navigation links into `SettingsNav`, your form controls into `ChildContent`, and save/cancel action buttons into `SectionFooter`.

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
| `Title` | `string` | overall page title; defaults to "Settings" |
| `Description` | `string?` | subtitle below title |
| `SettingsNav` | `RenderFragment?` | sub-nav links or tabs for settings categories |
| `SectionTitle` | `string?` | title for active settings section |
| `SectionDescription` | `string?` | description for active settings section |
| `SectionFooter` | `RenderFragment?` | action buttons at bottom of settings card |
| `ChildContent` | `RenderFragment?` | settings fields / controls |

[← Back to index](index.md) · [← Back to page templates](page-templates.md)
