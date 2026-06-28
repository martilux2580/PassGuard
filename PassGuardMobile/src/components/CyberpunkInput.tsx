/**
 * CyberpunkInput.tsx
 *
 * A styled TextInput with a Cyberpunk 2077 look:
 *   • Dark surface background with a coloured bottom border
 *   • Accent-coloured border when focused
 *   • Optional label above
 *   • Optional password toggle (eye icon) for secure fields
 *   • Error state with red border
 */

import React, { useState } from 'react';
import {
  View,
  TextInput,
  Text,
  Pressable,
  StyleSheet,
  TextInputProps,
} from 'react-native';
import { Colors } from '../constants/colors';

interface CyberpunkInputProps extends TextInputProps {
  label?: string;
  error?: string;
  isPassword?: boolean;
  clearable?: boolean;
  onClear?: () => void;
}

const CyberpunkInput: React.FC<CyberpunkInputProps> = ({
  label,
  error,
  isPassword = false,
  clearable = false,
  onClear,
  style,
  onFocus: externalOnFocus,
  onBlur: externalOnBlur,
  ...rest
}) => {
  const [focused, setFocused] = useState(false);
  const [hidden, setHidden] = useState(isPassword);

  const hasValue = typeof rest.value === 'string' && rest.value.length > 0;

  const borderColor = error
    ? Colors.danger
    : focused
    ? Colors.primary
    : Colors.border;

  return (
    <View style={styles.wrapper}>
      {label ? <Text style={styles.label}>{label}</Text> : null}

      <View style={[styles.inputRow, { borderColor }]}>
        <TextInput
          style={[styles.input, style]}
          placeholderTextColor={Colors.textDisabled}
          selectionColor={Colors.primary}
          secureTextEntry={hidden}
          onFocus={(e) => { setFocused(true); externalOnFocus?.(e); }}
          onBlur={(e) => { setFocused(false); externalOnBlur?.(e); }}
          autoCapitalize="none"
          autoCorrect={false}
          {...rest}
        />

        {/* Clear button */}
        {clearable && hasValue ? (
          <Pressable
            onPress={onClear}
            style={styles.iconButton}
            accessibilityLabel="Clear field"
            hitSlop={6}
          >
            <Text style={styles.clearText}>✕</Text>
          </Pressable>
        ) : null}

        {/* Password visibility toggle */}
        {isPassword ? (
          <Pressable
            onPress={() => setHidden((h) => !h)}
            style={styles.iconButton}
            accessibilityLabel={hidden ? 'Show password' : 'Hide password'}
          >
            <Text style={styles.eyeText}>{hidden ? '👁' : '🙈'}</Text>
          </Pressable>
        ) : null}
      </View>

      {error ? <Text style={styles.errorText}>{error}</Text> : null}
    </View>
  );
};

const styles = StyleSheet.create({
  wrapper: {
    marginBottom: 16,
  },
  label: {
    color: Colors.textSecondary,
    fontSize: 12,
    fontWeight: '700',
    letterSpacing: 1,
    marginBottom: 6,
    textTransform: 'uppercase',
  },
  inputRow: {
    flexDirection: 'row',
    alignItems: 'center',
    backgroundColor: Colors.surfaceAlt,
    borderWidth: 1,
    borderRadius: 2,
    paddingHorizontal: 12,
  },
  input: {
    flex: 1,
    color: Colors.textPrimary,
    fontSize: 15,
    paddingVertical: 12,
    fontFamily: 'monospace',
  },
  iconButton: {
    padding: 6,
  },
  clearText: {
    fontSize: 13,
    color: Colors.textSecondary,
    fontWeight: '700',
  },
  eyeText: {
    fontSize: 18,
  },
  errorText: {
    color: Colors.danger,
    fontSize: 11,
    marginTop: 4,
    letterSpacing: 0.5,
  },
});

export default CyberpunkInput;
