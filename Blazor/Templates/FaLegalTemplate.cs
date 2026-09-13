using Fran.Layout;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Fran.Templates;

/// <summary>
/// A legal / compliance page template: <see cref="FaStandardShell"/> wrapped around
/// a legal document header with title, effective/last updated date, optional actions (print, PDF),
/// a sticky table-of-contents navigation rail, and a structured, readable legal text body.
/// Suitable for Terms of Service, Privacy Policy, Cookie Policy, SLAs, and Compliance notices.
/// </summary>
public sealed class FaLegalTemplate : ComponentBase
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

    /// <summary>Document title, e.g. "Terms of Service" or "Privacy Policy".</summary>
    [Parameter] public string Title { get; set; } = "Terms of Service";

    /// <summary>Effective or last-updated date label, e.g. "Last updated: September 1, 2026".</summary>
    [Parameter] public string? LastUpdated { get; set; }

    /// <summary>Introductory summary or overview paragraph.</summary>
    [Parameter] public string? Description { get; set; }

    /// <summary>Optional top-right actions (e.g. "Download PDF" or "Print").</summary>
    [Parameter] public RenderFragment? HeaderActions { get; set; }

    /// <summary>Table of contents navigation links pointing to in-page section anchors.</summary>
    [Parameter] public RenderFragment? TableOfContents { get; set; }

    /// <summary>The legal agreement sections and text content.</summary>
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
            body.AddAttribute(19, "class", "fa-legal-template");

            // Header banner
            body.OpenElement(20, "header");
            body.AddAttribute(21, "class", "fa-legal-header");

            body.OpenElement(22, "div");
            body.AddAttribute(23, "class", "fa-legal-header-main");
            body.OpenElement(24, "h1");
            body.AddAttribute(25, "class", "fa-legal-title");
            body.AddContent(26, Title);
            body.CloseElement(); // h1

            if (!string.IsNullOrWhiteSpace(LastUpdated))
            {
                body.OpenElement(27, "div");
                body.AddAttribute(28, "class", "fa-legal-date");
                body.AddContent(29, LastUpdated);
                body.CloseElement(); // fa-legal-date
            }

            if (!string.IsNullOrWhiteSpace(Description))
            {
                body.OpenElement(30, "p");
                body.AddAttribute(31, "class", "fa-legal-description");
                body.AddContent(32, Description);
                body.CloseElement(); // p
            }
            body.CloseElement(); // fa-legal-header-main

            if (HeaderActions != null)
            {
                body.OpenElement(33, "div");
                body.AddAttribute(34, "class", "fa-legal-actions");
                body.AddContent(35, HeaderActions);
                body.CloseElement(); // fa-legal-actions
            }
            body.CloseElement(); // header

            // Main 2-column layout: TOC aside + document body
            body.OpenElement(36, "div");
            body.AddAttribute(37, "class", "fa-legal-layout");

            if (TableOfContents != null)
            {
                body.OpenElement(38, "nav");
                body.AddAttribute(39, "class", "fa-legal-toc");
                body.OpenElement(40, "h4");
                body.AddAttribute(41, "class", "fa-legal-toc-title");
                body.AddContent(42, "Contents");
                body.CloseElement(); // h4
                body.OpenElement(43, "div");
                body.AddAttribute(44, "class", "fa-legal-toc-links");
                body.AddContent(45, TableOfContents);
                body.CloseElement(); // fa-legal-toc-links
                body.CloseElement(); // nav
            }

            body.OpenElement(46, "article");
            body.AddAttribute(47, "class", "fa-legal-body");
            body.AddContent(48, ChildContent);
            body.CloseElement(); // article

            body.CloseElement(); // fa-legal-layout
            body.CloseElement(); // fa-legal-template
        }));

        builder.CloseComponent();
    }
}
