using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yeasca.Metier;

namespace Yeasca.TestsUnitaires.Constats
{
    [TestClass]
    public class TestClientSociete
    {
        private string _nomClientParticulier = "DYLAN";
        private string _prénomClientParticulier = "Bob";

        private ClientSociete générerUnClientSociétéComplet()
        {
            ClientSociete client = new ClientSociete();
            client.Abréviation = Abreviation.Mademoiselle;
            client.Nom = _nomClientParticulier;
            client.Prénom = _prénomClientParticulier;
            client.Société = ConstantesTest.SOCIÉTÉ_VALIDE;
            return client;
        }

        [TestMethod]
        public void TestClientParticulier_unClientParticulierVideRenvoieUneDescriptionVide()
        {
            ClientSociete unClientSociete = new ClientSociete();

            Assert.IsTrue(string.IsNullOrEmpty(unClientSociete.obtenirLaDescription()));
        }

        [TestMethod]
        public void TestClientParticulier_unClientParticulierCompletRenvoieLaBonneDescription()
        {
            ClientSociete unClientSociete = générerUnClientSociétéComplet();
            string description = unClientSociete.obtenirLaDescription();
            string descriptionAttendue = générerLaDescriptionAttendue();

            Assert.AreEqual(descriptionAttendue, description);
        }

        private string générerLaDescriptionAttendue()
        {
            return string.Concat(
                "Mlle ", _nomClientParticulier, " ", _prénomClientParticulier, ", ",
                "travaillant pour l'entreprise ", ConstantesTest.SOCIÉTÉ_VALIDE.Dénomination,
                " à l'adresse ", ConstantesTest.SOCIÉTÉ_VALIDE.Adresse.enChaineAvecUnSéparateur("-"),
                ", me déclare que");
        }
    }
}
