/**
 * vault-credentials.tsx  –  Vault Search (unified single-screen)
 *
 * Flow:
 *   1. Select .encrypted vault file
 *   2. Enter credentials (email, master password, security key)
 *   3. Enter search name → tap SEARCH
 *   4. Keys derived, vault decrypted, result shown
 *
 * Cancellation:
 *   Pressing Back (or navigating away) during key derivation aborts the
 *   operation immediately via AbortController. The vault is also auto-locked.
 *
 * Auto-lock:
 *   useFocusEffect cleanup fires whenever the screen loses focus (including
 *   back navigation), clearing cKey and deleting the temp vault file.
 *
 * Credentials are NEVER stored persistently (per spec).
 */

import React, {
  useState,
  useRef,
  useEffect,
  useCallback,
} from 'react';
import {
  View,
  Text,
  StyleSheet,
  ScrollView,
  Pressable,
  Keyboard,
  Alert,
  KeyboardAvoidingView,
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';
import { useNavigation } from 'expo-router';
import * as DocumentPicker from 'expo-document-picker';
import { Buffer } from 'buffer';
import { Colors } from '../src/constants/colors';
import ScreenHeader from '../src/components/ScreenHeader';
import CyberpunkInput from '../src/components/CyberpunkInput';
import CyberpunkButton from '../src/components/CyberpunkButton';
import CopyButton from '../src/components/CopyButton';
import {
  openVault,
  cleanupTempVault,
  VaultCredentials,
} from '../src/services/vault/vaultFileService';
import { searchVault, VaultEntry } from '../src/services/vault/vaultDbService';

// ── Credential row ────────────────────────────────────────────────────────────

interface CredentialRowProps {
  label: string;
  value: string;
  isPassword?: boolean;
}

const CredentialRow: React.FC<CredentialRowProps> = ({ label, value, isPassword = false }) => {
  const [revealed, setRevealed] = useState(false);
  return (
    <View style={rowStyles.wrapper}>
      <Text style={rowStyles.label}>{label}</Text>
      <View style={rowStyles.valueRow}>
        <Text style={rowStyles.value} selectable numberOfLines={1}>
          {isPassword && !revealed ? '•'.repeat(Math.min(value.length, 20)) : value}
        </Text>
        {isPassword && (
          <Text style={rowStyles.toggle} onPress={() => setRevealed((r) => !r)}>
            {revealed ? '🙈' : '👁'}
          </Text>
        )}
        <CopyButton value={value} label={`COPY ${label.toUpperCase()}`} />
      </View>
    </View>
  );
};

const rowStyles = StyleSheet.create({
  wrapper: { marginBottom: 16 },
  label: {
    color: Colors.textSecondary, fontSize: 11, fontWeight: '700',
    letterSpacing: 1, marginBottom: 6, textTransform: 'uppercase',
  },
  valueRow: {
    flexDirection: 'row', alignItems: 'center', gap: 10,
    backgroundColor: Colors.surfaceAlt, borderRadius: 2,
    borderWidth: 1, borderColor: Colors.border,
    paddingVertical: 10, paddingHorizontal: 12,
  },
  value: { flex: 1, color: Colors.textPrimary, fontSize: 15, fontFamily: 'monospace' },
  toggle: { fontSize: 18, padding: 2 },
});

// ── Main screen ───────────────────────────────────────────────────────────────

export default function VaultScreen() {
  // Form fields
  const [vaultPath, setVaultPath] = useState('');
  const [vaultName, setVaultName] = useState('');
  const [email, setEmail]             = useState('');
  const [masterPassword, setMasterPassword] = useState('');
  const [securityKey, setSecurityKey]   = useState('');
  const [query, setQuery]               = useState('');

  // Vault unlock state — cKey cached in memory only
  const [cKey, setCKey] = useState<Buffer | null>(null);

  // UI state
  const [loading, setLoading]         = useState(false);
  const [progress, setProgress]       = useState(0);       // 0–100, only meaningful when loading
  const [match, setMatch]             = useState<VaultEntry | null>(null);
  const [suggestions, setSuggestions] = useState<string[]>([]);
  const [searchError, setSearchError] = useState('');
  const [searched, setSearched]       = useState(false);

  const navigation      = useNavigation();
  const scrollRef       = useRef<ScrollView>(null);
  const abortRef        = useRef<AbortController | null>(null);
  const searchSectionY  = useRef(0);

  // ── Auto-lock & abort only on real back-navigation ──────────────────────────
  // 'beforeRemove' fires when the screen is actually popped from the stack
  // (back button / router.back()). It does NOT fire when the app goes to
  // background or the user opens Android recents — so derivation keeps running.
  useEffect(() => {
    const unsub = navigation.addListener('beforeRemove', () => {
      abortRef.current?.abort();
      abortRef.current = null;
      setCKey(null);
      setSearched(false);
      setMatch(null);
      setSuggestions([]);
      setSearchError('');
      cleanupTempVault().catch(() => {});
    });
    return unsub;
  }, [navigation]);

  // Fallback cleanup on full unmount
  useEffect(() => {
    return () => {
      abortRef.current?.abort();
      cleanupTempVault().catch(() => {});
    };
  }, []);

  const isUnlocked = cKey !== null;

  // ── File picker ─────────────────────────────────────────────────────────────
  const handlePickFile = async () => {
    try {
      const result = await DocumentPicker.getDocumentAsync({ copyToCacheDirectory: true });
      if (result.canceled) return;
      const file = result.assets[0];
      if (!file.name.toLowerCase().endsWith('.encrypted')) {
        Alert.alert('Wrong file type', 'Select a .encrypted PassGuard vault file.');
        return;
      }
      setVaultPath(file.uri);
      setVaultName(file.name);
      if (isUnlocked) handleLock();
    } catch {
      Alert.alert('Error', 'Could not open the file picker.');
    }
  };

  // ── Lock vault ───────────────────────────────────────────────────────────────
  const handleLock = useCallback(() => {
    abortRef.current?.abort();
    abortRef.current = null;
    setCKey(null);
    setSearched(false);
    setMatch(null);
    setSuggestions([]);
    setSearchError('');
    cleanupTempVault().catch(() => {});
  }, []);

  // ── Search ───────────────────────────────────────────────────────────────────
  const handleSearch = async () => {
    Keyboard.dismiss();

    if (!vaultPath) {
      Alert.alert('No vault selected', 'Please select a .encrypted vault file first.');
      return;
    }
    if (!query.trim()) {
      Alert.alert('Empty search', 'Enter a name to search for.');
      return;
    }

    // Cancel any previous derivation
    abortRef.current?.abort();
    const controller = new AbortController();
    abortRef.current = controller;

    setLoading(true);
    setProgress(0);
    setSearched(false);
    setMatch(null);
    setSuggestions([]);
    setSearchError('');

    let activeKey = cKey;

    try {
      // Step 1: open vault (only if not already unlocked)
      if (!activeKey) {
        if (!email.trim() || !masterPassword || !securityKey.trim()) {
          Alert.alert('Missing credentials', 'Fill in Email, Master Password, and Security Key.');
          setLoading(false);
          return;
        }
        await cleanupTempVault();
        const credentials: VaultCredentials = {
          email: email.trim(),
          masterPassword,
          securityKey: securityKey.trim(),
        };
        const { cKey: derived } = await openVault(
          vaultPath,
          credentials,
          controller.signal,
          (pct) => setProgress(pct),
        );
        activeKey = derived;
        setCKey(derived);
      }

      // Step 2: search
      const result = await searchVault(query.trim(), activeKey);
      setMatch(result.match);
      setSuggestions(result.suggestions);
      if (!result.match && result.suggestions.length === 0) {
        setSearchError(`No entries found containing "${query.trim()}".`);
      }
      setSearched(true);

      setTimeout(() => scrollRef.current?.scrollToEnd({ animated: true }), 500);

    } catch (e: any) {
      // AbortError = user pressed back → silently discard
      if (e?.name === 'AbortError') return;

      setSearchError(String(e));
      setSearched(true);
      if (!cKey) setCKey(null);
    } finally {
      setLoading(false);
      setProgress(0);
    }
  };

  // ── Cancel in-progress derivation ────────────────────────────────────────────
  const handleCancel = useCallback(() => {
    abortRef.current?.abort();
    abortRef.current = null;
    setLoading(false);
    setProgress(0);
  }, []);

  // ── Scroll so Name field + button are visible above the keyboard ──────────────
  // • No results yet (including during loading): scrollToEnd → button at bottom edge
  // • Results already showing: scrollTo section → section + button visible, results below
  const handleSearchFocus = () => {
    setTimeout(() => {
      if (!searched) {
        scrollRef.current?.scrollToEnd({ animated: true });
      } else {
        scrollRef.current?.scrollTo({
          y: Math.max(0, searchSectionY.current - 16),
          animated: true,
        });
      }
    }, 350);
  };

  // ── Render ───────────────────────────────────────────────────────────────────
  return (
    <SafeAreaView style={styles.safe} edges={['top', 'bottom']}>
      <ScreenHeader title="Vault Search" />

      <KeyboardAvoidingView style={styles.kav} behavior="padding">
        <ScrollView
          ref={scrollRef}
          contentContainerStyle={styles.scroll}
          showsVerticalScrollIndicator={false}
          keyboardShouldPersistTaps="handled"
          keyboardDismissMode="on-drag"
        >
          {/* Warning */}
          <View style={styles.warningBanner}>
            <Text style={styles.warningText}>
              ⚠  Credentials are never stored. You must enter them each time.
            </Text>
          </View>

          {/* Vault file */}
          <View style={styles.section}>
            <Text style={styles.sectionTitle}>VAULT FILE</Text>
            <CyberpunkButton
              label={vaultName || 'Select .encrypted file'}
              onPress={handlePickFile}
              variant="outline"
            />
          </View>

          {/* Credentials (only when vault is locked) */}
          {!isUnlocked && (
            <View style={styles.section}>
              <Text style={styles.sectionTitle}>CREDENTIALS</Text>
              <CyberpunkInput
                label="Email"
                placeholder="yourname@example.com"
                value={email}
                onChangeText={setEmail}
                keyboardType="email-address"
                clearable
                onClear={() => setEmail('')}
              />
              <CyberpunkInput
                label="Master Password"
                placeholder="••••••••••••••••"
                value={masterPassword}
                onChangeText={setMasterPassword}
                isPassword
                clearable
                onClear={() => setMasterPassword('')}
              />
              <CyberpunkInput
                label="Security Key"
                placeholder="Base64-encoded security key"
                value={securityKey}
                onChangeText={setSecurityKey}
                clearable
                onClear={() => setSecurityKey('')}
              />
            </View>
          )}

          {/* Vault unlocked banner */}
          {isUnlocked && (
            <Pressable style={styles.unlockedBanner} onPress={handleLock}>
              <Text style={styles.unlockedText}>🔓  VAULT UNLOCKED</Text>
              <Text style={styles.lockHint}>Tap to lock and re-enter credentials</Text>
            </Pressable>
          )}

          {/* Search field */}
          <View
            style={styles.section}
            onLayout={(e) => { searchSectionY.current = e.nativeEvent.layout.y; }}
          >
            <Text style={styles.sectionTitle}>SEARCH</Text>
            <CyberpunkInput
              label="Name"
              placeholder='e.g. "Gmail", "Netflix"'
              value={query}
              onChangeText={(t) => {
                setQuery(t);
                if (searched) {
                  setSearched(false); setMatch(null);
                  setSuggestions([]); setSearchError('');
                }
              }}
              onFocus={handleSearchFocus}
              returnKeyType="search"
              onSubmitEditing={handleSearch}
              clearable
              onClear={() => {
                setQuery(''); setSearched(false);
                setMatch(null); setSuggestions([]); setSearchError('');
              }}
            />
          </View>

          {/* Loading indicator with progress */}
          {loading && (
            <Text style={styles.loadingNote}>
              {progress < 100
                ? `Deriving keys… ${progress.toFixed(0)}%`
                : 'Searching vault…'}
            </Text>
          )}

          {/* SEARCH / CANCELAR button */}
          <CyberpunkButton
            label={loading ? 'CANCELAR' : 'SEARCH'}
            onPress={loading ? handleCancel : handleSearch}
            variant={loading ? 'outline' : 'cyan'}
            style={styles.searchBtn}
          />

          {/* Results */}
          {searched && !loading && (
            <View style={styles.resultsSection}>
              {searchError ? (
                <View style={[styles.section, styles.errorSection]}>
                  <Text style={styles.noMatchTitle}>✗ {searchError}</Text>
                </View>
              ) : match ? (
                <View style={[styles.section, styles.matchSection]}>
                  <Text style={styles.matchTitle}>✓ MATCH: {match.name}</Text>
                  <CredentialRow label="Username" value={match.username} />
                  <CredentialRow label="Password" value={match.sitePassword} isPassword />
                </View>
              ) : (
                <View style={styles.section}>
                  <Text style={styles.noMatchTitle}>
                    ✗ No exact match for "{query}".
                  </Text>
                  {suggestions.length > 0 ? (
                    <>
                      <Text style={styles.suggestLabel}>Did you mean one of these?</Text>
                      {suggestions.map((s) => (
                        <Text
                          key={s}
                          style={styles.suggestion}
                          onPress={() => {
                            setQuery(s);
                            setSearched(false); setMatch(null);
                            setSuggestions([]); setSearchError('');
                          }}
                        >
                          › {s}
                        </Text>
                      ))}
                    </>
                  ) : (
                    <Text style={styles.noSuggestText}>
                      No entries found containing "{query}".
                    </Text>
                  )}
                </View>
              )}
            </View>
          )}
        </ScrollView>
      </KeyboardAvoidingView>
    </SafeAreaView>
  );
}

// ── Styles ─────────────────────────────────────────────────────────────────────

const styles = StyleSheet.create({
  safe:   { flex: 1, backgroundColor: Colors.background },
  kav:    { flex: 1 },
  scroll: { padding: 20, paddingBottom: 40 },

  warningBanner: {
    backgroundColor: Colors.dangerDim + '33',
    borderWidth: 1, borderColor: Colors.danger + '60',
    borderRadius: 2, padding: 12, marginBottom: 20,
  },
  warningText: { color: Colors.danger, fontSize: 12, lineHeight: 17 },

  section: {
    backgroundColor: Colors.surface, borderRadius: 2,
    padding: 16, marginBottom: 16,
    borderWidth: 1, borderColor: Colors.border,
  },
  sectionTitle: {
    color: Colors.textSecondary, fontSize: 11,
    fontWeight: '800', letterSpacing: 2, marginBottom: 14,
  },

  unlockedBanner: {
    backgroundColor: Colors.success + '15',
    borderWidth: 1, borderColor: Colors.success + '60',
    borderRadius: 2, padding: 14, marginBottom: 16, alignItems: 'center',
  },
  unlockedText: { color: Colors.success, fontSize: 13, fontWeight: '800', letterSpacing: 1 },
  lockHint:     { color: Colors.successDim, fontSize: 11, marginTop: 4 },

  loadingNote: {
    color: Colors.textSecondary, fontSize: 12,
    textAlign: 'center', fontStyle: 'italic', marginBottom: 12,
  },

  searchBtn:     { marginBottom: 0 },
  resultsSection: { marginTop: 20 },

  matchSection: { borderColor: Colors.cyan + '80' },
  matchTitle: {
    color: Colors.cyan, fontSize: 13, fontWeight: '800',
    letterSpacing: 1, marginBottom: 16,
  },

  errorSection: { borderColor: Colors.danger + '60' },
  noMatchTitle: { color: Colors.danger, fontSize: 13, fontWeight: '700', marginBottom: 12 },

  suggestLabel:  { color: Colors.textSecondary, fontSize: 12, marginBottom: 10 },
  suggestion: {
    color: Colors.primary, fontSize: 15, fontWeight: '600',
    paddingVertical: 8,
    borderBottomWidth: 1, borderBottomColor: Colors.border,
  },
  noSuggestText: { color: Colors.textDisabled, fontSize: 13 },
});
