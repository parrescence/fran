[← Back to index](index.md)

# FaInputGroup

A seamless composite container that joins an input element with buttons and static text addons on the left, right, or in between. Modeled after button groups, the exterior wrapper controls the outer border radius while interior borders collapse and outer corners curve seamlessly.

## Usage

### 1. Button on the Right (Search / Apply Action)

```razor
<FaInputGroup>
    <input type="text" class="fa-input" placeholder="Search accounts or payees..." @bind="_searchQuery" />
    <FaButton Variant="FaButtonVariant.Primary" OnClick="HandleSearch">Search</FaButton>
</FaInputGroup>
```

### 2. Button on the Left (Filter / Menu)

```razor
<FaInputGroup>
    <FaButton Variant="FaButtonVariant.Secondary" OnClick="ToggleFilter">Filter</FaButton>
    <input type="text" class="fa-input" placeholder="Search within filtered set..." @bind="_query" />
</FaInputGroup>
```

### 3. Buttons on Both Sides (Input In Between / Stepper)

```razor
<FaInputGroup AriaLabel="Quantity Stepper">
    <FaButton Variant="FaButtonVariant.Secondary" OnClick="Decrement">-</FaButton>
    <input type="number" class="fa-input" style="text-align:center;" @bind="_count" />
    <FaButton Variant="FaButtonVariant.Secondary" OnClick="Increment">+</FaButton>
</FaInputGroup>
```

### 4. Pill Search Bar (Full Pill Border Radius)

```razor
<FaInputGroup Pill="true">
    <input type="text" class="fa-input" placeholder="Quick search..." @bind="_search" />
    <FaButton Variant="FaButtonVariant.Primary">
        <FaIcon Name="FaIconName.Search" Size="14" Color="FaIconColor.White" />
    </FaButton>
</FaInputGroup>
```

### 5. Static Text Addons (`FaInputGroupText`)

```razor
<FaInputGroup Label="Transaction Amount">
    <FaInputGroupText>$</FaInputGroupText>
    <input type="number" step="0.01" class="fa-input" @bind="_amount" />
    <FaInputGroupText>USD</FaInputGroupText>
    <FaButton Variant="FaButtonVariant.Primary" OnClick="SubmitTransfer">Transfer</FaButton>
</FaInputGroup>
```

### 6. Using Slots (`Prepend` and `Append`)

```razor
<FaInputGroup>
    <Prepend>
        <FaButton Variant="FaButtonVariant.Outline">Browse</FaButton>
    </Prepend>
    <ChildContent>
        <input type="text" class="fa-input" @bind="_selectedPath" placeholder="/path/to/file" />
    </ChildContent>
    <Append>
        <FaButton Variant="FaButtonVariant.Primary">Upload</FaButton>
    </Append>
</FaInputGroup>
```

## Parameters

| Parameter | Type | Default | Notes |
|---|---|---|---|
| `Label` | `string?` | `null` | Optional block label rendered above the group in `.fa-label` |
| `HelpText` | `string?` | `null` | Optional helper text rendered below the group in `.fa-help-text` |
| `Size` | `FaSize` | `FaSize.Medium` | `XSmall` \| `Small` \| `Medium` \| `Large` \| `XLarge` — cascades padding to child inputs, buttons, and addons |
| `Pill` | `bool` | `false` | Sets exterior corner radius to full pill (`var(--fa-radius-pill)`) |
| `Inline` | `bool` | `false` | Uses `inline-flex` with intrinsic width instead of 100% flex width |
| `Responsive` | `bool` | `false` | Stretches to 100% width below 720px (`.fa-responsive`) |
| `AriaLabel` | `string?` | `null` | Accessible label on the `role="group"` container |
| `Prepend` | `RenderFragment?` | `null` | Content rendered before `ChildContent` |
| `ChildContent` | `RenderFragment?` | `null` | Main content (inputs, buttons, addons) |
| `Append` | `RenderFragment?` | `null` | Content rendered after `ChildContent` |
| `CssClass` | `string?` | `null` | Extra CSS classes applied to `.fa-input-group` |
| `ContainerCssClass` | `string?` | `null` | Extra CSS classes applied to outer `.fa-field` if Label/HelpText is used |

[← Back to index](index.md)
