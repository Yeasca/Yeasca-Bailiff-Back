using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yeasca.Metier;

namespace Yeasca.TestsUnitaires.Constats
{
    [TestClass]
    public class TestClientParticulier
    {
        private string _nomClientParticulier = "DYLAN";
        private string _prénomClientParticulier = "Bob";

        private ClientParticulier générerUnClientParticulierComplet()
        {
            ClientParticulier client = new ClientParticulier();
            client.Abréviation = Abreviation.Madame;
            client.Nom = _nomClientParticulier;
            client.Prénom = _prénomClientParticulier;
            client.Adresse = ConstantesTest.ADRESSE_VALIDE;
            return client;
        }

        [TestMethod]
        public void TestClientParticulier_unClientParticulierVideRenvoieUneDescriptionVide()
        {
            ClientParticulier unClientParticulier = new ClientParticulier();

            Assert.IsTrue(string.IsNullOrEmpty(unClientParticulier.obtenirLaDescription()));
        }

        [TestMethod]
        public void TestClientParticulier_unClientParticulierCompletRenvoieLaBonneDescription()
        {
            ClientParticulier unClientParticulier = générerUnClientParticulierComplet();
            string description = unClientParticulier.obtenirLaDescription();
            string descriptionAttendue = générerLaDescriptionAttendue();

            Assert.AreEqual(descriptionAttendue, description);
        }

        private string générerLaDescriptionAttendue()
        {
            return string.Concat(
                "Mme ", _nomClientParticulier, " ", _prénomClientParticulier, ", ",
                "résidant à l'adresse ", ConstantesTest.ADRESSE_VALIDE.enChaineAvecUnSéparateur("-"),
                ", me déclare que");
        }
    }
}
