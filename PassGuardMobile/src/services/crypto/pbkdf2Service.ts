/**
 * pbkdf2Service.ts
 *
 * PBKDF2-SHA256, 650 060 iterations, 32-byte output.
 * Matches: Rfc2898DeriveBytes(password, salt, 650_060, SHA256).GetBytes(32)
 *
 * Key derivation paths (tried in order):
 *   1. react-native-quick-crypto — native OpenSSL, < 1 s  ✓ (requires prebuild)
 *   2. globalThis.crypto.subtle  — native Web Crypto,  < 1 s  (RN 0.73+ Hermes)
 *   3. Pure-JS chunked loop      — slow but cancellable, used only as last resort
 *
 * The AbortSignal / onProgress only apply to path 3 (pure JS).
 * Paths 1 and 2 complete in under a second, so cancellation is not needed.
 */

import { Buffer } from 'buffer';

// ── Path 1: react-native-quick-crypto (native OpenSSL) ──────────────────────

let _nativePbkdf2: (
  (
    password: string | Buffer,
    salt: string | Buffer,
    iterations: number,
    keylen: number,
    digest: string,
    callback: (err: Error | null, key: Buffer) => void,
  ) => void
) | undefined;

try {
  // Dynamic require so that if the native module is absent (e.g. Expo Go without
  // prebuild) the app doesn't crash — it just falls through to the next path.
  // eslint-disable-next-line @typescript-eslint/no-var-requires
  const qc = require('react-native-quick-crypto');
  _nativePbkdf2 = qc.pbkdf2 ?? qc.default?.pbkdf2;
} catch {
  _nativePbkdf2 = undefined;
}

async function deriveKeyNative(password: string, salt: Uint8Array): Promise<Buffer> {
  return new Promise<Buffer>((resolve, reject) => {
    _nativePbkdf2!(
      password,
      Buffer.from(salt),
      650_060,
      32,
      'sha256',
      (err, key) => {
        if (err) reject(err);
        else resolve(key);
      },
    );
  });
}

// ── Path 2: Web Crypto subtle (available in some Hermes builds) ──────────────

const _subtle: SubtleCrypto | undefined =
  (typeof globalThis !== 'undefined' && (globalThis as any).crypto?.subtle) ||
  (typeof global !== 'undefined' && (global as any).crypto?.subtle) ||
  undefined;

async function deriveKeyWebCrypto(password: string, salt: Uint8Array): Promise<Buffer> {
  const enc     = new TextEncoder();
  const baseKey = await _subtle!.importKey(
    'raw', enc.encode(password), 'PBKDF2', false, ['deriveBits'],
  );
  const bits = await _subtle!.deriveBits(
    { name: 'PBKDF2', salt, iterations: 650_060, hash: 'SHA-256' },
    baseKey,
    256,
  );
  return Buffer.from(bits);
}

// ── Path 3: Pure-JS SHA-256 + HMAC + PBKDF2 (chunked, cancellable) ──────────

const K = new Uint32Array([
  0x428a2f98, 0x71374491, 0xb5c0fbcf, 0xe9b5dba5,
  0x3956c25b, 0x59f111f1, 0x923f82a4, 0xab1c5ed5,
  0xd807aa98, 0x12835b01, 0x243185be, 0x550c7dc3,
  0x72be5d74, 0x80deb1fe, 0x9bdc06a7, 0xc19bf174,
  0xe49b69c1, 0xefbe4786, 0x0fc19dc6, 0x240ca1cc,
  0x2de92c6f, 0x4a7484aa, 0x5cb0a9dc, 0x76f988da,
  0x983e5152, 0xa831c66d, 0xb00327c8, 0xbf597fc7,
  0xc6e00bf3, 0xd5a79147, 0x06ca6351, 0x14292967,
  0x27b70a85, 0x2e1b2138, 0x4d2c6dfc, 0x53380d13,
  0x650a7354, 0x766a0abb, 0x81c2c92e, 0x92722c85,
  0xa2bfe8a1, 0xa81a664b, 0xc24b8b70, 0xc76c51a3,
  0xd192e819, 0xd6990624, 0xf40e3585, 0x106aa070,
  0x19a4c116, 0x1e376c08, 0x2748774c, 0x34b0bcb5,
  0x391c0cb3, 0x4ed8aa4a, 0x5b9cca4f, 0x682e6ff3,
  0x748f82ee, 0x78a5636f, 0x84c87814, 0x8cc70208,
  0x90befffa, 0xa4506ceb, 0xbef9a3f7, 0xc67178f2,
]);
const H0 = new Uint32Array([
  0x6a09e667, 0xbb67ae85, 0x3c6ef372, 0xa54ff53a,
  0x510e527f, 0x9b05688c, 0x1f83d9ab, 0x5be0cd19,
]);

const _W    = new Uint32Array(64);
const IH    = new Uint32Array(8);
const OH    = new Uint32Array(8);
const HOT_BLK = new Uint8Array(64);
HOT_BLK[32] = 0x80;
HOT_BLK[62] = 0x03;

function compress(h: Uint32Array, m: Uint8Array, off: number): void {
  let j = off;
  for (let i = 0; i < 16; i++, j += 4)
    _W[i] = ((m[j] << 24) | (m[j+1] << 16) | (m[j+2] << 8) | m[j+3]) >>> 0;
  for (let i = 16; i < 64; i++) {
    const v15 = _W[i-15], v2 = _W[i-2];
    const s0 = (((v15>>>7)|(v15<<25))^((v15>>>18)|(v15<<14))^(v15>>>3))>>>0;
    const s1 = (((v2>>>17)|(v2<<15))^((v2>>>19)|(v2<<13))^(v2>>>10))>>>0;
    _W[i] = (_W[i-16]+s0+_W[i-7]+s1)>>>0;
  }
  let a=h[0],b=h[1],c=h[2],d=h[3],e=h[4],f=h[5],g=h[6],hh=h[7];
  for (let i = 0; i < 64; i++) {
    const S1=(((e>>>6)|(e<<26))^((e>>>11)|(e<<21))^((e>>>25)|(e<<7)))>>>0;
    const ch=((e&f)^(~e&g))>>>0;
    const t1=(hh+S1+ch+K[i]+_W[i])>>>0;
    const S0=(((a>>>2)|(a<<30))^((a>>>13)|(a<<19))^((a>>>22)|(a<<10)))>>>0;
    const mj=((a&b)^(a&c)^(b&c))>>>0;
    const t2=(S0+mj)>>>0;
    hh=g;g=f;f=e;e=(d+t1)>>>0;d=c;c=b;b=a;a=(t1+t2)>>>0;
  }
  h[0]=(h[0]+a)>>>0;h[1]=(h[1]+b)>>>0;h[2]=(h[2]+c)>>>0;h[3]=(h[3]+d)>>>0;
  h[4]=(h[4]+e)>>>0;h[5]=(h[5]+f)>>>0;h[6]=(h[6]+g)>>>0;h[7]=(h[7]+hh)>>>0;
}

function sha256cont(st: Uint32Array, data: Uint8Array, pre: number): Uint8Array {
  const h=new Uint32Array(st), len=data.length;
  const pLen=((len+9+63)&~63), buf=new Uint8Array(pLen);
  buf.set(data); buf[len]=0x80;
  const bits=(pre+len)*8;
  buf[pLen-4]=(bits>>>24)&0xff;buf[pLen-3]=(bits>>>16)&0xff;
  buf[pLen-2]=(bits>>>8)&0xff;buf[pLen-1]=bits&0xff;
  for (let i=0;i<pLen;i+=64) compress(h,buf,i);
  const out=new Uint8Array(32);
  for (let i=0;i<8;i++){const v=h[i];out[i*4]=(v>>>24)&0xff;out[i*4+1]=(v>>>16)&0xff;out[i*4+2]=(v>>>8)&0xff;out[i*4+3]=v&0xff;}
  return out;
}
function sha256(data: Uint8Array): Uint8Array { return sha256cont(H0,data,0); }

interface HmacKey { iSt: Uint32Array; oSt: Uint32Array; }
function hmacInit(key: Uint8Array): HmacKey {
  const k=key.length>64?sha256(key):key, kp=new Uint8Array(64); kp.set(k);
  const ip=new Uint8Array(64), op=new Uint8Array(64);
  for (let i=0;i<64;i++){ip[i]=kp[i]^0x36;op[i]=kp[i]^0x5c;}
  const iSt=new Uint32Array(H0); compress(iSt,ip,0);
  const oSt=new Uint32Array(H0); compress(oSt,op,0);
  return {iSt,oSt};
}
function hmacCompute(hk: HmacKey, data: Uint8Array): Uint8Array {
  return sha256cont(hk.oSt,sha256cont(hk.iSt,data,64),64);
}
function hmacFast(iSt: Uint32Array, oSt: Uint32Array, u: Uint8Array): void {
  HOT_BLK[ 0]=u[ 0];HOT_BLK[ 1]=u[ 1];HOT_BLK[ 2]=u[ 2];HOT_BLK[ 3]=u[ 3];
  HOT_BLK[ 4]=u[ 4];HOT_BLK[ 5]=u[ 5];HOT_BLK[ 6]=u[ 6];HOT_BLK[ 7]=u[ 7];
  HOT_BLK[ 8]=u[ 8];HOT_BLK[ 9]=u[ 9];HOT_BLK[10]=u[10];HOT_BLK[11]=u[11];
  HOT_BLK[12]=u[12];HOT_BLK[13]=u[13];HOT_BLK[14]=u[14];HOT_BLK[15]=u[15];
  HOT_BLK[16]=u[16];HOT_BLK[17]=u[17];HOT_BLK[18]=u[18];HOT_BLK[19]=u[19];
  HOT_BLK[20]=u[20];HOT_BLK[21]=u[21];HOT_BLK[22]=u[22];HOT_BLK[23]=u[23];
  HOT_BLK[24]=u[24];HOT_BLK[25]=u[25];HOT_BLK[26]=u[26];HOT_BLK[27]=u[27];
  HOT_BLK[28]=u[28];HOT_BLK[29]=u[29];HOT_BLK[30]=u[30];HOT_BLK[31]=u[31];
  IH[0]=iSt[0];IH[1]=iSt[1];IH[2]=iSt[2];IH[3]=iSt[3];
  IH[4]=iSt[4];IH[5]=iSt[5];IH[6]=iSt[6];IH[7]=iSt[7];
  compress(IH,HOT_BLK,0);
  let v: number;
  v=IH[0];HOT_BLK[ 0]=(v>>>24)&0xff;HOT_BLK[ 1]=(v>>>16)&0xff;HOT_BLK[ 2]=(v>>>8)&0xff;HOT_BLK[ 3]=v&0xff;
  v=IH[1];HOT_BLK[ 4]=(v>>>24)&0xff;HOT_BLK[ 5]=(v>>>16)&0xff;HOT_BLK[ 6]=(v>>>8)&0xff;HOT_BLK[ 7]=v&0xff;
  v=IH[2];HOT_BLK[ 8]=(v>>>24)&0xff;HOT_BLK[ 9]=(v>>>16)&0xff;HOT_BLK[10]=(v>>>8)&0xff;HOT_BLK[11]=v&0xff;
  v=IH[3];HOT_BLK[12]=(v>>>24)&0xff;HOT_BLK[13]=(v>>>16)&0xff;HOT_BLK[14]=(v>>>8)&0xff;HOT_BLK[15]=v&0xff;
  v=IH[4];HOT_BLK[16]=(v>>>24)&0xff;HOT_BLK[17]=(v>>>16)&0xff;HOT_BLK[18]=(v>>>8)&0xff;HOT_BLK[19]=v&0xff;
  v=IH[5];HOT_BLK[20]=(v>>>24)&0xff;HOT_BLK[21]=(v>>>16)&0xff;HOT_BLK[22]=(v>>>8)&0xff;HOT_BLK[23]=v&0xff;
  v=IH[6];HOT_BLK[24]=(v>>>24)&0xff;HOT_BLK[25]=(v>>>16)&0xff;HOT_BLK[26]=(v>>>8)&0xff;HOT_BLK[27]=v&0xff;
  v=IH[7];HOT_BLK[28]=(v>>>24)&0xff;HOT_BLK[29]=(v>>>16)&0xff;HOT_BLK[30]=(v>>>8)&0xff;HOT_BLK[31]=v&0xff;
  OH[0]=oSt[0];OH[1]=oSt[1];OH[2]=oSt[2];OH[3]=oSt[3];
  OH[4]=oSt[4];OH[5]=oSt[5];OH[6]=oSt[6];OH[7]=oSt[7];
  compress(OH,HOT_BLK,0);
  v=OH[0];u[ 0]=(v>>>24)&0xff;u[ 1]=(v>>>16)&0xff;u[ 2]=(v>>>8)&0xff;u[ 3]=v&0xff;
  v=OH[1];u[ 4]=(v>>>24)&0xff;u[ 5]=(v>>>16)&0xff;u[ 6]=(v>>>8)&0xff;u[ 7]=v&0xff;
  v=OH[2];u[ 8]=(v>>>24)&0xff;u[ 9]=(v>>>16)&0xff;u[10]=(v>>>8)&0xff;u[11]=v&0xff;
  v=OH[3];u[12]=(v>>>24)&0xff;u[13]=(v>>>16)&0xff;u[14]=(v>>>8)&0xff;u[15]=v&0xff;
  v=OH[4];u[16]=(v>>>24)&0xff;u[17]=(v>>>16)&0xff;u[18]=(v>>>8)&0xff;u[19]=v&0xff;
  v=OH[5];u[20]=(v>>>24)&0xff;u[21]=(v>>>16)&0xff;u[22]=(v>>>8)&0xff;u[23]=v&0xff;
  v=OH[6];u[24]=(v>>>24)&0xff;u[25]=(v>>>16)&0xff;u[26]=(v>>>8)&0xff;u[27]=v&0xff;
  v=OH[7];u[28]=(v>>>24)&0xff;u[29]=(v>>>16)&0xff;u[30]=(v>>>8)&0xff;u[31]=v&0xff;
}

const TOTAL_ITERS = 650_060;
const CHUNK_SIZE  = 5_000;

function abortError(): Error {
  const e = new Error('Aborted');
  e.name = 'AbortError';
  return e;
}

async function deriveKeyPureJS(
  password: string,
  salt: Uint8Array,
  signal?: AbortSignal,
  onProgress?: (pct: number) => void,
): Promise<Buffer> {
  if (signal?.aborted) throw abortError();

  const pw = new TextEncoder().encode(password);
  const hk = hmacInit(pw);

  const blk1 = new Uint8Array(salt.length + 4);
  blk1.set(salt);
  blk1[salt.length + 3] = 1;
  const u = new Uint8Array(hmacCompute(hk, blk1));
  const T = new Uint8Array(u);

  for (let i = 2; i <= TOTAL_ITERS; i++) {
    hmacFast(hk.iSt, hk.oSt, u);
    for (let j = 0; j < 32; j++) T[j] ^= u[j];

    if (i % CHUNK_SIZE === 0) {
      await new Promise<void>((r) => setTimeout(r, 0));
      if (signal?.aborted) throw abortError();
      onProgress?.(i / TOTAL_ITERS * 100);
    }
  }

  onProgress?.(100);
  return Buffer.from(T);
}

// ── Public API ────────────────────────────────────────────────────────────────

/**
 * Derives a 32-byte key with PBKDF2-SHA256 (650 060 iterations).
 * Uses the fastest available implementation automatically.
 *
 * @param signal      AbortSignal — only honoured in the pure-JS fallback path.
 * @param onProgress  0–100 progress — only fired in the pure-JS fallback path.
 */
export async function deriveKey(
  password: string,
  salt: Uint8Array,
  signal?: AbortSignal,
  onProgress?: (pct: number) => void,
): Promise<Buffer> {
  // Path 1 — native OpenSSL via react-native-quick-crypto (fastest, < 1 s)
  if (_nativePbkdf2) {
    try {
      return await deriveKeyNative(password, salt);
    } catch (e) {
      console.warn('quick-crypto pbkdf2 failed, falling back:', e);
    }
  }

  // Path 2 — Web Crypto subtle (available on some Hermes builds)
  if (_subtle) {
    try {
      return await deriveKeyWebCrypto(password, salt);
    } catch (e) {
      console.warn('crypto.subtle PBKDF2 failed, falling back:', e);
    }
  }

  // Path 3 — pure JS, chunked and cancellable (slowest)
  return deriveKeyPureJS(password, salt, signal, onProgress);
}
