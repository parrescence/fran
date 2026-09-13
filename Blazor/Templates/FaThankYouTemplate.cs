using Fran.Components;
using Fran.Icons;
using Fran.Layout;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Fran.Templates;

/// <summary>
/// A thank-you / confirmation page template: renders a celebratory completion screen post-signup,
/// post-purchase, or post-form submission. Includes a success icon badge, confirmation headline,
/// order/account details summary card, next steps guide, and action buttons.
/// Supports both standard shell layout (<see cref="FaStandardShell"/>) and focused chrome-free standalone mode.
/// </summary>
public sealed class FaThankYouTemplate : ComponentBase
{
    [Parameter, EditorRequired] public string BrandText { get; set; } = "";
    [Parameter] public string BrandHref { get; set; } = "/";
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

    /// <summary>When true, renders a centered chrome-free full-viewport card. Defaults to false.</summary>
    [Parameter] public bool Standalone { get; set; }

    /// <summary>Success badge / icon rendered above the title.</summary>
    [Parameter] public RenderFragment? IconContent { get; set; }

    /// <summary>Confirmation headline, e.g. "Thank you for your order!" or "You're all signed up!".</summary>
    [Parameter] public string Title { get; set; } = "Thank you!";

    /// <summary>Confirmation subtitle or reference ID, e.g. "We sent a confirmation receipt to your email."</summary>
    [Parameter] public string? Description { get; set; } = "We've received your submission and are processing it now.";

    /// <summary>Order receipt, account details, or transaction summary card.</summary>
    [Parameter] public RenderFragment? SummaryContent { get; set; }

    /// <summary>Next steps guide or checklist (e.g. "1. Verify email, 2. Invite team").</summary>
    [Parameter] public RenderFragment? NextStepsContent { get; set; }

    /// <summary>Primary and secondary action buttons (e.g. "Go to Dashboard", "View Receipt").</summary>
    [Parameter] public RenderFragment? Actions { get; set; }

    /// <summary>Additional content rendered at the bottom of the card.</summary>
    [Parameter] public RenderFragment? ChildContent { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (Standalone)
        {
            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", "fa-thank-you-template fa-thank-you-standalone");

            builder.OpenElement(2, "div");
            builder.AddAttribute(3, "class", "fa-thank-you-standalone-card");
            RenderContent(builder, 4);
            builder.CloseElement(); // fa-thank-you-standalone-card

            builder.CloseElement(); // fa-thank-you-standalone
        }
        else
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

            builder.AddComponentParameter(17, nameof(FaStandardShell.ChildContent), (RenderFragment)(body =>
            {
                body.OpenElement(18, "div");
                body.AddAttribute(19, "class", "fa-thank-you-template");
                RenderContent(body, 20);
                body.CloseElement(); // fa-thank-you-template
            }));

            builder.CloseComponent();
        }
    }

    private void RenderContent(RenderTreeBuilder builder, int sequence)
    {
        builder.OpenElement(sequence++, "div");
        builder.AddAttribute(sequence++, "class", "fa-thank-you-hero");

        // Icon
        builder.OpenElement(sequence++, "div");
        builder.AddAttribute(sequence++, "class", "fa-thank-you-icon-wrap");
        if (IconContent != null)
        {
            builder.AddContent(sequence++, IconContent);
        }
        else
        {
            builder.OpenComponent<FaBadge>(sequence++);
            builder.AddComponentParameter(sequence++, nameof(FaBadge.Variant), FaBadgeVariant.Success);
            builder.AddComponentParameter(sequence++, nameof(FaBadge.ChildContent), (RenderFragment)(b =>
            {
                b.OpenComponent<FaIcon>(0);
                b.AddComponentParameter(1, nameof(FaIcon.Name), FaIconName.Success);
                b.AddComponentParameter(2, nameof(FaIcon.Size), 20);
                b.CloseComponent();
            }));
            builder.CloseComponent(); // FaBadge
        }
        builder.CloseElement(); // fa-thank-you-icon-wrap

        // Title
        builder.OpenElement(sequence++, "h1");
        builder.AddAttribute(sequence++, "class", "fa-thank-you-title");
        builder.AddContent(sequence++, Title);
        builder.CloseElement(); // h1

        // Description
        if (!string.IsNullOrWhiteSpace(Description))
        {
            builder.OpenElement(sequence++, "p");
            builder.AddAttribute(sequence++, "class", "fa-thank-you-description");
            builder.AddContent(sequence++, Description);
            builder.CloseElement(); // p
        }
        builder.CloseElement(); // fa-thank-you-hero

        // Summary card
        if (SummaryContent != null)
        {
            builder.OpenElement(sequence++, "div");
            builder.AddAttribute(sequence++, "class", "fa-thank-you-summary");
            builder.AddContent(sequence++, SummaryContent);
            builder.CloseElement(); // fa-thank-you-summary
        }

        // Next steps
        if (NextStepsContent != null)
        {
            builder.OpenElement(sequence++, "div");
            builder.AddAttribute(sequence++, "class", "fa-thank-you-steps");
            builder.AddContent(sequence++, NextStepsContent);
            builder.CloseElement(); // fa-thank-you-steps
        }

        // Actions
        if (Actions != null)
        {
            builder.OpenElement(sequence++, "div");
            builder.AddAttribute(sequence++, "class", "fa-thank-you-actions");
            builder.AddContent(sequence++, Actions);
            builder.CloseElement(); // fa-thank-you-actions
        }

        // Child content
        if (ChildContent != null)
        {
            builder.AddContent(sequence++, ChildContent);
        }
    }
}
