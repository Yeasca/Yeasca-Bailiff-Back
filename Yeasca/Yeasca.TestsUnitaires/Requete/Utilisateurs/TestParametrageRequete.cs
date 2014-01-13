using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yeasca.Metier;
using Yeasca.Requete;

namespace Yeasca.TestsUnitaires.Requete.Utilisateurs
{
    [TestClass]
    public class TestParametrageRequete
    {
        [TestInitialize]
        public void initialiser()
        {
            ConfigurationTest.initialiserLesEntrepotsMongo();
            ConfigurationTest.initialiserLaSessionHTTP(TypeUtilisateur.Secrétaire);
        }

        [TestMethod]
        public void TestParametrageRequete_peutRécupérerLeParamétrage()
        {
            ReponseRequete réponse = BusRequete.exécuter(new ParametrageMessageTest());

            Assert.IsTrue(réponse.Réussite);

            ParametrageReponse résultat = réponse.Résultat as ParametrageReponse;

            Assert.IsNotNull(résultat);
        }
    }

    public class ParametrageMessageTest : IParametrageMessage
    {
    }
}
