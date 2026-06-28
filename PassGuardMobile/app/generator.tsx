/**
 * generator.tsx  –  Password Generator Screen
 *
 * Scroll fix: the result card NEVER changes height during generation.
 * During loading we dim the existing content (opacity 0.35) instead of
 * swapping it for a spinner. Zero height change → zero scroll movement.
 */

import React, { useState, useCallback, useRef, useEffect } from 'react';
import {
  View,
  Text,
  StyleSheet,
  ScrollView,
  Switch,
  Alert,
  PanResponder,
  TextInput,
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';
import { Colors } from '../src/constants/colors';
import ScreenHeader from '../src/components/ScreenHeader';
import CyberpunkButton from '../src/components/CyberpunkButton';
import CopyButton from '../src/components/CopyButton';
import {
  generatePassword,
  GeneratorOptions,
} from '../src/services/passwordGeneratorService';
import { checkPasswordBreached } from '../src/services/hibpService';

// ── Custom Slider ─────────────────────────────────────────────────────────────

interface LengthSliderProps {
  value: number;
  min: number;
  max: number;
  onChange: (v: number) => void;
}

const THUMB_R = 13;

const LengthSlider: React.FC<LengthSliderProps> = ({ value, min, max, onChange }) => {
  const trackRef = useRef<View>(null);
  const trackPageXRef = useRef(0);
  const trackWidthRef = useRef(0);
  const [trackWidth, setTrackWidth] = useState(0);
  const onChangeRef = useRef(onChange);
  useEffect(() => { onChangeRef.current = onChange; }, [onChange]);

  const [inputText, setInputText] = useState(String(value));
  const prevValueRef = useRef(value);
  if (prevValueRef.current !== value) {
    prevValueRef.current = value;
    setInputText(String(value));
  }

  const clamp = (v: number) => Math.max(min, Math.min(max, Math.round(v)));

  const getValueFromPageX = (pageX: number) => {
    const w = trackWidthRef.current;
    if (w <= 0) return clamp(value);
    const localX = pageX - trackPageXRef.current;
    return clamp(min + (Math.max(0, Math.min(w, localX)) / w) * (max - min));
  };

  const measureTrack = useCallback((e: any) => {
    const w = e?.nativeEvent?.layout?.width ?? 0;
    if (w > 0) { trackWidthRef.current = w; setTrackWidth(w); }
    trackRef.current?.measure((_x, _y, _w, _h, pageX) => {
      trackPageXRef.current = pageX;
    });
  }, []);

  const panResponder = useRef(
    PanResponder.create({
      onStartShouldSetPanResponder: () => true,
      onMoveShouldSetPanResponder: () => true,
      onPanResponderGrant: (evt) => {
        trackRef.current?.measure((_x, _y, width, _h, pageX) => {
          trackPageXRef.current = pageX;
          if (width > 0) { trackWidthRef.current = width; setTrackWidth(width); }
          onChangeRef.current(getValueFromPageX(evt.nativeEvent.pageX));
        });
      },
      onPanResponderMove: (evt) => {
        onChangeRef.current(getValueFromPageX(evt.nativeEvent.pageX));
      },
    })
  ).current;

  const pct = trackWidth > 0 ? (clamp(value) - min) / (max - min) : 0;
  const thumbX = pct * trackWidth;

  const handleInputChange = (text: string) => {
    const digits = text.replace(/[^0-9]/g, '');
    setInputText(digits);
    const n = parseInt(digits, 10);
    if (!isNaN(n) && n >= min && n <= max) onChangeRef.current(n);
  };

  const handleInputBlur = () => {
    const n = parseInt(inputText, 10);
    if (isNaN(n) || n < min) { setInputText(String(min)); onChangeRef.current(min); }
    else if (n > max)        { setInputText(String(max)); onChangeRef.current(max); }
    else                      { setInputText(String(n)); }
  };

  return (
    <View style={sliderStyles.container}>
      <View
        ref={trackRef}
        onLayout={(e) => measureTrack(e)}
        {...panResponder.panHandlers}
        style={sliderStyles.trackWrapper}
        hitSlop={{ top: 16, bottom: 16 }}
      >
        <View style={sliderStyles.trackBg} />
        <View style={[sliderStyles.trackFill, { width: Math.max(0, thumbX) }]} />
        <View style={[sliderStyles.thumb, { left: Math.max(0, thumbX - THUMB_R) }]} />
      </View>
      <View style={sliderStyles.inputWrapper}>
        <TextInput
          style={sliderStyles.input}
          value={inputText}
          onChangeText={handleInputChange}
          onBlur={handleInputBlur}
          keyboardType="number-pad"
          maxLength={2}
          selectTextOnFocus
          returnKeyType="done"
        />
        <Text style={sliderStyles.inputUnit}>chars</Text>
      </View>
    </View>
  );
};

const sliderStyles = StyleSheet.create({
  container: { flexDirection: 'row', alignItems: 'center', gap: 14, paddingTop: 8 },
  trackWrapper: { flex: 1, height: 44, justifyContent: 'center' },
  trackBg: {
    position: 'absolute', left: 0, right: 0, height: 3,
    backgroundColor: Colors.border, borderRadius: 2,
  },
  trackFill: {
    position: 'absolute', left: 0, height: 3,
    backgroundColor: Colors.primary, borderRadius: 2,
  },
  thumb: {
    position: 'absolute',
    width: THUMB_R * 2, height: THUMB_R * 2, borderRadius: THUMB_R,
    backgroundColor: Colors.primary, top: 22 - THUMB_R,
    elevation: 4,
    shadowColor: Colors.primary, shadowOffset: { width: 0, height: 0 },
    shadowOpacity: 0.8, shadowRadius: 6,
  },
  inputWrapper: { flexDirection: 'row', alignItems: 'center', gap: 5 },
  input: {
    width: 48, height: 36, backgroundColor: Colors.surfaceAlt,
    borderWidth: 1, borderColor: Colors.primary + '80', borderRadius: 2,
    color: Colors.primary, fontSize: 14, fontWeight: '800',
    textAlign: 'center', fontFamily: 'monospace',
    paddingHorizontal: 2, paddingVertical: 0,
  },
  inputUnit: { color: Colors.textSecondary, fontSize: 11 },
});

// ── Option row ────────────────────────────────────────────────────────────────

interface OptionRowProps {
  label: string;
  value: boolean;
  onToggle: (v: boolean) => void;
  locked?: boolean;
}

const OptionRow: React.FC<OptionRowProps> = ({ label, value, onToggle, locked = false }) => (
  <View style={optStyles.row}>
    <View style={{ flex: 1 }}>
      <Text style={optStyles.label}>{label}</Text>
      {locked && <Text style={optStyles.lockedBadge}>Always enabled</Text>}
    </View>
    <Switch
      value={value}
      onValueChange={locked ? undefined : onToggle}
      disabled={locked}
      trackColor={{ false: Colors.border, true: Colors.primaryDim }}
      thumbColor={value ? Colors.primary : Colors.textDisabled}
    />
  </View>
);

const optStyles = StyleSheet.create({
  row: {
    flexDirection: 'row', alignItems: 'center', justifyContent: 'space-between',
    paddingVertical: 12, borderBottomWidth: 1, borderBottomColor: Colors.border,
  },
  label: { color: Colors.textPrimary, fontSize: 15, fontWeight: '600' },
  lockedBadge: { color: Colors.textDisabled, fontSize: 10, letterSpacing: 0.5, marginTop: 2 },
});

// ── Main screen ───────────────────────────────────────────────────────────────

const MIN_LENGTH = 12;
const MAX_LENGTH = 64;
const DEFAULT_OPTIONS: GeneratorOptions = { length: 24, useNumbers: true, useSymbols: false };

export default function GeneratorScreen() {
  const [options, setOptions] = useState<GeneratorOptions>(DEFAULT_OPTIONS);
  const [hibpEnabled, setHibpEnabled] = useState(false);
  const [password, setPassword] = useState<string | null>(null);
  const [loading, setLoading] = useState(false);
  const [hibpStatus, setHibpStatus] = useState<'idle' | 'safe' | 'pwned' | 'unavailable'>('idle');
  const [pwnedCount, setPwnedCount] = useState(0);

  const handleGenerate = useCallback(async () => {
    // Start loading — card stays visible with same structure, just dimmed
    setLoading(true);
    // Don't reset hibpStatus/pwnedCount here so badge stays visible while loading
    try {
      if (hibpEnabled) {
        let safePassword: string | null = null;
        // Track the last *checked* candidate so we always display the password
        // whose count we actually measured — never an unchecked new one.
        let lastCheckedPassword = '';
        let lastCount = 0;
        for (let i = 0; i < 10; i++) {
          const candidate = generatePassword(options);
          const result = await checkPasswordBreached(candidate);
          lastCheckedPassword = candidate;
          if (result.isSafe) { safePassword = candidate; break; }
          lastCount = result.pwnedCount;
        }
        if (safePassword) {
          setPassword(safePassword);
          setHibpStatus('safe');
          setPwnedCount(0);
        } else {
          // Show the last verified candidate — count and password are from same lookup
          setPassword(lastCheckedPassword);
          setHibpStatus('pwned');
          setPwnedCount(lastCount);
          Alert.alert(
            'All Attempts Breached',
            'Could not generate a breach-free password in 10 tries. Try enabling Symbols.',
          );
        }
      } else {
        const pwd = generatePassword(options);
        setPassword(pwd);
        try {
          const result = await checkPasswordBreached(pwd);
          setHibpStatus(result.isSafe ? 'safe' : 'pwned');
          setPwnedCount(result.pwnedCount);
        } catch {
          setHibpStatus('unavailable');
        }
      }
    } catch (e) {
      setPassword(generatePassword(options));
      setHibpStatus('unavailable');
      if (hibpEnabled) {
        Alert.alert('HIBP Unavailable', 'Could not reach haveibeenpwned.com.');
      }
    } finally {
      setLoading(false);
    }
  }, [options, hibpEnabled]);

  // HIBP badge text/color
  const hibpColor =
    hibpStatus === 'safe' ? Colors.success :
    hibpStatus === 'pwned' ? Colors.danger :
    Colors.textSecondary;

  const hibpText =
    hibpStatus === 'safe' ? '✓ Not found in any breach' :
    hibpStatus === 'pwned' ? `✗ Found in ${pwnedCount.toLocaleString()} breach${pwnedCount !== 1 ? 'es' : ''}` :
    '';

  return (
    <SafeAreaView style={styles.safe} edges={['top', 'bottom']}>
      <ScreenHeader title="Password Generator" />
      <ScrollView
        contentContainerStyle={styles.scroll}
        showsVerticalScrollIndicator={false}
        keyboardShouldPersistTaps="handled"
      >
        {/* ── Length ──────────────────────────────────────────────────── */}
        <View style={styles.section}>
          <Text style={styles.sectionTitle}>LENGTH</Text>
          <LengthSlider
            value={options.length}
            min={MIN_LENGTH}
            max={MAX_LENGTH}
            onChange={(v) => setOptions((o) => ({ ...o, length: v }))}
          />
        </View>

        {/* ── Character sets ───────────────────────────────────────────── */}
        <View style={styles.section}>
          <Text style={styles.sectionTitle}>CHARACTER SETS</Text>
          <OptionRow label="Uppercase  (A–Z)" value={true} onToggle={() => {}} locked />
          <OptionRow label="Lowercase  (a–z)" value={true} onToggle={() => {}} locked />
          <OptionRow
            label="Numbers  (0–9)"
            value={options.useNumbers}
            onToggle={(v) => setOptions((o) => ({ ...o, useNumbers: v }))}
          />
          <OptionRow
            label="Symbols  (!@#…)"
            value={options.useSymbols}
            onToggle={(v) => setOptions((o) => ({ ...o, useSymbols: v }))}
          />
        </View>

        {/* ── Security ─────────────────────────────────────────────────── */}
        <View style={styles.section}>
          <Text style={styles.sectionTitle}>SECURITY</Text>
          <OptionRow
            label="Safe Mode (HIBP)"
            value={hibpEnabled}
            onToggle={setHibpEnabled}
          />
          <Text style={styles.hibpNote}>
            HIBP is checked on every generation.{'\n'}
            Safe Mode keeps retrying until no breach is found.
          </Text>
        </View>

        {/* ── Generate button ──────────────────────────────────────────── */}
        <CyberpunkButton
          label="Generate"
          onPress={handleGenerate}
          loading={loading}
          style={styles.generateBtn}
        />

        {/* ── Result card ───────────────────────────────────────────────
             Key: card structure is IDENTICAL whether loading or not.
             Only opacity changes → zero height change → zero scroll jump. */}
        {password !== null ? (
          <View style={[styles.resultCard, loading && styles.resultCardLoading]}>
            {/* Badge row — always present once a password has been generated.
                Empty string when unavailable keeps the row height stable. */}
            {hibpStatus !== 'idle' && (
              <Text style={[styles.hibpBadge, { color: hibpColor }]}>
                {hibpText}
              </Text>
            )}
            <Text style={styles.passwordText} selectable={!loading}>
              {password}
            </Text>
            <View style={styles.resultActions}>
              <CopyButton value={password} label="COPY PASSWORD" />
            </View>
          </View>
        ) : null}
      </ScrollView>
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  safe: { flex: 1, backgroundColor: Colors.background },
  scroll: { padding: 20, paddingBottom: 40 },
  section: {
    backgroundColor: Colors.surface, borderRadius: 2, padding: 16,
    marginBottom: 16, borderWidth: 1, borderColor: Colors.border,
  },
  sectionTitle: {
    color: Colors.textSecondary, fontSize: 11, fontWeight: '800',
    letterSpacing: 2, marginBottom: 12,
  },
  hibpNote: { color: Colors.textDisabled, fontSize: 11, marginTop: 8, lineHeight: 16 },
  generateBtn: { marginBottom: 20 },
  resultCard: {
    backgroundColor: Colors.surface, borderRadius: 2,
    borderWidth: 1, borderColor: Colors.primary + '60',
    padding: 20, gap: 14,
  },
  resultCardLoading: {
    opacity: 0.35,
  },
  hibpBadge: { fontSize: 12, fontWeight: '700', letterSpacing: 0.5 },
  passwordText: {
    color: Colors.textPrimary, fontSize: 17, fontFamily: 'monospace',
    letterSpacing: 1, lineHeight: 26,
  },
  resultActions: { flexDirection: 'row', justifyContent: 'flex-end' },
});
