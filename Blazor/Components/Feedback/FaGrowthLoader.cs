using Fran.Rendering;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Fran.Components;

/// <summary>
/// A dynamic financial growth and market-trend chart loader — displays 5 sequential
/// ascending trend bars that surge and fluctuate with live market rhythm, paired with
/// an apex pulsing spark and floating currency symbol. Pure CSS keyframes, zero JS.
/// </summary>
public sealed class FaGrowthLoader : ComponentBase
{
    /// <summary>The color variant (Accent, Primary, Gold, Danger).</summary>
    [Parameter] public FaLoaderVariant Variant { get; set; } = FaLoaderVariant.Accent;

    /// <summary>The scale size of the loader (Small, Medium, Large).</summary>
    [Parameter] public FaSize Size { get; set; } = FaSize.Medium;

    /// <summary>The currency symbol displayed floating above the peak growth bar. Defaults to "$".</summary>
    [Parameter] public string CurrencySymbol { get; set; } = "$";

    /// <summary>Whether to display the floating currency symbol above the growth peak. Defaults to true.</summary>
    [Parameter] public bool ShowCurrency { get; set; } = true;

    /// <summary>Optional text message displayed below the growth loader.</summary>
    [Parameter] public string? Label { get; set; }

    /// <summary>Optional additional CSS class.</summary>
    [Parameter] public string? CssClass { get; set; }

    private string? VariantClass => Variant switch
    {
        FaLoaderVariant.Primary => "fa-loader-primary",
        FaLoaderVariant.Gold => "fa-loader-gold",
        FaLoaderVariant.Danger => "fa-loader-danger",
        _ => null
    };

    private string? SizeClass => Size switch
    {
        FaSize.Small or FaSize.XSmall => "fa-growth-sm",
        FaSize.Large or FaSize.XLarge => "fa-growth-lg",
        _ => null
    };

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        var seq = 0;
        builder.OpenElement(seq++, "span");
        builder.AddAttribute(seq++, "class", CssClassNames.Combine("fa-growth-loader", VariantClass, SizeClass, CssClass));
        builder.AddAttribute(seq++, "role", "status");
        builder.AddAttribute(seq++, "aria-label", Label ?? "Loading financial data");

        // The growth chart stage
        builder.OpenElement(seq++, "span");
        builder.AddAttribute(seq++, "class", "fa-growth-stage");

        // 5 animated financial trend bars
        for (int i = 0; i < 5; i++)
        {
            builder.OpenElement(seq++, "span");
            builder.AddAttribute(seq++, "class", "fa-growth-bar");
            builder.CloseElement();
        }

        // Apex spark
        builder.OpenElement(seq++, "span");
        builder.AddAttribute(seq++, "class", "fa-growth-spark");
        builder.CloseElement();

        // Floating currency symbol
        if (ShowCurrency && !string.IsNullOrEmpty(CurrencySymbol))
        {
            builder.OpenElement(seq++, "span");
            builder.AddAttribute(seq++, "class", "fa-growth-currency-symbol");
            builder.AddAttribute(seq++, "aria-hidden", "true");
            builder.AddContent(seq++, CurrencySymbol);
            builder.CloseElement();
        }

        builder.CloseElement(); // .fa-growth-stage

        // Optional status label
        if (!string.IsNullOrEmpty(Label))
        {
            builder.OpenElement(seq++, "span");
            builder.AddAttribute(seq++, "class", "fa-growth-label");
            builder.AddContent(seq++, Label);
            builder.CloseElement();
        }

        builder.CloseElement(); // .fa-growth-loader
    }
}
