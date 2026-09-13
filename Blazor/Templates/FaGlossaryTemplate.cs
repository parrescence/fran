using Fran.Layout;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Fran.Templates;

/// <summary>
/// A glossary / terminology index template: <see cref="FaStandardShell"/> wrapped around
/// a header with title and search input, an alphabetical jump navigation bar (A–Z),
/// and an indexed list of terminology definitions and tags.
/// </summary>
public sealed class FaGlossaryTemplate : ComponentBase
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

    /// <summary>Page heading, e.g. "Glossary of Terms".</summary>
    [Parameter] public string Title { get; set; } = "Glossary";

    /// <summary>Subheading or explanatory note.</summary>
    [Parameter] public string? Description { get; set; } = "An alphabetical index of terms, definitions, and technical concepts.";

    /// <summary>Filter or search input slot.</summary>
    [Parameter] public RenderFragment? SearchContent { get; set; }

    /// <summary>A–Z letter quick-jump navigation bar slot.</summary>
    [Parameter] public RenderFragment? AlphabetNav { get; set; }

    /// <summary>Glossary sections and definition cards.</summary>
    [Parameter] public RenderFragment? ChildContent { get; set; }

    /// <summary>Slot rendered when no matching terms are found.</summary>
    [Parameter] public RenderFragment? EmptyContent { get; set; }

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
            body.AddAttribute(19, "class", "fa-glossary-template");

            // Header
            body.OpenElement(20, "header");
            body.AddAttribute(21, "class", "fa-glossary-header");
            body.OpenElement(22, "h1");
            body.AddAttribute(23, "class", "fa-glossary-title");
            body.AddContent(24, Title);
            body.CloseElement(); // h1

            if (!string.IsNullOrWhiteSpace(Description))
            {
                body.OpenElement(25, "p");
                body.AddAttribute(26, "class", "fa-glossary-desc");
                body.AddContent(27, Description);
                body.CloseElement(); // p
            }

            if (SearchContent != null)
            {
                body.OpenElement(28, "div");
                body.AddAttribute(29, "class", "fa-glossary-search");
                body.AddContent(30, SearchContent);
                body.CloseElement(); // fa-glossary-search
            }
            body.CloseElement(); // header

            // Alphabet jump bar
            if (AlphabetNav != null)
            {
                body.OpenElement(31, "nav");
                body.AddAttribute(32, "class", "fa-glossary-alphabet-nav");
                body.AddAttribute(33, "aria-label", "Alphabetical index");
                body.AddContent(34, AlphabetNav);
                body.CloseElement(); // nav
            }

            // Main body
            body.OpenElement(35, "main");
            body.AddAttribute(36, "class", "fa-glossary-main");
            if (ChildContent != null)
            {
                body.AddContent(37, ChildContent);
            }
            else if (EmptyContent != null)
            {
                body.OpenElement(38, "div");
                body.AddAttribute(39, "class", "fa-glossary-empty");
                body.AddContent(40, EmptyContent);
                body.CloseElement(); // empty
            }
            body.CloseElement(); // main

            body.CloseElement(); // fa-glossary-template
        }));

        builder.CloseComponent();
    }
}
