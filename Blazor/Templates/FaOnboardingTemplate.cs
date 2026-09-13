using Fran.Layout;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Fran.Templates;

/// <summary>
/// An onboarding / wizard flow template: <see cref="FaStandardShell"/> wrapped around
/// a step progress indicator, focused card container with step title and description,
/// interactive step content/form, and a footer toolbar with Back, Skip, and Continue actions.
/// </summary>
public sealed class FaOnboardingTemplate : ComponentBase
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

    /// <summary>Global onboarding title or app welcome message.</summary>
    [Parameter] public string Title { get; set; } = "Getting Started";

    /// <summary>Optional global onboarding subtitle.</summary>
    [Parameter] public string? Description { get; set; }

    /// <summary>Horizontal step bar or progress indicator slot.</summary>
    [Parameter] public RenderFragment? StepsIndicator { get; set; }

    /// <summary>Current active step index (1-based).</summary>
    [Parameter] public int CurrentStep { get; set; } = 1;

    /// <summary>Total number of steps in the onboarding wizard.</summary>
    [Parameter] public int TotalSteps { get; set; } = 4;

    /// <summary>Current step heading, e.g. "Create your first team".</summary>
    [Parameter] public string? StepTitle { get; set; }

    /// <summary>Current step instructions or guidance.</summary>
    [Parameter] public string? StepDescription { get; set; }

    /// <summary>Primary content or form fields for the current step.</summary>
    [Parameter] public RenderFragment? ChildContent { get; set; }

    /// <summary>Back button slot rendered in the step navigation bar.</summary>
    [Parameter] public RenderFragment? BackAction { get; set; }

    /// <summary>Skip link or button slot.</summary>
    [Parameter] public RenderFragment? SkipAction { get; set; }

    /// <summary>Primary forward action button slot (e.g. Next, Continue, or Launch).</summary>
    [Parameter] public RenderFragment? ContinueAction { get; set; }

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
            body.AddAttribute(19, "class", "fa-onboarding-template");

            // Progress Header
            body.OpenElement(20, "header");
            body.AddAttribute(21, "class", "fa-onboarding-header");

            if (!string.IsNullOrWhiteSpace(Title))
            {
                body.OpenElement(22, "h1");
                body.AddAttribute(23, "class", "fa-onboarding-brand-title");
                body.AddContent(24, Title);
                body.CloseElement(); // h1
            }

            if (!string.IsNullOrWhiteSpace(Description))
            {
                body.OpenElement(25, "p");
                body.AddAttribute(26, "class", "fa-onboarding-brand-desc");
                body.AddContent(27, Description);
                body.CloseElement(); // p
            }

            if (StepsIndicator != null)
            {
                body.OpenElement(28, "div");
                body.AddAttribute(29, "class", "fa-onboarding-indicator");
                body.AddContent(30, StepsIndicator);
                body.CloseElement(); // indicator
            }
            else if (TotalSteps > 1)
            {
                body.OpenElement(31, "div");
                body.AddAttribute(32, "class", "fa-onboarding-step-counter");
                body.AddContent(33, $"Step {CurrentStep} of {TotalSteps}");
                body.CloseElement(); // counter
            }
            body.CloseElement(); // header

            // Main step card
            body.OpenElement(34, "main");
            body.AddAttribute(35, "class", "fa-onboarding-card");

            if (!string.IsNullOrWhiteSpace(StepTitle) || !string.IsNullOrWhiteSpace(StepDescription))
            {
                body.OpenElement(36, "div");
                body.AddAttribute(37, "class", "fa-onboarding-step-header");

                if (!string.IsNullOrWhiteSpace(StepTitle))
                {
                    body.OpenElement(38, "h2");
                    body.AddAttribute(39, "class", "fa-onboarding-step-title");
                    body.AddContent(40, StepTitle);
                    body.CloseElement(); // h2
                }

                if (!string.IsNullOrWhiteSpace(StepDescription))
                {
                    body.OpenElement(41, "p");
                    body.AddAttribute(42, "class", "fa-onboarding-step-desc");
                    body.AddContent(43, StepDescription);
                    body.CloseElement(); // p
                }
                body.CloseElement(); // step-header
            }

            body.OpenElement(44, "div");
            body.AddAttribute(45, "class", "fa-onboarding-step-body");
            body.AddContent(46, ChildContent);
            body.CloseElement(); // step-body

            // Footer actions
            if (BackAction != null || SkipAction != null || ContinueAction != null)
            {
                body.OpenElement(47, "footer");
                body.AddAttribute(48, "class", "fa-onboarding-nav");

                body.OpenElement(49, "div");
                body.AddAttribute(50, "class", "fa-onboarding-nav-left");
                if (BackAction != null)
                {
                    body.AddContent(51, BackAction);
                }
                body.CloseElement(); // left

                body.OpenElement(52, "div");
                body.AddAttribute(53, "class", "fa-onboarding-nav-right");
                if (SkipAction != null)
                {
                    body.AddContent(54, SkipAction);
                }
                if (ContinueAction != null)
                {
                    body.AddContent(55, ContinueAction);
                }
                body.CloseElement(); // right

                body.CloseElement(); // nav
            }

            body.CloseElement(); // fa-onboarding-card
            body.CloseElement(); // fa-onboarding-template
        }));

        builder.CloseComponent();
    }
}
