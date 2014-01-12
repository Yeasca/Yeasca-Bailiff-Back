using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yeasca.Metier;

namespace Yeasca.TestsUnitaires.Constats
{
    [TestClass]
    public class TestPartieAvecSociete
    {
        [TestMethod]
        public void TestPartieAvecSociete_unePartieAvecSociétéVideEstInvalide()
        {
            PartieAvecSocieteMock unePartieAvecSocieteMockVide = new PartieAvecSocieteMock();

            Assert.IsFalse(unePartieAvecSocieteMockVide.estValide());
        }

        [TestMethod]
        public void TestPartieAvecSociete_unePartieAvecSociétéValideEstValide()
        {
            PartieAvecSocieteMock unePartieAvecSocieteMockValide = new PartieAvecSocieteMock();
            unePartieAvecSocieteMockValide.Nom = "MARLEY";
            unePartieAvecSocieteMockValide.Prénom = "Bob";
            unePartieAvecSocieteMockValide.Société = ConstantesTest.SOCIÉTÉ_VALIDE;

            Assert.IsTrue(unePartieAvecSocieteMockValide.estValide());
        }

        [TestMethod]
        public void TestPartieAvecSociete_unePartieAvecDonnéesInvalidePeutRenvoyerSesMessagesDErreur()
        {
            PartieAvecSocieteMock unePartieAvecSocieteMock = new PartieAvecSocieteMock();
            unePartieAvecSocieteMock.Société.Adresse = new Adresse(null, "Bis", "rue", "Bob l'éponge", "dit le marrant", "33520", "Bruges");
            List<string> messagesDErreurs = unePartieAvecSocieteMock.obtenirLesErreurs();

            Assert.AreEqual(4, messagesDErreurs.Count);
        }

        internal class PartieAvecSocieteMock : PartieAvecSociete
        {
            public override string obtenirLaDescription()
            {
                throw new NotImplementedException();
            }
        }
    }
}
