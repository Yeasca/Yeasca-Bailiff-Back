using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yeasca.Metier;
using Yeasca.Requete;

namespace Yeasca.TestsUnitaires.Requete.Utilisateurs
{
    [TestClass]
    public class TestAdminEstCreeRequete
    {
        [TestInitialize]
        public void initialiser()
        {
            ConfigurationTest.initialiserLesEntrepotsMongo();
        }

        [TestMethod]
        public void TestAdminEstCreeRequete_peutVoirSiUnAdminEstCrééDansUnEnvironnementAvecAdmin()
        {
            ConfigurationTest.initialiserLaSessionHTTP(TypeUtilisateur.Administrateur);
            IAdminEstCreeMessage message = new AdminEstCreeMessageTest();
            ReponseRequete réponse = BusRequete.exécuter(message);

            Assert.IsTrue(réponse.Réussite);
            Assert.IsTrue((bool)réponse.Résultat);
        }

        [TestMethod]
        public void TestAdminEstCreeRequete_peutVoirSiUnAdminEstCrééDansUnEnvironnementSansAdmin()
        {
            IAdminEstCreeMessage message = new AdminEstCreeMessageTest();
            ReponseRequete réponse = BusRequete.exécuter(message);

            Assert.IsTrue(réponse.Réussite);
            Assert.IsFalse((bool)réponse.Résultat);
        }
    }

    public class AdminEstCreeMessageTest : IAdminEstCreeMessage
    {
        
    }
}
