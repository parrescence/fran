using Bunit;
using Fran.Components;
using Microsoft.AspNetCore.Components;
using Xunit;

namespace Fran.Tests;

public class FaBookmarkNavTests : BunitContext
{
    [Fact]
    public void FaBookmarkNav_RendersItemsWithNumbersAndActiveState()
    {
        var items = new[]
        {
            new FaBookmarkItem("overview", "Overview", "01"),
            new FaBookmarkItem("architecture", "Architecture", "02", badge: "Core"),
            new FaBookmarkItem("roadmap", "Roadmap", "03")
        };

        var cut = Render<FaBookmarkNav>(p => p
            .Add(x => x.Title, "Contents")
            .Add(x => x.Items, items)
            .Add(x => x.ActiveId, "architecture")
            .Add(x => x.Sticky, true));

        // Nav element
        var nav = cut.Find("nav.fa-bookmark-nav");
        Assert.Contains("fa-bookmark-nav-sticky", nav.ClassName);

        // Header
        var title = cut.Find(".fa-bookmark-nav-title");
        Assert.Equal("Contents", title.TextContent.Trim());

        // Items
        var links = cut.FindAll(".fa-bookmark-nav-link");
        Assert.Equal(3, links.Count);

        // Check active link
        var activeLink = cut.Find(".fa-bookmark-nav-link-active");
        Assert.Contains("Architecture", activeLink.TextContent);
        Assert.Equal("true", activeLink.GetAttribute("aria-current"));

        // Check number and badge
        Assert.Contains("02", activeLink.TextContent);
        Assert.Contains("Core", cut.Find(".fa-bookmark-nav-badge").TextContent);
    }

    [Fact]
    public void FaBookmarkNav_InvokesItemClickCallback()
    {
        FaBookmarkItem? clicked = null;
        var items = new[]
        {
            new FaBookmarkItem("overview", "Overview", "01"),
            new FaBookmarkItem("team", "Team", "02")
        };

        var cut = Render<FaBookmarkNav>(p => p
            .Add(x => x.Items, items)
            .Add(x => x.OnItemClick, EventCallback.Factory.Create<FaBookmarkItem>(this, item => clicked = item)));

        var links = cut.FindAll(".fa-bookmark-nav-link");
        links[1].Click();

        Assert.NotNull(clicked);
        Assert.Equal("team", clicked.Id);
    }

    [Fact]
    public void FaBookmarkLink_RendersDeclarativelyInsideNav()
    {
        var cut = Render<FaBookmarkNav>(p => p
            .Add(x => x.Title, "Bookmarks")
            .Add(x => x.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenElement(0, "ol");
                builder.AddAttribute(1, "class", "fa-bookmark-nav-list");

                builder.OpenComponent<FaBookmarkLink>(2);
                builder.AddComponentParameter(3, nameof(FaBookmarkLink.Href), "#custom");
                builder.AddComponentParameter(4, nameof(FaBookmarkLink.Number), "§1");
                builder.AddComponentParameter(5, nameof(FaBookmarkLink.Text), "Custom Section");
                builder.AddComponentParameter(6, nameof(FaBookmarkLink.IsActive), true);
                builder.CloseComponent();

                builder.CloseElement();
            })));

        var link = cut.Find("a.fa-bookmark-nav-link-active");
        Assert.Contains("§1", link.TextContent);
        Assert.Contains("Custom Section", link.TextContent);
        Assert.Equal("#custom", link.GetAttribute("href"));
    }
}
