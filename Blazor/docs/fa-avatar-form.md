[← Back to index](index.md)

# FaAvatarForm

An avatar-triggered profile and session menu designed for app headers and topbars.

In its resting state, `FaAvatarForm` renders a clean avatar trigger. You can configure whether the user's name appears alongside the avatar in the topbar or whether only the avatar is shown.

When the user selects their avatar, it opens a contextual panel containing:
- **User Identity**: Avatar photo or initials, display name, and email or role subtitle.
- **Account Navigation**: A direct action/link to navigate to the user's account form to update their picture and information.
- **Application Items**: A slot (`ChildContent`) for any host-application-specific information, navigation links, or actions.
- **Theme Selection**: Built-in Light / Dark / Colorblind mode switcher.
- **Login / Logout Action**: A prominent Log in or Log out action button.

## Usage

### Topbar with only avatar (default)

```razor
<FaAvatarForm IsAuthenticated="true"
              DisplayName="Jane Doe"
              ImageUrl="https://example.com/avatar.jpg"
              Email="jane@example.com"
              AccountHref="/account"
              OnLogout="HandleLogout" />
```

### Topbar showing user name alongside avatar

Set `ShowDisplayName="true"` to display the user's name next to the avatar in the topbar:

```razor
<FaAvatarForm IsAuthenticated="true"
              DisplayName="Jane Doe"
              ShowDisplayName="true"
              AccountHref="/account"
              OnLogout="HandleLogout" />
```

### With custom application items

Provide custom items or info via `ChildContent`:

```razor
<FaAvatarForm IsAuthenticated="true"
              DisplayName="Jane Doe"
              Email="jane@example.com"
              AccountHref="/account"
              OnLogout="HandleLogout">
    <a class="fa-avatar-form-item" href="/billing">Billing & Plans</a>
    <a class="fa-avatar-form-item" href="/team">Team Settings</a>
</FaAvatarForm>
```

### Unauthenticated state

```razor
<FaAvatarForm IsAuthenticated="false"
              OnLogin="NavigateToLogin" />
```

### In `FaHeader` / Shells

`FaHeader`, `FaStandardShell`, and `FaSidebarShell` support `UseAvatarForm`:

```razor
<FaSidebarShell BrandText="My App"
                IsAuthenticated="true"
                UserDisplayName="Jane Doe"
                UserImageUrl="https://example.com/avatar.jpg"
                UserEmail="jane@example.com"
                UseAvatarForm="true"
                ShowUserNameInHeader="false"
                AccountHref="/account"
                OnLogout="HandleLogout">
    ...
</FaSidebarShell>
```

## Parameters

| Parameter | Type | Default | Notes |
|---|---|---|---|
| `IsAuthenticated` | `bool` | `false` | When true, renders user identity, account action, and Logout button; when false, renders Guest and Login button |
| `DisplayName` | `string?` | `null` | User display name |
| `ImageUrl` | `string?` | `null` | Photo URL for avatar |
| `Email` | `string?` | `null` | Secondary text (email, role) displayed in the identity panel |
| `Subtitle` | `string?` | `null` | Alternative secondary text if `Email` is not used |
| `ShowDisplayName` | `bool` | `false` | When false, topbar only shows the avatar; when true, shows name alongside avatar |
| `ShowCaret` | `bool?` | `null` | Dropdown caret visibility; defaults to matching `ShowDisplayName` |
| `ShowThemeSwitcher` | `bool` | `true` | Whether to display Light / Dark / Colorblind switcher in the menu |
| `ThemeSectionTitle` | `string` | `"Theme"` | Title text above the theme switcher |
| `ShowAccountLink` | `bool` | `true` | Whether to show the account action when authenticated |
| `AccountHref` | `string?` | `null` | URL navigating to the account form |
| `OnAccountClick` | `EventCallback` | | Callback invoked when the account action is clicked |
| `AccountText` | `string` | `"Account settings"` | Label for the account action |
| `OnLogin` | `EventCallback` | | Callback invoked on Login click |
| `OnLogout` | `EventCallback` | | Callback invoked on Logout click |
| `LoginText` | `string` | `"Log in"` | Label for Login button |
| `LogoutText` | `string` | `"Log out"` | Label for Logout button |
| `ChildContent` | `RenderFragment?` | `null` | Application-specific items rendered inside the opened panel |
| `MenuAlign` | `FaAlign` | `FaAlign.End` | `End` (right-aligned) or `Start` (left-aligned) |
| `CssClass` | `string?` | `null` | Optional CSS class on the container |
| `TriggerCssClass` | `string?` | `null` | Optional CSS class on the trigger button |

[← Back to index](index.md)
