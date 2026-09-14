using Fran.Rendering;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Fran.Components;

/// <summary>
/// Sticky bookmark rail / table of contents navigation. Pinned alongside long-form documents
/// (charters, runbooks, FAQs, how-tos, tutorials) with numbered section badges, smooth scrolling
/// jump links, and active selection state.
/// </summary>
public sealed class FaBookmarkNav : ComponentBase
{
    /// <summary>Section heading above the bookmark list. Defaults to "Contents".</summary>
    [Parameter] public string Title { get; set; } = "Contents";

    /// <summary>Bookmark items to render in an ordered list.</summary>
    [Parameter] public IReadOnlyList<FaBookmarkItem>? Items { get; set; }

    /// <summary>Target element ID of the currently active bookmark item (e.g. "architecture").</summary>
    [Parameter] public string? ActiveId { get; set; }

    /// <summary>Whether the navigation rail pins itself stickily during scroll. Defaults to true.</summary>
    [Parameter] public bool Sticky { get; set; } = true;

    /// <summary>Optional explicit width override (e.g. "250px").</summary>
    [Parameter] public string? Width { get; set; }

    /// <summary>Accessible label for the nav element. Defaults to "Table of contents bookmarks".</summary>
    [Parameter] public string AriaLabel { get; set; } = "Table of contents bookmarks";

    /// <summary>Optional callback invoked when a bookmark is clicked.</summary>
    [Parameter] public EventCallback<FaBookmarkItem> OnItemClick { get; set; }

    /// <summary>Custom CSS class.</summary>
    [Parameter] public string? CssClass { get; set; }

    /// <summary>Arbitrary child content (custom links, filters, or additional controls).</summary>
    [Parameter] public RenderFragment? ChildContent { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "nav");
        builder.AddAttribute(1, "class", CssClassNames.Combine("fa-bookmark-nav", Sticky ? "fa-bookmark-nav-sticky" : null, CssClass));
        builder.AddAttribute(2, "aria-label", AriaLabel);

        if (!string.IsNullOrEmpty(Width))
        {
            builder.AddAttribute(3, "style", $"width:{Width};min-width:{Width};");
        }

        if (!string.IsNullOrWhiteSpace(Title))
        {
            builder.OpenElement(4, "div");
            builder.AddAttribute(5, "class", "fa-bookmark-nav-header");
            builder.OpenElement(6, "span");
            builder.AddAttribute(7, "class", "fa-bookmark-nav-title");
            builder.AddContent(8, Title);
            builder.CloseElement(); // span
            builder.CloseElement(); // div.fa-bookmark-nav-header
        }

        if (Items is { Count: > 0 })
        {
            builder.OpenElement(9, "ol");
            builder.AddAttribute(10, "class", "fa-bookmark-nav-list");

            var seq = 11;
            foreach (var item in Items)
            {
                var isActive = item.IsActive || (!string.IsNullOrEmpty(ActiveId) && string.Equals(item.Id, ActiveId, StringComparison.OrdinalIgnoreCase));
                var href = !string.IsNullOrEmpty(item.Href) ? item.Href : $"#{item.Id}";

                builder.OpenElement(seq++, "li");
                builder.AddAttribute(seq++, "class", "fa-bookmark-nav-item");

                builder.OpenElement(seq++, "a");
                builder.AddAttribute(seq++, "class", CssClassNames.Combine("fa-bookmark-nav-link", isActive ? "fa-bookmark-nav-link-active" : null));
                builder.AddAttribute(seq++, "href", href);
                if (isActive)
                {
                    builder.AddAttribute(seq++, "aria-current", "true");
                }
                if (OnItemClick.HasDelegate)
                {
                    builder.AddAttribute(seq++, "onclick", EventCallback.Factory.Create(this, () => OnItemClick.InvokeAsync(item)));
                }

                if (!string.IsNullOrEmpty(item.Number))
                {
                    builder.OpenElement(seq++, "span");
                    builder.AddAttribute(seq++, "class", "fa-bookmark-nav-num");
                    builder.AddContent(seq++, item.Number);
                    builder.CloseElement();
                }

                builder.OpenElement(seq++, "span");
                builder.AddAttribute(seq++, "class", "fa-bookmark-nav-label");
                builder.AddContent(seq++, item.Title);
                builder.CloseElement();

                if (!string.IsNullOrEmpty(item.Badge))
                {
                    builder.OpenElement(seq++, "span");
                    builder.AddAttribute(seq++, "class", "fa-bookmark-nav-badge");
                    builder.AddContent(seq++, item.Badge);
                    builder.CloseElement();
                }

                builder.CloseElement(); // a
                builder.CloseElement(); // li
            }

            builder.CloseElement(); // ol
        }

        if (ChildContent is not null)
        {
            builder.AddContent(100, ChildContent);
        }

        builder.CloseElement(); // nav
    }
}
