namespace SuperMassive.Tests.Unit.Attributes
{
    using System;
    using NUnit.Framework;
    using SuperMassive;

    public class GuidStringAttributeTest
    {
        [Test]
        public void Should_Succeeded()
        {
            GuidStringAttribute attribute = new GuidStringAttribute(true);
            Assert.IsTrue(attribute.IsValid(Guid.Empty.ToString()));
            Assert.IsTrue(attribute.IsValid("b77ad6f9-624a-4c28-96e8-545923e56502"));
            Assert.IsTrue(attribute.IsValid("B77AD6F9-624A-4C28-96E8-545923E56502"));
            Assert.IsTrue(attribute.IsValid(Guid.NewGuid().ToString()));
        }

        [Test]
        public void Should_Fail()
        {
            GuidStringAttribute attribute = new GuidStringAttribute(false);
            Assert.IsFalse(attribute.IsValid(Guid.Empty.ToString()));
            Assert.IsFalse(attribute.IsValid("abc"));
            Assert.IsFalse(attribute.IsValid("123"));
            Assert.IsFalse(attribute.IsValid("g77ad6f9-624a-4c28-96e8-545923e56502"));
        }
    }
}
