using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SuperMassive.Tests
{
    [TestClass]
    public class GuardTest
    {
        [TestMethod]
        public void IsInstanceOfType()
        {
            Type type = typeof(String);
            object value = "Test";
            string argumentName = "arg1";
            Guard.IsInstanceOfType(type, value, argumentName);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void IsInstanceOfType_ExceptionThrown()
        {
            Type type = typeof(Int32);
            object value = "Test";
            string argumentName = "arg1";
            Guard.IsInstanceOfType(type, value, argumentName);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ArgumentNotNullOrWhiteSpace_1()
        {
            Guard.ArgumentNotNullOrWhiteSpace(null, "argumentName");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ArgumentNotNullOrWhiteSpace_2()
        {
            Guard.ArgumentNotNullOrWhiteSpace("", "argumentName");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ArgumentNotNullOrWhiteSpace_3()
        {
            Guard.ArgumentNotNullOrWhiteSpace(" ", "argumentName");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ArgumentNotNullOrWhiteSpace_4()
        {
            Guard.ArgumentNotNullOrWhiteSpace("    ", "argumentName");
        }
    }
}
