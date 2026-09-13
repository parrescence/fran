using Fran.Layout;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Fran.Templates;

/// <summary>
/// A blog post / single-article template: <see cref="FaStandardShell"/> wrapped around
/// a back navigation link, article header with title, subtitle, author, publication date,
/// and reading time, optional hero cover image, structured article body, author bio card,
/// share action links, and related posts at the bottom.
/// </summary>
public sealed class FaBlogPostTemplate : ComponentBase
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
    [Parameter] public RenderFragment? FooterContent { get; set; }

    [Parameter] public FaNavPosition HeaderPosition { get; set; } = FaNavPosition.Standard;
    [Parameter] public FaNavPosition FooterPosition { get; set; } = FaNavPosition.Standard;

    /// <summary>Back navigation URL (e.g. "/blog").</summary>
    [Parameter] public string? BackHref { get; set; }

    /// <summary>Label for the back navigation link.</summary>
    [Parameter] public string BackText { get; set; } = "← Back to Blog";

    /// <summary>Custom back navigation slot if more control is needed.</summary>
    [Parameter] public RenderFragment? BackAction { get; set; }

    /// <summary>Category or topic pill badge.</summary>
    [Parameter] public RenderFragment? CategoryBadge { get; set; }

    /// <summary>Main article title.</summary>
    [Parameter] public string Title { get; set; } = "Post Title";

    /// <summary>Article subtitle or deck.</summary>
    [Parameter] public string? Subtitle { get; set; }

    /// <summary>Publication date string (e.g. "October 14, 2026").</summary>
    [Parameter] public string? PublishedDate { get; set; }

    /// <summary>Estimated reading time (e.g. "6 min read").</summary>
    [Parameter] public string? ReadTime { get; set; }

    /// <summary>Author details slot (e.g. avatar, name, handle).</summary>
    [Parameter] public RenderFragment? AuthorContent { get; set; }

    /// <summary>Hero cover image URL.</summary>
    [Parameter] public string? CoverImageUrl { get; set; }

    /// <summary>Alt text for the cover image.</summary>
    [Parameter] public string? CoverImageAlt { get; set; }

    /// <summary>Main article body content.</summary>
    [Parameter] public RenderFragment? ChildContent { get; set; }

    /// <summary>Author bio box displayed after the article body.</summary>
    [Parameter] public RenderFragment? AuthorBio { get; set; }

    /// <summary>Social sharing or bookmark action buttons.</summary>
    [Parameter] public RenderFragment? ShareContent { get; set; }

    /// <summary>Related or recommended articles section.</summary>
    [Parameter] public RenderFragment? RelatedPosts { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenComponent<FaStandardShell>(0);
        builder.AddComponentParameter(1, nameof(FaStandardShell.BrandText), BrandText);
        builder.AddComponentParameter(2, nameof(FaStandardShell.BrandHref), BrandHref);
        builder.AddComponentParameter(3, nameof(FaStandardShell.BrandIconUrl), BrandIconUrl);
        builder.AddComponentParameter(4, nameof(FaStandardShell.IsAuthenticated), IsAuthenticated);
        builder.AddComponentParameter(5, nameof(FaStandardShell.UserDisplayName), UserDisplayName);
        builder.AddComponentParameter(6, nameof(FaStandardShell.UserImageUrl), UserImageUrl);
        builder.AddComponentParameter(7, nameof(FaStandardShell.UserEmail), UserEmail);
        builder.AddComponentParameter(8, nameof(FaStandardShell.UseAvatarForm), UseAvatarForm);
        builder.AddComponentParameter(9, nameof(FaStandardShell.ShowUserNameInHeader), ShowUserNameInHeader);
        builder.AddComponentParameter(10, nameof(FaStandardShell.AccountHref), AccountHref);
        builder.AddComponentParameter(11, nameof(FaStandardShell.OnAccountClick), OnAccountClick);
        builder.AddComponentParameter(12, nameof(FaStandardShell.OnLogin), OnLogin);
        builder.AddComponentParameter(13, nameof(FaStandardShell.OnLogout), OnLogout);
        builder.AddComponentParameter(14, nameof(FaStandardShell.FooterContent), FooterContent);
        builder.AddComponentParameter(15, nameof(FaStandardShell.HeaderPosition), HeaderPosition);
        builder.AddComponentParameter(16, nameof(FaStandardShell.FooterPosition), FooterPosition);

        builder.AddComponentParameter(17, nameof(FaStandardShell.ChildContent), (RenderFragment)(body =>
        {
            body.OpenElement(18, "article");
            body.AddAttribute(19, "class", "fa-blog-post-template");

            // Back link
            if (BackAction != null)
            {
                body.OpenElement(20, "div");
                body.AddAttribute(21, "class", "fa-blog-post-back");
                body.AddContent(22, BackAction);
                body.CloseElement(); // back
            }
            else if (!string.IsNullOrWhiteSpace(BackHref))
            {
                body.OpenElement(23, "div");
                body.AddAttribute(24, "class", "fa-blog-post-back");
                body.OpenElement(25, "a");
                body.AddAttribute(26, "href", BackHref);
                body.AddAttribute(27, "class", "fa-blog-post-back-link");
                body.AddContent(28, BackText);
                body.CloseElement(); // a
                body.CloseElement(); // back
            }

            // Article Header
            body.OpenElement(29, "header");
            body.AddAttribute(30, "class", "fa-blog-post-header");

            if (CategoryBadge != null)
            {
                body.OpenElement(31, "div");
                body.AddAttribute(32, "class", "fa-blog-post-category");
                body.AddContent(33, CategoryBadge);
                body.CloseElement(); // category
            }

            body.OpenElement(34, "h1");
            body.AddAttribute(35, "class", "fa-blog-post-title");
            body.AddContent(36, Title);
            body.CloseElement(); // h1

            if (!string.IsNullOrWhiteSpace(Subtitle))
            {
                body.OpenElement(37, "p");
                body.AddAttribute(38, "class", "fa-blog-post-subtitle");
                body.AddContent(39, Subtitle);
                body.CloseElement(); // subtitle
            }

            // Meta row
            body.OpenElement(40, "div");
            body.AddAttribute(41, "class", "fa-blog-post-meta");

            if (AuthorContent != null)
            {
                body.OpenElement(42, "div");
                body.AddAttribute(43, "class", "fa-blog-post-author-meta");
                body.AddContent(44, AuthorContent);
                body.CloseElement(); // author
            }

            body.OpenElement(45, "div");
            body.AddAttribute(46, "class", "fa-blog-post-dates");
            if (!string.IsNullOrWhiteSpace(PublishedDate))
            {
                body.OpenElement(47, "time");
                body.AddAttribute(48, "class", "fa-blog-post-date");
                body.AddContent(49, PublishedDate);
                body.CloseElement(); // time
            }
            if (!string.IsNullOrWhiteSpace(ReadTime))
            {
                body.OpenElement(50, "span");
                body.AddAttribute(51, "class", "fa-blog-post-readtime");
                body.AddContent(52, ReadTime);
                body.CloseElement(); // span
            }
            body.CloseElement(); // dates
            body.CloseElement(); // meta

            body.CloseElement(); // header

            // Cover Image
            if (!string.IsNullOrWhiteSpace(CoverImageUrl))
            {
                body.OpenElement(53, "figure");
                body.AddAttribute(54, "class", "fa-blog-post-cover");
                body.OpenElement(55, "img");
                body.AddAttribute(56, "src", CoverImageUrl);
                body.AddAttribute(57, "alt", CoverImageAlt ?? Title);
                body.CloseElement(); // img
                body.CloseElement(); // figure
            }

            // Article Content
            body.OpenElement(58, "div");
            body.AddAttribute(59, "class", "fa-blog-post-content");
            body.AddContent(60, ChildContent);
            body.CloseElement(); // content

            // Share Actions
            if (ShareContent != null)
            {
                body.OpenElement(61, "section");
                body.AddAttribute(62, "class", "fa-blog-post-share");
                body.AddContent(63, ShareContent);
                body.CloseElement(); // share
            }

            // Author Bio
            if (AuthorBio != null)
            {
                body.OpenElement(64, "aside");
                body.AddAttribute(65, "class", "fa-blog-post-author-bio");
                body.AddContent(66, AuthorBio);
                body.CloseElement(); // bio
            }

            // Related Posts
            if (RelatedPosts != null)
            {
                body.OpenElement(67, "section");
                body.AddAttribute(68, "class", "fa-blog-post-related");
                body.OpenElement(69, "h2");
                body.AddAttribute(70, "class", "fa-blog-post-related-title");
                body.AddContent(71, "Related Articles");
                body.CloseElement(); // h2
                body.OpenElement(72, "div");
                body.AddAttribute(73, "class", "fa-blog-post-related-grid");
                body.AddContent(74, RelatedPosts);
                body.CloseElement(); // grid
                body.CloseElement(); // section
            }

            body.CloseElement(); // fa-blog-post-template
        }));

        builder.CloseComponent();
    }
}
