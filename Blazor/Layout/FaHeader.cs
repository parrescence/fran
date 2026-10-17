using Fran.Components;
using Fran.Icons;
using Fran.Rendering;
using Fran.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Fran.Layout;

/// <summary>
/// Header bar — brand on the left, user name + avatar + login/logout at the far right.
/// Purely presentational: the consuming app supplies auth state and wires up the
/// login/logout callbacks (e.g. FinanceApp.Web's MainLayout, an example consumer, for
/// real wiring).
/// </summary>
public sealed class FaHeader : ComponentBase, IDisposable
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

    /// <summary>Optional secondary navigation row / subnavbar rendered as an attachment under the header.</summary>
    [Parameter] public RenderFragment? SubNavContent { get; set; }

    /// <summary>Alias for <see cref="SubNavContent"/> for convenience.</summary>
    [Parameter] public RenderFragment? SubNav { get; set; }

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

    /// <summary>
    /// When true and navigation content is present (<see cref="NavContent"/> or <see cref="NavButtons"/>)
    /// without a sidebar toggle, renders a hamburger button on mobile screens that toggles the header
    /// nav menu slide-down via window.faToggleNavMobile(). Defaults to true.
    /// </summary>
    [Parameter] public bool ShowNavToggle { get; set; } = true;

    [Inject] private IServiceProvider? Services { get; set; }
    private FaSubNavService? _subNavService;

    protected override void OnInitialized()
    {
        _subNavService = Services?.GetService(typeof(FaSubNavService)) as FaSubNavService;
        if (_subNavService is not null)
        {
            _subNavService.OnChange += OnSubNavChanged;
        }
    }

    private void OnSubNavChanged()
    {
        _ = InvokeAsync(StateHasChanged);
    }

    public void Dispose()
    {
        if (_subNavService is not null)
        {
            _subNavService.OnChange -= OnSubNavChanged;
        }
    }

    private string? PositionClass => Position switch
    {
        FaNavPosition.Sticky => "fa-header-sticky",
        FaNavPosition.Floating => "fa-header-floating",
        _ => null
    };

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        var effectiveNav = NavContent ?? NavButtons;
        var effectiveSubNav = SubNavContent ?? SubNav ?? _subNavService?.SubNavContent;
        var hasSubNav = effectiveSubNav is not null;

        int seq = 0;
        if (hasSubNav)
        {
            builder.OpenElement(seq++, "div");
            builder.AddAttribute(seq++, "class", CssClassNames.Combine("fa-header-group", PositionClass));
        }

        builder.OpenElement(seq++, "header");
        builder.AddAttribute(seq++, "class", CssClassNames.Combine("fa-header", !hasSubNav ? PositionClass : null, hasSubNav ? "fa-header-has-subnav" : null));

        builder.OpenElement(seq++, "div");
        builder.AddAttribute(seq++, "class", "fa-header-left");

        if (ShowSidebarToggle)
        {
            builder.OpenElement(seq++, "button");
            builder.AddAttribute(seq++, "type", "button");
            builder.AddAttribute(seq++, "class", "fa-header-menu-toggle fa-header-sidebar-toggle");
            builder.AddAttribute(seq++, "data-sidebar-mobile-toggle", true);
            builder.AddAttribute(seq++, "title", "Toggle menu");
            builder.AddAttribute(seq++, "aria-label", "Toggle menu");
            builder.AddAttribute(seq++, "aria-expanded", "false");
            builder.AddAttribute(seq++, "onclick", "faToggleSidebarMobile()");

            builder.OpenComponent<FaIcon>(seq++);
            builder.AddComponentParameter(seq++, nameof(FaIcon.Name), FaIconName.Menu);
            builder.AddComponentParameter(seq++, nameof(FaIcon.Color), FaIconColor.White);
            builder.AddComponentParameter(seq++, nameof(FaIcon.Size), 18);
            builder.CloseComponent();

            builder.CloseElement(); // .fa-header-menu-toggle
        }
        else if (ShowNavToggle && effectiveNav is not null)
        {
            builder.OpenElement(seq++, "button");
            builder.AddAttribute(seq++, "type", "button");
            builder.AddAttribute(seq++, "class", "fa-header-menu-toggle fa-header-nav-toggle");
            builder.AddAttribute(seq++, "data-nav-mobile-toggle", true);
            builder.AddAttribute(seq++, "title", "Toggle navigation");
            builder.AddAttribute(seq++, "aria-label", "Toggle navigation");
            builder.AddAttribute(seq++, "aria-expanded", "false");
            builder.AddAttribute(seq++, "onclick", "faToggleNavMobile()");

            builder.OpenComponent<FaIcon>(seq++);
            builder.AddComponentParameter(seq++, nameof(FaIcon.Name), FaIconName.Menu);
            builder.AddComponentParameter(seq++, nameof(FaIcon.Color), FaIconColor.White);
            builder.AddComponentParameter(seq++, nameof(FaIcon.Size), 18);
            builder.CloseComponent();

            builder.CloseElement(); // .fa-header-nav-toggle
        }

        builder.OpenElement(seq++, "a");
        builder.AddAttribute(seq++, "class", "fa-header-brand");
        builder.AddAttribute(seq++, "href", BrandHref);

        if (!string.IsNullOrEmpty(BrandIconUrl))
        {
            builder.OpenElement(seq++, "img");
            builder.AddAttribute(seq++, "class", "fa-header-brand-icon");
            builder.AddAttribute(seq++, "src", BrandIconUrl);
            builder.AddAttribute(seq++, "alt", "");
            builder.CloseElement();
        }

        builder.AddContent(seq++, BrandText);
        builder.CloseElement(); // .fa-header-brand

        builder.CloseElement(); // .fa-header-left

        if (effectiveNav is not null)
        {
            builder.OpenElement(seq++, "div");
            builder.AddAttribute(seq++, "class", "fa-header-collapse");

            builder.OpenElement(seq++, "nav");
            builder.AddAttribute(seq++, "class", "fa-header-nav");
            builder.AddAttribute(seq++, "aria-label", "Header navigation");
            builder.AddContent(seq++, effectiveNav);
            builder.CloseElement(); // nav.fa-header-nav

            builder.CloseElement(); // div.fa-header-collapse
        }

        builder.OpenElement(seq++, "div");
        builder.AddAttribute(seq++, "class", "fa-header-user");

        if (UseAvatarForm)
        {
            builder.OpenComponent<FaAvatarForm>(seq++);
            builder.AddComponentParameter(seq++, nameof(FaAvatarForm.IsAuthenticated), IsAuthenticated);
            builder.AddComponentParameter(seq++, nameof(FaAvatarForm.DisplayName), UserDisplayName);
            builder.AddComponentParameter(seq++, nameof(FaAvatarForm.ImageUrl), UserImageUrl);
            builder.AddComponentParameter(seq++, nameof(FaAvatarForm.Email), UserEmail);
            builder.AddComponentParameter(seq++, nameof(FaAvatarForm.ShowDisplayName), ShowUserNameInHeader);
            builder.AddComponentParameter(seq++, nameof(FaAvatarForm.AccountHref), AccountHref);
            builder.AddComponentParameter(seq++, nameof(FaAvatarForm.OnAccountClick), OnAccountClick);
            builder.AddComponentParameter(seq++, nameof(FaAvatarForm.AccountText), AccountText);
            builder.AddComponentParameter(seq++, nameof(FaAvatarForm.OnLogin), OnLogin);
            builder.AddComponentParameter(seq++, nameof(FaAvatarForm.OnLogout), OnLogout);
            builder.AddComponentParameter(seq++, nameof(FaAvatarForm.ChildContent), UserMenuContent);
            builder.CloseComponent();
        }
        else
        {
            builder.OpenComponent<FaThemeSwitcher>(seq++);
            builder.CloseComponent();

            if (IsAuthenticated)
            {
                builder.OpenComponent<FaAvatar>(seq++);
                builder.AddComponentParameter(seq++, nameof(FaAvatar.DisplayName), UserDisplayName);
                builder.AddComponentParameter(seq++, nameof(FaAvatar.ImageUrl), UserImageUrl);
                builder.CloseComponent();

                builder.OpenElement(seq++, "span");
                builder.AddAttribute(seq++, "class", "fa-header-username");
                builder.AddContent(seq++, UserDisplayName);
                builder.CloseElement();

                builder.OpenComponent<FaButton>(seq++);
                builder.AddComponentParameter(seq++, nameof(FaButton.Variant), FaButtonVariant.Secondary);
                builder.AddComponentParameter(seq++, nameof(FaButton.Size), FaSize.Small);
                builder.AddComponentParameter(seq++, nameof(FaButton.OnClick), EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, () => OnLogout.InvokeAsync()));
                builder.AddComponentParameter(seq++, nameof(FaButton.ChildContent), (RenderFragment)(b => b.AddContent(0, "Log out")));
                builder.CloseComponent();
            }
            else
            {
                builder.OpenComponent<FaButton>(seq++);
                builder.AddComponentParameter(seq++, nameof(FaButton.Variant), FaButtonVariant.Secondary);
                builder.AddComponentParameter(seq++, nameof(FaButton.Size), FaSize.Small);
                builder.AddComponentParameter(seq++, nameof(FaButton.OnClick), EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, () => OnLogin.InvokeAsync()));
                builder.AddComponentParameter(seq++, nameof(FaButton.ChildContent), (RenderFragment)(b => b.AddContent(0, "Log in")));
                builder.CloseComponent();
            }
        }

        builder.CloseElement(); // .fa-header-user

        builder.CloseElement(); // header

        if (hasSubNav)
        {
            builder.OpenElement(seq++, "div");
            builder.AddAttribute(seq++, "class", CssClassNames.Combine("fa-header-subnav-row", "fa-shell-subnav-row"));
            builder.AddContent(seq++, effectiveSubNav);
            builder.CloseElement(); // div.fa-header-subnav-row

            builder.CloseElement(); // div.fa-header-group
        }
    }
}

