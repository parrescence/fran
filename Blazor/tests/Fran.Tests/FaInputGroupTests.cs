using Bunit;
using Fran.Components;
using Microsoft.AspNetCore.Components;
using Xunit;

namespace Fran.Tests;

public class FaInputGroupTests : BunitContext
{
    [Fact]
    public void Default_RendersGroupRoleAndInputGroupClass()
    {
        var cut = Render<FaInputGroup>(p => p
            .AddChildContent("<input type=\"text\" class=\"fa-input\" />")
            .AddChildContent("<button type=\"button\" class=\"fa-btn fa-btn-primary\">Search</button>"));

        var group = cut.Find("div.fa-input-group");
        Assert.NotNull(group);
        Assert.Equal("group", group.GetAttribute("role"));
        Assert.Contains("<input", cut.Markup);
        Assert.Contains("<button", cut.Markup);
    }

    [Fact]
    public void InputInBetweenButtons_RendersButtonsOnBothSides()
    {
        var cut = Render<FaInputGroup>(p => p
            .Add(x => x.AriaLabel, "Quantity Stepper")
            .AddChildContent("<button type=\"button\" class=\"fa-btn fa-btn-secondary\" id=\"dec\">-</button>")
            .AddChildContent("<input type=\"number\" class=\"fa-input\" value=\"1\" />")
            .AddChildContent("<button type=\"button\" class=\"fa-btn fa-btn-secondary\" id=\"inc\">+</button>"));

        var group = cut.Find("div.fa-input-group");
        Assert.Equal("Quantity Stepper", group.GetAttribute("aria-label"));

        var decBtn = cut.Find("#dec");
        var incBtn = cut.Find("#inc");
        Assert.NotNull(decBtn);
        Assert.NotNull(incBtn);
    }

    [Fact]
    public void PillModifier_AddsFaInputGroupPillClass()
    {
        var cut = Render<FaInputGroup>(p => p
            .Add(x => x.Pill, true)
            .AddChildContent("<input type=\"text\" class=\"fa-input\" />"));

        var group = cut.Find("div.fa-input-group");
        Assert.Contains("fa-input-group-pill", group.ClassName);
    }

    [Fact]
    public void InlineModifier_AddsFaInputGroupInlineClass()
    {
        var cut = Render<FaInputGroup>(p => p
            .Add(x => x.Inline, true)
            .AddChildContent("<input type=\"text\" class=\"fa-input\" />"));

        var group = cut.Find("div.fa-input-group");
        Assert.Contains("fa-input-group-inline", group.ClassName);
    }

    [Theory]
    [InlineData(FaSize.XSmall, "fa-input-group-xs")]
    [InlineData(FaSize.Small, "fa-input-group-sm")]
    [InlineData(FaSize.Large, "fa-input-group-lg")]
    [InlineData(FaSize.XLarge, "fa-input-group-xl")]
    public void Size_AddsCorrespondingSizeClass(FaSize size, string expectedClass)
    {
        var cut = Render<FaInputGroup>(p => p
            .Add(x => x.Size, size)
            .AddChildContent("<input type=\"text\" class=\"fa-input\" />"));

        var group = cut.Find("div.fa-input-group");
        Assert.Contains(expectedClass, group.ClassName);
    }

    [Fact]
    public void WithLabelAndHelpText_WrapsInFaFieldContainer()
    {
        var cut = Render<FaInputGroup>(p => p
            .Add(x => x.Label, "Discount Code")
            .Add(x => x.HelpText, "Enter a valid promo or coupon code")
            .AddChildContent("<input type=\"text\" class=\"fa-input\" />")
            .AddChildContent("<button type=\"button\" class=\"fa-btn fa-btn-primary\">Apply</button>"));

        var field = cut.Find("div.fa-field");
        Assert.NotNull(field);

        var label = cut.Find("label.fa-label");
        Assert.Equal("Discount Code", label.TextContent.Trim());

        var help = cut.Find("div.fa-help-text");
        Assert.Equal("Enter a valid promo or coupon code", help.TextContent.Trim());
    }

    [Fact]
    public void PrependAndAppendSlots_RenderInCorrectOrder()
    {
        var cut = Render<FaInputGroup>(p => p
            .Add(x => x.Prepend, (RenderFragment)(b => b.AddMarkupContent(0, "<span id=\"pre\">Prepend</span>")))
            .Add(x => x.ChildContent, (RenderFragment)(b => b.AddMarkupContent(0, "<input type=\"text\" class=\"fa-input\" id=\"main\" />")))
            .Add(x => x.Append, (RenderFragment)(b => b.AddMarkupContent(0, "<span id=\"app\">Append</span>"))));

        var markup = cut.Markup;
        var preIdx = markup.IndexOf("id=\"pre\"", StringComparison.Ordinal);
        var mainIdx = markup.IndexOf("id=\"main\"", StringComparison.Ordinal);
        var appIdx = markup.IndexOf("id=\"app\"", StringComparison.Ordinal);

        Assert.True(preIdx < mainIdx);
        Assert.True(mainIdx < appIdx);
    }

    [Fact]
    public void FaInputGroupText_RendersAddonSpanWithText()
    {
        var cut = Render<FaInputGroupText>(p => p
            .Add(x => x.Text, "$"));

        var span = cut.Find("span.fa-input-group-text");
        Assert.Equal("$", span.TextContent.Trim());
    }

    [Fact]
    public void FaInputGroupText_RendersChildContent()
    {
        var cut = Render<FaInputGroupText>(p => p
            .AddChildContent("<strong>USD</strong>"));

        var span = cut.Find("span.fa-input-group-text");
        Assert.Contains("<strong>USD</strong>", span.InnerHtml);
    }
}
