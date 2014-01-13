using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yeasca.Metier;
using Yeasca.Mongo;

namespace Yeasca.TestsUnitaires.Persistance.Entrepots
{
    [TestClass]
    public class TestEntrepotConstat
    {
        private Client _client1 = new Client() { Nom = "Léponge", Prénom = "Mortelle"};
        private Client _client2 = new Client() { Nom = "Morane", Prénom = "Bob" };
        private DateTime _date1 = new DateTime(2014, 1, 5);
        private DateTime _date2 = new DateTime(2014, 2, 10);
        private DateTime _date3 = new DateTime(2014, 3, 15);

        private IFournisseur _fournisseur;
        private IEntrepotConstat _entrepot;

        [TestInitialize]
        public void initialiserLEntrepot()
        {
            initialiserLeFournisseur();
            _entrepot = new EntrepotConstat(_fournisseur);
        }

        private void initialiserLeFournisseur()
        {
            _fournisseur = new FournisseurTest();
            _fournisseur.insérer<Profile>(_client1);
            _fournisseur.insérer<Profile>(_client2);
            Constat constat1 = new Constat() {Date = _date1, Client = _client1};
            Constat constat2 = new Constat() {Date = _date2, Client = _client2};
            Constat constat3 = new Constat() {Date = _date3, Client = _client1};
            _fournisseur.insérer<Constat>(constat1);
            _fournisseur.insérer<Constat>(constat2);
            _fournisseur.insérer<Constat>(constat3);
        }


        [TestMethod]
        public void TestEntrepotConstat_peutAjouterUnConstat()
        {
            Constat unConstat = new Constat();
            int nombreDElémentsInitial = _fournisseur.obtenirLaCollection<Constat>().Count();
            bool ajout = _entrepot.ajouter(unConstat);
            int nombreDElémentsFinal = _fournisseur.obtenirLaCollection<Constat>().Count();

            Assert.IsTrue(ajout);
            Assert.AreEqual(nombreDElémentsInitial + 1, nombreDElémentsFinal);
        }

        [TestMethod]
        public void TestEntrepotConstat_peutRécupérerUnConstat()
        {
            Constat unConstat = new Constat();
            _entrepot.ajouter(unConstat);
            Constat constatRécupéré = _entrepot.récupérerLeConstat(unConstat.Id);

            Assert.IsNotNull(constatRécupéré);
        }

        [TestMethod]
        public void TestEntrepotConstat_peutModifierUnConstat()
        {
            DateTime dateTest = new DateTime(2012, 1, 1);
            Constat unConstat = new Constat();
            _entrepot.ajouter(unConstat);
            unConstat.Date = dateTest;
            _entrepot.modifier(unConstat);
            Constat constatRécupéré = _entrepot.récupérerLeConstat(unConstat.Id);

            Assert.AreEqual(dateTest, constatRécupéré.Date);
        }
        
        [TestMethod]
        public void TestEntrepotConstat_peutRechercherUnConstatAvecUneRechercheSpécifiqueSansCritères()
        {
            RechercheConstatTest recherche = new RechercheConstatTest();
            IList<Constat> constats = _entrepot.récupérerLaListeDesConstats(recherche);

            Assert.AreEqual(3, constats.Count);
        }
        
        [TestMethod]
        public void TestEntrepotConstat_peutRechercherUnConstatAvecUneRechercheSpécifiqueSurLeNomClient()
        {
            string nomClient = "Léponge";
            RechercheConstatTest recherche = new RechercheConstatTest();
            recherche.NomClient = nomClient;
            IList<Constat> constats = _entrepot.récupérerLaListeDesConstats(recherche);

            Assert.AreEqual(2, constats.Count);
            Assert.IsTrue(constats.All(constat => constat.Client.Nom == nomClient));
        }
        
        [TestMethod]
        public void TestEntrepotConstat_peutRechercherUnConstatAvecUneRechercheSpécifiqueSurLaDateDébut()
        {
            RechercheConstatTest recherche = new RechercheConstatTest();
            recherche.DateDébut = _date2;
            IList<Constat> constats = _entrepot.récupérerLaListeDesConstats(recherche);

            Assert.AreEqual(2, constats.Count);
            Assert.AreEqual(_date3, constats[0].Date);
            Assert.AreEqual(_date2, constats[1].Date);
        }
        
        [TestMethod]
        public void TestEntrepotConstat_peutRechercherUnConstatAvecUneRechercheSpécifiqueSurLaDateFin()
        {
            RechercheConstatTest recherche = new RechercheConstatTest();
            recherche.DateFin = _date2;
            IList<Constat> constats = _entrepot.récupérerLaListeDesConstats(recherche);

            Assert.AreEqual(2, constats.Count);
            Assert.AreEqual(_date2, constats[0].Date);
            Assert.AreEqual(_date1, constats[1].Date);
        }
        
        [TestMethod]
        public void TestEntrepotConstat_peutRechercherUnConstatAvecUneRechercheSpécifiqueSurTousLesCritères()
        {
            string nomClient = "Léponge";
            RechercheConstatTest recherche;
            recherche = new RechercheConstatTest();
            recherche.NomClient = nomClient;
            recherche.DateDébut = _date2;
            recherche.DateFin = _date3;
            IList<Constat> constats = _entrepot.récupérerLaListeDesConstats(recherche);

            Assert.AreEqual(1, constats.Count);
            Assert.AreEqual(nomClient, constats[0].Client.Nom);
        }

        [TestMethod]
        public void TestEntrepotConstat_peutRechercheUnConstatAvecUneRechercheGénérale()
        {
            string texteRecherche2 = "épon";
            string texteRecherche3 = "mor";
            RechercheGlobaleTest recherche1 = new RechercheGlobaleTest();
            RechercheGlobaleTest recherche2 = new RechercheGlobaleTest() { Texte = texteRecherche2 };
            RechercheGlobaleTest recherche3 = new RechercheGlobaleTest() { Texte = texteRecherche3 };
            IList<Constat> résultats1 = _entrepot.récupérerLaListeDesConstats(recherche1);
            IList<Constat> résultats2 = _entrepot.récupérerLaListeDesConstats(recherche2);
            IList<Constat> résultats3 = _entrepot.récupérerLaListeDesConstats(recherche3);

            Assert.AreEqual(3, résultats1.Count);
            Assert.AreEqual(2, résultats2.Count);
            Assert.IsTrue(résultats2.All(constat => constat.Client.Nom.Contains(texteRecherche2) || constat.Client.Prénom.Contains(texteRecherche2)));
            Assert.AreEqual(3, résultats3.Count);
            Assert.IsTrue(résultats3.All(constat => constat.Client.Nom.IndexOf(texteRecherche3, StringComparison.OrdinalIgnoreCase) >= 0 || constat.Client.Prénom.IndexOf(texteRecherche3, StringComparison.OrdinalIgnoreCase) >= 0));
        }

        [TestMethod]
        public void TestEntrepotConstat_peutLesConstatsDUnClient()
        {
            IList<Constat> résultats = _entrepot.récupérerLaListeDesConstatsDuClient(_client1.Id);

            Assert.AreEqual(2, résultats.Count);
        }

        [TestMethod]
        public void TestEntrepotConstat_peutTrouverLeNombreDeConstatDUnClient()
        {
            int nombre = _entrepot.nombreDeConstatPourLeClient(_client1.Id);

            Assert.AreEqual(2, nombre);
        }
    }
}
