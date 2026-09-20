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
        EaseOutSoftBack = 4,
        EaseInQuad = 5,
        EaseOutQuad = 6,
        EaseInOutQuad = 7,
        EaseInOutCubic = 8,
        EaseInQuart = 9,
        EaseOutQuart = 10,
        EaseInOutQuart = 11,
        EaseInQuint = 12,
        EaseOutQuint = 13,
        EaseInOutQuint = 14,
        EaseInSine = 15,
        EaseOutSine = 16,
        EaseInOutSine = 17,
        EaseInExpo = 18,
        EaseOutExpo = 19,
        EaseInOutExpo = 20,
        EaseInCirc = 21,
        EaseOutCirc = 22,
        EaseInOutCirc = 23,
        EaseInBack = 24,
        EaseInOutBack = 25,
        EaseInElastic = 26,
        EaseOutElastic = 27,
        EaseInOutElastic = 28,
        EaseInBounce = 29,
        EaseOutBounce = 30,
        EaseInOutBounce = 31
    }

    /// <summary>
    /// Evaluates shared Deucarian easing presets with clamped normalized input.
    /// Unrecognized presets use linear interpolation after clamping the input.
    /// </summary>
    public static class DeucarianEasingUtility
    {
        public static float Evaluate(DeucarianEasing easing, float value)
        {
            float t = Mathf.Clamp01(value);
            if (t <= 0 || t >= 1) return t;
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
                case DeucarianEasing.EaseInQuad: return t * t;
                case DeucarianEasing.EaseOutQuad: return 1 - (1 - t) * (1 - t);
                case DeucarianEasing.EaseInOutQuad: return EvaluateInOutPower(t, 2);
                case DeucarianEasing.EaseInOutCubic: return EvaluateInOutPower(t, 3);
                case DeucarianEasing.EaseInQuart: return Mathf.Pow(t, 4);
                case DeucarianEasing.EaseOutQuart: return 1 - Mathf.Pow(1 - t, 4);
                case DeucarianEasing.EaseInOutQuart: return EvaluateInOutPower(t, 4);
                case DeucarianEasing.EaseInQuint: return Mathf.Pow(t, 5);
                case DeucarianEasing.EaseOutQuint: return 1 - Mathf.Pow(1 - t, 5);
                case DeucarianEasing.EaseInOutQuint: return EvaluateInOutPower(t, 5);
                case DeucarianEasing.EaseInSine: return 1 - Mathf.Cos(t * Mathf.PI * .5f);
                case DeucarianEasing.EaseOutSine: return Mathf.Sin(t * Mathf.PI * .5f);
                case DeucarianEasing.EaseInOutSine: return (1 - Mathf.Cos(t * Mathf.PI)) * .5f;
                case DeucarianEasing.EaseInExpo: return Mathf.Pow(2, 10 * t - 10);
                case DeucarianEasing.EaseOutExpo: return 1 - Mathf.Pow(2, -10 * t);
                case DeucarianEasing.EaseInOutExpo:
                    return t < .5f ? Mathf.Pow(2, 20 * t - 10) * .5f : 1 - Mathf.Pow(2, -20 * t + 10) * .5f;
                case DeucarianEasing.EaseInCirc: return 1 - Mathf.Sqrt(1 - t * t);
                case DeucarianEasing.EaseOutCirc: return Mathf.Sqrt(1 - (t - 1) * (t - 1));
                case DeucarianEasing.EaseInOutCirc:
                    return t < .5f ? (1 - Mathf.Sqrt(1 - 4 * t * t)) * .5f :
                        (Mathf.Sqrt(1 - Mathf.Pow(-2 * t + 2, 2)) + 1) * .5f;
                case DeucarianEasing.EaseInBack: return 1 - EvaluateOutBack(1 - t);
                case DeucarianEasing.EaseInOutBack: return EvaluateInOutBack(t);
                case DeucarianEasing.EaseInElastic: return 1 - EvaluateOutElastic(1 - t);
                case DeucarianEasing.EaseOutElastic: return EvaluateOutElastic(t);
                case DeucarianEasing.EaseInOutElastic: return EvaluateInOutElastic(t);
                case DeucarianEasing.EaseInBounce: return 1 - EvaluateOutBounce(1 - t);
                case DeucarianEasing.EaseOutBounce: return EvaluateOutBounce(t);
                case DeucarianEasing.EaseInOutBounce:
                    return t < .5f ? (1 - EvaluateOutBounce(1 - 2 * t)) * .5f :
                        (1 + EvaluateOutBounce(2 * t - 1)) * .5f;
                default:
                    return t;
            }
        }

        private static float EvaluateInOutPower(float t, int power)
            => t < .5f ? Mathf.Pow(2 * t, power) * .5f : 1 - Mathf.Pow(2 - 2 * t, power) * .5f;

        private static float EvaluateInOutBack(float t)
        {
            const float overshoot = 1.70158f * 1.525f;
            float shifted = 2 * t - 2;
            return t < .5f ? 2 * t * t * ((overshoot + 1) * 2 * t - overshoot) :
                (shifted * shifted * ((overshoot + 1) * shifted + overshoot) + 2) * .5f;
        }

        private static float EvaluateOutElastic(float t)
            => Mathf.Pow(2, -10 * t) * Mathf.Sin((10 * t - .75f) * (2 * Mathf.PI / 3)) + 1;

        private static float EvaluateInOutElastic(float t)
        {
            float wave = Mathf.Sin((20 * t - 11.125f) * (2 * Mathf.PI / 4.5f));
            return t < .5f ? -Mathf.Pow(2, 20 * t - 10) * wave * .5f :
                Mathf.Pow(2, -20 * t + 10) * wave * .5f + 1;
        }

        private static float EvaluateOutBounce(float t)
        {
            const float n = 7.5625f;
            const float d = 2.75f;
            if (t < 1 / d) return n * t * t;
            if (t < 2 / d) { t -= 1.5f / d; return n * t * t + .75f; }
            if (t < 2.5f / d) { t -= 2.25f / d; return n * t * t + .9375f; }
            t -= 2.625f / d;
            return n * t * t + .984375f;
        }

        private static float EvaluateOutBack(float t)
        {
            const float overshoot = 1.70158f;
            float shifted = t - 1f;
            return 1f + shifted * shifted * ((overshoot + 1f) * shifted + overshoot);
        }
    }
}
