using Fran.Rendering;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Fran.Components;

/// <summary>
/// Static text or icon addon element inside an <see cref="FaInputGroup"/> (e.g. currency symbol, unit, or icon).
/// </summary>
public sealed class FaInputGroupText : ComponentBase
{
    /// <summary>Text content if not using ChildContent.</summary>
    [Parameter] public string? Text { get; set; }

    /// <summary>Child content (text or icons).</summary>
    [Parameter] public RenderFragment? ChildContent { get; set; }

    /// <summary>Additional CSS classes.</summary>
    [Parameter] public string? CssClass { get; set; }

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "span");
        builder.AddAttribute(1, "class", CssClassNames.Combine("fa-input-group-text", CssClass));
        builder.AddMultipleAttributes(2, AdditionalAttributes);

        if (ChildContent != null)
        {
            builder.AddContent(3, ChildContent);
        }
        else if (!string.IsNullOrEmpty(Text))
        {
            builder.AddContent(4, Text);
        }

        builder.CloseElement();
    }
}
