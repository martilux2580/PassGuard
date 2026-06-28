/**
 * aesService.ts
 *
 * Replicates the AES-256-CBC encryption / decryption used by PassGuard Desktop
 * (AESAlgorithm.cs – .NET 7).
 *
 * ── File-level encryption (Encrypt / Decrypt) ──────────────────────────────
 *   The .encrypted vault is structured as:
 *     [ IV (16 bytes) ][ AES-256-CBC encrypted SQLite .db3 bytes ]
 *   Padding: PKCS#7 (the .NET default).
 *
 * ── Field-level encryption (EncryptText / DecryptText) ──────────────────────
 *   Each database field is stored as:
 *     Base64( IV (16 bytes) + AES-256-CBC encrypted UTF-8 text )
 *   Same AES mode and padding.
 */

import aesjs from 'aes-js';
import { Buffer } from 'buffer';

// ─── PKCS#7 padding helpers ──────────────────────────────────────────────────

/** Adds PKCS#7 padding so the data length is a multiple of blockSize (16). */
function pkcs7Pad(data: Uint8Array, blockSize = 16): Uint8Array {
  const padLen = blockSize - (data.length % blockSize);
  const padded = new Uint8Array(data.length + padLen);
  padded.set(data);
  padded.fill(padLen, data.length); // fill padding bytes with the pad length value
  return padded;
}

/**
 * Removes PKCS#7 padding.
 * Throws if the padding is invalid.
 */
function pkcs7Unpad(data: Uint8Array): Uint8Array {
  if (data.length === 0) throw new Error('AES: empty plaintext after decrypt');
  const padLen = data[data.length - 1];
  if (padLen === 0 || padLen > 16) throw new Error('AES: invalid PKCS#7 padding');
  // Verify all padding bytes are correct
  for (let i = data.length - padLen; i < data.length; i++) {
    if (data[i] !== padLen) throw new Error('AES: invalid PKCS#7 padding byte');
  }
  return data.slice(0, data.length - padLen);
}

// ─── Low-level AES-CBC ───────────────────────────────────────────────────────

/**
 * AES-256-CBC encrypt.
 *
 * @param key        32-byte key.
 * @param iv         16-byte initialisation vector.
 * @param plaintext  Raw bytes to encrypt (will be PKCS#7 padded internally).
 * @returns          Ciphertext bytes (same length as padded plaintext).
 */
export function aesCbcEncrypt(
  key: Uint8Array,
  iv: Uint8Array,
  plaintext: Uint8Array,
): Uint8Array {
  const padded = pkcs7Pad(plaintext);
  const aesCbc = new aesjs.ModeOfOperation.cbc(key, iv);
  return aesCbc.encrypt(padded);
}

/**
 * AES-256-CBC decrypt.
 *
 * @param key        32-byte key.
 * @param iv         16-byte initialisation vector.
 * @param ciphertext Encrypted bytes (must be a multiple of 16).
 * @returns          Original plaintext bytes (PKCS#7 padding removed).
 */
export function aesCbcDecrypt(
  key: Uint8Array,
  iv: Uint8Array,
  ciphertext: Uint8Array,
): Uint8Array {
  const aesCbc = new aesjs.ModeOfOperation.cbc(key, iv);
  const padded = aesCbc.decrypt(ciphertext);
  return pkcs7Unpad(padded);
}

// ─── File-level helpers ──────────────────────────────────────────────────────

/** Size of the IV prepended to every encrypted file / field. */
const IV_SIZE = 16;

/**
 * Decrypts a PassGuard vault file.
 *
 * The file bytes are structured as:
 *   [ IV (16 bytes) ][ AES-CBC ciphertext (the SQLite .db3 file) ]
 *
 * @param key        32-byte vault key (vKey).
 * @param fileBytes  Complete bytes of the .encrypted file.
 * @returns          Decrypted SQLite database bytes.
 */
export function decryptVaultFile(
  key: Uint8Array,
  fileBytes: Uint8Array,
): Uint8Array {
  if (fileBytes.length <= IV_SIZE) {
    throw new Error('Vault file is too short to contain a valid IV.');
  }
  const iv = fileBytes.slice(0, IV_SIZE);
  const ciphertext = fileBytes.slice(IV_SIZE);
  return aesCbcDecrypt(key, iv, ciphertext);
}

// ─── Field-level helpers ─────────────────────────────────────────────────────

/**
 * Decrypts a single encrypted database field.
 *
 * Fields are stored as Base64( IV[16] + AES-CBC ciphertext ).
 *
 * @param key           32-byte content key (cKey).
 * @param base64Field   The raw value from the SQLite column.
 * @returns             Decrypted plain-text string, or the original value
 *                      if decryption fails (matches C# fallback behaviour).
 */
export function decryptField(key: Uint8Array, base64Field: string): string {
  try {
    const combined = Buffer.from(base64Field, 'base64');
    if (combined.length <= IV_SIZE) return base64Field; // too short – return as-is
    const iv = combined.slice(0, IV_SIZE);
    const ciphertext = combined.slice(IV_SIZE);
    const plainBytes = aesCbcDecrypt(key, iv, new Uint8Array(ciphertext));
    return Buffer.from(plainBytes).toString('utf8');
  } catch {
    // Matches C# DecryptText catch block: return original on failure
    return base64Field;
  }
}
