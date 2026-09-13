using Fran.Layout;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Fran.Templates;

/// <summary>
/// A detail / profile view page template: <see cref="FaSidebarShell"/> wrapped around
/// a back link or breadcrumb trail, a record header row with title, status badge, subtitle,
/// and actions, a navigation tabs slot (<see cref="Fran.Components.FaTabs{TValue}"/>), and a main
/// body with an optional side summary card/panel.
/// </summary>
public sealed class FaDetailViewTemplate : ComponentBase
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

    [Parameter] public RenderFragment? Sidebar { get; set; }
    [Parameter] public RenderFragment? FooterContent { get; set; }

    [Parameter] public FaNavPosition HeaderPosition { get; set; } = FaNavPosition.Standard;
    [Parameter] public FaNavPosition FooterPosition { get; set; } = FaNavPosition.Standard;
    [Parameter] public FaNavPosition SidebarPosition { get; set; } = FaNavPosition.Standard;
    [Parameter] public bool SidebarCollapsible { get; set; }
    [Parameter] public bool ContainScroll { get; set; }

    /// <summary>Optional back link destination (e.g. "/customers").</summary>
    [Parameter] public string? BackHref { get; set; }

    /// <summary>Back link label. Defaults to "Back".</summary>
    [Parameter] public string BackText { get; set; } = "Back";

    /// <summary>Optional breadcrumbs component or trail (overrides BackHref if set).</summary>
    [Parameter] public RenderFragment? BreadcrumbContent { get; set; }

    /// <summary>Entity / record title (e.g. customer name, order number, product name).</summary>
    [Parameter] public string Title { get; set; } = "Detail";

    /// <summary>Status badge rendered next to the title (e.g. &lt;FaBadge Variant="Success"&gt;Active&lt;/FaBadge&gt;).</summary>
    [Parameter] public RenderFragment? StatusBadge { get; set; }

    /// <summary>Subtitle or timestamp metadata rendered below the title.</summary>
    [Parameter] public string? Subtitle { get; set; }

    /// <summary>Action buttons on the right side of the header (Edit, Delete, Download, etc.).</summary>
    [Parameter] public RenderFragment? Actions { get; set; }

    /// <summary>Navigation tabs strip for tabbed detail pages (Overview, History, Documents, etc.).</summary>
    [Parameter] public RenderFragment? Tabs { get; set; }

    /// <summary>Primary detail content — data fields, tables, timelines, cards.</summary>
    [Parameter] public RenderFragment? ChildContent { get; set; }

    /// <summary>Optional right-hand summary column or metadata aside panel.</summary>
    [Parameter] public RenderFragment? AsideContent { get; set; }

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
        builder.AddComponentParameter(21, nameof(FaSidebarShell.ChildContent), (RenderFragment)RenderBody);
        builder.CloseComponent();
    }

    private void RenderBody(RenderTreeBuilder builder)
    {
        var seq = 0;
        builder.OpenElement(seq++, "div");
        builder.AddAttribute(seq++, "class", "fa-template-detail");

        // Back link or breadcrumbs
        if (BreadcrumbContent is not null)
        {
            builder.OpenElement(seq++, "div");
            builder.AddAttribute(seq++, "class", "fa-template-detail-breadcrumb");
            builder.AddContent(seq++, BreadcrumbContent);
            builder.CloseElement();
        }
        else if (!string.IsNullOrEmpty(BackHref))
        {
            builder.OpenElement(seq++, "a");
            builder.AddAttribute(seq++, "class", "fa-template-back-link");
            builder.AddAttribute(seq++, "href", BackHref);
            builder.AddContent(seq++, $"‹ {BackText}");
            builder.CloseElement();
        }

        // Header Row
        builder.OpenElement(seq++, "div");
        builder.AddAttribute(seq++, "class", "fa-template-detail-header");

        builder.OpenElement(seq++, "div");
        builder.AddAttribute(seq++, "class", "fa-template-detail-title-wrap");

        builder.OpenElement(seq++, "div");
        builder.AddAttribute(seq++, "class", "fa-template-detail-title-row");

        builder.OpenElement(seq++, "h1");
        builder.AddAttribute(seq++, "class", "fa-template-detail-title");
        builder.AddContent(seq++, Title);
        builder.CloseElement();

        if (StatusBadge is not null)
        {
            builder.OpenElement(seq++, "div");
            builder.AddAttribute(seq++, "class", "fa-template-detail-badge");
            builder.AddContent(seq++, StatusBadge);
            builder.CloseElement();
        }

        builder.CloseElement(); // .fa-template-detail-title-row

        if (!string.IsNullOrEmpty(Subtitle))
        {
            builder.OpenElement(seq++, "p");
            builder.AddAttribute(seq++, "class", "fa-template-detail-subtitle");
            builder.AddContent(seq++, Subtitle);
            builder.CloseElement();
        }

        builder.CloseElement(); // .fa-template-detail-title-wrap

        if (Actions is not null)
        {
            builder.OpenElement(seq++, "div");
            builder.AddAttribute(seq++, "class", "fa-template-detail-actions");
            builder.AddContent(seq++, Actions);
            builder.CloseElement();
        }

        builder.CloseElement(); // .fa-template-detail-header

        // Tabs
        if (Tabs is not null)
        {
            builder.OpenElement(seq++, "div");
            builder.AddAttribute(seq++, "class", "fa-template-detail-tabs");
            builder.AddContent(seq++, Tabs);
            builder.CloseElement();
        }

        // Main & Aside Grid
        if (AsideContent is not null)
        {
            builder.OpenElement(seq++, "div");
            builder.AddAttribute(seq++, "class", "fa-template-detail-grid");

            builder.OpenElement(seq++, "div");
            builder.AddAttribute(seq++, "class", "fa-template-detail-main");
            builder.AddContent(seq++, ChildContent);
            builder.CloseElement();

            builder.OpenElement(seq++, "aside");
            builder.AddAttribute(seq++, "class", "fa-template-detail-aside");
            builder.AddContent(seq++, AsideContent);
            builder.CloseElement();

            builder.CloseElement(); // .fa-template-detail-grid
        }
        else
        {
            builder.OpenElement(seq++, "div");
            builder.AddAttribute(seq++, "class", "fa-template-detail-main");
            builder.AddContent(seq++, ChildContent);
            builder.CloseElement();
        }

        builder.CloseElement(); // .fa-template-detail
    }
}
