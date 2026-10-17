using Fran.Layout;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Fran.Templates;

/// <summary>
/// A modern financial 360° overview template: renders either a full <see cref="FaSidebarShell"/>
/// or an embedded layout container (<see cref="EmbedInLayout"/> = true) with a period switcher header,
/// a touch-friendly swipeable KPI metric strip on mobile (grid on desktop), cash flow analysis,
/// category spending breakdown, and recent transactions feed.
/// </summary>
public sealed class FaFinanceOverviewTemplate : ComponentBase
{
    /// <summary>When true, skips rendering <see cref="FaSidebarShell"/> so this template can be embedded directly inside an existing application layout.</summary>
    [Parameter] public bool EmbedInLayout { get; set; }

    [Parameter] public string BrandText { get; set; } = "";
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

    /// <summary>Page heading, e.g. "Financial Overview" or "Monthly Snapshot".</summary>
    [Parameter] public string Title { get; set; } = "Financial Overview";

    /// <summary>Optional subtitle or period summary description.</summary>
    [Parameter] public string? Subtitle { get; set; }

    /// <summary>Period navigator (e.g. Month stepper, date range picker).</summary>
    [Parameter] public RenderFragment? PeriodSelector { get; set; }

    /// <summary>Header quick action buttons.</summary>
    [Parameter] public RenderFragment? HeaderActions { get; set; }

    /// <summary>KPI metric cards: rendered as a multi-column grid on desktop and an auto-snap horizontal swipe strip on mobile.</summary>
    [Parameter] public RenderFragment? KpiMetrics { get; set; }

    /// <summary>Cash flow overview card (Income vs Expense bar, savings rate, net cash flow).</summary>
    [Parameter] public RenderFragment? CashFlowContent { get; set; }

    /// <summary>Category spending allocation or visual breakdown.</summary>
    [Parameter] public RenderFragment? CategoryBreakdown { get; set; }

    /// <summary>Recent transaction activity feed or ledger teaser.</summary>
    [Parameter] public RenderFragment? RecentActivity { get; set; }

    /// <summary>Optional additional widgets or dashboard cards.</summary>
    [Parameter] public RenderFragment? ChildContent { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (EmbedInLayout)
        {
            RenderBody(builder);
            return;
        }

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
        builder.AddAttribute(seq++, "class", "fa-finance-overview-template");

        // Header Row: Title, Period Selector, Actions
        builder.OpenElement(seq++, "div");
        builder.AddAttribute(seq++, "class", "fa-overview-header");

        builder.OpenElement(seq++, "div");
        builder.AddAttribute(seq++, "class", "fa-overview-title-group");
        builder.OpenElement(seq++, "h1");
        builder.AddAttribute(seq++, "class", "fa-overview-title");
        builder.AddContent(seq++, Title);
        builder.CloseElement();

        if (!string.IsNullOrEmpty(Subtitle))
        {
            builder.OpenElement(seq++, "p");
            builder.AddAttribute(seq++, "class", "fa-overview-subtitle");
            builder.AddContent(seq++, Subtitle);
            builder.CloseElement();
        }
        builder.CloseElement(); // .fa-overview-title-group

        // Controls: Period selector + actions
        if (PeriodSelector is not null || HeaderActions is not null)
        {
            builder.OpenElement(seq++, "div");
            builder.AddAttribute(seq++, "class", "fa-overview-controls");

            if (PeriodSelector is not null)
            {
                builder.OpenElement(seq++, "div");
                builder.AddAttribute(seq++, "class", "fa-overview-period-slot");
                builder.AddContent(seq++, PeriodSelector);
                builder.CloseElement();
            }

            if (HeaderActions is not null)
            {
                builder.OpenElement(seq++, "div");
                builder.AddAttribute(seq++, "class", "fa-overview-actions-slot");
                builder.AddContent(seq++, HeaderActions);
                builder.CloseElement();
            }

            builder.CloseElement(); // .fa-overview-controls
        }
        builder.CloseElement(); // .fa-overview-header

        // KPI Metric Cards: Desktop Grid / Mobile Snap-Scroll Strip
        if (KpiMetrics is not null)
        {
            builder.OpenElement(seq++, "section");
            builder.AddAttribute(seq++, "class", "fa-kpi-swipe-strip");
            builder.AddAttribute(seq++, "aria-label", "Financial Key Performance Indicators");
            builder.AddContent(seq++, KpiMetrics);
            builder.CloseElement();
        }

        // Cash Flow Visualizer (Income vs Outflow)
        if (CashFlowContent is not null)
        {
            builder.OpenElement(seq++, "section");
            builder.AddAttribute(seq++, "class", "fa-overview-cashflow-section");
            builder.AddContent(seq++, CashFlowContent);
            builder.CloseElement();
        }

        // Two-column Split: Spending by Category + Recent Activity
        if (CategoryBreakdown is not null || RecentActivity is not null)
        {
            builder.OpenElement(seq++, "div");
            builder.AddAttribute(seq++, "class", "fa-overview-columns");

            if (CategoryBreakdown is not null)
            {
                builder.OpenElement(seq++, "div");
                builder.AddAttribute(seq++, "class", "fa-overview-column-primary");
                builder.AddContent(seq++, CategoryBreakdown);
                builder.CloseElement();
            }

            if (RecentActivity is not null)
            {
                builder.OpenElement(seq++, "div");
                builder.AddAttribute(seq++, "class", "fa-overview-column-secondary");
                builder.AddContent(seq++, RecentActivity);
                builder.CloseElement();
            }

            builder.CloseElement(); // .fa-overview-columns
        }

        // Additional Children
        if (ChildContent is not null)
        {
            builder.OpenElement(seq++, "div");
            builder.AddAttribute(seq++, "class", "fa-overview-extra");
            builder.AddContent(seq++, ChildContent);
            builder.CloseElement();
        }

        builder.CloseElement(); // .fa-finance-overview-template
    }
}
