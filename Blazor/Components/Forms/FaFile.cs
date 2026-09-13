using Fran.Rendering;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;

namespace Fran.Components;

/// <summary>
/// File picker. Set <see cref="AsButton"/> to show a styled button (a &lt;label&gt;
/// wired to a visually-hidden &lt;InputFile&gt; via a stable id) instead of the
/// browser's default file input chrome, or set <see cref="DropZone"/> to render an
/// interactive drag-and-drop upload zone.
/// </summary>
public sealed class FaFile : ComponentBase
{
    [Parameter] public EventCallback<InputFileChangeEventArgs> OnChange { get; set; }
    [Parameter] public bool AsButton { get; set; }
    [Parameter] public string ButtonLabel { get; set; } = "Choose file";
    [Parameter] public bool Multiple { get; set; }
    [Parameter] public bool DropZone { get; set; }
    [Parameter] public string? Prompt { get; set; }
    [Parameter] public string? Hint { get; set; }
    [Parameter] public string? Accept { get; set; }
    [Parameter] public bool ShowFileList { get; set; } = true;
    [Parameter] public bool Disabled { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public string? CssClass { get; set; }
    [Parameter(CaptureUnmatchedValues = true)] public IReadOnlyDictionary<string, object>? AdditionalAttributes { get; set; }

    private readonly string _id = $"fa-file-{Guid.NewGuid():N}";

    private Task HandleChange(InputFileChangeEventArgs e) => OnChange.InvokeAsync(e);

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (DropZone)
        {
            builder.OpenComponent<FaFileDropZone>(0);
            builder.AddComponentParameter(1, nameof(FaFileDropZone.OnChange), OnChange);
            builder.AddComponentParameter(2, nameof(FaFileDropZone.Multiple), Multiple);
            if (!string.IsNullOrEmpty(Accept)) builder.AddComponentParameter(3, nameof(FaFileDropZone.Accept), Accept);
            if (!string.IsNullOrEmpty(Prompt)) builder.AddComponentParameter(4, nameof(FaFileDropZone.Prompt), Prompt);
            if (!string.IsNullOrEmpty(Hint)) builder.AddComponentParameter(5, nameof(FaFileDropZone.Hint), Hint);
            if (!string.IsNullOrEmpty(ButtonLabel)) builder.AddComponentParameter(6, nameof(FaFileDropZone.ButtonText), ButtonLabel);
            builder.AddComponentParameter(7, nameof(FaFileDropZone.ShowFileList), ShowFileList);
            builder.AddComponentParameter(8, nameof(FaFileDropZone.Disabled), Disabled);
            if (ChildContent is not null) builder.AddComponentParameter(9, nameof(FaFileDropZone.ChildContent), ChildContent);
            if (!string.IsNullOrEmpty(CssClass)) builder.AddComponentParameter(10, nameof(FaFileDropZone.CssClass), CssClass);
            if (AdditionalAttributes is not null) builder.AddComponentParameter(11, nameof(FaFileDropZone.AdditionalAttributes), AdditionalAttributes);
            builder.CloseComponent();
            return;
        }

        if (AsButton)
        {
            builder.OpenElement(0, "label");
            builder.AddAttribute(1, "for", _id);
            builder.AddAttribute(2, "class", CssClassNames.Combine("fa-btn", "fa-btn-secondary", CssClass));
            builder.AddContent(3, ButtonLabel);
            builder.CloseElement();
        }

        builder.OpenComponent<InputFile>(10);
        builder.AddComponentParameter(11, "id", _id);
        builder.AddComponentParameter(12, "class", AsButton ? "fa-file-hidden" : CssClassNames.Combine("fa-file-input", CssClass));
        builder.AddComponentParameter(13, "multiple", Multiple);
        if (!string.IsNullOrEmpty(Accept))
        {
            builder.AddComponentParameter(14, "accept", Accept);
        }
        if (Disabled)
        {
            builder.AddComponentParameter(15, "disabled", true);
        }
        builder.AddComponentParameter(16, nameof(InputFile.OnChange), EventCallback.Factory.Create<InputFileChangeEventArgs>(this, HandleChange));
        builder.AddMultipleAttributes(17, AdditionalAttributes);
        builder.CloseComponent();
    }
}
