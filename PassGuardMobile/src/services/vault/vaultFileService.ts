/**
 * vaultFileService.ts
 *
 * File-level vault operations:
 *   1. Read the .encrypted file.
 *   2. Derive vKey → decrypt → write temp SQLite.
 *   3. Derive cKey (for field decryption).
 *   4. Cleanup temp file when done.
 *
 * Progress reporting:
 *   0–50%   vKey derivation
 *   50–100% cKey derivation
 */

import * as FileSystem from 'expo-file-system/legacy';
import { Buffer } from 'buffer';
import { decryptVaultFile } from '../crypto/aesService';
import { deriveVaultKey, deriveContentKey } from '../crypto/vaultKeys';

const TEMP_DB_FILENAME = 'passguard_temp_vault.db3';

export function getTempVaultPath(): string {
  return FileSystem.cacheDirectory + TEMP_DB_FILENAME;
}

export interface VaultCredentials {
  email: string;
  masterPassword: string;
  securityKey: string;
}

export interface DerivedKeys {
  vKey: Buffer;
  cKey: Buffer;
}

export async function openVault(
  encryptedFilePath: string,
  credentials: VaultCredentials,
  signal?: AbortSignal,
  onProgress?: (pct: number) => void,
): Promise<DerivedKeys> {
  const { email, masterPassword, securityKey } = credentials;

  // 1. Validate Security Key
  let saltBytes: Buffer;
  try {
    saltBytes = Buffer.from(securityKey, 'base64');
    if (saltBytes.length === 0) throw new Error('empty');
  } catch {
    throw new Error('The Security Key is not valid Base64. Check that you copied it correctly.');
  }

  // 2. Derive vKey (0–50% progress)
  let vKey: Buffer;
  try {
    vKey = await deriveVaultKey(
      email, masterPassword, securityKey,
      signal,
      (pct) => onProgress?.(pct * 0.5),
    );
  } catch (e) {
    if ((e as Error).name === 'AbortError') throw e;
    throw new Error('Key derivation failed: ' + String(e));
  }

  if (signal?.aborted) { const e = new Error('Aborted'); e.name = 'AbortError'; throw e; }

  // 3. Read the encrypted vault file
  let fileBase64: string;
  try {
    fileBase64 = await FileSystem.readAsStringAsync(encryptedFilePath, {
      encoding: FileSystem.EncodingType.Base64,
    });
  } catch {
    throw new Error('Could not read the vault file. Make sure the app has permission to access it.');
  }

  const fileBytes = new Uint8Array(Buffer.from(fileBase64, 'base64'));

  // 4. Decrypt
  let sqliteBytes: Uint8Array;
  try {
    sqliteBytes = decryptVaultFile(new Uint8Array(vKey), fileBytes);
  } catch {
    throw new Error('Could not decrypt the vault. Check your Email, Password, and Security Key.');
  }

  // 5. Sanity check
  const magic = Buffer.from(sqliteBytes.slice(0, 15)).toString('ascii');
  if (!magic.startsWith('SQLite format')) {
    throw new Error('Decrypted data is not a valid SQLite database. Credentials may be incorrect.');
  }

  // 6. Write temp file
  const tempPath = getTempVaultPath();
  await FileSystem.writeAsStringAsync(
    tempPath,
    Buffer.from(sqliteBytes).toString('base64'),
    { encoding: FileSystem.EncodingType.Base64 },
  );

  // 7. Derive cKey (50–100% progress)
  let cKey: Buffer;
  try {
    cKey = await deriveContentKey(
      vKey, email, masterPassword, securityKey,
      signal,
      (pct) => onProgress?.(50 + pct * 0.5),
    );
  } catch (e) {
    if ((e as Error).name === 'AbortError') throw e;
    throw new Error('Content key derivation failed: ' + String(e));
  }

  return { vKey, cKey };
}

export async function cleanupTempVault(): Promise<void> {
  const tempPath = getTempVaultPath();
  try {
    const info = await FileSystem.getInfoAsync(tempPath);
    if (info.exists) {
      await FileSystem.deleteAsync(tempPath, { idempotent: true });
    }
  } catch {
    console.warn('PassGuard: could not delete temp vault file.');
  }
}
