using Fran.Rendering;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Fran.Components;

/// <summary>
/// Declarative bookmark link item for use inside <see cref="FaBookmarkNav"/>'s ChildContent.
/// </summary>
public sealed class FaBookmarkLink : ComponentBase
{
    /// <summary>Target anchor or URL (e.g. "#brand").</summary>
    [Parameter, EditorRequired] public string Href { get; set; } = "";

    /// <summary>Optional section index or ledger number (e.g. "01", "02", "§1").</summary>
    [Parameter] public string? Number { get; set; }

    /// <summary>Display label of the link.</summary>
    [Parameter] public string? Text { get; set; }

    /// <summary>Optional status pill or badge text (e.g. "New", "WIP").</summary>
    [Parameter] public string? Badge { get; set; }

    /// <summary>Whether this item is currently active / selected.</summary>
    [Parameter] public bool IsActive { get; set; }

    /// <summary>Custom CSS class.</summary>
    [Parameter] public string? CssClass { get; set; }

    /// <summary>Arbitrary child content for custom label markup.</summary>
    [Parameter] public RenderFragment? ChildContent { get; set; }

    /// <summary>Optional click callback.</summary>
    [Parameter] public EventCallback OnClick { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "li");
        builder.AddAttribute(1, "class", "fa-bookmark-nav-item");

        builder.OpenElement(2, "a");
        builder.AddAttribute(3, "class", CssClassNames.Combine("fa-bookmark-nav-link", IsActive ? "fa-bookmark-nav-link-active" : null, CssClass));
        builder.AddAttribute(4, "href", Href);
        if (IsActive)
        {
            builder.AddAttribute(5, "aria-current", "true");
        }
        if (OnClick.HasDelegate)
        {
            builder.AddAttribute(6, "onclick", OnClick);
        }

        if (!string.IsNullOrEmpty(Number))
        {
            builder.OpenElement(7, "span");
            builder.AddAttribute(8, "class", "fa-bookmark-nav-num");
            builder.AddContent(9, Number);
            builder.CloseElement();
        }

        builder.OpenElement(10, "span");
        builder.AddAttribute(11, "class", "fa-bookmark-nav-label");
        if (ChildContent is not null)
        {
            builder.AddContent(12, ChildContent);
        }
        else
        {
            builder.AddContent(13, Text);
        }
        builder.CloseElement();

        if (!string.IsNullOrEmpty(Badge))
        {
            builder.OpenElement(14, "span");
            builder.AddAttribute(15, "class", "fa-bookmark-nav-badge");
            builder.AddContent(16, Badge);
            builder.CloseElement();
        }

        builder.CloseElement(); // a
        builder.CloseElement(); // li
    }
}
