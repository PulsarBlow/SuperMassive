using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuperMassive.Fakers;
using System.Collections.Generic;
using System.Linq;

namespace SuperMassive.FakersTests
{
    [TestClass]
    public class InternetTest
    {
        [TestMethod]
        public void UserName_BulkGeneration()
        {
            Dictionary<string, string> userNames = new Dictionary<string, string>();
            for (int i = 0; i < 100; i++)
            {
                string value = Internet.UserName();
                Assert.IsTrue(!userNames.ContainsKey(value));
                userNames.Add(value, value);
            }
        }
    }
}
