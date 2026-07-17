# Easing Preview

This sample shows the smallest useful integration with Deucarian Common: evaluating one shared motion preset at representative normalized times.

Call `EasingPreviewExample.Evaluate(DeucarianEasing.EaseOutCubic)` to obtain the start, midpoint, and end values. Use the same `DeucarianEasingUtility.Evaluate` call with your own normalized time when driving an animation or editor preview.

The example is pure C# and needs no scene.
