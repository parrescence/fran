using Fran.Rendering;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;

namespace Fran.Components;

/// <summary>
/// The standard "you need to sign in to view this page" gate shown when an
/// unauthenticated visitor hits a protected route — not the credential-entry page
/// itself (that's typically a hosted identity provider's own login page an app
/// redirects to, outside this library's reach), just the one screen every consumer
/// controls in between. Renders as a fixed-position card anchored via
/// <see cref="Position"/> rather than reserving a full-viewport block, so it can sit
/// over whatever's already on the page instead of shoving it out of the way.
/// FaSignInGate never performs the actual sign-in redirect itself —
/// <see cref="OnSignIn"/> is where the caller navigates to its own login route
/// (e.g. <c>"authentication/login?returnUrl=..."</c> for a Blazor WebAssembly app
/// using the built-in MSAL authentication handler).
/// </summary>
public sealed class FaSignInGate : ComponentBase
{
    [Parameter, EditorRequired] public string BrandText { get; set; } = "";
    [Parameter] public string BrandHref { get; set; } = "/";
    [Parameter] public string? BrandIconUrl { get; set; }
    [Parameter] public string Title { get; set; } = "Sign in required";
    [Parameter] public string Description { get; set; } = "You need to sign in to view this page.";
    [Parameter, EditorRequired] public EventCallback OnSignIn { get; set; }
    [Parameter] public string SignInText { get; set; } = "Sign In";

    /// <summary>Which of the 9 viewport anchors the card sits at. Defaults to top-center.</summary>
    [Parameter] public FaSignInGatePosition Position { get; set; } = FaSignInGatePosition.TopCenter;

    /// <summary>Small content below the card — a "Contact support" link, terms text, etc.</summary>
    [Parameter] public RenderFragment? FooterContent { get; set; }
    [Parameter] public string? CssClass { get; set; }

    private string PositionClass => Position switch
    {
        FaSignInGatePosition.TopLeft => "fa-sign-in-gate-top-left",
        FaSignInGatePosition.TopRight => "fa-sign-in-gate-top-right",
        FaSignInGatePosition.CenterLeft => "fa-sign-in-gate-center-left",
        FaSignInGatePosition.Center => "fa-sign-in-gate-center",
        FaSignInGatePosition.CenterRight => "fa-sign-in-gate-center-right",
        FaSignInGatePosition.BottomLeft => "fa-sign-in-gate-bottom-left",
        FaSignInGatePosition.BottomCenter => "fa-sign-in-gate-bottom-center",
        FaSignInGatePosition.BottomRight => "fa-sign-in-gate-bottom-right",
        _ => "fa-sign-in-gate-top-center"
    };

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "div");
        builder.AddAttribute(1, "class", CssClassNames.Combine("fa-sign-in-gate", PositionClass, CssClass));

        builder.OpenComponent<FaCard>(2);
        builder.AddComponentParameter(3, nameof(FaCard.CssClass), "fa-sign-in-gate-panel");
        builder.AddComponentParameter(4, nameof(FaCard.ChildContent), (RenderFragment)RenderCard);
        builder.CloseComponent();

        if (FooterContent is not null)
        {
            builder.OpenElement(5, "div");
            builder.AddAttribute(6, "class", "fa-sign-in-gate-footer");
            builder.AddContent(7, FooterContent);
            builder.CloseElement();
        }

        builder.CloseElement();
    }

    private void RenderCard(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "a");
        builder.AddAttribute(1, "class", "fa-sign-in-gate-brand");
        builder.AddAttribute(2, "href", BrandHref);

        if (!string.IsNullOrEmpty(BrandIconUrl))
        {
            builder.OpenElement(3, "img");
            builder.AddAttribute(4, "class", "fa-sign-in-gate-brand-icon");
            builder.AddAttribute(5, "src", BrandIconUrl);
            builder.AddAttribute(6, "alt", "");
            builder.CloseElement();
        }

        builder.AddContent(7, BrandText);
        builder.CloseElement();

        builder.OpenElement(8, "h2");
        builder.AddAttribute(9, "class", "fa-template-form-title");
        builder.AddContent(10, Title);
        builder.CloseElement();

        builder.OpenElement(11, "p");
        builder.AddAttribute(12, "class", "fa-sign-in-gate-message");
        builder.AddContent(13, Description);
        builder.CloseElement();

        builder.OpenComponent<FaButton>(14);
        builder.AddComponentParameter(15, nameof(FaButton.Variant), FaButtonVariant.Primary);
        builder.AddComponentParameter(16, nameof(FaButton.OnClick), EventCallback.Factory.Create<MouseEventArgs>(this, () => OnSignIn.InvokeAsync()));
        builder.AddComponentParameter(17, nameof(FaButton.ChildContent), (RenderFragment)(b => b.AddContent(0, SignInText)));
        builder.CloseComponent();
    }
}
