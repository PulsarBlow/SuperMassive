namespace SuperMassive.Logging.AzureTable.Tests.Unit
{
    using System;
    using System.Globalization;
    using NUnit.Framework;

    public class ApplicationLogEntityManagerTest
    {
        [Test]
        public void CreatePartitionKeyGuardTest_NullArgument()
        {
            Assert.Throws<ArgumentNullException>(() => ApplicationLogEntityManager.CreatePartitionKey(null, DateTimeOffset.MinValue));
        }

        [Test]
        public void CreatePartitionKeyGuardTest_EmptyArgument()
        {
            Assert.Throws<ArgumentException>(() => ApplicationLogEntityManager.CreatePartitionKey("", DateTimeOffset.MinValue));
        }

        [Test]
        public void CreatePartitionKeyTest()
        {
            string expected = "myApplication_201503";
            string actual = ApplicationLogEntityManager.CreatePartitionKey("myApplication", new DateTimeOffset(2015, 3, 2, 0, 0, 0, new TimeSpan()));

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateFromLogEntryTest()
        {
            ApplicationLogEntity entity = ApplicationLogEntityManager.CreateFromLogEntry(new LogEntry
            {
                ApplicationName = "myApplication",
                TimeStamp = new DateTime(2015, 3, 2, 0, 0, 0),
                Message = "This is a log message"
            }, null);

            Assert.AreEqual("myApplication_201503", entity.PartitionKey);
            Assert.AreNotEqual(Guid.Empty.ToString(), entity.RowKey);
            Assert.AreEqual("This is a log message", entity.Message);
        }

        [Test]
        public void CreateRowKey_Ascending_WithSuccess()
        {
            DateTime date = new DateTime(DateTime.MinValue.Ticks, DateTimeKind.Utc);
            Guid guid = Guid.Empty;

            string expected = String.Format(CultureInfo.InvariantCulture, "{0:D19}_{1}", date.Ticks, guid);
            string actual = ApplicationLogEntityManager.CreateRowKey(guid, date, SortOrder.Ascending);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CreateRowKey_Descending_WithSuccess()
        {
            DateTime date = new DateTime(DateTime.MaxValue.Ticks, DateTimeKind.Utc);
            Guid guid = Guid.Empty;

            string expected = String.Format(CultureInfo.InvariantCulture, "{0:D19}_{1}", DateTime.MaxValue.Ticks - date.Ticks, guid);
            string actual = ApplicationLogEntityManager.CreateRowKey(guid, date, SortOrder.Descending);

            Assert.AreEqual(expected, actual);
        }
    }
}
