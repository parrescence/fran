using Fran.Components;
using Fran.Layout;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Fran.Templates;

/// <summary>
/// Operational Runbook Template: A living operational playbook for engineering and SRE teams
/// covering system dependencies, on-call escalation matrices, incident severity tiers &amp; SLAs,
/// diagnostic CLI commands, step-by-step recovery playbooks, rollback procedures, and post-mortem review.
/// Paired with a sticky bookmark navigation rail for rapid triage during live incidents.
/// </summary>
public sealed class FaRunbookTemplate : ComponentBase
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

    [Parameter] public RenderFragment? HeaderNav { get; set; }
    [Parameter] public RenderFragment? FooterNav { get; set; }
    [Parameter] public RenderFragment? FooterContent { get; set; }
    [Parameter] public FaNavPosition HeaderPosition { get; set; } = FaNavPosition.Standard;
    [Parameter] public FaNavPosition FooterPosition { get; set; } = FaNavPosition.Standard;
    [Parameter] public bool ContainScroll { get; set; }

    // Runbook metadata
    [Parameter] public string SystemName { get; set; } = "Core API & Service Engine";
    [Parameter] public string SystemDescription { get; set; } = "Operational runbook, SLA guarantees, triage diagnostics, and emergency recovery procedures.";
    [Parameter] public string ServiceTier { get; set; } = "Tier 1 — Mission Critical";
    [Parameter] public string SlaTarget { get; set; } = "99.95% Availability (< 21.9 min downtime / month)";
    [Parameter] public string PrimaryOnCall { get; set; } = "SRE Primary (PagerDuty Schedule)";
    [Parameter] public string SecondaryOnCall { get; set; } = "Platform Engineering Lead";
    [Parameter] public string WarRoomChannel { get; set; } = "#incident-war-room (Slack) / Bridge";
    [Parameter] public string? StatusPageUrl { get; set; }

    // Navigation rail
    [Parameter] public IReadOnlyList<FaBookmarkItem>? BookmarkItems { get; set; }
    [Parameter] public string? ActiveSectionId { get; set; }
    [Parameter] public EventCallback<FaBookmarkItem> OnBookmarkClick { get; set; }

    // Section overrides
    [Parameter] public RenderFragment? OverviewContent { get; set; }
    [Parameter] public RenderFragment? EscalationContent { get; set; }
    [Parameter] public RenderFragment? SeverityContent { get; set; }
    [Parameter] public RenderFragment? HealthContent { get; set; }
    [Parameter] public RenderFragment? DiagnosticsContent { get; set; }
    [Parameter] public RenderFragment? RecoveryContent { get; set; }
    [Parameter] public RenderFragment? RollbackContent { get; set; }
    [Parameter] public RenderFragment? PostMortemContent { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public string? ClosingFootnote { get; set; }

    private IReadOnlyList<FaBookmarkItem> DefaultBookmarks => new[]
    {
        new FaBookmarkItem("overview", "System overview & topology", "01"),
        new FaBookmarkItem("escalation", "On-call & escalation matrix", "02"),
        new FaBookmarkItem("severity", "Severity tiers & SLAs", "03"),
        new FaBookmarkItem("health", "Health checks & telemetry", "04"),
        new FaBookmarkItem("diagnostics", "Diagnostic commands & triage", "05"),
        new FaBookmarkItem("recovery", "Common failures & recovery", "06"),
        new FaBookmarkItem("rollback", "Deployment & rollback", "07"),
        new FaBookmarkItem("post-mortem", "Post-mortem guidelines", "08")
    };

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
        builder.AddComponentParameter(14, nameof(FaStandardShell.HeaderNav), HeaderNav);
        builder.AddComponentParameter(15, nameof(FaStandardShell.FooterNav), FooterNav);
        builder.AddComponentParameter(16, nameof(FaStandardShell.FooterContent), FooterContent);
        builder.AddComponentParameter(17, nameof(FaStandardShell.HeaderPosition), HeaderPosition);
        builder.AddComponentParameter(18, nameof(FaStandardShell.FooterPosition), FooterPosition);
        builder.AddComponentParameter(19, nameof(FaStandardShell.ContainScroll), ContainScroll);

        builder.AddComponentParameter(20, nameof(FaStandardShell.ChildContent), (RenderFragment)(body =>
        {
            body.OpenElement(21, "div");
            body.AddAttribute(22, "class", "fa-runbook-template");

            // Header Banner
            body.OpenElement(23, "header");
            body.AddAttribute(24, "class", "fa-runbook-header");
            body.OpenElement(25, "div");
            body.AddAttribute(26, "class", "fa-runbook-header-inner");

            body.OpenElement(27, "div");
            body.AddAttribute(28, "class", "fa-runbook-kicker");
            body.AddContent(29, "Operational Runbook & Incident Playbook");
            body.CloseElement();

            body.OpenElement(30, "h1");
            body.AddAttribute(31, "class", "fa-runbook-title");
            body.AddContent(32, SystemName);
            body.CloseElement();

            body.OpenElement(33, "p");
            body.AddAttribute(34, "class", "fa-runbook-subtitle");
            body.AddContent(35, SystemDescription);
            body.CloseElement();

            // Status & On-Call Badges Row
            body.OpenElement(36, "div");
            body.AddAttribute(37, "class", "fa-runbook-badges");

            body.OpenElement(38, "div");
            body.AddAttribute(39, "class", "fa-runbook-badge-card");
            body.OpenElement(40, "span");
            body.AddAttribute(41, "class", "fa-runbook-badge-label");
            body.AddContent(42, "Service Tier");
            body.CloseElement();
            body.OpenElement(43, "strong");
            body.AddContent(44, ServiceTier);
            body.CloseElement();
            body.CloseElement(); // badge-card

            body.OpenElement(45, "div");
            body.AddAttribute(46, "class", "fa-runbook-badge-card");
            body.OpenElement(47, "span");
            body.AddAttribute(48, "class", "fa-runbook-badge-label");
            body.AddContent(49, "SLA Guarantee");
            body.CloseElement();
            body.OpenElement(50, "strong");
            body.AddContent(51, SlaTarget);
            body.CloseElement();
            body.CloseElement(); // badge-card

            body.OpenElement(52, "div");
            body.AddAttribute(53, "class", "fa-runbook-badge-card");
            body.OpenElement(54, "span");
            body.AddAttribute(55, "class", "fa-runbook-badge-label");
            body.AddContent(56, "Primary On-Call");
            body.CloseElement();
            body.OpenElement(57, "strong");
            body.AddContent(58, PrimaryOnCall);
            body.CloseElement();
            body.CloseElement(); // badge-card

            body.OpenElement(59, "div");
            body.AddAttribute(60, "class", "fa-runbook-badge-card");
            body.OpenElement(61, "span");
            body.AddAttribute(62, "class", "fa-runbook-badge-label");
            body.AddContent(63, "Incident Bridge");
            body.CloseElement();
            body.OpenElement(64, "strong");
            body.AddContent(65, WarRoomChannel);
            body.CloseElement();
            body.CloseElement(); // badge-card

            body.CloseElement(); // fa-runbook-badges
            body.CloseElement(); // fa-runbook-header-inner
            body.CloseElement(); // fa-runbook-header

            // Layout
            body.OpenElement(66, "div");
            body.AddAttribute(67, "class", "fa-runbook-layout");

            // Left Rail: Bookmark Nav
            body.OpenComponent<FaBookmarkNav>(68);
            body.AddComponentParameter(69, nameof(FaBookmarkNav.Items), BookmarkItems ?? DefaultBookmarks);
            body.AddComponentParameter(70, nameof(FaBookmarkNav.ActiveId), ActiveSectionId);
            body.AddComponentParameter(71, nameof(FaBookmarkNav.OnItemClick), OnBookmarkClick);
            body.AddComponentParameter(72, nameof(FaBookmarkNav.Title), "Runbook Sections");
            body.AddComponentParameter(73, nameof(FaBookmarkNav.Sticky), true);
            body.AddComponentParameter(74, nameof(FaBookmarkNav.CssClass), "fa-runbook-toc");
            body.CloseComponent();

            // Right Rail: Content Sections
            body.OpenElement(75, "main");
            body.AddAttribute(76, "class", "fa-runbook-main");

            // 01 Overview
            body.OpenElement(77, "section");
            body.AddAttribute(78, "id", "overview");
            body.AddAttribute(79, "class", "fa-runbook-section");
            body.OpenElement(80, "div");
            body.AddAttribute(81, "class", "fa-runbook-eyebrow");
            body.OpenElement(82, "span");
            body.AddAttribute(83, "class", "fa-runbook-ledger-no");
            body.AddContent(84, "01");
            body.CloseElement();
            body.AddContent(85, " System overview & topology");
            body.CloseElement();
            body.OpenElement(86, "h2");
            body.AddContent(87, "System Architecture & Dependencies");
            body.CloseElement();
            if (OverviewContent != null)
            {
                body.AddContent(88, OverviewContent);
            }
            else
            {
                body.OpenElement(89, "p");
                body.AddAttribute(90, "class", "fa-runbook-lede");
                body.AddContent(91, "High-level service architecture, inbound/outbound dependencies, and regional deployment layout.");
                body.CloseElement();
                body.OpenElement(92, "div");
                body.AddAttribute(93, "class", "fa-runbook-table-wrap");
                body.OpenElement(94, "table");
                body.OpenElement(95, "thead");
                body.OpenElement(96, "tr");
                body.OpenElement(97, "th"); body.AddContent(98, "Component"); body.CloseElement();
                body.OpenElement(99, "th"); body.AddContent(100, "Hosting Provider"); body.CloseElement();
                body.OpenElement(101, "th"); body.AddContent(102, "Dependency Criticality"); body.CloseElement();
                body.CloseElement(); // tr
                body.CloseElement(); // thead
                body.OpenElement(103, "tbody");

                body.OpenElement(104, "tr");
                body.OpenElement(105, "td"); body.AddContent(106, "Web Client (WASM)"); body.CloseElement();
                body.OpenElement(107, "td"); body.AddContent(108, "Azure Static Web Apps / CDN"); body.CloseElement();
                body.OpenElement(109, "td"); body.AddContent(110, "High — Public entry point"); body.CloseElement();
                body.CloseElement();

                body.OpenElement(111, "tr");
                body.OpenElement(112, "td"); body.AddContent(113, "API Worker"); body.CloseElement();
                body.OpenElement(114, "td"); body.AddContent(115, "Azure Functions (Isolated Worker)"); body.CloseElement();
                body.OpenElement(116, "td"); body.AddContent(117, "Critical — Core transactions"); body.CloseElement();
                body.CloseElement();

                body.OpenElement(118, "tr");
                body.OpenElement(119, "td"); body.AddContent(120, "Database Layer"); body.CloseElement();
                body.OpenElement(121, "td"); body.AddContent(122, "Azure Cosmos DB (Serverless/Provisioned)"); body.CloseElement();
                body.OpenElement(123, "td"); body.AddContent(124, "Critical — Authoritative storage"); body.CloseElement();
                body.CloseElement();

                body.CloseElement(); // tbody
                body.CloseElement(); // table
                body.CloseElement(); // table-wrap
            }
            body.CloseElement(); // #overview

            // 02 Escalation Matrix
            body.OpenElement(125, "section");
            body.AddAttribute(126, "id", "escalation");
            body.AddAttribute(127, "class", "fa-runbook-section");
            body.OpenElement(128, "div");
            body.AddAttribute(129, "class", "fa-runbook-eyebrow");
            body.OpenElement(130, "span");
            body.AddAttribute(131, "class", "fa-runbook-ledger-no");
            body.AddContent(132, "02");
            body.CloseElement();
            body.AddContent(133, " On-call & escalation matrix");
            body.CloseElement();
            body.OpenElement(134, "h2");
            body.AddContent(135, "Incident Roles & Notification Trees");
            body.CloseElement();
            if (EscalationContent != null)
            {
                body.AddContent(136, EscalationContent);
            }
            else
            {
                body.OpenElement(137, "div");
                body.AddAttribute(138, "class", "fa-runbook-table-wrap");
                body.OpenElement(139, "table");
                body.OpenElement(140, "thead");
                body.OpenElement(141, "tr");
                body.OpenElement(142, "th"); body.AddContent(143, "Escalation Tier"); body.CloseElement();
                body.OpenElement(144, "th"); body.AddContent(145, "Contact / Schedule"); body.CloseElement();
                body.OpenElement(146, "th"); body.AddContent(147, "SLA Window"); body.CloseElement();
                body.CloseElement(); // tr
                body.CloseElement(); // thead
                body.OpenElement(148, "tbody");

                body.OpenElement(149, "tr");
                body.OpenElement(150, "td"); body.AddContent(151, "Tier 1: Primary On-Call"); body.CloseElement();
                body.OpenElement(152, "td"); body.AddContent(153, PrimaryOnCall); body.CloseElement();
                body.OpenElement(154, "td"); body.AddContent(155, "< 15 minutes"); body.CloseElement();
                body.CloseElement();

                body.OpenElement(156, "tr");
                body.OpenElement(157, "td"); body.AddContent(158, "Tier 2: Secondary / Platform Lead"); body.CloseElement();
                body.OpenElement(159, "td"); body.AddContent(160, SecondaryOnCall); body.CloseElement();
                body.OpenElement(161, "td"); body.AddContent(162, "< 30 minutes if unacknowledged"); body.CloseElement();
                body.CloseElement();

                body.OpenElement(163, "tr");
                body.OpenElement(164, "td"); body.AddContent(165, "Tier 3: Engineering Executive"); body.CloseElement();
                body.OpenElement(166, "td"); body.AddContent(167, "VP of Engineering / Incident Commander"); body.CloseElement();
                body.OpenElement(168, "td"); body.AddContent(169, "< 1 hour (SEV-1 only)"); body.CloseElement();
                body.CloseElement();

                body.CloseElement(); // tbody
                body.CloseElement(); // table
                body.CloseElement(); // table-wrap
            }
            body.CloseElement(); // #escalation

            // 03 Severity Tiers
            body.OpenElement(170, "section");
            body.AddAttribute(171, "id", "severity");
            body.AddAttribute(172, "class", "fa-runbook-section");
            body.OpenElement(173, "div");
            body.AddAttribute(174, "class", "fa-runbook-eyebrow");
            body.OpenElement(175, "span");
            body.AddAttribute(176, "class", "fa-runbook-ledger-no");
            body.AddContent(177, "03");
            body.CloseElement();
            body.AddContent(178, " Severity tiers & SLAs");
            body.CloseElement();
            body.OpenElement(179, "h2");
            body.AddContent(180, "Classification & Response SLAs");
            body.CloseElement();
            if (SeverityContent != null)
            {
                body.AddContent(181, SeverityContent);
            }
            else
            {
                body.OpenElement(182, "div");
                body.AddAttribute(183, "class", "fa-runbook-grid-2");

                body.OpenElement(184, "div");
                body.AddAttribute(185, "class", "fa-runbook-card fa-runbook-card-danger");
                body.OpenElement(186, "h4"); body.AddContent(187, "SEV-1: Critical Outage"); body.CloseElement();
                body.OpenElement(188, "p"); body.AddContent(189, "Complete service downtime or critical data impairment. Response target: < 15 min. Updates every 30 min."); body.CloseElement();
                body.CloseElement();

                body.OpenElement(190, "div");
                body.AddAttribute(191, "class", "fa-runbook-card fa-runbook-card-warning");
                body.OpenElement(192, "h4"); body.AddContent(193, "SEV-2: Major Degradation"); body.CloseElement();
                body.OpenElement(194, "p"); body.AddContent(195, "Significant feature impairment or elevated error rate with partial workaround. Response target: < 30 min."); body.CloseElement();
                body.CloseElement();

                body.OpenElement(196, "div");
                body.AddAttribute(197, "class", "fa-runbook-card");
                body.OpenElement(198, "h4"); body.AddContent(199, "SEV-3: Moderate Defect"); body.CloseElement();
                body.OpenElement(200, "p"); body.AddContent(201, "Minor system defect or non-critical latency elevation. Response target: < 4 business hours."); body.CloseElement();
                body.CloseElement();

                body.OpenElement(202, "div");
                body.AddAttribute(203, "class", "fa-runbook-card");
                body.OpenElement(204, "h4"); body.AddContent(205, "SEV-4: Minor Inconvenience"); body.CloseElement();
                body.OpenElement(206, "p"); body.AddContent(207, "Cosmetic flaws, documentation errors, or minor UI glitches. Triaged in next sprint backlog."); body.CloseElement();
                body.CloseElement();

                body.CloseElement(); // grid-2
            }
            body.CloseElement(); // #severity

            // 04 Health Checks & Telemetry
            body.OpenElement(208, "section");
            body.AddAttribute(209, "id", "health");
            body.AddAttribute(210, "class", "fa-runbook-section");
            body.OpenElement(211, "div");
            body.AddAttribute(212, "class", "fa-runbook-eyebrow");
            body.OpenElement(213, "span");
            body.AddAttribute(214, "class", "fa-runbook-ledger-no");
            body.AddContent(215, "04");
            body.CloseElement();
            body.AddContent(216, " Health checks & telemetry");
            body.CloseElement();
            body.OpenElement(217, "h2");
            body.AddContent(218, "Diagnostic Probes & Telemetry");
            body.CloseElement();
            if (HealthContent != null)
            {
                body.AddContent(219, HealthContent);
            }
            else
            {
                body.OpenElement(220, "p");
                body.AddAttribute(221, "class", "fa-runbook-lede");
                body.AddContent(222, "Automated synthetic probes and log queries for validating system vitality.");
                body.CloseElement();

                body.OpenElement(223, "div");
                body.AddAttribute(224, "class", "fa-runbook-code-block");
                body.OpenElement(225, "pre");
                body.AddContent(226, "# Synthetic health probe verification\ncurl -fsS -I https://api.parrescence.com/api/healthz\n# Expected: HTTP/2 200 OK (Content-Type: application/json; status=healthy)");
                body.CloseElement();
                body.CloseElement();
            }
            body.CloseElement(); // #health

            // 05 Diagnostics & Triage Commands
            body.OpenElement(227, "section");
            body.AddAttribute(228, "id", "diagnostics");
            body.AddAttribute(229, "class", "fa-runbook-section");
            body.OpenElement(230, "div");
            body.AddAttribute(231, "class", "fa-runbook-eyebrow");
            body.OpenElement(232, "span");
            body.AddAttribute(233, "class", "fa-runbook-ledger-no");
            body.AddContent(234, "05");
            body.CloseElement();
            body.AddContent(235, " Diagnostic commands & triage");
            body.CloseElement();
            body.OpenElement(236, "h2");
            body.AddContent(237, "Triage Commands & Log Inspection");
            body.CloseElement();
            if (DiagnosticsContent != null)
            {
                body.AddContent(238, DiagnosticsContent);
            }
            else
            {
                body.OpenElement(239, "div");
                body.AddAttribute(240, "class", "fa-runbook-code-block");
                body.OpenElement(241, "pre");
                body.AddContent(242, "# Stream live Azure Function App execution logs\naz functionapp log tail --name <app-name> --resource-group <rg-name>\n\n# Query Application Insights for 5xx exceptions in last 30 minutes\nexceptions | where timestamp > ago(30m) | summarize count() by type, outerMessage");
                body.CloseElement();
                body.CloseElement();
            }
            body.CloseElement(); // #diagnostics

            // 06 Recovery Procedures
            body.OpenElement(243, "section");
            body.AddAttribute(244, "id", "recovery");
            body.AddAttribute(245, "class", "fa-runbook-section");
            body.OpenElement(246, "div");
            body.AddAttribute(247, "class", "fa-runbook-eyebrow");
            body.OpenElement(248, "span");
            body.AddAttribute(249, "class", "fa-runbook-ledger-no");
            body.AddContent(250, "06");
            body.CloseElement();
            body.AddContent(251, " Common failures & recovery");
            body.CloseElement();
            body.OpenElement(252, "h2");
            body.AddContent(253, "Emergency Recovery Procedures");
            body.CloseElement();
            if (RecoveryContent != null)
            {
                body.AddContent(254, RecoveryContent);
            }
            else
            {
                body.OpenElement(255, "div");
                body.AddAttribute(256, "class", "fa-runbook-card");
                body.OpenElement(257, "h4"); body.AddContent(258, "Scenario A: Database Rate Limit (HTTP 429 Too Many Requests)"); body.CloseElement();
                body.OpenElement(259, "p"); body.AddContent(260, "1. Confirm RU/s throttling in Azure Monitor metrics.\n2. Scale provisioned throughput or activate autoscale bursting.\n3. Verify retry backoff policies in client SDK."); body.CloseElement();
                body.CloseElement();

                body.OpenElement(261, "div");
                body.AddAttribute(262, "class", "fa-runbook-card");
                body.OpenElement(263, "h4"); body.AddContent(264, "Scenario B: Token Validation & CIAM Authority Unreachable"); body.CloseElement();
                body.OpenElement(265, "p"); body.AddContent(266, "1. Check Microsoft Entra ID status health dashboard.\n2. Verify local token cache expiry in Functions middleware.\n3. Validate JWKS endpoint reachability via diagnostic ping."); body.CloseElement();
                body.CloseElement();
            }
            body.CloseElement(); // #recovery

            // 07 Deployment & Rollback
            body.OpenElement(267, "section");
            body.AddAttribute(268, "id", "rollback");
            body.AddAttribute(269, "class", "fa-runbook-section");
            body.OpenElement(270, "div");
            body.AddAttribute(271, "class", "fa-runbook-eyebrow");
            body.OpenElement(272, "span");
            body.AddAttribute(273, "class", "fa-runbook-ledger-no");
            body.AddContent(274, "07");
            body.CloseElement();
            body.AddContent(275, " Deployment & rollback");
            body.CloseElement();
            body.OpenElement(276, "h2");
            body.AddContent(277, "Zero-Downtime Rollback Procedure");
            body.CloseElement();
            if (RollbackContent != null)
            {
                body.AddContent(278, RollbackContent);
            }
            else
            {
                body.OpenElement(279, "div");
                body.AddAttribute(280, "class", "fa-runbook-code-block");
                body.OpenElement(281, "pre");
                body.AddContent(282, "# 1. Swap deployment slots immediately back to stable staging slot\naz functionapp deployment slot swap -g <rg-name> -n <app-name> --slot staging --target-slot production\n\n# 2. Revert breaking commit on dev branch via git revert\ngit revert <commit-sha> -m 1\ngit push origin dev");
                body.CloseElement();
                body.CloseElement();
            }
            body.CloseElement(); // #rollback

            // 08 Post-Mortem Guidelines
            body.OpenElement(283, "section");
            body.AddAttribute(284, "id", "post-mortem");
            body.AddAttribute(285, "class", "fa-runbook-section");
            body.OpenElement(286, "div");
            body.AddAttribute(287, "class", "fa-runbook-eyebrow");
            body.OpenElement(288, "span");
            body.AddAttribute(289, "class", "fa-runbook-ledger-no");
            body.AddContent(290, "08");
            body.CloseElement();
            body.AddContent(291, " Post-mortem guidelines");
            body.CloseElement();
            body.OpenElement(292, "h2");
            body.AddContent(293, "Blameless Root Cause Analysis (RCA)");
            body.CloseElement();
            if (PostMortemContent != null)
            {
                body.AddContent(294, PostMortemContent);
            }
            else
            {
                body.OpenElement(295, "ul");
                body.AddAttribute(296, "class", "fa-runbook-checklist");
                body.OpenElement(297, "li"); body.AddContent(298, "Host blameless RCA within 48 hours of SEV-1 or SEV-2 resolution."); body.CloseElement();
                body.OpenElement(299, "li"); body.AddContent(300, "Establish factual chronological timeline from first telemetry anomaly to final mitigation."); body.CloseElement();
                body.OpenElement(301, "li"); body.AddContent(302, "Execute 5 Whys analysis to uncover systemic and process gaps."); body.CloseElement();
                body.OpenElement(303, "li"); body.AddContent(304, "Assign corrective action items with owners and target milestone deadlines."); body.CloseElement();
                body.CloseElement();
            }
            body.CloseElement(); // #post-mortem

            if (ChildContent != null)
            {
                body.AddContent(305, ChildContent);
            }

            body.OpenElement(306, "div");
            body.AddAttribute(307, "class", "fa-runbook-foot");
            body.AddContent(308, ClosingFootnote ?? $"{SystemName} Operational Runbook. Review and update after every incident or deployment change.");
            body.CloseElement();

            body.CloseElement(); // fa-runbook-main
            body.CloseElement(); // fa-runbook-layout
            body.CloseElement(); // fa-runbook-template
        }));

        builder.CloseComponent();
    }
}
