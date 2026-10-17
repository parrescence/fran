using Bunit;
using Fran.Templates;
using Microsoft.AspNetCore.Components;
using Xunit;

namespace Fran.Tests;

public class FaFinancialTemplatesTests : BunitContext
{
    [Fact]
    public void FaLedgerTemplate_Standalone_RendersShellAndLedgerSections()
    {
        var cut = Render<FaLedgerTemplate>(p => p
            .Add(x => x.BrandText, "Vince")
            .Add(x => x.Title, "Transactions Ledger")
            .Add(x => x.Description, "Zero-knowledge transaction ledger")
            .Add(x => x.TitleBadge, (RenderFragment)(b => b.AddMarkupContent(0, "<span class=\"badge\">150 Txns</span>")))
            .Add(x => x.HeaderActions, (RenderFragment)(b => b.AddMarkupContent(0, "<button class=\"add-btn\">+ Add</button>")))
            .Add(x => x.SummaryContent, (RenderFragment)(b => b.AddMarkupContent(0, "<div class=\"summary-card\">Account: Everyday Checking</div>")))
            .Add(x => x.FiltersContent, (RenderFragment)(b => b.AddMarkupContent(0, "<input class=\"search-box\" placeholder=\"Search\" />")))
            .Add(x => x.ChildContent, (RenderFragment)(b => b.AddMarkupContent(0, "<div class=\"txn-row\">Coffee Shop -$4.50</div>")))
            .Add(x => x.PaginationContent, (RenderFragment)(b => b.AddMarkupContent(0, "<div class=\"pager\">Page 1 of 5</div>")))
            .Add(x => x.FloatingAction, (RenderFragment)(b => b.AddMarkupContent(0, "<button class=\"fa-financial-fab\">+</button>"))));

        // Shell is rendered
        Assert.NotNull(cut.Find(".fa-shell"));

        // Header and elements
        var title = cut.Find("h1.fa-ledger-title");
        Assert.Equal("Transactions Ledger", title.TextContent.Trim());

        var desc = cut.Find(".fa-ledger-desc");
        Assert.Equal("Zero-knowledge transaction ledger", desc.TextContent.Trim());

        Assert.NotNull(cut.Find(".fa-ledger-summary"));
        Assert.NotNull(cut.Find(".fa-ledger-filter-bar"));
        Assert.NotNull(cut.Find(".fa-ledger-content"));
        Assert.NotNull(cut.Find(".fa-ledger-pagination"));
        Assert.NotNull(cut.Find(".fa-ledger-fab-slot"));
    }

    [Fact]
    public void FaLedgerTemplate_EmbedInLayout_RendersWithoutOuterShell()
    {
        var cut = Render<FaLedgerTemplate>(p => p
            .Add(x => x.EmbedInLayout, true)
            .Add(x => x.Title, "Transactions")
            .Add(x => x.ChildContent, (RenderFragment)(b => b.AddMarkupContent(0, "<div class=\"txn-list\">Items</div>"))));

        // When EmbedInLayout = true, no .fa-shell is rendered
        Assert.Empty(cut.FindAll(".fa-shell"));

        var container = cut.Find(".fa-financial-ledger-template");
        Assert.NotNull(container);
        var title = cut.Find("h1.fa-ledger-title");
        Assert.Equal("Transactions", title.TextContent.Trim());
    }

    [Fact]
    public void FaFinanceOverviewTemplate_RendersHeaderKpiStripAndColumns()
    {
        var cut = Render<FaFinanceOverviewTemplate>(p => p
            .Add(x => x.BrandText, "Vince")
            .Add(x => x.Title, "October 2026 Overview")
            .Add(x => x.Subtitle, "Cash flow and portfolio health")
            .Add(x => x.PeriodSelector, (RenderFragment)(b => b.AddMarkupContent(0, "<div class=\"period-stepper\">Oct 2026</div>")))
            .Add(x => x.HeaderActions, (RenderFragment)(b => b.AddMarkupContent(0, "<button class=\"refresh-btn\">Refresh</button>")))
            .Add(x => x.KpiMetrics, (RenderFragment)(b => b.AddMarkupContent(0, "<div class=\"kpi-card\">Inflow: $4,500</div><div class=\"kpi-card\">Outflow: $2,100</div>")))
            .Add(x => x.CashFlowContent, (RenderFragment)(b => b.AddMarkupContent(0, "<div class=\"cashflow-meter\">Net: +$2,400</div>")))
            .Add(x => x.CategoryBreakdown, (RenderFragment)(b => b.AddMarkupContent(0, "<div class=\"category-chart\">Groceries 40%</div>")))
            .Add(x => x.RecentActivity, (RenderFragment)(b => b.AddMarkupContent(0, "<div class=\"recent-txns\">Recent 5</div>"))));

        var title = cut.Find("h1.fa-overview-title");
        Assert.Equal("October 2026 Overview", title.TextContent.Trim());

        var kpiStrip = cut.Find("section.fa-kpi-swipe-strip");
        Assert.NotNull(kpiStrip);
        Assert.Contains("Inflow: $4,500", kpiStrip.TextContent);

        Assert.NotNull(cut.Find(".fa-overview-cashflow-section"));
        Assert.NotNull(cut.Find(".fa-overview-columns"));
    }

    [Fact]
    public void FaFinanceOverviewTemplate_EmbedInLayout_RendersWithoutOuterShell()
    {
        var cut = Render<FaFinanceOverviewTemplate>(p => p
            .Add(x => x.EmbedInLayout, true)
            .Add(x => x.Title, "Dashboard")
            .Add(x => x.KpiMetrics, (RenderFragment)(b => b.AddMarkupContent(0, "<div class=\"kpi\">$100</div>"))));

        Assert.Empty(cut.FindAll(".fa-shell"));
        Assert.NotNull(cut.Find(".fa-finance-overview-template"));
    }

    [Fact]
    public void FaAccountPortfolioTemplate_RendersNetWorthInstitutionsAndGroups()
    {
        var cut = Render<FaAccountPortfolioTemplate>(p => p
            .Add(x => x.BrandText, "Vince")
            .Add(x => x.Title, "Accounts Portfolio")
            .Add(x => x.Description, "Manage linked banking institutions")
            .Add(x => x.HeaderActions, (RenderFragment)(b => b.AddMarkupContent(0, "<button class=\"connect-btn\">Connect Bank</button>")))
            .Add(x => x.NetWorthContent, (RenderFragment)(b => b.AddMarkupContent(0, "<div class=\"networth-banner\">Net Worth: $82,450</div>")))
            .Add(x => x.InstitutionsContent, (RenderFragment)(b => b.AddMarkupContent(0, "<div class=\"institution-card\">Chase Bank</div>")))
            .Add(x => x.AccountGroupsContent, (RenderFragment)(b => b.AddMarkupContent(0, "<div class=\"account-group\">Cash & Checking</div>")))
            .Add(x => x.VaultSecurityContent, (RenderFragment)(b => b.AddMarkupContent(0, "<div class=\"vault-badge\">AES-256 Vault</div>"))));

        var title = cut.Find("h1.fa-portfolio-title");
        Assert.Equal("Accounts Portfolio", title.TextContent.Trim());

        Assert.NotNull(cut.Find(".fa-portfolio-networth-section"));
        Assert.NotNull(cut.Find(".fa-portfolio-institutions-section"));
        Assert.NotNull(cut.Find(".fa-portfolio-accounts-section"));
        Assert.NotNull(cut.Find(".fa-portfolio-vault-section"));
    }

    [Fact]
    public void FaAccountPortfolioTemplate_EmbedInLayout_RendersWithoutOuterShell()
    {
        var cut = Render<FaAccountPortfolioTemplate>(p => p
            .Add(x => x.EmbedInLayout, true)
            .Add(x => x.Title, "Accounts")
            .Add(x => x.NetWorthContent, (RenderFragment)(b => b.AddMarkupContent(0, "<div class=\"stat\">$50k</div>"))));

        Assert.Empty(cut.FindAll(".fa-shell"));
        Assert.NotNull(cut.Find(".fa-account-portfolio-template"));
    }
}
