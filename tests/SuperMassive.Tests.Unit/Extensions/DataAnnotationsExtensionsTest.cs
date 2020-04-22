namespace SuperMassive.Tests.Unit.Extensions
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using NUnit.Framework;
    using SuperMassive.Extensions;

    public class DataAnnotationsExtensionsTest
    {
        [Test]
        public void ToDictionary_Returns_Dictionary()
        {
            IEnumerable<ValidationResult> obj = new List<ValidationResult>
            {
                new ValidationResult("error", new[] {"member1", "MEMBER2"}),
            };
            var result = obj.ToDictionary();
            Assert.That(result["member1,MEMBER2"], Is.EqualTo("error"));
        }
    }
}
