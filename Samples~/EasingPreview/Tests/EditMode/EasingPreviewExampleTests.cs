using NUnit.Framework;

namespace Deucarian.Common.Samples.Tests
{
    public sealed class EasingPreviewExampleTests
    {
        [Test]
        public void EvaluateReturnsExpectedEaseOutCubicPreview()
        {
            float[] values = EasingPreviewExample.Evaluate(DeucarianEasing.EaseOutCubic);

            Assert.That(values, Has.Length.EqualTo(3));
            Assert.That(values[0], Is.EqualTo(0f));
            Assert.That(values[1], Is.EqualTo(0.875f));
            Assert.That(values[2], Is.EqualTo(1f));
        }
    }
}
