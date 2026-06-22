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

        private sealed class TransientScriptableObject : ScriptableObject
        {
        }
    }
}
