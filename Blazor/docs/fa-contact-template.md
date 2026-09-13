[← Back to index](index.md) · [← Back to page templates](page-templates.md)

# FaContactTemplate

`FaStandardShell` wrapped around a contact page layout: a header with title and subtitle, a two-column responsive split containing contact information cards/details on one side and an inquiry/message form on the other, plus an optional map embed slot below.

## Usage

```razor
@page "/contact"
@using Fran.Templates

<FaContactTemplate BrandText="MyApp" BrandHref="/"
                   Title="Get in touch"
                   Description="Have questions or need support? Reach out and our team will get back to you shortly.">
    <ContactInfo>
        <FaCard>
            <h4>Customer Support</h4>
            <p>Email: support@example.com</p>
            <p>Phone: +1 (555) 123-4567</p>
        </FaCard>
        <FaCard>
            <h4>Headquarters</h4>
            <p>100 Innovation Way, Suite 400<br />San Francisco, CA 94105</p>
        </FaCard>
    </ContactInfo>

    <FormContent>
        <FaCard>
            <FaInput TValue="string" Label="Your name" @bind-Value="_name" />
            <FaInput TValue="string" Label="Your email" @bind-Value="_email" />
            <FaTextarea Label="Message" @bind-Value="_message" Rows="5" />
            <FaButton Variant="FaButtonVariant.Primary" OnClick="SendMessage">Send message</FaButton>
        </FaCard>
    </FormContent>
</FaContactTemplate>

@code {
    private string _name = "";
    private string _email = "";
    private string _message = "";

    private void SendMessage() { /* Submit logic */ }
}
```

## Getting the value

Pure layout — no bound value. Pass your contact cards into `ContactInfo`, your contact form into `FormContent`, and an optional map or office photo into `MapContent`.

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
| `Title` | `string` | page heading; defaults to "Get in touch" |
| `Description` | `string?` | subheading text below the title |
| `ContactInfo` | `RenderFragment?` | contact information cards, addresses, phone numbers |
| `FormContent` | `RenderFragment?` | message or inquiry form |
| `MapContent` | `RenderFragment?` | optional interactive map or office image embed |
| `ChildContent` | `RenderFragment?` | additional content rendered at the bottom of the page |

[← Back to index](index.md) · [← Back to page templates](page-templates.md)
