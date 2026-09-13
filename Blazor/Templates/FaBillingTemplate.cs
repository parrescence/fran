using Fran.Layout;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Fran.Templates;

/// <summary>
/// A plans &amp; billing template: <see cref="FaSidebarShell"/> wrapped around a header
/// with title and upgrade action, an active plan summary card, payment methods manager,
/// billing address/details, and an invoice history table.
/// </summary>
public sealed class FaBillingTemplate : ComponentBase
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

    [Parameter] public RenderFragment? Sidebar { get; set; }
    [Parameter] public RenderFragment? FooterContent { get; set; }

    [Parameter] public FaNavPosition HeaderPosition { get; set; } = FaNavPosition.Standard;
    [Parameter] public FaNavPosition FooterPosition { get; set; } = FaNavPosition.Standard;
    [Parameter] public FaNavPosition SidebarPosition { get; set; } = FaNavPosition.Standard;
    [Parameter] public bool SidebarCollapsible { get; set; }
    [Parameter] public bool ContainScroll { get; set; }

    /// <summary>Page title, e.g. "Plans &amp; Billing".</summary>
    [Parameter] public string Title { get; set; } = "Plans & Billing";

    /// <summary>Page description.</summary>
    [Parameter] public string? Description { get; set; } = "Manage your team's subscription, payment methods, and invoice receipts.";

    /// <summary>Action in the header, e.g. "Upgrade Plan" or "Contact Sales".</summary>
    [Parameter] public RenderFragment? HeaderAction { get; set; }

    /// <summary>Active plan tier card with usage, limits, and renewal dates.</summary>
    [Parameter] public RenderFragment? CurrentPlanContent { get; set; }

    /// <summary>Payment methods card/list and "Add card" action.</summary>
    [Parameter] public RenderFragment? PaymentMethodsContent { get; set; }

    /// <summary>Billing address, tax ID, and invoice recipient email card.</summary>
    [Parameter] public RenderFragment? BillingDetailsContent { get; set; }

    /// <summary>Invoice history table or list.</summary>
    [Parameter] public RenderFragment? InvoicesContent { get; set; }

    /// <summary>Optional custom or additional content.</summary>
    [Parameter] public RenderFragment? ChildContent { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
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

        builder.AddComponentParameter(21, nameof(FaSidebarShell.ChildContent), (RenderFragment)(body =>
        {
            body.OpenElement(22, "div");
            body.AddAttribute(23, "class", "fa-billing-template");

            // Header
            body.OpenElement(24, "header");
            body.AddAttribute(25, "class", "fa-billing-header");

            body.OpenElement(26, "div");
            body.AddAttribute(27, "class", "fa-billing-title-group");
            body.OpenElement(28, "h1");
            body.AddAttribute(29, "class", "fa-billing-title");
            body.AddContent(30, Title);
            body.CloseElement(); // h1

            if (!string.IsNullOrWhiteSpace(Description))
            {
                body.OpenElement(31, "p");
                body.AddAttribute(32, "class", "fa-billing-desc");
                body.AddContent(33, Description);
                body.CloseElement(); // p
            }
            body.CloseElement(); // title-group

            if (HeaderAction != null)
            {
                body.OpenElement(34, "div");
                body.AddAttribute(35, "class", "fa-billing-header-action");
                body.AddContent(36, HeaderAction);
                body.CloseElement(); // action
            }
            body.CloseElement(); // header

            // Current plan section
            if (CurrentPlanContent != null)
            {
                body.OpenElement(37, "section");
                body.AddAttribute(38, "class", "fa-billing-section fa-billing-plan-section");
                body.OpenElement(39, "h2");
                body.AddAttribute(40, "class", "fa-billing-section-title");
                body.AddContent(41, "Current Plan");
                body.CloseElement(); // h2
                body.AddContent(42, CurrentPlanContent);
                body.CloseElement(); // section
            }

            // Payment methods & billing details 2-col or stack
            if (PaymentMethodsContent != null || BillingDetailsContent != null)
            {
                body.OpenElement(43, "div");
                body.AddAttribute(44, "class", "fa-billing-grid");

                if (PaymentMethodsContent != null)
                {
                    body.OpenElement(45, "section");
                    body.AddAttribute(46, "class", "fa-billing-section fa-billing-payment-section");
                    body.OpenElement(47, "h2");
                    body.AddAttribute(48, "class", "fa-billing-section-title");
                    body.AddContent(49, "Payment Methods");
                    body.CloseElement(); // h2
                    body.AddContent(50, PaymentMethodsContent);
                    body.CloseElement(); // section
                }

                if (BillingDetailsContent != null)
                {
                    body.OpenElement(51, "section");
                    body.AddAttribute(52, "class", "fa-billing-section fa-billing-details-section");
                    body.OpenElement(53, "h2");
                    body.AddAttribute(54, "class", "fa-billing-section-title");
                    body.AddContent(55, "Billing Information");
                    body.CloseElement(); // h2
                    body.AddContent(56, BillingDetailsContent);
                    body.CloseElement(); // section
                }

                body.CloseElement(); // fa-billing-grid
            }

            // Invoices table
            if (InvoicesContent != null)
            {
                body.OpenElement(57, "section");
                body.AddAttribute(58, "class", "fa-billing-section fa-billing-invoices-section");
                body.OpenElement(59, "h2");
                body.AddAttribute(60, "class", "fa-billing-section-title");
                body.AddContent(61, "Invoice History");
                body.CloseElement(); // h2
                body.AddContent(62, InvoicesContent);
                body.CloseElement(); // section
            }

            // Child content
            if (ChildContent != null)
            {
                body.OpenElement(63, "div");
                body.AddAttribute(64, "class", "fa-billing-content");
                body.AddContent(65, ChildContent);
                body.CloseElement(); // content
            }

            body.CloseElement(); // fa-billing-template
        }));

        builder.CloseComponent();
    }
}
