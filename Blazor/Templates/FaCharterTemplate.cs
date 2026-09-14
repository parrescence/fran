using Fran.Components;
using Fran.Layout;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Fran.Templates;

/// <summary>
/// Project Charter Template matching the Parrescence design standards (cover with brand plate,
/// metadata pills, sticky table of contents bookmark rail, numbered sections with ledger numbers,
/// project delivery makeup &amp; methodology form [Agile, Scrum, Waterfall, Kanban], DevOps agreement,
/// architecture, design tokens, roadmap, open decisions, and living document footer).
/// </summary>
public sealed class FaCharterTemplate : ComponentBase
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

    // Cover fields
    [Parameter] public string ProjectName { get; set; } = "Project Charter";
    [Parameter] public string ProjectKicker { get; set; } = "Charter & Design System · Prepared for Parrescence";
    [Parameter] public string Headline { get; set; } = "Every dollar, touched once, owned by the household that spent it.";
    [Parameter] public string? HeadlineAccent { get; set; }
    [Parameter] public string Subtitle { get; set; } = "A living brand, product, and architecture charter uniting engineering, design, and operations.";
    [Parameter] public string? Tagline { get; set; }
    [Parameter] public RenderFragment? BrandPlateLogo { get; set; }
    [Parameter] public string? BrandPlateLabel { get; set; } = "Official Brand Kit";
    [Parameter] public IReadOnlyList<string>? MetaPills { get; set; }

    // Methodology & DevOps
    [Parameter] public FaProjectMethodology Methodology { get; set; } = FaProjectMethodology.AgileScrum;
    [Parameter] public string MethodologyDetails { get; set; } = "2-week sprints · Daily standups · Sprint demo & retrospective";
    [Parameter] public string DevOpsBranching { get; set; } = "GitHub Flow: main (production) + dev (integration) + feature/* (ephemeral). Direct push prohibited.";
    [Parameter] public string DevOpsCiCd { get; set; } = "GitHub Actions: Automated lint, unit & integration tests, artifact pack, and staging/prod deployments.";
    [Parameter] public IReadOnlyList<string>? QualityGates { get; set; }
    [Parameter] public IReadOnlyList<string>? Environments { get; set; }

    // Optional section overrides
    [Parameter] public RenderFragment? BrandSectionContent { get; set; }
    [Parameter] public RenderFragment? MethodologySectionContent { get; set; }
    [Parameter] public RenderFragment? DevOpsSectionContent { get; set; }
    [Parameter] public RenderFragment? ArchitectureSectionContent { get; set; }
    [Parameter] public RenderFragment? DesignSystemSectionContent { get; set; }
    [Parameter] public RenderFragment? TemplatesSectionContent { get; set; }
    [Parameter] public RenderFragment? RoadmapSectionContent { get; set; }
    [Parameter] public RenderFragment? DecisionsSectionContent { get; set; }
    [Parameter] public RenderFragment? ReferencesSectionContent { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public string? ClosingFootnote { get; set; }

    // Navigation rail
    [Parameter] public IReadOnlyList<FaBookmarkItem>? BookmarkItems { get; set; }
    [Parameter] public string? ActiveSectionId { get; set; }
    [Parameter] public EventCallback<FaBookmarkItem> OnBookmarkClick { get; set; }

    private IReadOnlyList<FaBookmarkItem> DefaultBookmarks => new[]
    {
        new FaBookmarkItem("brand", "Brand identity", "01"),
        new FaBookmarkItem("methodology", "Project makeup & methodology", "02"),
        new FaBookmarkItem("devops", "DevOps agreement & CI/CD", "03"),
        new FaBookmarkItem("architecture", "Architecture & data model", "04"),
        new FaBookmarkItem("design-system", "Design system & tokens", "05"),
        new FaBookmarkItem("templates", "Page templates & surface", "06"),
        new FaBookmarkItem("roadmap", "Roadmap & milestones", "07"),
        new FaBookmarkItem("decisions", "Open decisions", "08"),
        new FaBookmarkItem("references", "References", "09")
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
            body.AddAttribute(22, "class", "fa-charter-template");

            // Cover Banner
            body.OpenElement(23, "header");
            body.AddAttribute(24, "class", "fa-charter-cover");
            body.OpenElement(25, "div");
            body.AddAttribute(26, "class", "fa-charter-cover-inner");

            if (BrandPlateLogo != null || !string.IsNullOrEmpty(BrandPlateLabel))
            {
                body.OpenElement(27, "div");
                body.AddAttribute(28, "class", "fa-charter-brand-plate");
                if (BrandPlateLogo != null)
                {
                    body.AddContent(29, BrandPlateLogo);
                }
                if (!string.IsNullOrEmpty(BrandPlateLabel))
                {
                    body.OpenElement(30, "div");
                    body.AddAttribute(31, "class", "fa-charter-plate-label");
                    body.AddContent(32, BrandPlateLabel);
                    body.CloseElement();
                }
                body.CloseElement(); // brand-plate
            }

            body.OpenElement(33, "div");
            body.AddAttribute(34, "class", "fa-charter-kicker");
            body.AddContent(35, ProjectKicker);
            body.CloseElement();

            body.OpenElement(36, "h1");
            body.AddAttribute(37, "class", "fa-charter-title");
            body.AddContent(38, Headline);
            if (!string.IsNullOrEmpty(HeadlineAccent))
            {
                body.OpenElement(39, "span");
                body.AddAttribute(40, "class", "fa-charter-title-accent");
                body.AddContent(41, $" {HeadlineAccent}");
                body.CloseElement();
            }
            body.CloseElement(); // h1

            body.OpenElement(42, "p");
            body.AddAttribute(43, "class", "fa-charter-subtitle");
            body.AddContent(44, Subtitle);
            body.CloseElement();

            if (!string.IsNullOrEmpty(Tagline))
            {
                body.OpenElement(45, "p");
                body.AddAttribute(46, "class", "fa-charter-tagline");
                body.AddContent(47, Tagline);
                body.CloseElement();
            }

            // Meta Pills
            body.OpenElement(48, "div");
            body.AddAttribute(49, "class", "fa-charter-meta-row");

            // Methodology pill
            body.OpenElement(50, "span");
            body.AddAttribute(51, "class", "fa-charter-pill fa-charter-pill-primary");
            body.OpenElement(52, "b");
            body.AddContent(53, Methodology.ToDisplayName());
            body.CloseElement();
            body.CloseElement();

            if (MetaPills != null)
            {
                foreach (var pill in MetaPills)
                {
                    body.OpenElement(54, "span");
                    body.AddAttribute(55, "class", "fa-charter-pill");
                    body.AddContent(56, pill);
                    body.CloseElement();
                }
            }
            else
            {
                body.OpenElement(57, "span");
                body.AddAttribute(58, "class", "fa-charter-pill");
                body.AddContent(59, "Blazor WebAssembly");
                body.CloseElement();

                body.OpenElement(60, "span");
                body.AddAttribute(61, "class", "fa-charter-pill");
                body.AddContent(62, "Azure Cloud Native");
                body.CloseElement();

                body.OpenElement(63, "span");
                body.AddAttribute(64, "class", "fa-charter-pill");
                body.AddContent(65, $"{DateTime.UtcNow:MMMM yyyy}");
                body.CloseElement();
            }

            body.CloseElement(); // meta-row
            body.CloseElement(); // cover-inner
            body.CloseElement(); // cover

            // Layout: Sticky Bookmark Nav + Main Body
            body.OpenElement(66, "div");
            body.AddAttribute(67, "class", "fa-charter-layout");

            // Left Rail: Bookmark Nav
            body.OpenComponent<FaBookmarkNav>(68);
            body.AddComponentParameter(69, nameof(FaBookmarkNav.Items), BookmarkItems ?? DefaultBookmarks);
            body.AddComponentParameter(70, nameof(FaBookmarkNav.ActiveId), ActiveSectionId);
            body.AddComponentParameter(71, nameof(FaBookmarkNav.OnItemClick), OnBookmarkClick);
            body.AddComponentParameter(72, nameof(FaBookmarkNav.Title), "Contents");
            body.AddComponentParameter(73, nameof(FaBookmarkNav.Sticky), true);
            body.AddComponentParameter(74, nameof(FaBookmarkNav.CssClass), "fa-charter-toc");
            body.CloseComponent();

            // Right Rail: Content Sections
            body.OpenElement(75, "main");
            body.AddAttribute(76, "class", "fa-charter-main");

            // 01 Brand Identity
            body.OpenElement(77, "section");
            body.AddAttribute(78, "id", "brand");
            body.AddAttribute(79, "class", "fa-charter-section");
            body.OpenElement(80, "div");
            body.AddAttribute(81, "class", "fa-charter-eyebrow");
            body.OpenElement(82, "span");
            body.AddAttribute(83, "class", "fa-charter-ledger-no");
            body.AddContent(84, "01");
            body.CloseElement();
            body.AddContent(85, " Brand identity");
            body.CloseElement();
            body.OpenElement(86, "h2");
            body.AddContent(87, ProjectName);
            body.CloseElement();
            if (BrandSectionContent != null)
            {
                body.AddContent(88, BrandSectionContent);
            }
            else
            {
                body.OpenElement(89, "p");
                body.AddAttribute(90, "class", "fa-charter-lede");
                body.AddContent(91, "A cohesive brand voice and visual mark grounded in authentic product identity.");
                body.CloseElement();
                body.OpenElement(92, "blockquote");
                body.AddAttribute(93, "class", "fa-charter-blockquote");
                body.OpenElement(94, "p");
                body.AddContent(95, Subtitle);
                body.CloseElement();
                body.CloseElement();
            }
            body.CloseElement(); // #brand

            // 02 Project Makeup & Methodology
            body.OpenElement(96, "section");
            body.AddAttribute(97, "id", "methodology");
            body.AddAttribute(98, "class", "fa-charter-section");
            body.OpenElement(99, "div");
            body.AddAttribute(100, "class", "fa-charter-eyebrow");
            body.OpenElement(101, "span");
            body.AddAttribute(102, "class", "fa-charter-ledger-no");
            body.AddContent(103, "02");
            body.CloseElement();
            body.AddContent(104, " Project makeup & methodology");
            body.CloseElement();
            body.OpenElement(105, "h2");
            body.AddContent(106, $"{Methodology.ToDisplayName()} Framework");
            body.CloseElement();
            if (MethodologySectionContent != null)
            {
                body.AddContent(107, MethodologySectionContent);
            }
            else
            {
                body.OpenElement(108, "p");
                body.AddAttribute(109, "class", "fa-charter-lede");
                body.AddContent(110, Methodology.ToDescription());
                body.CloseElement();

                body.OpenElement(111, "div");
                body.AddAttribute(112, "class", "fa-charter-grid-2");

                body.OpenElement(113, "div");
                body.AddAttribute(114, "class", "fa-charter-card");
                body.OpenElement(115, "h4");
                body.AddContent(116, "Execution Cadence & Ceremonies");
                body.CloseElement();
                body.OpenElement(117, "p");
                body.AddContent(118, MethodologyDetails);
                body.CloseElement();
                body.CloseElement(); // card

                body.OpenElement(119, "div");
                body.AddAttribute(120, "class", "fa-charter-card");
                body.OpenElement(121, "h4");
                body.AddContent(122, "Delivery & Review Rhythm");
                body.CloseElement();
                body.OpenElement(123, "p");
                body.AddContent(124, "Continuous backlog refinement with working software demonstrated at the end of each iteration.");
                body.CloseElement();
                body.CloseElement(); // card

                body.CloseElement(); // grid-2
            }
            body.CloseElement(); // #methodology

            // 03 DevOps Agreement & CI/CD
            body.OpenElement(125, "section");
            body.AddAttribute(126, "id", "devops");
            body.AddAttribute(127, "class", "fa-charter-section");
            body.OpenElement(128, "div");
            body.AddAttribute(129, "class", "fa-charter-eyebrow");
            body.OpenElement(130, "span");
            body.AddAttribute(131, "class", "fa-charter-ledger-no");
            body.AddContent(132, "03");
            body.CloseElement();
            body.AddContent(133, " DevOps agreement & CI/CD");
            body.CloseElement();
            body.OpenElement(134, "h2");
            body.AddContent(135, "Engineering & Pipeline Standards");
            body.CloseElement();
            if (DevOpsSectionContent != null)
            {
                body.AddContent(136, DevOpsSectionContent);
            }
            else
            {
                body.OpenComponent<FaDevOpsAgreementCard>(137);
                body.AddComponentParameter(138, nameof(FaDevOpsAgreementCard.Methodology), Methodology);
                body.AddComponentParameter(139, nameof(FaDevOpsAgreementCard.MethodologyDetails), MethodologyDetails);
                body.AddComponentParameter(140, nameof(FaDevOpsAgreementCard.BranchingStrategy), DevOpsBranching);
                body.AddComponentParameter(141, nameof(FaDevOpsAgreementCard.CiCdPipeline), DevOpsCiCd);
                if (QualityGates != null)
                {
                    body.AddComponentParameter(142, nameof(FaDevOpsAgreementCard.QualityGates), QualityGates);
                }
                if (Environments != null)
                {
                    body.AddComponentParameter(143, nameof(FaDevOpsAgreementCard.Environments), Environments);
                }
                body.CloseComponent();
            }
            body.CloseElement(); // #devops

            // 04 Architecture & Data Model
            body.OpenElement(144, "section");
            body.AddAttribute(145, "id", "architecture");
            body.AddAttribute(146, "class", "fa-charter-section");
            body.OpenElement(147, "div");
            body.AddAttribute(148, "class", "fa-charter-eyebrow");
            body.OpenElement(149, "span");
            body.AddAttribute(150, "class", "fa-charter-ledger-no");
            body.AddContent(151, "04");
            body.CloseElement();
            body.AddContent(152, " Architecture & data model");
            body.CloseElement();
            body.OpenElement(153, "h2");
            body.AddContent(154, "System Topography & Storage");
            body.CloseElement();
            if (ArchitectureSectionContent != null)
            {
                body.AddContent(155, ArchitectureSectionContent);
            }
            else
            {
                body.OpenElement(156, "p");
                body.AddAttribute(157, "class", "fa-charter-lede");
                body.AddContent(158, "Clean architecture with isolated presentation, domain models, and cloud-native services.");
                body.CloseElement();
            }
            body.CloseElement(); // #architecture

            // 05 Design System & Tokens
            body.OpenElement(159, "section");
            body.AddAttribute(160, "id", "design-system");
            body.AddAttribute(161, "class", "fa-charter-section");
            body.OpenElement(162, "div");
            body.AddAttribute(163, "class", "fa-charter-eyebrow");
            body.OpenElement(164, "span");
            body.AddAttribute(165, "class", "fa-charter-ledger-no");
            body.AddContent(166, "05");
            body.CloseElement();
            body.AddContent(167, " Design system & tokens");
            body.CloseElement();
            body.OpenElement(168, "h2");
            body.AddContent(169, "Fran Component System & Palettes");
            body.CloseElement();
            if (DesignSystemSectionContent != null)
            {
                body.AddContent(170, DesignSystemSectionContent);
            }
            else
            {
                body.OpenElement(171, "p");
                body.AddAttribute(172, "class", "fa-charter-lede");
                body.AddContent(173, "WCAG contrast-compliant design tokens, typography scales, and accessible components.");
                body.CloseElement();
            }
            body.CloseElement(); // #design-system

            // 06 Surface & Page Templates
            body.OpenElement(174, "section");
            body.AddAttribute(175, "id", "templates");
            body.AddAttribute(176, "class", "fa-charter-section");
            body.OpenElement(177, "div");
            body.AddAttribute(178, "class", "fa-charter-eyebrow");
            body.OpenElement(179, "span");
            body.AddAttribute(180, "class", "fa-charter-ledger-no");
            body.AddContent(181, "06");
            body.CloseElement();
            body.AddContent(182, " Page templates & surface");
            body.CloseElement();
            body.OpenElement(183, "h2");
            body.AddContent(184, "User Experience & Core Shapes");
            body.CloseElement();
            if (TemplatesSectionContent != null)
            {
                body.AddContent(185, TemplatesSectionContent);
            }
            else
            {
                body.OpenElement(186, "p");
                body.AddAttribute(187, "class", "fa-charter-lede");
                body.AddContent(188, "Primary application surfaces built from responsive, consistent layouts.");
                body.CloseElement();
            }
            body.CloseElement(); // #templates

            // 07 Roadmap & Milestones
            body.OpenElement(189, "section");
            body.AddAttribute(190, "id", "roadmap");
            body.AddAttribute(191, "class", "fa-charter-section");
            body.OpenElement(192, "div");
            body.AddAttribute(193, "class", "fa-charter-eyebrow");
            body.OpenElement(194, "span");
            body.AddAttribute(195, "class", "fa-charter-ledger-no");
            body.AddContent(196, "07");
            body.CloseElement();
            body.AddContent(197, " Roadmap & milestones");
            body.CloseElement();
            body.OpenElement(198, "h2");
            body.AddContent(199, "Execution Phases");
            body.CloseElement();
            if (RoadmapSectionContent != null)
            {
                body.AddContent(200, RoadmapSectionContent);
            }
            else
            {
                body.OpenElement(201, "div");
                body.AddAttribute(202, "class", "fa-charter-timeline");

                body.OpenElement(203, "div");
                body.AddAttribute(204, "class", "fa-charter-phase");
                body.OpenElement(205, "div");
                body.AddAttribute(206, "class", "fa-charter-when");
                body.AddContent(207, "Phase 1 (Done)");
                body.CloseElement();
                body.OpenElement(208, "div");
                body.OpenElement(209, "h4");
                body.AddContent(210, "Foundations & Core Framework");
                body.CloseElement();
                body.OpenElement(211, "p");
                body.AddContent(212, "Architecture scaffolding, automated CI/CD pipeline, and core UI tokens.");
                body.CloseElement();
                body.CloseElement();
                body.CloseElement(); // phase 1

                body.OpenElement(213, "div");
                body.AddAttribute(214, "class", "fa-charter-phase");
                body.OpenElement(215, "div");
                body.AddAttribute(216, "class", "fa-charter-when");
                body.AddContent(217, "Phase 2 (Active)");
                body.CloseElement();
                body.OpenElement(218, "div");
                body.OpenElement(219, "h4");
                body.AddContent(220, "Feature Surface & Client Experience");
                body.CloseElement();
                body.OpenElement(221, "p");
                body.AddContent(222, "Implementing end-to-end user workflows, testing suites, and telemetry.");
                body.CloseElement();
                body.CloseElement();
                body.CloseElement(); // phase 2

                body.OpenElement(223, "div");
                body.AddAttribute(224, "class", "fa-charter-phase");
                body.OpenElement(225, "div");
                body.AddAttribute(226, "class", "fa-charter-when");
                body.AddContent(227, "Phase 3 (Next)");
                body.CloseElement();
                body.OpenElement(228, "div");
                body.OpenElement(229, "h4");
                body.AddContent(230, "Scale & Optimization");
                body.CloseElement();
                body.OpenElement(231, "p");
                body.AddContent(232, "Production hardening, automated synthetic health probes, and performance optimizations.");
                body.CloseElement();
                body.CloseElement();
                body.CloseElement(); // phase 3

                body.CloseElement(); // timeline
            }
            body.CloseElement(); // #roadmap

            // 08 Open Decisions
            body.OpenElement(233, "section");
            body.AddAttribute(234, "id", "decisions");
            body.AddAttribute(235, "class", "fa-charter-section");
            body.OpenElement(236, "div");
            body.AddAttribute(237, "class", "fa-charter-eyebrow");
            body.OpenElement(238, "span");
            body.AddAttribute(239, "class", "fa-charter-ledger-no");
            body.AddContent(240, "08");
            body.CloseElement();
            body.AddContent(241, " Open decisions");
            body.CloseElement();
            body.OpenElement(242, "h2");
            body.AddContent(243, "Architecture & Scope Calls");
            body.CloseElement();
            if (DecisionsSectionContent != null)
            {
                body.AddContent(244, DecisionsSectionContent);
            }
            else
            {
                body.OpenElement(245, "ul");
                body.AddAttribute(246, "class", "fa-charter-checklist");
                body.OpenElement(247, "li");
                body.AddContent(248, "Evaluation of automated synthetic performance probes vs. real user monitoring.");
                body.CloseElement();
                body.OpenElement(249, "li");
                body.AddContent(250, "Review of multi-region failover and read-replica strategies.");
                body.CloseElement();
                body.CloseElement(); // checklist
            }
            body.CloseElement(); // #decisions

            // 09 References
            body.OpenElement(251, "section");
            body.AddAttribute(252, "id", "references");
            body.AddAttribute(253, "class", "fa-charter-section");
            body.OpenElement(254, "div");
            body.AddAttribute(255, "class", "fa-charter-eyebrow");
            body.OpenElement(256, "span");
            body.AddAttribute(257, "class", "fa-charter-ledger-no");
            body.AddContent(258, "09");
            body.CloseElement();
            body.AddContent(259, " References");
            body.CloseElement();
            body.OpenElement(260, "h2");
            body.AddContent(261, "Source Documentation");
            body.CloseElement();
            if (ReferencesSectionContent != null)
            {
                body.AddContent(262, ReferencesSectionContent);
            }
            else
            {
                body.OpenElement(263, "ul");
                body.AddAttribute(264, "class", "fa-charter-checklist");
                body.OpenElement(265, "li");
                body.AddContent(266, "Parrescence Design Standards & Project Charter Guide.");
                body.CloseElement();
                body.OpenElement(267, "li");
                body.AddContent(268, "Repository source code, ARCHITECTURE.md guidelines, and continuous integration workflows.");
                body.CloseElement();
                body.CloseElement();
            }
            body.CloseElement(); // #references

            // Custom ChildContent
            if (ChildContent != null)
            {
                body.AddContent(269, ChildContent);
            }

            // Living Document Footnote
            body.OpenElement(270, "div");
            body.AddAttribute(271, "class", "fa-charter-foot");
            body.AddContent(272, ClosingFootnote ?? $"{ProjectName} — a living project charter. Keep this document updated as open decisions close out.");
            body.CloseElement();

            body.CloseElement(); // fa-charter-main
            body.CloseElement(); // fa-charter-layout
            body.CloseElement(); // fa-charter-template
        }));

        builder.CloseComponent();
    }
}
