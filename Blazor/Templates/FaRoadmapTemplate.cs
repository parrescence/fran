using Fran.Layout;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Fran.Templates;

/// <summary>
/// A product roadmap template: <see cref="FaStandardShell"/> wrapped around a header
/// with title, description, and feature request action, a filter bar, and a 3-column
/// Kanban board (Planned, In Progress, Completed) or custom timeline stream.
/// </summary>
public sealed class FaRoadmapTemplate : ComponentBase
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

    /// <summary>Page heading, e.g. "Product Roadmap".</summary>
    [Parameter] public string Title { get; set; } = "Roadmap";

    /// <summary>Subheading or mission statement.</summary>
    [Parameter] public string? Description { get; set; } = "Follow upcoming features, work in progress, and recent releases.";

    /// <summary>Header action slot, e.g. "Suggest a feature" button or subscribe link.</summary>
    [Parameter] public RenderFragment? HeaderAction { get; set; }

    /// <summary>Filter toolbar or category tabs slot.</summary>
    [Parameter] public RenderFragment? FilterBar { get; set; }

    /// <summary>Kanban column content for Planned items.</summary>
    [Parameter] public RenderFragment? PlannedColumn { get; set; }

    /// <summary>Kanban column content for In Progress items.</summary>
    [Parameter] public RenderFragment? InProgressColumn { get; set; }

    /// <summary>Kanban column content for Completed / Released items.</summary>
    [Parameter] public RenderFragment? CompletedColumn { get; set; }

    /// <summary>Optional custom board, timeline, or additional roadmap content.</summary>
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
            body.AddAttribute(19, "class", "fa-roadmap-template");

            // Header
            body.OpenElement(20, "header");
            body.AddAttribute(21, "class", "fa-roadmap-header");

            body.OpenElement(22, "div");
            body.AddAttribute(23, "class", "fa-roadmap-title-row");

            body.OpenElement(24, "div");
            body.AddAttribute(25, "class", "fa-roadmap-title-group");
            body.OpenElement(26, "h1");
            body.AddAttribute(27, "class", "fa-roadmap-title");
            body.AddContent(28, Title);
            body.CloseElement(); // h1

            if (!string.IsNullOrWhiteSpace(Description))
            {
                body.OpenElement(29, "p");
                body.AddAttribute(30, "class", "fa-roadmap-desc");
                body.AddContent(31, Description);
                body.CloseElement(); // p
            }
            body.CloseElement(); // fa-roadmap-title-group

            if (HeaderAction != null)
            {
                body.OpenElement(32, "div");
                body.AddAttribute(33, "class", "fa-roadmap-actions");
                body.AddContent(34, HeaderAction);
                body.CloseElement(); // fa-roadmap-actions
            }
            body.CloseElement(); // fa-roadmap-title-row

            if (FilterBar != null)
            {
                body.OpenElement(35, "div");
                body.AddAttribute(36, "class", "fa-roadmap-filter-bar");
                body.AddContent(37, FilterBar);
                body.CloseElement(); // fa-roadmap-filter-bar
            }
            body.CloseElement(); // header

            // Kanban columns if any column is defined
            if (PlannedColumn != null || InProgressColumn != null || CompletedColumn != null)
            {
                body.OpenElement(38, "div");
                body.AddAttribute(39, "class", "fa-roadmap-kanban");

                // Planned
                body.OpenElement(40, "div");
                body.AddAttribute(41, "class", "fa-roadmap-col fa-roadmap-col-planned");
                body.OpenElement(42, "div");
                body.AddAttribute(43, "class", "fa-roadmap-col-header");
                body.OpenElement(44, "h3");
                body.AddAttribute(45, "class", "fa-roadmap-col-title");
                body.AddContent(46, "Planned");
                body.CloseElement(); // h3
                body.CloseElement(); // fa-roadmap-col-header
                body.OpenElement(47, "div");
                body.AddAttribute(48, "class", "fa-roadmap-col-body");
                body.AddContent(49, PlannedColumn);
                body.CloseElement(); // fa-roadmap-col-body
                body.CloseElement(); // col

                // In Progress
                body.OpenElement(50, "div");
                body.AddAttribute(51, "class", "fa-roadmap-col fa-roadmap-col-progress");
                body.OpenElement(52, "div");
                body.AddAttribute(53, "class", "fa-roadmap-col-header");
                body.OpenElement(54, "h3");
                body.AddAttribute(55, "class", "fa-roadmap-col-title");
                body.AddContent(56, "In Progress");
                body.CloseElement(); // h3
                body.CloseElement(); // fa-roadmap-col-header
                body.OpenElement(57, "div");
                body.AddAttribute(58, "class", "fa-roadmap-col-body");
                body.AddContent(59, InProgressColumn);
                body.CloseElement(); // fa-roadmap-col-body
                body.CloseElement(); // col

                // Completed
                body.OpenElement(60, "div");
                body.AddAttribute(61, "class", "fa-roadmap-col fa-roadmap-col-completed");
                body.OpenElement(62, "div");
                body.AddAttribute(63, "class", "fa-roadmap-col-header");
                body.OpenElement(64, "h3");
                body.AddAttribute(65, "class", "fa-roadmap-col-title");
                body.AddContent(66, "Completed");
                body.CloseElement(); // h3
                body.CloseElement(); // fa-roadmap-col-header
                body.OpenElement(67, "div");
                body.AddAttribute(68, "class", "fa-roadmap-col-body");
                body.AddContent(69, CompletedColumn);
                body.CloseElement(); // fa-roadmap-col-body
                body.CloseElement(); // col

                body.CloseElement(); // fa-roadmap-kanban
            }

            if (ChildContent != null)
            {
                body.OpenElement(70, "div");
                body.AddAttribute(71, "class", "fa-roadmap-content");
                body.AddContent(72, ChildContent);
                body.CloseElement(); // fa-roadmap-content
            }

            body.CloseElement(); // fa-roadmap-template
        }));

        builder.CloseComponent();
    }
}
