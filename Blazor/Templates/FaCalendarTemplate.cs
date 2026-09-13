using Fran.Layout;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Fran.Templates;

/// <summary>
/// A calendar &amp; scheduling template: <see cref="FaSidebarShell"/> wrapped around
/// a toolbar with month/week/day view switchers, date navigation controls, "New Event" action,
/// a main calendar grid slot, and a side agenda panel for selected date events.
/// </summary>
public sealed class FaCalendarTemplate : ComponentBase
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

    /// <summary>Heading or active view title, e.g. "Calendar".</summary>
    [Parameter] public string Title { get; set; } = "Calendar";

    /// <summary>Current date range title, e.g. "October 2026" or "Oct 12 – 18, 2026".</summary>
    [Parameter] public string? DateRangeTitle { get; set; }

    /// <summary>Date navigation pager (Previous, Next, Today buttons).</summary>
    [Parameter] public RenderFragment? DateNav { get; set; }

    /// <summary>Calendar view switcher (Month / Week / Day / Agenda).</summary>
    [Parameter] public RenderFragment? ViewSwitcher { get; set; }

    /// <summary>Primary header action button (e.g. "+ New Event").</summary>
    [Parameter] public RenderFragment? PrimaryAction { get; set; }

    /// <summary>Sub-filters or mini calendar picker inside the calendar toolbar.</summary>
    [Parameter] public RenderFragment? FilterContent { get; set; }

    /// <summary>The main calendar grid or list slot.</summary>
    [Parameter] public RenderFragment? ChildContent { get; set; }

    /// <summary>Side agenda panel for upcoming meetings or selected date details.</summary>
    [Parameter] public RenderFragment? EventAsideContent { get; set; }

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
            body.AddAttribute(23, "class", "fa-calendar-template");

            // Toolbar
            body.OpenElement(24, "header");
            body.AddAttribute(25, "class", "fa-calendar-header");

            body.OpenElement(26, "div");
            body.AddAttribute(27, "class", "fa-calendar-title-group");
            body.OpenElement(28, "h1");
            body.AddAttribute(29, "class", "fa-calendar-title");
            body.AddContent(30, Title);
            body.CloseElement(); // h1

            if (!string.IsNullOrWhiteSpace(DateRangeTitle))
            {
                body.OpenElement(31, "span");
                body.AddAttribute(32, "class", "fa-calendar-range-title");
                body.AddContent(33, DateRangeTitle);
                body.CloseElement(); // span
            }
            body.CloseElement(); // title-group

            body.OpenElement(34, "div");
            body.AddAttribute(35, "class", "fa-calendar-controls");

            if (DateNav != null)
            {
                body.OpenElement(36, "div");
                body.AddAttribute(37, "class", "fa-calendar-nav-group");
                body.AddContent(38, DateNav);
                body.CloseElement(); // nav-group
            }

            if (ViewSwitcher != null)
            {
                body.OpenElement(39, "div");
                body.AddAttribute(40, "class", "fa-calendar-view-switcher");
                body.AddContent(41, ViewSwitcher);
                body.CloseElement(); // view-switcher
            }

            if (PrimaryAction != null)
            {
                body.OpenElement(42, "div");
                body.AddAttribute(43, "class", "fa-calendar-primary-action");
                body.AddContent(44, PrimaryAction);
                body.CloseElement(); // primary-action
            }

            body.CloseElement(); // controls
            body.CloseElement(); // header

            if (FilterContent != null)
            {
                body.OpenElement(45, "div");
                body.AddAttribute(46, "class", "fa-calendar-filters");
                body.AddContent(47, FilterContent);
                body.CloseElement(); // filters
            }

            // Main layout (Calendar grid + Event aside)
            body.OpenElement(48, "div");
            body.AddAttribute(49, "class", "fa-calendar-body");

            body.OpenElement(50, "main");
            body.AddAttribute(51, "class", "fa-calendar-main");
            body.AddContent(52, ChildContent);
            body.CloseElement(); // main

            if (EventAsideContent != null)
            {
                body.OpenElement(53, "aside");
                body.AddAttribute(54, "class", "fa-calendar-aside");
                body.AddContent(55, EventAsideContent);
                body.CloseElement(); // aside
            }

            body.CloseElement(); // body
            body.CloseElement(); // fa-calendar-template
        }));

        builder.CloseComponent();
    }
}
