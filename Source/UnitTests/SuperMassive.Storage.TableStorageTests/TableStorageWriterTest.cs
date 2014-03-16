using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SuperMassive.Storage.TableStorageTests
{
    [TestClass]
    public class TableStorageWriterTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateTableStorageWiter_NoTableName_WithException()
        {
            //TableStorageWriter writer = new TableStorageWriter("", )
        }
    }
}
