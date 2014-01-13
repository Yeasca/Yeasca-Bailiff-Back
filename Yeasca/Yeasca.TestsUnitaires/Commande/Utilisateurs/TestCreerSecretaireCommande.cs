using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yeasca.Commande;
using Yeasca.Metier;
using Yeasca.Mongo;

namespace Yeasca.TestsUnitaires.Commande.Utilisateurs
{
    [TestClass]
    public class TestCreerSecretaireCommande
    {
        private string _prénomSecrétaire = "Scarlette";

        [TestInitialize]
        public void initialiser()
        {
            ConfigurationTest.initialiserLesEntrepotsMongo();
        }

        [TestMethod]
        public void TestCreerSecretaireCommande_créerUneSecrétaireAvecLesBonsParamètresEnAdminFonctionne()
        {
            ConfigurationTest.initialiserLaSessionHTTP(TypeUtilisateur.Administrateur);
            ICreerSecretaireMessage message = initialiserUnMessageValide();
            ReponseCommande réponse = BusCommande.exécuter(message);
            Guid idSecrétaireCréé;

            Assert.IsTrue(réponse.Réussite);
            Assert.IsTrue(Guid.TryParse(réponse.Message, out idSecrétaireCréé));

            IEntrepotUtilisateur entrepotUtilisateur = EntrepotMongo.fabriquerEntrepot<IEntrepotUtilisateur>();
            Utilisateur utilisateurRécupéré = entrepotUtilisateur.récupérer(idSecrétaireCréé);

            Assert.IsNotNull(utilisateurRécupéré);
            Assert.AreEqual(_prénomSecrétaire, utilisateurRécupéré.Profile.Prénom);

            IEntrepotProfile entrepotProfile = EntrepotMongo.fabriquerEntrepot<IEntrepotProfile>();
            Secretaire secrétaireRécupérée = entrepotProfile.récupérerLaSecrétaire(utilisateurRécupéré.Profile.Id);

            Assert.IsNotNull(secrétaireRécupérée);
            Assert.AreEqual(_prénomSecrétaire, secrétaireRécupérée.Prénom);
        }

        [TestMethod]
        public void TestCreerSecretaireCommande_créerUneSecrétaireSansLesBonsParamètresEnAdminRenvoieUneErreur()
        {
            ConfigurationTest.initialiserLaSessionHTTP(TypeUtilisateur.Administrateur);
            ICreerSecretaireMessage message = initialiserUnMessageValide();
            message.MotDePasse = "pouet";
            ReponseCommande réponse = BusCommande.exécuter(message);

            Assert.IsFalse(réponse.Réussite);
            Assert.IsTrue(réponse.Message.Contains(Ressource.Validation.MOT_DE_PASSE_LONGUEUR_MIN));
        }

        [TestMethod]
        public void TestCreerSecretaireCommande_créerUneSecrétaireAvecLesBonsParamètresSansEtreAuthentifiéCorrectementRenvoieUneErreur()
        {
            ConfigurationTest.initialiserLaSessionHTTP(TypeUtilisateur.Huissier);
            ICreerSecretaireMessage message = initialiserUnMessageValide();
            ReponseCommande réponse = BusCommande.exécuter(message);

            Assert.IsFalse(réponse.Réussite);
            Assert.AreEqual(Ressource.Commandes.ERREUR_AUTH_ADMIN, réponse.Message);
        }

        private ICreerSecretaireMessage initialiserUnMessageValide()
        {
            ICreerSecretaireMessage message = new CreerSecretaireMessageTest();
            message.Email = ConstantesTest.EMAIL_VALIDE.Valeur;
            message.MotDePasse = ConstantesTest.MOT_DE_PASSE_VALIDE.ValeurDéchiffrée;
            message.Civilité = 0;
            message.Nom = "Johanson";
            message.Prénom = _prénomSecrétaire;
            return message;
        }
    }

    public class CreerSecretaireMessageTest : ICreerSecretaireMessage
    {
        public string Email { get; set; }
        public string MotDePasse { get; set; }
        public int Civilité { get; set; }
        public string Nom { get; set; }
        public string Prénom { get; set; }
    }
}
