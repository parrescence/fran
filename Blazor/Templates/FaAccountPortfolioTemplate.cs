using Fran.Layout;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Fran.Templates;

/// <summary>
/// A financial accounts and institutions portfolio template: renders either a full <see cref="FaSidebarShell"/>
/// or an embedded layout container (<see cref="EmbedInLayout"/> = true) with a net worth hero banner,
/// connected institution cards, grouped account cards (cash, credit, investments, loans),
/// and zero-knowledge security credentials management.
/// </summary>
public sealed class FaAccountPortfolioTemplate : ComponentBase
{
    /// <summary>When true, skips rendering <see cref="FaSidebarShell"/> so this template can be embedded directly inside an existing application layout.</summary>
    [Parameter] public bool EmbedInLayout { get; set; }

    [Parameter] public string BrandText { get; set; } = "";
    [Parameter] public string BrandHref { get; set; } = "";
    [Parameter] public string? BrandIconUrl { get; set; }

    [Parameter] public bool IsAuthenticated { get; set; }
    [Parameter] public string? UserDisplayName { get; set; }
    [Parameter] public string? UserImageUrl { get; set; }
    [Parameter] public string? UserEmail { get; set; }
    [Parameter] public bool UseAvatarForm { get; set; }
    [Parameter] public bool ShowUserNameInHeader { get; set; }
    [Parameter] public string? AccountHref { get; set; }
    [Parameter] public EventCallback OnAccountClick { get; set; }
    [Parameter] public EventCallback OnLogin { get; set; }
    [Parameter] public EventCallback OnLogout { get; set; }

    [Parameter] public RenderFragment? Sidebar { get; set; }
    [Parameter] public RenderFragment? FooterContent { get; set; }

    [Parameter] public FaNavPosition HeaderPosition { get; set; } = FaNavPosition.Standard;
    [Parameter] public FaNavPosition FooterPosition { get; set; } = FaNavPosition.Standard;
    [Parameter] public FaNavPosition SidebarPosition { get; set; } = FaNavPosition.Standard;
    [Parameter] public bool SidebarCollapsible { get; set; }
    [Parameter] public bool ContainScroll { get; set; }

    /// <summary>Page heading, e.g. "Accounts &amp; Institutions" or "Net Worth Portfolio".</summary>
    [Parameter] public string Title { get; set; } = "Accounts";

    /// <summary>Optional short description or vault security notice.</summary>
    [Parameter] public string? Description { get; set; }

    /// <summary>Header quick action buttons (e.g. "Connect Bank", "Vault Security", "Add Account").</summary>
    [Parameter] public RenderFragment? HeaderActions { get; set; }

    /// <summary>Net worth summary banner or asset/liability metric cards.</summary>
    [Parameter] public RenderFragment? NetWorthContent { get; set; }

    /// <summary>Connected financial institutions section with sync statuses.</summary>
    [Parameter] public RenderFragment? InstitutionsContent { get; set; }

    /// <summary>Grouped account list/grid (Checking, Savings, Credit Cards, Investments).</summary>
    [Parameter] public RenderFragment? AccountGroupsContent { get; set; }

    /// <summary>Zero-knowledge security banner or vault status card.</summary>
    [Parameter] public RenderFragment? VaultSecurityContent { get; set; }

    /// <summary>Optional additional content.</summary>
    [Parameter] public RenderFragment? ChildContent { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (EmbedInLayout)
        {
            RenderBody(builder);
            return;
        }

        builder.OpenComponent<FaSidebarShell>(0);
        builder.AddComponentParameter(1, nameof(FaSidebarShell.BrandText), BrandText);
        builder.AddComponentParameter(2, nameof(FaSidebarShell.BrandHref), BrandHref);
        builder.AddComponentParameter(3, nameof(FaSidebarShell.BrandIconUrl), BrandIconUrl);
        builder.AddComponentParameter(4, nameof(FaSidebarShell.IsAuthenticated), IsAuthenticated);
        builder.AddComponentParameter(5, nameof(FaSidebarShell.UserDisplayName), UserDisplayName);
        builder.AddComponentParameter(6, nameof(FaSidebarShell.UserImageUrl), UserImageUrl);
        builder.AddComponentParameter(7, nameof(FaSidebarShell.UserEmail), UserEmail);
        builder.AddComponentParameter(8, nameof(FaSidebarShell.UseAvatarForm), UseAvatarForm);
        builder.AddComponentParameter(9, nameof(FaSidebarShell.ShowUserNameInHeader), ShowUserNameInHeader);
        builder.AddComponentParameter(10, nameof(FaSidebarShell.AccountHref), AccountHref);
        builder.AddComponentParameter(11, nameof(FaSidebarShell.OnAccountClick), OnAccountClick);
        builder.AddComponentParameter(12, nameof(FaSidebarShell.OnLogin), OnLogin);
        builder.AddComponentParameter(13, nameof(FaSidebarShell.OnLogout), OnLogout);
        builder.AddComponentParameter(14, nameof(FaSidebarShell.Sidebar), Sidebar);
        builder.AddComponentParameter(15, nameof(FaSidebarShell.FooterContent), FooterContent);
        builder.AddComponentParameter(16, nameof(FaSidebarShell.HeaderPosition), HeaderPosition);
        builder.AddComponentParameter(17, nameof(FaSidebarShell.FooterPosition), FooterPosition);
        builder.AddComponentParameter(18, nameof(FaSidebarShell.SidebarPosition), SidebarPosition);
        builder.AddComponentParameter(19, nameof(FaSidebarShell.SidebarCollapsible), SidebarCollapsible);
        builder.AddComponentParameter(20, nameof(FaSidebarShell.ContainScroll), ContainScroll);
        builder.AddComponentParameter(21, nameof(FaSidebarShell.ChildContent), (RenderFragment)RenderBody);
        builder.CloseComponent();
    }

    private void RenderBody(RenderTreeBuilder builder)
    {
        var seq = 0;
        builder.OpenElement(seq++, "div");
        builder.AddAttribute(seq++, "class", "fa-account-portfolio-template");

        // Header Row
        builder.OpenElement(seq++, "div");
        builder.AddAttribute(seq++, "class", "fa-portfolio-header");

        builder.OpenElement(seq++, "div");
        builder.AddAttribute(seq++, "class", "fa-portfolio-title-group");
        builder.OpenElement(seq++, "h1");
        builder.AddAttribute(seq++, "class", "fa-portfolio-title");
        builder.AddContent(seq++, Title);
        builder.CloseElement();

        if (!string.IsNullOrEmpty(Description))
        {
            builder.OpenElement(seq++, "p");
            builder.AddAttribute(seq++, "class", "fa-portfolio-desc");
            builder.AddContent(seq++, Description);
            builder.CloseElement();
        }
        builder.CloseElement(); // .fa-portfolio-title-group

        if (HeaderActions is not null)
        {
            builder.OpenElement(seq++, "div");
            builder.AddAttribute(seq++, "class", "fa-portfolio-header-actions");
            builder.AddContent(seq++, HeaderActions);
            builder.CloseElement();
        }
        builder.CloseElement(); // .fa-portfolio-header

        // Net Worth Hero / Metrics
        if (NetWorthContent is not null)
        {
            builder.OpenElement(seq++, "section");
            builder.AddAttribute(seq++, "class", "fa-portfolio-networth-section");
            builder.AddContent(seq++, NetWorthContent);
            builder.CloseElement();
        }

        // Institutions sync section
        if (InstitutionsContent is not null)
        {
            builder.OpenElement(seq++, "section");
            builder.AddAttribute(seq++, "class", "fa-portfolio-institutions-section");
            builder.AddContent(seq++, InstitutionsContent);
            builder.CloseElement();
        }

        // Account Groups (Cards grid)
        if (AccountGroupsContent is not null)
        {
            builder.OpenElement(seq++, "section");
            builder.AddAttribute(seq++, "class", "fa-portfolio-accounts-section");
            builder.AddContent(seq++, AccountGroupsContent);
            builder.CloseElement();
        }

        // Vault Security section
        if (VaultSecurityContent is not null)
        {
            builder.OpenElement(seq++, "section");
            builder.AddAttribute(seq++, "class", "fa-portfolio-vault-section");
            builder.AddContent(seq++, VaultSecurityContent);
            builder.CloseElement();
        }

        // Additional child content
        if (ChildContent is not null)
        {
            builder.OpenElement(seq++, "div");
            builder.AddAttribute(seq++, "class", "fa-portfolio-extra");
            builder.AddContent(seq++, ChildContent);
            builder.CloseElement();
        }

        builder.CloseElement(); // .fa-account-portfolio-template
    }
}
