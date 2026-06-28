/**
 * temp-storage.tsx  –  Temporary Storage Screen
 *
 * Workflow:
 *   1. On mount, trigger biometric / PIN authentication.
 *   2. If authentication fails, show a locked state with a retry button.
 *   3. On success, load and display stored entries.
 *   4. The user can add new entries or delete existing ones.
 *
 * Each entry shows: Name, Username, Password (hidden by default with toggle),
 * saved date, and a Delete button.
 *
 * Passwords are encrypted at rest via tempStorageService (AES + Android Keystore).
 */

import React, { useState, useCallback, useEffect } from 'react';
import {
  View,
  Text,
  StyleSheet,
  FlatList,
  Alert,
  Pressable,
  Modal,
  KeyboardAvoidingView,
  Platform,
  ScrollView,
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';
import { Colors } from '../src/constants/colors';
import ScreenHeader from '../src/components/ScreenHeader';
import CyberpunkButton from '../src/components/CyberpunkButton';
import CyberpunkInput from '../src/components/CyberpunkInput';
import CopyButton from '../src/components/CopyButton';
import { authenticate, isBiometricAvailable } from '../src/services/temp-storage/biometricService';
import {
  getAllTempEntries,
  saveTempEntry,
  deleteTempEntry,
  TempEntry,
} from '../src/services/temp-storage/tempStorageService';

// ── Entry card component ──────────────────────────────────────────────────────

interface EntryCardProps {
  entry: TempEntry;
  onDelete: (id: number) => void;
}

const EntryCard: React.FC<EntryCardProps> = ({ entry, onDelete }) => {
  const dateLabel = new Date(entry.savedAt).toLocaleDateString('en-US', {
    year: 'numeric',
    month: 'short',
    day: 'numeric',
    hour: '2-digit',
    minute: '2-digit',
  });

  const confirmDelete = () => {
    Alert.alert(
      'Delete Entry',
      `Delete "${entry.name}"? This cannot be undone.`,
      [
        { text: 'Cancel', style: 'cancel' },
        {
          text: 'Delete',
          style: 'destructive',
          onPress: () => onDelete(entry.id),
        },
      ],
    );
  };

  return (
    <View style={cardStyles.card}>
      {/* Name — single line with ellipsis */}
      <Text style={cardStyles.name} numberOfLines={1}>{entry.name}</Text>

      {/* Username */}
      <View style={cardStyles.fieldRow}>
        <Text style={cardStyles.fieldLabel}>USERNAME</Text>
        <Text style={cardStyles.fieldValue} numberOfLines={1}>{entry.username}</Text>
        <CopyButton value={entry.username} label="COPY" style={cardStyles.btn} />
      </View>

      {/* Password — always dots, no eye toggle */}
      <View style={cardStyles.fieldRow}>
        <Text style={cardStyles.fieldLabel}>PASSWORD</Text>
        <Text style={cardStyles.fieldValue} numberOfLines={1}>
          {'•'.repeat(Math.min(16, entry.password.length))}
        </Text>
        <CopyButton value={entry.password} label="COPY" style={cardStyles.btn} />
      </View>

      {/* Footer: date (left) + delete (right) */}
      <View style={cardStyles.footerRow}>
        <Text style={cardStyles.date}>{dateLabel}</Text>
        <Pressable onPress={confirmDelete} style={cardStyles.deleteBtn}>
          <Text style={cardStyles.deleteText}>DELETE</Text>
        </Pressable>
      </View>
    </View>
  );
};

const cardStyles = StyleSheet.create({
  card: {
    backgroundColor: Colors.surface,
    borderRadius: 2,
    borderWidth: 1,
    borderColor: Colors.border,
    borderLeftWidth: 3,
    borderLeftColor: Colors.pink,
    padding: 14,
    marginBottom: 12,
  },
  name: {
    color: Colors.pink,
    fontWeight: '800',
    fontSize: 15,
    letterSpacing: 0.5,
    marginBottom: 10,
  },
  footerRow: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    marginTop: 6,
  },
  date: {
    color: Colors.textDisabled,
    fontSize: 10,
  },
  fieldRow: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 8,
    marginBottom: 8,
  },
  fieldLabel: {
    color: Colors.textSecondary,
    fontSize: 10,
    fontWeight: '700',
    letterSpacing: 0.8,
    minWidth: 80,
  },
  fieldValue: {
    flex: 1,
    color: Colors.textPrimary,
    fontSize: 13,
    fontFamily: 'monospace',
  },
  btn: {
    width: 110,
    alignItems: 'center',
  },
  deleteBtn: {
    width: 110,
    alignItems: 'center',
    paddingVertical: 8,
    paddingHorizontal: 14,
    borderWidth: 1,
    borderColor: Colors.danger,
    borderRadius: 2,
    backgroundColor: Colors.surfaceAlt,
  },
  deleteText: {
    color: Colors.danger,
    fontSize: 11,
    fontWeight: '800',
    letterSpacing: 1.5,
  },
});

// ── Add entry modal ────────────────────────────────────────────────────────────

interface AddModalProps {
  visible: boolean;
  onClose: () => void;
  onSave: (name: string, username: string, password: string) => Promise<void>;
}

const AddModal: React.FC<AddModalProps> = ({ visible, onClose, onSave }) => {
  const [name, setName] = useState('');
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [saving, setSaving] = useState(false);

  const reset = () => {
    setName('');
    setUsername('');
    setPassword('');
  };

  const handleSave = async () => {
    if (!name.trim() || !username.trim() || !password) {
      Alert.alert('Missing fields', 'All fields are required.');
      return;
    }
    setSaving(true);
    try {
      await onSave(name.trim(), username.trim(), password);
      reset();
      onClose();
    } catch (e) {
      Alert.alert('Error', String(e));
    } finally {
      setSaving(false);
    }
  };

  return (
    <Modal visible={visible} animationType="slide" transparent onRequestClose={onClose}>
      <KeyboardAvoidingView
        style={modalStyles.overlay}
        behavior={Platform.OS === 'ios' ? 'padding' : 'height'}
      >
        <View style={modalStyles.sheet}>
          <ScrollView
            keyboardShouldPersistTaps="handled"
            showsVerticalScrollIndicator={false}
            bounces={false}
          >
            <Text style={modalStyles.title}>ADD TEMP ENTRY</Text>
            <CyberpunkInput
              label="Name"
              placeholder='e.g. "Twitter"'
              value={name}
              onChangeText={setName}
            />
            <CyberpunkInput
              label="Username"
              placeholder="yourname@example.com"
              value={username}
              onChangeText={setUsername}
              keyboardType="email-address"
            />
            <CyberpunkInput
              label="Password"
              placeholder="••••••••"
              value={password}
              onChangeText={setPassword}
              isPassword
            />
            <View style={modalStyles.actions}>
              <CyberpunkButton
                label="Cancel"
                onPress={() => { reset(); onClose(); }}
                variant="outline"
                style={{ flex: 1 }}
              />
              <CyberpunkButton
                label="Save"
                onPress={handleSave}
                loading={saving}
                style={{ flex: 1 }}
              />
            </View>
          </ScrollView>
        </View>
      </KeyboardAvoidingView>
    </Modal>
  );
};

const modalStyles = StyleSheet.create({
  overlay: {
    flex: 1,
    backgroundColor: '#000000CC',
    justifyContent: 'flex-end',
  },
  sheet: {
    backgroundColor: Colors.surface,
    borderTopWidth: 2,
    borderTopColor: Colors.pink,
    borderRadius: 2,
    padding: 24,
    paddingBottom: 36,
  },
  title: {
    color: Colors.pink,
    fontWeight: '800',
    fontSize: 14,
    letterSpacing: 2,
    marginBottom: 20,
  },
  actions: {
    flexDirection: 'row',
    gap: 12,
    marginTop: 8,
  },
});

// ── Main screen ───────────────────────────────────────────────────────────────

type AuthState = 'idle' | 'authenticating' | 'authenticated' | 'denied';

export default function TempStorageScreen() {
  const [authState, setAuthState] = useState<AuthState>('idle');
  const [entries, setEntries] = useState<TempEntry[]>([]);
  const [loadingEntries, setLoadingEntries] = useState(false);
  const [showAddModal, setShowAddModal] = useState(false);

  // ── Biometric auth on mount ───────────────────────────────────────────────
  const requestAuth = useCallback(async () => {
    setAuthState('authenticating');
    const available = await isBiometricAvailable();
    if (!available) {
      // No biometric enrolled → warn but grant access (device has no lock set)
      Alert.alert(
        'No Biometric / PIN Found',
        'Your device has no biometric or PIN set up. Access is granted, but setting up a screen lock is strongly recommended for security.',
      );
      setAuthState('authenticated');
      return;
    }
    const ok = await authenticate('Authenticate to access Temporary Storage');
    setAuthState(ok ? 'authenticated' : 'denied');
  }, []);

  useEffect(() => {
    requestAuth();
  }, [requestAuth]);

  // ── Load entries once authenticated ──────────────────────────────────────
  useEffect(() => {
    if (authState !== 'authenticated') return;
    setLoadingEntries(true);
    getAllTempEntries()
      .then(setEntries)
      .catch((e) => Alert.alert('Error loading entries', String(e)))
      .finally(() => setLoadingEntries(false));
  }, [authState]);

  // ── Delete entry ──────────────────────────────────────────────────────────
  const handleDelete = async (id: number) => {
    await deleteTempEntry(id);
    setEntries((prev) => prev.filter((e) => e.id !== id));
  };

  // ── Save new entry ────────────────────────────────────────────────────────
  const handleSave = async (name: string, username: string, password: string) => {
    await saveTempEntry(name, username, password);
    const updated = await getAllTempEntries();
    setEntries(updated);
  };

  // ── Disclaimer ────────────────────────────────────────────────────────────
  const showDisclaimer = () => {
    Alert.alert(
      'About Temporary Storage',
      [
        '⏱  Designed for short-term use (1–2 days max). Transfer credentials to your PassGuard Desktop vault as soon as possible.',
        '',
        '🔐  Encryption: fields are encrypted with AES-256-CBC using a key stored in the Android Keystore, protected by your biometric or PIN. The Desktop vault uses PBKDF2 with 650,060 iterations — a significantly stronger barrier against brute-force attacks.',
        '',
        '⚠️  If you uninstall the app or clear its data, all entries here are permanently lost with no recovery option.',
      ].join('\n'),
      [{ text: 'Got it' }],
    );
  };

  // ── Render: locked ────────────────────────────────────────────────────────
  if (authState === 'idle' || authState === 'authenticating') {
    return (
      <SafeAreaView style={styles.safe} edges={['top', 'bottom']}>
        <ScreenHeader title="Temporary Storage" />
        <View style={styles.centred}>
          <Text style={styles.lockIcon}>🔒</Text>
          <Text style={styles.lockText}>Authenticating…</Text>
        </View>
      </SafeAreaView>
    );
  }

  if (authState === 'denied') {
    return (
      <SafeAreaView style={styles.safe} edges={['top', 'bottom']}>
        <ScreenHeader title="Temporary Storage" />
        <View style={styles.centred}>
          <Text style={styles.lockIcon}>🔒</Text>
          <Text style={styles.lockText}>Authentication failed.</Text>
          <CyberpunkButton
            label="Retry"
            onPress={requestAuth}
            style={{ marginTop: 20 }}
          />
        </View>
      </SafeAreaView>
    );
  }

  // ── Render: authenticated ─────────────────────────────────────────────────
  return (
    <SafeAreaView style={styles.safe} edges={['top', 'bottom']}>
      <ScreenHeader title="Temporary Storage" />

      {/* Top bar: entry count + warning icon */}
      <View style={styles.topBar}>
        <Text style={styles.entryCount}>
          {entries.length} {entries.length === 1 ? 'entry' : 'entries'}
        </Text>
        <Pressable onPress={showDisclaimer} style={styles.readmeBtn} accessibilityLabel="About Temporary Storage">
          <Text style={styles.readmeBtnText}>README</Text>
        </Pressable>
      </View>

      {/* Entries list — flex: 1 so it fills space between topBar and footer */}
      {entries.length === 0 && !loadingEntries ? (
        <View style={styles.empty}>
          <Text style={styles.emptyText}>
            No temporary entries saved yet.{'\n'}Tap "+ Add Entry" below to save a credential.
          </Text>
        </View>
      ) : (
        <FlatList
          data={entries}
          keyExtractor={(item) => String(item.id)}
          contentContainerStyle={styles.list}
          renderItem={({ item }) => (
            <View>
              <EntryCard entry={item} onDelete={handleDelete} />
            </View>
          )}
          showsVerticalScrollIndicator={false}
        />
      )}

      {/* Footer: Add Entry button pinned at the bottom */}
      <View style={styles.footer}>
        <CyberpunkButton
          label="+ Add Entry"
          onPress={() => setShowAddModal(true)}
          style={styles.addBtn}
        />
      </View>

      {/* Add entry modal */}
      <AddModal
        visible={showAddModal}
        onClose={() => setShowAddModal(false)}
        onSave={handleSave}
      />
    </SafeAreaView>
  );
}

// ── Styles ─────────────────────────────────────────────────────────────────────

const styles = StyleSheet.create({
  safe: { flex: 1, backgroundColor: Colors.background },
  centred: { flex: 1, alignItems: 'center', justifyContent: 'center', padding: 24 },
  lockIcon: { fontSize: 56, marginBottom: 16 },
  lockText: { color: Colors.textSecondary, fontSize: 16, textAlign: 'center' },
  topBar: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'space-between',
    paddingLeft: 20,
    paddingRight: 30,   // list(16) + card(14) = 30 → aligns with COPY/DELETE right edge
    paddingVertical: 12,
    borderBottomWidth: 1,
    borderBottomColor: Colors.border,
  },
  entryCount: {
    color: Colors.pink,
    fontSize: 15,
    fontWeight: '700',
    letterSpacing: 0.5,
  },
  readmeBtn: {
    width: 110,
    alignItems: 'center',
    paddingVertical: 8,
    paddingHorizontal: 14,
    borderWidth: 1,
    borderColor: Colors.primary,
    borderRadius: 2,
    backgroundColor: Colors.surfaceAlt,
  },
  readmeBtnText: {
    color: Colors.primary,
    fontSize: 11,
    fontWeight: '800',
    letterSpacing: 1.5,
  },
  list: { padding: 16, paddingBottom: 8 },
  empty: {
    flex: 1,
    alignItems: 'center',
    justifyContent: 'center',
    padding: 32,
  },
  emptyText: {
    color: Colors.textSecondary,
    fontSize: 14,
    textAlign: 'center',
    lineHeight: 22,
  },
  footer: {
    paddingHorizontal: 20,
    paddingVertical: 14,
    borderTopWidth: 1,
    borderTopColor: Colors.border,
  },
  addBtn: { width: '100%' },
});
