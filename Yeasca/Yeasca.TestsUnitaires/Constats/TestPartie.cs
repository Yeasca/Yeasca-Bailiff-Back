using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yeasca.Metier;

namespace Yeasca.TestsUnitaires.Constats
{
    [TestClass]
    public class TestPartie
    {
        [TestMethod]
        public void TestPartie_UnePartieVideEstInvalide()
        {
            PartieMock unePartieVide = new PartieMock();

            Assert.IsFalse(unePartieVide.estValide());
        }

        [TestMethod]
        public void TestPartie_unePartieValideEstValide()
        {
            PartieMock unePartieValide = new PartieMock("MARLEY", "Bob");

            Assert.IsTrue(unePartieValide.estValide());
        }

        [TestMethod]
        public void TestPartie_unePartieVideRenvoieLesBonsMessagesDErreur()
        {
            PartieMock unePartieVide = new PartieMock();
            List<string> messagesDErreurs = unePartieVide.obtenirLesErreurs();

            Assert.IsFalse(unePartieVide.estValide());
            Assert.AreEqual(2, messagesDErreurs.Count);
            Assert.AreEqual(Ressource.Validation.NOM_REQUIS, messagesDErreurs[0]);
            Assert.AreEqual(Ressource.Validation.PRÉNOM_REQUIS, messagesDErreurs[1]);
        }

        [TestMethod]
        public void TestPartie_unePartieAvecDesDonnéesTropLonguesRenvoieLesBonsMessagesDErreur()
        {
            PartieMock unePartieAvecDonnéesTropLongues = new PartieMock();
            unePartieAvecDonnéesTropLongues.Nom = ConstantesTest.CHAINE_DE_256;
            unePartieAvecDonnéesTropLongues.Prénom = ConstantesTest.CHAINE_DE_256;
            List<string> messagesDErreurs = unePartieAvecDonnéesTropLongues.obtenirLesErreurs();

            Assert.IsFalse(unePartieAvecDonnéesTropLongues.estValide());
            Assert.AreEqual(2, messagesDErreurs.Count);
            Assert.AreEqual(Ressource.Validation.NOM_LONGUEUR_MAX, messagesDErreurs[0]);
            Assert.AreEqual(Ressource.Validation.PRÉNOM_LONGUEUR_MAX, messagesDErreurs[1]);

        }
    }

    public class PartieMock : Partie
    {
        public PartieMock() : base()
        {
        }

        public PartieMock(string nom, string prénom) : base(nom, prénom)
        {
        }

        public override string obtenirLaDescription()
        {
            throw new NotImplementedException();
        }
    }
}
