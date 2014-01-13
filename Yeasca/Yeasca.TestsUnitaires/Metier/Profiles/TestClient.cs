using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yeasca.Metier;

namespace Yeasca.TestsUnitaires.Constats
{
    [TestClass]
    public class TestClient
    {
        private string _nomClientParticulier = "DYLAN";
        private string _prénomClientParticulier = "Bob";

        private Client générerUnClientParticulierComplet()
        {
            Client client = new Client();
            client.Abréviation = Abreviation.Madame;
            client.Nom = _nomClientParticulier;
            client.Prénom = _prénomClientParticulier;
            client.Adresse = ConstantesTest.ADRESSE_VALIDE;
            return client;
        }

        [TestMethod]
        public void TestClient_unClientParticulierVideRenvoieUneDescriptionVide()
        {
            Client unClientParticulier = new Client();

            Assert.IsTrue(string.IsNullOrEmpty(unClientParticulier.obtenirLaDescription()));
        }

        [TestMethod]
        public void TestClient_unClientParticulierCompletRenvoieLaBonneDescription()
        {
            Client unClientParticulier = générerUnClientParticulierComplet();
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
