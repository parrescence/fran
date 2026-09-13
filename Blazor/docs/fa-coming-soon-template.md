[← Back to index](index.md) · [← Back to page templates](page-templates.md)

# FaComingSoonTemplate

A coming-soon / under-maintenance page template. By default, renders a focused, chrome-free splash screen with brand logo, launch status badge, headline, countdown timer slot, email notification subscription form, and social links. Can also wrap inside `FaStandardShell` when `Standalone="false"`.

## Usage

```razor
@page "/coming-soon"
@using Fran.Templates

<FaComingSoonTemplate BrandText="NextGen App"
                      BrandHref="/"
                      Title="We're launching something new"
                      Description="Our team is putting the final touches on the next evolution of our analytics platform. Sign up to get early access.">
    <StatusBadge>
        <FaBadge Variant="FaBadgeVariant.Primary">Launching Q4 2026</FaBadge>
    </StatusBadge>

    <CountdownContent>
        <div class="fa-flex fa-justify-center fa-gap-3">
            <FaCard CssClass="fa-text-center fa-p-2" style="min-width: 70px;">
                <h3>14</h3>
                <small class="fa-text-muted">Days</small>
            </FaCard>
            <FaCard CssClass="fa-text-center fa-p-2" style="min-width: 70px;">
                <h3>08</h3>
                <small class="fa-text-muted">Hours</small>
            </FaCard>
            <FaCard CssClass="fa-text-center fa-p-2" style="min-width: 70px;">
                <h3>45</h3>
                <small class="fa-text-muted">Mins</small>
            </FaCard>
        </div>
    </CountdownContent>

    <NotifyContent>
        <div class="fa-flex fa-gap-2">
            <FaInput TValue="string" Placeholder="Enter your email" @bind-Value="_email" />
            <FaButton Variant="FaButtonVariant.Primary" OnClick="Subscribe">Notify me</FaButton>
        </div>
    </NotifyContent>

    <SocialContent>
        <a href="https://github.com" target="_blank" class="fa-text-muted">GitHub</a>
        <span class="fa-text-muted">·</span>
        <a href="https://twitter.com" target="_blank" class="fa-text-muted">Twitter / X</a>
    </SocialContent>
</FaComingSoonTemplate>

@code {
    private string _email = "";
    private void Subscribe() { /* handle subscription */ }
}
```

## Getting the value

Pure layout — no bound value. Pass badge into `StatusBadge`, countdown timer into `CountdownContent`, email capture into `NotifyContent`, and links into `SocialContent`.

## Parameters

| Parameter | Type | Notes |
|---|---|---|
| `BrandText` | `string` | **required** |
| `BrandHref` | `string` | defaults to "/" |
| `BrandIconUrl` | `string?` | optional logo shown left of `BrandText` |
| `Standalone` | `bool` | `true` (default) renders chrome-free; `false` wraps in `FaStandardShell` |
| `StatusBadge` | `RenderFragment?` | launch status badge |
| `Title` | `string` | headline; defaults to "Coming soon" |
| `Description` | `string` | lead description or maintenance notice |
| `CountdownContent` | `RenderFragment?` | countdown timer display slot |
| `NotifyContent` | `RenderFragment?` | email subscription form slot |
| `SocialContent` | `RenderFragment?` | social media or contact links |
| `ChildContent` | `RenderFragment?` | additional elements |
| `IsAuthenticated` / `UserDisplayName` | `string?` | used when `Standalone="false"` |
| `HeaderPosition` / `FooterPosition` | `FaNavPosition` | used when `Standalone="false"` |

[← Back to index](index.md) · [← Back to page templates](page-templates.md)
