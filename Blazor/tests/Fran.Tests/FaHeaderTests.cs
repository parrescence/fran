using Bunit;
using Fran.Components;
using Fran.Layout;
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
}
