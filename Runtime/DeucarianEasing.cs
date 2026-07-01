using UnityEngine;

namespace Deucarian.Common
{
    /// <summary>
    /// Shared easing presets used by Deucarian runtime motion helpers.
    /// </summary>
    public enum DeucarianEasing
    {
        Linear = 0,
        EaseInCubic = 1,
        EaseOutCubic = 2,
        EaseOutBack = 3,
        EaseOutSoftBack = 4
    }

    /// <summary>
    /// Evaluates shared Deucarian easing presets with clamped normalized input.
    /// </summary>
    public static class DeucarianEasingUtility
    {
        public static float Evaluate(DeucarianEasing easing, float value)
        {
            float t = Mathf.Clamp01(value);
            switch (easing)
            {
                case DeucarianEasing.EaseInCubic:
                    return t * t * t;
                case DeucarianEasing.EaseOutCubic:
                {
                    float inverse = 1f - t;
                    return 1f - inverse * inverse * inverse;
                }
                case DeucarianEasing.EaseOutBack:
                    return EvaluateOutBack(t);
                case DeucarianEasing.EaseOutSoftBack:
                    return Mathf.Lerp(
                        Evaluate(DeucarianEasing.EaseOutCubic, t),
                        EvaluateOutBack(t),
                        0.42f);
                default:
                    return t;
            }
        }

        private static float EvaluateOutBack(float t)
        {
            const float overshoot = 1.70158f;
            float shifted = t - 1f;
            return 1f + shifted * shifted * ((overshoot + 1f) * shifted + overshoot);
        }
    }
}
