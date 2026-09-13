using Bunit;
using Fran.Components;
using Xunit;

namespace Fran.Tests;

public class FaFontStyleSwitcherTests : BunitContext
{
    [Fact]
    public void FaFontStyleSwitcher_DropdownMode_RendersSelectWithAllOptions()
    {
        var cut = Render<FaFontStyleSwitcher>(p => p.Add(x => x.AsDropdown, true));

        var select = cut.Find("select.fa-font-style-select");
        Assert.NotNull(select);
        Assert.True(select.HasAttribute("data-font-style-select"));
        Assert.Equal("faSetFontStyle(this.value)", select.GetAttribute("onchange"));

        var options = select.QuerySelectorAll("option");
        Assert.Equal(10, options.Length);
        Assert.Contains(options, o => o.GetAttribute("value") == "flow");
        Assert.Contains(options, o => o.GetAttribute("value") == "dos");
        Assert.Contains(options, o => o.GetAttribute("value") == "cli");
        Assert.Contains(options, o => o.GetAttribute("value") == "elementary");
        Assert.Contains(options, o => o.GetAttribute("value") == "college");
        Assert.Contains(options, o => o.GetAttribute("value") == "flowing");
        Assert.Contains(options, o => o.GetAttribute("value") == "water");
        Assert.Contains(options, o => o.GetAttribute("value") == "rock");
        Assert.Contains(options, o => o.GetAttribute("value") == "comical");
        Assert.Contains(options, o => o.GetAttribute("value") == "contrasting");
    }

    [Fact]
    public void FaFontStyleSwitcher_ButtonsMode_RendersAllButtons()
    {
        var cut = Render<FaFontStyleSwitcher>(p => p.Add(x => x.AsDropdown, false));

        var container = cut.Find("div.fa-font-style-switcher");
        Assert.NotNull(container);

        var buttons = container.QuerySelectorAll("button.fa-font-style-btn");
        Assert.Equal(10, buttons.Length);
        Assert.Contains(buttons, b => b.GetAttribute("data-font-style-btn") == "dos" && b.GetAttribute("onclick") == "faSetFontStyle('dos')");
        Assert.Contains(buttons, b => b.GetAttribute("data-font-style-btn") == "contrasting" && b.GetAttribute("onclick") == "faSetFontStyle('contrasting')");
    }
}
