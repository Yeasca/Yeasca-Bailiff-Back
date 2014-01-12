using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yeasca.Metier;

namespace Yeasca.TestsUnitaires.Persistance
{
    [TestClass]
    public class TestFournisseur
    {
        [TestMethod]
        public void TestFournisseur_peutAjouter()
        {
            IFournisseur fournisseur = new FournisseurTest();
            int nombreDElémentsInitial = fournisseur.obtenirLaCollection<TestAgregat>().Count();
            TestAgregat agrégat = new TestAgregat();
            bool ajout = fournisseur.insérer<TestAgregat>(agrégat);
            int nombreDElémentsFinal = fournisseur.obtenirLaCollection<TestAgregat>().Count();

            Assert.IsTrue(ajout);
            Assert.IsNotNull(agrégat.Id);
            Assert.AreEqual(nombreDElémentsInitial + 1, nombreDElémentsFinal);
        }

        [TestMethod]
        public void TestFournisseur_peutRécupérer()
        {
            string valeurTest = "Pouet";
            IFournisseur fournisseur = new FournisseurTest();
            TestAgregat agrégat = new TestAgregat() {ValeurTest = valeurTest};
            fournisseur.insérer<TestAgregat>(agrégat);
            TestAgregat agrégatRécupéré = fournisseur.obtenirLaCollection<TestAgregat>().SingleOrDefault(x => x.Id == agrégat.Id);

            Assert.IsNotNull(agrégatRécupéré);
            Assert.AreEqual(valeurTest, agrégatRécupéré.ValeurTest);
        }

        [TestMethod]
        public void TestFournisseur_peutModifier()
        {
            string valeurTest = "Pouet";
            IFournisseur fournisseur = new FournisseurTest();
            TestAgregat agrégat = new TestAgregat();
            fournisseur.insérer<TestAgregat>(agrégat);
            TestAgregat agrégatRécupéré = fournisseur.obtenirLaCollection<TestAgregat>().SingleOrDefault(x => x.Id == agrégat.Id);
            agrégatRécupéré.ValeurTest = valeurTest;
            int nombreDElémentsInitial = fournisseur.obtenirLaCollection<TestAgregat>().Count();
            fournisseur.modifier<TestAgregat>(agrégatRécupéré);
            int nombreDElémentsFinal = fournisseur.obtenirLaCollection<TestAgregat>().Count();
            TestAgregat agrégatModifié = fournisseur.obtenirLaCollection<TestAgregat>().SingleOrDefault(x => x.Id == agrégat.Id);

            Assert.IsNotNull(agrégatModifié);
            Assert.AreEqual(valeurTest, agrégatModifié.ValeurTest);
            Assert.AreEqual(nombreDElémentsInitial, nombreDElémentsFinal);
        }
    }

    internal class TestAgregat : IAgregat
    {
        public Guid Id { get; set; }
        public string ValeurTest { get; set; }
    }
}
