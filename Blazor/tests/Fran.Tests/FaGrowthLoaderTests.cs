using Bunit;
using Fran.Components;
using Fran.Rendering;
using Microsoft.AspNetCore.Components;
using Xunit;

namespace Fran.Tests;

public class FaGrowthLoaderTests : BunitContext
{
    [Fact]
    public void FaGrowthLoader_Default_RendersFiveBarsExclusivelyWithoutYellowCircleOrCurrency()
    {
        var cut = Render<FaGrowthLoader>();

        var root = cut.Find(".fa-growth-loader");
        Assert.NotNull(root);
        Assert.Equal("status", root.GetAttribute("role"));
        Assert.Equal("Loading financial data", root.GetAttribute("aria-label"));

        var stage = cut.Find(".fa-growth-stage");
        Assert.NotNull(stage);

        var bars = cut.FindAll(".fa-growth-bar");
        Assert.Equal(5, bars.Count);

        // No yellow circle spark
        Assert.Empty(cut.FindAll(".fa-growth-spark"));

        // No currency symbol by default
        Assert.Empty(cut.FindAll(".fa-growth-currency-symbol"));
    }

    [Fact]
    public void FaGrowthLoader_WithVariantAndSize_AppliesCorrectClasses()
    {
        var cut = Render<FaGrowthLoader>(p => p
            .Add(x => x.Variant, FaLoaderVariant.Gold)
            .Add(x => x.Size, FaSize.Large)
            .Add(x => x.CssClass, "custom-market-loader"));

        var root = cut.Find(".fa-growth-loader");
        Assert.Contains("fa-loader-gold", root.ClassName);
        Assert.Contains("fa-growth-lg", root.ClassName);
        Assert.Contains("custom-market-loader", root.ClassName);
    }

    [Fact]
    public void FaGrowthLoader_CustomCurrencyAndLabel_RendersLabelAndAria()
    {
        var cut = Render<FaGrowthLoader>(p => p
            .Add(x => x.ShowCurrency, true)
            .Add(x => x.CurrencySymbol, "£")
            .Add(x => x.Label, "Syncing market ledgers..."));

        var root = cut.Find(".fa-growth-loader");
        Assert.Equal("Syncing market ledgers...", root.GetAttribute("aria-label"));

        var currency = cut.Find(".fa-growth-currency-symbol");
        Assert.Equal("£", currency.TextContent.Trim());

        var label = cut.Find(".fa-growth-label");
        Assert.Equal("Syncing market ledgers...", label.TextContent.Trim());
    }

    [Fact]
    public void FaGrowthLoader_ShowCurrencyFalse_HidesCurrencySymbol()
    {
        var cut = Render<FaGrowthLoader>(p => p
            .Add(x => x.ShowCurrency, false));

        Assert.Empty(cut.FindAll(".fa-growth-currency-symbol"));
    }
}
