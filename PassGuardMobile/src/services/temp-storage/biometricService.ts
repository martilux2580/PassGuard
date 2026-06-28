/**
 * biometricService.ts
 *
 * Wraps expo-local-authentication to provide a single function that triggers
 * device biometric / PIN authentication before granting access to
 * the Temporary Storage screen.
 */

import * as LocalAuthentication from 'expo-local-authentication';

/**
 * Checks whether biometric or device-PIN authentication is available.
 * Returns true if the device has enrolled biometrics or a PIN set.
 */
export async function isBiometricAvailable(): Promise<boolean> {
  const hasHardware = await LocalAuthentication.hasHardwareAsync();
  const isEnrolled = await LocalAuthentication.isEnrolledAsync();
  return hasHardware && isEnrolled;
}

/**
 * Prompts the user for biometric (fingerprint / face) or device PIN
 * authentication.
 *
 * @param reason  Message shown in the system authentication dialog.
 * @returns       true if the user authenticated successfully, false otherwise.
 */
export async function authenticate(
  reason = 'Authenticate to access Temporary Storage',
): Promise<boolean> {
  try {
    const result = await LocalAuthentication.authenticateAsync({
      promptMessage: reason,
      // Fall back to device PIN / pattern if biometric not available
      fallbackLabel: 'Use PIN',
      disableDeviceFallback: false,
      cancelLabel: 'Cancel',
    });
    return result.success;
  } catch {
    return false;
  }
}
