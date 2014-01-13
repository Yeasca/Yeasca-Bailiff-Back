using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yeasca.Metier;
using Yeasca.Mongo;

namespace Yeasca.TestsIntegration.Persistance.Mongo
{
    [TestClass]
    public class TestFournisseurMongo
    {
        private IFournisseur _fournisseur;

        [TestInitialize]
        public void connecter()
        {
            _fournisseur = new FournisseurMongo();
            _fournisseur.seConnecter();
            Assert.IsTrue(_fournisseur.EstConnecté);
        }

        [TestCleanup]
        public void déconnecter()
        {
            _fournisseur.seDéconnecter();
            Assert.IsFalse(_fournisseur.EstConnecté);
            _fournisseur = null;
        }

        #region Mapping Constat

        [TestMethod]
        public void TestFournisseurMongo_peutAjouterUnElementDansLaCollectionConstat()
        {
            int nombreDeConstatInitial = _fournisseur.obtenirLaCollection<Constat>().Count();
            Constat unConstat = new Constat();

            bool ajout = _fournisseur.insérer<Constat>(unConstat);
            int nombreDeConstatFinal = _fournisseur.obtenirLaCollection<Constat>().Count();

            Assert.IsTrue(ajout);
            Assert.AreEqual(nombreDeConstatInitial + 1, nombreDeConstatFinal);
            Assert.IsNotNull(unConstat.Id);
        }

        [TestMethod]
        public void TestFournisseurMongo_peutRécupérerUnElémentDansLaCollectionConstat()
        {
            Constat unConstat = new Constat();
            _fournisseur.insérer<Constat>(unConstat);
            Constat constatRécupéré = _fournisseur.obtenirLaCollection<Constat>().SingleOrDefault(x => x.Id == unConstat.Id);

            Assert.IsNotNull(constatRécupéré);
        }

        [TestMethod]
        public void TestFournisseurMongo_peutModifierUnElementDansLaCollectionConstat()
        {
            DateTime dateInitiale = new DateTime(2014, 1, 1);
            DateTime dateFinale = new DateTime(2014, 2, 1);
            Constat unConstat = new Constat();
            unConstat.Date = dateInitiale;
            _fournisseur.insérer<Constat>(unConstat);
            unConstat.Date = dateFinale;
            int nombreDeConstatInitial = _fournisseur.obtenirLaCollection<Constat>().Count();
            _fournisseur.modifier<Constat>(unConstat);
            Constat constatRécupéré = _fournisseur.obtenirLaCollection<Constat>().SingleOrDefault(x => x.Id == unConstat.Id);
            int nombreDeConstatFinal = _fournisseur.obtenirLaCollection<Constat>().Count();

            Assert.IsNotNull(constatRécupéré);
            Assert.AreEqual(nombreDeConstatInitial, nombreDeConstatFinal);
            Assert.AreEqual(dateFinale, constatRécupéré.Date);
        }

        #endregion

        #region Mapping Partie

        [TestMethod]
        public void TestFournisseurMongo_peutAjouterUnElementDansLaCollectionPartie()
        {
            int nombreDeConstatInitial = _fournisseur.obtenirLaCollection<Profile>().Count();
            Profile unePartie = new Client();

            bool ajout = _fournisseur.insérer<Profile>(unePartie);
            int nombreDeConstatFinal = _fournisseur.obtenirLaCollection<Profile>().Count();

            Assert.IsTrue(ajout);
            Assert.AreEqual(nombreDeConstatInitial + 1, nombreDeConstatFinal);
            Assert.IsNotNull(unePartie.Id);
        }

        [TestMethod]
        public void TestFournisseurMongo_peutRécupérerUnElémentDansLaCollectionPartie()
        {
            Profile unePartie = new Client();
            _fournisseur.insérer<Profile>(unePartie);
            Profile partieRécupérée = _fournisseur.obtenirLaCollection<Profile>().SingleOrDefault(x => x.Id == unePartie.Id);

            Assert.IsNotNull(partieRécupérée);
            Assert.IsInstanceOfType(unePartie, typeof(Client));
        }

        [TestMethod]
        public void TestFournisseurMongo_peutModifierUnElementDansLaCollectionPartie()
        {
            string nomInitial = "Morane";
            string nomFinal = "Marley";
            Profile unePartie = new Huissier();
            unePartie.Nom = nomInitial;
            _fournisseur.insérer<Profile>(unePartie);
            unePartie.Nom = nomFinal;
            int nombreDeConstatInitial = _fournisseur.obtenirLaCollection<Profile>().Count();
            _fournisseur.modifier<Profile>(unePartie);
            Profile partieRécupérée = _fournisseur.obtenirLaCollection<Profile>().SingleOrDefault(x => x.Id == unePartie.Id);
            int nombreDeConstatFinal = _fournisseur.obtenirLaCollection<Profile>().Count();

            Assert.IsNotNull(partieRécupérée);
            Assert.AreEqual(nombreDeConstatInitial, nombreDeConstatFinal);
            Assert.AreEqual(nomFinal, partieRécupérée.Nom);
        }

        #endregion

        #region Mapping Utilisateur

        [TestMethod]
        public void TestFournisseurMongo_peutAjouterUnElémentDansLaCollectionUtilisateur()
        {
            int nombreDeConstatInitial = _fournisseur.obtenirLaCollection<Utilisateur>().Count();
            Utilisateur unUtilisateur = new Utilisateur();

            bool ajout = _fournisseur.insérer<Utilisateur>(unUtilisateur);
            int nombreDeConstatFinal = _fournisseur.obtenirLaCollection<Utilisateur>().Count();

            Assert.IsTrue(ajout);
            Assert.AreEqual(nombreDeConstatInitial + 1, nombreDeConstatFinal);
            Assert.IsNotNull(unUtilisateur.Id);
        }

        [TestMethod]
        public void TestFournisseurMongo_peutRécupérerUnElémentDansLaCollectionUtilisateur()
        {
            Utilisateur unUtilisateur = new Utilisateur();
            _fournisseur.insérer<Utilisateur>(unUtilisateur);
            Utilisateur utilisateurRécupéré = _fournisseur.obtenirLaCollection<Utilisateur>().SingleOrDefault(x => x.Id == unUtilisateur.Id);

            Assert.IsNotNull(utilisateurRécupéré);
        }

        [TestMethod]
        public void TestFournisseurMongo_peutModifierUnElementDansLaCollectionUtilisateur()
        {
            TypeUtilisateur typeInitial = TypeUtilisateur.Huissier;
            TypeUtilisateur typeFinal = TypeUtilisateur.Secrétaire;
            Utilisateur unUtilisateur = new Utilisateur();
            unUtilisateur.TypeUtilisateur = typeInitial;
            _fournisseur.insérer<Utilisateur>(unUtilisateur);
            unUtilisateur.TypeUtilisateur = typeFinal;
            int nombreDUtilisateursInitial = _fournisseur.obtenirLaCollection<Utilisateur>().Count();
            _fournisseur.modifier<Utilisateur>(unUtilisateur);
            Utilisateur utilisateurRécupéré = _fournisseur.obtenirLaCollection<Utilisateur>().SingleOrDefault(x => x.Id == unUtilisateur.Id);
            int nombreDUtilisateurFinal = _fournisseur.obtenirLaCollection<Utilisateur>().Count();

            Assert.IsNotNull(utilisateurRécupéré);
            Assert.AreEqual(nombreDUtilisateursInitial, nombreDUtilisateurFinal);
            Assert.AreEqual(typeFinal, utilisateurRécupéré.TypeUtilisateur);
        }

        #endregion

        #region Autre

        [TestMethod]
        public void TestFournisseurMongo_peutRécupérerLeConstatEtLesPartiesAssociées()
        {
            Huissier unHuissier = créerUnHuissierVide();
            Client unClient = créerUnClientParticulierVide();
            Constat unConstat = créerUnConstatAvecHuissierEtClient(unHuissier, unClient);
            Constat constatRécupéré = _fournisseur.obtenirLaCollection<Constat>().SingleOrDefault(x => x.Id == unConstat.Id);

            Assert.IsNotNull(constatRécupéré);
            Assert.AreEqual(unHuissier.Id, constatRécupéré.Huissier.Id);
            Assert.IsInstanceOfType(constatRécupéré.Huissier, typeof(Huissier));
            Assert.IsInstanceOfType(constatRécupéré.Client, typeof(Client));
            Assert.AreEqual(unClient.Id, constatRécupéré.Client.Id);
        }

        [TestMethod]
        public void TestFournisseurMongo_peutModifierLesPartiesDUnConstatMaisPasLesPartiesDansLaCollection()
        {
            string nomFinal = "Sinclar";
            Huissier unHuissier = créerUnHuissierVide();
            Client unClient = créerUnClientParticulierVide();
            Constat unConstat = créerUnConstatAvecHuissierEtClient(unHuissier, unClient);
            unConstat.Huissier.Nom = nomFinal;
            _fournisseur.modifier<Constat>(unConstat);
            Huissier huissierRécupéré = _fournisseur.obtenirLaCollection<Profile>().SingleOrDefault(x => x.Id == unHuissier.Id) as Huissier;
            Constat constatRécupéré = _fournisseur.obtenirLaCollection<Constat>().SingleOrDefault(x => x.Id == unConstat.Id);

            Assert.AreNotEqual(nomFinal, huissierRécupéré.Nom);
            Assert.AreEqual(nomFinal, constatRécupéré.Huissier.Nom);
        }

        private Huissier créerUnHuissierVide()
        {
            Huissier unHuissier = new Huissier();
            _fournisseur.insérer<Profile>(unHuissier);
            return unHuissier;
        }

        private Client créerUnClientParticulierVide()
        {
            Client unClient = new Client();
            _fournisseur.insérer<Profile>(unClient);
            return unClient;
        }

        private Constat créerUnConstatAvecHuissierEtClient(Profile huissier, Profile client)
        {
            Constat unConstat = new Constat();
            unConstat.Huissier = huissier;
            unConstat.Client = client;
            _fournisseur.insérer<Constat>(unConstat);
            return unConstat;
        }

        [TestMethod]
        public void TestFournisseurMongo_peutAjouterUnElementAvecUnIdDansUneCollection()
        {
            Guid Id = Guid.NewGuid();
            Constat unConstat = new Constat();
            unConstat.Id = Id;
            bool ajout = _fournisseur.insérer<Constat>(unConstat);
            Constat constatRécupéré = _fournisseur.obtenirLaCollection<Constat>().SingleOrDefault(x => x.Id == Id);

            Assert.IsTrue(ajout);
            Assert.IsNotNull(constatRécupéré);
        }

        #endregion
    }
}
