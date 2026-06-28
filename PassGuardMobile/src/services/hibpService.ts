/**
 * hibpService.ts
 *
 * Checks whether a password has appeared in a known data breach using the
 * "Have I Been Pwned" (HIBP) Pwned Passwords API v3.
 *
 * Privacy: k-anonymity model – only the first 5 characters of the SHA-1 hash
 * are sent to the API. The full hash never leaves the device.
 *
 * SHA-1 is implemented in pure JavaScript to avoid crypto.subtle compatibility
 * issues across Hermes versions.
 */

const HIBP_API = 'https://api.pwnedpasswords.com/range/';
const TIMEOUT_MS = 10_000;

// ── Pure-JS SHA-1 (RFC 3174) ─────────────────────────────────────────────────

function sha1(message: string): string {
  const bytes = new TextEncoder().encode(message);
  const ml = bytes.length;
  // Padded length: must be multiple of 64, with room for 0x80 byte + 8-byte length
  const bl = Math.ceil((ml + 9) / 64) * 64;
  const buf = new Uint8Array(bl); // initialised to 0
  buf.set(bytes);
  buf[ml] = 0x80;
  // Write bit-length as big-endian 64-bit (upper 32 bits are 0 for short messages)
  const bitLen = ml * 8;
  buf[bl - 4] = (bitLen >>> 24) & 0xff;
  buf[bl - 3] = (bitLen >>> 16) & 0xff;
  buf[bl - 2] = (bitLen >>> 8) & 0xff;
  buf[bl - 1] = bitLen & 0xff;

  let h0 = 0x67452301;
  let h1 = 0xefcdab89;
  let h2 = 0x98badcfe;
  let h3 = 0x10325476;
  let h4 = 0xc3d2e1f0;

  const w = new Uint32Array(80);

  for (let i = 0; i < bl; i += 64) {
    // Load 16 words
    for (let j = 0; j < 16; j++) {
      w[j] =
        ((buf[i + j * 4] << 24) |
          (buf[i + j * 4 + 1] << 16) |
          (buf[i + j * 4 + 2] << 8) |
          buf[i + j * 4 + 3]) >>>
        0;
    }
    // Expand to 80 words
    for (let j = 16; j < 80; j++) {
      const x = (w[j - 3] ^ w[j - 8] ^ w[j - 14] ^ w[j - 16]) >>> 0;
      w[j] = ((x << 1) | (x >>> 31)) >>> 0;
    }

    let a = h0,
      b = h1,
      c = h2,
      d = h3,
      e = h4;

    for (let j = 0; j < 80; j++) {
      let f: number, k: number;
      if (j < 20) {
        f = ((b & c) | (~b & d)) >>> 0;
        k = 0x5a827999;
      } else if (j < 40) {
        f = (b ^ c ^ d) >>> 0;
        k = 0x6ed9eba1;
      } else if (j < 60) {
        f = ((b & c) | (b & d) | (c & d)) >>> 0;
        k = 0x8f1bbcdc;
      } else {
        f = (b ^ c ^ d) >>> 0;
        k = 0xca62c1d6;
      }
      const rotA = ((a << 5) | (a >>> 27)) >>> 0;
      const temp = (rotA + f + e + k + w[j]) >>> 0;
      e = d;
      d = c;
      c = ((b << 30) | (b >>> 2)) >>> 0;
      b = a;
      a = temp;
    }

    h0 = (h0 + a) >>> 0;
    h1 = (h1 + b) >>> 0;
    h2 = (h2 + c) >>> 0;
    h3 = (h3 + d) >>> 0;
    h4 = (h4 + e) >>> 0;
  }

  return [h0, h1, h2, h3, h4]
    .map((h) => h.toString(16).padStart(8, '0'))
    .join('')
    .toUpperCase();
}

// ── HIBP API call ─────────────────────────────────────────────────────────────

export interface HibpResult {
  isSafe: boolean;
  pwnedCount: number;
}

export async function checkPasswordBreached(
  password: string,
): Promise<HibpResult> {
  const hashHex = sha1(password);
  const prefix = hashHex.slice(0, 5);
  const suffix = hashHex.slice(5);

  const controller = new AbortController();
  const timer = setTimeout(() => controller.abort(), TIMEOUT_MS);

  let response: Response;
  try {
    response = await fetch(HIBP_API + prefix, {
      headers: {
        'Add-Padding': 'true',
        'User-Agent': 'PassGuard-Mobile/1.0',
      },
      signal: controller.signal,
    });
  } finally {
    clearTimeout(timer);
  }

  if (!response.ok) {
    throw new Error(`HIBP API error: ${response.status} ${response.statusText}`);
  }

  const text = await response.text();
  for (const line of text.split('\n')) {
    const [lineSuffix, countStr] = line.trim().split(':');
    if (lineSuffix === suffix) {
      return { isSafe: false, pwnedCount: parseInt(countStr, 10) };
    }
  }

  return { isSafe: true, pwnedCount: 0 };
}
