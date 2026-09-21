using Fran.Rendering;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Fran.Components;

/// <summary>
/// A switcher component for the application-wide font style axis (<c>data-fa-font-style</c>).
/// Supports 10 distinct typographic styles:
/// <list type="bullet">
/// <item><term>Flow</term><description>Warm, rounded modern look ('Baloo 2', default)</description></item>
/// <item><term>DOS</term><description>Retro PC / MS-DOS terminal monospace with zero radii</description></item>
/// <item><term>CLI</term><description>Modern developer console monospace</description></item>
/// <item><term>Elementary</term><description>Playful primary schoolbook cursive/handwriting</description></item>
/// <item><term>College</term><description>Varsity athletic collegiate slab serif</description></item>
/// <item><term>Flowing</term><description>Fluid, elegant cursive/script display</description></item>
/// <item><term>Water</term><description>Aqueous fluid droplet curves and tranquil geometry</description></item>
/// <item><term>Rock</term><description>Brutalist, heavy chiseled stone display</description></item>
/// <item><term>Comical</term><description>Comic book / cartoon pop-art typography</description></item>
/// <item><term>Contrasting</term><description>Musical octave harmonic contrast: Didone serif display paired with clean technical body</description></item>
/// </list>
/// Can be rendered as a dropdown (default) or a button group via <see cref="AsDropdown"/>.
/// </summary>
public sealed class FaFontStyleSwitcher : ComponentBase
{
    /// <summary>
    /// When true (default), renders a compact &lt;select&gt; dropdown.
    /// When false, renders a segmented row of buttons.
    /// </summary>
    [Parameter] public bool AsDropdown { get; set; } = true;

    /// <summary>Optional custom CSS class.</summary>
    [Parameter] public string? CssClass { get; set; }

    private static readonly (string Value, string Label)[] Styles =
    [
        ("flow", "Flow (Default)"),
        ("dos", "DOS (Retro PC)"),
        ("cli", "CLI (Terminal)"),
        ("elementary", "Elementary School"),
        ("college", "College (Varsity)"),
        ("flowing", "Flowing (Script)"),
        ("water", "Water (Fluid)"),
        ("rock", "Rock (Heavy)"),
        ("comical", "Comical (Cartoon)"),
        ("contrasting", "Contrasting (Octave Harmonic)"),
        ("executive", "Executive (Command Center)")
    ];

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (AsDropdown)
        {
            builder.OpenElement(0, "select");
            builder.AddAttribute(1, "class", CssClassNames.Combine("fa-font-style-select", CssClass));
            builder.AddAttribute(2, "data-font-style-select", "");
            builder.AddAttribute(3, "aria-label", "Font style");
            builder.AddAttribute(4, "onchange", "faSetFontStyle(this.value)");

            var seq = 5;
            foreach (var (value, label) in Styles)
            {
                builder.OpenElement(seq++, "option");
                builder.AddAttribute(seq++, "value", value);
                builder.AddContent(seq++, label);
                builder.CloseElement(); // option
            }

            builder.CloseElement(); // select
        }
        else
        {
            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", CssClassNames.Combine("fa-font-style-switcher", CssClass));
            builder.AddAttribute(2, "role", "group");
            builder.AddAttribute(3, "aria-label", "Font style");

            var seq = 4;
            foreach (var (value, label) in Styles)
            {
                builder.OpenElement(seq++, "button");
                builder.AddAttribute(seq++, "type", "button");
                builder.AddAttribute(seq++, "class", "fa-font-style-btn");
                builder.AddAttribute(seq++, "data-font-style-btn", value);
                builder.AddAttribute(seq++, "aria-pressed", "false");
                builder.AddAttribute(seq++, "onclick", $"faSetFontStyle('{value}')");
                builder.AddContent(seq++, label);
                builder.CloseElement(); // button
            }

            builder.CloseElement(); // div
        }
    }
}
