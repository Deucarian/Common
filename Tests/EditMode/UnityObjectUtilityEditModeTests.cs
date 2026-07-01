using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Deucarian.Common.Tests
{
    public sealed class UnityObjectUtilityEditModeTests
    {
        [Test]
        public void DestroySafelyWithNullDoesNotThrow()
        {
            Assert.DoesNotThrow(() => UnityObjectUtility.DestroySafely(null));
            LogAssert.NoUnexpectedReceived();
        }

        [Test]
        public void DestroySafelyWithUnityFakeNullDoesNotThrow()
        {
            GameObject gameObject = new GameObject("Common Fake Null Target");
            Object retainedReference = gameObject;
            Object.DestroyImmediate(gameObject);

            Assert.DoesNotThrow(() => UnityObjectUtility.DestroySafely(retainedReference));
            LogAssert.NoUnexpectedReceived();
        }

        [Test]
        public void DestroySafelyDestroysGameObjectImmediatelyOutsidePlayMode()
        {
            GameObject gameObject = new GameObject("Common EditMode GameObject Target");
            Object retainedReference = gameObject;

            UnityObjectUtility.DestroySafely(gameObject);

            Assert.IsTrue(retainedReference == null);
            LogAssert.NoUnexpectedReceived();
        }

        [Test]
        public void DestroySafelyDestroysScriptableObjectImmediatelyOutsidePlayMode()
        {
            TransientScriptableObject scriptableObject = ScriptableObject.CreateInstance<TransientScriptableObject>();
            Object retainedReference = scriptableObject;

            UnityObjectUtility.DestroySafely(scriptableObject);

            Assert.IsTrue(retainedReference == null);
            LogAssert.NoUnexpectedReceived();
        }

        [Test]
        public void RuntimeAssemblyDoesNotReferenceUnityEditor()
        {
            bool referencesUnityEditor = typeof(UnityObjectUtility)
                .Assembly
                .GetReferencedAssemblies()
                .Any(assemblyName => assemblyName.Name == "UnityEditor");

            Assert.IsFalse(referencesUnityEditor);
        }

        [Test]
        public void EasingUtilityMatchesSharedMotionPresets()
        {
            Assert.That(DeucarianEasingUtility.Evaluate(DeucarianEasing.Linear, 0.25f), Is.EqualTo(0.25f));
            Assert.That(DeucarianEasingUtility.Evaluate(DeucarianEasing.EaseInCubic, 0.5f), Is.EqualTo(0.125f));
            Assert.That(DeucarianEasingUtility.Evaluate(DeucarianEasing.EaseOutCubic, 0.5f), Is.EqualTo(0.875f));
            Assert.That(DeucarianEasingUtility.Evaluate(DeucarianEasing.EaseOutSoftBack, 0f), Is.EqualTo(0f));
            Assert.That(DeucarianEasingUtility.Evaluate(DeucarianEasing.EaseOutSoftBack, 1f), Is.EqualTo(1f));
            Assert.That(DeucarianEasingUtility.Evaluate(DeucarianEasing.EaseOutBack, -1f), Is.EqualTo(0f));
            Assert.That(DeucarianEasingUtility.Evaluate(DeucarianEasing.EaseOutBack, 2f), Is.EqualTo(1f));
        }

        private sealed class TransientScriptableObject : ScriptableObject
        {
        }
    }
}
