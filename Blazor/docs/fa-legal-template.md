[← Back to index](index.md) · [← Back to page templates](page-templates.md)

# FaLegalTemplate

`FaStandardShell` wrapped around a legal or compliance document layout: a clean header with title, effective/last updated date, optional actions (print, PDF download), a sticky table-of-contents navigation rail, and a structured, readable legal text body. Perfect for Terms of Service, Privacy Policy, Acceptable Use, and SLAs.

## Usage

```razor
@page "/terms"
@using Fran.Templates

<FaLegalTemplate BrandText="MyApp" BrandHref="/"
                 Title="Terms of Service"
                 LastUpdated="Effective Date: September 1, 2026"
                 Description="Please read these terms carefully before using our software and services.">
    <HeaderActions>
        <FaButton Variant="FaButtonVariant.Secondary">Download PDF</FaButton>
    </HeaderActions>

    <TableOfContents>
        <a href="#acceptance">1. Acceptance of Terms</a>
        <a href="#accounts">2. Account Registration</a>
        <a href="#billing">3. Fees and Billing</a>
        <a href="#termination">4. Termination</a>
    </TableOfContents>

    <ChildContent>
        <section id="acceptance">
            <h3>1. Acceptance of Terms</h3>
            <p>By accessing or using our services, you agree to be bound by these terms. If you disagree with any part, you may not access our services.</p>
        </section>
        <section id="accounts">
            <h3>2. Account Registration</h3>
            <p>You must provide accurate and complete information when creating an account and keep your credentials secure.</p>
        </section>
    </ChildContent>
</FaLegalTemplate>
```

## Getting the value

Pure layout — no bound value. Pass your document sections into `ChildContent`, anchor links into `TableOfContents`, and action buttons into `HeaderActions`.

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
| `Title` | `string` | document title; defaults to "Terms of Service" |
| `LastUpdated` | `string?` | effective / updated date label |
| `Description` | `string?` | introductory summary |
| `HeaderActions` | `RenderFragment?` | print, PDF, or sharing actions |
| `TableOfContents` | `RenderFragment?` | sticky TOC navigation links |
| `ChildContent` | `RenderFragment?` | legal document sections |

[← Back to index](index.md) · [← Back to page templates](page-templates.md)
