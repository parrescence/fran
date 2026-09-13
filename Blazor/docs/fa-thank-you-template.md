[← Back to index](index.md) · [← Back to page templates](page-templates.md)

# FaThankYouTemplate

A thank-you / confirmation page template: renders a celebratory completion screen post-signup, post-purchase, or post-form submission. Includes a success icon badge, confirmation headline, order/account details summary card, next steps guide, and action buttons. Supports both standard shell layout (`FaStandardShell`) and focused chrome-free standalone mode (`Standalone="true"`).

## Usage

### Inside Standard Shell

```razor
@page "/thank-you"
@using Fran.Templates

<FaThankYouTemplate BrandText="MyApp" BrandHref="/"
                    Title="Thank you for your purchase!"
                    Description="We've sent a receipt and license key to your email address.">
    <SummaryContent>
        <FaCard>
            <h4>Order Summary</h4>
            <p><strong>Plan:</strong> Pro Annual ($390.00)</p>
            <p><strong>Order ID:</strong> #ORD-84920</p>
        </FaCard>
    </SummaryContent>

    <NextStepsContent>
        <ol>
            <li>Check your email for your activation link</li>
            <li>Invite your team members to the workspace</li>
            <li>Explore our quickstart documentation</li>
        </ol>
    </NextStepsContent>

    <Actions>
        <FaButton Variant="FaButtonVariant.Primary" Href="/dashboard">Go to Dashboard</FaButton>
        <FaButton Variant="FaButtonVariant.Secondary" Href="/docs">Documentation</FaButton>
    </Actions>
</FaThankYouTemplate>
```

### Standalone (Chrome-free)

```razor
@page "/confirmed"
@using Fran.Templates

<FaThankYouTemplate BrandText="MyApp"
                    Standalone="true"
                    Title="You're all set!"
                    Description="Your account has been verified successfully.">
    <Actions>
        <FaButton Variant="FaButtonVariant.Primary" Href="/login">Log in to your account</FaButton>
    </Actions>
</FaThankYouTemplate>
```

## Getting the value

Pure layout — no bound value. Pass receipts or details into `SummaryContent`, checklist into `NextStepsContent`, and buttons into `Actions`.

## Parameters

| Parameter | Type | Notes |
|---|---|---|
| `BrandText` | `string` | **required** |
| `BrandHref` | `string` | defaults to "/" |
| `BrandIconUrl` | `string?` | optional logo shown left of `BrandText` |
| `Standalone` | `bool` | `true` renders chrome-free; `false` (default) wraps in `FaStandardShell` |
| `IconContent` | `RenderFragment?` | custom icon/badge; defaults to green checkmark badge |
| `Title` | `string` | headline; defaults to "Thank you!" |
| `Description` | `string?` | confirmation description or message |
| `SummaryContent` | `RenderFragment?` | receipt, invoice summary, or account info card |
| `NextStepsContent` | `RenderFragment?` | next steps checklist or guide |
| `Actions` | `RenderFragment?` | action buttons |
| `ChildContent` | `RenderFragment?` | additional content |
| `IsAuthenticated` / `UserDisplayName` | `string?` | used when `Standalone="false"` |
| `HeaderPosition` / `FooterPosition` | `FaNavPosition` | used when `Standalone="false"` |

[← Back to index](index.md) · [← Back to page templates](page-templates.md)
