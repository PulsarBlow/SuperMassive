namespace SuperMassive.Tests.Unit.Helpers
{
    using System;
    using NUnit.Framework;

    public class AssemblyHelperTest
    {
        [TestCase(null)]
        [TestCase(typeof(AssemblyHelper))]
        public void GetFileVersion_Returns_NotNullOrEmpty_String(Type type)
        {
            var actual = type != null ?
                AssemblyHelper.GetFileVersion(type.Assembly) :
                AssemblyHelper.GetFileVersion();

            Assert.That(actual, Is.Not.Empty);
        }

        [TestCase(null)]
        [TestCase(typeof(AssemblyHelper))]
        public void GetInformationalVersion(Type type)
        {
            var actual = type != null ?
                AssemblyHelper.GetInformationalVersion(type.Assembly) :
                AssemblyHelper.GetInformationalVersion();

            Assert.That(actual, Is.Not.Empty);
        }
    }
}
