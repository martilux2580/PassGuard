/**
 * CopyButton.tsx
 *
 * A small button that copies a value to the clipboard.
 * After copying it briefly shows a "Copied!" confirmation before
 * resetting to its normal state.
 */

import React, { useState } from 'react';
import { Pressable, Text, StyleSheet, ViewStyle } from 'react-native';
import * as Clipboard from 'expo-clipboard';
import { Colors } from '../constants/colors';

interface CopyButtonProps {
  value: string;
  label?: string;
  style?: ViewStyle;
}

const FEEDBACK_DURATION_MS = 1500;

const CopyButton: React.FC<CopyButtonProps> = ({
  value,
  label = 'COPY',
  style,
}) => {
  const [copied, setCopied] = useState(false);

  const handleCopy = async () => {
    await Clipboard.setStringAsync(value);
    setCopied(true);
    setTimeout(() => setCopied(false), FEEDBACK_DURATION_MS);
  };

  return (
    <Pressable
      onPress={handleCopy}
      style={[styles.button, copied && styles.buttonCopied, style]}
      accessibilityLabel={copied ? 'Copied to clipboard' : `Copy ${label}`}
    >
      <Text style={[styles.text, copied && styles.textCopied]}>
        {copied ? '✓ COPIED' : label}
      </Text>
    </Pressable>
  );
};

const styles = StyleSheet.create({
  button: {
    paddingVertical: 8,
    paddingHorizontal: 14,
    borderWidth: 1,
    borderColor: Colors.primary,
    borderRadius: 2,
    backgroundColor: Colors.surfaceAlt,
  },
  buttonCopied: {
    borderColor: Colors.success,
    backgroundColor: Colors.successDim + '33',
  },
  text: {
    color: Colors.primary,
    fontWeight: '800',
    fontSize: 11,
    letterSpacing: 1.5,
  },
  textCopied: {
    color: Colors.success,
  },
});

export default CopyButton;
