using Fran.Rendering;
using Fran.Templates;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;

namespace Fran.Components;

/// <summary>
/// The standard "you need to sign in to view this page" gate shown when an
/// unauthenticated visitor hits a protected route — not the credential-entry page
/// itself (that's typically a hosted identity provider's own login page an app
/// redirects to, outside this library's reach), just the one screen every consumer
/// controls in between. Wraps <see cref="FaAuthTemplate"/> so every app's gate
/// shares the same chrome-free centered-card structure and copy instead of each
/// app hand-rolling its own. FaSignInGate never performs the actual sign-in
/// redirect itself — <see cref="OnSignIn"/> is where the caller navigates to its
/// own login route (e.g. <c>"authentication/login?returnUrl=..."</c> for a
/// Blazor WebAssembly app using the built-in MSAL authentication handler).
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
    /// <summary>Small content below the card — see <see cref="FaAuthTemplate.FooterContent"/>.</summary>
    [Parameter] public RenderFragment? FooterContent { get; set; }
    [Parameter] public string? CssClass { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenComponent<FaAuthTemplate>(0);
        builder.AddComponentParameter(1, nameof(FaAuthTemplate.BrandText), BrandText);
        builder.AddComponentParameter(2, nameof(FaAuthTemplate.BrandHref), BrandHref);
        builder.AddComponentParameter(3, nameof(FaAuthTemplate.BrandIconUrl), BrandIconUrl);
        builder.AddComponentParameter(4, nameof(FaAuthTemplate.Title), Title);
        builder.AddComponentParameter(5, nameof(FaAuthTemplate.ChildContent), (RenderFragment)RenderCard);
        builder.AddComponentParameter(6, nameof(FaAuthTemplate.FooterContent), FooterContent);
        builder.CloseComponent();
    }

    private void RenderCard(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "div");
        builder.AddAttribute(1, "class", CssClassNames.Combine("fa-form fa-sign-in-gate", CssClass));

        builder.OpenElement(2, "p");
        builder.AddAttribute(3, "class", "fa-sign-in-gate-message");
        builder.AddContent(4, Description);
        builder.CloseElement();

        builder.OpenComponent<FaButton>(5);
        builder.AddComponentParameter(6, nameof(FaButton.Variant), FaButtonVariant.Primary);
        builder.AddComponentParameter(7, nameof(FaButton.OnClick), EventCallback.Factory.Create<MouseEventArgs>(this, () => OnSignIn.InvokeAsync()));
        builder.AddComponentParameter(8, nameof(FaButton.ChildContent), (RenderFragment)(b => b.AddContent(0, SignInText)));
        builder.CloseComponent();

        builder.CloseElement();
    }
}
