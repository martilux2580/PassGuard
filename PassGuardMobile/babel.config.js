module.exports = function (api) {
  api.cache(true);
  return {
    presets: [
      [
        'babel-preset-expo',
        {
          // Disable the automatic react-native-reanimated/plugin injection.
          // Reanimated 4.x requires react-native-worklets which is not available
          // in the Expo Go managed workflow. We only use predefined animations
          // (FadeIn, FadeInDown, withSpring) which work without the Babel plugin.
          reanimated: false,
        },
      ],
    ],
  };
};
