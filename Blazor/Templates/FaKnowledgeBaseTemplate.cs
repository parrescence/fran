using Fran.Layout;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Fran.Templates;

/// <summary>
/// A knowledge base / help center template: <see cref="FaStandardShell"/> wrapped around
/// a search hero banner, category cards grid, popular articles section, and a support contact teaser.
/// </summary>
public sealed class FaKnowledgeBaseTemplate : ComponentBase
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

    /// <summary>Hero banner title, e.g. "How can we help you today?".</summary>
    [Parameter] public string Title { get; set; } = "Help Center";

    /// <summary>Hero banner subtitle or descriptive guidance.</summary>
    [Parameter] public string? Description { get; set; } = "Search our knowledge base or browse help topics below.";

    /// <summary>Search bar slot inside the hero banner.</summary>
    [Parameter] public RenderFragment? SearchContent { get; set; }

    /// <summary>Grid of topic/category cards (e.g., Getting Started, Billing, Security).</summary>
    [Parameter] public RenderFragment? CategoryContent { get; set; }

    /// <summary>List or grid of frequently asked or trending articles.</summary>
    [Parameter] public RenderFragment? PopularArticlesContent { get; set; }

    /// <summary>Contact support or open ticket call-to-action banner at the bottom.</summary>
    [Parameter] public RenderFragment? SupportActionContent { get; set; }

    /// <summary>Optional additional content.</summary>
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
            body.AddAttribute(19, "class", "fa-kb-template");

            // Hero
            body.OpenElement(20, "header");
            body.AddAttribute(21, "class", "fa-kb-hero");
            body.OpenElement(22, "h1");
            body.AddAttribute(23, "class", "fa-kb-title");
            body.AddContent(24, Title);
            body.CloseElement(); // h1

            if (!string.IsNullOrWhiteSpace(Description))
            {
                body.OpenElement(25, "p");
                body.AddAttribute(26, "class", "fa-kb-desc");
                body.AddContent(27, Description);
                body.CloseElement(); // p
            }

            if (SearchContent != null)
            {
                body.OpenElement(28, "div");
                body.AddAttribute(29, "class", "fa-kb-search");
                body.AddContent(30, SearchContent);
                body.CloseElement(); // fa-kb-search
            }
            body.CloseElement(); // header

            // Categories
            if (CategoryContent != null)
            {
                body.OpenElement(31, "section");
                body.AddAttribute(32, "class", "fa-kb-categories");
                body.OpenElement(33, "h2");
                body.AddAttribute(34, "class", "fa-kb-section-title");
                body.AddContent(35, "Browse Topics");
                body.CloseElement(); // h2
                body.OpenElement(36, "div");
                body.AddAttribute(37, "class", "fa-kb-category-grid");
                body.AddContent(38, CategoryContent);
                body.CloseElement(); // fa-kb-category-grid
                body.CloseElement(); // section
            }

            // Popular Articles
            if (PopularArticlesContent != null)
            {
                body.OpenElement(39, "section");
                body.AddAttribute(40, "class", "fa-kb-popular");
                body.OpenElement(41, "h2");
                body.AddAttribute(42, "class", "fa-kb-section-title");
                body.AddContent(43, "Frequently Read");
                body.CloseElement(); // h2
                body.OpenElement(44, "div");
                body.AddAttribute(45, "class", "fa-kb-popular-grid");
                body.AddContent(46, PopularArticlesContent);
                body.CloseElement(); // fa-kb-popular-grid
                body.CloseElement(); // section
            }

            // Optional extra child content
            if (ChildContent != null)
            {
                body.OpenElement(47, "div");
                body.AddAttribute(48, "class", "fa-kb-content");
                body.AddContent(49, ChildContent);
                body.CloseElement(); // fa-kb-content
            }

            // Support CTA
            if (SupportActionContent != null)
            {
                body.OpenElement(50, "aside");
                body.AddAttribute(51, "class", "fa-kb-support");
                body.AddContent(52, SupportActionContent);
                body.CloseElement(); // aside
            }

            body.CloseElement(); // fa-kb-template
        }));

        builder.CloseComponent();
    }
}
