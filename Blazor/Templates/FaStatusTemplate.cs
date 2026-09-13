using Fran.Components;
using Fran.Layout;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Fran.Templates;

/// <summary>
/// A system status &amp; uptime template: <see cref="FaStandardShell"/> wrapped around
/// an overall status announcement banner, a subscribe-to-updates header action, a breakdown
/// of individual service and API health statuses, uptime metrics, and an incident history log.
/// </summary>
public sealed class FaStatusTemplate : ComponentBase
{
    [Parameter, EditorRequired] public string BrandText { get; set; } = "";
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
    [Parameter] public RenderFragment? FooterContent { get; set; }

    [Parameter] public FaNavPosition HeaderPosition { get; set; } = FaNavPosition.Standard;
    [Parameter] public FaNavPosition FooterPosition { get; set; } = FaNavPosition.Standard;

    /// <summary>Main status headline, e.g. "System Status".</summary>
    [Parameter] public string Title { get; set; } = "System Status";

    /// <summary>Overall health indicator text, e.g. "All Systems Operational" or "Partial Outage".</summary>
    [Parameter] public string OverallStatus { get; set; } = "All Systems Operational";

    /// <summary>Variant tone for the overall status card (Success, Danger, Warning, Neutral).</summary>
    [Parameter] public FaBadgeVariant OverallStatusVariant { get; set; } = FaBadgeVariant.Success;

    /// <summary>Timestamp string indicating when status was last verified.</summary>
    [Parameter] public string? LastUpdated { get; set; } = "Refreshed just now";

    /// <summary>Action in header, e.g. "Subscribe to Updates" or RSS button.</summary>
    [Parameter] public RenderFragment? HeaderAction { get; set; }

    /// <summary>List or grid of component/service health statuses.</summary>
    [Parameter] public RenderFragment? ServicesContent { get; set; }

    /// <summary>Uptime percentage charts or 90-day availability history bar.</summary>
    [Parameter] public RenderFragment? UptimeMetricsContent { get; set; }

    /// <summary>Chronological log of past incidents, scheduled maintenance, and resolutions.</summary>
    [Parameter] public RenderFragment? PastIncidentsContent { get; set; }

    /// <summary>Optional custom or extra status information.</summary>
    [Parameter] public RenderFragment? ChildContent { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenComponent<FaStandardShell>(0);
        builder.AddComponentParameter(1, nameof(FaStandardShell.BrandText), BrandText);
        builder.AddComponentParameter(2, nameof(FaStandardShell.BrandHref), BrandHref);
        builder.AddComponentParameter(3, nameof(FaStandardShell.BrandIconUrl), BrandIconUrl);
        builder.AddComponentParameter(4, nameof(FaStandardShell.IsAuthenticated), IsAuthenticated);
        builder.AddComponentParameter(5, nameof(FaStandardShell.UserDisplayName), UserDisplayName);
        builder.AddComponentParameter(6, nameof(FaStandardShell.UserImageUrl), UserImageUrl);
        builder.AddComponentParameter(7, nameof(FaStandardShell.UserEmail), UserEmail);
        builder.AddComponentParameter(8, nameof(FaStandardShell.UseAvatarForm), UseAvatarForm);
        builder.AddComponentParameter(9, nameof(FaStandardShell.ShowUserNameInHeader), ShowUserNameInHeader);
        builder.AddComponentParameter(10, nameof(FaStandardShell.AccountHref), AccountHref);
        builder.AddComponentParameter(11, nameof(FaStandardShell.OnAccountClick), OnAccountClick);
        builder.AddComponentParameter(12, nameof(FaStandardShell.OnLogin), OnLogin);
        builder.AddComponentParameter(13, nameof(FaStandardShell.OnLogout), OnLogout);
        builder.AddComponentParameter(14, nameof(FaStandardShell.FooterContent), FooterContent);
        builder.AddComponentParameter(15, nameof(FaStandardShell.HeaderPosition), HeaderPosition);
        builder.AddComponentParameter(16, nameof(FaStandardShell.FooterPosition), FooterPosition);

        builder.AddComponentParameter(17, nameof(FaStandardShell.ChildContent), (RenderFragment)(body =>
        {
            body.OpenElement(18, "div");
            body.AddAttribute(19, "class", "fa-status-template");

            // Header
            body.OpenElement(20, "header");
            body.AddAttribute(21, "class", "fa-status-header");

            body.OpenElement(22, "div");
            body.AddAttribute(23, "class", "fa-status-title-group");
            body.OpenElement(24, "h1");
            body.AddAttribute(25, "class", "fa-status-title");
            body.AddContent(26, Title);
            body.CloseElement(); // h1

            if (!string.IsNullOrWhiteSpace(LastUpdated))
            {
                body.OpenElement(27, "p");
                body.AddAttribute(28, "class", "fa-status-last-updated");
                body.AddContent(29, LastUpdated);
                body.CloseElement(); // p
            }
            body.CloseElement(); // title-group

            if (HeaderAction != null)
            {
                body.OpenElement(30, "div");
                body.AddAttribute(31, "class", "fa-status-header-action");
                body.AddContent(32, HeaderAction);
                body.CloseElement(); // action
            }
            body.CloseElement(); // header

            // Overall Banner
            var variantClass = OverallStatusVariant switch
            {
                FaBadgeVariant.Success => "fa-status-banner-success",
                FaBadgeVariant.Danger => "fa-status-banner-danger",
                FaBadgeVariant.Primary => "fa-status-banner-primary",
                _ => "fa-status-banner-neutral"
            };

            body.OpenElement(33, "div");
            body.AddAttribute(34, "class", $"fa-status-banner {variantClass}");
            body.OpenElement(35, "span");
            body.AddAttribute(36, "class", "fa-status-dot");
            body.CloseElement(); // dot
            body.OpenElement(37, "span");
            body.AddAttribute(38, "class", "fa-status-banner-text");
            body.AddContent(39, OverallStatus);
            body.CloseElement(); // banner-text
            body.CloseElement(); // banner

            // Services health
            if (ServicesContent != null)
            {
                body.OpenElement(40, "section");
                body.AddAttribute(41, "class", "fa-status-section fa-status-services");
                body.OpenElement(42, "h2");
                body.AddAttribute(43, "class", "fa-status-section-title");
                body.AddContent(44, "System Components");
                body.CloseElement(); // h2
                body.AddContent(45, ServicesContent);
                body.CloseElement(); // section
            }

            // Metrics
            if (UptimeMetricsContent != null)
            {
                body.OpenElement(46, "section");
                body.AddAttribute(47, "class", "fa-status-section fa-status-metrics");
                body.OpenElement(48, "h2");
                body.AddAttribute(49, "class", "fa-status-section-title");
                body.AddContent(50, "Uptime & Historical Performance");
                body.CloseElement(); // h2
                body.AddContent(51, UptimeMetricsContent);
                body.CloseElement(); // section
            }

            // Past incidents
            if (PastIncidentsContent != null)
            {
                body.OpenElement(52, "section");
                body.AddAttribute(53, "class", "fa-status-section fa-status-incidents");
                body.OpenElement(54, "h2");
                body.AddAttribute(55, "class", "fa-status-section-title");
                body.AddContent(56, "Past Incidents");
                body.CloseElement(); // h2
                body.AddContent(57, PastIncidentsContent);
                body.CloseElement(); // section
            }

            if (ChildContent != null)
            {
                body.OpenElement(58, "div");
                body.AddAttribute(59, "class", "fa-status-content");
                body.AddContent(60, ChildContent);
                body.CloseElement(); // content
            }

            body.CloseElement(); // fa-status-template
        }));

        builder.CloseComponent();
    }
}
