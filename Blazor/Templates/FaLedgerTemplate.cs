using Fran.Layout;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Fran.Templates;

/// <summary>
/// A specialized financial ledger page template: renders either a full <see cref="FaSidebarShell"/>
/// or an embedded layout container (<see cref="EmbedInLayout"/> = true) with a financial header,
/// account/statement summary bar, responsive search/filter bar (with mobile bottom-sheet toggle),
/// transaction ledger body (table on desktop, tap-friendly cards on mobile), pagination bar,
/// and an optional thumb-zone mobile floating action button (FAB).
/// </summary>
public sealed class FaLedgerTemplate : ComponentBase
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

    /// <summary>Ledger page title, e.g. "Transactions" or "Account Ledger".</summary>
    [Parameter] public string Title { get; set; } = "Transactions";

    /// <summary>Optional subtitle or account balance summary line.</summary>
    [Parameter] public string? Description { get; set; }

    /// <summary>Optional badge next to title (e.g. transaction count or status).</summary>
    [Parameter] public RenderFragment? TitleBadge { get; set; }

    /// <summary>Header action buttons (e.g. "+ Add Transaction", "Import Statement").</summary>
    [Parameter] public RenderFragment? HeaderActions { get; set; }

    /// <summary>Account summary card, running balances, or active account switcher banner.</summary>
    [Parameter] public RenderFragment? SummaryContent { get; set; }

    /// <summary>Search, date ranges, and filter controls.</summary>
    [Parameter] public RenderFragment? FiltersContent { get; set; }

    /// <summary>Main ledger body — table on desktop, cards on mobile.</summary>
    [Parameter] public RenderFragment? ChildContent { get; set; }

    /// <summary>Optional pagination or ledger footer.</summary>
    [Parameter] public RenderFragment? PaginationContent { get; set; }

    /// <summary>Optional thumb-zone mobile floating action button (FAB).</summary>
    [Parameter] public RenderFragment? FloatingAction { get; set; }

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
        builder.AddAttribute(seq++, "class", "fa-financial-ledger-template");

        // Header Row
        builder.OpenElement(seq++, "div");
        builder.AddAttribute(seq++, "class", "fa-ledger-header");

        builder.OpenElement(seq++, "div");
        builder.AddAttribute(seq++, "class", "fa-ledger-title-group");

        builder.OpenElement(seq++, "div");
        builder.AddAttribute(seq++, "class", "fa-ledger-title-row");
        builder.OpenElement(seq++, "h1");
        builder.AddAttribute(seq++, "class", "fa-ledger-title");
        builder.AddContent(seq++, Title);
        builder.CloseElement(); // h1

        if (TitleBadge is not null)
        {
            builder.OpenElement(seq++, "span");
            builder.AddAttribute(seq++, "class", "fa-ledger-title-badge");
            builder.AddContent(seq++, TitleBadge);
            builder.CloseElement();
        }
        builder.CloseElement(); // .fa-ledger-title-row

        if (!string.IsNullOrEmpty(Description))
        {
            builder.OpenElement(seq++, "p");
            builder.AddAttribute(seq++, "class", "fa-ledger-desc");
            builder.AddContent(seq++, Description);
            builder.CloseElement();
        }
        builder.CloseElement(); // .fa-ledger-title-group

        if (HeaderActions is not null)
        {
            builder.OpenElement(seq++, "div");
            builder.AddAttribute(seq++, "class", "fa-ledger-header-actions");
            builder.AddContent(seq++, HeaderActions);
            builder.CloseElement();
        }
        builder.CloseElement(); // .fa-ledger-header

        // Summary bar slot (e.g. Account Card / Balances)
        if (SummaryContent is not null)
        {
            builder.OpenElement(seq++, "div");
            builder.AddAttribute(seq++, "class", "fa-ledger-summary");
            builder.AddContent(seq++, SummaryContent);
            builder.CloseElement();
        }

        // Filters bar slot
        if (FiltersContent is not null)
        {
            builder.OpenElement(seq++, "div");
            builder.AddAttribute(seq++, "class", "fa-ledger-filter-bar");
            builder.AddContent(seq++, FiltersContent);
            builder.CloseElement();
        }

        // Main Ledger Items
        builder.OpenElement(seq++, "div");
        builder.AddAttribute(seq++, "class", "fa-ledger-content");
        builder.AddContent(seq++, ChildContent);
        builder.CloseElement();

        // Pagination slot
        if (PaginationContent is not null)
        {
            builder.OpenElement(seq++, "div");
            builder.AddAttribute(seq++, "class", "fa-ledger-pagination");
            builder.AddContent(seq++, PaginationContent);
            builder.CloseElement();
        }

        // Floating Action Button (Mobile Thumb Zone)
        if (FloatingAction is not null)
        {
            builder.OpenElement(seq++, "div");
            builder.AddAttribute(seq++, "class", "fa-ledger-fab-slot");
            builder.AddContent(seq++, FloatingAction);
            builder.CloseElement();
        }

        builder.CloseElement(); // .fa-financial-ledger-template
    }
}
