[← Back to index](index.md)

# FaFile & FaFileDropZone

File picker and drag-and-drop file upload target. Set `DropZone="true"` (or use `<FaFileDropZone>`) for an authentic drag-and-drop upload zone, or `AsButton="true"` to show a styled button instead of the browser's default file-input chrome.

## Usage

### 1. Drag & Drop File Upload Zone

Use `<FaFileDropZone>` (or `<FaFile DropZone="true">`) to render a full drag-and-drop target with hover/drag-over state highlights, file list previews, and remove controls:

```razor
<FaFileDropZone Multiple="true"
                Accept=".pdf,.png,.jpg"
                Prompt="Drag & drop documents here, or browse"
                Hint="PDF, PNG, or JPG up to 10 MB"
                OnChange="HandleFilesSelectedAsync" />

@code {
    private async Task HandleFilesSelectedAsync(InputFileChangeEventArgs e)
    {
        foreach (var file in e.GetMultipleFiles())
        {
            await using var stream = file.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024);
            // Process file...
        }
    }
}
```

### 2. Styled Button

```razor
<FaFile AsButton="true" ButtonLabel="Upload receipt" OnChange="HandleFileSelectedAsync" />
```

### 3. Default Native Input

```razor
<FaFile OnChange="HandleFileSelectedAsync" />
```

## Getting the value

There's no bound `Value` — `OnChange` hands you the raw `InputFileChangeEventArgs` (same as Blazor's own `InputFile.OnChange`), from which you read `.File` (or `.GetMultipleFiles()` when `Multiple="true"`).

`<FaFileDropZone>` also exposes a public `.Files` collection and `.ClearFiles()` method for programmatic manipulation when bound via `@ref`.

## Parameters

| Parameter | Type | Notes |
|---|---|---|
| `OnChange` | `EventCallback<InputFileChangeEventArgs>` | Fired when files are dropped or chosen |
| `DropZone` | `bool` | Renders an interactive drag-and-drop dropzone (`FaFile` only; default `false`) |
| `Prompt` | `string` | Dropzone headline; default `"Drag & drop files here, or browse"` |
| `Hint` | `string?` | Dropzone subtext (e.g. accepted formats or size limit) |
| `ButtonText` / `ButtonLabel` | `string` | Button label text (default `"Browse files"` on dropzone, `"Choose file"` on `FaFile`) |
| `Accept` | `string?` | Comma-separated file extensions or MIME types (e.g. `".pdf,image/*"`) |
| `Multiple` | `bool` | Allows selecting/dropping multiple files |
| `Disabled` | `bool` | Disables user interaction and dims the control |
| `ShowFileList` | `bool` | Displays interactive list of dropped files with remove buttons (default `true`) |
| `ChildContent` | `RenderFragment?` | Custom markup rendered inside the drop target |
| `CssClass` | `string?` | Additional CSS class on the root element |

[← Back to index](index.md)
