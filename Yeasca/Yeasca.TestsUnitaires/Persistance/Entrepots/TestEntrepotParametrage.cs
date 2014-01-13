using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yeasca.Metier;
using Yeasca.Mongo;

namespace Yeasca.TestsUnitaires.Persistance.Entrepots
{
    [TestClass]
    public class TestEntrepotParametrage
    {
        private string _répétition1 = "Bis";
        private string _répétition2 = "Ter";
        private string _typeVoie1 = "Rue";
        private string _typeVoie2 = "Allée";

        private IFournisseur _fournisseur;
        private IEntrepotParametrage _entrepot;

        [TestInitialize]
        public void initialiser()
        {
            initialiserLeFournisseur();
            _entrepot = new EntrepotParametrage(_fournisseur);
        }

        private void initialiserLeFournisseur()
        {
            _fournisseur = new FournisseurTest();
            _fournisseur.insérer<ParametreMongo>(new ParametreRepetition() { Valeur = _répétition1 });
            _fournisseur.insérer<ParametreMongo>(new ParametreRepetition() { Valeur = _répétition2 });
            _fournisseur.insérer<ParametreMongo>(new ParametreTypeVoie() { Valeur = _typeVoie1 });
            _fournisseur.insérer<ParametreMongo>(new ParametreTypeVoie() { Valeur = _typeVoie2 });
        }

        [TestMethod]
        public void TestEntrepotParametrage_peutRécupérerLaListeDesRépétitions()
        {
            IList<string> listeDesRépétitions = _entrepot.récupérerLesRépétitionDeVoie();

            Assert.AreEqual(2, listeDesRépétitions.Count);
            Assert.AreEqual(_répétition1, listeDesRépétitions[0]);
            Assert.AreEqual(_répétition2, listeDesRépétitions[1]);
        }

        [TestMethod]
        public void TestEntrepotParametrage_peutRécupérerLaListeDesTypesDeVoie()
        {
            IList<string> listeDesTypesDeVoie = _entrepot.récupérerLesTypesDeVoie();

            Assert.AreEqual(2, listeDesTypesDeVoie.Count);
            Assert.AreEqual(_typeVoie2, listeDesTypesDeVoie[0]);
            Assert.AreEqual(_typeVoie1, listeDesTypesDeVoie[1]);
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
