using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuperMassive;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SuperMassiveTests
{
    /// <summary>
    ///This is a test class for BinarySerializationHelperTest and is intended
    ///to contain all BinarySerializationHelperTest Unit Tests
    ///</summary>
    [TestClass()]
    public class BinarySerializationHelperTest
    {


        /// <summary>
        ///A test for Serialize
        ///</summary>
        [TestMethod()]
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

        /// <summary>
        ///A test for Deserialize
        ///</summary>
        [TestMethod()]
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
        /// <summary>
        /// A container object to store rules into the underlying storage
        /// </summary>
        [Serializable]
        private class TestObject
        {
            /// <summary>
            /// Collection of rules
            /// </summary>
            public IEnumerable<string> ListOfElements { get; set; }
            /// <summary>
            /// Completion type
            /// </summary>
            public byte PeriphericalValue { get; set; }
        }
    }
}
