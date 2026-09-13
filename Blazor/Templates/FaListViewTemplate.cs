using Fran.Layout;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Fran.Templates;

/// <summary>
/// A list / table view page template: <see cref="FaSidebarShell"/> wrapped around a
/// header row with title, count/badge, and action buttons, a search &amp; filter toolbar,
/// a primary data container (typically <see cref="Fran.Components.FaGrid{TItem}"/> or
/// <see cref="Fran.Components.FaTable"/>), and an optional pagination/footer slot.
/// </summary>
public sealed class FaListViewTemplate : ComponentBase
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

    /// <summary>Page heading, e.g. "Customers", "Invoices", "Products".</summary>
    [Parameter] public string Title { get; set; } = "Items";

    /// <summary>Optional subtitle or item count badge rendered next to the title.</summary>
    [Parameter] public RenderFragment? TitleBadge { get; set; }

    /// <summary>Optional short description under the title.</summary>
    [Parameter] public string? Description { get; set; }

    /// <summary>Primary CTA button, e.g. "Add customer" or "New invoice".</summary>
    [Parameter] public RenderFragment? PrimaryAction { get; set; }

    /// <summary>Secondary actions next to the primary button (e.g. Export, Import, Filter).</summary>
    [Parameter] public RenderFragment? SecondaryActions { get; set; }

    /// <summary>Search input, filter selects, or date range toolbar above the data table.</summary>
    [Parameter] public RenderFragment? SearchFilterBar { get; set; }

    /// <summary>The data table, grid, or list of cards.</summary>
    [Parameter] public RenderFragment? ChildContent { get; set; }

    /// <summary>Optional pagination or table summary footer below the data container.</summary>
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
        builder.AddComponentParameter(21, nameof(FaSidebarShell.ChildContent), (RenderFragment)RenderBody);
        builder.CloseComponent();
    }

    private void RenderBody(RenderTreeBuilder builder)
    {
        var seq = 0;
        builder.OpenElement(seq++, "div");
        builder.AddAttribute(seq++, "class", "fa-template-list");

        // Header Row
        builder.OpenElement(seq++, "div");
        builder.AddAttribute(seq++, "class", "fa-template-list-header");

        builder.OpenElement(seq++, "div");
        builder.AddAttribute(seq++, "class", "fa-template-list-title-wrap");

        builder.OpenElement(seq++, "div");
        builder.AddAttribute(seq++, "class", "fa-template-list-title-row");

        builder.OpenElement(seq++, "h1");
        builder.AddAttribute(seq++, "class", "fa-template-list-title");
        builder.AddContent(seq++, Title);
        builder.CloseElement();

        if (TitleBadge is not null)
        {
            builder.OpenElement(seq++, "div");
            builder.AddAttribute(seq++, "class", "fa-template-list-badge");
            builder.AddContent(seq++, TitleBadge);
            builder.CloseElement();
        }

        builder.CloseElement(); // .fa-template-list-title-row

        if (!string.IsNullOrEmpty(Description))
        {
            builder.OpenElement(seq++, "p");
            builder.AddAttribute(seq++, "class", "fa-template-list-description");
            builder.AddContent(seq++, Description);
            builder.CloseElement();
        }

        builder.CloseElement(); // .fa-template-list-title-wrap

        // Actions
        if (PrimaryAction is not null || SecondaryActions is not null)
        {
            builder.OpenElement(seq++, "div");
            builder.AddAttribute(seq++, "class", "fa-template-list-actions");

            if (SecondaryActions is not null)
            {
                builder.AddContent(seq++, SecondaryActions);
            }

            if (PrimaryAction is not null)
            {
                builder.AddContent(seq++, PrimaryAction);
            }

            builder.CloseElement(); // .fa-template-list-actions
        }

        builder.CloseElement(); // .fa-template-list-header

        // Search & Filters Toolbar
        if (SearchFilterBar is not null)
        {
            builder.OpenElement(seq++, "div");
            builder.AddAttribute(seq++, "class", "fa-template-list-toolbar");
            builder.AddContent(seq++, SearchFilterBar);
            builder.CloseElement();
        }

        // Data Table / Grid Body
        if (ChildContent is not null)
        {
            builder.OpenElement(seq++, "div");
            builder.AddAttribute(seq++, "class", "fa-template-list-content");
            builder.AddContent(seq++, ChildContent);
            builder.CloseElement();
        }

        // Pagination / Summary
        if (PaginationContent is not null)
        {
            builder.OpenElement(seq++, "div");
            builder.AddAttribute(seq++, "class", "fa-template-list-pagination");
            builder.AddContent(seq++, PaginationContent);
            builder.CloseElement();
        }

        builder.CloseElement(); // .fa-template-list
    }
}
