namespace SuperMassive.Fakers.Tests
{
    using System;
    using NUnit.Framework;

    public class NameTest
    {
        [Test]
        public void TaxonomyNameTest()
        {
            for (int i = 0; i < 500; i++)
            {
                string result = Name.TaxonomyName();
                Assert.False(String.IsNullOrWhiteSpace(result));
            }
        }

        [Test]
        public void StarNameTest()
        {
            string separator = ",";
            string name = Name.StarName(separator: separator);
            Assert.True(!String.IsNullOrWhiteSpace(name));

            name = Name.StarName(2, 2, separator);
            Assert.True(!String.IsNullOrWhiteSpace(name));
            Assert.True(name.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries).Length == 2);
        }
    }
}
