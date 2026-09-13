using Fran.Layout;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Fran.Templates;

/// <summary>
/// A coming-soon / under-maintenance page template. By default, renders a focused, chrome-free
/// splash screen with brand logo, launch status badge, headline, countdown timer slot,
/// email notification subscription form, and social links.
/// Can also wrap inside <see cref="FaStandardShell"/> when <see cref="Standalone"/> is set to false.
/// </summary>
public sealed class FaComingSoonTemplate : ComponentBase
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

    /// <summary>
    /// When true (default), renders a focused chrome-free full-viewport card.
    /// When false, wraps inside <see cref="FaStandardShell"/>.
    /// </summary>
    [Parameter] public bool Standalone { get; set; } = true;

    /// <summary>Optional status badge above the title (e.g. &lt;FaBadge Variant="Primary"&gt;Coming Soon&lt;/FaBadge&gt;).</summary>
    [Parameter] public RenderFragment? StatusBadge { get; set; }

    /// <summary>Headline, e.g. "We're launching something new".</summary>
    [Parameter] public string Title { get; set; } = "Coming soon";

    /// <summary>Subheading description or maintenance notice.</summary>
    [Parameter] public string Description { get; set; } = "We're working hard behind the scenes to bring you an exceptional experience. Sign up to get notified when we launch.";

    /// <summary>Optional countdown timer display slot.</summary>
    [Parameter] public RenderFragment? CountdownContent { get; set; }

    /// <summary>Email subscription / notify-me form slot.</summary>
    [Parameter] public RenderFragment? NotifyContent { get; set; }

    /// <summary>Social media links or contact links at the bottom.</summary>
    [Parameter] public RenderFragment? SocialContent { get; set; }

    /// <summary>Additional content rendered inside the splash card.</summary>
    [Parameter] public RenderFragment? ChildContent { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (Standalone)
        {
            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", "fa-coming-soon-template fa-coming-soon-standalone");

            builder.OpenElement(2, "div");
            builder.AddAttribute(3, "class", "fa-coming-soon-standalone-card");
            RenderContent(builder, 4);
            builder.CloseElement(); // fa-coming-soon-standalone-card

            builder.CloseElement(); // fa-coming-soon-standalone
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
                body.AddAttribute(19, "class", "fa-coming-soon-template");
                RenderContent(body, 20);
                body.CloseElement(); // fa-coming-soon-template
            }));

            builder.CloseComponent();
        }
    }

    private void RenderContent(RenderTreeBuilder builder, int sequence)
    {
        // Brand header
        builder.OpenElement(sequence++, "div");
        builder.AddAttribute(sequence++, "class", "fa-coming-soon-brand");
        if (!string.IsNullOrWhiteSpace(BrandIconUrl))
        {
            builder.OpenElement(sequence++, "img");
            builder.AddAttribute(sequence++, "src", BrandIconUrl);
            builder.AddAttribute(sequence++, "alt", BrandText);
            builder.AddAttribute(sequence++, "class", "fa-coming-soon-logo");
            builder.CloseElement(); // img
        }
        builder.OpenElement(sequence++, "span");
        builder.AddAttribute(sequence++, "class", "fa-coming-soon-brand-name");
        builder.AddContent(sequence++, BrandText);
        builder.CloseElement(); // span
        builder.CloseElement(); // fa-coming-soon-brand

        // Status badge
        if (StatusBadge != null)
        {
            builder.OpenElement(sequence++, "div");
            builder.AddAttribute(sequence++, "class", "fa-coming-soon-badge");
            builder.AddContent(sequence++, StatusBadge);
            builder.CloseElement(); // fa-coming-soon-badge
        }

        // Title
        builder.OpenElement(sequence++, "h1");
        builder.AddAttribute(sequence++, "class", "fa-coming-soon-title");
        builder.AddContent(sequence++, Title);
        builder.CloseElement(); // h1

        // Description
        if (!string.IsNullOrWhiteSpace(Description))
        {
            builder.OpenElement(sequence++, "p");
            builder.AddAttribute(sequence++, "class", "fa-coming-soon-description");
            builder.AddContent(sequence++, Description);
            builder.CloseElement(); // p
        }

        // Countdown timer slot
        if (CountdownContent != null)
        {
            builder.OpenElement(sequence++, "div");
            builder.AddAttribute(sequence++, "class", "fa-coming-soon-countdown");
            builder.AddContent(sequence++, CountdownContent);
            builder.CloseElement(); // fa-coming-soon-countdown
        }

        // Notification subscription form slot
        if (NotifyContent != null)
        {
            builder.OpenElement(sequence++, "div");
            builder.AddAttribute(sequence++, "class", "fa-coming-soon-notify");
            builder.AddContent(sequence++, NotifyContent);
            builder.CloseElement(); // fa-coming-soon-notify
        }

        // Child content
        if (ChildContent != null)
        {
            builder.AddContent(sequence++, ChildContent);
        }

        // Social / contact links
        if (SocialContent != null)
        {
            builder.OpenElement(sequence++, "div");
            builder.AddAttribute(sequence++, "class", "fa-coming-soon-social");
            builder.AddContent(sequence++, SocialContent);
            builder.CloseElement(); // fa-coming-soon-social
        }
    }
}
