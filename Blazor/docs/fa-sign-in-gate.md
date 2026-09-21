[← Back to index](index.md)

# FaSignInGate

The standard "you need to sign in to view this page" gate — shown when an
unauthenticated visitor hits a protected route. Not the credential-entry page
itself (that's typically a hosted identity provider's own login page, outside
this library's reach) — just the one screen every app controls in between, so
every Parrescence app's gate shares the same look instead of each app
hand-rolling its own card. Renders as an absolutely-positioned card anchored
via `Position` rather than a full-viewport block, so it overlays the page's
content area instead of shoving it out of the way — scoped to that content
area (e.g. FaStandardShell/FaSidebarShell's `<main>`), not the viewport, so it
never covers a header or sidebar outside it. FaSignInGate never performs the
actual sign-in redirect itself — `OnSignIn` is where the caller navigates to
its own login route.

## Usage

```razor
@page "/"
@using Fran.Components
@inject NavigationManager Navigation

<AuthorizeView>
    <NotAuthorized>
        <FaSignInGate BrandText="Vince" OnSignIn="SignIn" />
    </NotAuthorized>
</AuthorizeView>

@code {
    private void SignIn() =>
        Navigation.NavigateTo($"authentication/login?returnUrl={Uri.EscapeDataString(Navigation.Uri)}");
}
```

## Getting the value

No bound value — `OnSignIn` is the signal to navigate to your app's own login
route (an MSAL `authentication/login` redirect, a hosted CIAM login page,
whatever the app actually uses).

## Parameters

| Parameter | Type | Notes |
|---|---|---|
| `BrandText` | `string` | **required** |
| `BrandHref` | `string` | defaults to `"/"` |
| `BrandIconUrl` | `string?` | optional logo shown left of `BrandText` |
| `OnSignIn` | `EventCallback` | required |
| `Title` | `string` | defaults to `"Sign in required"` |
| `Description` | `string` | defaults to `"You need to sign in to view this page."` |
| `SignInText` | `string` | defaults to `"Sign In"` |
| `Position` | `FaSignInGatePosition` | which of the 9 anchors within the page's content area the card sits at (not the viewport) — `TopLeft`/`TopCenter`/`TopRight`/`CenterLeft`/`Center`/`CenterRight`/`BottomLeft`/`BottomCenter`/`BottomRight`. Defaults to `TopCenter` |
| `FooterContent` | `RenderFragment?` | small content below the card |
| `CssClass` | `string?` | |

[← Back to index](index.md)
