using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SuperMassive.Fakers.Tests
{
    [TestClass]
    public class PathTest
    {
        [TestMethod]
        public void GenerateRandomFileName_WithSuccess()
        {
            string result = Path.FileName();
            Assert.IsFalse(String.IsNullOrWhiteSpace(result));
            Assert.IsTrue(result.IndexOf(".") > 0); // Has a default random extension

            result = Path.FileName(".txt");
            Assert.IsFalse(String.IsNullOrWhiteSpace(result));
            Assert.IsTrue(result.EndsWith(".txt"));
        }
    }
}
