/**
 * vault-search.tsx  –  Vault Search Screen
 *
 * Step 2 of the Vault Search flow.
 * Receives the serialised cKey via router params (cKeyB64).
 *
 * The user enters a Name and taps Search.
 * Results:
 *   • Exact match  → shows Username + Password, each with a Copy button.
 *   • No match     → shows up to 3 fuzzy suggestions.
 *   • Error        → shows an error message.
 *
 * Navigating back cleans up the temp vault file.
 */

import React, { useState, useEffect } from 'react';
import {
  View,
  Text,
  StyleSheet,
  ScrollView,
  SafeAreaView,
  Alert,
} from 'react-native';
import { useLocalSearchParams, useRouter } from 'expo-router';
import { Buffer } from 'buffer';
import { Colors } from '../src/constants/colors';
import ScreenHeader from '../src/components/ScreenHeader';
import CyberpunkInput from '../src/components/CyberpunkInput';
import CyberpunkButton from '../src/components/CyberpunkButton';
import CopyButton from '../src/components/CopyButton';
import { searchVault, VaultEntry } from '../src/services/vault/vaultDbService';
import { cleanupTempVault } from '../src/services/vault/vaultFileService';

// ── Credential row (label + masked/unmasked value + copy button) ──────────────

interface CredentialRowProps {
  label: string;
  value: string;
  isPassword?: boolean;
}

const CredentialRow: React.FC<CredentialRowProps> = ({
  label,
  value,
  isPassword = false,
}) => {
  const [revealed, setRevealed] = useState(false);

  return (
    <View style={rowStyles.wrapper}>
      <Text style={rowStyles.label}>{label}</Text>
      <View style={rowStyles.valueRow}>
        <Text style={rowStyles.value} selectable numberOfLines={1}>
          {isPassword && !revealed
            ? '•'.repeat(Math.min(value.length, 20))
            : value}
        </Text>
        {isPassword && (
          <Text
            style={rowStyles.toggle}
            onPress={() => setRevealed((r) => !r)}
          >
            {revealed ? '🙈' : '👁'}
          </Text>
        )}
        <CopyButton value={value} label={`COPY ${label.toUpperCase()}`} />
      </View>
    </View>
  );
};

const rowStyles = StyleSheet.create({
  wrapper: {
    marginBottom: 16,
  },
  label: {
    color: Colors.textSecondary,
    fontSize: 11,
    fontWeight: '700',
    letterSpacing: 1,
    marginBottom: 6,
    textTransform: 'uppercase',
  },
  valueRow: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 10,
    backgroundColor: Colors.surfaceAlt,
    borderRadius: 2,
    borderWidth: 1,
    borderColor: Colors.border,
    paddingVertical: 10,
    paddingHorizontal: 12,
  },
  value: {
    flex: 1,
    color: Colors.textPrimary,
    fontSize: 15,
    fontFamily: 'monospace',
  },
  toggle: {
    fontSize: 18,
    padding: 2,
  },
});

// ── Main screen ───────────────────────────────────────────────────────────────

export default function VaultSearchScreen() {
  const router = useRouter();
  const { cKeyB64 } = useLocalSearchParams<{ cKeyB64: string }>();

  const [query, setQuery] = useState('');
  const [loading, setLoading] = useState(false);
  const [match, setMatch] = useState<VaultEntry | null>(null);
  const [suggestions, setSuggestions] = useState<string[]>([]);
  const [searched, setSearched] = useState(false);

  // Deserialise cKey from the base64 param
  const cKey: Buffer | null = cKeyB64
    ? Buffer.from(cKeyB64, 'base64')
    : null;

  // Clean up temp vault when leaving this screen
  useEffect(() => {
    return () => {
      cleanupTempVault().catch(() => {});
    };
  }, []);

  if (!cKey) {
    return (
      <SafeAreaView style={styles.safe}>
        <ScreenHeader title="Vault Search" />
        <View style={styles.centred}>
          <Text style={styles.errorText}>
            No vault is open. Please go back and open your vault first.
          </Text>
          <CyberpunkButton
            label="Go Back"
            onPress={() => router.back()}
            style={{ marginTop: 20 }}
          />
        </View>
      </SafeAreaView>
    );
  }

  // ── Search handler ──────────────────────────────────────────────────────
  const handleSearch = async () => {
    const trimmed = query.trim();

    if (!trimmed) {
      Alert.alert('Empty search', 'Please enter a Name to search for.');
      return;
    }

    setLoading(true);
    setMatch(null);
    setSuggestions([]);
    setSearched(false);

    try {
      const result = await searchVault(trimmed, cKey);
      setMatch(result.match);
      setSuggestions(result.suggestions);
      setSearched(true);
    } catch (e) {
      Alert.alert('Search Error', String(e));
    } finally {
      setLoading(false);
    }
  };

  // ── Render ──────────────────────────────────────────────────────────────
  return (
    <SafeAreaView style={styles.safe}>
      <ScreenHeader title="Vault Search" />
      <ScrollView
        contentContainerStyle={styles.scroll}
        showsVerticalScrollIndicator={false}
        keyboardShouldPersistTaps="handled"
      >
        {/* ── Search input ────────────────────────────────────────────────── */}
        <View style={styles.section}>
          <Text style={styles.sectionTitle}>SEARCH BY NAME</Text>
          <CyberpunkInput
            label="Name"
            placeholder='e.g. "Gmail", "Netflix"'
            value={query}
            onChangeText={(t) => {
              setQuery(t);
              if (searched) {
                setSearched(false);
                setMatch(null);
                setSuggestions([]);
              }
            }}
            returnKeyType="search"
            onSubmitEditing={handleSearch}
          />
          <CyberpunkButton
            label="Search"
            onPress={handleSearch}
            variant="cyan"
            loading={loading}
          />
        </View>

        {/* ── Results ─────────────────────────────────────────────────────── */}
        {searched && !loading && (
          <View>
            {match ? (
              /* Exact match found */
              <View style={[styles.section, styles.matchSection]}>
                <Text style={styles.matchTitle}>
                  ✓ MATCH FOUND: {match.name}
                </Text>
                <CredentialRow label="Username" value={match.username} />
                <CredentialRow
                  label="Password"
                  value={match.sitePassword}
                  isPassword
                />
              </View>
            ) : (
              /* No exact match */
              <View style={styles.section}>
                <Text style={styles.noMatchTitle}>✗ No exact match found.</Text>
                {suggestions.length > 0 ? (
                  <>
                    <Text style={styles.suggestionsLabel}>
                      Did you mean one of these?
                    </Text>
                    {suggestions.map((s) => (
                      <Text
                        key={s}
                        style={styles.suggestion}
                        onPress={() => {
                          setQuery(s);
                          setSearched(false);
                        }}
                      >
                        › {s}
                      </Text>
                    ))}
                  </>
                ) : (
                  <Text style={styles.noSuggestionsText}>
                    No similar entries found in the vault.
                  </Text>
                )}
              </View>
            )}
          </View>
        )}
      </ScrollView>
    </SafeAreaView>
  );
}

// ── Styles ─────────────────────────────────────────────────────────────────────

const styles = StyleSheet.create({
  safe: { flex: 1, backgroundColor: Colors.background },
  scroll: { padding: 20, paddingBottom: 40 },
  centred: {
    flex: 1,
    alignItems: 'center',
    justifyContent: 'center',
    padding: 24,
  },
  errorText: {
    color: Colors.danger,
    textAlign: 'center',
    fontSize: 15,
    lineHeight: 22,
  },
  section: {
    backgroundColor: Colors.surface,
    borderRadius: 2,
    padding: 16,
    marginBottom: 16,
    borderWidth: 1,
    borderColor: Colors.border,
  },
  matchSection: {
    borderColor: Colors.cyan + '80',
  },
  sectionTitle: {
    color: Colors.textSecondary,
    fontSize: 11,
    fontWeight: '800',
    letterSpacing: 2,
    marginBottom: 14,
  },
  matchTitle: {
    color: Colors.cyan,
    fontSize: 13,
    fontWeight: '800',
    letterSpacing: 1,
    marginBottom: 16,
  },
  noMatchTitle: {
    color: Colors.danger,
    fontSize: 13,
    fontWeight: '700',
    marginBottom: 12,
  },
  suggestionsLabel: {
    color: Colors.textSecondary,
    fontSize: 12,
    marginBottom: 10,
  },
  suggestion: {
    color: Colors.primary,
    fontSize: 15,
    fontWeight: '600',
    paddingVertical: 8,
    borderBottomWidth: 1,
    borderBottomColor: Colors.border,
  },
  noSuggestionsText: {
    color: Colors.textDisabled,
    fontSize: 13,
  },
});
