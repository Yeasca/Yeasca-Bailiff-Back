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
            Erreur messagesDErreur = unUtilisateurVide.obtenirLesErreurs();

            Assert.AreEqual(2, messagesDErreur.Nombre);
            Assert.AreEqual(Ressource.Validation.MOT_DE_PASSE_REQUIS, messagesDErreur.donnerLaListeDesErreurs()[0]);
            Assert.AreEqual(Ressource.Validation.EMAIL_REQUIS, messagesDErreur.donnerLaListeDesErreurs()[1]);
        }

        [TestMethod]
        public void TestUtilisateur_unUtilisateurAvecDesChampsTropLongsRenvoieLesBonsMessagesDErreur()
        {
            Utilisateur unUtilisateurAvecChampsTropLongs = new Utilisateur();
            unUtilisateurAvecChampsTropLongs.MotDePasse = new MotDePasse(ConstantesTest.CHAINE_DE_256);
            unUtilisateurAvecChampsTropLongs.Email = new Email(ConstantesTest.CHAINE_DE_256);
            Erreur messagesDErreurs = unUtilisateurAvecChampsTropLongs.obtenirLesErreurs();

            Assert.AreEqual(2, messagesDErreurs.Nombre);
            Assert.AreEqual(Ressource.Validation.MOT_DE_PASSE_LONGUEUR_MAX, messagesDErreurs.donnerLaListeDesErreurs()[0]);
            Assert.AreEqual(Ressource.Validation.EMAIL_LONGUEUR_MAX, messagesDErreurs.donnerLaListeDesErreurs()[1]);
        }
    }
}
