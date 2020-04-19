namespace SuperMassive.Tests
{
    using NUnit.Framework;

    public class BitwiseMaskTest
    {
        [Test]
        public void Has_Returns_True()
        {
            var mask = new BitwiseMask(short.MaxValue);
            Assert.That(mask.Has(short.MaxValue), Is.True);

            mask = new BitwiseMask(int.MaxValue);
            Assert.That(mask.Has(int.MaxValue), Is.True);

            mask = new BitwiseMask(long.MaxValue);
            Assert.That(mask.Has(long.MaxValue), Is.True);
        }

        [Test]
        public void Has_Returns_False()
        {
            var mask = new BitwiseMask();
            Assert.That(mask.Has(short.MaxValue), Is.False);
            Assert.That(mask.Has(int.MaxValue), Is.False);
            Assert.That(mask.Has(long.MaxValue), Is.False);
        }

        [Test]
        public void Is_Returns_True()
        {
            var mask = new BitwiseMask(short.MaxValue);
            Assert.That(mask.Is(short.MaxValue), Is.True);

            mask = new BitwiseMask(int.MaxValue);
            Assert.That(mask.Is(int.MaxValue), Is.True);

            mask = new BitwiseMask(long.MaxValue);
            Assert.That(mask.Is(long.MaxValue), Is.True);
        }

        [Test]
        public void Is_Returns_False()
        {
            var mask = new BitwiseMask();
            Assert.That(mask.Is(short.MaxValue), Is.False);
            Assert.That(mask.Is(int.MaxValue), Is.False);
            Assert.That(mask.Is(long.MaxValue), Is.False);
        }

        [Test]
        public void Add_Adds()
        {
            var mask = new BitwiseMask((short)1);
            mask.Add((short) 2);
            Assert.That(mask.Is((short)3), Is.True);

            mask = new BitwiseMask(1);
            mask.Add(2);
            Assert.That(mask.Is(3), Is.True);

            mask = new BitwiseMask((long) 1);
            mask.Add((long) 2);
            Assert.That(mask.Is((long) 3), Is.True);
        }

        [Test]
        public void Remove_Removes()
        {
            var mask = new BitwiseMask((short) 3);
            mask.Remove((short) 1);
            Assert.That(mask.Is((short) 2), Is.True);

            mask = new BitwiseMask(3);
            mask.Remove(1);
            Assert.That(mask.Is(2), Is.True);

            mask = new BitwiseMask((long) 3);
            mask.Remove((long) 1);
            Assert.That(mask.Is((long) 2), Is.True);
        }

        [Test]
        public void Is_Empty_Returns_True_When_Empty()
        {
            var mask = new BitwiseMask();
            Assert.That(mask.IsEmpty(), Is.True);
        }

        [Test]
        public void Reset_Resets()
        {
            var mask = new BitwiseMask(1);
            mask.Reset();

            Assert.That(mask.IsEmpty, Is.True);
        }
    }
}
