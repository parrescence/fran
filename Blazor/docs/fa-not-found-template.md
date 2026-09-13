[← Back to index](index.md) · [← Back to page templates](page-templates.md)

# FaNotFoundTemplate

A 404 / error page template. By default, renders a clean, focused, chrome-free error screen (similar to `FaAuthTemplate`) with a status code badge, clear error title, descriptive explanation, and return-home action. Can also run inside `FaStandardShell` when `Standalone="false"`.

## Usage

### Standalone (chrome-free)

```razor
@page "/404"
@using Fran.Templates

<FaNotFoundTemplate BrandText="MyApp"
                    BrandHref="/"
                    StatusCode="404"
                    Title="Page not found"
                    Description="Sorry, we couldn't find the page you're looking for."
                    HomeHref="/"
                    HomeText="Return to dashboard" />
```

### Inside standard layout shell

```razor
@page "/error"
@using Fran.Templates

<FaNotFoundTemplate BrandText="MyApp"
                    BrandHref="/"
                    Standalone="false"
                    StatusCode="500"
                    Title="Internal server error"
                    Description="Something went wrong on our end. Please try again shortly.">
    <Actions>
        <FaButton Variant="FaButtonVariant.Primary" Href="/">Home</FaButton>
        <FaButton Variant="FaButtonVariant.Secondary" Href="/contact">Contact support</FaButton>
    </Actions>
</FaNotFoundTemplate>
```

## Getting the value

Pure layout — no bound value. Customize status code with `StatusCode` ("404", "500", "403", etc.), override default button with `HomeHref`/`HomeText`, or provide custom buttons via `Actions`.

## Parameters

| Parameter | Type | Notes |
|---|---|---|
| `BrandText` | `string` | **required** |
| `BrandHref` | `string` | defaults to "/" |
| `BrandIconUrl` | `string?` | optional logo shown left of `BrandText` |
| `Standalone` | `bool` | `true` (default) renders chrome-free; `false` wraps in `FaStandardShell` |
| `StatusCode` | `string` | HTTP or error status code; defaults to "404" |
| `Title` | `string` | error heading; defaults to "Page not found" |
| `Description` | `string` | explanatory text |
| `HomeHref` | `string` | destination for default return button; defaults to "/" |
| `HomeText` | `string` | label for default return button; defaults to "Back to home" |
| `Actions` | `RenderFragment?` | custom action buttons (overrides or supplements default button) |
| `ChildContent` | `RenderFragment?` | optional additional content inside the error card |
| `IsAuthenticated` / `UserDisplayName` / `UserImageUrl` / `UserEmail` | `string?` | used when `Standalone="false"` |
| `UseAvatarForm` / `ShowUserNameInHeader` / `AccountHref` | | used when `Standalone="false"` |
| `OnLogin` / `OnLogout` / `OnAccountClick` | `EventCallback` | used when `Standalone="false"` |
| `HeaderPosition` / `FooterPosition` | `FaNavPosition` | used when `Standalone="false"` |

[← Back to index](index.md) · [← Back to page templates](page-templates.md)
