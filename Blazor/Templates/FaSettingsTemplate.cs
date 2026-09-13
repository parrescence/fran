using Fran.Components;
using Fran.Layout;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Fran.Templates;

/// <summary>
/// A settings / preferences page template: <see cref="FaSidebarShell"/> wrapped around a
/// settings header row and a two-column layout with a settings sub-navigation sidebar
/// (Account, Security, Billing, Team, etc.) on the left and a framed settings card/panel on the right.
/// </summary>
public sealed class FaSettingsTemplate : ComponentBase
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

    /// <summary>The main app shell sidebar nav content (e.g. NavMenu).</summary>
    [Parameter] public RenderFragment? Sidebar { get; set; }
    [Parameter] public RenderFragment? FooterContent { get; set; }

    [Parameter] public FaNavPosition HeaderPosition { get; set; } = FaNavPosition.Standard;
    [Parameter] public FaNavPosition FooterPosition { get; set; } = FaNavPosition.Standard;
    [Parameter] public FaNavPosition SidebarPosition { get; set; } = FaNavPosition.Standard;
    [Parameter] public bool SidebarCollapsible { get; set; }
    [Parameter] public bool ContainScroll { get; set; }

    /// <summary>Overall page title, e.g. "Settings".</summary>
    [Parameter] public string Title { get; set; } = "Settings";

    /// <summary>Overall page description, e.g. "Manage your profile, team, and account preferences."</summary>
    [Parameter] public string? Description { get; set; }

    /// <summary>Settings sub-navigation sidebar links (e.g. Profile, Security, Billing, Notifications).</summary>
    [Parameter] public RenderFragment? SettingsNav { get; set; }

    /// <summary>Current active section title, e.g. "Profile Information".</summary>
    [Parameter] public string? SectionTitle { get; set; }

    /// <summary>Current active section description.</summary>
    [Parameter] public string? SectionDescription { get; set; }

    /// <summary>Actions at the bottom of the active settings card (e.g. Save changes / Cancel buttons).</summary>
    [Parameter] public RenderFragment? SectionFooter { get; set; }

    /// <summary>The active settings form or configuration controls.</summary>
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
        builder.AddComponentParameter(21, nameof(FaSidebarShell.ChildContent), (RenderFragment)RenderBody);
        builder.CloseComponent();
    }

    private void RenderBody(RenderTreeBuilder builder)
    {
        var seq = 0;
        builder.OpenElement(seq++, "div");
        builder.AddAttribute(seq++, "class", "fa-template-settings");

        // Page Header
        if (!string.IsNullOrEmpty(Title) || !string.IsNullOrEmpty(Description))
        {
            builder.OpenElement(seq++, "div");
            builder.AddAttribute(seq++, "class", "fa-template-settings-header");

            if (!string.IsNullOrEmpty(Title))
            {
                builder.OpenElement(seq++, "h1");
                builder.AddAttribute(seq++, "class", "fa-template-settings-title");
                builder.AddContent(seq++, Title);
                builder.CloseElement();
            }

            if (!string.IsNullOrEmpty(Description))
            {
                builder.OpenElement(seq++, "p");
                builder.AddAttribute(seq++, "class", "fa-template-settings-description");
                builder.AddContent(seq++, Description);
                builder.CloseElement();
            }

            builder.CloseElement(); // .fa-template-settings-header
        }

        // Two-column layout
        builder.OpenElement(seq++, "div");
        builder.AddAttribute(seq++, "class", "fa-template-settings-body");

        if (SettingsNav is not null)
        {
            builder.OpenElement(seq++, "nav");
            builder.AddAttribute(seq++, "class", "fa-template-settings-nav");
            builder.AddAttribute(seq++, "aria-label", "Settings categories");
            builder.AddContent(seq++, SettingsNav);
            builder.CloseElement();
        }

        builder.OpenElement(seq++, "div");
        builder.AddAttribute(seq++, "class", "fa-template-settings-content");

        builder.OpenComponent<FaCard>(seq++);
        builder.AddComponentParameter(seq++, nameof(FaCard.ChildContent), (RenderFragment)(b =>
        {
            var cardSeq = 0;
            if (!string.IsNullOrEmpty(SectionTitle) || !string.IsNullOrEmpty(SectionDescription))
            {
                b.OpenElement(cardSeq++, "div");
                b.AddAttribute(cardSeq++, "class", "fa-template-settings-section-header");

                if (!string.IsNullOrEmpty(SectionTitle))
                {
                    b.OpenElement(cardSeq++, "h2");
                    b.AddAttribute(cardSeq++, "class", "fa-template-settings-section-title");
                    b.AddContent(cardSeq++, SectionTitle);
                    b.CloseElement();
                }

                if (!string.IsNullOrEmpty(SectionDescription))
                {
                    b.OpenElement(cardSeq++, "p");
                    b.AddAttribute(cardSeq++, "class", "fa-template-settings-section-description");
                    b.AddContent(cardSeq++, SectionDescription);
                    b.CloseElement();
                }

                b.CloseElement(); // .fa-template-settings-section-header
            }

            b.AddContent(cardSeq++, ChildContent);

            if (SectionFooter is not null)
            {
                b.OpenElement(cardSeq++, "div");
                b.AddAttribute(cardSeq++, "class", "fa-template-settings-section-footer");
                b.AddContent(cardSeq++, SectionFooter);
                b.CloseElement();
            }
        }));
        builder.CloseComponent(); // FaCard

        builder.CloseElement(); // .fa-template-settings-content
        builder.CloseElement(); // .fa-template-settings-body
        builder.CloseElement(); // .fa-template-settings
    }
}
