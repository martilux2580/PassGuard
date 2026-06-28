/**
 * index.tsx  –  Home Screen
 */

import React, { useState, useCallback } from 'react';
import {
  View,
  Text,
  Pressable,
  StyleSheet,
  ScrollView,
  Image,
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';
import { useRouter, useFocusEffect } from 'expo-router';
import { Colors } from '../src/constants/colors';
import { getTempEntryCount } from '../src/services/temp-storage/tempStorageService';

interface FeatureCard {
  id: string;
  route: string;
  title: string;
  subtitle: string;
  accentColor: string;
  icon: string;
}

const FEATURES: FeatureCard[] = [
  {
    id: 'generator',
    route: '/generator',
    title: 'Password Generator',
    subtitle: 'Create strong, breach-free passwords with custom settings.',
    accentColor: Colors.primary,
    icon: '⚡',
  },
  {
    id: 'vault',
    route: '/vault-credentials',
    title: 'Vault Search',
    subtitle: 'Decrypt your PassGuard vault and retrieve any credential.',
    accentColor: Colors.cyan,
    icon: '🔒',
  },
  {
    id: 'temp',
    route: '/temp-storage',
    title: 'Temporary Storage',
    subtitle: 'Securely store new credentials until you sync your vault.',
    accentColor: Colors.pink,
    icon: '📋',
  },
];

// ── Temp badge helpers ─────────────────────────────────────────────────────────

function getTempBadgeText(count: number | null | undefined): string {
  if (count === undefined) return ' ';
  if (count === null) return '+ Tap to set up your temporary vault';
  if (count === 0) return '✓ No entries pending sync';
  return `${count} ${count === 1 ? 'entry' : 'entries'} pending sync to Desktop Vault`;
}

function getTempBadgeColor(count: number | null | undefined): string {
  if (count === undefined) return 'transparent';
  if (count === null || count === 0) return Colors.success;
  return Colors.pink;
}

// ── Screen ─────────────────────────────────────────────────────────────────────

export default function HomeScreen() {
  const router = useRouter();
  const [tempCount, setTempCount] = useState<number | null | undefined>(undefined);

  useFocusEffect(
    useCallback(() => {
      getTempEntryCount().then(setTempCount).catch(() => setTempCount(null));
    }, []),
  );

  return (
    <SafeAreaView style={styles.safe} edges={['top', 'bottom']}>
      <ScrollView
        contentContainerStyle={styles.scroll}
        showsVerticalScrollIndicator={false}
      >
        <View>
          <View style={styles.logoSection}>
            <Image
              source={require('../assets/images/logo.png')}
              style={styles.logo}
              resizeMode="contain"
            />
            <Text
              style={styles.appName}
              numberOfLines={1}
              adjustsFontSizeToFit
            >
              PASSGUARD
            </Text>
            <Text style={styles.tagline}>Your offline password manager — mobile.</Text>
          </View>

          <View style={styles.divider} />

          {FEATURES.map((feature) => {
            const isTemp = feature.id === 'temp';
            const badgeText = isTemp ? getTempBadgeText(tempCount) : null;
            const badgeColor = isTemp ? getTempBadgeColor(tempCount) : 'transparent';

            return (
              <View key={feature.id}>
                <Pressable
                  style={({ pressed }) => [
                    styles.card,
                    { borderLeftColor: feature.accentColor },
                    pressed && styles.cardPressed,
                  ]}
                  onPress={() => router.push(feature.route as any)}
                  accessibilityRole="button"
                  accessibilityLabel={feature.title}
                >
                  {/* Inner row: icon + text + arrow, vertically centered */}
                  <View style={styles.cardRow}>
                    <Text style={styles.cardIcon}>{feature.icon}</Text>
                    <View style={styles.cardText}>
                      <Text style={[styles.cardTitle, { color: feature.accentColor }]}>
                        {feature.title.toUpperCase()}
                      </Text>
                      <Text style={styles.cardSubtitle}>{feature.subtitle}</Text>
                    </View>
                    <Text style={[styles.cardArrow, { color: feature.accentColor }]}>
                      ›
                    </Text>
                  </View>
                  {/* Badge only on temp card — non-temp cards don't render it */}
                  {badgeText !== null && (
                    <Text style={[styles.cardBadge, { color: badgeColor }]}>
                      {badgeText}
                    </Text>
                  )}
                </Pressable>
              </View>
            );
          })}
        </View>

        <Text style={styles.footer}>PassGuard Mobile v1.0.0</Text>
      </ScrollView>
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  safe: {
    flex: 1,
    backgroundColor: Colors.background,
  },
  scroll: {
    flexGrow: 1,
    justifyContent: 'space-between',
    paddingHorizontal: 20,
    paddingTop: 16,
    paddingBottom: 24,
  },
  logoSection: {
    alignItems: 'center',
    marginBottom: 32,
    marginTop: 8,
  },
  logo: {
    width: 110,
    height: 110,
  },
  appName: {
    fontFamily: 'CampOpti-Caps',
    color: Colors.primary,
    fontSize: 26,
    letterSpacing: 2,
    marginTop: 14,
    alignSelf: 'stretch',
    textAlign: 'center',
  },
  tagline: {
    color: Colors.textSecondary,
    fontSize: 13,
    marginTop: 6,
    letterSpacing: 0.5,
  },
  divider: {
    height: 1,
    backgroundColor: Colors.border,
    marginBottom: 28,
  },
  card: {
    flexDirection: 'column',
    justifyContent: 'center',
    minHeight: 112,
    backgroundColor: Colors.surface,
    borderRadius: 2,
    borderLeftWidth: 4,
    paddingHorizontal: 18,
    paddingVertical: 18,
    marginBottom: 14,
    shadowColor: Colors.primary,
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.08,
    shadowRadius: 6,
    elevation: 3,
  },
  cardPressed: {
    opacity: 0.75,
  },
  cardRow: {
    flexDirection: 'row',
    alignItems: 'center',
    gap: 16,
  },
  cardIcon: {
    fontSize: 28,
    marginTop: -2,
  },
  cardText: {
    flex: 1,
  },
  cardTitle: {
    fontWeight: '800',
    fontSize: 13,
    letterSpacing: 1,
    marginBottom: 4,
  },
  cardSubtitle: {
    color: Colors.textSecondary,
    fontSize: 12,
    lineHeight: 17,
  },
  cardBadge: {
    fontSize: 11,
    fontWeight: '700',
    letterSpacing: 0.3,
    marginTop: 8,
    marginLeft: 44,
  },
  cardArrow: {
    fontSize: 28,
    fontWeight: '300',
    lineHeight: 28,
    marginTop: -2,
  },
  footer: {
    textAlign: 'center',
    color: Colors.textDisabled,
    fontSize: 11,
    marginTop: 24,
    letterSpacing: 0.5,
  },
});
