using System.Collections.Generic;
using Bunit;
using Fran.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Xunit;

namespace Fran.Tests;

public class FaFileDropZoneTests : BunitContext
{
    [Fact]
    public void DropZone_RendersDefaultStructure()
    {
        var cut = Render<FaFileDropZone>();

        var wrapper = cut.Find("div.fa-file-dropzone-wrapper");
        Assert.NotNull(wrapper);

        var dropzone = cut.Find("div.fa-file-dropzone");
        Assert.NotNull(dropzone);
        Assert.DoesNotContain("fa-file-dropzone-disabled", dropzone.ClassName);
        Assert.DoesNotContain("fa-file-dropzone-dragover", dropzone.ClassName);

        var input = cut.Find("input[type=file]");
        Assert.Contains("fa-file-dropzone-input", input.ClassName);

        var title = cut.Find("div.fa-file-dropzone-title");
        Assert.Equal("Drag & drop files here, or browse", title.TextContent);

        var btn = cut.Find("span.fa-file-dropzone-btn");
        Assert.Equal("Browse files", btn.TextContent);
    }

    [Fact]
    public void DropZone_CustomPromptAndHint_RendersCorrectly()
    {
        var cut = Render<FaFileDropZone>(p => p
            .Add(x => x.Prompt, "Drop PDF contracts here")
            .Add(x => x.Hint, "PDF up to 25MB")
            .Add(x => x.ButtonText, "Select contract")
            .Add(x => x.Accept, ".pdf")
            .Add(x => x.Multiple, true));

        var title = cut.Find("div.fa-file-dropzone-title");
        Assert.Equal("Drop PDF contracts here", title.TextContent);

        var hint = cut.Find("div.fa-file-dropzone-hint");
        Assert.Equal("PDF up to 25MB", hint.TextContent);

        var btn = cut.Find("span.fa-file-dropzone-btn");
        Assert.Equal("Select contract", btn.TextContent);

        var input = cut.Find("input[type=file]");
        Assert.Equal(".pdf", input.GetAttribute("accept"));
        Assert.True(input.HasAttribute("multiple"));
    }

    [Fact]
    public void DropZone_Disabled_RendersDisabledState()
    {
        var cut = Render<FaFileDropZone>(p => p
            .Add(x => x.Disabled, true));

        var dropzone = cut.Find("div.fa-file-dropzone");
        Assert.Contains("fa-file-dropzone-disabled", dropzone.ClassName);

        var input = cut.Find("input[type=file]");
        Assert.True(input.HasAttribute("disabled"));
    }

    [Fact]
    public void DropZone_DragEnterAndLeave_TogglesDragOverClass()
    {
        var cut = Render<FaFileDropZone>();

        var input = cut.Find("input[type=file]");

        input.TriggerEvent("ondragenter", new DragEventArgs());
        Assert.Contains("fa-file-dropzone-dragover", cut.Find("div.fa-file-dropzone").ClassName);

        input.TriggerEvent("ondragleave", new DragEventArgs());
        Assert.DoesNotContain("fa-file-dropzone-dragover", cut.Find("div.fa-file-dropzone").ClassName);
    }

    [Fact]
    public void FaFile_WithDropZone_RendersFaFileDropZone()
    {
        var cut = Render<FaFile>(p => p
            .Add(x => x.DropZone, true)
            .Add(x => x.Prompt, "Drop receipts here"));

        var dropzone = cut.Find("div.fa-file-dropzone");
        Assert.NotNull(dropzone);

        var title = cut.Find("div.fa-file-dropzone-title");
        Assert.Equal("Drop receipts here", title.TextContent);
    }
}
