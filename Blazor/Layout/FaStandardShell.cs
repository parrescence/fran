using Fran.Rendering;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Fran.Layout;

/// <summary>
/// Template 1: header + content + footer, no sidebar. For pages that don't need app
/// navigation alongside them (landing/marketing pages, standalone flows).
/// </summary>
public sealed class FaStandardShell : ComponentBase
{
    [Parameter, EditorRequired] public string BrandText { get; set; } = "";
    [Parameter] public string BrandHref { get; set; } = "";

    /// <summary>Passed straight through to <see cref="FaHeader.BrandIconUrl"/>.</summary>
    [Parameter] public string? BrandIconUrl { get; set; }

    [Parameter] public bool IsAuthenticated { get; set; }
    [Parameter] public string? UserDisplayName { get; set; }
    [Parameter] public string? UserImageUrl { get; set; }
    [Parameter] public EventCallback OnLogin { get; set; }
    [Parameter] public EventCallback OnLogout { get; set; }

    /// <summary>Passed straight through to <see cref="FaHeader.UseAvatarForm"/>.</summary>
    [Parameter] public bool UseAvatarForm { get; set; }

    /// <summary>Passed straight through to <see cref="FaHeader.ShowUserNameInHeader"/>.</summary>
    [Parameter] public bool ShowUserNameInHeader { get; set; }

    /// <summary>Passed straight through to <see cref="FaHeader.UserEmail"/>.</summary>
    [Parameter] public string? UserEmail { get; set; }

    /// <summary>Passed straight through to <see cref="FaHeader.AccountHref"/>.</summary>
    [Parameter] public string? AccountHref { get; set; }

    /// <summary>Passed straight through to <see cref="FaHeader.OnAccountClick"/>.</summary>
    [Parameter] public EventCallback OnAccountClick { get; set; }

    /// <summary>Passed straight through to <see cref="FaHeader.AccountText"/>.</summary>
    [Parameter] public string AccountText { get; set; } = "Account settings";

    /// <summary>Passed straight through to <see cref="FaHeader.UserMenuContent"/>.</summary>
    [Parameter] public RenderFragment? UserMenuContent { get; set; }

    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public RenderFragment? FooterContent { get; set; }

    /// <summary>Passed straight through to <see cref="FaHeader.Position"/>.</summary>
    [Parameter] public FaNavPosition HeaderPosition { get; set; } = FaNavPosition.Standard;

    /// <summary>Passed straight through to <see cref="FaFooter.Position"/>.</summary>
    [Parameter] public FaNavPosition FooterPosition { get; set; } = FaNavPosition.Standard;

    /// <summary>
    /// Opt-in, off by default. Pins the header/footer to the viewport edges and lets
    /// only the page's own content (<see cref="ChildContent"/>, inside &lt;main&gt;)
    /// scroll internally, instead of the whole page growing past one viewport and
    /// scrolling as a single document (the default — fine for most content, but wrong
    /// for a dashboard-style page that wants the chrome pinned). See
    /// <c>_layout.scss</c>'s <c>.fa-shell-contained</c> for the mechanics.
    /// </summary>
    [Parameter] public bool ContainScroll { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "div");
        builder.AddAttribute(1, "class", CssClassNames.Combine("fa-shell", "fa-shell-standard", ContainScroll ? "fa-shell-contained" : null));

        builder.OpenComponent<FaHeader>(2);
        builder.AddComponentParameter(3, nameof(FaHeader.BrandText), BrandText);
        builder.AddComponentParameter(4, nameof(FaHeader.BrandHref), BrandHref);
        builder.AddComponentParameter(22, nameof(FaHeader.BrandIconUrl), BrandIconUrl);
        builder.AddComponentParameter(5, nameof(FaHeader.IsAuthenticated), IsAuthenticated);
        builder.AddComponentParameter(6, nameof(FaHeader.UserDisplayName), UserDisplayName);
        builder.AddComponentParameter(7, nameof(FaHeader.UserImageUrl), UserImageUrl);
        builder.AddComponentParameter(8, nameof(FaHeader.OnLogin), OnLogin);
        builder.AddComponentParameter(9, nameof(FaHeader.OnLogout), OnLogout);
        builder.AddComponentParameter(20, nameof(FaHeader.Position), HeaderPosition);
        builder.AddComponentParameter(23, nameof(FaHeader.UseAvatarForm), UseAvatarForm);
        builder.AddComponentParameter(24, nameof(FaHeader.ShowUserNameInHeader), ShowUserNameInHeader);
        builder.AddComponentParameter(25, nameof(FaHeader.UserEmail), UserEmail);
        builder.AddComponentParameter(26, nameof(FaHeader.AccountHref), AccountHref);
        builder.AddComponentParameter(27, nameof(FaHeader.OnAccountClick), OnAccountClick);
        builder.AddComponentParameter(28, nameof(FaHeader.AccountText), AccountText);
        builder.AddComponentParameter(29, nameof(FaHeader.UserMenuContent), UserMenuContent);
        builder.CloseComponent();

        builder.OpenElement(10, "main");
        builder.AddAttribute(11, "class", "fa-shell-main");
        builder.AddContent(12, ChildContent);
        builder.CloseElement();

        builder.OpenComponent<FaFooter>(13);
        builder.AddComponentParameter(14, nameof(FaFooter.BrandText), BrandText);
        builder.AddComponentParameter(15, nameof(FaFooter.ChildContent), FooterContent);
        builder.AddComponentParameter(21, nameof(FaFooter.Position), FooterPosition);
        builder.CloseComponent();

        builder.CloseElement();
    }
}
