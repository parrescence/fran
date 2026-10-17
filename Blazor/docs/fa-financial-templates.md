# Financial Templates

Fran provides three specialized financial templates in `namespace Fran.Templates` designed for high-density desktop displays, clean container-query transitions, and mobile thumb-zone ergonomics:

1. **`FaLedgerTemplate`** — Statement ledger, chronological transactions, account activity.
2. **`FaFinanceOverviewTemplate`** — 360° financial overview, income vs. expense cashflow, swipeable mobile KPI strip.
3. **`FaAccountPortfolioTemplate`** — Net worth portfolio, connected banking institutions, fluid account cards.

All three templates support `EmbedInLayout="true"` to render directly inside an application's shared `MainLayout.razor` without duplicating outer shell bars.

---

## 1. FaLedgerTemplate

Desktop: tabular ledger with running balances, filter bar, and sortable headers.
Mobile: auto-adapts to card-list mode with touch-friendly tap targets and optional thumb-zone Floating Action Button (FAB).

```razor
@page "/transactions"
@using Fran.Templates

<FaLedgerTemplate EmbedInLayout="true"
                  Title="Transactions"
                  Description="Review and categorize all posted activity."
                  TotalCount="@_totalCount">
    <TitleBadge>
        <FaBadge Variant="FaBadgeVariant.Primary">@_totalCount transactions</FaBadge>
    </TitleBadge>
    <HeaderActions>
        <FaButton Variant="FaButtonVariant.Primary" OnClick="OpenNewTxnModal">+ Add Transaction</FaButton>
        <FaButton Variant="FaButtonVariant.Secondary" OnClick="OpenImportModal">Import Statement</FaButton>
    </HeaderActions>
    <SummaryContent>
        <div class="active-account-pill">Everyday Checking ••••1001</div>
    </SummaryContent>
    <FiltersContent>
        <!-- Filter bar / search -->
    </FiltersContent>
    <ChildContent>
        <!-- Transaction list items -->
    </ChildContent>
    <FloatingAction>
        <button type="button" class="fa-financial-fab" @onclick="OpenNewTxnModal" aria-label="Add transaction">+</button>
    </FloatingAction>
</FaLedgerTemplate>
```

---

## 2. FaFinanceOverviewTemplate

Desktop: multi-column dashboard with KPI grid, income vs. expense cash flow visualizer, and category spending breakdown.
Mobile: KPI cards automatically collapse into a smooth horizontal snap-scroll strip (`.fa-kpi-swipe-strip`) eliminating vertical scroll fatigue.

```razor
@page "/dashboard"
@using Fran.Templates

<FaFinanceOverviewTemplate EmbedInLayout="true"
                           Title="October 2026 Overview"
                           Subtitle="Financial snapshot and cash flow analysis">
    <PeriodSelector>
        <!-- Stepper: ‹ October 2026 › -->
    </PeriodSelector>
    <HeaderActions>
        <FaButton Variant="FaButtonVariant.Secondary" OnClick="Refresh">Refresh</FaButton>
    </HeaderActions>
    <KpiMetrics>
        <!-- 4 KPI cards: swipeable on mobile, grid on desktop -->
    </KpiMetrics>
    <CashFlowContent>
        <!-- Income vs. Expense visualizer -->
    </CashFlowContent>
    <CategoryBreakdown>
        <!-- Category spend allocation -->
    </CategoryBreakdown>
    <RecentActivity>
        <!-- Recent ledger feed -->
    </RecentActivity>
</FaFinanceOverviewTemplate>
```

---

## 3. FaAccountPortfolioTemplate

Desktop: Net worth banner, institution sync statuses, and grouped asset/liability cards.
Mobile: Fluid card sizing (`min(100%, 300px)`) preventing overflow on narrow mobile viewports.

```razor
@page "/accounts"
@using Fran.Templates

<FaAccountPortfolioTemplate EmbedInLayout="true"
                            Title="Accounts & Institutions"
                            Description="Protected with zero-knowledge vault security.">
    <HeaderActions>
        <FaButton Variant="FaButtonVariant.Primary" OnClick="OpenConnectModal">Connect Bank</FaButton>
        <FaButton Variant="FaButtonVariant.Secondary" OnClick="OpenVaultModal">Vault Security</FaButton>
    </HeaderActions>
    <NetWorthContent>
        <!-- Net worth summary -->
    </NetWorthContent>
    <InstitutionsContent>
        <!-- Connected institutions -->
    </InstitutionsContent>
    <AccountGroupsContent>
        <!-- Checking, Savings, Credit Cards, Investments -->
    </AccountGroupsContent>
</FaAccountPortfolioTemplate>
```
