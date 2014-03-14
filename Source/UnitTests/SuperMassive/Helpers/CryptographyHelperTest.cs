using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuperMassive;

namespace SuperMassiveTests
{
    /// <summary>
    ///This is a test class for CryptographyHelperTest and is intended
    ///to contain all CryptographyHelperTest Unit Tests
    ///</summary>
    [TestClass]
    public class CryptographyHelperTest
    {
        /// <summary>
        ///ComputeMD5Hash
        ///</summary>
        [TestMethod]
        public void ComputeMD5HashTest()
        {

            string input = "Un algorithme est un processus systématique de résolution, par le calcul, d'un problème permettant de présenter les étapes vers le résultat à une autre personne physique (un autre humain) ou virtuelle (un calculateur). En d'autres termes, un algorithme est un énoncé d’une suite finie et non-ambiguë d’opérations permettant de donner la réponse à un problème. Il décrit formellement une procédure concrète. Si ces opérations s’exécutent en séquence, on parle d’algorithme séquentiel. Si les opérations s’exécutent sur plusieurs processeurs en parallèle, on parle d’algorithme parallèle. Si les tâches s’exécutent sur un réseau de processeurs on parle d’algorithme réparti ou distribué.";
            string expected = "785f513b187ed260c2e8fc890bd6180b";
            string actual = CryptographyHelper.ComputeMD5Hash(input);
            Assert.AreEqual<string>(expected, actual);
        }
        /// <summary>
        /// Test pour ComputeSHA256Hash
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "SHA"), TestMethod]
        public void ComputeSHA256HashTest()
        {
            string input = "abcdefgh";
            string expected = "9c56cc51b374c3ba189210d5b6d4bf57790d351c96c47c02190ecf1e430635ab";
            string actual = CryptographyHelper.ComputeSHA256Hash(input);
            Assert.AreEqual<string>(expected, actual);

            input = "Un algorithme est un processus systématique de résolution, par le calcul, d'un problème permettant de présenter les étapes vers le résultat à une autre personne physique (un autre humain) ou virtuelle (un calculateur). En d'autres termes, un algorithme est un énoncé d’une suite finie et non-ambiguë d’opérations permettant de donner la réponse à un problème. Il décrit formellement une procédure concrète. Si ces opérations s’exécutent en séquence, on parle d’algorithme séquentiel. Si les opérations s’exécutent sur plusieurs processeurs en parallèle, on parle d’algorithme parallèle. Si les tâches s’exécutent sur un réseau de processeurs on parle d’algorithme réparti ou distribué.";
            expected = "27718ccffef4e1cce1cff2a7ac4bde42ef51f6dc9efdd2e1728c9fdddb3450d7";
            actual = CryptographyHelper.ComputeSHA256Hash(input);
            Assert.AreEqual<string>(expected, actual);
        }
    }
}
