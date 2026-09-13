using Fran.Layout;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Fran.Templates;

/// <summary>
/// An activity feed / audit log template: <see cref="FaSidebarShell"/> wrapped around
/// a header with title and export action, optional summary metric chips, a filter toolbar,
/// a chronological timeline stream of events, and pagination or infinite scroll controls.
/// </summary>
public sealed class FaActivityFeedTemplate : ComponentBase
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

    /// <summary>Page title, e.g. "Activity Feed" or "Audit Log".</summary>
    [Parameter] public string Title { get; set; } = "Activity Feed";

    /// <summary>Page description or audit scope.</summary>
    [Parameter] public string? Description { get; set; } = "Audit trail of user actions, deployments, and security events.";

    /// <summary>Action in the header, e.g. "Export CSV" or "Live Feed" toggle.</summary>
    [Parameter] public RenderFragment? HeaderAction { get; set; }

    /// <summary>Quick metric counters or stat cards above the feed.</summary>
    [Parameter] public RenderFragment? SummaryStats { get; set; }

    /// <summary>Filter toolbar (actor select, event type filter, date picker, search).</summary>
    [Parameter] public RenderFragment? FilterToolbar { get; set; }

    /// <summary>Timeline stream of activity items.</summary>
    [Parameter] public RenderFragment? ChildContent { get; set; }

    /// <summary>Pagination or "Load more" container at the bottom.</summary>
    [Parameter] public RenderFragment? PaginationContent { get; set; }

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
            body.AddAttribute(23, "class", "fa-activity-feed-template");

            // Header
            body.OpenElement(24, "header");
            body.AddAttribute(25, "class", "fa-activity-feed-header");

            body.OpenElement(26, "div");
            body.AddAttribute(27, "class", "fa-activity-feed-title-group");
            body.OpenElement(28, "h1");
            body.AddAttribute(29, "class", "fa-activity-feed-title");
            body.AddContent(30, Title);
            body.CloseElement(); // h1

            if (!string.IsNullOrWhiteSpace(Description))
            {
                body.OpenElement(31, "p");
                body.AddAttribute(32, "class", "fa-activity-feed-desc");
                body.AddContent(33, Description);
                body.CloseElement(); // p
            }
            body.CloseElement(); // title-group

            if (HeaderAction != null)
            {
                body.OpenElement(34, "div");
                body.AddAttribute(35, "class", "fa-activity-feed-header-action");
                body.AddContent(36, HeaderAction);
                body.CloseElement(); // action
            }
            body.CloseElement(); // header

            // Stats
            if (SummaryStats != null)
            {
                body.OpenElement(37, "div");
                body.AddAttribute(38, "class", "fa-activity-feed-stats");
                body.AddContent(39, SummaryStats);
                body.CloseElement(); // stats
            }

            // Filter Toolbar
            if (FilterToolbar != null)
            {
                body.OpenElement(40, "div");
                body.AddAttribute(41, "class", "fa-activity-feed-filters");
                body.AddContent(42, FilterToolbar);
                body.CloseElement(); // filters
            }

            // Timeline stream
            if (ChildContent != null)
            {
                body.OpenElement(43, "section");
                body.AddAttribute(44, "class", "fa-activity-feed-stream");
                body.AddContent(45, ChildContent);
                body.CloseElement(); // stream
            }

            // Pagination
            if (PaginationContent != null)
            {
                body.OpenElement(46, "nav");
                body.AddAttribute(47, "class", "fa-activity-feed-pagination");
                body.AddAttribute(48, "aria-label", "Activity feed pagination");
                body.AddContent(49, PaginationContent);
                body.CloseElement(); // pagination
            }

            body.CloseElement(); // fa-activity-feed-template
        }));

        builder.CloseComponent();
    }
}
