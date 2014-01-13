using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yeasca.Metier;

namespace Yeasca.TestsUnitaires.Metier.Constats
{
    [TestClass]
    public class TestSecretaire
    {
        private string _nomSecrétaire = "Johanson";
        private string _prénomSecrétaire = "Scarlett";

        [TestMethod]
        public void TestSecretaire_uneSecrétaireVideRenvoieUneDescriptionVide()
        {
            Secretaire uneSecrétaire = new Secretaire();

            Assert.AreEqual(string.Empty, uneSecrétaire.obtenirLaDescription());
        }

        [TestMethod]
        public void TestSecretaire_uneSecrétaireRemplieRenvoieLaBonneDescription()
        {
            Secretaire uneSecrétaire = new Secretaire();
            uneSecrétaire.Nom = _nomSecrétaire;
            uneSecrétaire.Prénom = _prénomSecrétaire;
            string descriptionAttendu = "Mlle Scarlett Johanson";

            Assert.AreEqual(descriptionAttendu, uneSecrétaire.obtenirLaDescription());
        }
    }
}
