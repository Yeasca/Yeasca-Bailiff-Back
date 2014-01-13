using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yeasca.Metier;
using Yeasca.Mongo;

namespace Yeasca.TestsUnitaires.Persistance.Entrepots
{
    [TestClass]
    public class TestEntrepotJeton
    {
        private IEntrepotJeton _entrepot;
        private IFournisseur _fournisseur;

        [TestInitialize]
        public void initialiser()
        {
            _fournisseur = new FournisseurTest();
            _entrepot = new EntrepotJeton(_fournisseur);
        }

        [TestMethod]
        public void TestEntrepotJeton_peutAjouterUnJeton()
        {
            Jeton unJeton = new Jeton();
            int nombreDElémentsInitial = _fournisseur.obtenirLaCollection<Jeton>().Count();
            bool ajout = _entrepot.ajouter(unJeton);
            int nombreDElémentsFinal = _fournisseur.obtenirLaCollection<Jeton>().Count();

            Assert.IsTrue(ajout);
            Assert.AreEqual(nombreDElémentsInitial + 1, nombreDElémentsFinal);
        }

        [TestMethod]
        public void TestEntrepotJeton_peutVérifierSiUnJetonAEtéAjouté()
        {
            string clé1 = "pouet";
            string clé2 = "pouetpouet";
            Jeton jeton1 = new Jeton(Jeton.générerUnJeton(clé1));
            Jeton jeton2 = new Jeton(Jeton.générerUnJeton(clé2));
            _entrepot.ajouter(jeton1);

            Assert.IsTrue(_entrepot.aEtéUtilisé(jeton1.Valeur));
            Assert.IsFalse(_entrepot.aEtéUtilisé(jeton2.Valeur));
        }
    }
}
