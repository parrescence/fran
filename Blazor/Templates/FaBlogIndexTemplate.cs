using Fran.Layout;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Fran.Templates;

/// <summary>
/// A blog index / list view template: <see cref="FaStandardShell"/> wrapped around
/// a header with title and newsletter subscribe action, an optional featured post hero,
/// category/tag filter pills, a responsive grid of blog post cards, and pagination.
/// </summary>
public sealed class FaBlogIndexTemplate : ComponentBase
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

    /// <summary>Blog heading, e.g. "Blog" or "Engineering Log".</summary>
    [Parameter] public string Title { get; set; } = "Blog";

    /// <summary>Subheading or editorial description.</summary>
    [Parameter] public string? Description { get; set; } = "Stories, technical insights, and company updates.";

    /// <summary>Action in the header, e.g. newsletter subscribe button or RSS feed link.</summary>
    [Parameter] public RenderFragment? HeaderAction { get; set; }

    /// <summary>Hero banner or large card featuring the highlighted post.</summary>
    [Parameter] public RenderFragment? FeaturedPost { get; set; }

    /// <summary>Category or tag filter chips bar.</summary>
    [Parameter] public RenderFragment? CategoryBar { get; set; }

    /// <summary>Grid of blog post cards.</summary>
    [Parameter] public RenderFragment? ChildContent { get; set; }

    /// <summary>Pagination controls at the bottom.</summary>
    [Parameter] public RenderFragment? PaginationContent { get; set; }

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
            body.AddAttribute(19, "class", "fa-blog-index-template");

            // Header
            body.OpenElement(20, "header");
            body.AddAttribute(21, "class", "fa-blog-index-header");

            body.OpenElement(22, "div");
            body.AddAttribute(23, "class", "fa-blog-index-title-row");
            body.OpenElement(24, "div");
            body.AddAttribute(25, "class", "fa-blog-index-title-group");
            body.OpenElement(26, "h1");
            body.AddAttribute(27, "class", "fa-blog-index-title");
            body.AddContent(28, Title);
            body.CloseElement(); // h1

            if (!string.IsNullOrWhiteSpace(Description))
            {
                body.OpenElement(29, "p");
                body.AddAttribute(30, "class", "fa-blog-index-desc");
                body.AddContent(31, Description);
                body.CloseElement(); // p
            }
            body.CloseElement(); // fa-blog-index-title-group

            if (HeaderAction != null)
            {
                body.OpenElement(32, "div");
                body.AddAttribute(33, "class", "fa-blog-index-header-action");
                body.AddContent(34, HeaderAction);
                body.CloseElement(); // fa-blog-index-header-action
            }
            body.CloseElement(); // fa-blog-index-title-row

            if (CategoryBar != null)
            {
                body.OpenElement(35, "nav");
                body.AddAttribute(36, "class", "fa-blog-index-categories");
                body.AddAttribute(37, "aria-label", "Blog categories");
                body.AddContent(38, CategoryBar);
                body.CloseElement(); // nav
            }
            body.CloseElement(); // header

            // Featured Post
            if (FeaturedPost != null)
            {
                body.OpenElement(39, "section");
                body.AddAttribute(40, "class", "fa-blog-index-featured");
                body.AddContent(41, FeaturedPost);
                body.CloseElement(); // section
            }

            // Post Grid
            if (ChildContent != null)
            {
                body.OpenElement(42, "section");
                body.AddAttribute(43, "class", "fa-blog-index-grid");
                body.AddContent(44, ChildContent);
                body.CloseElement(); // section
            }

            // Pagination
            if (PaginationContent != null)
            {
                body.OpenElement(45, "nav");
                body.AddAttribute(46, "class", "fa-blog-index-pagination");
                body.AddAttribute(47, "aria-label", "Blog pagination");
                body.AddContent(48, PaginationContent);
                body.CloseElement(); // nav
            }

            body.CloseElement(); // fa-blog-index-template
        }));

        builder.CloseComponent();
    }
}
