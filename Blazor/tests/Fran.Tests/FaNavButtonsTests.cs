using Bunit;
using Fran.Components;
using Fran.Layout;
using Microsoft.AspNetCore.Components;
using Xunit;

namespace Fran.Tests;

public class FaNavButtonsTests : BunitContext
{
    [Fact]
    public void FaHeader_RendersNavContent_WhenProvided()
    {
        var cut = Render<FaHeader>(p => p
            .Add(x => x.BrandText, "Test App")
            .Add(x => x.NavContent, (RenderFragment)(builder =>
            {
                builder.OpenElement(0, "a");
                builder.AddAttribute(1, "href", "/docs");
                builder.AddContent(2, "Docs");
                builder.CloseElement();
            })));

        var nav = cut.Find("nav.fa-header-nav");
        Assert.NotNull(nav);
        Assert.Contains("Docs", nav.TextContent);
    }

    [Fact]
    public void FaSidebar_RendersHeaderAndFooterActions_WhenProvided()
    {
        var cut = Render<FaSidebar>(p => p
            .Add(x => x.HeaderActions, (RenderFragment)(builder =>
            {
                builder.OpenElement(0, "button");
                builder.AddContent(1, "New Item");
                builder.CloseElement();
            }))
            .Add(x => x.FooterActions, (RenderFragment)(builder =>
            {
                builder.OpenElement(0, "button");
                builder.AddContent(1, "Settings");
                builder.CloseElement();
            }))
            .Add(x => x.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenElement(0, "a");
                builder.AddContent(1, "Dashboard");
                builder.CloseElement();
            })));

        var top = cut.Find(".fa-sidebar-actions-top");
        Assert.Contains("New Item", top.TextContent);

        var bottom = cut.Find(".fa-sidebar-actions-bottom");
        Assert.Contains("Settings", bottom.TextContent);

        var scroll = cut.Find(".fa-sidebar-scroll");
        Assert.Contains("Dashboard", scroll.TextContent);
    }

    [Fact]
    public void FaFooter_RendersNavContent_WhenProvided()
    {
        var cut = Render<FaFooter>(p => p
            .Add(x => x.BrandText, "Test App")
            .Add(x => x.NavContent, (RenderFragment)(builder =>
            {
                builder.OpenElement(0, "a");
                builder.AddAttribute(1, "href", "/privacy");
                builder.AddContent(2, "Privacy");
                builder.CloseElement();
            })));

        var footer = cut.Find("footer.fa-footer");
        Assert.Contains("fa-footer-with-nav", footer.ClassName);

        var nav = cut.Find("nav.fa-footer-nav");
        Assert.NotNull(nav);
        Assert.Contains("Privacy", nav.TextContent);

        var brand = cut.Find(".fa-footer-brand");
        Assert.Contains("Test App", brand.TextContent);
    }

    [Fact]
    public void FaStandardShell_PassesThroughHeaderNavAndFooterNav()
    {
        var cut = Render<FaStandardShell>(p => p
            .Add(x => x.BrandText, "Standard App")
            .Add(x => x.HeaderNav, (RenderFragment)(builder =>
            {
                builder.OpenElement(0, "span");
                builder.AddContent(1, "TopNav");
                builder.CloseElement();
            }))
            .Add(x => x.FooterNav, (RenderFragment)(builder =>
            {
                builder.OpenElement(0, "span");
                builder.AddContent(1, "BottomNav");
                builder.CloseElement();
            })));

        Assert.NotNull(cut.Find("nav.fa-header-nav"));
        Assert.Contains("TopNav", cut.Find("nav.fa-header-nav").TextContent);

        Assert.NotNull(cut.Find("nav.fa-footer-nav"));
        Assert.Contains("BottomNav", cut.Find("nav.fa-footer-nav").TextContent);
    }

    [Fact]
    public void FaSidebarShell_PassesThroughNavActions()
    {
        var cut = Render<FaSidebarShell>(p => p
            .Add(x => x.BrandText, "Sidebar App")
            .Add(x => x.HeaderNav, (RenderFragment)(builder => builder.AddContent(0, "TopNav")))
            .Add(x => x.FooterNav, (RenderFragment)(builder => builder.AddContent(0, "BottomNav")))
            .Add(x => x.SidebarHeaderActions, (RenderFragment)(builder => builder.AddContent(0, "SidebarTop")))
            .Add(x => x.SidebarFooterActions, (RenderFragment)(builder => builder.AddContent(0, "SidebarBottom"))));

        Assert.Contains("TopNav", cut.Find("nav.fa-header-nav").TextContent);
        Assert.Contains("BottomNav", cut.Find("nav.fa-footer-nav").TextContent);
        Assert.Contains("SidebarTop", cut.Find(".fa-sidebar-actions-top").TextContent);
        Assert.Contains("SidebarBottom", cut.Find(".fa-sidebar-actions-bottom").TextContent);
    }
}
