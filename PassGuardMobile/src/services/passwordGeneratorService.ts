/**
 * passwordGeneratorService.ts – Length: 12–64, default 24.
 */

const CHARS_UPPER   = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ';
const CHARS_LOWER   = 'abcdefghijklmnopqrstuvwxyz';
const CHARS_NUMBERS = '0123456789';
const CHARS_SYMBOLS = '!@#~$%&/()=?+*[]{}ñÑ-_.:,;<>';

export interface GeneratorOptions {
  length: number;       // 12–64, default 24
  useNumbers: boolean;
  useSymbols: boolean;
}

export function generatePassword(options: GeneratorOptions): string {
  const { length, useNumbers, useSymbols } = options;

  if (length < 12 || length > 64) {
    throw new RangeError('Password length must be between 12 and 64.');
  }

  let pool = CHARS_UPPER + CHARS_LOWER;
  if (useNumbers) pool += CHARS_NUMBERS;
  if (useSymbols) pool += CHARS_SYMBOLS;

  // Guarantee one char per enabled group, but never exceed target length
  const guaranteed: string[] = [];
  if (guaranteed.length < length) guaranteed.push(randomChar(CHARS_UPPER));
  if (guaranteed.length < length) guaranteed.push(randomChar(CHARS_LOWER));
  if (useNumbers && guaranteed.length < length) guaranteed.push(randomChar(CHARS_NUMBERS));
  if (useSymbols && guaranteed.length < length) guaranteed.push(randomChar(CHARS_SYMBOLS));

  const chars = [...guaranteed];
  for (let i = chars.length; i < length; i++) chars.push(randomChar(pool));

  for (let i = chars.length - 1; i > 0; i--) {
    const j = secureRandomInt(i + 1);
    [chars[i], chars[j]] = [chars[j], chars[i]];
  }

  return chars.join('');
}

function secureRandomInt(max: number): number {
  if (typeof global.crypto !== 'undefined' && global.crypto.getRandomValues) {
    const LIMIT = 256 - (256 % max);
    const buf = new Uint8Array(1);
    let val: number;
    do { global.crypto.getRandomValues(buf); val = buf[0]; } while (val >= LIMIT);
    return val % max;
  }
  return Math.floor(Math.random() * max);
}

function randomChar(charset: string): string {
  return charset[secureRandomInt(charset.length)];
}
