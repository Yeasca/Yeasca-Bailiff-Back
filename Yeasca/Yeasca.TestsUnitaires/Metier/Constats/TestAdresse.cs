using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yeasca.Metier;

namespace Yeasca.TestsUnitaires.Constats
{
    [TestClass]
    public class TestAdresse
    {
        #region Adresse vide

        [TestMethod]
        public void TestAdresse_uneAdresseVideEstInvalide()
        {
            Adresse uneAdresseVide = new Adresse();

            Assert.IsFalse(uneAdresseVide.estValide());
        }

        [TestMethod]
        public void TestAdresse_uneAdresseVideRenvoieUneChaineVide()
        {
            Adresse uneAdresseVide = new Adresse();

            Assert.IsTrue(string.IsNullOrEmpty(uneAdresseVide.enChaine()));
        }

        [TestMethod]
        public void TestAdresse_uneAdresseVideRenvoieUneListeDerreursRemplie()
        {
            Adresse uneAdresseVide = new Adresse();

            Assert.AreNotEqual(0, uneAdresseVide.obtenirLesErreurs().Count);
        }

        #endregion

        #region Adresse Valide

        [TestMethod]
        public void TestAdresse_uneAdresseValideEstValide()
        {
            Adresse uneAdresseValide = générerUneAdresseValide();

            Assert.IsTrue(uneAdresseValide.estValide());
        }

        [TestMethod]
        public void TestAdress_uneAdresseValideRenvoieUneChaineCorrecte()
        {
            Adresse uneAdresseValide = générerUneAdresseValide();
            validerLAdresseComplète(uneAdresseValide);
            validerLAdresseSansLaRépétition(uneAdresseValide);
            validerLAdresseSansLeNuméroDeVoie(uneAdresseValide);
            validerLAdresseSansLeTypeDeVoie(uneAdresseValide);
            validerLAdresseSansLeComplémentDeVoie(uneAdresseValide);
        }

        private void validerLAdresseComplète(Adresse uneAdresseValide)
        {
            uneAdresseValide = générerUneAdresseValide();
            string chaîneAttendue = "10 Bis Rue Bob l'éponge dit le marrant 33520 Bruges";

            Assert.AreEqual(chaîneAttendue, uneAdresseValide.enChaine());
        }

        private void validerLAdresseSansLaRépétition(Adresse uneAdresseValide)
        {
            uneAdresseValide.Voie.NuméroVoie.Répétition = null;
            string chaîneAttendue = "10 Rue Bob l'éponge dit le marrant 33520 Bruges";

            Assert.AreEqual(chaîneAttendue, uneAdresseValide.enChaine());
        }

        private static void validerLAdresseSansLeNuméroDeVoie(Adresse uneAdresseValide)
        {
            uneAdresseValide.Voie.NuméroVoie.Numéro = null;
            string chaîneAttendue = "Rue Bob l'éponge dit le marrant 33520 Bruges";

            Assert.AreEqual(chaîneAttendue, uneAdresseValide.enChaine());
        }

        private static void validerLAdresseSansLeTypeDeVoie(Adresse uneAdresseValide)
        {
            uneAdresseValide.Voie.TypeVoie = null;
            string chaîneAttendue = "Bob l'éponge dit le marrant 33520 Bruges";

            Assert.AreEqual(chaîneAttendue, uneAdresseValide.enChaine());
        }

        private static void validerLAdresseSansLeComplémentDeVoie(Adresse uneAdresseValide)
        {
            uneAdresseValide.Voie.ComplémentVoie = null;
            string chaîneAttendue = "Bob l'éponge 33520 Bruges";

            Assert.AreEqual(chaîneAttendue, uneAdresseValide.enChaine());
        }

        [TestMethod]
        public void TestAdresse_uneAdresseValidePeutRetournerUneChaineCorrecteAvecUnRetourEntreLaVoieEtLaVille()
        {
            string valeurDeRetour = "\n";
            Adresse uneAdresseValide = générerUneAdresseValide();
            string chaîneAttendue = string.Concat("10 Bis Rue Bob l'éponge dit le marrant", valeurDeRetour, "33520 Bruges");

            Assert.AreEqual(chaîneAttendue, uneAdresseValide.enChaineAvecUnRetourALaLigne(valeurDeRetour));
        }

        [TestMethod]
        public void TestAdresse_uneAdresseValidePeutRetournerUneChaineCorrecteAvecUnSéparateurEntreLaVoieEtLaVille()
        {
            string valeurDuSéparateur = "-";
            Adresse uneAdresseValide = générerUneAdresseValide();
            string chaîneAttendue = string.Concat("10 Bis Rue Bob l'éponge dit le marrant ", valeurDuSéparateur, " 33520 Bruges");

            Assert.AreEqual(chaîneAttendue, uneAdresseValide.enChaineAvecUnSéparateur(valeurDuSéparateur));
        }

        [TestMethod]
        public void TestAdresse_uneAdresseValideRetourneUneListeDErreursVide()
        {
            Adresse uneAdresseValide = générerUneAdresseValide();

            Assert.AreEqual(0, uneAdresseValide.obtenirLesErreurs().Count);
        }

        private Adresse générerUneAdresseValide()
        {
            return new Adresse("10", "Bis", "Rue", "Bob l'éponge", "dit le marrant", "33520", "Bruges");
        }

        #endregion

        #region Erreurs sur Adresse

        [TestMethod]
        public void TestAdresse_uneAdresseVideRenvoieLesBonsMessagesDErreur()
        {
            Adresse uneAdresseVide = new Adresse();
            List<string> messagesDErreur = uneAdresseVide.obtenirLesErreurs();

            Assert.AreEqual(3, messagesDErreur.Count);
            Assert.AreEqual(Ressource.Validation.NOM_VOIE_REQUIS, messagesDErreur[0]);
            Assert.AreEqual(Ressource.Validation.CODE_POSTAL_REQUIS, messagesDErreur[1]);
            Assert.AreEqual(Ressource.Validation.LIBELLÉ_COMMUNE_REQUIS, messagesDErreur[2]);
        }

        [TestMethod]
        public void TestAdresse_uneAdresseAvecDesValeursTropLonguesRenvoieLesBonsMessagesDErreur()
        {
            validerLesChaînesDeRéférence();
            Adresse uneAdresseAvecValeursTropLongues = new Adresse(
                ConstantesTest.CHAINE_DE_6,
                ConstantesTest.CHAINE_DE_10,
                ConstantesTest.CHAINE_DE_256,
                ConstantesTest.CHAINE_DE_256,
                ConstantesTest.CHAINE_DE_256,
                ConstantesTest.CHAINE_DE_6,
                ConstantesTest.CHAINE_DE_256);
            List<string> messagesDErreur = uneAdresseAvecValeursTropLongues.obtenirLesErreurs();

            testerLesMessagesDErreurPourLesValeursTropLongues(messagesDErreur);
        }

        private void testerLesMessagesDErreurPourLesValeursTropLongues(List<string> messagesDErreur)
        {
            Assert.AreEqual(7, messagesDErreur.Count);
            Assert.AreEqual(Ressource.Validation.NUMÉRO_VOIE_LONGUEUR_MAX, messagesDErreur[0]);
            Assert.AreEqual(Ressource.Validation.RÉPÉTITION_VOIE_LONGUEUR_MAX, messagesDErreur[1]);
            Assert.AreEqual(Ressource.Validation.TYPE_VOIE_LONGUEUR_MAX, messagesDErreur[2]);
            Assert.AreEqual(Ressource.Validation.NOM_VOIE_LONGUEUR_MAX, messagesDErreur[3]);
            Assert.AreEqual(Ressource.Validation.COMPLÉMENT_VOIE_LONGUEUR_MAX, messagesDErreur[4]);
            Assert.AreEqual(Ressource.Validation.CODE_POSTAL_INVALIDE, messagesDErreur[5]);
            Assert.AreEqual(Ressource.Validation.LIBELLÉ_COMMUNE_LONGUEUR_MAX, messagesDErreur[6]);
        }

        private void validerLesChaînesDeRéférence()
        {
            Assert.AreEqual(6, ConstantesTest.CHAINE_DE_6.Length);
            Assert.AreEqual(10, ConstantesTest.CHAINE_DE_10.Length);
            Assert.AreEqual(256, ConstantesTest.CHAINE_DE_256.Length);
        }

        [TestMethod]
        public void TestAdresse_uneAdresseAvecDesDonnéesInvalidesRenvoieLesBonsMessagesDErreur()
        {
            Adresse uneAdresseAvecDonnéesInvalide = new Adresse("1A", "Bis", "Rue", "Bob l'éponge", "dit le marrant", "33A20", "Bruges");
            List<string> messagesDErreur = uneAdresseAvecDonnéesInvalide.obtenirLesErreurs();

            Assert.AreEqual(2, messagesDErreur.Count);
            Assert.AreEqual(Ressource.Validation.NUMÉRO_VOIE_INVALIDE, messagesDErreur[0]);
            Assert.AreEqual(Ressource.Validation.CODE_POSTAL_INVALIDE, messagesDErreur[1]);
        }

        [TestMethod]
        public void TestAdresse_uneAdresseSignaleLesValeursOptionnellesDevenuesRequises()
        {
            testerLaValeurOptionnelleRequisePourLeNuméroDeVoie();
            testLaValeurOptionnelleRequisePourLeTypeDeVoie();
        }

        private void testerLaValeurOptionnelleRequisePourLeNuméroDeVoie()
        {
            Adresse uneAdresseAvecDonnéeOptionnelleRequise = new Adresse(null, "Bis", "Rue", "Bob l'éponge", "dit le marrant", "33520", "Bruges");
            List<string> messagesDErreur = uneAdresseAvecDonnéeOptionnelleRequise.obtenirLesErreurs();

            Assert.AreEqual(1, messagesDErreur.Count);
            Assert.AreEqual(Ressource.Validation.NUMÉRO_VOIE_REQUIS, messagesDErreur[0]);
        }

        private void testLaValeurOptionnelleRequisePourLeTypeDeVoie()
        {
            Adresse uneAdresseAvecDonnéeOptionnelleRequise = new Adresse("10", "Bis", null, "Bob l'éponge", "dit le marrant", "33520", "Bruges");
            List<string> messagesDErreur = uneAdresseAvecDonnéeOptionnelleRequise.obtenirLesErreurs();

            Assert.AreEqual(1, messagesDErreur.Count);
            Assert.AreEqual(Ressource.Validation.TYPE_VOIE_REQUIS, messagesDErreur[0]);
        }

        #endregion
    }
}
