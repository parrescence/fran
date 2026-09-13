[← Back to index](index.md) · [← Back to page templates](page-templates.md)

# FaCalendarTemplate

`FaSidebarShell` wrapped around a calendar &amp; scheduling layout: top toolbar with date range title, date navigation controls (previous, next, today), month/week/day view switcher, primary "New Event" action, sub-calendar filter bar, main calendar grid slot, and a side agenda drawer for selected date events.

## Usage

```razor
@page "/calendar"
@using Fran.Templates
@using Fran.Components

<FaCalendarTemplate BrandText="MyApp" BrandHref="/"
                    Title="Calendar"
                    DateRangeTitle="October 2026">
    <Sidebar>
        <nav class="fa-nav-tree">
            <a href="/dashboard">Dashboard</a>
            <a href="/calendar" class="active">Calendar</a>
            <a href="/files">Files</a>
        </nav>
    </Sidebar>

    <DateNav>
        <FaButton Variant="FaButtonVariant.Outline">Today</FaButton>
        <FaButton Variant="FaButtonVariant.Outline">&lt;</FaButton>
        <FaButton Variant="FaButtonVariant.Outline">&gt;</FaButton>
    </DateNav>

    <ViewSwitcher>
        <FaButton Variant="FaButtonVariant.Primary">Month</FaButton>
        <FaButton Variant="FaButtonVariant.Outline">Week</FaButton>
        <FaButton Variant="FaButtonVariant.Outline">Day</FaButton>
    </ViewSwitcher>

    <PrimaryAction>
        <FaButton Variant="FaButtonVariant.Primary">+ New Event</FaButton>
    </PrimaryAction>

    <ChildContent>
        <div class="fa-card fa-p-4">
            <!-- Calendar grid or component -->
            <p>October 2026 Grid View</p>
        </div>
    </ChildContent>

    <EventAsideContent>
        <h4>Selected Date: Oct 14, 2026</h4>
        <FaCard class="fa-mt-2">
            <strong>Sprint Planning</strong>
            <p class="fa-text-muted">10:00 AM - 11:30 AM</p>
        </FaCard>
    </EventAsideContent>
</FaCalendarTemplate>
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
| `Sidebar` | `RenderFragment?` | navigation links in the side rail |
| `FooterContent` | `RenderFragment?` | overrides default footer |
| `HeaderPosition` / `FooterPosition` / `SidebarPosition` | `FaNavPosition` | `Standard` (default) \| `Sticky` \| `Floating` |
| `SidebarCollapsible` | `bool` | enables mobile drawer collapse |
| `ContainScroll` | `bool` | confines scroll to content pane |
| `Title` | `string` | calendar title; defaults to "Calendar" |
| `DateRangeTitle` | `string?` | e.g. "October 2026" or "Oct 12 – 18, 2026" |
| `DateNav` | `RenderFragment?` | Previous / Next / Today date navigation controls |
| `ViewSwitcher` | `RenderFragment?` | Month / Week / Day toggle buttons |
| `PrimaryAction` | `RenderFragment?` | primary action button (e.g. "+ New Event") |
| `FilterContent` | `RenderFragment?` | sub-calendar check boxes / filters |
| `ChildContent` | `RenderFragment?` | main calendar grid or agenda view |
| `EventAsideContent` | `RenderFragment?` | side panel for selected day events |
