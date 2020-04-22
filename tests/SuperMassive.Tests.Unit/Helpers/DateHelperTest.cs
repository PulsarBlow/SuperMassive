namespace SuperMassive.Tests.Unit.Helpers
{
    using System;
    using NUnit.Framework;

    public class DateHelperTest
    {
        [Test]
        public void ToUnixTimeTest()
        {
            const string comparisonFormat = "yyyy/MM/dd HH:mm:ss";

            var expected = DateTime.Now;
            var timeStamp = DateHelper.ToUnixTime(expected);
            var actual = DateHelper.FromUnixTime(timeStamp);

            Assert.That(actual.ToString(comparisonFormat), Is.EqualTo(expected.ToString(comparisonFormat)));
        }
    }
}
