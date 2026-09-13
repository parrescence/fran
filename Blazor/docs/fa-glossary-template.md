[← Back to index](index.md) · [← Back to page templates](page-templates.md)

# FaGlossaryTemplate

`FaStandardShell` wrapped around an alphabetical terminology / glossary directory: page title and description, search bar, sticky A–Z quick jump navigation bar, and organized definition sections.

## Usage

```razor
@page "/glossary"
@using Fran.Templates
@using Fran.Components

<FaGlossaryTemplate BrandText="MyApp" BrandHref="/"
                    Title="Design Systems Glossary"
                    Description="A dictionary of core terminology, accessibility standards, and UI architecture concepts.">
    <SearchContent>
        <FaInput Placeholder="Filter terminology..." />
    </SearchContent>

    <AlphabetNav>
        <a href="#A" class="fa-glossary-letter-link">A</a>
        <a href="#B" class="fa-glossary-letter-link">B</a>
        <a href="#C" class="fa-glossary-letter-link">C</a>
    </AlphabetNav>

    <ChildContent>
        <section id="A" class="fa-mb-4">
            <h2>A</h2>
            <FaCard class="fa-mb-2">
                <h4>Accessibility (a11y)</h4>
                <p>The inclusive practice of ensuring web products are usable by people of all abilities and disabilities.</p>
            </FaCard>
        </section>
        <section id="B" class="fa-mb-4">
            <h2>B</h2>
            <FaCard class="fa-mb-2">
                <h4>Borders &amp; Radii</h4>
                <p>Geometric design tokens that define edge curvature and separation boundaries across elements.</p>
            </FaCard>
        </section>
    </ChildContent>
</FaGlossaryTemplate>
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
| `Title` | `string` | title; defaults to "Glossary" |
| `Description` | `string?` | descriptive intro |
| `SearchContent` | `RenderFragment?` | search / filter input slot |
| `AlphabetNav` | `RenderFragment?` | A–Z letter link bar |
| `ChildContent` | `RenderFragment?` | alphabetical term sections and definitions |
| `EmptyContent` | `RenderFragment?` | state rendered when terms are missing |
