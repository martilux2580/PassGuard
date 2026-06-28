/**
 * colors.ts
 *
 * Cyberpunk 2077 colour palette for PassGuard Mobile.
 *
 * Inspired by the game's visual identity:
 *   • Near-black backgrounds
 *   • Neon electric yellow as the primary brand colour
 *   • Electric cyan as a secondary accent
 *   • Hot-pink / magenta for highlights
 *   • Red for destructive / danger actions
 */

export const Colors = {
  // ── Backgrounds ─────────────────────────────────────────────────────────────
  /** Main app background */
  background: '#0A0A0F',
  /** Slightly lighter surface (cards, modals) */
  surface: '#13131A',
  /** Card border / input background */
  surfaceAlt: '#1C1C28',

  // ── Primary accent (neon electric yellow) ─────────────────────────────────
  primary: '#E8FF00',
  primaryDim: '#9AB000',

  // ── Secondary accent (electric cyan) ─────────────────────────────────────────
  cyan: '#00F5FF',
  cyanDim: '#00A8B0',

  // ── Tertiary accent (hot pink / magenta) ─────────────────────────────────────
  pink: '#FF006E',
  pinkDim: '#99003D',

  // ── Danger / destructive (red) ────────────────────────────────────────────────
  danger: '#FF003C',
  dangerDim: '#800020',

  // ── Success (green) ───────────────────────────────────────────────────────────
  success: '#39FF14',
  successDim: '#1E8A07',

  // ── Text ─────────────────────────────────────────────────────────────────────
  textPrimary: '#F0F0F0',
  textSecondary: '#9090A0',
  textDisabled: '#404050',

  // ── Utility ───────────────────────────────────────────────────────────────────
  border: '#2A2A3A',
  borderAccent: '#E8FF0040',
  transparent: 'transparent',
} as const;
