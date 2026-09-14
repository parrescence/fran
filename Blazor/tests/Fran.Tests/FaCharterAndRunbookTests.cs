using Bunit;
using Fran.Components;
using Fran.Templates;
using Xunit;

namespace Fran.Tests;

public class FaCharterAndRunbookTests : BunitContext
{
    [Fact]
    public void FaDevOpsAgreementCard_RendersMethodologyAndQualityGates()
    {
        var cut = Render<FaDevOpsAgreementCard>(p => p
            .Add(x => x.Methodology, FaProjectMethodology.AgileScrum)
            .Add(x => x.MethodologyDetails, "2-week sprints · daily standup")
            .Add(x => x.BranchingStrategy, "GitHub Flow with branch protection")
            .Add(x => x.QualityGates, new[] { "100% test pass", "Zero warnings" })
            .Add(x => x.Environments, new[] { "Dev", "Prod" }));

        // Methodology badge
        var badge = cut.Find(".fa-devops-methodology-badge");
        Assert.Equal("Agile (Scrum)", badge.TextContent.Trim());

        // Quality gates checklist
        var checklistItems = cut.FindAll(".fa-devops-checklist li");
        Assert.Equal(2, checklistItems.Count);
        Assert.Contains("100% test pass", checklistItems[0].TextContent);

        // Environments
        var envPills = cut.FindAll(".fa-devops-env-pill");
        Assert.Equal(2, envPills.Count);
        Assert.Equal("Dev", envPills[0].TextContent.Trim());
        Assert.Equal("Prod", envPills[1].TextContent.Trim());
    }

    [Fact]
    public void FaCharterTemplate_RendersCoverAndStandardSections()
    {
        var cut = Render<FaCharterTemplate>(p => p
            .Add(x => x.BrandText, "Fran")
            .Add(x => x.ProjectName, "Vince")
            .Add(x => x.Headline, "Every dollar, touched once.")
            .Add(x => x.HeadlineAccent, "owned by the family.")
            .Add(x => x.Methodology, FaProjectMethodology.AgileScrum)
            .Add(x => x.Tagline, "\"Your money's new best friend.\""));

        // Title and accent
        var title = cut.Find("h1.fa-charter-title");
        Assert.Contains("Every dollar, touched once.", title.TextContent);
        Assert.Contains("owned by the family.", cut.Find(".fa-charter-title-accent").TextContent);

        // Tagline
        Assert.Contains("Your money's new best friend.", cut.Find(".fa-charter-tagline").TextContent);

        // Methodology pill in cover
        Assert.Contains("Agile (Scrum)", cut.Find(".fa-charter-pill-primary").TextContent);

        // Sticky TOC bookmark nav
        var tocLinks = cut.FindAll(".fa-charter-toc .fa-bookmark-nav-link");
        Assert.Equal(9, tocLinks.Count);
        Assert.Contains("Brand identity", tocLinks[0].TextContent);
        Assert.Contains("Project makeup & methodology", tocLinks[1].TextContent);
        Assert.Contains("DevOps agreement & CI/CD", tocLinks[2].TextContent);

        // Section anchors
        Assert.NotNull(cut.Find("section#brand"));
        Assert.NotNull(cut.Find("section#methodology"));
        Assert.NotNull(cut.Find("section#devops"));
        Assert.NotNull(cut.Find("section#architecture"));
        Assert.NotNull(cut.Find("section#design-system"));
        Assert.NotNull(cut.Find("section#templates"));
        Assert.NotNull(cut.Find("section#roadmap"));
        Assert.NotNull(cut.Find("section#decisions"));
        Assert.NotNull(cut.Find("section#references"));

        // Embedded DevOps agreement card
        Assert.NotNull(cut.Find(".fa-devops-card"));

        // Living document footer
        Assert.Contains("living project charter", cut.Find(".fa-charter-foot").TextContent);
    }

    [Fact]
    public void FaRunbookTemplate_RendersOperationalPlaybook()
    {
        var cut = Render<FaRunbookTemplate>(p => p
            .Add(x => x.BrandText, "Operations")
            .Add(x => x.SystemName, "Vince Payment Engine")
            .Add(x => x.ServiceTier, "Tier 1 — Mission Critical")
            .Add(x => x.SlaTarget, "99.95% Availability")
            .Add(x => x.PrimaryOnCall, "Alice (PagerDuty)")
            .Add(x => x.WarRoomChannel, "#incident-bridge"));

        // Header and badges
        Assert.Contains("Vince Payment Engine", cut.Find(".fa-runbook-title").TextContent);
        Assert.Contains("Tier 1 — Mission Critical", cut.Find(".fa-runbook-badges").TextContent);
        Assert.Contains("99.95% Availability", cut.Find(".fa-runbook-badges").TextContent);
        Assert.Contains("Alice (PagerDuty)", cut.Find(".fa-runbook-badges").TextContent);
        Assert.Contains("#incident-bridge", cut.Find(".fa-runbook-badges").TextContent);

        // Sticky TOC bookmarks
        var tocLinks = cut.FindAll(".fa-runbook-toc .fa-bookmark-nav-link");
        Assert.Equal(8, tocLinks.Count);
        Assert.Contains("System overview & topology", tocLinks[0].TextContent);
        Assert.Contains("On-call & escalation matrix", tocLinks[1].TextContent);
        Assert.Contains("Severity tiers & SLAs", tocLinks[2].TextContent);

        // Sections
        Assert.NotNull(cut.Find("section#overview"));
        Assert.NotNull(cut.Find("section#escalation"));
        Assert.NotNull(cut.Find("section#severity"));
        Assert.NotNull(cut.Find("section#health"));
        Assert.NotNull(cut.Find("section#diagnostics"));
        Assert.NotNull(cut.Find("section#recovery"));
        Assert.NotNull(cut.Find("section#rollback"));
        Assert.NotNull(cut.Find("section#post-mortem"));

        // Footnote
        Assert.Contains("Operational Runbook", cut.Find(".fa-runbook-foot").TextContent);
    }
}
