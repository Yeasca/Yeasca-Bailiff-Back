using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yeasca.Metier;
using Yeasca.Requete;

namespace Yeasca.TestsUnitaires.Requete.Utilisateurs
{
    [TestClass]
    public class TestUtilisateurConnecteRequete
    {
        [TestInitialize]
        public void initialiser()
        {
            ConfigurationTest.initialiserLesEntrepotsMongo();
        }

        [TestMethod]
        public void TestUtilisateurConnecteRequete_peutRécupérerLUtilisateurEnCoursSiUtilisateurConnecté()
        {
            ConfigurationTest.initialiserLaSessionHTTP(TypeUtilisateur.Huissier);
            IUtilisateurConnecteMessage message = new UtilisateurConnecteMessageTest();
            ReponseRequete réponse = BusRequete.exécuter(message);

            Assert.IsTrue(réponse.Réussite);
            Assert.IsNotNull(réponse.Résultat);
        }

        [TestMethod]
        public void TestUtilisateurConnecteRequete_renvoieNullSiAucunUtilisateur()
        {
            IUtilisateurConnecteMessage message = new UtilisateurConnecteMessageTest();
            ReponseRequete réponse = BusRequete.exécuter(message);

            Assert.IsTrue(réponse.Réussite);
            Assert.IsNull(réponse.Résultat);
        }
    }

    public class UtilisateurConnecteMessageTest : IUtilisateurConnecteMessage
    {
    }
}
