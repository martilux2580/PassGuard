/**
 * ScreenHeader.tsx – Reusable header bar with back arrow and title.
 */

import React from 'react';
import { View, Text, Pressable, StyleSheet } from 'react-native';
import { useRouter } from 'expo-router';
import { Colors } from '../constants/colors';

interface ScreenHeaderProps {
  title: string;
}

const ScreenHeader: React.FC<ScreenHeaderProps> = ({ title }) => {
  const router = useRouter();

  return (
    <View style={styles.header}>
      <Pressable
        onPress={() => router.back()}
        style={styles.backButton}
        accessibilityLabel="Go back"
      >
        <Text style={styles.backArrow}>{'‹'}</Text>
      </Pressable>
      <Text style={styles.title}>{title.toUpperCase()}</Text>
      <View style={styles.spacer} />
    </View>
  );
};

const styles = StyleSheet.create({
  header: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 16,
    paddingVertical: 14,
    borderBottomWidth: 1,
    borderBottomColor: Colors.border,
    backgroundColor: Colors.surface,
  },
  backButton: {
    padding: 4,
    minWidth: 36,
  },
  backArrow: {
    color: Colors.primary,
    fontSize: 32,
    lineHeight: 32,
    fontWeight: '300',
  },
  title: {
    flex: 1,
    textAlign: 'center',
    color: Colors.primary,
    fontFamily: 'CampOpti-Caps',
    fontSize: 17,
    letterSpacing: 2,
  },
  spacer: {
    minWidth: 36,
  },
});

export default ScreenHeader;
