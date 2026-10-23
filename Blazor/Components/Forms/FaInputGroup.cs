using Fran.Rendering;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Fran.Components;

/// <summary>
/// Seamless group joining an input element with buttons and addons on the left,
/// right, or in between. The exterior wrapper defines the group's border radius
/// while interior borders collapse and outer corners curve seamlessly.
/// </summary>
public sealed class FaInputGroup : ComponentBase
{
    /// <summary>Optional label rendered above the input group.</summary>
    [Parameter] public string? Label { get; set; }

    /// <summary>Optional helper text rendered below the input group.</summary>
    [Parameter] public string? HelpText { get; set; }

    /// <summary>XSmall/Small/Medium(default)/Large/XLarge — scales padding and font sizes across children.</summary>
    [Parameter] public FaSize Size { get; set; } = FaSize.Medium;

    /// <summary>Renders with full pill-shaped rounded ends (<c>.fa-input-group-pill</c>).</summary>
    [Parameter] public bool Pill { get; set; }

    /// <summary>Renders as an inline-flex container (<c>.fa-input-group-inline</c>) rather than 100% width.</summary>
    [Parameter] public bool Inline { get; set; }

    /// <summary>Stretches to 100% width below the 720px breakpoint (<c>.fa-responsive</c>).</summary>
    [Parameter] public bool Responsive { get; set; }

    /// <summary>Optional accessible label for the input group container.</summary>
    [Parameter] public string? AriaLabel { get; set; }

    /// <summary>Additional CSS classes applied directly to the .fa-input-group element.</summary>
    [Parameter] public string? CssClass { get; set; }

    /// <summary>CSS class applied to the outer .fa-field container if Label or HelpText is supplied.</summary>
    [Parameter] public string? ContainerCssClass { get; set; }

    /// <summary>Optional content to render at the beginning of the group (before ChildContent).</summary>
    [Parameter] public RenderFragment? Prepend { get; set; }

    /// <summary>Main content (typically an input, or buttons and inputs combined).</summary>
    [Parameter] public RenderFragment? ChildContent { get; set; }

    /// <summary>Optional content to render at the end of the group (after ChildContent).</summary>
    [Parameter] public RenderFragment? Append { get; set; }

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        var hasFieldWrapper = !string.IsNullOrEmpty(Label) || !string.IsNullOrEmpty(HelpText) || !string.IsNullOrEmpty(ContainerCssClass);
        var seq = 0;

        if (hasFieldWrapper)
        {
            builder.OpenElement(seq++, "div");
            builder.AddAttribute(seq++, "class", CssClassNames.Combine("fa-field", Responsive ? "fa-responsive" : null, ContainerCssClass));

            if (!string.IsNullOrEmpty(Label))
            {
                builder.OpenElement(seq++, "label");
                builder.AddAttribute(seq++, "class", "fa-label");
                builder.AddContent(seq++, Label);
                builder.CloseElement();
            }
        }

        builder.OpenElement(seq++, "div");
        builder.AddAttribute(seq++, "class", CssClassNames.Combine(
            "fa-input-group",
            FaSizeClassNames.Class("fa-input-group", Size),
            Pill ? "fa-input-group-pill" : null,
            Inline ? "fa-input-group-inline" : null,
            Responsive ? "fa-responsive" : null,
            CssClass));
        builder.AddAttribute(seq++, "role", "group");

        if (!string.IsNullOrEmpty(AriaLabel))
        {
            builder.AddAttribute(seq++, "aria-label", AriaLabel);
        }

        builder.AddMultipleAttributes(seq++, AdditionalAttributes);

        if (Prepend != null)
        {
            builder.AddContent(seq++, Prepend);
        }

        if (ChildContent != null)
        {
            builder.AddContent(seq++, ChildContent);
        }

        if (Append != null)
        {
            builder.AddContent(seq++, Append);
        }

        builder.CloseElement(); // end .fa-input-group

        if (hasFieldWrapper)
        {
            if (!string.IsNullOrEmpty(HelpText))
            {
                builder.OpenElement(seq++, "div");
                builder.AddAttribute(seq++, "class", "fa-help-text");
                builder.AddAttribute(seq++, "style", "font-size:0.8rem;color:var(--fa-text-muted);margin-top:0.25rem;");
                builder.AddContent(seq++, HelpText);
                builder.CloseElement();
            }

            builder.CloseElement(); // end outer .fa-field
        }
    }
}
