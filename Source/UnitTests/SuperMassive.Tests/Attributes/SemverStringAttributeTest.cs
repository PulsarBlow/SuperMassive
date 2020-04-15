namespace SuperMassive.Tests.Attributes
{
    using NUnit.Framework;
    using SuperMassive;

    public class SemverStringAttributeTest
    {
        [Test]
        public void Should_Succeeded()
        {
            SemverStringAttribute attribute = new SemverStringAttribute();
            Assert.IsTrue(attribute.IsValid("1.0.0-alpha-a.b-c-somethinglong+build.1-aef.1-its-okay"));
            Assert.IsTrue(attribute.IsValid("1.0.0+0.build.1-rc.10000aaa-kk-0.1"));
            Assert.IsTrue(attribute.IsValid("1.0.0-rc.1+build.1"));
            Assert.IsTrue(attribute.IsValid("2.0.0-rc.1+build.123"));
        }

        [Test]
        public void Should_Fail()
        {
            SemverStringAttribute attribute = new SemverStringAttribute();
            Assert.IsFalse(attribute.IsValid("01.1.1"));
            Assert.IsFalse(attribute.IsValid("1.2"));
            Assert.IsFalse(attribute.IsValid("1.2.3.DEV"));
            Assert.IsFalse(attribute.IsValid("-1.0.3-gamma+b7718"));
        }
    }
}
