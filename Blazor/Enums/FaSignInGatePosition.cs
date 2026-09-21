namespace Fran.Components;

/// <summary>Where <see cref="FaSignInGate"/>'s card anchors within the page's content area (not the viewport — it never covers a header/sidebar outside that area). Default is <see cref="TopCenter"/>.</summary>
public enum FaSignInGatePosition
{
    TopLeft,
    TopCenter,
    TopRight,
    CenterLeft,
    Center,
    CenterRight,
    BottomLeft,
    BottomCenter,
    BottomRight
}
