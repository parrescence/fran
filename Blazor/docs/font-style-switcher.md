[← Back to index](index.md)

# FaFontStyleSwitcher

A switcher for the application-wide font style axis (`data-fa-font-style`): an independent axis alongside [palette](palette-switcher.md), [theme mode](theme-switcher.md), [input style](input-style-switcher.md), and [UI style](ui-style-switcher.md).

Can be rendered as a compact `<select>` dropdown (default) or as a segmented button group via `AsDropdown="false"`.

## Usage

### Dropdown Mode (Default)

```razor
<FaFontStyleSwitcher />
```

### Segmented Buttons Mode

```razor
<FaFontStyleSwitcher AsDropdown="false" />
```

## The 10 Font Styles

- **Flow** (`flow`, default) — the signature Fran rounded look: `'Baloo 2'`, ui-rounded, `'Segoe UI Rounded'`, `'Nunito'`, system-ui, sans-serif. Warm, friendly, and approachable.
- **DOS** (`dos`) — classic retro PC / MS-DOS monospace: `'Fixedsys'`, `'DOS VGA'`, `'Courier New'`, monospace. Features crisp uppercase display, zero border radius, and nostalgic terminal box geometry.
- **CLI** (`cli`) — modern programmer console: `'Cascadia Code'`, `'Fira Code'`, `'JetBrains Mono'`, `'Consolas'`, `'Menlo'`, monospace. Compact, technical, and precise.
- **Elementary School** (`elementary`) — primary education / handwriting cursive: `'Comic Sans MS'`, `'Chalkboard SE'`, `'Comic Neue'`, `'Short Stack'`, `'Fredoka'`, cursive, sans-serif. Playful, friendly, and easy to read.
- **College** (`college`) — varsity athletic / collegiate slab serif: `'Rockwell'`, `'Clarendon'`, `'Impact'`, `'College'`, serif. Heavy academic slab serifs, uppercase letterman jacket aesthetics.
- **Flowing** (`flowing`) — fluid cursive and calligraphic script display: `'Caveat'`, `'Brush Script MT'`, `'Segoe Script'`, `'Dancing Script'`, cursive. Dynamic, organic handwriting cadence.
- **Water** (`water`) — serene, fluid droplet curves: `'Comfortaa'`, `'Quicksand'`, `'Segoe UI Rounded'`, `'Nunito'`, sans-serif. Soft, aqueous wave-like letterforms.
- **Rock** (`rock`) — heavy, brutalist, chiseled stone: `'Impact'`, `'Haettenschweiler'`, `'Arial Black'`, `'Franklin Gothic Heavy'`, sans-serif. Monumental, ultra-heavy display typography with hard edges.
- **Comical** (`comical`) — comic book / cartoon pop-art: `'Comic Sans MS'`, `'Bangers'`, `'Chalkboard'`, cursive, fantasy, sans-serif. Animated pop energy with punchy speech-bubble character.
- **Contrasting** (`contrasting`) — harmonic octave register pairing: dramatic high-contrast Didone display serif (`'Playfair Display'`, `'Didot'`, `'Bodoni MT'`, `'Cinzel'`, serif) paired with clean geometric monospace/sans body. Like musical octaves or contrasting accent colors, it introduces intentional typographic tension and resonance between headlines and interactive controls.

## Getting the value

Pure client-side — no Blazor state needed. The chosen style is stored in `localStorage` (`fa-font-style` key) and reflected as `data-fa-font-style` on `<html>`.

JavaScript API:
```javascript
// Set font style
window.faSetFontStyle('dos');

// Read active font style (defaults to 'flow')
var current = window.faGetFontStyle();
```

[← Back to index](index.md)
