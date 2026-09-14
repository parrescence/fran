using Fran.Rendering;
using Fran.Templates;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Fran.Components;

/// <summary>
/// A standardized card displaying a project's DevOps agreement, project delivery makeup
/// (Agile, Scrum, Waterfall, Kanban, etc.), git branching policy, quality gates, and deployment strategy.
/// </summary>
public sealed class FaDevOpsAgreementCard : ComponentBase
{
    /// <summary>Project delivery methodology.</summary>
    [Parameter] public FaProjectMethodology Methodology { get; set; } = FaProjectMethodology.AgileScrum;

    /// <summary>Details on iteration cadence, ceremonies, or planning cycle.</summary>
    [Parameter] public string MethodologyDetails { get; set; } = "2-week sprints · Daily standups · Sprint review & demo · Retrospective";

    /// <summary>Git branching policy.</summary>
    [Parameter] public string BranchingStrategy { get; set; } = "GitHub Flow: main (production) + dev (integration) + feature/* (ephemeral branches). Direct push prohibited.";

    /// <summary>CI/CD pipeline and runner strategy.</summary>
    [Parameter] public string CiCdPipeline { get; set; } = "GitHub Actions: Automated lint, unit & integration tests, artifact pack, and staging/prod deployments.";

    /// <summary>Automated quality gates required before merging.</summary>
    [Parameter] public IReadOnlyList<string> QualityGates { get; set; } = new[]
    {
        "100% unit & integration test pass rate",
        "Zero compiler warnings and zero linter diagnostics",
        "Branch protection rules with mandatory peer review",
        "Automated Dependabot & secret scanning"
    };

    /// <summary>Deployment environment tiers.</summary>
    [Parameter] public IReadOnlyList<string> Environments { get; set; } = new[] { "Dev", "Staging", "Production" };

    /// <summary>Secrets and cloud infrastructure governance posture.</summary>
    [Parameter] public string SecretsManagement { get; set; } = "Azure Key Vault + Managed Identities (Zero checked-in secrets; principle of least privilege).";

    /// <summary>Custom CSS class.</summary>
    [Parameter] public string? CssClass { get; set; }

    /// <summary>Optional additional notes, SLA details, or team agreements.</summary>
    [Parameter] public RenderFragment? ChildContent { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "div");
        builder.AddAttribute(1, "class", CssClassNames.Combine("fa-devops-card", CssClass));

        // Header
        builder.OpenElement(2, "div");
        builder.AddAttribute(3, "class", "fa-devops-card-header");

        builder.OpenElement(4, "div");
        builder.AddAttribute(5, "class", "fa-devops-card-title-group");
        builder.OpenElement(6, "h3");
        builder.AddAttribute(7, "class", "fa-devops-card-title");
        builder.AddContent(8, "DevOps Agreement & Project Makeup");
        builder.CloseElement();

        builder.OpenElement(9, "span");
        builder.AddAttribute(10, "class", "fa-devops-methodology-badge");
        builder.AddContent(11, Methodology.ToDisplayName());
        builder.CloseElement();
        builder.CloseElement(); // title-group

        builder.OpenElement(12, "p");
        builder.AddAttribute(13, "class", "fa-devops-card-subtitle");
        builder.AddContent(14, MethodologyDetails);
        builder.CloseElement();
        builder.CloseElement(); // card-header

        // Body grid
        builder.OpenElement(15, "div");
        builder.AddAttribute(16, "class", "fa-devops-card-grid");

        // Cell 1: Branching
        builder.OpenElement(17, "div");
        builder.AddAttribute(18, "class", "fa-devops-card-cell");
        builder.OpenElement(19, "h4");
        builder.AddAttribute(20, "class", "fa-devops-cell-label");
        builder.AddContent(21, "Branching Strategy");
        builder.CloseElement();
        builder.OpenElement(22, "p");
        builder.AddContent(23, BranchingStrategy);
        builder.CloseElement();
        builder.CloseElement(); // cell 1

        // Cell 2: CI/CD
        builder.OpenElement(24, "div");
        builder.AddAttribute(25, "class", "fa-devops-card-cell");
        builder.OpenElement(26, "h4");
        builder.AddAttribute(27, "class", "fa-devops-cell-label");
        builder.AddContent(28, "CI/CD Automation");
        builder.CloseElement();
        builder.OpenElement(29, "p");
        builder.AddContent(30, CiCdPipeline);
        builder.CloseElement();
        builder.CloseElement(); // cell 2

        // Cell 3: Quality Gates
        builder.OpenElement(31, "div");
        builder.AddAttribute(32, "class", "fa-devops-card-cell");
        builder.OpenElement(33, "h4");
        builder.AddAttribute(34, "class", "fa-devops-cell-label");
        builder.AddContent(35, "Quality Gates");
        builder.CloseElement();
        builder.OpenElement(36, "ul");
        builder.AddAttribute(37, "class", "fa-devops-checklist");
        foreach (var gate in QualityGates)
        {
            builder.OpenElement(38, "li");
            builder.AddContent(39, gate);
            builder.CloseElement();
        }
        builder.CloseElement(); // ul
        builder.CloseElement(); // cell 3

        // Cell 4: Environments & Secrets
        builder.OpenElement(40, "div");
        builder.AddAttribute(41, "class", "fa-devops-card-cell");
        builder.OpenElement(42, "h4");
        builder.AddAttribute(43, "class", "fa-devops-cell-label");
        builder.AddContent(44, "Environments & Secrets");
        builder.CloseElement();
        builder.OpenElement(45, "div");
        builder.AddAttribute(46, "class", "fa-devops-env-pills");
        foreach (var env in Environments)
        {
            builder.OpenElement(47, "span");
            builder.AddAttribute(48, "class", "fa-devops-env-pill");
            builder.AddContent(49, env);
            builder.CloseElement();
        }
        builder.CloseElement(); // env-pills
        builder.OpenElement(50, "p");
        builder.AddAttribute(51, "class", "fa-devops-secrets-note");
        builder.AddContent(52, SecretsManagement);
        builder.CloseElement();
        builder.CloseElement(); // cell 4

        builder.CloseElement(); // card-grid

        if (ChildContent is not null)
        {
            builder.OpenElement(53, "div");
            builder.AddAttribute(54, "class", "fa-devops-card-extra");
            builder.AddContent(55, ChildContent);
            builder.CloseElement();
        }

        builder.CloseElement(); // fa-devops-card
    }
}
