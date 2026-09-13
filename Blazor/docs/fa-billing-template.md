[← Back to index](index.md) · [← Back to page templates](page-templates.md)

# FaBillingTemplate

`FaSidebarShell` wrapped around a billing and subscription management layout: current plan summary card with usage metrics, payment method cards with "Add payment method" action, billing contact information, and an invoice history table.

## Usage

```razor
@page "/billing"
@using Fran.Templates
@using Fran.Components

<FaBillingTemplate BrandText="MyApp" BrandHref="/"
                   Title="Plans &amp; Billing"
                   Description="Manage your workspace subscription, credit cards, and invoice history.">
    <Sidebar>
        <nav class="fa-nav-tree">
            <a href="/settings">General</a>
            <a href="/billing" class="active">Billing &amp; Plans</a>
            <a href="/activity">Audit Log</a>
        </nav>
    </Sidebar>

    <HeaderAction>
        <FaButton Variant="FaButtonVariant.Primary">Change Plan</FaButton>
    </HeaderAction>

    <CurrentPlanContent>
        <FaCard>
            <div class="fa-flex fa-justify-between fa-items-center">
                <div>
                    <h3>Enterprise Cloud</h3>
                    <p class="fa-text-muted">$99 / user / month · Billed annually</p>
                </div>
                <FaBadge Variant="FaBadgeVariant.Success">Active</FaBadge>
            </div>
        </FaCard>
    </CurrentPlanContent>

    <PaymentMethodsContent>
        <FaCard>
            <p><strong>Visa</strong> ending in 4242 (Expires 12/28)</p>
        </FaCard>
    </PaymentMethodsContent>

    <BillingDetailsContent>
        <FaCard>
            <p><strong>Acme Corporation</strong><br />123 Tech Boulevard, Suite 400<br />San Francisco, CA 94105</p>
        </FaCard>
    </BillingDetailsContent>

    <InvoicesContent>
        <FaTable>
            <thead>
                <tr>
                    <th>Date</th>
                    <th>Invoice #</th>
                    <th>Amount</th>
                    <th>Status</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>Oct 1, 2026</td>
                    <td>INV-2026-1001</td>
                    <td>$1,188.00</td>
                    <td><FaBadge Variant="FaBadgeVariant.Success">Paid</FaBadge></td>
                </tr>
            </tbody>
        </FaTable>
    </InvoicesContent>
</FaBillingTemplate>
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
| `Title` | `string` | page title; defaults to "Plans & Billing" |
| `Description` | `string?` | introductory guidance |
| `HeaderAction` | `RenderFragment?` | upgrade or plan action button |
| `CurrentPlanContent` | `RenderFragment?` | subscription tier card |
| `PaymentMethodsContent` | `RenderFragment?` | credit cards and payment methods |
| `BillingDetailsContent` | `RenderFragment?` | billing contact and tax information |
| `InvoicesContent` | `RenderFragment?` | invoice history table |
| `ChildContent` | `RenderFragment?` | optional additional content |
