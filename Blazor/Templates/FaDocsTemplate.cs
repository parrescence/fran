using Fran.Layout;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Fran.Templates;

/// <summary>
/// A documentation / API reference page template: <see cref="FaSidebarShell"/> wrapped around
/// a docs hierarchy sidebar navigation, a top breadcrumbs/search row, an article header with title,
/// version/method badge, and description, a main documentation body slot, an optional "On this page"
/// table-of-contents right rail, and bottom previous/next page navigation links.
/// </summary>
public sealed class FaDocsTemplate : ComponentBase
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

    /// <summary>Left documentation hierarchy sidebar tree navigation.</summary>
    [Parameter] public RenderFragment? Sidebar { get; set; }
    [Parameter] public RenderFragment? FooterContent { get; set; }

    [Parameter] public FaNavPosition HeaderPosition { get; set; } = FaNavPosition.Standard;
    [Parameter] public FaNavPosition FooterPosition { get; set; } = FaNavPosition.Standard;
    [Parameter] public FaNavPosition SidebarPosition { get; set; } = FaNavPosition.Standard;
    [Parameter] public bool SidebarCollapsible { get; set; } = true;
    [Parameter] public bool ContainScroll { get; set; }

    /// <summary>Breadcrumb trail above the title (e.g. Docs &gt; Components &gt; Button).</summary>
    [Parameter] public RenderFragment? BreadcrumbContent { get; set; }

    /// <summary>Optional search bar or shortcut input above the content.</summary>
    [Parameter] public RenderFragment? SearchContent { get; set; }

    /// <summary>Page or article title, e.g. "Authentication" or "GET /api/v1/users".</summary>
    [Parameter] public string Title { get; set; } = "Documentation";

    /// <summary>Optional badge next to title (e.g. version badge or HTTP method pill).</summary>
    [Parameter] public RenderFragment? Badge { get; set; }

    /// <summary>Subheading description or summary below the title.</summary>
    [Parameter] public string? Description { get; set; }

    /// <summary>Right-side "On this page" table of contents navigation.</summary>
    [Parameter] public RenderFragment? TableOfContents { get; set; }

    /// <summary>Previous / Next article navigation links rendered at the bottom of the article.</summary>
    [Parameter] public RenderFragment? PrevNextNav { get; set; }

    /// <summary>Main documentation article content (paragraphs, code blocks, tables).</summary>
    [Parameter] public RenderFragment? ChildContent { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenComponent<FaSidebarShell>(0);
        builder.AddComponentParameter(1, nameof(FaSidebarShell.BrandText), BrandText);
        builder.AddComponentParameter(2, nameof(FaSidebarShell.BrandHref), BrandHref);
        builder.AddComponentParameter(3, nameof(FaSidebarShell.BrandIconUrl), BrandIconUrl);
        builder.AddComponentParameter(4, nameof(FaSidebarShell.IsAuthenticated), IsAuthenticated);
        builder.AddComponentParameter(5, nameof(FaSidebarShell.UserDisplayName), UserDisplayName);
        builder.AddComponentParameter(6, nameof(FaSidebarShell.UserImageUrl), UserImageUrl);
        builder.AddComponentParameter(7, nameof(FaSidebarShell.UserEmail), UserEmail);
        builder.AddComponentParameter(8, nameof(FaSidebarShell.UseAvatarForm), UseAvatarForm);
        builder.AddComponentParameter(9, nameof(FaSidebarShell.ShowUserNameInHeader), ShowUserNameInHeader);
        builder.AddComponentParameter(10, nameof(FaSidebarShell.AccountHref), AccountHref);
        builder.AddComponentParameter(11, nameof(FaSidebarShell.OnAccountClick), OnAccountClick);
        builder.AddComponentParameter(12, nameof(FaSidebarShell.OnLogin), OnLogin);
        builder.AddComponentParameter(13, nameof(FaSidebarShell.OnLogout), OnLogout);
        builder.AddComponentParameter(14, nameof(FaSidebarShell.Sidebar), Sidebar);
        builder.AddComponentParameter(15, nameof(FaSidebarShell.FooterContent), FooterContent);
        builder.AddComponentParameter(16, nameof(FaSidebarShell.HeaderPosition), HeaderPosition);
        builder.AddComponentParameter(17, nameof(FaSidebarShell.FooterPosition), FooterPosition);
        builder.AddComponentParameter(18, nameof(FaSidebarShell.SidebarPosition), SidebarPosition);
        builder.AddComponentParameter(19, nameof(FaSidebarShell.SidebarCollapsible), SidebarCollapsible);
        builder.AddComponentParameter(20, nameof(FaSidebarShell.ContainScroll), ContainScroll);

        builder.AddComponentParameter(21, nameof(FaSidebarShell.ChildContent), (RenderFragment)(body =>
        {
            body.OpenElement(22, "div");
            body.AddAttribute(23, "class", "fa-docs-template");

            // Top bar: breadcrumbs & search
            if (BreadcrumbContent != null || SearchContent != null)
            {
                body.OpenElement(24, "div");
                body.AddAttribute(25, "class", "fa-docs-topbar");
                if (BreadcrumbContent != null)
                {
                    body.OpenElement(26, "div");
                    body.AddAttribute(27, "class", "fa-docs-breadcrumbs");
                    body.AddContent(28, BreadcrumbContent);
                    body.CloseElement(); // fa-docs-breadcrumbs
                }
                if (SearchContent != null)
                {
                    body.OpenElement(29, "div");
                    body.AddAttribute(30, "class", "fa-docs-search");
                    body.AddContent(31, SearchContent);
                    body.CloseElement(); // fa-docs-search
                }
                body.CloseElement(); // fa-docs-topbar
            }

            // Two-column body: main article + right aside TOC
            body.OpenElement(32, "div");
            body.AddAttribute(33, "class", "fa-docs-layout");

            body.OpenElement(34, "article");
            body.AddAttribute(35, "class", "fa-docs-article");

            // Article header
            body.OpenElement(36, "header");
            body.AddAttribute(37, "class", "fa-docs-header");

            body.OpenElement(38, "div");
            body.AddAttribute(39, "class", "fa-docs-title-row");
            body.OpenElement(40, "h1");
            body.AddAttribute(41, "class", "fa-docs-title");
            body.AddContent(42, Title);
            body.CloseElement(); // h1

            if (Badge != null)
            {
                body.OpenElement(43, "div");
                body.AddAttribute(44, "class", "fa-docs-badge");
                body.AddContent(45, Badge);
                body.CloseElement(); // fa-docs-badge
            }
            body.CloseElement(); // fa-docs-title-row

            if (!string.IsNullOrWhiteSpace(Description))
            {
                body.OpenElement(46, "p");
                body.AddAttribute(47, "class", "fa-docs-description");
                body.AddContent(48, Description);
                body.CloseElement(); // p
            }
            body.CloseElement(); // header

            // Article body
            body.OpenElement(49, "div");
            body.AddAttribute(50, "class", "fa-docs-body");
            body.AddContent(51, ChildContent);
            body.CloseElement(); // fa-docs-body

            // Prev / Next nav
            if (PrevNextNav != null)
            {
                body.OpenElement(52, "nav");
                body.AddAttribute(53, "class", "fa-docs-prev-next");
                body.AddContent(54, PrevNextNav);
                body.CloseElement(); // nav
            }
            body.CloseElement(); // article

            // Right aside table of contents
            if (TableOfContents != null)
            {
                body.OpenElement(55, "aside");
                body.AddAttribute(56, "class", "fa-docs-toc");
                body.OpenElement(57, "h4");
                body.AddAttribute(58, "class", "fa-docs-toc-title");
                body.AddContent(59, "On this page");
                body.CloseElement(); // h4
                body.OpenElement(60, "div");
                body.AddAttribute(61, "class", "fa-docs-toc-content");
                body.AddContent(62, TableOfContents);
                body.CloseElement(); // fa-docs-toc-content
                body.CloseElement(); // aside
            }

            body.CloseElement(); // fa-docs-layout
            body.CloseElement(); // fa-docs-template
        }));

        builder.CloseComponent();
    }
}
