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
            if (target == null)
            {
                return;
            }

            if (Application.isPlaying)
            {
                Object.Destroy(target);
            }
            else
            {
                Object.DestroyImmediate(target);
            }
        }
    }
}
