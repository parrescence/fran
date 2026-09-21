using Bunit;
using Fran.Templates;
using Microsoft.AspNetCore.Components;
using Xunit;

namespace Fran.Tests;

public class FaCommandCenterTemplateTests : BunitContext
{
    [Fact]
    public void FaCommandCenterTemplate_UnauthenticatedGated_RendersAuthOverlay()
    {
        var cut = Render<FaCommandCenterTemplate>(p => p
            .Add(x => x.BrandText, "Parrescence")
            .Add(x => x.Subtitle, "Cloud Command Center")
            .Add(x => x.IsAuthenticated, false)
            .Add(x => x.IsGated, true)
            .Add(x => x.AuthTitle, "Access Restricted")
            .Add(x => x.AuthDescription, "Sign in required"));

        var overlay = cut.Find(".fa-command-center-auth-overlay");
        Assert.NotNull(overlay);

        var title = cut.Find("h2");
        Assert.Equal("Access Restricted", title.TextContent.Trim());

        var desc = cut.Find(".fa-command-center-auth-desc");
        Assert.Equal("Sign in required", desc.TextContent.Trim());

        var signInBtn = cut.Find("button.fa-command-center-auth-btn");
        Assert.NotNull(signInBtn);
    }

    [Fact]
    public void FaCommandCenterTemplate_Authenticated_RendersFullCommandDeck()
    {
        var cut = Render<FaCommandCenterTemplate>(p => p
            .Add(x => x.BrandText, "Parrescence")
            .Add(x => x.Subtitle, "Cloud Command Center")
            .Add(x => x.IsAuthenticated, true)
            .Add(x => x.UserDisplayName, "Security Admin")
            .Add(x => x.Tabs, (RenderFragment)(b => b.AddMarkupContent(0, "<div class=\"test-tab\">Topology</div>")))
            .Add(x => x.KpiCards, (RenderFragment)(b => b.AddMarkupContent(0, "<div class=\"test-kpi\">Burn Rate: $15.50</div>")))
            .Add(x => x.QuickLaunch, (RenderFragment)(b => b.AddMarkupContent(0, "<div class=\"test-quick\">Vince</div>")))
            .Add(x => x.Filters, (RenderFragment)(b => b.AddMarkupContent(0, "<select class=\"test-filter\"><option>All</option></select>")))
            .Add(x => x.ChildContent, (RenderFragment)(b => b.AddMarkupContent(0, "<div class=\"test-topology\">2 Tenants, 2 Subscriptions</div>"))));

        // Brand & User
        var brand = cut.Find(".fa-command-center-brand-meta h1");
        Assert.Equal("Parrescence", brand.TextContent.Trim());

        var userBadge = cut.Find(".fa-command-center-auth-badge");
        Assert.Contains("Security Admin", userBadge.TextContent);

        // Sections
        var tabs = cut.Find(".fa-command-center-tab-bar");
        Assert.Contains("Topology", tabs.TextContent);

        var kpi = cut.Find(".fa-command-center-kpi-grid");
        Assert.Contains("Burn Rate: $15.50", kpi.TextContent);

        var quick = cut.Find(".fa-command-center-quick-launch");
        Assert.Contains("Vince", quick.TextContent);

        var filter = cut.Find(".fa-command-center-filter-bar");
        Assert.NotNull(filter.QuerySelector("select.test-filter"));

        var content = cut.Find(".fa-command-center-content");
        Assert.Contains("2 Tenants, 2 Subscriptions", content.TextContent);
    }
}
