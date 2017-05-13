using NUnit.Framework;

namespace SuperMassive.Tests
{

    public class StringHelperTest
    {
        [Test]
        public void Base64Utf8EncodeTest()
        {
            // GameChiefs1234é# -> R2FtZUNoaWVmczEyMzTDqSM=
            string data = "GameChiefs1234é#";
            string expected = "R2FtZUNoaWVmczEyMzTDqSM=";
            string actual;
            actual = StringHelper.Base64Utf8Encode(data);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Base64Utf8DecodeTest()
        {
            // GameChiefs1234é# -> R2FtZUNoaWVmczEyMzTDqSM=
            string data = "R2FtZUNoaWVmczEyMzTDqSM=";
            string expected = "GameChiefs1234é#";
            string actual;
            actual = StringHelper.Base64Utf8Decode(data);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void RemoveDiacriticsTest()
        {
            string str = "Un algorithme est un processus systématique de résolution, par le calcul, d'un problème permettant de présenter les étapes vers le résultat à une autre personne physique (un autre humain) ou virtuelle (un calculateur). En d'autres termes, un algorithme est un énoncé d’une suite finie et non-ambiguë d’opérations permettant de donner la réponse à un problème. Il décrit formellement une procédure concrète. Si ces opérations s’exécutent en séquence, on parle d’algorithme séquentiel. Si les opérations s’exécutent sur plusieurs processeurs en parallèle, on parle d’algorithme parallèle. Si les tâches s’exécutent sur un réseau de processeurs on parle d’algorithme réparti ou distribué.";
            string result = StringHelper.RemoveDiacritics(str);
            string expected = "Un algorithme est un processus systematique de resolution, par le calcul, d'un probleme permettant de presenter les etapes vers le resultat a une autre personne physique (un autre humain) ou virtuelle (un calculateur). En d'autres termes, un algorithme est un enonce d’une suite finie et non-ambigue d’operations permettant de donner la reponse a un probleme. Il decrit formellement une procedure concrete. Si ces operations s’executent en sequence, on parle d’algorithme sequentiel. Si les operations s’executent sur plusieurs processeurs en parallele, on parle d’algorithme parallele. Si les taches s’executent sur un reseau de processeurs on parle d’algorithme reparti ou distribue.";
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void CapitalizeTest()
        {
            string value = "éxécution d'un processus simple";
            Assert.AreEqual("Éxécution d'un processus simple", StringHelper.Capitalize(value));
        }

        [Test]
        public void DasherizeTest()
        {
            Assert.IsNull(StringHelper.Dasherize(null));
            Assert.AreEqual("", StringHelper.Dasherize(""));
            Assert.AreEqual(" ", StringHelper.Dasherize(" "));
            Assert.AreEqual("data-rate", StringHelper.Dasherize("dataRate"));
            Assert.AreEqual("-car-speed", StringHelper.Dasherize("CarSpeed"));
            Assert.AreEqual("yes-we-can", StringHelper.Dasherize("yesWeCan"));
            Assert.AreEqual("-élite-éclat", StringHelper.Dasherize("ÉliteÉclat"));
        }

        [Test]
        public void CamelizeTest()
        {
            Assert.IsNull(StringHelper.Camelize(null));
            Assert.AreEqual("", StringHelper.Camelize(""));
            Assert.AreEqual(" ", StringHelper.Camelize(" "));
            Assert.AreEqual("dataRate", StringHelper.Camelize("data_rate"));
            Assert.AreEqual("dataRate", StringHelper.Camelize("data__rate"));
            Assert.AreEqual("dataRate", StringHelper.Camelize("data_-rate"));
            Assert.AreEqual("CarSpeed", StringHelper.Camelize("-car-speed"));
            Assert.AreEqual("yesWeCan", StringHelper.Camelize("yes-we-can"));
            Assert.AreEqual("ÉliteÉclat", StringHelper.Camelize("-élite-éclat"));
        }

        [Test]
        public void CollapseWhiteSpaces()
        {
            Assert.AreEqual(null, StringHelper.CollapseWhiteSpaces(null));
            Assert.AreEqual("", StringHelper.CollapseWhiteSpaces(""));
            Assert.AreEqual(" String value ", StringHelper.CollapseWhiteSpaces(" String value "));
            Assert.AreEqual(" String value", StringHelper.CollapseWhiteSpaces("  String value"));
            Assert.AreEqual("String value ", StringHelper.CollapseWhiteSpaces("String value  "));
            Assert.AreEqual("String value", StringHelper.CollapseWhiteSpaces("String  value"));
            Assert.AreEqual(" String value ", StringHelper.CollapseWhiteSpaces("  String  value  "));
            Assert.AreEqual(" String value ", StringHelper.CollapseWhiteSpaces("     String      value     "));
        }
    }
}
