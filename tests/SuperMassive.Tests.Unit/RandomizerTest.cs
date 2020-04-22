namespace SuperMassive.Tests.Unit
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;

    [TestFixture]
    public class RandomizerTest
    {
        [TestCase(null)]
        [TestCase(-10)]
        public void Randomizer_SecureRandomString_WithWrongArgument_Throws(int? length)
        {
            Assert.Throws<ArgumentException>(() => Randomizer.GetSecureRandomString(length));
        }

        [Test]
        public void Randomizer_SecureRandomString_WithValidArgument_WithDefaultLength_Returns_ValidString()
        {
            Assert.IsTrue(Randomizer.GetSecureRandomString().Length == 64);
        }

        [TestCase(10)]
        [TestCase(11)]
        [TestCase(64)]
        public void Randomizer_SecureRandomString_WithValidArgument_WithGivenLength_Returns_ValidString(int length)
        {
            Assert.IsTrue(Randomizer.GetSecureRandomString(length).Length == length);
        }

        [Test]
        public void Randomizer_SecureRandomString_Called_MultipleTime_Returns_UniqueString()
        {
            HashSet<string> hashSet = new HashSet<string>();
            for (int i = 0; i < 10000; i++)
            {
                Assert.IsTrue(hashSet.Add(Randomizer.GetSecureRandomString(Randomizer.GetInt(10, 100))));
            }
        }

        [TestCase(null)]
        [TestCase(-10)]
        public void Randomizer_RandomString_WithWrongArgument_Throws(int? length)
        {
            Assert.Throws<ArgumentException>(() => Randomizer.GetRandomString(length));
        }

        [Test]
        public void Randomizer_RandomString_WithValidArgument_Returns_ValidString()
        {
            Assert.IsTrue(Randomizer.GetRandomString().Length == 64);
        }

        [Test]
        public void Randomizer_RandomString_WithValidArgument_WithGivenLength_Returns_ValidString()
        {
            Assert.IsTrue(Randomizer.GetRandomString(10).Length == 10);
        }

        [Test]
        public void Randomizer_RandomString_Called_MultipleTime_Returns_DifferentString()
        {
            HashSet<string> hashSet = new HashSet<string>();
            for (int i = 0; i < 10000; i++)
            {
                Assert.IsTrue(hashSet.Add(Randomizer.GetRandomString(Randomizer.GetInt(10, 100))));
            }
        }

        [Test]
        public void Randomizer_FlipCoin_Returns_OneOrZero()
        {
            for (int i = 0; i < 10000; i++)
            {
                Assert.That(Randomizer.FlipCoin(), Is.EqualTo(0).Or.EqualTo(1));
            }
        }

        [Test]
        public void Randomizer_DoBernouilliTrial_Returns_OneOrZero()
        {
            Assert.That(Randomizer.DoBernoulliTrial(0), Is.EqualTo(0));
            Assert.That(Randomizer.DoBernoulliTrial(1), Is.EqualTo(1));
            Assert.That(Randomizer.DoBernoulliTrial(0.5), Is.EqualTo(0).Or.EqualTo(1));
            Assert.That(Randomizer.DoBernoulliTrial(Randomizer.GetFloat(0.5f), 0.5), Is.EqualTo(1));
            Assert.That(Randomizer.DoBernoulliTrial(Randomizer.GetFloat(0.5f, 1f), 0.5), Is.EqualTo(0));
        }

        [Test]
        public void Randomizer_Float_Returns_RandomFloat()
        {
            Assert.That(Randomizer.GetFloat(), Is.AtLeast(0).And.AtMost(1));
        }

        [Test]
        public void Randomizer_Float_WithOutOfRangeMax_Throws()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Randomizer.GetFloat(1, 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => Randomizer.GetFloat(-1));
        }

        [Test]
        public void Randomizer_Int_Returns_RandomInt()
        {
            Assert.That(Randomizer.GetInt(), Is.TypeOf<int>());
            Assert.That(Randomizer.GetInt(int.MaxValue), Is.LessThan(int.MaxValue));
            Assert.That(Randomizer.GetInt(int.MaxValue, int.MaxValue), Is.EqualTo(int.MaxValue));
        }
    }
}
