using Fran.Layout;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Fran.Templates;

/// <summary>
/// A file manager / drive storage template: <see cref="FaSidebarShell"/> wrapped around
/// a toolbar with breadcrumbs, storage quota indicator, file search, upload/folder action buttons,
/// a folder and file browser grid/table, and a file detail inspector aside panel.
/// </summary>
public sealed class FaFileManagerTemplate : ComponentBase
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

    [Parameter] public RenderFragment? Sidebar { get; set; }
    [Parameter] public RenderFragment? FooterContent { get; set; }

    [Parameter] public FaNavPosition HeaderPosition { get; set; } = FaNavPosition.Standard;
    [Parameter] public FaNavPosition FooterPosition { get; set; } = FaNavPosition.Standard;
    [Parameter] public FaNavPosition SidebarPosition { get; set; } = FaNavPosition.Standard;
    [Parameter] public bool SidebarCollapsible { get; set; }
    [Parameter] public bool ContainScroll { get; set; }

    /// <summary>Header title, e.g. "Files" or "My Documents".</summary>
    [Parameter] public string Title { get; set; } = "Files";

    /// <summary>Folder breadcrumb path navigation above or in the toolbar.</summary>
    [Parameter] public RenderFragment? BreadcrumbContent { get; set; }

    /// <summary>Cloud storage capacity or quota progress indicator.</summary>
    [Parameter] public RenderFragment? StorageQuotaContent { get; set; }

    /// <summary>Search input or filter toolbar for files.</summary>
    [Parameter] public RenderFragment? SearchContent { get; set; }

    /// <summary>Primary upload action button (e.g. "+ Upload Files").</summary>
    [Parameter] public RenderFragment? PrimaryAction { get; set; }

    /// <summary>Secondary actions (e.g. New Folder, Grid/List view toggle).</summary>
    [Parameter] public RenderFragment? SecondaryActions { get; set; }

    /// <summary>Main folder and file grid or table.</summary>
    [Parameter] public RenderFragment? ChildContent { get; set; }

    /// <summary>Side drawer or inspector panel for selected file metadata and actions.</summary>
    [Parameter] public RenderFragment? InspectorContent { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenComponent<FaSidebarShell>(0);
        builder.AddComponentParameter(1, nameof(FaSidebarShell.BrandText), BrandText);
        builder.AddComponentParameter(2, nameof(FaSidebarShell.BrandHref), BrandHref);
        builder.AddComponentParameter(3, nameof(FaSidebarShell.BrandIconUrl), BrandIconUrl);
        builder.AddComponentParameter(4, nameof(FaSidebarShell.IsAuthenticated), IsAuthenticated);
        builder.AddComponentParameter(5, nameof(FaSidebarShell.UserDisplayName), UserDisplayName);
        builder.AddComponentParameter(6, nameof(FaSidebarShell.UserImageUrl), UserImageUrl);
        builder.AddComponentParameter(7, nameof(FaSidebarShell.UserEmail), UserEmail);
        builder.AddComponentParameter(8, nameof(FaSidebarShell.UseAvatarForm), UseAvatarForm);
        builder.AddComponentParameter(9, nameof(FaSidebarShell.ShowUserNameInHeader), ShowUserNameInHeader);
        builder.AddComponentParameter(10, nameof(FaSidebarShell.AccountHref), AccountHref);
        builder.AddComponentParameter(11, nameof(FaSidebarShell.OnAccountClick), OnAccountClick);
        builder.AddComponentParameter(12, nameof(FaSidebarShell.OnLogin), OnLogin);
        builder.AddComponentParameter(13, nameof(FaSidebarShell.OnLogout), OnLogout);
        builder.AddComponentParameter(14, nameof(FaSidebarShell.Sidebar), Sidebar);
        builder.AddComponentParameter(15, nameof(FaSidebarShell.FooterContent), FooterContent);
        builder.AddComponentParameter(16, nameof(FaSidebarShell.HeaderPosition), HeaderPosition);
        builder.AddComponentParameter(17, nameof(FaSidebarShell.FooterPosition), FooterPosition);
        builder.AddComponentParameter(18, nameof(FaSidebarShell.SidebarPosition), SidebarPosition);
        builder.AddComponentParameter(19, nameof(FaSidebarShell.SidebarCollapsible), SidebarCollapsible);
        builder.AddComponentParameter(20, nameof(FaSidebarShell.ContainScroll), ContainScroll);

        builder.AddComponentParameter(21, nameof(FaSidebarShell.ChildContent), (RenderFragment)(body =>
        {
            body.OpenElement(22, "div");
            body.AddAttribute(23, "class", "fa-file-manager-template");

            // Header Toolbar
            body.OpenElement(24, "header");
            body.AddAttribute(25, "class", "fa-file-manager-header");

            body.OpenElement(26, "div");
            body.AddAttribute(27, "class", "fa-file-manager-title-row");

            body.OpenElement(28, "div");
            body.AddAttribute(29, "class", "fa-file-manager-title-group");
            body.OpenElement(30, "h1");
            body.AddAttribute(31, "class", "fa-file-manager-title");
            body.AddContent(32, Title);
            body.CloseElement(); // h1

            if (StorageQuotaContent != null)
            {
                body.OpenElement(33, "div");
                body.AddAttribute(34, "class", "fa-file-manager-quota");
                body.AddContent(35, StorageQuotaContent);
                body.CloseElement(); // quota
            }
            body.CloseElement(); // title-group

            body.OpenElement(36, "div");
            body.AddAttribute(37, "class", "fa-file-manager-actions");
            if (SecondaryActions != null)
            {
                body.OpenElement(38, "div");
                body.AddAttribute(39, "class", "fa-file-manager-secondary-actions");
                body.AddContent(40, SecondaryActions);
                body.CloseElement(); // secondary
            }
            if (PrimaryAction != null)
            {
                body.OpenElement(41, "div");
                body.AddAttribute(42, "class", "fa-file-manager-primary-action");
                body.AddContent(43, PrimaryAction);
                body.CloseElement(); // primary
            }
            body.CloseElement(); // actions
            body.CloseElement(); // title-row

            // Breadcrumbs & Search toolbar row
            if (BreadcrumbContent != null || SearchContent != null)
            {
                body.OpenElement(44, "div");
                body.AddAttribute(45, "class", "fa-file-manager-toolbar");
                if (BreadcrumbContent != null)
                {
                    body.OpenElement(46, "div");
                    body.AddAttribute(47, "class", "fa-file-manager-breadcrumbs");
                    body.AddContent(48, BreadcrumbContent);
                    body.CloseElement(); // breadcrumbs
                }
                if (SearchContent != null)
                {
                    body.OpenElement(49, "div");
                    body.AddAttribute(50, "class", "fa-file-manager-search");
                    body.AddContent(51, SearchContent);
                    body.CloseElement(); // search
                }
                body.CloseElement(); // toolbar
            }

            body.CloseElement(); // header

            // Main browser + Inspector aside
            body.OpenElement(52, "div");
            body.AddAttribute(53, "class", "fa-file-manager-body");

            body.OpenElement(54, "main");
            body.AddAttribute(55, "class", "fa-file-manager-main");
            body.AddContent(56, ChildContent);
            body.CloseElement(); // main

            if (InspectorContent != null)
            {
                body.OpenElement(57, "aside");
                body.AddAttribute(58, "class", "fa-file-manager-inspector");
                body.AddContent(59, InspectorContent);
                body.CloseElement(); // aside
            }

            body.CloseElement(); // body
            body.CloseElement(); // fa-file-manager-template
        }));

        builder.CloseComponent();
    }
}
