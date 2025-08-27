using System;
using IdleSkiller.Core.Time;
using NUnit.Framework;

namespace IdleSkiller.Tests
{
    public class TimeServiceTests
    {
        [Test]
        public void UtcNow_ReturnsCurrentTime()
        {
            var service = new TimeService();
            var before = DateTime.UtcNow;

            var serviceNow = service.UtcNow;

            var after = DateTime.UtcNow;
            Assert.That(serviceNow, Is.GreaterThanOrEqualTo(before).And.LessThanOrEqualTo(after));
        }

        [Test]
        [TestCase(10, 2, 1, 5)]
        [TestCase(10, 3, 1, 3)]
        [TestCase(10, 2, 2, 10)]
        [TestCase(0, 5, 1, 0)]
        [TestCase(10, 0, 1, 0)]
        [TestCase(10, 5, 0, 0)]
        public void CompletedCycles_ComputesExpected(double elapsed, double duration, double speedMultiplier, int expected)
        {
            var result = OfflineGrant.CompletedCycles(elapsed, duration, speedMultiplier);
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
