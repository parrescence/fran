using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bunit;
using Fran.Components;
using Microsoft.AspNetCore.Components.Web;
using Xunit;

namespace Fran.Tests;

public class FaTabsTests : BunitContext
{
    private readonly IReadOnlyList<(string Title, string Value)> _sampleTabs = new[]
    {
        ("Account", "account"),
        ("Security", "security"),
        ("Notifications", "notifications")
    };

    [Fact]
    public void DefaultStyle_RendersUnderlineClassAndRoleTablist()
    {
        var active = "account";
        var cut = Render<FaTabs<string>>(p => p
            .Add(x => x.Options, _sampleTabs)
            .Add(x => x.Value, active));

        var tablist = cut.Find("div.fa-tabs");
        Assert.Contains("fa-tabs-underline", tablist.ClassName);
        Assert.Equal("tablist", tablist.GetAttribute("role"));

        var tabs = cut.FindAll("button.fa-tabs-tab");
        Assert.Equal(3, tabs.Count);
        Assert.Contains("fa-tabs-tab-active", tabs[0].ClassName);
        Assert.Equal("true", tabs[0].GetAttribute("aria-selected"));
        Assert.Equal("0", tabs[0].GetAttribute("tabindex"));

        Assert.DoesNotContain("fa-tabs-tab-active", tabs[1].ClassName);
        Assert.Equal("false", tabs[1].GetAttribute("aria-selected"));
        Assert.Equal("-1", tabs[1].GetAttribute("tabindex"));
    }

    [Fact]
    public void FolderStyle_RendersFaTabsFolderClass()
    {
        var cut = Render<FaTabs<string>>(p => p
            .Add(x => x.Options, _sampleTabs)
            .Add(x => x.Value, "security")
            .Add(x => x.Style, FaTabStyle.Folder));

        var tablist = cut.Find("div.fa-tabs");
        Assert.Contains("fa-tabs-folder", tablist.ClassName);

        var tabs = cut.FindAll("button.fa-tabs-tab");
        Assert.Contains("fa-tabs-tab-active", tabs[1].ClassName);
        Assert.Equal("true", tabs[1].GetAttribute("aria-selected"));
    }

    [Fact]
    public void SlideStyle_RendersFaTabsSlideClass()
    {
        var cut = Render<FaTabs<string>>(p => p
            .Add(x => x.Options, _sampleTabs)
            .Add(x => x.Value, "notifications")
            .Add(x => x.Style, FaTabStyle.Slide));

        var tablist = cut.Find("div.fa-tabs");
        Assert.Contains("fa-tabs-slide", tablist.ClassName);

        var activeTab = cut.Find("button.fa-tabs-tab.fa-tabs-tab-active");
        Assert.Equal("Notifications", activeTab.TextContent.Trim());
        Assert.Equal("true", activeTab.GetAttribute("aria-selected"));
    }

    [Fact]
    public void WheelStyle_RendersStepperButtonsAndTrack()
    {
        var cut = Render<FaTabs<string>>(p => p
            .Add(x => x.Options, _sampleTabs)
            .Add(x => x.Value, "account")
            .Add(x => x.Style, FaTabStyle.Wheel));

        var container = cut.Find("div.fa-tabs-wheel");
        Assert.NotNull(container);

        var prevBtn = cut.Find("button.fa-tabs-wheel-prev");
        var nextBtn = cut.Find("button.fa-tabs-wheel-next");
        Assert.NotNull(prevBtn);
        Assert.NotNull(nextBtn);

        // Since first tab is selected, prev should be disabled
        Assert.True(prevBtn.HasAttribute("disabled"));
        Assert.False(nextBtn.HasAttribute("disabled"));

        var track = cut.Find(".fa-tabs-wheel-track");
        Assert.Equal("tablist", track.GetAttribute("role"));
    }

    [Fact]
    public async Task WheelStyle_StepperNextButton_AdvancesSelection()
    {
        string selected = "account";
        var cut = Render<FaTabs<string>>(p => p
            .Add(x => x.Options, _sampleTabs)
            .Add(x => x.Value, selected)
            .Add(x => x.ValueChanged, v => selected = v)
            .Add(x => x.Style, FaTabStyle.Wheel));

        var nextBtn = cut.Find("button.fa-tabs-wheel-next");
        await nextBtn.ClickAsync(new MouseEventArgs());

        Assert.Equal("security", selected);
    }

    [Fact]
    public async Task WheelEvent_ScrollsSelection()
    {
        string selected = "account";
        var cut = Render<FaTabs<string>>(p => p
            .Add(x => x.Options, _sampleTabs)
            .Add(x => x.Value, selected)
            .Add(x => x.ValueChanged, v => selected = v)
            .Add(x => x.Style, FaTabStyle.Wheel));

        var container = cut.Find("div.fa-tabs-wheel");
        await container.TriggerEventAsync("onwheel", new WheelEventArgs { DeltaY = 100 });

        Assert.Equal("security", selected);
    }

    [Fact]
    public void ChildContent_RendersFaTabsSectionAndPanel()
    {
        var cut = Render<FaTabs<string>>(p => p
            .Add(x => x.Options, _sampleTabs)
            .Add(x => x.Value, "account")
            .Add(x => x.Style, FaTabStyle.Folder)
            .AddChildContent("<p class=\"custom-panel\">Account Details Panel</p>"));

        var section = cut.Find("div.fa-tabs-section");
        Assert.Contains("fa-tabs-section-folder", section.ClassName);

        var panel = cut.Find("div.fa-tabs-panel");
        Assert.Equal("tabpanel", panel.GetAttribute("role"));
        Assert.Contains("Account Details Panel", panel.TextContent);
    }

    [Fact]
    public async Task KeyboardArrows_NavigateTabs()
    {
        string selected = "account";
        var cut = Render<FaTabs<string>>(p => p
            .Add(x => x.Options, _sampleTabs)
            .Add(x => x.Value, selected)
            .Add(x => x.ValueChanged, v => selected = v)
            .Add(x => x.Style, FaTabStyle.Folder));

        var tablist = cut.Find("div.fa-tabs");
        await tablist.KeyDownAsync(new KeyboardEventArgs { Key = "ArrowRight" });

        Assert.Equal("security", selected);

        await tablist.KeyDownAsync(new KeyboardEventArgs { Key = "End" });
        Assert.Equal("notifications", selected);

        await tablist.KeyDownAsync(new KeyboardEventArgs { Key = "Home" });
        Assert.Equal("account", selected);
    }
}
