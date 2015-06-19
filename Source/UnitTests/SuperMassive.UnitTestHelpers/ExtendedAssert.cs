using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;

namespace SuperMassive.UnitTestHelpers
{
    public static class ExtendedAssert
    {
        public static void IsTrueWithMessage(Func<string, bool> assertion, string value)
        {
            Assert.IsTrue(assertion.Invoke(value), String.Format(CultureInfo.InvariantCulture, "{0} should succeed", value));
        }

        public static void IsFalseWithMessage(Func<string, bool> assertion, string value)
        {
            Assert.IsFalse(assertion.Invoke(value), String.Format(CultureInfo.InvariantCulture, "{0} should fail", value));
        }
    }
}
