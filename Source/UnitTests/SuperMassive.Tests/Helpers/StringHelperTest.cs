using System;

namespace SuperMassive.Tests.Helpers
{
    using NUnit.Framework;

    public class StringHelperTest
    {
        private const string Base64DecodedValue = "SuperMassive";
        private const string Base64EncodedValue = "U3VwZXJNYXNzaXZl";

        [TestCase("", "")]
        [TestCase(Base64DecodedValue, Base64EncodedValue)]
        public void Base64Utf8EncodeTest(string value, string expected)
        {
            Assert.AreEqual(expected, StringHelper.Base64Utf8Encode(value));
        }

        [TestCase("", null)]
        [TestCase(" ", "")]
        [TestCase(Base64EncodedValue, Base64DecodedValue)]
        [TestCase("NotBase64Format", null)]
        public void Base64Utf8DecodeTest(string value, string expected)
        {
            Assert.AreEqual(expected, StringHelper.Base64Utf8Decode(value));
        }

        [Test]
        public void RemoveDiacriticsTest()
        {
            const string value = "éàèäâaùabc";
            const string expected = "eaeaaauabc";

            var actual = StringHelper.RemoveDiacritics(value);

            Assert.AreEqual(expected, actual);
        }

        [TestCase("", "")]
        [TestCase(" ", " ")]
        [TestCase("à l'heure", "À l'heure")]
        [TestCase("a demain", "A demain")]
        public void CapitalizeTest(string value, string expected)
        {
            Assert.AreEqual(expected, StringHelper.Capitalize(value));
        }

        [TestCase("", "")]
        [TestCase(" ", " ")]
        [TestCase("dataRate", "data-rate")]
        [TestCase("yesWeCan", "yes-we-can")]
        // TODO: Fix StringHelper.Dasherize behavior. Should not return a dash for the word boundaries
        [TestCase("CarSpeed", "-car-speed")]
        [TestCase("ÉliteÉclat", "-élite-éclat")]
        public void DasherizeTest(string value, string expected)
        {
            Assert.AreEqual(expected, StringHelper.Dasherize(value));
        }

        [TestCase("", "")]
        [TestCase(" ", " ")]
        [TestCase("abc def", "abc def")]
        [TestCase("data_rate", "dataRate")]
        [TestCase("data__rate", "dataRate")]
        [TestCase("data_-rate", "dataRate")]
        [TestCase("-car-speed", "CarSpeed")]
        [TestCase("yes-we-can", "yesWeCan")]
        [TestCase("-élite-éclat", "ÉliteÉclat")]
        public void CamelizeTest(string value, string expected)
        {
            Assert.AreEqual(expected, StringHelper.Camelize(value));
        }

        [TestCase("", "")]
        [TestCase(" ", " ")]
        [TestCase("  ", " ")]
        [TestCase(" two words ", " two words ")]
        [TestCase("  two words", " two words")]
        [TestCase("two words  ", "two words ")]
        [TestCase("two  words", "two words")]
        [TestCase("  two  words  ", " two words ")]
        [TestCase("     two      words     ", " two words ")]
        public void CollapseWhiteSpaces(string value, string expected)
        {
            Assert.AreEqual(expected, StringHelper.CollapseWhiteSpaces(value));
        }
    }
}
