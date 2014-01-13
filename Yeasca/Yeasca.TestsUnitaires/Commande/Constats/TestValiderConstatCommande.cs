using System;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yeasca.Commande;
using Yeasca.Metier;
using Yeasca.Mongo;

namespace Yeasca.TestsUnitaires.Commande.Constats
{
    [TestClass]
    public class TestValiderConstatCommande
    {
        private Guid _idConstatAEffacer;

        [TestInitialize]
        public void initialiser()
        {
            _idConstatAEffacer = new Guid();
            ConfigurationTest.initialiserLesEntrepotsMongo();
        }

        [TestCleanup]
        public void nettoyer()
        {
            if (_idConstatAEffacer != null && _idConstatAEffacer != new Guid())
            {
                IEntrepotConstat entrepot = EntrepotMongo.fabriquerEntrepot<IEntrepotConstat>();
                Constat constatANettoyer = entrepot.récupérerLeConstat(_idConstatAEffacer);
                nettoyerLesFichiers(constatANettoyer);
            }
        }

        private static void nettoyerLesFichiers(Constat constatANettoyer)
        {
            foreach (Fichier fichier in constatANettoyer.Fichiers)
                supprimerLeFichier(fichier);
        }

        private static void supprimerLeFichier(Fichier fichier)
        {
            string pathFichier = fichier.PathFichier;
            if (pathFichier != null && File.Exists(pathFichier))
                File.Delete(pathFichier);
        }

        [TestMethod]
        public void TestValiderConstatCommande_validerLeContratAvecUnWordAuthentifiéFonctione()
        {
            ConfigurationTest.initialiserLaSessionHTTP(TypeUtilisateur.Huissier);
            Constat constat = ConfigurationTest.créerUnConstatDeTest();
            IEntrepotConstat entrepot = EntrepotMongo.fabriquerEntrepot<IEntrepotConstat>();
            IValiderConstatMessage message = new ValiderConstatMessageTest();
            message.IdConstat = constat.Id.ToString();
            message.Fichier = new MemoryStream(Encoding.Default.GetBytes("Pouet"));
            message.Nom = "Poulou";
            message.Extension = ".docx";
            ReponseCommande réponse = BusCommande.exécuter(message);
            constat = entrepot.récupérerLeConstat(constat.Id);

            Assert.IsTrue(réponse.Réussite);
            Assert.AreEqual(1, constat.Fichiers.Count);
            Assert.IsTrue(constat.EstValidé);

            _idConstatAEffacer = constat.Id;
        }

        [TestMethod]
        public void TestValiderConstatCommande_validerLeContratAvecUnWordSansEtreAuthentifiéEchoue()
        {
            ConfigurationTest.initialiserLaSessionHTTP(TypeUtilisateur.Inconnu);
            Constat constat = ConfigurationTest.créerUnConstatDeTest();
            IValiderConstatMessage message = new ValiderConstatMessageTest();
            message.IdConstat = constat.Id.ToString();
            message.Fichier = new MemoryStream(Encoding.Default.GetBytes("Pouet"));
            message.Nom = "Poulou";
            message.Extension = ".docx";
            ReponseCommande réponse = BusCommande.exécuter(message);

            Assert.IsFalse(réponse.Réussite);
            Assert.AreEqual(Ressource.Commandes.ERREUR_AUTH, réponse.Message);
        }

        [TestMethod]
        public void TestValiderConstatCommande_validerLeContratSansWordEtAuthentifiéEchoue()
        {
            ConfigurationTest.initialiserLaSessionHTTP(TypeUtilisateur.Secrétaire);
            Constat constat = ConfigurationTest.créerUnConstatDeTest();
            IValiderConstatMessage message = new ValiderConstatMessageTest();
            message.IdConstat = constat.Id.ToString();
            message.Fichier = null;
            message.Nom = "Poulou";
            message.Extension = ".docx";
            ReponseCommande réponse = BusCommande.exécuter(message);

            Assert.IsFalse(réponse.Réussite);
            Assert.AreEqual(Ressource.Commandes.ERREUR_AJOUT_FICHIER, réponse.Message);
        }

        [TestMethod]
        public void TestValiderConstatCommande_validerLeContratAvecMauvaiseExtensionFichierEtAuthentifiéEchoue()
        {
            ConfigurationTest.initialiserLaSessionHTTP(TypeUtilisateur.Secrétaire);
            Constat constat = ConfigurationTest.créerUnConstatDeTest();
            IValiderConstatMessage message = new ValiderConstatMessageTest();
            message.IdConstat = constat.Id.ToString();
            message.Fichier = null;
            message.Nom = "Poulou";
            message.Extension = ".txt";
            ReponseCommande réponse = BusCommande.exécuter(message);

            Assert.IsFalse(réponse.Réussite);
            Assert.AreEqual(Ressource.Commandes.ERREUR_AJOUT_FICHIER, réponse.Message);
        }
    }

    public class ValiderConstatMessageTest : IValiderConstatMessage
    {
        public string IdConstat { get; set; }
        public MemoryStream Fichier { get; set; }
        public string Nom { get; set; }
        public string Extension { get; set; }
    }
}
