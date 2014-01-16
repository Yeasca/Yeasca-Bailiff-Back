using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yeasca.Metier;
using Yeasca.Mongo;

namespace Yeasca.TestsUnitaires.Persistance.Entrepots
{
    [TestClass]
    public class TestEntrepotParametrage
    {
        private IFournisseur _fournisseur;
        private IEntrepotParametrage _entrepot;

        [TestInitialize]
        public void initialiser()
        {
            _entrepot = new EntrepotParametrage(_fournisseur);
        }

        [TestMethod]
        public void TestEntrepotParametrage_peutRécupérerLaListeDesRépétitions()
        {
            IList<string> listeDesRépétitions = _entrepot.récupérerLesRépétitionDeVoie();

            Assert.IsTrue(listeDesRépétitions.Count > 0);
        }

        [TestMethod]
        public void TestEntrepotParametrage_peutRécupérerLaListeDesTypesDeVoie()
        {
            IList<string> listeDesTypesDeVoie = _entrepot.récupérerLesTypesDeVoie();

            Assert.IsTrue(listeDesTypesDeVoie.Count > 0);
        }

        [TestMethod]
        public void TestEntrepotParametrage_peutRécupérerLesTypesDUtilisateur()
        {
            IDictionary<TypeUtilisateur, string> typesDUtilisateurs = _entrepot.récupérerLesTypesUtilisateurs();

            Assert.AreEqual(2, typesDUtilisateurs.Count);
            Assert.AreEqual(Ressource.Paramètres.LIBELLÉ_HUISSIER, typesDUtilisateurs[TypeUtilisateur.Huissier]);
            Assert.AreEqual(Ressource.Paramètres.LIBELLÉ_SECRÉTAIRE, typesDUtilisateurs[TypeUtilisateur.Secrétaire]);
        }

        [TestMethod]
        public void TestEntrepotParametrage_peutRécupérerLesCivilités()
        {
            IDictionary<Abreviation, string> civilités = _entrepot.récupérerLesCivilités();

            Assert.AreEqual(3, civilités.Count);
            Assert.AreEqual(Ressource.Paramètres.MADEMOISELLE, civilités[Abreviation.Mademoiselle]);
            Assert.AreEqual(Ressource.Paramètres.MADAME, civilités[Abreviation.Madame]);
            Assert.AreEqual(Ressource.Paramètres.MONSIEUR, civilités[Abreviation.Monsieur]);
        }
    }
}
