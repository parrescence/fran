using Fran.Components;
using Fran.Layout;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Fran.Templates;

/// <summary>
/// A 404 / error page template. By default, renders a clean, focused, chrome-free error screen
/// with a status code badge, descriptive title, explanation, and action links (e.g. Back to home).
/// Can also wrap inside <see cref="FaStandardShell"/> when <see cref="Standalone"/> is set to false.
/// </summary>
public sealed class FaNotFoundTemplate : ComponentBase
{
    [Parameter, EditorRequired] public string BrandText { get; set; } = "";
    [Parameter] public string BrandHref { get; set; } = "/";
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

    /// <summary>
    /// When true (default), renders a focused chrome-free full-viewport card (like <see cref="FaAuthTemplate"/>).
    /// When false, wraps inside <see cref="FaStandardShell"/>.
    /// </summary>
    [Parameter] public bool Standalone { get; set; } = true;

    /// <summary>HTTP or error status code, e.g. "404", "500", "403". Defaults to "404".</summary>
    [Parameter] public string StatusCode { get; set; } = "404";

    /// <summary>Page heading, e.g. "Page not found".</summary>
    [Parameter] public string Title { get; set; } = "Page not found";

    /// <summary>Description explaining why the resource was not found or what to do next.</summary>
    [Parameter] public string Description { get; set; } = "Sorry, we couldn't find the page you're looking for. It may have been moved, deleted, or never existed.";

    /// <summary>URL for the primary return action. Defaults to "/".</summary>
    [Parameter] public string HomeHref { get; set; } = "/";

    /// <summary>Label for the return action button. Defaults to "Back to home".</summary>
    [Parameter] public string HomeText { get; set; } = "Back to home";

    /// <summary>Custom action buttons or search box.</summary>
    [Parameter] public RenderFragment? Actions { get; set; }

    /// <summary>Additional content rendered inside the error card.</summary>
    [Parameter] public RenderFragment? ChildContent { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (Standalone)
        {
            RenderStandalone(builder);
        }
        else
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
            builder.AddComponentParameter(17, nameof(FaStandardShell.ChildContent), (RenderFragment)RenderCardContent);
            builder.CloseComponent();
        }
    }

    private void RenderStandalone(RenderTreeBuilder builder)
    {
        var seq = 0;
        builder.OpenElement(seq++, "div");
        builder.AddAttribute(seq++, "class", "fa-template-notfound-standalone");

        builder.OpenElement(seq++, "a");
        builder.AddAttribute(seq++, "class", "fa-template-notfound-brand");
        builder.AddAttribute(seq++, "href", BrandHref);

        if (!string.IsNullOrEmpty(BrandIconUrl))
        {
            builder.OpenElement(seq++, "img");
            builder.AddAttribute(seq++, "class", "fa-template-notfound-brand-icon");
            builder.AddAttribute(seq++, "src", BrandIconUrl);
            builder.AddAttribute(seq++, "alt", "");
            builder.CloseElement();
        }

        builder.AddContent(seq++, BrandText);
        builder.CloseElement();

        builder.OpenElement(seq++, "div");
        builder.AddAttribute(seq++, "class", "fa-template-notfound-wrap");

        builder.OpenComponent<FaCard>(seq++);
        builder.AddComponentParameter(seq++, nameof(FaCard.ChildContent), (RenderFragment)RenderCardContent);
        builder.CloseComponent();

        builder.CloseElement(); // .fa-template-notfound-wrap
        builder.CloseElement(); // .fa-template-notfound-standalone
    }

    private void RenderCardContent(RenderTreeBuilder builder)
    {
        var seq = 0;
        builder.OpenElement(seq++, "div");
        builder.AddAttribute(seq++, "class", "fa-template-notfound-card");

        if (!string.IsNullOrEmpty(StatusCode))
        {
            builder.OpenElement(seq++, "span");
            builder.AddAttribute(seq++, "class", "fa-template-notfound-code");
            builder.AddContent(seq++, StatusCode);
            builder.CloseElement();
        }

        builder.OpenElement(seq++, "h1");
        builder.AddAttribute(seq++, "class", "fa-template-notfound-title");
        builder.AddContent(seq++, Title);
        builder.CloseElement();

        if (!string.IsNullOrEmpty(Description))
        {
            builder.OpenElement(seq++, "p");
            builder.AddAttribute(seq++, "class", "fa-template-notfound-description");
            builder.AddContent(seq++, Description);
            builder.CloseElement();
        }

        if (Actions is not null)
        {
            builder.OpenElement(seq++, "div");
            builder.AddAttribute(seq++, "class", "fa-template-notfound-actions");
            builder.AddContent(seq++, Actions);
            builder.CloseElement();
        }
        else if (!string.IsNullOrEmpty(HomeHref))
        {
            builder.OpenElement(seq++, "div");
            builder.AddAttribute(seq++, "class", "fa-template-notfound-actions");

            builder.OpenComponent<FaButton>(seq++);
            builder.AddComponentParameter(seq++, nameof(FaButton.Variant), FaButtonVariant.Primary);
            builder.AddComponentParameter(seq++, nameof(FaButton.Href), HomeHref);
            builder.AddComponentParameter(seq++, nameof(FaButton.ChildContent), (RenderFragment)(b => b.AddContent(0, HomeText)));
            builder.CloseComponent();

            builder.CloseElement();
        }

        if (ChildContent is not null)
        {
            builder.AddContent(seq++, ChildContent);
        }

        builder.CloseElement(); // .fa-template-notfound-card
    }
}
