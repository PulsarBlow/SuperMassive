using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SuperMassive.Fakers.Tests
{
    [TestClass]
    public class NameTest
    {
        public void TaxonomyNameTest()
        {
            for (int i = 0; i < 500; i++)
            {
                string result = Name.TaxonomyName();
                Assert.IsFalse(String.IsNullOrWhiteSpace(result));
            }
        }
    }
}
