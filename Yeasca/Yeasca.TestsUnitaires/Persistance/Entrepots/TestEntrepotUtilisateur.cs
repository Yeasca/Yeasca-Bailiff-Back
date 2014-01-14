using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yeasca.Metier;
using Yeasca.Mongo;

namespace Yeasca.TestsUnitaires.Persistance.Entrepots
{
    [TestClass]
    public class TestEntrepotUtilisateur
    {
        private string _nomHuissier = "Uncle";
        private string _prénomSecrétaire = "Scarlett";
        private TypeUtilisateur _typeUtilisateur = TypeUtilisateur.Secrétaire;

        private IFournisseur _fournisseur;
        private IEntrepotUtilisateur _entrepot;

        [TestInitialize]
        public void initialiser()
        {
            _fournisseur = new FournisseurTest();
            _fournisseur.insérer<Utilisateur>(new Utilisateur() {TypeUtilisateur = _typeUtilisateur});
            _fournisseur.insérer<Utilisateur>(new Utilisateur()
            {
                TypeUtilisateur = TypeUtilisateur.Huissier,
                Profile = new Huissier() {Nom = _nomHuissier}
            });
            _fournisseur.insérer<Utilisateur>(new Utilisateur()
            {
                TypeUtilisateur = TypeUtilisateur.Secrétaire,
                Profile = new Secretaire() {Prénom = _prénomSecrétaire}
            });
            _entrepot = new EntrepotUtilisateur(_fournisseur);
        }

        [TestMethod]
        public void TestEntrepotUtilisateur_peutAjouterUnUtilisateur()
        {
            Utilisateur unUtilisateur = new Utilisateur();
            int nombreDElémentsInitial = _fournisseur.obtenirLaCollection<Utilisateur>().Count();
            bool ajout = _entrepot.ajouter(unUtilisateur);
            int nombreDElémentsFinal = _fournisseur.obtenirLaCollection<Utilisateur>().Count();

            Assert.IsTrue(ajout);
            Assert.AreEqual(nombreDElémentsInitial + 1, nombreDElémentsFinal);
        }

        [TestMethod]
        public void TestEntrepotUtilisateur_peutAuthentifierUnUtilisateur()
        {
            Email email = new Email("abc@def.ghi");
            MotDePasse motDePasse = new MotDePasse("pouet");
            Utilisateur unUtilisateur = new Utilisateur();
            unUtilisateur.Email = email;
            unUtilisateur.MotDePasse = motDePasse;
            _entrepot.ajouter(unUtilisateur);
            Utilisateur utilisateurAuthentifié = _entrepot.authentifier(email, motDePasse);

            Assert.IsNotNull(utilisateurAuthentifié);
            Assert.AreEqual(email, utilisateurAuthentifié.Email);
            Assert.AreEqual(motDePasse, utilisateurAuthentifié.MotDePasse);
        }

        [TestMethod]
        public void TestEntrepotUtilisateur_peutRécupérerUnUtilisateur()
        {
            Utilisateur unUtilisateur = new Utilisateur();
            _entrepot.ajouter(unUtilisateur);
            Utilisateur utilisateurRécupéré = _entrepot.récupérer(unUtilisateur.Id);

            Assert.IsNotNull(utilisateurRécupéré);
        }

        [TestMethod]
        public void TestEntrepotUtilisateur_peutRechercherUnUtilisateurSpécifiqueSansCritères()
        {
            RechercheUtilisateurTest recherche = new RechercheUtilisateurTest();
            IList<Utilisateur> résultats = _entrepot.récupérerLaListeDesUtilisateurs(recherche);

            Assert.AreEqual(3, résultats.Count);
        }

        [TestMethod]
        public void TestEntrepotUtilisateur_peutRechercherUnUtilisateurSpécifiqueSurLeNom()
        {
            RechercheUtilisateurTest recherche = new RechercheUtilisateurTest();
            recherche.NomUtilisateur = _nomHuissier;
            IList<Utilisateur> résultats = _entrepot.récupérerLaListeDesUtilisateurs(recherche);

            Assert.AreEqual(1, résultats.Count);
            Assert.AreEqual(_nomHuissier, résultats[0].Profile.Nom);
        }

        [TestMethod]
        public void TestEntrepotUtilisateur_peutRechercherUnUtilisateurSpécifiqueSurLeType()
        {
            RechercheUtilisateurTest recherche = new RechercheUtilisateurTest();
            recherche.Type = (int)TypeUtilisateur.Secrétaire;
            IList<Utilisateur> résultats = _entrepot.récupérerLaListeDesUtilisateurs(recherche);

            Assert.AreEqual(2, résultats.Count);
            Assert.IsTrue(résultats.All(x => x.TypeUtilisateur == TypeUtilisateur.Secrétaire));
        }

        [TestMethod]
        public void TestEntrepotUtilisateur_peutRechercheUnUtilisateurSpécifiqueSurTousLesCritères()
        {
            RechercheUtilisateurTest recherche = new RechercheUtilisateurTest();
            recherche.Type = (int)TypeUtilisateur.Secrétaire;
            recherche.NomUtilisateur = _prénomSecrétaire;
            IList<Utilisateur> résultats = _entrepot.récupérerLaListeDesUtilisateurs(recherche);

            Assert.AreEqual(1, résultats.Count);
            Assert.AreEqual(_prénomSecrétaire, résultats[0].Profile.Prénom);
        }
    }
}
