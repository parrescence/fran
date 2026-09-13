using Fran.Layout;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Fran.Templates;

/// <summary>
/// A pricing page template: <see cref="FaStandardShell"/> wrapped around a pricing hero header,
/// an optional billing cycle toggle (Monthly / Annual), a pricing plans cards section, an optional
/// comparison table slot, and an FAQ teaser section.
/// </summary>
public sealed class FaPricingTemplate : ComponentBase
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

    /// <summary>Pricing page title, e.g. "Simple, transparent pricing".</summary>
    [Parameter] public string Title { get; set; } = "Simple, transparent pricing";

    /// <summary>Subheading description below the title.</summary>
    [Parameter] public string? Description { get; set; } = "Choose the plan that fits your business. Upgrade, downgrade, or cancel anytime.";

    /// <summary>Optional billing period toggle (e.g. Monthly / Annual with discount badge).</summary>
    [Parameter] public RenderFragment? BillingToggle { get; set; }

    /// <summary>Pricing cards / tiers section.</summary>
    [Parameter] public RenderFragment? Plans { get; set; }

    /// <summary>Optional feature comparison table section.</summary>
    [Parameter] public RenderFragment? ComparisonContent { get; set; }

    /// <summary>Heading for the FAQ teaser section. Defaults to "Frequently asked questions".</summary>
    [Parameter] public string FaqTitle { get; set; } = "Frequently asked questions";

    /// <summary>FAQ accordion items or teaser content.</summary>
    [Parameter] public RenderFragment? FaqContent { get; set; }

    /// <summary>Additional content rendered at the bottom of the pricing page.</summary>
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
        builder.AddAttribute(seq++, "class", "fa-template-pricing");

        // Header & Intro
        builder.OpenElement(seq++, "div");
        builder.AddAttribute(seq++, "class", "fa-template-pricing-header");

        builder.OpenElement(seq++, "h1");
        builder.AddAttribute(seq++, "class", "fa-template-pricing-title");
        builder.AddContent(seq++, Title);
        builder.CloseElement();

        if (!string.IsNullOrEmpty(Description))
        {
            builder.OpenElement(seq++, "p");
            builder.AddAttribute(seq++, "class", "fa-template-pricing-description");
            builder.AddContent(seq++, Description);
            builder.CloseElement();
        }

        if (BillingToggle is not null)
        {
            builder.OpenElement(seq++, "div");
            builder.AddAttribute(seq++, "class", "fa-template-pricing-toggle");
            builder.AddContent(seq++, BillingToggle);
            builder.CloseElement();
        }

        builder.CloseElement(); // .fa-template-pricing-header

        // Plans section
        if (Plans is not null)
        {
            builder.OpenElement(seq++, "div");
            builder.AddAttribute(seq++, "class", "fa-template-pricing-plans");
            builder.AddContent(seq++, Plans);
            builder.CloseElement();
        }

        // Comparison table
        if (ComparisonContent is not null)
        {
            builder.OpenElement(seq++, "div");
            builder.AddAttribute(seq++, "class", "fa-template-pricing-comparison");
            builder.AddContent(seq++, ComparisonContent);
            builder.CloseElement();
        }

        // FAQ Teaser
        if (FaqContent is not null)
        {
            builder.OpenElement(seq++, "div");
            builder.AddAttribute(seq++, "class", "fa-template-pricing-faq");

            if (!string.IsNullOrEmpty(FaqTitle))
            {
                builder.OpenElement(seq++, "h2");
                builder.AddAttribute(seq++, "class", "fa-template-pricing-faq-title");
                builder.AddContent(seq++, FaqTitle);
                builder.CloseElement();
            }

            builder.AddContent(seq++, FaqContent);
            builder.CloseElement();
        }

        if (ChildContent is not null)
        {
            builder.AddContent(seq++, ChildContent);
        }

        builder.CloseElement(); // .fa-template-pricing
    }
}
