using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yeasca.Metier;
using Yeasca.Mongo;

namespace Yeasca.TestsUnitaires.Persistance.Entrepots
{
    [TestClass]
    public class TestEntrepotProfile
    {
        private string _nomHuissier = "Dylan";
        private string _nomClient = "Sinclar";
        private string _nomSecrétaire = "Johanson";

        private IFournisseur _fournisseur;
        private IEntrepotProfile _entrepot;

        [TestInitialize]
        public void initialiser()
        {
            initialiserLeFournisseur();
            _entrepot = new EntrepotProfile(_fournisseur);
        }

        private void initialiserLeFournisseur()
        {
            _fournisseur = new FournisseurTest();
            _fournisseur.insérer<Profile>(new Huissier() { Nom = _nomHuissier });
            _fournisseur.insérer<Profile>(new Huissier() );
            _fournisseur.insérer<Profile>(new Client() { Nom = _nomClient });
            _fournisseur.insérer<Profile>(new Client());
            _fournisseur.insérer<Profile>(new Secretaire() { Nom = _nomSecrétaire });
            _fournisseur.insérer<Profile>(new Secretaire());
        }

        [TestMethod]
        public void TestEntrepotProfile_peutAjouterUnePartie()
        {
            Profile unePartie = new Client();
            int nombreDElémentsInitial = _fournisseur.obtenirLaCollection<Profile>().Count();
            bool ajout = _entrepot.ajouter(unePartie);
            int nombreDElémentsFinal = _fournisseur.obtenirLaCollection<Profile>().Count();

            Assert.IsTrue(ajout);
            Assert.AreEqual(nombreDElémentsInitial + 1, nombreDElémentsFinal);
        }

        [TestMethod]
        public void TestEntrepotProfile_peutRécupérerUnClient()
        {
            Profile unClient = new Client();
            _entrepot.ajouter(unClient);
            Profile clientRécupéré = _entrepot.récupérerLeClient(unClient.Id);

            Assert.IsNotNull(clientRécupéré);
        }

        [TestMethod]
        public void TestEntrepotProfile_peutRécupérerUnHuissier()
        {
            Huissier unHuissier = new Huissier();
            _entrepot.ajouter(unHuissier);
            Huissier huissierRécupéré = _entrepot.récupérerLHuissier(unHuissier.Id);

            Assert.IsNotNull(huissierRécupéré);
        }

        [TestMethod]
        public void TestEntrepotProfile_peutRécupérerUneSecrétaire()
        {
            Secretaire uneSecrétaire = new Secretaire();
            _entrepot.ajouter(uneSecrétaire);
            Secretaire secrétaireRécupérée = _entrepot.récupérerLaSecrétaire(uneSecrétaire.Id);

            Assert.IsNotNull(secrétaireRécupérée);
        }

        [TestMethod]
        public void TestEntrepotProfile_peutRechercheUnClient()
        {
            RechercheGlobaleTest recherche1 = new RechercheGlobaleTest();
            RechercheGlobaleTest recherche2 = new RechercheGlobaleTest() { Texte = "sinc"};
            IList<Client> résultats1 = _entrepot.récupérerLaListeDesClients(recherche1);
            IList<Client> résultats2 = _entrepot.récupérerLaListeDesClients(recherche2);

            Assert.AreEqual(2, résultats1.Count);
            Assert.AreEqual(1, résultats2.Count);
            Assert.AreEqual(_nomClient, résultats2[0].Nom);
        }

        [TestMethod]
        public void TestEntrepotProfile_peutRechercheUnHuissier()
        {
            RechercheGlobaleTest recherche1 = new RechercheGlobaleTest();
            RechercheGlobaleTest recherche2 = new RechercheGlobaleTest() { Texte = "dyl" };
            IList<Huissier> résultats1 = _entrepot.récupérerLaListeDesHuissier(recherche1);
            IList<Huissier> résultats2 = _entrepot.récupérerLaListeDesHuissier(recherche2);

            Assert.AreEqual(2, résultats1.Count);
            Assert.AreEqual(1, résultats2.Count);
            Assert.AreEqual(_nomHuissier, résultats2[0].Nom);
        }

        [TestMethod]
        public void TestEntrepotProfile_peutRechercheUneSecrétaire()
        {
            RechercheGlobaleTest recherche1 = new RechercheGlobaleTest();
            RechercheGlobaleTest recherche2 = new RechercheGlobaleTest() { Texte = "joh" };
            IList<Secretaire> résultats1 = _entrepot.récupérerLaListeDesSecrétaires(recherche1);
            IList<Secretaire> résultats2 = _entrepot.récupérerLaListeDesSecrétaires(recherche2);

            Assert.AreEqual(2, résultats1.Count);
            Assert.AreEqual(1, résultats2.Count);
            Assert.AreEqual(_nomSecrétaire, résultats2[0].Nom);
        }
    }
}
