namespace SuperMassive.Fakers.Tests.Units
{
    using System;
    using NUnit.Framework;

    public class PathTest
    {
        [Test]
        public void GenerateRandomFileName_WithSuccess()
        {
            string result = Path.FileName();
            Assert.False(String.IsNullOrWhiteSpace(result));
            Assert.True(result.IndexOf(".") > 0); // Has a default random extension

            result = Path.FileName(".txt");
            Assert.False(String.IsNullOrWhiteSpace(result));
            Assert.True(result.EndsWith(".txt"));
        }
    }
}
