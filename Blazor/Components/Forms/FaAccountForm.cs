using Fran.Icons;
using Fran.Rendering;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;

namespace Fran.Components;

/// <summary>
/// A ready-made account profile form allowing users to view and update their profile picture,
/// display name, email, and biographical information.
/// Fires <see cref="OnSubmit"/> with the updated <see cref="FaAccountModel"/>.
/// </summary>
public sealed class FaAccountForm : ComponentBase
{
    private readonly string _id = $"fa-account-form-{Guid.NewGuid():N}";
    private readonly string _nameId = $"fa-account-name-{Guid.NewGuid():N}";
    private readonly string _emailId = $"fa-account-email-{Guid.NewGuid():N}";
    private readonly string _imageId = $"fa-account-image-{Guid.NewGuid():N}";
    private readonly string _bioId = $"fa-account-bio-{Guid.NewGuid():N}";
    private readonly string _phoneId = $"fa-account-phone-{Guid.NewGuid():N}";

    private readonly FaAccountModel _workingModel = new();

    [Parameter] public FaAccountModel? Model { get; set; }
    [Parameter] public string? DisplayName { get; set; }
    [Parameter] public string? Email { get; set; }
    [Parameter] public string? ImageUrl { get; set; }
    [Parameter] public string? Bio { get; set; }
    [Parameter] public string? PhoneNumber { get; set; }

    [Parameter, EditorRequired] public EventCallback<FaAccountModel> OnSubmit { get; set; }
    [Parameter] public EventCallback OnCancel { get; set; }

    [Parameter] public string Title { get; set; } = "Account settings";
    [Parameter] public string? Subtitle { get; set; } = "Update your profile picture and personal information.";
    [Parameter] public string SubmitText { get; set; } = "Save changes";
    [Parameter] public string CancelText { get; set; } = "Cancel";

    [Parameter] public bool Busy { get; set; }
    [Parameter] public string? ErrorText { get; set; }
    [Parameter] public string? SuccessText { get; set; }

    /// <summary>Optional custom fields or content rendered before the action buttons.</summary>
    [Parameter] public RenderFragment? AdditionalContent { get; set; }

    [Parameter] public string? CssClass { get; set; }

    protected override void OnParametersSet()
    {
        if (Model is not null)
        {
            _workingModel.DisplayName = Model.DisplayName;
            _workingModel.Email = Model.Email;
            _workingModel.ImageUrl = Model.ImageUrl;
            _workingModel.Bio = Model.Bio;
            _workingModel.PhoneNumber = Model.PhoneNumber;
        }
        else
        {
            if (DisplayName is not null) _workingModel.DisplayName = DisplayName;
            if (Email is not null) _workingModel.Email = Email;
            if (ImageUrl is not null) _workingModel.ImageUrl = ImageUrl;
            if (Bio is not null) _workingModel.Bio = Bio;
            if (PhoneNumber is not null) _workingModel.PhoneNumber = PhoneNumber;
        }
    }

    private Task HandleSubmit() => OnSubmit.InvokeAsync(_workingModel);

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        var seq = 0;
        builder.OpenElement(seq++, "form");
        builder.AddAttribute(seq++, "id", _id);
        builder.AddAttribute(seq++, "class", CssClassNames.Combine("fa-form fa-account-form", CssClass));
        builder.AddAttribute(seq++, "onsubmit", EventCallback.Factory.Create(this, HandleSubmit));
        builder.AddEventPreventDefaultAttribute(seq++, "onsubmit", true);

        // Header
        if (!string.IsNullOrWhiteSpace(Title) || !string.IsNullOrWhiteSpace(Subtitle))
        {
            builder.OpenElement(seq++, "div");
            builder.AddAttribute(seq++, "class", "fa-account-header");

            if (!string.IsNullOrWhiteSpace(Title))
            {
                builder.OpenElement(seq++, "h2");
                builder.AddAttribute(seq++, "class", "fa-account-title");
                builder.AddContent(seq++, Title);
                builder.CloseElement();
            }

            if (!string.IsNullOrWhiteSpace(Subtitle))
            {
                builder.OpenElement(seq++, "p");
                builder.AddAttribute(seq++, "class", "fa-account-subtitle");
                builder.AddContent(seq++, Subtitle);
                builder.CloseElement();
            }

            builder.CloseElement(); // .fa-account-header
        }

        // Success / Error Alerts
        if (!string.IsNullOrEmpty(SuccessText))
        {
            builder.OpenComponent<FaAlert>(seq++);
            builder.AddComponentParameter(seq++, nameof(FaAlert.Variant), FaAlertVariant.Success);
            builder.AddComponentParameter(seq++, nameof(FaAlert.ChildContent), (RenderFragment)(b => b.AddContent(0, SuccessText)));
            builder.CloseComponent();
        }

        if (!string.IsNullOrEmpty(ErrorText))
        {
            builder.OpenComponent<FaAlert>(seq++);
            builder.AddComponentParameter(seq++, nameof(FaAlert.Variant), FaAlertVariant.Danger);
            builder.AddComponentParameter(seq++, nameof(FaAlert.ChildContent), (RenderFragment)(b => b.AddContent(0, ErrorText)));
            builder.CloseComponent();
        }

        // Avatar / Picture Section with Live Preview
        builder.OpenElement(seq++, "div");
        builder.AddAttribute(seq++, "class", "fa-account-avatar-section");

        builder.OpenComponent<FaAvatar>(seq++);
        builder.AddComponentParameter(seq++, nameof(FaAvatar.DisplayName), _workingModel.DisplayName);
        builder.AddComponentParameter(seq++, nameof(FaAvatar.ImageUrl), _workingModel.ImageUrl);
        builder.AddComponentParameter(seq++, nameof(FaAvatar.CssClass), "fa-account-avatar-preview");
        builder.CloseComponent();

        builder.OpenElement(seq++, "div");
        builder.AddAttribute(seq++, "class", "fa-account-avatar-input");

        builder.OpenElement(seq++, "label");
        builder.AddAttribute(seq++, "class", "fa-label");
        builder.AddAttribute(seq++, "for", _imageId);
        builder.AddContent(seq++, "Profile Picture URL");
        builder.CloseElement();

        builder.OpenElement(seq++, "input");
        builder.AddAttribute(seq++, "id", _imageId);
        builder.AddAttribute(seq++, "class", "fa-input");
        builder.AddAttribute(seq++, "type", "url");
        builder.AddAttribute(seq++, "placeholder", "https://example.com/avatar.jpg");
        builder.AddAttribute(seq++, "value", _workingModel.ImageUrl);
        builder.AddAttribute(seq++, "oninput", EventCallback.Factory.Create<ChangeEventArgs>(this, e => _workingModel.ImageUrl = e.Value?.ToString()));
        builder.CloseElement();

        builder.CloseElement(); // .fa-account-avatar-input
        builder.CloseElement(); // .fa-account-avatar-section

        // Display Name Field
        builder.OpenElement(seq++, "div");
        builder.AddAttribute(seq++, "class", "fa-field");

        builder.OpenElement(seq++, "label");
        builder.AddAttribute(seq++, "class", "fa-label");
        builder.AddAttribute(seq++, "for", _nameId);
        builder.AddContent(seq++, "Display Name");
        builder.CloseElement();

        builder.OpenElement(seq++, "input");
        builder.AddAttribute(seq++, "id", _nameId);
        builder.AddAttribute(seq++, "class", "fa-input");
        builder.AddAttribute(seq++, "type", "text");
        builder.AddAttribute(seq++, "required", true);
        builder.AddAttribute(seq++, "value", _workingModel.DisplayName);
        builder.AddAttribute(seq++, "oninput", EventCallback.Factory.Create<ChangeEventArgs>(this, e => _workingModel.DisplayName = e.Value?.ToString() ?? ""));
        builder.CloseElement();

        builder.CloseElement(); // .fa-field

        // Email Field
        builder.OpenElement(seq++, "div");
        builder.AddAttribute(seq++, "class", "fa-field");

        builder.OpenElement(seq++, "label");
        builder.AddAttribute(seq++, "class", "fa-label");
        builder.AddAttribute(seq++, "for", _emailId);
        builder.AddContent(seq++, "Email Address");
        builder.CloseElement();

        builder.OpenElement(seq++, "input");
        builder.AddAttribute(seq++, "id", _emailId);
        builder.AddAttribute(seq++, "class", "fa-input");
        builder.AddAttribute(seq++, "type", "email");
        builder.AddAttribute(seq++, "value", _workingModel.Email);
        builder.AddAttribute(seq++, "oninput", EventCallback.Factory.Create<ChangeEventArgs>(this, e => _workingModel.Email = e.Value?.ToString()));
        builder.CloseElement();

        builder.CloseElement(); // .fa-field

        // Bio / About Field
        builder.OpenElement(seq++, "div");
        builder.AddAttribute(seq++, "class", "fa-field");

        builder.OpenElement(seq++, "label");
        builder.AddAttribute(seq++, "class", "fa-label");
        builder.AddAttribute(seq++, "for", _bioId);
        builder.AddContent(seq++, "Bio / About");
        builder.CloseElement();

        builder.OpenElement(seq++, "textarea");
        builder.AddAttribute(seq++, "id", _bioId);
        builder.AddAttribute(seq++, "class", "fa-textarea");
        builder.AddAttribute(seq++, "rows", 3);
        builder.AddAttribute(seq++, "placeholder", "Tell us a bit about yourself…");
        builder.AddAttribute(seq++, "value", _workingModel.Bio);
        builder.AddAttribute(seq++, "oninput", EventCallback.Factory.Create<ChangeEventArgs>(this, e => _workingModel.Bio = e.Value?.ToString()));
        builder.CloseElement();

        builder.CloseElement(); // .fa-field

        // Optional Additional Content
        if (AdditionalContent is not null)
        {
            builder.AddContent(seq++, AdditionalContent);
        }

        // Actions
        builder.OpenElement(seq++, "div");
        builder.AddAttribute(seq++, "class", "fa-form-actions fa-form-actions-end");

        if (OnCancel.HasDelegate)
        {
            builder.OpenComponent<FaButton>(seq++);
            builder.AddComponentParameter(seq++, nameof(FaButton.Variant), FaButtonVariant.Outline);
            builder.AddComponentParameter(seq++, nameof(FaButton.OnClick), EventCallback.Factory.Create<MouseEventArgs>(this, () => OnCancel.InvokeAsync()));
            builder.AddComponentParameter(seq++, nameof(FaButton.ChildContent), (RenderFragment)(b => b.AddContent(0, CancelText)));
            builder.CloseComponent();
        }

        builder.OpenComponent<FaButton>(seq++);
        builder.AddComponentParameter(seq++, nameof(FaButton.Type), "submit");
        builder.AddComponentParameter(seq++, nameof(FaButton.Variant), FaButtonVariant.Primary);
        builder.AddComponentParameter(seq++, nameof(FaButton.Disabled), Busy);
        builder.AddComponentParameter(seq++, nameof(FaButton.ChildContent), (RenderFragment)(b => b.AddContent(0, Busy ? "Saving…" : SubmitText)));
        builder.CloseComponent();

        builder.CloseElement(); // .fa-form-actions

        builder.CloseElement(); // form
    }
}
