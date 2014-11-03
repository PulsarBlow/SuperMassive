using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SuperMassive.Fakers.Tests
{
    [TestClass]
    public class DateSequenceTest
    {
        [TestMethod]
        public void TestSequence()
        {
            DateTime dateStart = new DateTime(2010, 1, 1);
            TimeSpan interval = TimeSpan.FromHours(1);
            DateSequence sequence = new DateSequence(dateStart, interval);
            DateTime prev = dateStart;
            for (int i = 0; i < 1000; i++)
            {
                DateTime nextDate = sequence.Next();
                Assert.IsTrue(nextDate > prev);
            }
        }
    }
}
