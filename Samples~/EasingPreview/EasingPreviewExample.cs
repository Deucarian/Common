namespace Deucarian.Common.Samples
{
    /// <summary>
    /// Evaluates a shared easing preset at the start, midpoint, and end of a normalized transition.
    /// </summary>
    public static class EasingPreviewExample
    {
        /// <summary>
        /// Creates three representative values that can be shown in a custom preview or inspector.
        /// </summary>
        /// <param name="easing">The shared preset to evaluate.</param>
        /// <returns>Values evaluated at normalized times 0, 0.5, and 1.</returns>
        public static float[] Evaluate(DeucarianEasing easing)
        {
            return new[]
            {
                DeucarianEasingUtility.Evaluate(easing, 0f),
                DeucarianEasingUtility.Evaluate(easing, 0.5f),
                DeucarianEasingUtility.Evaluate(easing, 1f)
            };
        }
    }
}
