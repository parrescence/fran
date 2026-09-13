[← Back to index](index.md)

# FaTabs&lt;TValue&gt;

Tab strip and tab section component — pass 2+ `(string Title, TValue Value)` options,
same tuple shape as `FaToggle`/`FaRadioGroup`. Supports multiple visual presentation
styles through `FaTabStyle`:

- **Underline** (default): Clean underline active-indicator on a flat tab row.
- **Folder**: Classic file folder tabs with rounded top shoulders, border outlines, distinct hover lift, and an active tab that seamlessly merges into the panel below.
- **Slide**: Separated choice capsules. When selected, the active tab smoothly slides to the left and is placed first in order with a contrasting filled background.
- **Wheel** (or **Thumbwheel**): Cylindrical drum selector with scrollable rotary track, edge gradient fade masks, previous/next stepper buttons, mousewheel support, and centered alignment indicators.

FaTabs can render purely the tab strip, or a complete tab section when `ChildContent` is provided.

## Usage

### 1. Default Underline

```razor
<FaTabs TValue="string" Options="_tabs" @bind-Value="_activeTab" />
```

### 2. File Folder Tabs

```razor
<FaTabs TValue="string" Style="FaTabStyle.Folder" Options="_tabs" @bind-Value="_activeTab">
    @if (_activeTab == "details")
    {
        <p>Order #4821 — placed August 12, shipped August 14.</p>
    }
    else if (_activeTab == "history")
    {
        <p>Created → Paid → Shipped → Delivered.</p>
    }
</FaTabs>
```

### 3. Slide Left (Placed First)

```razor
<FaTabs TValue="string" Style="FaTabStyle.Slide" Options="_tabs" @bind-Value="_activeTab" />
```

### 4. Thumbwheel Switch Scroll

```razor
<FaTabs TValue="string" Style="FaTabStyle.Wheel" Options="_tabs" @bind-Value="_activeTab" />
```

## Getting the value

`@bind-Value` keeps `_activeTab` in sync as tabs are clicked, scrolled (via mousewheel on thumbwheel mode), or navigated with keyboard arrow keys (Home/End jump to the first/last tab).

## Parameters

| Parameter | Type | Notes |
|---|---|---|
| `Options` | `IReadOnlyList<(string Title, TValue Value)>` | **required**, at least 2 entries |
| `Value` / `ValueChanged` | `TValue` | `@bind-Value` two-way binding |
| `Style` | `FaTabStyle` | `Underline` (default), `Folder`, `Slide`, `Wheel` / `Thumbwheel` |
| `TabsStyle` | `FaTabsStyle?` | Plural enum alias for `Style` |
| `ChildContent` | `RenderFragment?` | Optional; wraps the strip and panel into a complete `.fa-tabs-section` |
| `CssClass` | `string?` | Optional custom CSS class |

[← Back to index](index.md)
