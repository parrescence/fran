namespace Fran.Components;

/// <summary>
/// Data model representing user profile/account information for <see cref="FaAccountForm"/>.
/// </summary>
public sealed class FaAccountModel
{
    public string DisplayName { get; set; } = "";
    public string? Email { get; set; }
    public string? ImageUrl { get; set; }
    public string? Bio { get; set; }
    public string? PhoneNumber { get; set; }
}
