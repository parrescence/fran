using Fran.Layout;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Fran.Templates;

/// <summary>
/// A how-to / tutorial page template: <see cref="FaStandardShell"/> wrapped around
/// breadcrumbs and difficulty/reading-time metadata, an article hero with author details,
/// a structured sequence of numbered tutorial steps (with screenshots, tips, and code samples),
/// and bottom navigation to subsequent tutorials or related guides.
/// </summary>
public sealed class FaTutorialTemplate : ComponentBase
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

    /// <summary>Breadcrumb trail above the title (e.g. Tutorials &gt; Blazor &gt; Getting Started).</summary>
    [Parameter] public RenderFragment? BreadcrumbContent { get; set; }

    /// <summary>Category or topic pill (e.g. &lt;FaBadge&gt;WebAssembly&lt;/FaBadge&gt;).</summary>
    [Parameter] public RenderFragment? CategoryBadge { get; set; }

    /// <summary>Estimated reading/completion time, e.g. "12 min read" or "15 mins".</summary>
    [Parameter] public string? ReadTime { get; set; }

    /// <summary>Difficulty level badge or label, e.g. "Beginner", "Intermediate", "Advanced".</summary>
    [Parameter] public string? Difficulty { get; set; }

    /// <summary>Tutorial headline, e.g. "Building your first dashboard with Fran".</summary>
    [Parameter] public string Title { get; set; } = "Tutorial";

    /// <summary>Introductory summary explaining what will be built and learned.</summary>
    [Parameter] public string? Description { get; set; }

    /// <summary>Author or contributor credit slot.</summary>
    [Parameter] public RenderFragment? AuthorContent { get; set; }

    /// <summary>Table of tutorial steps or quick jump list.</summary>
    [Parameter] public RenderFragment? StepJumpList { get; set; }

    /// <summary>Main tutorial content and numbered step cards.</summary>
    [Parameter] public RenderFragment? ChildContent { get; set; }

    /// <summary>Previous / Next tutorial navigation links rendered at the bottom.</summary>
    [Parameter] public RenderFragment? PrevNextNav { get; set; }

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
            body.AddAttribute(19, "class", "fa-tutorial-template");

            // Top meta row
            body.OpenElement(20, "div");
            body.AddAttribute(21, "class", "fa-tutorial-topbar");
            if (BreadcrumbContent != null)
            {
                body.OpenElement(22, "div");
                body.AddAttribute(23, "class", "fa-tutorial-breadcrumbs");
                body.AddContent(24, BreadcrumbContent);
                body.CloseElement(); // fa-tutorial-breadcrumbs
            }
            body.OpenElement(25, "div");
            body.AddAttribute(26, "class", "fa-tutorial-tags");
            if (CategoryBadge != null)
            {
                body.AddContent(27, CategoryBadge);
            }
            if (!string.IsNullOrWhiteSpace(Difficulty))
            {
                body.OpenElement(28, "span");
                body.AddAttribute(29, "class", "fa-tutorial-difficulty");
                body.AddContent(30, Difficulty);
                body.CloseElement(); // span
            }
            if (!string.IsNullOrWhiteSpace(ReadTime))
            {
                body.OpenElement(31, "span");
                body.AddAttribute(32, "class", "fa-tutorial-read-time");
                body.AddContent(33, ReadTime);
                body.CloseElement(); // span
            }
            body.CloseElement(); // fa-tutorial-tags
            body.CloseElement(); // fa-tutorial-topbar

            // Tutorial Hero Header
            body.OpenElement(34, "header");
            body.AddAttribute(35, "class", "fa-tutorial-header");
            body.OpenElement(36, "h1");
            body.AddAttribute(37, "class", "fa-tutorial-title");
            body.AddContent(38, Title);
            body.CloseElement(); // h1

            if (!string.IsNullOrWhiteSpace(Description))
            {
                body.OpenElement(39, "p");
                body.AddAttribute(40, "class", "fa-tutorial-description");
                body.AddContent(41, Description);
                body.CloseElement(); // p
            }

            if (AuthorContent != null)
            {
                body.OpenElement(42, "div");
                body.AddAttribute(43, "class", "fa-tutorial-author");
                body.AddContent(44, AuthorContent);
                body.CloseElement(); // fa-tutorial-author
            }
            body.CloseElement(); // header

            // Main body with step jump list
            body.OpenElement(45, "div");
            body.AddAttribute(46, "class", "fa-tutorial-layout");

            body.OpenElement(47, "main");
            body.AddAttribute(48, "class", "fa-tutorial-main");
            body.AddContent(49, ChildContent);

            if (PrevNextNav != null)
            {
                body.OpenElement(50, "nav");
                body.AddAttribute(51, "class", "fa-tutorial-prev-next");
                body.AddContent(52, PrevNextNav);
                body.CloseElement(); // nav
            }
            body.CloseElement(); // main

            if (StepJumpList != null)
            {
                body.OpenElement(53, "aside");
                body.AddAttribute(54, "class", "fa-tutorial-aside");
                body.OpenElement(55, "h4");
                body.AddAttribute(56, "class", "fa-tutorial-aside-title");
                body.AddContent(57, "Tutorial Steps");
                body.CloseElement(); // h4
                body.AddContent(58, StepJumpList);
                body.CloseElement(); // aside
            }

            body.CloseElement(); // fa-tutorial-layout
            body.CloseElement(); // fa-tutorial-template
        }));

        builder.CloseComponent();
    }
}
