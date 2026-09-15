using System;
using NUnit.Framework;

namespace Deucarian.Common.Tests
{
    public sealed class DeucarianEasingTests
    {
        public static Array Presets => Enum.GetValues(typeof(DeucarianEasing));

        [TestCaseSource(nameof(Presets))]
        public void EveryPresetHasExactClampedEndpointsAndFiniteSamples(DeucarianEasing easing)
        {
            Assert.That(DeucarianEasingUtility.Evaluate(easing, -1), Is.Zero);
            Assert.That(DeucarianEasingUtility.Evaluate(easing, 0), Is.Zero);
            Assert.That(DeucarianEasingUtility.Evaluate(easing, 1), Is.EqualTo(1));
            Assert.That(DeucarianEasingUtility.Evaluate(easing, 2), Is.EqualTo(1));
            for (int sample = 0; sample <= 256; sample++)
            {
                float value = DeucarianEasingUtility.Evaluate(easing, sample / 256f);
                Assert.That(float.IsNaN(value) || float.IsInfinity(value), Is.False, easing + " sample " + sample);
            }
        }

        [Test]
        public void PublishedSerializedValuesNeverChange()
        {
            Assert.That((int)DeucarianEasing.Linear, Is.Zero);
            Assert.That((int)DeucarianEasing.EaseInCubic, Is.EqualTo(1));
            Assert.That((int)DeucarianEasing.EaseOutCubic, Is.EqualTo(2));
            Assert.That((int)DeucarianEasing.EaseOutBack, Is.EqualTo(3));
            Assert.That((int)DeucarianEasing.EaseOutSoftBack, Is.EqualTo(4));
            Assert.That(Presets.Length, Is.EqualTo(32));
        }

        [TestCase(DeucarianEasing.EaseInQuad, DeucarianEasing.EaseOutQuad, DeucarianEasing.EaseInOutQuad)]
        [TestCase(DeucarianEasing.EaseInCubic, DeucarianEasing.EaseOutCubic, DeucarianEasing.EaseInOutCubic)]
        [TestCase(DeucarianEasing.EaseInQuart, DeucarianEasing.EaseOutQuart, DeucarianEasing.EaseInOutQuart)]
        [TestCase(DeucarianEasing.EaseInQuint, DeucarianEasing.EaseOutQuint, DeucarianEasing.EaseInOutQuint)]
        [TestCase(DeucarianEasing.EaseInSine, DeucarianEasing.EaseOutSine, DeucarianEasing.EaseInOutSine)]
        [TestCase(DeucarianEasing.EaseInExpo, DeucarianEasing.EaseOutExpo, DeucarianEasing.EaseInOutExpo)]
        [TestCase(DeucarianEasing.EaseInCirc, DeucarianEasing.EaseOutCirc, DeucarianEasing.EaseInOutCirc)]
        [TestCase(DeucarianEasing.EaseInBack, DeucarianEasing.EaseOutBack, DeucarianEasing.EaseInOutBack)]
        [TestCase(DeucarianEasing.EaseInElastic, DeucarianEasing.EaseOutElastic, DeucarianEasing.EaseInOutElastic)]
        [TestCase(DeucarianEasing.EaseInBounce, DeucarianEasing.EaseOutBounce, DeucarianEasing.EaseInOutBounce)]
        public void DirectionPairsMirrorAndInOutIsSymmetric(DeucarianEasing enter, DeucarianEasing exit, DeucarianEasing both)
        {
            for (int sample = 0; sample <= 100; sample++)
            {
                float t = sample / 100f;
                Assert.That(DeucarianEasingUtility.Evaluate(enter, t),
                    Is.EqualTo(1 - DeucarianEasingUtility.Evaluate(exit, 1 - t)).Within(.00001f));
                Assert.That(DeucarianEasingUtility.Evaluate(both, t),
                    Is.EqualTo(1 - DeucarianEasingUtility.Evaluate(both, 1 - t)).Within(.00001f));
            }
        }

        [Test]
        public void BackAndElasticPreserveOvershootWhileBounceReturnsToItsEndpoint()
        {
            Assert.That(DeucarianEasingUtility.Evaluate(DeucarianEasing.EaseOutBack, .7f), Is.GreaterThan(1));
            Assert.That(DeucarianEasingUtility.Evaluate(DeucarianEasing.EaseOutElastic, .15f), Is.GreaterThan(1));
            Assert.That(DeucarianEasingUtility.Evaluate(DeucarianEasing.EaseOutBounce, .6f), Is.LessThan(.9f));
            Assert.That(DeucarianEasingUtility.Evaluate((DeucarianEasing)999, .25f), Is.EqualTo(.25f));
        }
    }
}
