using Microsoft.VisualStudio.TestTools.UnitTesting;
using SuperMassive;

namespace SuperMassiveTests
{
    [TestClass]
    public class StringHelperTest
    {
        /// <summary>
        ///A test for Base64Utf8Encode
        ///</summary>
        [TestMethod()]
        public void Base64Utf8EncodeTest()
        {
            // GameChiefs1234é# -> R2FtZUNoaWVmczEyMzTDqSM=
            string data = "GameChiefs1234é#";
            string expected = "R2FtZUNoaWVmczEyMzTDqSM=";
            string actual;
            actual = StringHelper.Base64Utf8Encode(data);
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        ///A test for Base64Utf8Decode
        ///</summary>
        [TestMethod()]
        public void Base64Utf8DecodeTest()
        {
            // GameChiefs1234é# -> R2FtZUNoaWVmczEyMzTDqSM=
            string data = "R2FtZUNoaWVmczEyMzTDqSM=";
            string expected = "GameChiefs1234é#";
            string actual;
            actual = StringHelper.Base64Utf8Decode(data);
            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        ///Test pour RemoveDiacritics
        ///</summary>
        [TestMethod()]
        public void RemoveDiacriticsTest()
        {
            string str = "Un algorithme est un processus systématique de résolution, par le calcul, d'un problème permettant de présenter les étapes vers le résultat à une autre personne physique (un autre humain) ou virtuelle (un calculateur). En d'autres termes, un algorithme est un énoncé d’une suite finie et non-ambiguë d’opérations permettant de donner la réponse à un problème. Il décrit formellement une procédure concrète. Si ces opérations s’exécutent en séquence, on parle d’algorithme séquentiel. Si les opérations s’exécutent sur plusieurs processeurs en parallèle, on parle d’algorithme parallèle. Si les tâches s’exécutent sur un réseau de processeurs on parle d’algorithme réparti ou distribué.";
            string result = StringHelper.RemoveDiacritics(str);
            string expected = "Un algorithme est un processus systematique de resolution, par le calcul, d'un probleme permettant de presenter les etapes vers le resultat a une autre personne physique (un autre humain) ou virtuelle (un calculateur). En d'autres termes, un algorithme est un enonce d’une suite finie et non-ambigue d’operations permettant de donner la reponse a un probleme. Il decrit formellement une procedure concrete. Si ces operations s’executent en sequence, on parle d’algorithme sequentiel. Si les operations s’executent sur plusieurs processeurs en parallele, on parle d’algorithme parallele. Si les taches s’executent sur un reseau de processeurs on parle d’algorithme reparti ou distribue.";
            Assert.AreEqual<string>(expected, result);
        }
        [TestMethod]
        public void CapitalizeFirstLetterTest()
        {
            string value = "éxécution d'un processus simple";
            Assert.AreEqual("Éxécution d'un processus simple", StringHelper.CapitalizeFirstLetter(value));
        }
    }
}
