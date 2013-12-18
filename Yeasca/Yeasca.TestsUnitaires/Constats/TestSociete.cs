using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yeasca.Metier;

namespace Yeasca.TestsUnitaires.Constats
{
    [TestClass]
    public class TestSociete
    {
        #region Validation

        [TestMethod]
        public void TestSociete_uneSociétéVideEstInvalide()
        {
            Societe sociétéVide = new Societe();
            
            Assert.IsFalse(sociétéVide.estValide());
        }

        [TestMethod]
        public void TestSociete_uneSociétéValideEstValide()
        {
            Societe sociétéValide = ConstantesTest.SOCIÉTÉ_VALIDE;

            Assert.IsTrue(sociétéValide.estValide());
        }

        #endregion

        #region Erreurs sur société

        [TestMethod]
        public void TestSociete_uneSociétéVideRenvoieLesBonsMessagesDErreur()
        {
            Societe sociétéVide = new Societe();
            List<string> messagesDerreurs = sociétéVide.obtenirLesErreurs();

            Assert.AreEqual(4, messagesDerreurs.Count);
            Assert.AreEqual(Ressource.Validation.DÉNOMINATION_SOCIÉTÉ_REQUISE, messagesDerreurs[0]);
        }

        [TestMethod]
        public void TestSociete_uneSociétéAvecDesDonnéesInvalidesRenvoieLesBonsMessagesDErreur()
        {
            Societe sociétéAvecDonnéesInvalides = new Societe("poulou");
            sociétéAvecDonnéesInvalides.NuméroSIRET.Valeur = "123";
            List<string> messagesDerreurs = sociétéAvecDonnéesInvalides.obtenirLesErreurs();

            Assert.AreEqual(4, messagesDerreurs.Count);
            Assert.AreEqual(Ressource.Validation.NUMÉRO_SIRET_INVALIDE, messagesDerreurs[0]);
        }

        [TestMethod]
        public void TestSociete_uneSociétéAvecDesDonnéesTropLonguesRenvoieLesBonsMessagesDErreur()
        {
            Societe sociétéAvecDonnéesTropLongues = new Societe(ConstantesTest.CHAINE_DE_256);
            List<string> messagesDerreurs = sociétéAvecDonnéesTropLongues.obtenirLesErreurs();

            Assert.AreEqual(4, messagesDerreurs.Count);
            Assert.AreEqual(Ressource.Validation.DÉNOMINATION_SOCIÉTÉ_LONGUEUR_MAX, messagesDerreurs[0]);
        }

        #endregion
    }
}
