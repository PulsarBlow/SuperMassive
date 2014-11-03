using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuperMassive.UnitTestHelpers;
using System;

namespace SuperMassive.Tests
{
    [TestClass]
    public class DateHelperTest
    {
        [TestMethod]
        public void ToUnixTimeTest()
        {
            DateTime expected = DateTime.Now;
            long timeStamp = DateHelper.ToUnixTime(expected);
            DateTime actual = DateHelper.FromUnixTime(timeStamp);
            CommonComparers.AreSimilar(expected, actual);
        }
    }
}
