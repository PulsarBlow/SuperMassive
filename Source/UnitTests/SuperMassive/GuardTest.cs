using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

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

        [TestMethod]
        public void Requires()
        {
            try
            {
                Guard.Requires(() => Directory.Exists(Path.GetRandomFileName()), "shouldExists", "testArgument");
                Assert.Fail();
            }
            catch(Exception ex)
            {
                Assert.IsTrue(ex.Message.StartsWith("Precondition shouldExists for argument testArgument failed."));
            }
        }

        [TestMethod]
        public void Requires_WithArgumentName()
        {
            try
            {
                Guard.Requires(() => Directory.Exists(Path.GetRandomFileName()), argumentName: "testArgument");
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.StartsWith("Precondition for argument testArgument failed."));
            }
        }

        [TestMethod]
        public void Requires_WithPreconditionName()
        {
            try
            {
                Guard.Requires(() => Directory.Exists(Path.GetRandomFileName()), preconditionName: "shouldExist");
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.StartsWith("Precondition shouldExist for argument failed."));
            }
        }

        [TestMethod]
        public void Requires_WithNoArguments()
        {
            try
            {
                Guard.Requires(() => Directory.Exists(Path.GetRandomFileName()));
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.StartsWith("Precondition for argument failed."));
            }
        }
    }
}
