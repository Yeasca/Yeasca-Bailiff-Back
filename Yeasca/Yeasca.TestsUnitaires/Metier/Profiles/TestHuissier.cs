using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yeasca.Metier;

namespace Yeasca.TestsUnitaires.Constats
{
    /// <summary>
    /// Description résumée pour TestHuissier
    /// </summary>
    [TestClass]
    public class TestHuissier
    {
        private string _nomHuissier = "MORANE";
        private string _prénomHuissier = "Bob";
        private string _nomCabinet = "poulou";

        private Huissier générerUnHuissierComplet()
        {
            Huissier unHuissier = new Huissier();
            unHuissier.Abréviation = Abreviation.Monsieur;
            unHuissier.Nom = _nomHuissier;
            unHuissier.Prénom = _prénomHuissier;
            unHuissier.DénominationEntreprise = _nomCabinet;
            unHuissier.Adresse = ConstantesTest.ADRESSE_VALIDE;
            return unHuissier;
        }

        [TestMethod]
        public void TestHuissuier_unHuissierVideRenvoieUneDescriptionVide()
        {
            Huissier unHuissier = new Huissier();
            string description = unHuissier.obtenirLaDescription();

            Assert.IsTrue(string.IsNullOrEmpty(description));
        }

        [TestMethod]
        public void TestHuissier_unHuissierRempliRenvoieLaBonneDescription()
        {
            Huissier unHuissier = générerUnHuissierComplet();
            string description = unHuissier.obtenirLaDescription();
            string descriptionAttendue = générerLaDescriptionAttendue();

            Assert.AreEqual(descriptionAttendue, description);
        }

        private string générerLaDescriptionAttendue()
        {
            return string.Concat(
                "Je, M. " , _nomHuissier, " ", _prénomHuissier, ",\n",
                "Huissier de justice du cabinet ", _nomCabinet, " domicilié à l'adresse ", ConstantesTest.ADRESSE_VALIDE.enChaineAvecUnSéparateur("-"));
        }
    }
}
