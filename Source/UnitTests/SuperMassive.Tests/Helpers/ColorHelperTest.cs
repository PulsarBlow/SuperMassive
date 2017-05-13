using NUnit.Framework;

namespace SuperMassive.Tests
{
    public class ColorHelperTest
    {
        /// <summary>
        ///A test for HexToInt
        ///</summary>
        [Test()]
        public void HexToIntTest()
        {
            Assert.AreEqual(16777215, ColorHelper.HexToInt("FFFFFF"));
            Assert.AreEqual(16777215, ColorHelper.HexToInt("#FFFFFF"));

        }

        /// <summary>
        ///A test for IntToHex
        ///</summary>
        [Test()]
        public void IntToHexTest()
        {
            Assert.AreEqual("FFFFFF", ColorHelper.IntToHex(16777215));
        }
    }
}
