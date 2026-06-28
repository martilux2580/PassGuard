import React from 'react';
import Svg, { Path, G } from 'react-native-svg';
import { View, StyleSheet } from 'react-native';

interface PassGuardLogoProps {
  /** Overall size of the bounding box in dp */
  size?: number;
  /** Stroke/fill colour of the knot */
  color?: string;
}

/**
 * PassGuardLogo
 *
 * A clean, mathematically precise Celtic triquetra (triple knot) drawn
 * entirely in SVG so it renders crisp at any resolution.
 *
 * Construction: three identical D-shaped arcs, each rotated 120°.
 * The weaving (over/under) effect is achieved by drawing the three
 * "under" segments first and the three "over" segments on top.
 *
 * ViewBox: 0 0 200 200  —  the knot fits inside a circle of r=90 at (100,100).
 */
const PassGuardLogo: React.FC<PassGuardLogoProps> = ({
  size = 120,
  color = '#FFFFFF',
}) => {
  const sw = 13; // stroke width inside 200×200 viewBox

  return (
    <View style={[styles.container, { width: size, height: size }]}>
      <Svg width={size} height={size} viewBox="0 0 200 200" fill="none">

        {/* ─────────────────────────────────────────────────────────────
            UNDER-strands  (drawn first so they appear behind)
            Each arc is a circular arc: radius 75, going 240°.
            Centers of the three arcs are placed 43 px from the figure
            centre, separated by 120°.

            Arc centres (relative to 100,100):
              Top    : (100, 57)       → angle offset 270°
              BotLeft: (63,  78.5)     → angle offset  30°
              BotRight:(137, 78.5)     → angle offset 150°
        ───────────────────────────────────────────────────────────── */}

        {/* Strand A – the top lobe, under segments */}
        <Path
          d="M 59 130
             A 75 75 0 1 1 141 130"
          stroke={color}
          strokeWidth={sw}
          strokeLinecap="round"
        />

        {/* Strand B – bottom-right lobe, under segments */}
        <Path
          d="M 100 47
             A 75 75 0 1 1 41 148"
          stroke={color}
          strokeWidth={sw}
          strokeLinecap="round"
        />

        {/* Strand C – bottom-left lobe, under segments */}
        <Path
          d="M 159 148
             A 75 75 0 1 1 100 47"
          stroke={color}
          strokeWidth={sw}
          strokeLinecap="round"
        />

        {/* ─────────────────────────────────────────────────────────────
            OVER-strands  (drawn on top to create the weaving illusion)
            Each is a short segment near the three crossing points.
        ───────────────────────────────────────────────────────────── */}

        {/* Crossing top-left: strand C goes over strand A */}
        <Path
          d="M 47 103
             A 75 75 0 0 0 59 130"
          stroke={color}
          strokeWidth={sw}
          strokeLinecap="round"
        />

        {/* Crossing top-right: strand B goes over strand C */}
        <Path
          d="M 141 130
             A 75 75 0 0 0 153 103"
          stroke={color}
          strokeWidth={sw}
          strokeLinecap="round"
        />

        {/* Crossing bottom: strand A goes over strand B */}
        <Path
          d="M 68 60
             A 75 75 0 0 0 100 47"
          stroke={color}
          strokeWidth={sw}
          strokeLinecap="round"
        />
        <Path
          d="M 100 47
             A 75 75 0 0 0 132 60"
          stroke={color}
          strokeWidth={sw}
          strokeLinecap="round"
        />

      </Svg>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    alignItems: 'center',
    justifyContent: 'center',
  },
});

export default PassGuardLogo;
