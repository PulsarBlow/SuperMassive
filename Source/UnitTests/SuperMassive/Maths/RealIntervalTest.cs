using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuperMassive.Maths;
using System;

namespace SuperMassiveTests.Maths
{
    /// <summary>
    ///This is a test class for IntervalTest and is intended
    ///to contain all IntervalTest Unit Tests
    ///</summary>
    [TestClass]
    public class RealIntervalTest
    {
        /// <summary>
        ///A test for IsValueInLimits
        ///</summary>
        [TestMethod]
        public void IsValueInLimitsTest()
        {
            // [1; +inf[
            RealInterval interval = new RealInterval() { LowerLimit = 1, UpperLimit = Double.PositiveInfinity, IntervalType = IntervalTypes.HalfInclusive };
            Assert.IsTrue(interval.IsValueInLimits(1));
            Assert.IsTrue(interval.IsValueInLimits(Double.MaxValue));
            Assert.IsFalse(interval.IsValueInLimits(0));
            Assert.IsFalse(interval.IsValueInLimits(0.5));
            Assert.IsFalse(interval.IsValueInLimits(Double.NegativeInfinity));
            Assert.IsFalse(interval.IsValueInLimits(Double.PositiveInfinity));

            // ]1; +inf[
            interval = new RealInterval() { LowerLimit = 1, UpperLimit = Double.PositiveInfinity, IntervalType = IntervalTypes.Exclusive };
            Assert.IsTrue(interval.IsValueInLimits(Double.MaxValue));
            Assert.IsFalse(interval.IsValueInLimits(0));
            Assert.IsFalse(interval.IsValueInLimits(0.5));
            Assert.IsFalse(interval.IsValueInLimits(1));
            Assert.IsFalse(interval.IsValueInLimits(Double.PositiveInfinity));
            Assert.IsFalse(interval.IsValueInLimits(Double.NegativeInfinity));

            // ]0;1]
            interval = new RealInterval() { LowerLimit = 0, UpperLimit = 1, IntervalType = IntervalTypes.HalfExclusive };

            Assert.IsTrue(interval.IsValueInLimits(1));
            Assert.IsFalse(interval.IsValueInLimits(Double.MaxValue));
            Assert.IsFalse(interval.IsValueInLimits(-0.1));
            Assert.IsFalse(interval.IsValueInLimits(0));
            Assert.IsTrue(interval.IsValueInLimits(0.5));
            Assert.IsFalse(interval.IsValueInLimits(Double.PositiveInfinity));
            Assert.IsFalse(interval.IsValueInLimits(Double.NegativeInfinity));
        }
    }
}
