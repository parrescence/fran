[← Back to index](index.md) · [← Back to page templates](page-templates.md)

# FaKnowledgeBaseTemplate

`FaStandardShell` wrapped around a knowledge base / help center layout: an eye-catching search hero banner, a responsive category cards grid, a list/grid of frequently read articles, and a contact support footer banner.

## Usage

```razor
@page "/help"
@using Fran.Templates
@using Fran.Components

<FaKnowledgeBaseTemplate BrandText="MyApp" BrandHref="/"
                         Title="How can we help you?"
                         Description="Search our knowledge base or browse help topics below.">
    <SearchContent>
        <FaInput Placeholder="Search articles, guides, and tutorials..." />
    </SearchContent>

    <CategoryContent>
        <FaCard>
            <h4>Getting Started</h4>
            <p class="fa-text-muted">Installation, setup guides, and quick starts.</p>
        </FaCard>
        <FaCard>
            <h4>Billing &amp; Plans</h4>
            <p class="fa-text-muted">Invoices, payment methods, and subscription tiers.</p>
        </FaCard>
        <FaCard>
            <h4>Security &amp; IAM</h4>
            <p class="fa-text-muted">API tokens, SSO, SAML, and audit logs.</p>
        </FaCard>
    </CategoryContent>

    <PopularArticlesContent>
        <FaCard>
            <a href="/help/articles/reset-password">How to reset your account password</a>
        </FaCard>
        <FaCard>
            <a href="/help/articles/api-rate-limits">Understanding API rate limits</a>
        </FaCard>
    </PopularArticlesContent>

    <SupportActionContent>
        <h3>Still have questions?</h3>
        <p>Our support team is available 24/7 to help resolve technical issues.</p>
        <FaButton Variant="FaButtonVariant.Primary" Href="/contact">Contact Support</FaButton>
    </SupportActionContent>
</FaKnowledgeBaseTemplate>
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
| `FooterContent` | `RenderFragment?` | overrides default footer |
| `HeaderPosition` / `FooterPosition` | `FaNavPosition` | `Standard` (default) \| `Sticky` \| `Floating` |
| `Title` | `string` | hero title; defaults to "Help Center" |
| `Description` | `string?` | hero description |
| `SearchContent` | `RenderFragment?` | search bar inside the hero |
| `CategoryContent` | `RenderFragment?` | topic / category card grid |
| `PopularArticlesContent` | `RenderFragment?` | trending / popular articles |
| `SupportActionContent` | `RenderFragment?` | contact support call-to-action banner |
| `ChildContent` | `RenderFragment?` | optional additional content |
