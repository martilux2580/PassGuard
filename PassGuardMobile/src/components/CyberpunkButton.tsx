/**
 * CyberpunkButton.tsx
 *
 * Styled pressable button matching the Cyberpunk 2077 aesthetic.
 * Uses React Native's built-in Animated API for the press scale effect
 * (avoids react-native-reanimated which requires react-native-worklets in v4).
 */

import React, { useRef } from 'react';
import {
  Pressable,
  Text,
  StyleSheet,
  ViewStyle,
  TextStyle,
  ActivityIndicator,
  Animated,
} from 'react-native';
import { Colors } from '../constants/colors';

interface CyberpunkButtonProps {
  label: string;
  onPress: () => void;
  variant?: 'primary' | 'cyan' | 'danger' | 'outline';
  disabled?: boolean;
  loading?: boolean;
  style?: ViewStyle;
  textStyle?: TextStyle;
  icon?: React.ReactNode;
}

const CyberpunkButton: React.FC<CyberpunkButtonProps> = ({
  label,
  onPress,
  variant = 'primary',
  disabled = false,
  loading = false,
  style,
  textStyle,
  icon,
}) => {
  const scaleAnim = useRef(new Animated.Value(1)).current;

  const handlePressIn = () => {
    Animated.timing(scaleAnim, {
      toValue: 0.95,
      duration: 80,
      useNativeDriver: true,
    }).start();
  };

  const handlePressOut = () => {
    Animated.timing(scaleAnim, {
      toValue: 1,
      duration: 120,
      useNativeDriver: true,
    }).start();
  };

  const bg =
    variant === 'primary'
      ? Colors.primary
      : variant === 'cyan'
      ? Colors.cyan
      : variant === 'danger'
      ? Colors.danger
      : Colors.transparent;

  const borderCol = variant === 'outline' ? Colors.primary : Colors.transparent;

  const labelCol =
    variant === 'primary' || variant === 'cyan' || variant === 'danger'
      ? Colors.background
      : Colors.primary;

  return (
    <Animated.View style={[{ transform: [{ scale: scaleAnim }] }, style]}>
      <Pressable
        onPress={onPress}
        onPressIn={handlePressIn}
        onPressOut={handlePressOut}
        disabled={disabled || loading}
        style={[
          styles.button,
          { backgroundColor: bg, borderColor: borderCol },
          disabled && styles.disabled,
        ]}
        accessibilityRole="button"
        accessibilityLabel={label}
      >
        {loading ? (
          <ActivityIndicator color={labelCol} size="small" />
        ) : (
          <>
            {icon}
            <Text style={[styles.label, { color: labelCol }, textStyle]}>
              {label.toUpperCase()}
            </Text>
          </>
        )}
      </Pressable>
    </Animated.View>
  );
};

const styles = StyleSheet.create({
  button: {
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'center',
    gap: 8,
    paddingVertical: 14,
    paddingHorizontal: 24,
    borderWidth: 2,
    borderRadius: 2,
  },
  label: {
    fontWeight: '800',
    fontSize: 14,
    letterSpacing: 1.5,
  },
  disabled: {
    opacity: 0.4,
  },
});

export default CyberpunkButton;
