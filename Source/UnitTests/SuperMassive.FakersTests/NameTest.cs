using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SuperMassive.Fakers.Tests
{
    [TestClass]
    public class NameTest
    {
        [TestMethod]
        public void TaxonomyNameTest()
        {
            for (int i = 0; i < 500; i++)
            {
                string result = Name.TaxonomyName();
                Assert.IsFalse(String.IsNullOrWhiteSpace(result));
            }
        }

        [TestMethod]
        public void StarNameTest()
        {
            string separator = ",";
            string name = Name.StarName(separator: separator);
            Assert.IsTrue(!String.IsNullOrWhiteSpace(name));

            name = Name.StarName(2, 2, separator);
            Assert.IsTrue(!String.IsNullOrWhiteSpace(name));
            Assert.IsTrue(name.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries).Length == 2);
        }
    }
}
