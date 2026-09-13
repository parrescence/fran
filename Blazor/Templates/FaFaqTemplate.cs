using Fran.Components;
using Fran.Layout;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Fran.Templates;

/// <summary>
/// An FAQ page template: <see cref="FaStandardShell"/> wrapped around an FAQ header,
/// an optional search filter bar, optional category filter pills, the expandable Q&amp;A accordion list,
/// and a bottom contact-support prompt card.
/// </summary>
public sealed class FaFaqTemplate : ComponentBase
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

    /// <summary>Page title, e.g. "Frequently asked questions".</summary>
    [Parameter] public string Title { get; set; } = "Frequently asked questions";

    /// <summary>Subheading description below the title.</summary>
    [Parameter] public string? Description { get; set; } = "Find answers to commonly asked questions about our products, billing, and services.";

    /// <summary>Optional search bar or filter input placed under the header.</summary>
    [Parameter] public RenderFragment? SearchContent { get; set; }

    /// <summary>Optional category navigation or filter chips (e.g. All, Billing, Account, Security).</summary>
    [Parameter] public RenderFragment? Categories { get; set; }

    /// <summary>The expandable Q&amp;A items — typically <see cref="FaAccordion"/> or custom question blocks.</summary>
    [Parameter] public RenderFragment? ChildContent { get; set; }

    /// <summary>Heading for the bottom support card. Defaults to "Still have questions?".</summary>
    [Parameter] public string SupportTitle { get; set; } = "Still have questions?";

    /// <summary>Description inside the support card.</summary>
    [Parameter] public string? SupportDescription { get; set; } = "Can't find the answer you're looking for? Our support team is here to help.";

    /// <summary>Support action button or link inside the support prompt card (e.g. "Contact support").</summary>
    [Parameter] public RenderFragment? SupportAction { get; set; }

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
        builder.AddAttribute(seq++, "class", "fa-template-faq");

        // Header
        builder.OpenElement(seq++, "div");
        builder.AddAttribute(seq++, "class", "fa-template-faq-header");

        builder.OpenElement(seq++, "h1");
        builder.AddAttribute(seq++, "class", "fa-template-faq-title");
        builder.AddContent(seq++, Title);
        builder.CloseElement();

        if (!string.IsNullOrEmpty(Description))
        {
            builder.OpenElement(seq++, "p");
            builder.AddAttribute(seq++, "class", "fa-template-faq-description");
            builder.AddContent(seq++, Description);
            builder.CloseElement();
        }

        if (SearchContent is not null)
        {
            builder.OpenElement(seq++, "div");
            builder.AddAttribute(seq++, "class", "fa-template-faq-search");
            builder.AddContent(seq++, SearchContent);
            builder.CloseElement();
        }

        if (Categories is not null)
        {
            builder.OpenElement(seq++, "div");
            builder.AddAttribute(seq++, "class", "fa-template-faq-categories");
            builder.AddContent(seq++, Categories);
            builder.CloseElement();
        }

        builder.CloseElement(); // .fa-template-faq-header

        // Q&A Accordion Items
        if (ChildContent is not null)
        {
            builder.OpenElement(seq++, "div");
            builder.AddAttribute(seq++, "class", "fa-template-faq-items");
            builder.AddContent(seq++, ChildContent);
            builder.CloseElement();
        }

        // Support Prompt Card
        if (SupportAction is not null || !string.IsNullOrEmpty(SupportTitle))
        {
            builder.OpenElement(seq++, "div");
            builder.AddAttribute(seq++, "class", "fa-template-faq-support");

            builder.OpenComponent<FaCard>(seq++);
            builder.AddComponentParameter(seq++, nameof(FaCard.ChildContent), (RenderFragment)(b =>
            {
                var cardSeq = 0;
                b.OpenElement(cardSeq++, "div");
                b.AddAttribute(cardSeq++, "class", "fa-template-faq-support-content");

                b.OpenElement(cardSeq++, "h3");
                b.AddAttribute(cardSeq++, "class", "fa-template-faq-support-title");
                b.AddContent(cardSeq++, SupportTitle);
                b.CloseElement();

                if (!string.IsNullOrEmpty(SupportDescription))
                {
                    b.OpenElement(cardSeq++, "p");
                    b.AddAttribute(cardSeq++, "class", "fa-template-faq-support-description");
                    b.AddContent(cardSeq++, SupportDescription);
                    b.CloseElement();
                }

                if (SupportAction is not null)
                {
                    b.OpenElement(cardSeq++, "div");
                    b.AddAttribute(cardSeq++, "class", "fa-template-faq-support-action");
                    b.AddContent(cardSeq++, SupportAction);
                    b.CloseElement();
                }

                b.CloseElement(); // .fa-template-faq-support-content
            }));
            builder.CloseComponent(); // FaCard

            builder.CloseElement(); // .fa-template-faq-support
        }

        builder.CloseElement(); // .fa-template-faq
    }
}
