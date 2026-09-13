[← Back to index](index.md)

# Page templates

Four full-page compositions in `namespace Fran.Templates` (`@using Fran.Templates`),
each built on [`FaStandardShell`/`FaSidebarShell`](layout-shells.md) plus whatever
one recurring page shape needs beyond the bare shell:

| Template | Shell underneath | Adds |
|---|---|---|
| [FaDashboardTemplate](fa-dashboard-template.md) | `FaSidebarShell` | a title/actions row above the body |
| [FaFormTemplate](fa-form-template.md) | `FaStandardShell` | a centered card, optional back-link |
| [FaHomeTemplate](fa-home-template.md) | `FaStandardShell` | a hero band above the page's own sections |
| [FaPricingTemplate](fa-pricing-template.md) | `FaStandardShell` | pricing header, billing toggle, tier cards, FAQ teaser |
| [FaContactTemplate](fa-contact-template.md) | `FaStandardShell` | side-by-side contact details, inquiry form, map embed |
| [FaFaqTemplate](fa-faq-template.md) | `FaStandardShell` | search bar, category pills, accordion Q&A, support prompt |
| [FaNotFoundTemplate](fa-not-found-template.md) | *(none or FaStandardShell)* | 404 / error code badge, explanation, return actions |
| [FaSettingsTemplate](fa-settings-template.md) | `FaSidebarShell` | settings categories sub-nav sidebar, active section card |
| [FaListViewTemplate](fa-list-view-template.md) | `FaSidebarShell` | title/actions row, search/filter toolbar, data grid, pagination |
| [FaDetailViewTemplate](fa-detail-view-template.md) | `FaSidebarShell` | back link, status badge, action buttons, tabs, main/aside split |
| [FaAboutTemplate](fa-about-template.md) | `FaStandardShell` | hero, key stats grid, mission narrative, team grid, CTA |
| [FaDocsTemplate](fa-docs-template.md) | `FaSidebarShell` | hierarchy tree nav, breadcrumbs, search, code blocks, "on this page" TOC |
| [FaChangelogTemplate](fa-changelog-template.md) | `FaStandardShell` | title/actions row, category/version filters, release timeline |
| [FaLegalTemplate](fa-legal-template.md) | `FaStandardShell` | terms / privacy policy, sticky TOC navigation rail, readable clauses |
| [FaThankYouTemplate](fa-thank-you-template.md) | *(none or FaStandardShell)* | success badge, order summary, next steps, action buttons |
| [FaComingSoonTemplate](fa-coming-soon-template.md) | *(none or FaStandardShell)* | countdown timer, notify email signup, brand hero, social links |
| [FaTutorialTemplate](fa-tutorial-template.md) | `FaStandardShell` | step-by-step instructions, difficulty/time metadata, jump list |
| [FaKnowledgeBaseTemplate](fa-knowledge-base-template.md) | `FaStandardShell` | searchable article index, topic cards, trending articles, support CTA |
| [FaRoadmapTemplate](fa-roadmap-template.md) | `FaStandardShell` | Kanban/timeline columns (Planned, In Progress, Shipped), filters |
| [FaGlossaryTemplate](fa-glossary-template.md) | `FaStandardShell` | A–Z alphabetical index, term definitions, quick-jump letter bar |
| [FaBlogIndexTemplate](fa-blog-index-template.md) | `FaStandardShell` | featured article hero, category pills, 3-column post grid, pagination |
| [FaBlogPostTemplate](fa-blog-post-template.md) | `FaStandardShell` | single-article layout, author card, reading time, share actions, related posts |
| [FaBillingTemplate](fa-billing-template.md) | `FaSidebarShell` | subscription tier, payment cards, billing contact, invoices history table |
| [FaActivityFeedTemplate](fa-activity-feed-template.md) | `FaSidebarShell` | audit log & timeline stream, summary metrics, filter toolbar, pagination |
| [FaStatusTemplate](fa-status-template.md) | `FaStandardShell` | system status banner, component health list, 90-day uptime metrics, incidents |
| [FaOnboardingTemplate](fa-onboarding-template.md) | `FaStandardShell` | step progress wizard, step title & desc, active step card, Back/Continue |
| [FaCalendarTemplate](fa-calendar-template.md) | `FaSidebarShell` | date pager, month/week/day view switcher, calendar grid, agenda aside |
| [FaFileManagerTemplate](fa-file-manager-template.md) | `FaSidebarShell` | storage quota, folder breadcrumbs, search, file grid, inspector drawer |
| [FaAuthTemplate](fa-auth-template.md) | *(none — chrome-free)* | a centered card, no header/sidebar/footer |

They're **routed pages, not layouts** — use one directly on a `@page`-attributed
`.razor` file, the same way you'd use any other component, rather than wrapping
`@Body` in your `MainLayout.razor` (that's still `FaStandardShell`/`FaSidebarShell`'s
job — see [layout shells](layout-shells.md)). A template *is* the shell for that one
page, so a page using one shouldn't also render inside a shell-based `MainLayout`;
either give that page `@layout` set to something chrome-free, or don't apply a
shared layout to it at all.

## How to use one

```razor
@page "/dashboard"
@using Fran.Templates

<FaDashboardTemplate BrandText="MyApp" BrandHref="/"
                    IsAuthenticated="@_isAuthenticated"
                    UserDisplayName="@_userName"
                    OnLogin="HandleLoginAsync" OnLogout="HandleLogoutAsync"
                    Title="Overview">
    <Sidebar>
        <NavMenu /> @* your own nav links component *@
    </Sidebar>
    <Actions>
        <FaButton Variant="FaButtonVariant.Primary">New report</FaButton>
    </Actions>
    <ChildContent>
        <FaCard>Your dashboard body — stat cards, a grid, charts.</FaCard>
    </ChildContent>
</FaDashboardTemplate>

@code {
    private bool _isAuthenticated;
    private string? _userName;

    private Task HandleLoginAsync() { /* redirect to your auth flow */ return Task.CompletedTask; }
    private Task HandleLogoutAsync() { /* sign out */ return Task.CompletedTask; }
}
```

Every shell-level parameter (`BrandText`, `IsAuthenticated`, `UserDisplayName`,
`OnLogin`/`OnLogout`, `Sidebar`, `FooterContent`, and each bar's own
`HeaderPosition`/`FooterPosition`/`SidebarPosition`) uses the exact same name as
`FaStandardShell`/`FaSidebarShell` — see [layout shells](layout-shells.md) for what
each one does, `Position`/`Sticky`/`Floating`, `ContainScroll`, and
`SidebarCollapsible` in particular, since none of that is re-explained on the
individual template pages below.

`ChildContent` needs its own explicit `<ChildContent>` tag whenever the template
also has other `RenderFragment` parameters in use at that call site (`Sidebar`/
`Actions` above) — same Razor rule `layout-shells.md` covers for the shells
themselves. A template with nothing else set (e.g. `FaFormTemplate` with no
`BackHref`) can take bare, unwrapped content instead.

## Filling in the bars

- **Sidebar** (`FaDashboardTemplate` only) — pass your app's own nav-menu
  component, same as `FaSidebarShell.Sidebar`.
- **FooterContent** — overrides the default `© year BrandText` line, same as
  either shell's `FooterContent`.
- **Header** — not directly fillable beyond the shared `BrandText`/auth props;
  reach for the shells directly (or `FaHeader`/`FaSidebar`/`FaFooter` individually)
  if a page needs custom header content these templates don't expose.

## Picking sticky vs. not

Every bar a template renders takes the same `FaNavPosition` choice the raw shells
do — `Standard` (scrolls away, the default), `Sticky` (pinned once scrolled to), or
`Floating` (pinned, inset as a detached bar):

```razor
<FaDashboardTemplate BrandText="MyApp" BrandHref="/"
                    HeaderPosition="FaNavPosition.Sticky"
                    SidebarPosition="FaNavPosition.Sticky"
                    FooterPosition="FaNavPosition.Standard"
                    ContainScroll="true"
                    Title="Overview">
    ...
</FaDashboardTemplate>
```

`FaAuthTemplate` has no bars to position — it's chrome-free by design (see its own
page for why).

[← Back to index](index.md)
