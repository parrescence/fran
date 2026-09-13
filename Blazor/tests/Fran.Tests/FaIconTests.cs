using System;
using Bunit;
using Fran.Icons;
using Xunit;

namespace Fran.Tests;

public class FaIconTests : BunitContext
{
    [Theory]
    [InlineData(FaIconName.Document)]
    [InlineData(FaIconName.DocumentSearch)]
    [InlineData(FaIconName.Stopwatch)]
    [InlineData(FaIconName.Trophy)]
    [InlineData(FaIconName.Bolt)]
    [InlineData(FaIconName.Upload)]
    [InlineData(FaIconName.Search)]
    public void FaIcon_RendersSvg_ForIconName(FaIconName iconName)
    {
        var cut = Render<FaIcon>(p => p
            .Add(x => x.Name, iconName)
            .Add(x => x.Size, 24)
            .Add(x => x.Color, FaIconColor.Black));

        var svg = cut.Find("svg");
        Assert.NotNull(svg);
        Assert.Contains("fa-icon", svg.GetAttribute("class"));
        Assert.Contains("fa-icon-black", svg.GetAttribute("class"));
        Assert.Equal("0 0 24 24", svg.GetAttribute("viewBox"));
    }

    [Fact]
    public void FaIcon_AllEnumValues_RenderWithoutException()
    {
        foreach (FaIconName icon in Enum.GetValues<FaIconName>())
        {
            var cut = Render<FaIcon>(p => p.Add(x => x.Name, icon));
            Assert.NotNull(cut.Find("svg"));
        }
    }
}
