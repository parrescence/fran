[← Back to index](index.md) · [← Back to page templates](page-templates.md)

# FaPricingTemplate

`FaStandardShell` wrapped around a marketing pricing layout: a title and subtitle hero header, an optional billing cycle toggle (e.g. Monthly / Annual with discount badges), a pricing tiers/cards grid, an optional full feature-comparison table, and an FAQ teaser section.

## Usage

```razor
@page "/pricing"
@using Fran.Templates

<FaPricingTemplate BrandText="MyApp" BrandHref="/"
                   Title="Simple, transparent pricing"
                   Description="Choose the plan that fits your team. Upgrade or cancel anytime.">
    <BillingToggle>
        <FaToggle TValue="string" @bind-Value="_cycle" Options="_billingOptions" />
    </BillingToggle>

    <Plans>
        <FaCard Class="fa-pricing-card">
            <h3>Starter</h3>
            <div class="price">$19<span>/mo</span></div>
            <p>Everything you need to get started.</p>
            <FaButton Variant="FaButtonVariant.Secondary">Get started</FaButton>
        </FaCard>
        <FaCard Class="fa-pricing-card featured">
            <FaBadge Variant="FaBadgeVariant.Primary">Most popular</FaBadge>
            <h3>Pro</h3>
            <div class="price">$49<span>/mo</span></div>
            <p>For growing teams with higher volume.</p>
            <FaButton Variant="FaButtonVariant.Primary">Start free trial</FaButton>
        </FaCard>
    </Plans>

    <FaqContent>
        <FaAccordion Items="_faqItems" />
    </FaqContent>
</FaPricingTemplate>

@code {
    private string _cycle = "monthly";
    private readonly (string Value, string Label)[] _billingOptions =
    [
        ("monthly", "Monthly"),
        ("annual", "Annual (save 20%)")
    ];

    private readonly (string Header, RenderFragment Body)[] _faqItems =
    [
        ("Can I change plans later?", builder => builder.AddContent(0, "Yes, you can upgrade or downgrade at any time.")),
        ("What payment methods are accepted?", builder => builder.AddContent(0, "We accept all major credit cards and PayPal."))
    ];
}
```

## Getting the value

Pure layout — no bound value. Pass child components or HTML into the named slots (`BillingToggle`, `Plans`, `ComparisonContent`, `FaqContent`, `ChildContent`). Shell events (`OnLogin`, `OnLogout`, `OnAccountClick`) behave the same as on `FaStandardShell`.

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
| `FooterContent` | `RenderFragment?` | overrides default footer |
| `HeaderPosition` / `FooterPosition` | `FaNavPosition` | `Standard` (default) \| `Sticky` \| `Floating` |
| `Title` | `string` | page heading; defaults to "Simple, transparent pricing" |
| `Description` | `string?` | subheading text below the title |
| `BillingToggle` | `RenderFragment?` | toggle controls for Monthly vs Annual billing |
| `Plans` | `RenderFragment?` | grid of pricing plan cards / tiers |
| `ComparisonContent` | `RenderFragment?` | optional full feature-comparison matrix table |
| `FaqTitle` | `string` | FAQ section heading; defaults to "Frequently asked questions" |
| `FaqContent` | `RenderFragment?` | FAQ accordion or Q&A items |
| `ChildContent` | `RenderFragment?` | additional content rendered at the bottom of the page |

[← Back to index](index.md) · [← Back to page templates](page-templates.md)
