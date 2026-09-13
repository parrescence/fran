[← Back to index](index.md) · [← Back to page templates](page-templates.md)

# FaOnboardingTemplate

`FaStandardShell` wrapped around an onboarding / wizard setup flow: welcome header, step counter or progress indicator, focused card container with step title and instructions, active step input content, and a footer navigation row with Back, Skip, and Continue actions.

## Usage

```razor
@page "/onboarding"
@using Fran.Templates
@using Fran.Components

<FaOnboardingTemplate BrandText="MyApp" BrandHref="/"
                      Title="Set up your workspace"
                      Description="Complete a few quick details to personalize your experience."
                      CurrentStep="2"
                      TotalSteps="4"
                      StepTitle="Connect Source Code"
                      StepDescription="Select the Git provider where your projects are hosted.">
    <ChildContent>
        <div class="fa-flex fa-flex-column fa-gap-2">
            <FaButton Variant="FaButtonVariant.Outline">GitHub</FaButton>
            <FaButton Variant="FaButtonVariant.Outline">GitLab</FaButton>
            <FaButton Variant="FaButtonVariant.Outline">Bitbucket</FaButton>
        </div>
    </ChildContent>

    <BackAction>
        <FaButton Variant="FaButtonVariant.Outline" Href="/onboarding/step-1">Back</FaButton>
    </BackAction>

    <SkipAction>
        <FaButton Variant="FaButtonVariant.Secondary" Href="/dashboard">Skip for now</FaButton>
    </SkipAction>

    <ContinueAction>
        <FaButton Variant="FaButtonVariant.Primary" Href="/onboarding/step-3">Continue</FaButton>
    </ContinueAction>
</FaOnboardingTemplate>
```

## Parameters

| Parameter | Type | Notes |
|---|---|---|
| `BrandText` | `string` | **required** |
| `BrandHref` | `string` | |
| `BrandIconUrl` | `string?` | optional logo shown left of `BrandText` |
| `IsAuthenticated` | `bool` | |
| `UserDisplayName` / `UserImageUrl` / `UserEmail` | `string?` | |
| `UseAvatarForm` | `bool` | when true, renders `FaAvatarForm` in topbar |
| `ShowUserNameInHeader` | `bool` | when true with avatar form, shows user name |
| `AccountHref` | `string?` | account page link |
| `OnAccountClick` / `OnLogin` / `OnLogout` | `EventCallback` | |
| `FooterContent` | `RenderFragment?` | overrides default footer |
| `HeaderPosition` / `FooterPosition` | `FaNavPosition` | `Standard` (default) \| `Sticky` \| `Floating` |
| `Title` | `string` | onboarding title; defaults to "Getting Started" |
| `Description` | `string?` | onboarding description |
| `StepsIndicator` | `RenderFragment?` | custom step bar or progress component |
| `CurrentStep` | `int` | current step index (1-based); defaults to 1 |
| `TotalSteps` | `int` | total number of steps; defaults to 4 |
| `StepTitle` | `string?` | current step heading |
| `StepDescription` | `string?` | current step instructions |
| `ChildContent` | `RenderFragment?` | interactive form/inputs for the current step |
| `BackAction` | `RenderFragment?` | back navigation button |
| `SkipAction` | `RenderFragment?` | skip link / button |
| `ContinueAction` | `RenderFragment?` | continue / finish button |
