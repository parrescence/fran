# FaBookmarkNav

Sticky bookmark navigation rail and table of contents. Pinned alongside long-form reference documents (project charters, operational runbooks, FAQs, how-tos, documentation guides) with numbered section badges, smooth scrolling jump links, and active section tracking.

## Usage

### Structured Items Array

```razor
@using Fran.Components

<FaBookmarkNav Title="Contents"
               Items="_bookmarks"
               ActiveId="@_activeSection"
               Sticky="true"
               OnItemClick="HandleBookmarkClick" />

@code {
    private string _activeSection = "architecture";

    private readonly IReadOnlyList<FaBookmarkItem> _bookmarks = new[]
    {
        new FaBookmarkItem("overview", "Overview", "01"),
        new FaBookmarkItem("methodology", "Methodology", "02"),
        new FaBookmarkItem("architecture", "Architecture & Data", "03", badge: "Core"),
        new FaBookmarkItem("roadmap", "Roadmap", "04")
    };

    private void HandleBookmarkClick(FaBookmarkItem item)
    {
        _activeSection = item.Id;
    }
}
```

### Declarative Markup (`FaBookmarkLink`)

```razor
<FaBookmarkNav Title="On this page" Sticky="true">
    <ol class="fa-bookmark-nav-list">
        <FaBookmarkLink Href="#setup" Number="01" Text="Installation & Setup" />
        <FaBookmarkLink Href="#usage" Number="02" Text="Component Usage" IsActive="true" />
        <FaBookmarkLink Href="#api" Number="03" Text="API Reference" Badge="v0.9.0" />
    </ol>
</FaBookmarkNav>
```

## Parameters

| Parameter | Type | Default | Description |
|---|---|---|---|
| `Title` | `string` | `"Contents"` | Heading displayed above the bookmark list. |
| `Items` | `IReadOnlyList<FaBookmarkItem>?` | `null` | Structured list of bookmark items. |
| `ActiveId` | `string?` | `null` | Target element ID of the currently active/selected item. |
| `Sticky` | `bool` | `true` | Whether the navigation rail stays pinned during document scrolling. |
| `Width` | `string?` | `null` | Optional explicit width (e.g. `"260px"`). |
| `AriaLabel` | `string` | `"Table of contents bookmarks"` | Accessible name for screen readers. |
| `OnItemClick` | `EventCallback<FaBookmarkItem>` | — | Callback invoked when a bookmark is selected. |
| `CssClass` | `string?` | `null` | Additional CSS classes. |
| `ChildContent` | `RenderFragment?` | `null` | Arbitrary custom links or controls inside the nav container. |

## FaBookmarkItem Properties

| Property | Type | Description |
|---|---|---|
| `Id` | `string` | Anchor target ID (without `#`, e.g. `"brand"`). |
| `Title` | `string` | Display label. |
| `Number` | `string?` | Optional numbered badge / ledger indicator (e.g. `"01"`, `"§1"`). |
| `Href` | `string?` | URL or anchor href. Defaults to `"#"` + `Id`. |
| `Badge` | `string?` | Optional status tag / pill text. |
| `IsActive` | `bool` | Whether the item is selected / active. |
