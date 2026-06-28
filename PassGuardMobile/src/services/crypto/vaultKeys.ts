/**
 * vaultKeys.ts
 *
 * Derives the two keys used by PassGuard Desktop to open a vault.
 *
 * ── vKey  (Vault Key – used to decrypt the .encrypted file) ─────────────────
 *   PBKDF2-SHA256( UTF8(email+pass), base64Decode(SK), 650060, 32 )
 *
 * ── cKey  (Content Key – used to decrypt individual DB fields) ───────────────
 *   C# (VaultContentUC.cs constructor):
 *     keyVStr = Encoding.Default.GetString(vKey)          // UTF-8 with replacement
 *     skStr   = Encoding.Default.GetString(base64Decode(SK))
 *     cKey    = PBKDF2-SHA256(
 *                 UTF8(keyVStr + email + masterPassword),
 *                 UTF8(skStr + keyVStr),
 *                 650060, 32
 *               )
 */

import { Buffer } from 'buffer';
import { deriveKey } from './pbkdf2Service';

function bytesToUtf8WithReplacement(bytes: Uint8Array): string {
  return new TextDecoder('utf-8', { fatal: false }).decode(bytes);
}

function utf8Encode(str: string): Uint8Array {
  return new TextEncoder().encode(str);
}

export async function deriveVaultKey(
  email: string,
  masterPassword: string,
  securityKey: string,
  signal?: AbortSignal,
  onProgress?: (pct: number) => void,
): Promise<Buffer> {
  const salt     = Buffer.from(securityKey, 'base64');
  const password = email + masterPassword;
  return deriveKey(password, salt, signal, onProgress);
}

export async function deriveContentKey(
  vKey: Buffer,
  email: string,
  masterPassword: string,
  securityKey: string,
  signal?: AbortSignal,
  onProgress?: (pct: number) => void,
): Promise<Buffer> {
  const skBytes     = Buffer.from(securityKey, 'base64');
  const keyVStr     = bytesToUtf8WithReplacement(new Uint8Array(vKey));
  const skStr       = bytesToUtf8WithReplacement(new Uint8Array(skBytes));
  const passwordStr = keyVStr + email + masterPassword;
  const saltBytes   = utf8Encode(skStr + keyVStr);
  return deriveKey(passwordStr, saltBytes, signal, onProgress);
}
