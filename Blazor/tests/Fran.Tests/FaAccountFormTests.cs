using System.Threading.Tasks;
using Bunit;
using Fran.Components;
using Microsoft.AspNetCore.Components.Web;
using Xunit;

namespace Fran.Tests;

public class FaAccountFormTests : BunitContext
{
    [Fact]
    public void InitialValues_PopulateFieldsAndPreview()
    {
        var model = new FaAccountModel
        {
            DisplayName = "Jane Doe",
            Email = "jane@example.com",
            ImageUrl = "https://example.com/avatar.jpg",
            Bio = "Software engineer"
        };

        var cut = Render<FaAccountForm>(p => p
            .Add(x => x.Model, model));

        var nameInput = cut.Find("input[required]");
        Assert.Equal("Jane Doe", nameInput.GetAttribute("value"));

        var emailInput = cut.Find("input[type='email']");
        Assert.Equal("jane@example.com", emailInput.GetAttribute("value"));

        var bioTextarea = cut.Find("textarea");
        Assert.Equal("Software engineer", bioTextarea.GetAttribute("value"));

        // Live avatar preview renders the img
        var avatarImg = cut.Find(".fa-account-avatar-preview img");
        Assert.Equal("https://example.com/avatar.jpg", avatarImg.GetAttribute("src"));
    }

    [Fact]
    public async Task TypingInFields_UpdatesWorkingModelOnSubmit()
    {
        FaAccountModel? submitted = null;
        var cut = Render<FaAccountForm>(p => p
            .Add(x => x.DisplayName, "Initial Name")
            .Add(x => x.OnSubmit, m => submitted = m));

        var nameInput = cut.Find("input[required]");
        nameInput.Input("Updated Name");

        var emailInput = cut.Find("input[type='email']");
        emailInput.Input("updated@example.com");

        var form = cut.Find("form");
        await form.SubmitAsync();

        Assert.NotNull(submitted);
        Assert.Equal("Updated Name", submitted.DisplayName);
        Assert.Equal("updated@example.com", submitted.Email);
    }

    [Fact]
    public async Task CancelButton_InvokesOnCancelCallback()
    {
        var cancelled = false;
        var cut = Render<FaAccountForm>(p => p
            .Add(x => x.DisplayName, "Test")
            .Add(x => x.OnCancel, () => cancelled = true));

        var cancelBtn = cut.Find("button.fa-btn-outline");
        await cancelBtn.ClickAsync(new MouseEventArgs());

        Assert.True(cancelled);
    }

    [Fact]
    public void Alerts_RenderWhenProvided()
    {
        var cut = Render<FaAccountForm>(p => p
            .Add(x => x.DisplayName, "Test")
            .Add(x => x.SuccessText, "Profile saved successfully!")
            .Add(x => x.ErrorText, "Network error occurred."));

        var alerts = cut.FindAll(".fa-alert");
        Assert.Equal(2, alerts.Count);
        Assert.Contains("Profile saved successfully!", cut.Markup);
        Assert.Contains("Network error occurred.", cut.Markup);
    }
}
