namespace Fran.Components;

/// <summary>
/// A navigation bookmark item for <see cref="FaBookmarkNav"/>.
/// </summary>
public sealed class FaBookmarkItem
{
    /// <summary>Element ID targeted by the bookmark (without the leading #, e.g. "brand" or "architecture").</summary>
    public string Id { get; set; } = "";

    /// <summary>Display label / title of the bookmark.</summary>
    public string Title { get; set; } = "";

    /// <summary>Optional section number or ledger indicator (e.g. "01", "02", "§1").</summary>
    public string? Number { get; set; }

    /// <summary>Custom URL/anchor. Defaults to $"#{Id}" if not specified.</summary>
    public string? Href { get; set; }

    /// <summary>Optional status pill or badge text (e.g. "New", "Live", "WIP").</summary>
    public string? Badge { get; set; }

    /// <summary>Whether this bookmark is currently active / selected.</summary>
    public bool IsActive { get; set; }

    public FaBookmarkItem() { }

    public FaBookmarkItem(string id, string title, string? number = null, string? href = null, string? badge = null, bool isActive = false)
    {
        Id = id;
        Title = title;
        Number = number;
        Href = href ?? $"#{id}";
        Badge = badge;
        IsActive = isActive;
    }
}
