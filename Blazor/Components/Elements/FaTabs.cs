using Fran.Icons;
using Fran.Rendering;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;

namespace Fran.Components;

/// <summary>
/// Tab strip or tab section — pass 2+ (Title, Value) options. Supports multiple
/// visual styles via <see cref="FaTabStyle"/>: Underline (default), Folder (file folder tabs),
/// Slide (separated choices sliding to the left / placed first), and Wheel (scrollable
/// thumbwheel switch). If <see cref="ChildContent"/> is provided, it renders the tab strip
/// alongside an attached <c>fa-tabs-panel</c> to form a complete tab section.
/// </summary>
public sealed class FaTabs<TValue> : ComponentBase
{
    [Parameter, EditorRequired] public IReadOnlyList<(string Title, TValue Value)> Options { get; set; } = Array.Empty<(string, TValue)>();
    [Parameter] public TValue Value { get; set; } = default!;
    [Parameter] public EventCallback<TValue> ValueChanged { get; set; }
    [Parameter] public FaTabStyle Style { get; set; } = FaTabStyle.Underline;

    /// <summary>
    /// Alias parameter for <see cref="Style"/> supporting plural enum usage.
    /// </summary>
    [Parameter] public FaTabsStyle? TabsStyle { get; set; }

    /// <summary>
    /// Optional child content rendered inside an attached <c>fa-tabs-panel</c> content section.
    /// When omitted, <see cref="FaTabs{TValue}"/> renders solely the tab strip.
    /// </summary>
    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Parameter] public string? CssClass { get; set; }

    private ElementReference[] _tabRefs = Array.Empty<ElementReference>();

    protected override void OnParametersSet()
    {
        if (TabsStyle.HasValue)
            Style = (FaTabStyle)TabsStyle.Value;

        if (Options is null || Options.Count < 2)
            throw new ArgumentException("FaTabs requires at least two Options.", nameof(Options));

        if (_tabRefs.Length != Options.Count)
            _tabRefs = new ElementReference[Options.Count];
    }

    private async Task SelectAsync(int index)
    {
        var newValue = Options[index].Value;
        if (!EqualityComparer<TValue>.Default.Equals(Value, newValue))
        {
            Value = newValue;
            await ValueChanged.InvokeAsync(Value);
        }
    }

    private async Task SelectAndFocusAsync(int index)
    {
        await SelectAsync(index);
        if (index >= 0 && index < _tabRefs.Length)
        {
            await _tabRefs[index].FocusAsync();
        }
    }

    // Left/right and up/down (and Home/End) move both the selection and focus together.
    private async Task HandleKeyDown(KeyboardEventArgs e)
    {
        var currentIndex = IndexOfCurrentValue();
        var fallback = currentIndex < 0 ? 0 : currentIndex;

        int? targetIndex = e.Key switch
        {
            "ArrowLeft" or "ArrowUp" => Math.Max(0, fallback - 1),
            "ArrowRight" or "ArrowDown" => Math.Min(Options.Count - 1, fallback + 1),
            "Home" => 0,
            "End" => Options.Count - 1,
            _ => null
        };

        if (targetIndex is int index)
        {
            await SelectAsync(index);
            if (index >= 0 && index < _tabRefs.Length)
            {
                await _tabRefs[index].FocusAsync();
            }
        }
    }

    private async Task HandleWheel(WheelEventArgs e)
    {
        if (Style != FaTabStyle.Wheel && Style != FaTabStyle.Thumbwheel)
            return;

        var step = (e.DeltaY > 0 || e.DeltaX > 0) ? 1 : ((e.DeltaY < 0 || e.DeltaX < 0) ? -1 : 0);
        if (step != 0)
        {
            await StepWheelAsync(step);
        }
    }

    private async Task StepWheelAsync(int delta)
    {
        var currentIndex = IndexOfCurrentValue();
        var fallback = currentIndex < 0 ? 0 : currentIndex;
        var targetIndex = Math.Clamp(fallback + delta, 0, Options.Count - 1);
        if (targetIndex != currentIndex)
        {
            await SelectAndFocusAsync(targetIndex);
        }
    }

    private int IndexOfCurrentValue()
    {
        for (var i = 0; i < Options.Count; i++)
        {
            if (EqualityComparer<TValue>.Default.Equals(Options[i].Value, Value))
                return i;
        }
        return -1;
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        var styleSuffix = Style switch
        {
            FaTabStyle.Folder => "folder",
            FaTabStyle.Slide => "slide",
            FaTabStyle.Wheel or FaTabStyle.Thumbwheel => "wheel",
            _ => "underline"
        };
        var styleClass = $"fa-tabs-{styleSuffix}";

        if (ChildContent is not null)
        {
            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", CssClassNames.Combine("fa-tabs-section", $"fa-tabs-section-{styleSuffix}", CssClass));
            RenderTabStrip(builder, 2, styleClass, null);

            builder.OpenElement(50, "div");
            builder.AddAttribute(51, "class", "fa-tabs-panel");
            builder.AddAttribute(52, "role", "tabpanel");
            builder.AddContent(53, ChildContent);
            builder.CloseElement(); // fa-tabs-panel

            builder.CloseElement(); // fa-tabs-section
        }
        else
        {
            RenderTabStrip(builder, 0, styleClass, CssClass);
        }
    }

    private void RenderTabStrip(RenderTreeBuilder builder, int seq, string styleClass, string? extraClass)
    {
        var isWheel = Style is FaTabStyle.Wheel or FaTabStyle.Thumbwheel;

        builder.OpenElement(seq++, "div");
        builder.AddAttribute(seq++, "class", CssClassNames.Combine("fa-tabs", styleClass, extraClass));

        if (isWheel)
        {
            builder.AddAttribute(seq++, "role", "region");
            builder.AddAttribute(seq++, "aria-label", "Thumbwheel tab selector");
            builder.AddAttribute(seq++, "onwheel", EventCallback.Factory.Create<WheelEventArgs>(this, HandleWheel));

            var currentIndex = IndexOfCurrentValue();
            var canPrev = currentIndex > 0;
            var canNext = currentIndex < Options.Count - 1 && currentIndex >= 0;

            // Previous stepper button
            builder.OpenElement(seq++, "button");
            builder.AddAttribute(seq++, "type", "button");
            builder.AddAttribute(seq++, "class", "fa-tabs-wheel-btn fa-tabs-wheel-prev");
            builder.AddAttribute(seq++, "aria-label", "Previous tab");
            builder.AddAttribute(seq++, "disabled", !canPrev);
            builder.AddAttribute(seq++, "onclick", EventCallback.Factory.Create(this, () => StepWheelAsync(-1)));
            builder.OpenComponent<FaIcon>(seq++);
            builder.AddComponentParameter(seq++, nameof(FaIcon.Name), FaIconName.ChevronLeft);
            builder.AddComponentParameter(seq++, nameof(FaIcon.Size), 16);
            builder.CloseComponent();
            builder.CloseElement(); // button.fa-tabs-wheel-prev

            // Viewport
            builder.OpenElement(seq++, "div");
            builder.AddAttribute(seq++, "class", "fa-tabs-wheel-viewport");

            // Alignment indicator mark
            builder.OpenElement(seq++, "div");
            builder.AddAttribute(seq++, "class", "fa-tabs-wheel-indicator");
            builder.AddAttribute(seq++, "aria-hidden", "true");
            builder.CloseElement(); // fa-tabs-wheel-indicator

            // Rotary track
            builder.OpenElement(seq++, "div");
            builder.AddAttribute(seq++, "class", "fa-tabs-wheel-track");
            builder.AddAttribute(seq++, "role", "tablist");
            builder.AddAttribute(seq++, "onkeydown", EventCallback.Factory.Create<KeyboardEventArgs>(this, HandleKeyDown));

            RenderTabs(builder, ref seq);

            builder.CloseElement(); // fa-tabs-wheel-track
            builder.CloseElement(); // fa-tabs-wheel-viewport

            // Next stepper button
            builder.OpenElement(seq++, "button");
            builder.AddAttribute(seq++, "type", "button");
            builder.AddAttribute(seq++, "class", "fa-tabs-wheel-btn fa-tabs-wheel-next");
            builder.AddAttribute(seq++, "aria-label", "Next tab");
            builder.AddAttribute(seq++, "disabled", !canNext);
            builder.AddAttribute(seq++, "onclick", EventCallback.Factory.Create(this, () => StepWheelAsync(1)));
            builder.OpenComponent<FaIcon>(seq++);
            builder.AddComponentParameter(seq++, nameof(FaIcon.Name), FaIconName.ChevronRight);
            builder.AddComponentParameter(seq++, nameof(FaIcon.Size), 16);
            builder.CloseComponent();
            builder.CloseElement(); // button.fa-tabs-wheel-next
        }
        else
        {
            builder.AddAttribute(seq++, "role", "tablist");
            builder.AddAttribute(seq++, "onkeydown", EventCallback.Factory.Create<KeyboardEventArgs>(this, HandleKeyDown));

            RenderTabs(builder, ref seq);
        }

        builder.CloseElement(); // div.fa-tabs
    }

    private void RenderTabs(RenderTreeBuilder builder, ref int seq)
    {
        for (var i = 0; i < Options.Count; i++)
        {
            var index = i;
            var option = Options[index];
            var isActive = EqualityComparer<TValue>.Default.Equals(option.Value, Value);

            builder.OpenElement(seq++, "button");
            builder.SetKey(index);
            builder.AddAttribute(seq++, "type", "button");
            builder.AddAttribute(seq++, "class", CssClassNames.Combine("fa-tabs-tab", isActive ? "fa-tabs-tab-active" : null));
            builder.AddAttribute(seq++, "role", "tab");
            builder.AddAttribute(seq++, "aria-selected", isActive ? "true" : "false");
            builder.AddAttribute(seq++, "tabindex", isActive ? "0" : "-1");
            builder.AddAttribute(seq++, "onclick", EventCallback.Factory.Create(this, () => SelectAndFocusAsync(index)));
            builder.AddElementReferenceCapture(seq++, elementReference => _tabRefs[index] = elementReference);
            builder.AddContent(seq++, option.Title);
            builder.CloseElement(); // button.fa-tabs-tab
        }
    }
}
