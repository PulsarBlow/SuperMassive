namespace SuperMassive.Tests
{
    using System;
    using NUnit.Framework;
    using SuperMassive.Extensions;

    public class DateTimeExtensionsTest
    {
        [Test]
        public void TrimTest()
        {
            DateTime date = new DateTime(2013, 10, 25, 3, 2, 59, 354, DateTimeKind.Utc);
            Assert.AreEqual(
                new DateTime(2013, 10, 25, 3, 2, 59, 354, DateTimeKind.Utc),
                date);
            Assert.AreEqual(
                new DateTime(2013, 10, 25, 3, 2, 59, 354, DateTimeKind.Utc),
                date.Trim(TimeSpan.TicksPerMillisecond));
            Assert.AreEqual(
                new DateTime(2013, 10, 25, 3, 2, 59, 0, DateTimeKind.Utc),
                date.Trim(TimeSpan.TicksPerSecond));
            Assert.AreEqual(
                new DateTime(2013, 10, 25, 3, 2, 0, 0, DateTimeKind.Utc),
                date.Trim(TimeSpan.TicksPerMinute));
            Assert.AreEqual(
                new DateTime(2013, 10, 25, 3, 0, 0, 0, DateTimeKind.Utc),
                date.Trim(TimeSpan.TicksPerHour));

            date = new DateTime(2013, 10, 25, 3, 2, 59, 354, DateTimeKind.Local);
            Assert.AreEqual(new DateTime(2013, 10, 25, 0, 0, 0, 0, DateTimeKind.Local),
                date.Trim(TimeSpan.TicksPerDay));
        }

        [Test]
        public void RoundTest()
        {
            DateTime date = new DateTime(2013, 10, 25, 3, 2, 59, 354, DateTimeKind.Utc);
            Assert.AreEqual(
                new DateTime(2013, 10, 25, 0, 0, 0, DateTimeKind.Utc),
                date.Round(new TimeSpan(1, 0, 0, 0, 0)));

            date = new DateTime(2013, 10, 25, 18, 2, 59, 354, DateTimeKind.Utc);
            Assert.AreEqual(
                new DateTime(2013, 10, 26, 0, 0, 0, DateTimeKind.Utc),
                date.Round(new TimeSpan(1, 0, 0, 0, 0)));

            date = new DateTime(2013, 10, 25, 12, 0, 0, 0, DateTimeKind.Utc);
            Assert.AreEqual(
                new DateTime(2013, 10, 26, 0, 0, 0, DateTimeKind.Utc),
                date.Round(new TimeSpan(1, 0, 0, 0, 0)));

            date = new DateTime(2013, 10, 25, 11, 59, 59, 999, DateTimeKind.Utc);
            Assert.AreEqual(
                new DateTime(2013, 10, 25, 0, 0, 0, DateTimeKind.Utc),
                date.Round(new TimeSpan(1, 0, 0, 0, 0)));
        }

        [Test]
        public void FloorTest()
        {
            DateTime date = new DateTime(2013, 10, 25, 3, 2, 59, 354, DateTimeKind.Utc);
            Assert.AreEqual(
                new DateTime(2013, 10, 25, 0, 0, 0, DateTimeKind.Utc),
                date.Floor(new TimeSpan(1, 0, 0, 0, 0)));

            date = new DateTime(2013, 10, 25, 18, 2, 59, 354, DateTimeKind.Utc);
            Assert.AreEqual(
                new DateTime(2013, 10, 25, 0, 0, 0, DateTimeKind.Utc),
                date.Floor(new TimeSpan(1, 0, 0, 0, 0)));

            date = new DateTime(2013, 10, 25, 12, 0, 0, 0, DateTimeKind.Utc);
            Assert.AreEqual(
                new DateTime(2013, 10, 25, 0, 0, 0, DateTimeKind.Utc),
                date.Floor(new TimeSpan(1, 0, 0, 0, 0)));

            date = new DateTime(2013, 10, 25, 11, 59, 59, 999, DateTimeKind.Utc);
            Assert.AreEqual(
                new DateTime(2013, 10, 25, 0, 0, 0, DateTimeKind.Utc),
                date.Floor(new TimeSpan(1, 0, 0, 0, 0)));
        }

        [Test]
        public void CeilTest()
        {
            DateTime date = new DateTime(2013, 10, 25, 3, 2, 59, 354, DateTimeKind.Utc);
            Assert.AreEqual(
                new DateTime(2013, 10, 26, 0, 0, 0, DateTimeKind.Utc),
                date.Ceil(new TimeSpan(1, 0, 0, 0, 0)));

            date = new DateTime(2013, 10, 25, 18, 2, 59, 354, DateTimeKind.Utc);
            Assert.AreEqual(
                new DateTime(2013, 10, 26, 0, 0, 0, DateTimeKind.Utc),
                date.Ceil(new TimeSpan(1, 0, 0, 0, 0)));

            date = new DateTime(2013, 10, 25, 12, 0, 0, 0, DateTimeKind.Utc);
            Assert.AreEqual(
                new DateTime(2013, 10, 26, 0, 0, 0, DateTimeKind.Utc),
                date.Ceil(new TimeSpan(1, 0, 0, 0, 0)));

            date = new DateTime(2013, 10, 25, 11, 59, 59, 999, DateTimeKind.Utc);
            Assert.AreEqual(
                new DateTime(2013, 10, 26, 0, 0, 0, DateTimeKind.Utc),
                date.Ceil(new TimeSpan(1, 0, 0, 0, 0)));
        }
    }
}
