namespace Fran.Components;

/// <summary>
/// Visual presentation style for <see cref="FaTabs{TValue}"/>.
/// </summary>
public enum FaTabStyle
{
    /// <summary>
    /// Flat tab strip with an underline active-indicator.
    /// </summary>
    Underline = 0,

    /// <summary>
    /// File folder tabs with rounded top shoulders, border framing,
    /// distinct hover lift, and an active tab that connects directly to the panel.
    /// </summary>
    Folder = 1,

    /// <summary>
    /// Separated pill/capsule choices. When a tab is selected, it slides to the left
    /// and is placed first with a distinct contrasting background.
    /// </summary>
    Slide = 2,

    /// <summary>
    /// Cylindrical scroll drum / thumbwheel switch with rotary scrolling, side nudge buttons,
    /// edge fade masks, mousewheel support, and a centered active indicator.
    /// </summary>
    Wheel = 3,

    /// <summary>
    /// Alias for <see cref="Wheel"/>.
    /// </summary>
    Thumbwheel = Wheel
}

/// <summary>
/// Alias enum for <see cref="FaTabStyle"/> to support plural naming conventions.
/// </summary>
public enum FaTabsStyle
{
    Underline = FaTabStyle.Underline,
    Folder = FaTabStyle.Folder,
    Slide = FaTabStyle.Slide,
    Wheel = FaTabStyle.Wheel,
    Thumbwheel = FaTabStyle.Thumbwheel
}
