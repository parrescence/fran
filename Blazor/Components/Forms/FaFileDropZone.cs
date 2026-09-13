using Fran.Icons;
using Fran.Rendering;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;

namespace Fran.Components;

/// <summary>
/// Drag-and-drop file upload zone. Wraps Blazor's <see cref="InputFile"/> with a styled
/// drop target supporting drag hover feedback, file size/type constraints, and an optional
/// interactive file list with remove controls.
/// </summary>
public sealed class FaFileDropZone : ComponentBase
{
    [Parameter] public EventCallback<InputFileChangeEventArgs> OnChange { get; set; }
    [Parameter] public bool Multiple { get; set; }
    [Parameter] public string? Accept { get; set; }
    [Parameter] public string Prompt { get; set; } = "Drag & drop files here, or browse";
    [Parameter] public string? Hint { get; set; }
    [Parameter] public string ButtonText { get; set; } = "Browse files";
    [Parameter] public bool Disabled { get; set; }
    [Parameter] public bool ShowFileList { get; set; } = true;
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public string? CssClass { get; set; }
    [Parameter(CaptureUnmatchedValues = true)] public IReadOnlyDictionary<string, object>? AdditionalAttributes { get; set; }

    private readonly string _id = $"fa-dropzone-{Guid.NewGuid():N}";
    private bool _isDragOver;
    private readonly List<IBrowserFile> _files = new();

    /// <summary>
    /// Gets the current list of files selected or dropped into the drop zone.
    /// </summary>
    public IReadOnlyList<IBrowserFile> Files => _files;

    /// <summary>
    /// Clears the currently selected files.
    /// </summary>
    public void ClearFiles()
    {
        _files.Clear();
        StateHasChanged();
    }

    private async Task HandleChange(InputFileChangeEventArgs e)
    {
        _isDragOver = false;

        if (Multiple)
        {
            var newFiles = e.GetMultipleFiles();
            _files.AddRange(newFiles);
        }
        else
        {
            _files.Clear();
            _files.Add(e.File);
        }

        await OnChange.InvokeAsync(e);
    }

    private void RemoveFile(int index)
    {
        if (index >= 0 && index < _files.Count)
        {
            _files.RemoveAt(index);
        }
    }

    private void HandleDragEnter(DragEventArgs e)
    {
        if (!Disabled)
        {
            _isDragOver = true;
        }
    }

    private void HandleDragLeave(DragEventArgs e)
    {
        _isDragOver = false;
    }

    private void HandleDrop(DragEventArgs e)
    {
        _isDragOver = false;
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        var seq = 0;

        builder.OpenElement(seq++, "div");
        builder.AddAttribute(seq++, "class", CssClassNames.Combine("fa-file-dropzone-wrapper", CssClass));
        builder.AddMultipleAttributes(seq++, AdditionalAttributes);

        // Dropzone box
        builder.OpenElement(seq++, "div");
        var dropzoneClass = CssClassNames.Combine(
            "fa-file-dropzone",
            _isDragOver ? "fa-file-dropzone-dragover" : null,
            Disabled ? "fa-file-dropzone-disabled" : null
        );
        builder.AddAttribute(seq++, "class", dropzoneClass);

        // Native InputFile covering the entire dropzone to handle clicks and OS file drops
        builder.OpenComponent<InputFile>(seq++);
        builder.AddComponentParameter(seq++, "id", _id);
        builder.AddComponentParameter(seq++, "class", "fa-file-dropzone-input");
        builder.AddComponentParameter(seq++, "multiple", Multiple);
        if (!string.IsNullOrEmpty(Accept))
        {
            builder.AddComponentParameter(seq++, "accept", Accept);
        }
        builder.AddComponentParameter(seq++, "disabled", Disabled);
        builder.AddComponentParameter(seq++, nameof(InputFile.OnChange), EventCallback.Factory.Create<InputFileChangeEventArgs>(this, HandleChange));
        builder.AddAttribute(seq++, "ondragenter", EventCallback.Factory.Create<DragEventArgs>(this, HandleDragEnter));
        builder.AddAttribute(seq++, "ondragleave", EventCallback.Factory.Create<DragEventArgs>(this, HandleDragLeave));
        builder.AddAttribute(seq++, "ondrop", EventCallback.Factory.Create<DragEventArgs>(this, HandleDrop));
        builder.AddEventPreventDefaultAttribute(seq++, "ondragover", true);
        builder.CloseComponent();

        // Visual content inside dropzone
        builder.OpenElement(seq++, "div");
        builder.AddAttribute(seq++, "class", "fa-file-dropzone-content");

        if (ChildContent is not null)
        {
            builder.AddContent(seq++, ChildContent);
        }
        else
        {
            // Icon
            builder.OpenElement(seq++, "div");
            builder.AddAttribute(seq++, "class", "fa-file-dropzone-icon");
            builder.OpenComponent<FaIcon>(seq++);
            builder.AddComponentParameter(seq++, nameof(FaIcon.Name), FaIconName.Upload);
            builder.AddComponentParameter(seq++, nameof(FaIcon.Size), 24);
            builder.AddComponentParameter(seq++, nameof(FaIcon.Color), FaIconColor.Black);
            builder.CloseComponent();
            builder.CloseElement(); // .fa-file-dropzone-icon

            // Title / Prompt
            builder.OpenElement(seq++, "div");
            builder.AddAttribute(seq++, "class", "fa-file-dropzone-title");
            builder.AddContent(seq++, Prompt);
            builder.CloseElement();

            // Optional Hint
            if (!string.IsNullOrEmpty(Hint))
            {
                builder.OpenElement(seq++, "div");
                builder.AddAttribute(seq++, "class", "fa-file-dropzone-hint");
                builder.AddContent(seq++, Hint);
                builder.CloseElement();
            }

            // Browse button
            if (!string.IsNullOrEmpty(ButtonText))
            {
                builder.OpenElement(seq++, "span");
                builder.AddAttribute(seq++, "class", "fa-btn fa-btn-sm fa-btn-secondary fa-file-dropzone-btn");
                builder.AddContent(seq++, ButtonText);
                builder.CloseElement();
            }
        }

        builder.CloseElement(); // .fa-file-dropzone-content
        builder.CloseElement(); // .fa-file-dropzone

        // Selected files list
        if (ShowFileList && _files.Count > 0)
        {
            builder.OpenElement(seq++, "div");
            builder.AddAttribute(seq++, "class", "fa-file-list");

            for (var i = 0; i < _files.Count; i++)
            {
                var fileIndex = i;
                var file = _files[i];

                builder.OpenElement(seq++, "div");
                builder.AddAttribute(seq++, "class", "fa-file-item");

                // Info: Icon + Name + Size
                builder.OpenElement(seq++, "div");
                builder.AddAttribute(seq++, "class", "fa-file-item-info");

                builder.OpenComponent<FaIcon>(seq++);
                builder.AddComponentParameter(seq++, nameof(FaIcon.Name), FaIconName.Document);
                builder.AddComponentParameter(seq++, nameof(FaIcon.Size), 16);
                builder.AddComponentParameter(seq++, nameof(FaIcon.Color), FaIconColor.Black);
                builder.CloseComponent();

                builder.OpenElement(seq++, "span");
                builder.AddAttribute(seq++, "class", "fa-file-item-name");
                builder.AddContent(seq++, file.Name);
                builder.CloseElement();

                builder.OpenElement(seq++, "span");
                builder.AddAttribute(seq++, "class", "fa-file-item-size");
                builder.AddContent(seq++, $"({FormatBytes(file.Size)})");
                builder.CloseElement();

                builder.CloseElement(); // .fa-file-item-info

                // Remove button
                builder.OpenElement(seq++, "button");
                builder.AddAttribute(seq++, "type", "button");
                builder.AddAttribute(seq++, "class", "fa-file-item-remove");
                builder.AddAttribute(seq++, "title", "Remove file");
                builder.AddAttribute(seq++, "aria-label", $"Remove {file.Name}");
                builder.AddAttribute(seq++, "onclick", EventCallback.Factory.Create(this, () => RemoveFile(fileIndex)));

                builder.OpenComponent<FaIcon>(seq++);
                builder.AddComponentParameter(seq++, nameof(FaIcon.Name), FaIconName.Cancel);
                builder.AddComponentParameter(seq++, nameof(FaIcon.Size), 14);
                builder.AddComponentParameter(seq++, nameof(FaIcon.Color), FaIconColor.Black);
                builder.CloseComponent();

                builder.CloseElement(); // button.fa-file-item-remove

                builder.CloseElement(); // .fa-file-item
            }

            builder.CloseElement(); // .fa-file-list
        }

        builder.CloseElement(); // .fa-file-dropzone-wrapper
    }

    private static string FormatBytes(long bytes)
    {
        if (bytes < 1024) return $"{bytes} B";
        if (bytes < 1024 * 1024) return $"{bytes / 1024.0:F1} KB";
        if (bytes < 1024 * 1024 * 1024) return $"{bytes / (1024.0 * 1024.0):F1} MB";
        return $"{bytes / (1024.0 * 1024.0 * 1024.0):F1} GB";
    }
}
