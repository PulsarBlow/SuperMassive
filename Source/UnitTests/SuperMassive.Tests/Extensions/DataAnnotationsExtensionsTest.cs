namespace SuperMassive.Tests.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using NUnit.Framework;
    using SuperMassive.Extensions;

    public class DataAnnotationsExtensionsTest
    {
        [Test]
        public void DataAnnotationsExtensions_ToDictionary_With_NullArgument_Throws()
        {
            IEnumerable<ValidationResult> obj = null;
            Assert.Throws<ArgumentNullException>(() => obj.ToDictionary());
        }

        [Test]
        public void DataAnnotationsExtensions_ToDictionary_With_SingleValidationResult_Returns_ValidDictionary()
        {
            IEnumerable<ValidationResult> obj = new List<ValidationResult>
            {
                new ValidationResult("error", new[] { "member1" }),
            };
            var result = obj.ToDictionary();
            Assert.IsTrue(result.ContainsKey("member1"));
            Assert.IsTrue(result["member1"] == "error");
        }

        [Test]
        public void DataAnnotationsExtensions_ToDictionary_With_MultipleValidationResult_Returns_ValidDictionary()
        {
            IEnumerable<ValidationResult> obj = new List<ValidationResult>
            {
                new ValidationResult("error", new[] { "member1", "MEMBER2" }),
            };
            var result = obj.ToDictionary();
            Assert.IsTrue(result.ContainsKey("member1,MEMBER2"));
            Assert.IsTrue(result["member1,MEMBER2"] == "error");
        }
    }
}
