using Bunit;
using Fran.Components;
using Fran.Layout;
using Microsoft.AspNetCore.Components;
using Xunit;

namespace Fran.Tests;

public class FaHeaderTests : BunitContext
{
    [Fact]
    public void DefaultHeader_RendersInlineThemeSwitcherAndUserArea()
    {
        var cut = Render<FaHeader>(p => p
            .Add(x => x.BrandText, "Test App")
            .Add(x => x.IsAuthenticated, true)
            .Add(x => x.UserDisplayName, "Jane Doe"));

        // Header brand
        var brand = cut.Find("a.fa-header-brand");
        Assert.Equal("Test App", brand.TextContent.Trim());

        // By default, renders the inline FaThemeSwitcher
        var themeSwitcher = cut.Find(".fa-theme-switcher");
        Assert.NotNull(themeSwitcher);

        // Username is rendered inline
        var username = cut.Find("span.fa-header-username");
        Assert.Equal("Jane Doe", username.TextContent.Trim());

        // FaAvatarForm is NOT rendered
        var avatarForm = cut.FindAll(".fa-avatar-form");
        Assert.Empty(avatarForm);
    }

    [Fact]
    public void UseAvatarForm_RendersFaAvatarFormInUserArea()
    {
        var cut = Render<FaHeader>(p => p
            .Add(x => x.BrandText, "Test App")
            .Add(x => x.IsAuthenticated, true)
            .Add(x => x.UserDisplayName, "Jane Doe")
            .Add(x => x.UseAvatarForm, true));

        // Inline FaThemeSwitcher is NOT rendered in the topbar
        var inlineThemeSwitcher = cut.FindAll(".fa-header-user > .fa-theme-switcher");
        Assert.Empty(inlineThemeSwitcher);

        // FaAvatarForm IS rendered
        var avatarForm = cut.Find(".fa-avatar-form");
        Assert.NotNull(avatarForm);

        // By default, username is not shown in the topbar trigger
        var triggerName = cut.FindAll(".fa-avatar-form-trigger-name");
        Assert.Empty(triggerName);
    }

    [Fact]
    public void UseAvatarForm_WithShowUserNameInHeader_RendersNameInTrigger()
    {
        var cut = Render<FaHeader>(p => p
            .Add(x => x.BrandText, "Test App")
            .Add(x => x.IsAuthenticated, true)
            .Add(x => x.UserDisplayName, "Jane Doe")
            .Add(x => x.UseAvatarForm, true)
            .Add(x => x.ShowUserNameInHeader, true));

        var triggerName = cut.Find(".fa-avatar-form-trigger-name");
        Assert.NotNull(triggerName);
        Assert.Equal("Jane Doe", triggerName.TextContent.Trim());
    }

    [Fact]
    public void NavContent_WithShowNavToggle_RendersNavMobileToggle()
    {
        RenderFragment nav = builder =>
        {
            builder.OpenElement(0, "a");
            builder.AddAttribute(1, "href", "/dashboard");
            builder.AddContent(2, "Dashboard");
            builder.CloseElement();
        };

        var cut = Render<FaHeader>(p => p
            .Add(x => x.BrandText, "Test App")
            .Add(x => x.NavContent, nav));

        var toggle = cut.Find("button.fa-header-nav-toggle");
        Assert.NotNull(toggle);
        Assert.Equal("faToggleNavMobile()", toggle.GetAttribute("onclick"));
        Assert.True(toggle.HasAttribute("data-nav-mobile-toggle"));

        var navElement = cut.Find("nav.fa-header-nav");
        Assert.NotNull(navElement);
        Assert.Contains("Dashboard", navElement.TextContent);
    }

    [Fact]
    public void NavContent_WithShowNavToggleFalse_DoesNotRenderNavMobileToggle()
    {
        RenderFragment nav = builder =>
        {
            builder.OpenElement(0, "a");
            builder.AddAttribute(1, "href", "/dashboard");
            builder.AddContent(2, "Dashboard");
            builder.CloseElement();
        };

        var cut = Render<FaHeader>(p => p
            .Add(x => x.BrandText, "Test App")
            .Add(x => x.NavContent, nav)
            .Add(x => x.ShowNavToggle, false));

        var toggles = cut.FindAll("button.fa-header-nav-toggle");
        Assert.Empty(toggles);

        var navElement = cut.Find("nav.fa-header-nav");
        Assert.NotNull(navElement);
    }

    [Fact]
    public void ShowSidebarToggle_TakesPrecedenceOverNavToggle()
    {
        RenderFragment nav = builder =>
        {
            builder.OpenElement(0, "a");
            builder.AddAttribute(1, "href", "/dashboard");
            builder.AddContent(2, "Dashboard");
            builder.CloseElement();
        };

        var cut = Render<FaHeader>(p => p
            .Add(x => x.BrandText, "Test App")
            .Add(x => x.NavContent, nav)
            .Add(x => x.ShowSidebarToggle, true));

        // Renders sidebar toggle
        var sidebarToggle = cut.Find("button.fa-header-sidebar-toggle");
        Assert.NotNull(sidebarToggle);
        Assert.Equal("faToggleSidebarMobile()", sidebarToggle.GetAttribute("onclick"));

        // Does NOT render nav toggle
        var navToggles = cut.FindAll("button.fa-header-nav-toggle");
        Assert.Empty(navToggles);
    }
}
