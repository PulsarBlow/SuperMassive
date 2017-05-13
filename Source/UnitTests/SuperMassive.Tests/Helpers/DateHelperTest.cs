using System;
using NUnit.Framework;
using SuperMassive.UnitTestHelpers;

namespace SuperMassive.Tests
{
    public class DateHelperTest
    {
        [Test]
        public void ToUnixTimeTest()
        {
            DateTime expected = DateTime.Now;
            long timeStamp = DateHelper.ToUnixTime(expected);
            DateTime actual = DateHelper.FromUnixTime(timeStamp);
            CommonComparers.AreSimilar(expected, actual);
        }
    }
}
