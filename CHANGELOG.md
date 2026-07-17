# Changelog

## 0.2.1 - 2026-07-17

- Added the sample smoke test's direct Common assembly reference so imported samples compile independently.
- Hardened safe Unity object cleanup and added the compiled easing example required by the portfolio sample contract.

## 0.2.0 - 2026-07-01

- Added shared Deucarian easing presets for reusable runtime motion packages.

## 0.1.0 - 2026-06-22

- Created the initial `com.deucarian.common` package.
- Added `UnityObjectUtility.DestroySafely(UnityEngine.Object target)` for safe transient Unity object cleanup across Play Mode and Edit Mode.
