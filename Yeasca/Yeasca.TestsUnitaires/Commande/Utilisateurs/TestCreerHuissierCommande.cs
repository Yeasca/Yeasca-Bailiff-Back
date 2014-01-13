using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yeasca.Commande;
using Yeasca.Metier;
using Yeasca.Mongo;

namespace Yeasca.TestsUnitaires.Commande.Utilisateurs
{
    [TestClass]
    public class TestCreerHuissierCommande
    {
        private string _prénomHuissier = "Scarlette";

        [TestInitialize]
        public void initialiser()
        {
            ConfigurationTest.initialiserLesEntrepotsMongo();
        }

        [TestMethod]
        public void TestCreerHuissierCommande_créerUnHuisserAvecLesBonsParamètresEnAdminFonctionne()
        {
            ConfigurationTest.initialiserLaSessionHTTP(TypeUtilisateur.Administrateur);
            ICreerHuissierMessage message = initialiserUnMessageValide();
            ReponseCommande réponse = BusCommande.exécuter(message);
            Guid idHuissierCréé;

            Assert.IsTrue(réponse.Réussite);
            Assert.IsTrue(Guid.TryParse(réponse.Message, out idHuissierCréé));

            IEntrepotUtilisateur entrepotUtilisateur = EntrepotMongo.fabriquerEntrepot<IEntrepotUtilisateur>();
            Utilisateur utilisateurRécupéré = entrepotUtilisateur.récupérer(idHuissierCréé);

            Assert.IsNotNull(utilisateurRécupéré);
            Assert.AreEqual(_prénomHuissier, utilisateurRécupéré.Profile.Prénom);

            IEntrepotProfile entrepotProfile = EntrepotMongo.fabriquerEntrepot<IEntrepotProfile>();
            Huissier huissierRécupéré = entrepotProfile.récupérerLHuissier(utilisateurRécupéré.Profile.Id);

            Assert.IsNotNull(huissierRécupéré);
            Assert.AreEqual(_prénomHuissier, huissierRécupéré.Prénom);
        }

        [TestMethod]
        public void TestCreerHuissierCommande_créerUnHuissierSansLesBonsParamètresEnAdminRenvoieUneErreur()
        {
            ConfigurationTest.initialiserLaSessionHTTP(TypeUtilisateur.Administrateur);
            ICreerHuissierMessage message = initialiserUnMessageValide();
            message.MotDePasse = "pouet";
            ReponseCommande réponse = BusCommande.exécuter(message);

            Assert.IsFalse(réponse.Réussite);
            Assert.IsTrue(réponse.Message.Contains(Ressource.Validation.MOT_DE_PASSE_LONGUEUR_MIN));
        }

        [TestMethod]
        public void TestCreerHuissierCommande_créerUnHuissierAvecLesBonsParamètresSansEtreAuthentifiéCorrectementRenvoieUneErreur()
        {
            ConfigurationTest.initialiserLaSessionHTTP(TypeUtilisateur.Huissier);
            ICreerHuissierMessage message = initialiserUnMessageValide();
            ReponseCommande réponse = BusCommande.exécuter(message);

            Assert.IsFalse(réponse.Réussite);
            Assert.AreEqual(Ressource.Commandes.ERREUR_AUTH_ADMIN, réponse.Message);
        }

        private ICreerHuissierMessage initialiserUnMessageValide()
        {
            ICreerHuissierMessage message = new CreerHuissierMessageTest();
            message.Email = ConstantesTest.EMAIL_VALIDE.Valeur;
            message.MotDePasse = ConstantesTest.MOT_DE_PASSE_VALIDE.ValeurDéchiffrée;
            message.Civilité = 0;
            message.Nom = "Johanson";
            message.Prénom = _prénomHuissier;
            return message;
        }
    }

    public class CreerHuissierMessageTest : ICreerHuissierMessage
    {
        public string Email { get; set; }
        public string MotDePasse { get; set; }
        public int Civilité { get; set; }
        public string Nom { get; set; }
        public string Prénom { get; set; }
    }
}
