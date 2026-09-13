using Fran.Layout;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Fran.Templates;

/// <summary>
/// A contact page template: <see cref="FaStandardShell"/> wrapped around a contact header,
/// a side-by-side or stacked layout combining contact details/info cards, a contact form,
/// and an optional map or office location embed slot.
/// </summary>
public sealed class FaContactTemplate : ComponentBase
{
    [Parameter, EditorRequired] public string BrandText { get; set; } = "";
    [Parameter] public string BrandHref { get; set; } = "";
    [Parameter] public string? BrandIconUrl { get; set; }

    [Parameter] public bool IsAuthenticated { get; set; }
    [Parameter] public string? UserDisplayName { get; set; }
    [Parameter] public string? UserImageUrl { get; set; }
    [Parameter] public string? UserEmail { get; set; }
    [Parameter] public bool UseAvatarForm { get; set; }
    [Parameter] public bool ShowUserNameInHeader { get; set; }
    [Parameter] public string? AccountHref { get; set; }
    [Parameter] public EventCallback OnAccountClick { get; set; }
    [Parameter] public EventCallback OnLogin { get; set; }
    [Parameter] public EventCallback OnLogout { get; set; }
    [Parameter] public RenderFragment? FooterContent { get; set; }

    [Parameter] public FaNavPosition HeaderPosition { get; set; } = FaNavPosition.Standard;
    [Parameter] public FaNavPosition FooterPosition { get; set; } = FaNavPosition.Standard;

    /// <summary>Contact page title, e.g. "Get in touch".</summary>
    [Parameter] public string Title { get; set; } = "Get in touch";

    /// <summary>Subheading description below the title.</summary>
    [Parameter] public string? Description { get; set; } = "Have questions or need support? Reach out and our team will get back to you shortly.";

    /// <summary>Left column / info content: emails, phone numbers, office address, social links.</summary>
    [Parameter] public RenderFragment? ContactInfo { get; set; }

    /// <summary>The contact inquiry or message form.</summary>
    [Parameter] public RenderFragment? FormContent { get; set; }

    /// <summary>Optional map embed or location card rendered below the main columns.</summary>
    [Parameter] public RenderFragment? MapContent { get; set; }

    /// <summary>Additional content rendered at the bottom of the contact page.</summary>
    [Parameter] public RenderFragment? ChildContent { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenComponent<FaStandardShell>(0);
        builder.AddComponentParameter(1, nameof(FaStandardShell.BrandText), BrandText);
        builder.AddComponentParameter(2, nameof(FaStandardShell.BrandHref), BrandHref);
        builder.AddComponentParameter(3, nameof(FaStandardShell.BrandIconUrl), BrandIconUrl);
        builder.AddComponentParameter(4, nameof(FaStandardShell.IsAuthenticated), IsAuthenticated);
        builder.AddComponentParameter(5, nameof(FaStandardShell.UserDisplayName), UserDisplayName);
        builder.AddComponentParameter(6, nameof(FaStandardShell.UserImageUrl), UserImageUrl);
        builder.AddComponentParameter(7, nameof(FaStandardShell.UserEmail), UserEmail);
        builder.AddComponentParameter(8, nameof(FaStandardShell.UseAvatarForm), UseAvatarForm);
        builder.AddComponentParameter(9, nameof(FaStandardShell.ShowUserNameInHeader), ShowUserNameInHeader);
        builder.AddComponentParameter(10, nameof(FaStandardShell.AccountHref), AccountHref);
        builder.AddComponentParameter(11, nameof(FaStandardShell.OnAccountClick), OnAccountClick);
        builder.AddComponentParameter(12, nameof(FaStandardShell.OnLogin), OnLogin);
        builder.AddComponentParameter(13, nameof(FaStandardShell.OnLogout), OnLogout);
        builder.AddComponentParameter(14, nameof(FaStandardShell.FooterContent), FooterContent);
        builder.AddComponentParameter(15, nameof(FaStandardShell.HeaderPosition), HeaderPosition);
        builder.AddComponentParameter(16, nameof(FaStandardShell.FooterPosition), FooterPosition);
        builder.AddComponentParameter(17, nameof(FaStandardShell.ChildContent), (RenderFragment)RenderBody);
        builder.CloseComponent();
    }

    private void RenderBody(RenderTreeBuilder builder)
    {
        var seq = 0;
        builder.OpenElement(seq++, "div");
        builder.AddAttribute(seq++, "class", "fa-template-contact");

        // Header
        builder.OpenElement(seq++, "div");
        builder.AddAttribute(seq++, "class", "fa-template-contact-header");

        builder.OpenElement(seq++, "h1");
        builder.AddAttribute(seq++, "class", "fa-template-contact-title");
        builder.AddContent(seq++, Title);
        builder.CloseElement();

        if (!string.IsNullOrEmpty(Description))
        {
            builder.OpenElement(seq++, "p");
            builder.AddAttribute(seq++, "class", "fa-template-contact-description");
            builder.AddContent(seq++, Description);
            builder.CloseElement();
        }

        builder.CloseElement(); // .fa-template-contact-header

        // Grid of Info + Form
        builder.OpenElement(seq++, "div");
        builder.AddAttribute(seq++, "class", "fa-template-contact-body");

        if (ContactInfo is not null)
        {
            builder.OpenElement(seq++, "div");
            builder.AddAttribute(seq++, "class", "fa-template-contact-info");
            builder.AddContent(seq++, ContactInfo);
            builder.CloseElement();
        }

        if (FormContent is not null)
        {
            builder.OpenElement(seq++, "div");
            builder.AddAttribute(seq++, "class", "fa-template-contact-form");
            builder.AddContent(seq++, FormContent);
            builder.CloseElement();
        }

        builder.CloseElement(); // .fa-template-contact-body

        // Optional Map
        if (MapContent is not null)
        {
            builder.OpenElement(seq++, "div");
            builder.AddAttribute(seq++, "class", "fa-template-contact-map");
            builder.AddContent(seq++, MapContent);
            builder.CloseElement();
        }

        if (ChildContent is not null)
        {
            builder.AddContent(seq++, ChildContent);
        }

        builder.CloseElement(); // .fa-template-contact
    }
}
