using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yeasca.Metier;

namespace Yeasca.TestsUnitaires.Utilisateurs
{
    [TestClass]
    public class TestUtilisateur
    {
        [TestMethod]
        public void TestUtilisateur_unUtilisateurVideEstInvalide()
        {
            Utilisateur unUtilisateurVide = new Utilisateur();

            Assert.IsFalse(unUtilisateurVide.estValide());
        }

        [TestMethod]
        public void TestUtilisateur_unUtilisateurValideEstValide()
        {
            Utilisateur unUtilisateurValide = ConstantesTest.UTILISATEUR_VALIDE;

            Assert.IsTrue(unUtilisateurValide.estValide());
        }

        [TestMethod]
        public void TestUtilisateur_unUtilisateurVideRenvoieLesBonsMessagesDErreur()
        {
            Utilisateur unUtilisateurVide = new Utilisateur();
            List<string> messagesDErreur = unUtilisateurVide.obtenirLesErreurs();

            Assert.AreEqual(3, messagesDErreur.Count);
            Assert.AreEqual(Ressource.Validation.LOGIN_REQUIS, messagesDErreur[0]);
            Assert.AreEqual(Ressource.Validation.MOT_DE_PASSE_REQUIS, messagesDErreur[1]);
            Assert.AreEqual(Ressource.Validation.EMAIL_REQUIS, messagesDErreur[2]);
        }

        [TestMethod]
        public void TestUtilisateur_unUtilisateurAvecDesChampsTropLongsRenvoieLesBonsMessagesDErreur()
        {
            Utilisateur unUtilisateurAvecChampsTropLongs = new Utilisateur();
            unUtilisateurAvecChampsTropLongs.Login = ConstantesTest.CHAINE_DE_256;
            unUtilisateurAvecChampsTropLongs.MotDePasse = new MotDePasse(ConstantesTest.CHAINE_DE_256);
            unUtilisateurAvecChampsTropLongs.Email = new Email(ConstantesTest.CHAINE_DE_256);
            List<string> messagesDErreurs = unUtilisateurAvecChampsTropLongs.obtenirLesErreurs();

            Assert.AreEqual(3, messagesDErreurs.Count);
            Assert.AreEqual(Ressource.Validation.LOGIN_LONGUEUR_MAX, messagesDErreurs[0]);
            Assert.AreEqual(Ressource.Validation.MOT_DE_PASSE_LONGUEUR_MAX, messagesDErreurs[1]);
            Assert.AreEqual(Ressource.Validation.EMAIL_LONGUEUR_MAX, messagesDErreurs[2]);
        }
    }
}
