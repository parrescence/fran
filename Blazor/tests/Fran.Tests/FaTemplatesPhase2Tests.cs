using Bunit;
using Fran.Components;
using Fran.Templates;
using Microsoft.AspNetCore.Components;
using Xunit;

namespace Fran.Tests;

public class FaTemplatesPhase2Tests : BunitContext
{
    [Fact]
    public void FaAboutTemplate_RendersHeroStatsMissionTeamCta()
    {
        var cut = Render<FaAboutTemplate>(p => p
            .Add(x => x.BrandText, "Acme")
            .Add(x => x.Title, "Building the future")
            .Add(x => x.Description, "Our company story and culture")
            .Add(x => x.StatsContent, (RenderFragment)(b => b.AddMarkupContent(0, "<div class=\"test-stat\">10,000+ Teams</div>")))
            .Add(x => x.MissionTitle, "Our Mission")
            .Add(x => x.MissionContent, (RenderFragment)(b => b.AddMarkupContent(0, "<p class=\"mission-text\">We believe in quality software.</p>")))
            .Add(x => x.TeamTitle, "The Core Team")
            .Add(x => x.TeamContent, (RenderFragment)(b => b.AddMarkupContent(0, "<div class=\"team-member\">Jane Doe - Lead Architect</div>")))
            .Add(x => x.CtaContent, (RenderFragment)(b => b.AddMarkupContent(0, "<button class=\"cta-btn\">Join us</button>"))));

        var title = cut.Find("h1.fa-about-title");
        Assert.Equal("Building the future", title.TextContent.Trim());

        var desc = cut.Find("p.fa-about-description");
        Assert.Equal("Our company story and culture", desc.TextContent.Trim());

        var stats = cut.Find(".fa-about-stats");
        Assert.Contains("10,000+ Teams", stats.TextContent);

        var mission = cut.Find(".fa-about-mission");
        Assert.Contains("Our Mission", mission.TextContent);
        Assert.Contains("We believe in quality software.", mission.TextContent);

        var team = cut.Find(".fa-about-team");
        Assert.Contains("The Core Team", team.TextContent);
        Assert.Contains("Jane Doe - Lead Architect", team.TextContent);

        var cta = cut.Find(".fa-about-cta");
        Assert.NotNull(cta.QuerySelector("button.cta-btn"));
    }

    [Fact]
    public void FaDocsTemplate_RendersSidebarBreadcrumbsSearchAndArticle()
    {
        var cut = Render<FaDocsTemplate>(p => p
            .Add(x => x.BrandText, "Acme")
            .Add(x => x.Sidebar, (RenderFragment)(b => b.AddMarkupContent(0, "<nav class=\"docs-tree\">Tree Nav</nav>")))
            .Add(x => x.BreadcrumbContent, (RenderFragment)(b => b.AddMarkupContent(0, "<span class=\"breadcrumb-item\">Docs &gt; API</span>")))
            .Add(x => x.SearchContent, (RenderFragment)(b => b.AddMarkupContent(0, "<input class=\"docs-search-input\" />")))
            .Add(x => x.Title, "Authentication & Tokens")
            .Add(x => x.Description, "How to authenticate requests")
            .Add(x => x.Badge, (RenderFragment)(b => b.AddMarkupContent(0, "<span class=\"version-badge\">v2.0</span>")))
            .Add(x => x.ChildContent, (RenderFragment)(b => b.AddMarkupContent(0, "<p class=\"doc-body\">Pass the Bearer token in the header.</p>")))
            .Add(x => x.TableOfContents, (RenderFragment)(b => b.AddMarkupContent(0, "<a class=\"toc-link\" href=\"#headers\">Headers</a>")))
            .Add(x => x.PrevNextNav, (RenderFragment)(b => b.AddMarkupContent(0, "<a class=\"next-link\">Next: Endpoints</a>"))));

        var title = cut.Find("h1.fa-docs-title");
        Assert.Equal("Authentication & Tokens", title.TextContent.Trim());

        var badge = cut.Find(".fa-docs-badge");
        Assert.Contains("v2.0", badge.TextContent);

        var breadcrumbs = cut.Find(".fa-docs-breadcrumbs");
        Assert.Contains("Docs > API", breadcrumbs.TextContent);

        var toc = cut.Find(".fa-docs-toc");
        Assert.Contains("On this page", toc.TextContent);
        Assert.NotNull(toc.QuerySelector("a.toc-link"));

        var body = cut.Find(".fa-docs-body");
        Assert.Contains("Pass the Bearer token", body.TextContent);

        var prevNext = cut.Find(".fa-docs-prev-next");
        Assert.NotNull(prevNext.QuerySelector("a.next-link"));
    }

    [Fact]
    public void FaChangelogTemplate_RendersHeaderFiltersAndTimeline()
    {
        var cut = Render<FaChangelogTemplate>(p => p
            .Add(x => x.BrandText, "Acme")
            .Add(x => x.Title, "Release Changelog")
            .Add(x => x.Description, "All changes and fixes")
            .Add(x => x.HeaderActions, (RenderFragment)(b => b.AddMarkupContent(0, "<button class=\"rss-btn\">RSS</button>")))
            .Add(x => x.FilterContent, (RenderFragment)(b => b.AddMarkupContent(0, "<div class=\"filter-chips\">Chips</div>")))
            .Add(x => x.ChildContent, (RenderFragment)(b => b.AddMarkupContent(0, "<div class=\"release-entry\">Release v1.5.0</div>"))));

        var title = cut.Find("h1.fa-changelog-title");
        Assert.Equal("Release Changelog", title.TextContent.Trim());

        var actions = cut.Find(".fa-changelog-actions");
        Assert.NotNull(actions.QuerySelector("button.rss-btn"));

        var filters = cut.Find(".fa-changelog-filters");
        Assert.Contains("Chips", filters.TextContent);

        var timeline = cut.Find(".fa-changelog-timeline");
        Assert.Contains("Release v1.5.0", timeline.TextContent);
    }

    [Fact]
    public void FaLegalTemplate_RendersHeaderTOCAndBody()
    {
        var cut = Render<FaLegalTemplate>(p => p
            .Add(x => x.BrandText, "Acme")
            .Add(x => x.Title, "Terms of Service")
            .Add(x => x.LastUpdated, "September 13, 2026")
            .Add(x => x.HeaderActions, (RenderFragment)(b => b.AddMarkupContent(0, "<button class=\"pdf-btn\">PDF</button>")))
            .Add(x => x.TableOfContents, (RenderFragment)(b => b.AddMarkupContent(0, "<a class=\"toc-item\" href=\"#sec-1\">1. Overview</a>")))
            .Add(x => x.ChildContent, (RenderFragment)(b => b.AddMarkupContent(0, "<section id=\"sec-1\"><p>These terms govern your use.</p></section>"))));

        var title = cut.Find("h1.fa-legal-title");
        Assert.Equal("Terms of Service", title.TextContent.Trim());

        var date = cut.Find(".fa-legal-date");
        Assert.Equal("September 13, 2026", date.TextContent.Trim());

        var actions = cut.Find(".fa-legal-actions");
        Assert.NotNull(actions.QuerySelector("button.pdf-btn"));

        var toc = cut.Find(".fa-legal-toc");
        Assert.NotNull(toc.QuerySelector("a.toc-item"));

        var body = cut.Find(".fa-legal-body");
        Assert.Contains("These terms govern your use.", body.TextContent);
    }

    [Fact]
    public void FaThankYouTemplate_RendersSuccessDetailsAndActions()
    {
        var cut = Render<FaThankYouTemplate>(p => p
            .Add(x => x.BrandText, "Acme")
            .Add(x => x.Title, "Payment Successful")
            .Add(x => x.Description, "Confirmation #ORD-99812")
            .Add(x => x.SummaryContent, (RenderFragment)(b => b.AddMarkupContent(0, "<div class=\"receipt\">Total: $49.00</div>")))
            .Add(x => x.NextStepsContent, (RenderFragment)(b => b.AddMarkupContent(0, "<ol class=\"steps\"><li>Check your inbox</li></ol>")))
            .Add(x => x.Actions, (RenderFragment)(b => b.AddMarkupContent(0, "<a class=\"dashboard-btn\" href=\"/\">Go to Dashboard</a>"))));

        var title = cut.Find("h1.fa-thank-you-title");
        Assert.Equal("Payment Successful", title.TextContent.Trim());

        var summary = cut.Find(".fa-thank-you-summary");
        Assert.Contains("Total: $49.00", summary.TextContent);

        var steps = cut.Find(".fa-thank-you-steps");
        Assert.NotNull(steps.QuerySelector("ol.steps"));

        var actions = cut.Find(".fa-thank-you-actions");
        Assert.NotNull(actions.QuerySelector("a.dashboard-btn"));
    }

    [Fact]
    public void FaThankYouTemplate_Standalone_RendersChromeFree()
    {
        var cut = Render<FaThankYouTemplate>(p => p
            .Add(x => x.Standalone, true)
            .Add(x => x.Title, "Order Placed")
            .Add(x => x.Actions, (RenderFragment)(b => b.AddMarkupContent(0, "<button class=\"done-btn\">Done</button>"))));

        Assert.NotNull(cut.Find(".fa-thank-you-standalone"));
        Assert.NotNull(cut.Find(".fa-thank-you-standalone-card"));
        Assert.Empty(cut.FindAll("header.fa-header"));
    }

    [Fact]
    public void FaComingSoonTemplate_RendersCountdownNotifyAndSocial()
    {
        var cut = Render<FaComingSoonTemplate>(p => p
            .Add(x => x.BrandText, "Acme Next")
            .Add(x => x.Title, "Something big is coming")
            .Add(x => x.CountdownContent, (RenderFragment)(b => b.AddMarkupContent(0, "<div class=\"timer\">14 Days Left</div>")))
            .Add(x => x.NotifyContent, (RenderFragment)(b => b.AddMarkupContent(0, "<form class=\"email-form\"><input /></form>")))
            .Add(x => x.SocialContent, (RenderFragment)(b => b.AddMarkupContent(0, "<div class=\"social-links\"><a href=\"#\">Twitter</a></div>"))));

        var title = cut.Find("h1.fa-coming-soon-title");
        Assert.Equal("Something big is coming", title.TextContent.Trim());

        var brand = cut.Find(".fa-coming-soon-brand-name");
        Assert.Equal("Acme Next", brand.TextContent.Trim());

        var countdown = cut.Find(".fa-coming-soon-countdown");
        Assert.Contains("14 Days Left", countdown.TextContent);

        var notify = cut.Find(".fa-coming-soon-notify");
        Assert.NotNull(notify.QuerySelector("form.email-form"));

        var social = cut.Find(".fa-coming-soon-social");
        Assert.NotNull(social.QuerySelector("a"));
    }
}
