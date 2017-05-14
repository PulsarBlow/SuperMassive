namespace SuperMassive.Fakers.Tests
{
    using NUnit.Framework;

    public class ColorTest
    {
        [Test]
        public void ColorAsIntegerTest()
        {
            for (int i = 0; i < 500; i++)
            {
                int result = Fakers.Color.ColorAsInteger();
                System.Drawing.Color color = System.Drawing.Color.FromArgb(result);
                Assert.False(color.IsEmpty);
            }
        }
    }
}
