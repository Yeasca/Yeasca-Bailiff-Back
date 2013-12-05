using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yeasca;

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
        private Adresse _adresse = new Adresse("10", "Bis", "rue", "Bol l'éponge", "dit le marrant", "33520", "Bruges");

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

        private Huissier générerUnHuissierComplet()
        {
            Huissier unHuissier = new Huissier();
            unHuissier.Nom = _nomHuissier;
            unHuissier.Prénom = _prénomHuissier;
            return unHuissier;
        }

        private string générerLaDescriptionAttendue()
        {
        //    return string.Concat(
        //        "Je ", _nomHuissier, " ", _prénomHuissier, ",\n",
        //        "Huissier de justice domicilié ", _adresse.enChaineAvecUnSéparateur("-"));
            return null;
        }
        //TODO : inclure la société quand testée
    }
}
