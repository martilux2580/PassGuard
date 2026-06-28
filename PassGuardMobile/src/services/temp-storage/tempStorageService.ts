/**
 * tempStorageService.ts
 *
 * Manages temporary password storage on the device.
 *
 * Security model:
 *   - SQLite database lives in the app's private storage (inaccessible to other apps).
 *   - ALL fields (name, username, password) encrypted with AES-256-CBC.
 *   - Encryption key stored in Android Keystore via expo-secure-store.
 *   - Access gated by biometric / PIN authentication at the screen level.
 *   - Data is lost if the app is uninstalled or its data is cleared.
 *
 * DB schema:
 *   TempEntries(id PK, encName TEXT, encUsername TEXT, encPassword TEXT, savedAt TEXT)
 *
 * Schema migration:
 *   If the old schema (columns: name, username, encryptedPassword) is detected,
 *   the table is dropped and recreated. Old plaintext entries are discarded.
 */

import * as SQLite from 'expo-sqlite';
import * as SecureStore from 'expo-secure-store';
import * as FileSystem from 'expo-file-system/legacy';
import * as ExpoCrypto from 'expo-crypto';
import { Buffer } from 'buffer';
import { aesCbcEncrypt, aesCbcDecrypt } from '../crypto/aesService';

const DB_FILENAME = 'passguard_temp.db3';
const SECURE_STORE_KEY = 'passguard_temp_aes_key';
const KEY_SIZE = 32;
const IV_SIZE = 16;

// ── Key management ─────────────────────────────────────────────────────────────
//
// IMPORTANT: getTempKey() is memoised as a Promise so that concurrent callers
// (e.g. the three encryptTempField calls in Promise.all) all await the *same*
// Promise and therefore use the same key. Without this, each concurrent call
// could generate a different key and overwrite the others in SecureStore,
// causing AES PKCS#7 padding errors on decryption.

let _tempKeyPromise: Promise<Uint8Array> | undefined;

function getTempKey(): Promise<Uint8Array> {
  if (!_tempKeyPromise) {
    _tempKeyPromise = (async (): Promise<Uint8Array> => {
      let keyBase64 = await SecureStore.getItemAsync(SECURE_STORE_KEY);
      if (!keyBase64) {
        const keyBytes = ExpoCrypto.getRandomBytes(KEY_SIZE);
        keyBase64 = Buffer.from(keyBytes).toString('base64');
        await SecureStore.setItemAsync(SECURE_STORE_KEY, keyBase64, {
          requireAuthentication: false,
        });
      }
      return new Uint8Array(Buffer.from(keyBase64, 'base64'));
    })();
  }
  return _tempKeyPromise;
}

// ── Field encryption ───────────────────────────────────────────────────────────

async function encryptTempField(plaintext: string): Promise<string> {
  const key = await getTempKey();
  const iv = ExpoCrypto.getRandomBytes(IV_SIZE);
  const plainBytes = new TextEncoder().encode(plaintext);
  const ciphertext = aesCbcEncrypt(key, iv, plainBytes);
  const combined = new Uint8Array(IV_SIZE + ciphertext.length);
  combined.set(iv, 0);
  combined.set(ciphertext, IV_SIZE);
  return Buffer.from(combined).toString('base64');
}

async function decryptTempField(encryptedBase64: string): Promise<string> {
  const key = await getTempKey();
  const combined = new Uint8Array(Buffer.from(encryptedBase64, 'base64'));
  const iv = combined.slice(0, IV_SIZE);
  const ciphertext = combined.slice(IV_SIZE);
  const plainBytes = aesCbcDecrypt(key, iv, ciphertext);
  return new TextDecoder().decode(plainBytes);
}

// ── DB setup & migration ───────────────────────────────────────────────────────

function getDbPath(): string {
  return FileSystem.documentDirectory + DB_FILENAME;
}

const CREATE_TABLE_SQL = `
  CREATE TABLE IF NOT EXISTS TempEntries (
    id          INTEGER PRIMARY KEY AUTOINCREMENT,
    encName     TEXT NOT NULL,
    encUsername TEXT NOT NULL,
    encPassword TEXT NOT NULL,
    savedAt     TEXT NOT NULL
  );
`;

async function openTempDb(): Promise<SQLite.SQLiteDatabase> {
  const db = await SQLite.openDatabaseAsync(getDbPath());
  await db.execAsync(CREATE_TABLE_SQL);

  // Migration: detect old schema (has plain 'name' column) and recreate
  const columns = await db.getAllAsync<{ name: string }>(
    'PRAGMA table_info(TempEntries);',
  );
  const hasOldSchema = columns.some((c) => c.name === 'name');
  if (hasOldSchema) {
    await db.execAsync('DROP TABLE TempEntries;');
    await db.execAsync(CREATE_TABLE_SQL);
  }

  return db;
}

// ── Public API ─────────────────────────────────────────────────────────────────

export interface TempEntry {
  id: number;
  name: string;
  username: string;
  password: string;
  savedAt: string;
}

interface RawTempRow {
  id: number;
  encName: string;
  encUsername: string;
  encPassword: string;
  savedAt: string;
}

export async function saveTempEntry(
  name: string,
  username: string,
  password: string,
): Promise<void> {
  if (!name.trim()) throw new Error('Name must not be empty.');
  if (!username.trim()) throw new Error('Username must not be empty.');
  if (!password) throw new Error('Password must not be empty.');

  const [encName, encUsername, encPassword] = await Promise.all([
    encryptTempField(name.trim()),
    encryptTempField(username.trim()),
    encryptTempField(password),
  ]);

  const savedAt = new Date().toISOString();
  const db = await openTempDb();
  try {
    await db.runAsync(
      'INSERT INTO TempEntries (encName, encUsername, encPassword, savedAt) VALUES (?, ?, ?, ?);',
      [encName, encUsername, encPassword, savedAt],
    );
  } finally {
    await db.closeAsync();
  }
}

export async function getAllTempEntries(): Promise<TempEntry[]> {
  const db = await openTempDb();
  let rows: RawTempRow[];
  try {
    rows = await db.getAllAsync<RawTempRow>(
      'SELECT id, encName, encUsername, encPassword, savedAt FROM TempEntries ORDER BY savedAt DESC;',
    );
  } finally {
    await db.closeAsync();
  }
  return Promise.all(
    rows.map(async (row) => ({
      id: row.id,
      name: await decryptTempField(row.encName),
      username: await decryptTempField(row.encUsername),
      password: await decryptTempField(row.encPassword),
      savedAt: row.savedAt,
    })),
  );
}

export async function deleteTempEntry(id: number): Promise<void> {
  const db = await openTempDb();
  try {
    await db.runAsync('DELETE FROM TempEntries WHERE id = ?;', [id]);
  } finally {
    await db.closeAsync();
  }
}

/**
 * Returns the number of stored entries without decrypting any data.
 * Returns null if the database file does not exist yet (first install).
 * Returns 0 if the database exists but has no entries.
 * Safe to call from the home screen without authentication.
 */
export async function getTempEntryCount(): Promise<number | null> {
  try {
    const dbPath = getDbPath();
    const info = await FileSystem.getInfoAsync(dbPath);
    if (!info.exists) return null;
    const db = await SQLite.openDatabaseAsync(dbPath);
    try {
      const result = await db.getFirstAsync<{ count: number }>(
        'SELECT COUNT(*) as count FROM TempEntries;',
      );
      return result?.count ?? 0;
    } finally {
      await db.closeAsync();
    }
  } catch {
    return null;
  }
}
