using Fran.Rendering;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Fran.Layout;

/// <summary>
/// Lighter footer band, closes out both page templates.
/// </summary>
public sealed class FaFooter : ComponentBase
{
    [Parameter, EditorRequired] public string BrandText { get; set; } = "";
    [Parameter] public RenderFragment? ChildContent { get; set; }

    /// <summary>Optional navigation buttons or links rendered in the footer.</summary>
    [Parameter] public RenderFragment? NavContent { get; set; }

    /// <summary>Alias for <see cref="NavContent"/> for convenience.</summary>
    [Parameter] public RenderFragment? NavButtons { get; set; }

    /// <summary>Whether the footer scrolls away with the page (default) or stays pinned to the bottom.</summary>
    [Parameter] public FaNavPosition Position { get; set; } = FaNavPosition.Standard;

    private string? PositionClass => Position switch
    {
        FaNavPosition.Sticky => "fa-footer-sticky",
        FaNavPosition.Floating => "fa-footer-floating",
        _ => null
    };

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        var effectiveNav = NavContent ?? NavButtons;
        var hasNav = effectiveNav is not null;

        builder.OpenElement(0, "footer");
        builder.AddAttribute(1, "class", CssClassNames.Combine("fa-footer", PositionClass, hasNav ? "fa-footer-with-nav" : null));

        if (ChildContent is not null)
        {
            builder.AddContent(2, ChildContent);
        }
        else
        {
            builder.OpenElement(3, "div");
            builder.AddAttribute(4, "class", "fa-footer-brand");
            builder.OpenElement(5, "span");
            builder.AddContent(6, $"© {DateTime.UtcNow.Year} {BrandText}");
            builder.CloseElement();
            builder.CloseElement(); // .fa-footer-brand
        }

        if (effectiveNav is not null)
        {
            builder.OpenElement(7, "nav");
            builder.AddAttribute(8, "class", "fa-footer-nav");
            builder.AddAttribute(9, "aria-label", "Footer navigation");
            builder.AddContent(10, effectiveNav);
            builder.CloseElement(); // nav.fa-footer-nav
        }

        builder.CloseElement();
    }
}
