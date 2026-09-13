using Fran.Layout;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Fran.Templates;

/// <summary>
/// An About page template: <see cref="FaStandardShell"/> wrapped around an about hero/intro,
/// a company mission or story section, key numbers/stats highlights, a team members grid,
/// and a closing call-to-action banner.
/// </summary>
public sealed class FaAboutTemplate : ComponentBase
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

    /// <summary>Hero headline, e.g. "We build tools for craftspeople".</summary>
    [Parameter] public string Title { get; set; } = "About our mission";

    /// <summary>Hero subheading / lead paragraph.</summary>
    [Parameter] public string? Description { get; set; } = "We're a dedicated team building thoughtful software to empower modern businesses.";

    /// <summary>Optional hero visual, photo, or illustration slot.</summary>
    [Parameter] public RenderFragment? HeroMedia { get; set; }

    /// <summary>Heading for the mission/story section. Defaults to "Our Story".</summary>
    [Parameter] public string? MissionTitle { get; set; } = "Our Story";

    /// <summary>Company story, values, or mission paragraphs.</summary>
    [Parameter] public RenderFragment? MissionContent { get; set; }

    /// <summary>Stats/metrics slot (e.g. key figures: "10,000+ customers", "99.9% uptime").</summary>
    [Parameter] public RenderFragment? StatsContent { get; set; }

    /// <summary>Heading for the team section. Defaults to "Meet our team".</summary>
    [Parameter] public string? TeamTitle { get; set; } = "Meet our team";

    /// <summary>Description for the team section.</summary>
    [Parameter] public string? TeamDescription { get; set; }

    /// <summary>Team member cards or portraits.</summary>
    [Parameter] public RenderFragment? TeamContent { get; set; }

    /// <summary>Bottom call-to-action banner or prompt (e.g. "Join our team" or "Get started today").</summary>
    [Parameter] public RenderFragment? CtaContent { get; set; }

    /// <summary>Additional content rendered at the bottom of the page.</summary>
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

        builder.AddComponentParameter(17, nameof(FaStandardShell.ChildContent), (RenderFragment)(body =>
        {
            body.OpenElement(18, "div");
            body.AddAttribute(19, "class", "fa-about-template");

            // Hero section
            body.OpenElement(20, "header");
            body.AddAttribute(21, "class", "fa-about-hero");
            body.OpenElement(22, "h1");
            body.AddAttribute(23, "class", "fa-about-title");
            body.AddContent(24, Title);
            body.CloseElement(); // h1

            if (!string.IsNullOrWhiteSpace(Description))
            {
                body.OpenElement(25, "p");
                body.AddAttribute(26, "class", "fa-about-description");
                body.AddContent(27, Description);
                body.CloseElement(); // p
            }

            if (HeroMedia != null)
            {
                body.OpenElement(28, "div");
                body.AddAttribute(29, "class", "fa-about-media");
                body.AddContent(30, HeroMedia);
                body.CloseElement(); // fa-about-media
            }
            body.CloseElement(); // header

            // Stats section
            if (StatsContent != null)
            {
                body.OpenElement(31, "section");
                body.AddAttribute(32, "class", "fa-about-stats");
                body.AddContent(33, StatsContent);
                body.CloseElement(); // section
            }

            // Mission / Story section
            if (MissionContent != null || !string.IsNullOrWhiteSpace(MissionTitle))
            {
                body.OpenElement(34, "section");
                body.AddAttribute(35, "class", "fa-about-mission");
                if (!string.IsNullOrWhiteSpace(MissionTitle))
                {
                    body.OpenElement(36, "h2");
                    body.AddAttribute(37, "class", "fa-about-section-title");
                    body.AddContent(38, MissionTitle);
                    body.CloseElement(); // h2
                }
                if (MissionContent != null)
                {
                    body.OpenElement(39, "div");
                    body.AddAttribute(40, "class", "fa-about-mission-content");
                    body.AddContent(41, MissionContent);
                    body.CloseElement(); // div
                }
                body.CloseElement(); // section
            }

            // Team section
            if (TeamContent != null)
            {
                body.OpenElement(42, "section");
                body.AddAttribute(43, "class", "fa-about-team");
                if (!string.IsNullOrWhiteSpace(TeamTitle))
                {
                    body.OpenElement(44, "h2");
                    body.AddAttribute(45, "class", "fa-about-section-title");
                    body.AddContent(46, TeamTitle);
                    body.CloseElement(); // h2
                }
                if (!string.IsNullOrWhiteSpace(TeamDescription))
                {
                    body.OpenElement(47, "p");
                    body.AddAttribute(48, "class", "fa-about-section-description");
                    body.AddContent(49, TeamDescription);
                    body.CloseElement(); // p
                }
                body.OpenElement(50, "div");
                body.AddAttribute(51, "class", "fa-about-team-grid");
                body.AddContent(52, TeamContent);
                body.CloseElement(); // fa-about-team-grid
                body.CloseElement(); // section
            }

            // CTA section
            if (CtaContent != null)
            {
                body.OpenElement(53, "section");
                body.AddAttribute(54, "class", "fa-about-cta");
                body.AddContent(55, CtaContent);
                body.CloseElement(); // section
            }

            // Child content
            if (ChildContent != null)
            {
                body.AddContent(56, ChildContent);
            }

            body.CloseElement(); // fa-about-template
        }));

        builder.CloseComponent();
    }
}
