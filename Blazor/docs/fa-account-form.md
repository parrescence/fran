[← Back to index](index.md)

# FaAccountForm

A ready-made account profile form allowing users to update their profile picture, display name, email, and biographical information.

## Features

- **Live Avatar Preview**: Displays an `FaAvatar` preview that immediately updates as the user edits their picture URL or name.
- **Picture URL & Info Inputs**: Profile picture URL, display name, email address, and biographical textarea.
- **Custom Content**: Slot (`AdditionalContent`) for application-specific account fields.
- **Action Buttons**: Submit button with busy state and optional Cancel button.
- **Alerts**: Built-in display for `SuccessText` and `ErrorText`.

## Usage

```razor
<FaAccountForm Model="@accountModel"
               OnSubmit="HandleSave"
               OnCancel="HandleCancel"
               SuccessText="@successMessage"
               ErrorText="@errorMessage"
               Busy="@isSaving" />

@code {
    private FaAccountModel accountModel = new()
    {
        DisplayName = "Jane Doe",
        Email = "jane@example.com",
        ImageUrl = "https://example.com/photo.jpg",
        Bio = "Lead designer & engineer"
    };

    private bool isSaving;
    private string? successMessage;
    private string? errorMessage;

    private async Task HandleSave(FaAccountModel updated)
    {
        isSaving = true;
        // Save to backend...
        isSaving = false;
        successMessage = "Account updated successfully!";
    }

    private void HandleCancel()
    {
        // Navigate back or dismiss
    }
}
```

## Parameters

| Parameter | Type | Default | Notes |
|---|---|---|---|
| `Model` | `FaAccountModel?` | `null` | Pre-populated model with `DisplayName`, `Email`, `ImageUrl`, `Bio`, `PhoneNumber` |
| `DisplayName` | `string?` | `null` | Initial display name (if `Model` is omitted) |
| `Email` | `string?` | `null` | Initial email address |
| `ImageUrl` | `string?` | `null` | Initial avatar image URL |
| `Bio` | `string?` | `null` | Initial bio text |
| `PhoneNumber` | `string?` | `null` | Initial phone number |
| `OnSubmit` | `EventCallback<FaAccountModel>` | *Required* | Callback receiving the updated model |
| `OnCancel` | `EventCallback` | | Callback invoked when Cancel is clicked |
| `Title` | `string` | `"Account settings"` | Form heading |
| `Subtitle` | `string?` | `"Update your profile picture and personal information."` | Subheading text |
| `SubmitText` | `string` | `"Save changes"` | Submit button text |
| `CancelText` | `string` | `"Cancel"` | Cancel button text |
| `Busy` | `bool` | `false` | Disables submit button and shows loading text |
| `SuccessText` | `string?` | `null` | Success alert message |
| `ErrorText` | `string?` | `null` | Error alert message |
| `AdditionalContent` | `RenderFragment?` | `null` | Extra application fields |
| `CssClass` | `string?` | `null` | |

[← Back to index](index.md)
