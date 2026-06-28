/**
 * vaultDbService.ts
 *
 * Queries the decrypted SQLite vault and returns plaintext credential data.
 *
 * Search algorithm:
 *   1. Exact match   — query.trim() === name.trim() (case-insensitive)
 *                      → return the full entry
 *   2. Contains      — query.trim() appears literally inside name.trim() (case-insensitive)
 *                      → return up to 3 name suggestions
 *   3. No literal match → { match: null, suggestions: [] }
 *
 * No Levenshtein / fuzzy matching — only exact and literal substring.
 */

import * as SQLite from 'expo-sqlite';
import { Buffer } from 'buffer';
import { decryptField } from '../crypto/aesService';
import { getTempVaultPath } from './vaultFileService';

export interface VaultEntry {
  name: string;
  username: string;
  sitePassword: string;
}

export interface SearchResult {
  match: VaultEntry | null;
  suggestions: string[];
}

export async function searchVault(
  query: string,
  cKey: Buffer,
): Promise<SearchResult> {
  const trimmed = query.trim();
  if (!trimmed) throw new Error('Search query must not be empty.');

  const q = trimmed.toLowerCase();

  const db = await SQLite.openDatabaseAsync(getTempVaultPath());

  let rows: Array<{ Name: string; Username: string; SitePassword: string }>;
  try {
    rows = await db.getAllAsync<{ Name: string; Username: string; SitePassword: string }>(
      'SELECT Name, Username, SitePassword FROM Vault;',
    );
  } finally {
    await db.closeAsync();
  }

  if (rows.length === 0) return { match: null, suggestions: [] };

  const cKeyUint8 = new Uint8Array(cKey);

  // Decrypt all names up-front (username/password only decrypted on exact match)
  const decryptedRows = rows.map((row) => ({
    encUser: row.Username,
    encPass: row.SitePassword,
    decName: decryptField(cKeyUint8, row.Name),
  }));

  // ── 1. Exact match ──────────────────────────────────────────────────────────
  const exact = decryptedRows.find((r) => r.decName.trim().toLowerCase() === q);
  if (exact) {
    return {
      match: {
        name:         exact.decName,
        username:     decryptField(cKeyUint8, exact.encUser),
        sitePassword: decryptField(cKeyUint8, exact.encPass),
      },
      suggestions: [],
    };
  }

  // ── 2. Literal substring ─────────────────────────────────────────────────────
  const containing = decryptedRows.filter(
    (r) => r.decName.trim().toLowerCase().includes(q),
  );
  const suggestions = containing.slice(0, 3).map((r) => r.decName);

  return { match: null, suggestions };
}
