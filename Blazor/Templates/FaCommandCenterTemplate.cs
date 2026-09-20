using Fran.Components;
using Fran.Layout;
using Fran.Rendering;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Fran.Templates;

/// <summary>
/// Executive Cloud Command Center Template: an authoritative, login-gated operational dashboard
/// designed for high-density cloud infrastructure management, multi-tenant and multi-subscription
/// governance, FinOps monitoring, lifecycle tracking, and dependency blast-radius exploration.
/// Features a dark command deck navbar with brand badge, gated Entra CIAM overlay, tab navigation,
/// executive KPI summary cards, quick-launch venture pills, filter bar, and hierarchical topology body.
/// </summary>
public sealed class FaCommandCenterTemplate : ComponentBase
{
    [Parameter, EditorRequired] public string BrandText { get; set; } = "";
    [Parameter] public string BrandHref { get; set; } = "/";
    [Parameter] public string? BrandIconUrl { get; set; }
    [Parameter] public string? Subtitle { get; set; }

    [Parameter] public bool IsAuthenticated { get; set; }
    [Parameter] public string? UserDisplayName { get; set; }
    [Parameter] public string? UserImageUrl { get; set; }
    [Parameter] public EventCallback OnLogin { get; set; }
    [Parameter] public EventCallback OnLogout { get; set; }

    [Parameter] public RenderFragment? HeaderActions { get; set; }
    [Parameter] public RenderFragment? Tabs { get; set; }
    [Parameter] public RenderFragment? KpiCards { get; set; }
    [Parameter] public RenderFragment? QuickLaunch { get; set; }
    [Parameter] public RenderFragment? Filters { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public RenderFragment? FooterContent { get; set; }

    [Parameter] public bool IsGated { get; set; } = true;
    [Parameter] public string AuthTitle { get; set; } = "Command Center Authentication";
    [Parameter] public string? AuthDescription { get; set; } = "Please authenticate via Microsoft Entra External ID (CIAM) to access cloud operations.";
    [Parameter] public RenderFragment? AuthActions { get; set; }
    [Parameter] public string? ErrorMessage { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "div");
        builder.AddAttribute(1, "class", "fa-command-center-template");

        // Top Navigation Bar
        builder.OpenElement(2, "header");
        builder.AddAttribute(3, "class", "fa-command-center-navbar");

        builder.OpenElement(4, "div");
        builder.AddAttribute(5, "class", "fa-command-center-brand");

        if (!string.IsNullOrEmpty(BrandIconUrl))
        {
            builder.OpenElement(6, "img");
            builder.AddAttribute(7, "src", BrandIconUrl);
            builder.AddAttribute(8, "alt", BrandText);
            builder.AddAttribute(9, "class", "fa-command-center-brand-logo");
            builder.CloseElement();
        }

        builder.OpenElement(10, "div");
        builder.AddAttribute(11, "class", "fa-command-center-brand-meta");
        builder.OpenElement(12, "h1");
        builder.AddContent(13, BrandText);
        builder.CloseElement(); // h1

        if (!string.IsNullOrEmpty(Subtitle))
        {
            builder.OpenElement(14, "p");
            builder.AddContent(15, Subtitle);
            builder.CloseElement(); // p
        }
        builder.CloseElement(); // fa-command-center-brand-meta
        builder.CloseElement(); // fa-command-center-brand

        builder.OpenElement(16, "div");
        builder.AddAttribute(17, "class", "fa-command-center-nav-actions");

        if (IsAuthenticated)
        {
            builder.OpenElement(18, "div");
            builder.AddAttribute(19, "class", "fa-command-center-auth-badge");
            builder.OpenElement(20, "span");
            builder.AddAttribute(21, "class", "fa-command-center-status-dot");
            builder.CloseElement();
            builder.OpenElement(22, "span");
            builder.AddContent(23, UserDisplayName ?? "Authenticated");
            builder.CloseElement();
            builder.CloseElement(); // auth-badge

            builder.OpenElement(24, "button");
            builder.AddAttribute(25, "type", "button");
            builder.AddAttribute(26, "class", "fa-btn fa-btn-secondary");
            builder.AddAttribute(27, "onclick", OnLogout);
            builder.AddContent(28, "Lock Console");
            builder.CloseElement(); // button
        }

        if (HeaderActions != null)
        {
            builder.AddContent(29, HeaderActions);
        }

        builder.CloseElement(); // fa-command-center-nav-actions
        builder.CloseElement(); // header

        // Gated Auth Overlay vs Authenticated Body
        if (IsGated && !IsAuthenticated)
        {
            builder.OpenElement(30, "div");
            builder.AddAttribute(31, "class", "fa-command-center-auth-overlay");

            builder.OpenElement(32, "div");
            builder.AddAttribute(33, "class", "fa-card fa-command-center-auth-card");

            if (!string.IsNullOrEmpty(BrandIconUrl))
            {
                builder.OpenElement(34, "img");
                builder.AddAttribute(35, "src", BrandIconUrl);
                builder.AddAttribute(36, "alt", BrandText);
                builder.AddAttribute(37, "class", "fa-command-center-auth-logo");
                builder.CloseElement();
            }

            builder.OpenElement(38, "h2");
            builder.AddContent(39, AuthTitle);
            builder.CloseElement();

            if (!string.IsNullOrEmpty(AuthDescription))
            {
                builder.OpenElement(40, "p");
                builder.AddAttribute(41, "class", "fa-command-center-auth-desc");
                builder.AddContent(42, AuthDescription);
                builder.CloseElement();
            }

            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                builder.OpenElement(43, "div");
                builder.AddAttribute(44, "class", "fa-alert fa-alert-danger");
                builder.AddContent(45, ErrorMessage);
                builder.CloseElement();
            }

            if (AuthActions != null)
            {
                builder.AddContent(46, AuthActions);
            }
            else
            {
                builder.OpenElement(47, "button");
                builder.AddAttribute(48, "type", "button");
                builder.AddAttribute(49, "class", "fa-btn fa-btn-primary fa-command-center-auth-btn");
                builder.AddAttribute(50, "onclick", OnLogin);
                builder.AddContent(51, "🔐 Sign In with Microsoft Entra CIAM");
                builder.CloseElement();
            }

            builder.CloseElement(); // fa-card
            builder.CloseElement(); // fa-command-center-auth-overlay
        }
        else
        {
            // Tab Navigation
            if (Tabs != null)
            {
                builder.OpenElement(52, "nav");
                builder.AddAttribute(53, "class", "fa-command-center-tab-bar");
                builder.AddAttribute(54, "aria-label", "Command Center Navigation");
                builder.AddContent(55, Tabs);
                builder.CloseElement();
            }

            // Main Console Container
            builder.OpenElement(56, "main");
            builder.AddAttribute(57, "class", "fa-command-center-container");

            // KPI Grid
            if (KpiCards != null)
            {
                builder.OpenElement(58, "section");
                builder.AddAttribute(59, "class", "fa-command-center-kpi-grid");
                builder.AddContent(60, KpiCards);
                builder.CloseElement();
            }

            // Quick Launch Row
            if (QuickLaunch != null)
            {
                builder.OpenElement(61, "section");
                builder.AddAttribute(62, "class", "fa-command-center-quick-launch");
                builder.AddContent(63, QuickLaunch);
                builder.CloseElement();
            }

            // Filter Bar
            if (Filters != null)
            {
                builder.OpenElement(64, "div");
                builder.AddAttribute(65, "class", "fa-command-center-filter-bar");
                builder.AddContent(66, Filters);
                builder.CloseElement();
            }

            // Content Area
            builder.OpenElement(67, "section");
            builder.AddAttribute(68, "class", "fa-command-center-content");
            builder.AddContent(69, ChildContent);
            builder.CloseElement();

            builder.CloseElement(); // main

            // Optional Footer
            if (FooterContent != null)
            {
                builder.OpenElement(70, "footer");
                builder.AddAttribute(71, "class", "fa-command-center-footer");
                builder.AddContent(72, FooterContent);
                builder.CloseElement();
            }
        }

        builder.CloseElement(); // fa-command-center-template
    }
}
