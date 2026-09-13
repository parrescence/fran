[← Back to index](index.md) · [← Back to page templates](page-templates.md)

# FaFaqTemplate

`FaStandardShell` wrapped around a comprehensive FAQ / help layout: an introductory header, a search filter input, topic category filter pills, the collapsible Q&A items, and a bottom call-to-action support prompt card.

## Usage

```razor
@page "/faq"
@using Fran.Templates

<FaFaqTemplate BrandText="MyApp" BrandHref="/"
               Title="Frequently asked questions"
               Description="Find answers to commonly asked questions about our platform.">
    <SearchContent>
        <FaInput TValue="string" Placeholder="Search questions..." @bind-Value="_searchQuery" />
    </SearchContent>

    <Categories>
        <FaChip Label="All" Selected="true" />
        <FaChip Label="Billing" />
        <FaChip Label="Account" />
        <FaChip Label="Security" />
    </Categories>

    <ChildContent>
        <FaAccordion Items="_items" />
    </ChildContent>

    <SupportAction>
        <FaButton Variant="FaButtonVariant.Primary" Href="/contact">Contact support</FaButton>
    </SupportAction>
</FaFaqTemplate>

@code {
    private string _searchQuery = "";
    private readonly (string Header, RenderFragment Body)[] _items =
    [
        ("How do I invite teammates?", builder => builder.AddContent(0, "Go to Settings > Team and click Invite.")),
        ("Where can I download invoices?", builder => builder.AddContent(0, "Billing invoices are available under Account > Billing history."))
    ];
}
```

## Getting the value

Pure layout — no bound value. Pass your search input into `SearchContent`, category chips into `Categories`, Q&A items into `ChildContent` (typically with `FaAccordion`), and an action button into `SupportAction`.

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
| `Title` | `string` | page heading; defaults to "Frequently asked questions" |
| `Description` | `string?` | subheading text below the title |
| `SearchContent` | `RenderFragment?` | search field or filter input |
| `Categories` | `RenderFragment?` | category pills/chips bar |
| `ChildContent` | `RenderFragment?` | accordion Q&A items or question list |
| `SupportTitle` | `string` | bottom card title; defaults to "Still have questions?" |
| `SupportDescription` | `string?` | bottom card description |
| `SupportAction` | `RenderFragment?` | action button or link inside the bottom support card |

[← Back to index](index.md) · [← Back to page templates](page-templates.md)
