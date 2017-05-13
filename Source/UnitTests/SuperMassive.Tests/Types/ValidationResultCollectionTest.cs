using System.ComponentModel.DataAnnotations;
using NUnit.Framework;

namespace SuperMassive.Tests
{
    public class ValidationResultCollectionTest
    {
        [Test]
        public void ValidationResult_ToString_WithCorrectFormat()
        {
            ValidationResultCollection collection = new ValidationResultCollection();
            collection.Add(new ValidationResult("This is an error message", new string[] { "Property1", "Property2" }));
            collection.Add(new ValidationResult("This is an error message", new string[] { "Property1", "Property2" }));

            Assert.AreEqual("This is an error message (Property1, Property2)\nThis is an error message (Property1, Property2)\n", collection.ToString());
        }
    }
}
