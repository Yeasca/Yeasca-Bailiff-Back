using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yeasca.Metier;
using Yeasca.Mongo;
using Yeasca.Requete;

namespace Yeasca.TestsUnitaires.Requete.Utilisateurs
{
    [TestClass]
    public class TestAuthentificationRequete
    {
        private string _email = ConstantesTest.EMAIL_VALIDE.Valeur;
        private string _motDePasse = ConstantesTest.MOT_DE_PASSE_VALIDE.ValeurDéchiffrée;
        private Abreviation _civilité = Abreviation.Monsieur;
        private string _nom = "Pouet";
        private string _prénom = "Poulou";

        [TestInitialize]
        public void initialiser()
        {
            ConfigurationTest.initialiserLesEntrepotsMongo();
            initialiserUnCompte();
            ConfigurationTest.initialiserLaSessionHTTP(TypeUtilisateur.Inconnu);
        }

        private void initialiserUnCompte()
        {
            IEntrepotProfile entrepotProfile = EntrepotMongo.fabriquerEntrepot<IEntrepotProfile>();
            Huissier huissier = new Huissier()
            {
                Abréviation = Abreviation.Monsieur,
                Nom = _nom,
                Prénom = _prénom,
                Adresse = ConstantesTest.ADRESSE_VALIDE
            };
            entrepotProfile.ajouter(huissier);
            IEntrepotUtilisateur entrepotUtilisateur = EntrepotMongo.fabriquerEntrepot<IEntrepotUtilisateur>();
            Utilisateur compte = new Utilisateur();
            compte.Email.Valeur = _email;
            compte.MotDePasse.ValeurDéchiffrée = _motDePasse;
            compte.Profile = huissier;
            compte.TypeUtilisateur = TypeUtilisateur.Huissier;
            entrepotUtilisateur.ajouter(compte);
        }

        [TestMethod]
        public void TestAuthentificationRequete_peutAuthentifier()
        {
            IAuthentificationMessage message = new AuthentificationMessageTest();
            message.Email = _email;
            message.MotDePasse = _motDePasse;
            ReponseRequete réponse = BusRequete.exécuter(message);

            Assert.IsTrue(réponse.Réussite);

            AuthentificationReponse résultat = réponse.Résultat as AuthentificationReponse;
            
            Assert.IsNotNull(résultat);
            Assert.AreEqual(_nom, résultat.Nom);
            Assert.AreEqual(_prénom, résultat.Prénom);
        }
    }

    public class AuthentificationMessageTest : IAuthentificationMessage
    {
        public string Email { get; set; }
        public string MotDePasse { get; set; }
    }
}
