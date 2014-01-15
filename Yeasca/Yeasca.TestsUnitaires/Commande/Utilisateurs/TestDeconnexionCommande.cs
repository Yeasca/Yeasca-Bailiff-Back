using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yeasca.Commande;
using Yeasca.Metier;

namespace Yeasca.TestsUnitaires.Commande.Utilisateurs
{
    [TestClass]
    public class TestDeconnexionCommande
    {
        [TestInitialize]
        public void initialiser()
        {
            ConfigurationTest.initialiserLesEntrepotsMongo();
        }

        [TestMethod]
        public void TestDeconnexionCommande_peutDéconnecterUnUtilisateur()
        {
            ConfigurationTest.initialiserLaSessionHTTP(TypeUtilisateur.Huissier);
            IDeconnexionMessage message = new DeconnexionMessageTest();
            ReponseCommande réponse = BusCommande.exécuter(message);
            Utilisateur utilisateurConnecté = Utilisateur.chargerDepuisLaSession();

            Assert.IsTrue(réponse.Réussite);
            Assert.IsNull(utilisateurConnecté);
        }
    }

    public class DeconnexionMessageTest : IDeconnexionMessage
    {
        
    }
}
