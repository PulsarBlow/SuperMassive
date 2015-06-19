using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SuperMassive.Tests
{
    [TestClass]
    public class ColorHelperTest
    {
        /// <summary>
        ///A test for HexToInt
        ///</summary>
        [TestMethod()]
        public void HexToIntTest()
        {
            Assert.AreEqual<int>(16777215, ColorHelper.HexToInt("FFFFFF"));
            Assert.AreEqual<int>(16777215, ColorHelper.HexToInt("#FFFFFF"));

        }

        /// <summary>
        ///A test for IntToHex
        ///</summary>
        [TestMethod()]
        public void IntToHexTest()
        {
            Assert.AreEqual<string>("FFFFFF", ColorHelper.IntToHex(16777215));
        }
    }
}
