namespace SuperMassive.Tests.Unit.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NUnit.Framework;

    public class BinarySerializationHelperTest
    {
        [Test]
        public void SerializeTest()
        {
            TestObject myObject = new TestObject
            {
                ListOfElements = new List<string>(new string[] { "1==1", "A super string : #é" }),
                PeriphericalValue = 3
            };
            byte[] result = BinarySerializationHelper.Serialize(myObject);
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Length > 0);
        }

        [Test]
        public void DeserializeTest()
        {
            TestObject myObject = new TestObject
            {
                ListOfElements = new List<string>(new string[] { "1==1", "A super string : #é" }),
                PeriphericalValue = 3
            };
            TestObject result = BinarySerializationHelper.Deserialize(BinarySerializationHelper.Serialize(myObject)) as TestObject;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ListOfElements);
            Assert.IsTrue(result.ListOfElements.Count() == myObject.ListOfElements.Count());
            CollectionAssert.AreEqual(myObject.ListOfElements as List<string>, new List<string>(result.ListOfElements));
        }

        [Serializable]
        private class TestObject
        {
            public IEnumerable<string> ListOfElements { get; set; }
            public byte PeriphericalValue { get; set; }
        }
    }
}
