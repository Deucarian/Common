using UnityEngine;

namespace Deucarian.Common
{
    /// <summary>
    /// Provides approved low-level helpers for working with transient Unity object lifetime.
    /// </summary>
    public static class UnityObjectUtility
    {
        /// <summary>
        /// Destroys a transient Unity object using deferred Play Mode destruction or immediate Edit Mode destruction.
        /// </summary>
        /// <param name="target">The transient Unity object to destroy. Null and Unity fake-null references are ignored.</param>
        public static void DestroySafely(Object target)
        {
            DestroySafely(target, 0f);
        }

        /// <summary>
        /// Destroys a transient Unity object after an optional Play Mode delay, or immediately in Edit Mode.
        /// </summary>
        /// <param name="target">The transient Unity object to destroy. Null and Unity fake-null references are ignored.</param>
        /// <param name="delaySeconds">The non-negative Play Mode delay before destruction.</param>
        public static void DestroySafely(Object target, float delaySeconds)
        {
            if (target == null)
            {
                return;
            }

            if (Application.isPlaying)
            {
                Object.Destroy(target, Mathf.Max(0f, delaySeconds));
            }
            else
            {
                Object.DestroyImmediate(target);
            }
        }
    }
}
