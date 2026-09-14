using Fran.Components;
using Fran.Icons;
using Fran.Rendering;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Fran.Layout;

/// <summary>
/// Header bar — brand on the left, user name + avatar + login/logout at the far right.
/// Purely presentational: the consuming app supplies auth state and wires up the
/// login/logout callbacks (e.g. FinanceApp.Web's MainLayout, an example consumer, for
/// real wiring).
/// </summary>
public sealed class FaHeader : ComponentBase
{
    [Parameter, EditorRequired] public string BrandText { get; set; } = "";
    [Parameter] public string BrandHref { get; set; } = "";

    /// <summary>
    /// Optional logo/icon shown to the left of <see cref="BrandText"/> inside the brand
    /// link — any URL an &lt;img&gt; src accepts (a static asset path, a data: URI, a
    /// CDN URL). Omit for text-only branding (the default). Rendered decorative
    /// (<c>alt=""</c>) since <see cref="BrandText"/> already supplies the link's
    /// accessible name — this never doubles as the only content conveying meaning.
    /// </summary>
    [Parameter] public string? BrandIconUrl { get; set; }

    [Parameter] public bool IsAuthenticated { get; set; }
    [Parameter] public string? UserDisplayName { get; set; }
    [Parameter] public string? UserImageUrl { get; set; }
    [Parameter] public EventCallback OnLogin { get; set; }
    [Parameter] public EventCallback OnLogout { get; set; }

    /// <summary>
    /// When true, renders <see cref="FaAvatarForm"/> in the header user area instead
    /// of the inline theme switcher, username text, and standalone login/logout button.
    /// Default is false for backward compatibility.
    /// </summary>
    [Parameter] public bool UseAvatarForm { get; set; }

    /// <summary>
    /// When <see cref="UseAvatarForm"/> is true, controls whether the user's name is
    /// shown next to the avatar in the header bar. Defaults to false (only the avatar
    /// is shown in the topbar).
    /// </summary>
    [Parameter] public bool ShowUserNameInHeader { get; set; }

    /// <summary>Optional user email or subtitle shown in the opened avatar form.</summary>
    [Parameter] public string? UserEmail { get; set; }

    /// <summary>Optional URL navigating to the user's account settings/profile form.</summary>
    [Parameter] public string? AccountHref { get; set; }

    /// <summary>Optional callback invoked when the user clicks the account settings action.</summary>
    [Parameter] public EventCallback OnAccountClick { get; set; }

    /// <summary>Label for the account link/button. Defaults to "Account settings".</summary>
    [Parameter] public string AccountText { get; set; } = "Account settings";

    /// <summary>Optional application-specific content rendered inside the opened avatar form.</summary>
    [Parameter] public RenderFragment? UserMenuContent { get; set; }

    /// <summary>Optional navigation buttons or links rendered between the brand and the user area.</summary>
    [Parameter] public RenderFragment? NavContent { get; set; }

    /// <summary>Alias for <see cref="NavContent"/> for convenience.</summary>
    [Parameter] public RenderFragment? NavButtons { get; set; }

    /// <summary>Whether the header scrolls away with the page (default) or stays pinned to the top.</summary>
    [Parameter] public FaNavPosition Position { get; set; } = FaNavPosition.Standard;

    /// <summary>
    /// Renders a hamburger button left of the brand that calls window.faToggleSidebarMobile()
    /// (js/sidebar.js) on click. Only meaningful when this header is paired with a sidebar —
    /// FaSidebarShell sets this itself, so a consumer using FaHeader standalone (FaStandardShell,
    /// no sidebar to toggle) never needs to touch it. The button itself is always in the markup
    /// once set; it's <c>_responsive.scss</c>'s breakpoint that hides it above the mobile width and
    /// hides/shows <c>.fa-sidebar</c> off-canvas below it — independent of FaSidebar.Collapsible,
    /// which is a separate, desktop-only icon-rail affordance.
    /// </summary>
    [Parameter] public bool ShowSidebarToggle { get; set; }

    private string? PositionClass => Position switch
    {
        FaNavPosition.Sticky => "fa-header-sticky",
        FaNavPosition.Floating => "fa-header-floating",
        _ => null
    };

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "header");
        builder.AddAttribute(1, "class", CssClassNames.Combine("fa-header", PositionClass));

        builder.OpenElement(30, "div");
        builder.AddAttribute(31, "class", "fa-header-left");

        if (ShowSidebarToggle)
        {
            builder.OpenElement(32, "button");
            builder.AddAttribute(33, "type", "button");
            builder.AddAttribute(34, "class", "fa-header-menu-toggle");
            builder.AddAttribute(35, "data-sidebar-mobile-toggle", true);
            builder.AddAttribute(36, "title", "Toggle menu");
            builder.AddAttribute(37, "aria-label", "Toggle menu");
            builder.AddAttribute(38, "aria-expanded", "false");
            builder.AddAttribute(39, "onclick", "faToggleSidebarMobile()");

            builder.OpenComponent<FaIcon>(40);
            builder.AddComponentParameter(41, nameof(FaIcon.Name), FaIconName.Menu);
            builder.AddComponentParameter(42, nameof(FaIcon.Color), FaIconColor.White);
            builder.AddComponentParameter(43, nameof(FaIcon.Size), 18);
            builder.CloseComponent();

            builder.CloseElement(); // .fa-header-menu-toggle
        }

        builder.OpenElement(2, "a");
        builder.AddAttribute(3, "class", "fa-header-brand");
        builder.AddAttribute(4, "href", BrandHref);

        if (!string.IsNullOrEmpty(BrandIconUrl))
        {
            builder.OpenElement(44, "img");
            builder.AddAttribute(45, "class", "fa-header-brand-icon");
            builder.AddAttribute(46, "src", BrandIconUrl);
            builder.AddAttribute(47, "alt", "");
            builder.CloseElement();
        }

        builder.AddContent(5, BrandText);
        builder.CloseElement();

        builder.CloseElement(); // .fa-header-left

        var effectiveNav = NavContent ?? NavButtons;
        if (effectiveNav is not null)
        {
            builder.OpenElement(48, "nav");
            builder.AddAttribute(49, "class", "fa-header-nav");
            builder.AddAttribute(50, "aria-label", "Header navigation");
            builder.AddContent(51, effectiveNav);
            builder.CloseElement(); // nav.fa-header-nav
        }

        builder.OpenElement(6, "div");
        builder.AddAttribute(7, "class", "fa-header-user");

        if (UseAvatarForm)
        {
            builder.OpenComponent<FaAvatarForm>(8);
            builder.AddComponentParameter(9, nameof(FaAvatarForm.IsAuthenticated), IsAuthenticated);
            builder.AddComponentParameter(10, nameof(FaAvatarForm.DisplayName), UserDisplayName);
            builder.AddComponentParameter(11, nameof(FaAvatarForm.ImageUrl), UserImageUrl);
            builder.AddComponentParameter(12, nameof(FaAvatarForm.Email), UserEmail);
            builder.AddComponentParameter(13, nameof(FaAvatarForm.ShowDisplayName), ShowUserNameInHeader);
            builder.AddComponentParameter(14, nameof(FaAvatarForm.AccountHref), AccountHref);
            builder.AddComponentParameter(15, nameof(FaAvatarForm.OnAccountClick), OnAccountClick);
            builder.AddComponentParameter(16, nameof(FaAvatarForm.AccountText), AccountText);
            builder.AddComponentParameter(17, nameof(FaAvatarForm.OnLogin), OnLogin);
            builder.AddComponentParameter(18, nameof(FaAvatarForm.OnLogout), OnLogout);
            builder.AddComponentParameter(19, nameof(FaAvatarForm.ChildContent), UserMenuContent);
            builder.CloseComponent();
        }
        else
        {
            builder.OpenComponent<FaThemeSwitcher>(8);
            builder.CloseComponent();

            if (IsAuthenticated)
            {
                builder.OpenComponent<FaAvatar>(9);
                builder.AddComponentParameter(10, nameof(FaAvatar.DisplayName), UserDisplayName);
                builder.AddComponentParameter(11, nameof(FaAvatar.ImageUrl), UserImageUrl);
                builder.CloseComponent();

                builder.OpenElement(12, "span");
                builder.AddAttribute(13, "class", "fa-header-username");
                builder.AddContent(14, UserDisplayName);
                builder.CloseElement();

                builder.OpenComponent<FaButton>(15);
                builder.AddComponentParameter(16, nameof(FaButton.Variant), FaButtonVariant.Secondary);
                builder.AddComponentParameter(17, nameof(FaButton.Size), FaSize.Small);
                builder.AddComponentParameter(18, nameof(FaButton.OnClick), EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, () => OnLogout.InvokeAsync()));
                builder.AddComponentParameter(19, nameof(FaButton.ChildContent), (RenderFragment)(b => b.AddContent(0, "Log out")));
                builder.CloseComponent();
            }
            else
            {
                builder.OpenComponent<FaButton>(20);
                builder.AddComponentParameter(21, nameof(FaButton.Variant), FaButtonVariant.Secondary);
                builder.AddComponentParameter(22, nameof(FaButton.Size), FaSize.Small);
                builder.AddComponentParameter(23, nameof(FaButton.OnClick), EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, () => OnLogin.InvokeAsync()));
                builder.AddComponentParameter(24, nameof(FaButton.ChildContent), (RenderFragment)(b => b.AddContent(0, "Log in")));
                builder.CloseComponent();
            }
        }

        builder.CloseElement();
        builder.CloseElement();
    }
}
