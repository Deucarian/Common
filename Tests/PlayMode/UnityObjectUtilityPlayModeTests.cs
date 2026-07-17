using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Deucarian.Common.Tests
{
    public sealed class UnityObjectUtilityPlayModeTests
    {
        [UnityTest]
        public IEnumerator DestroySafelyDestroysGameObjectAfterFrameInPlayMode()
        {
            GameObject gameObject = new GameObject("Common PlayMode GameObject Target");

            UnityObjectUtility.DestroySafely(gameObject);

            Assert.IsFalse(gameObject == null);
            yield return null;
            Assert.IsTrue(gameObject == null);
        }

        [UnityTest]
        public IEnumerator DestroySafelyWithNullIsSafeInPlayMode()
        {
            Assert.DoesNotThrow(() => UnityObjectUtility.DestroySafely(null));
            yield return null;
            LogAssert.NoUnexpectedReceived();
        }

        [UnityTest]
        public IEnumerator DestroySafelyHonorsPlayModeDelay()
        {
            GameObject gameObject = new GameObject("Common Delayed Cleanup Target");

            UnityObjectUtility.DestroySafely(gameObject, 0.05f);

            yield return null;
            Assert.IsFalse(gameObject == null);
            yield return new WaitForSeconds(0.1f);
            Assert.IsTrue(gameObject == null);
        }

        [UnityTest]
        public IEnumerator DestroySafelyHandlesComponentTargets()
        {
            GameObject gameObject = new GameObject("Common Component Owner");
            CommonTestComponent component = gameObject.AddComponent<CommonTestComponent>();

            UnityObjectUtility.DestroySafely(component);

            yield return null;
            Assert.IsTrue(component == null);

            if (gameObject != null)
            {
                Object.Destroy(gameObject);
                yield return null;
            }
        }

        [UnityTest]
        public IEnumerator DestroySafelyDoesNotThrowDuringCleanupLikeUse()
        {
            GameObject gameObject = new GameObject("Common Cleanup Target");

            Assert.DoesNotThrow(() => UnityObjectUtility.DestroySafely(gameObject));
            yield return null;

            Assert.IsTrue(gameObject == null);
        }

        private sealed class CommonTestComponent : MonoBehaviour
        {
        }
    }
}
