﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yeasca.Commande;
using Yeasca.Metier;

namespace Yeasca.TestsUnitaires.Commande.Utilisateurs
{
    [TestClass]
    public class TestModifierHuissierCommande
    {
        [TestInitialize]
        public void initialiser()
        {
            ConfigurationTest.initialiserLesEntrepotsMongo();
        }

        [TestMethod]
        public void TestModifierHuissierCommande_modifierUnHuissierAvecLesBonsParamètresEnAdminRéussit()
        {
            ConfigurationTest.initialiserLaSessionHTTP(TypeUtilisateur.Administrateur);
            Utilisateur huissier = ConfigurationTest.créerUnHuissier();
            IModifierHuissierMessage message = initialiserUnMessageValide(huissier.Id.ToString());
            ReponseCommande réponse = BusCommande.exécuter(message);

            Assert.IsTrue(réponse.Réussite);
        }

        [TestMethod]
        public void TestModifierHuissierCommande_modifierUnHuissierDeMauvaisParamètresEnAdminEchoue()
        {
            ConfigurationTest.initialiserLaSessionHTTP(TypeUtilisateur.Administrateur);
            Utilisateur huissier = ConfigurationTest.créerUnHuissier();
            IModifierHuissierMessage message = initialiserUnMessageValide(huissier.Id.ToString());
            message.MotDePasse = "pouet";
            ReponseCommande réponse = BusCommande.exécuter(message);

            Assert.IsFalse(réponse.Réussite);
            Assert.IsTrue(réponse.Message.Contains(Ressource.Validation.MOT_DE_PASSE_LONGUEUR_MIN));
        }

        [TestMethod]
        public void TestModifierHuissierCommande_modifierUnHuissierAvecLesBonsParamètresSansEtreAdminEchoue()
        {
            ConfigurationTest.initialiserLaSessionHTTP(TypeUtilisateur.Huissier);
            Utilisateur huissier = ConfigurationTest.créerUnHuissier();
            IModifierHuissierMessage message = initialiserUnMessageValide(huissier.Id.ToString());
            ReponseCommande réponse = BusCommande.exécuter(message);

            Assert.IsFalse(réponse.Réussite);
            Assert.AreEqual(Ressource.Commandes.ERREUR_AUTH_ADMIN, réponse.Message);
        }

        private IModifierHuissierMessage initialiserUnMessageValide(string idHuissier)
        {
            IModifierHuissierMessage message = new ModifierHuissierMessageTest();
            message.IdHuissier = idHuissier;
            message.Email = ConstantesTest.EMAIL_VALIDE.Valeur;
            message.MotDePasse = ConstantesTest.MOT_DE_PASSE_VALIDE.ValeurDéchiffrée;
            message.Civilité = 0;
            message.Nom = "Johanson";
            message.Prénom = "Scarlett";
            return message;
        }
    }


    public class ModifierHuissierMessageTest : IModifierHuissierMessage
    {
        public string IdHuissier { get; set; }
        public string Email { get; set; }
        public string MotDePasse { get; set; }
        public int Civilité { get; set; }
        public string Nom { get; set; }
        public string Prénom { get; set; }
    }
}
