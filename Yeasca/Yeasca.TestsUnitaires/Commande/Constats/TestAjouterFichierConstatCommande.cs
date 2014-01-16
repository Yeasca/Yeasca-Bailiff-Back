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
    public class TestAjouterFichierConstatCommande
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
        public void TestAjouterFichierConstatCommande_peutAjouterUnFichierAUnConstatSiFichierOkEtHuissier()
        {
            ConfigurationTest.initialiserLaSessionHTTP(TypeUtilisateur.Huissier);
            Constat constat = ConfigurationTest.créerUnConstatDeTest();
            IEntrepotConstat entrepot = EntrepotMongo.fabriquerEntrepot<IEntrepotConstat>();
            IAjouterFichierConstatMessage message = new AjouterFichierConstatMessageTest();
            message.IdConstat = constat.Id.ToString();
            message.Fichier = new MemoryStream(Encoding.Default.GetBytes("Pouet"));
            message.Nom = "Poulou.mp3";
            ReponseCommande réponse = BusCommande.exécuter(message);
            constat = entrepot.récupérerLeConstat(constat.Id);

            Assert.IsTrue(réponse.Réussite);
            Assert.AreEqual(1, constat.Fichiers.Count);

            _idConstatAEffacer = constat.Id;
        }

        [TestMethod]
        public void TestAjouterFichierConstatCommande_ajouterUnFichierEchoueSiPasHuissier()
        {
            ConfigurationTest.initialiserLaSessionHTTP(TypeUtilisateur.Secrétaire);
            Constat constat = ConfigurationTest.créerUnConstatDeTest();
            IAjouterFichierConstatMessage message = new AjouterFichierConstatMessageTest();
            message.IdConstat = constat.Id.ToString();
            message.Fichier = new MemoryStream(Encoding.Default.GetBytes("Pouet"));
            message.Nom = "Poulou.mp3";
            ReponseCommande réponse = BusCommande.exécuter(message);

            Assert.IsFalse(réponse.Réussite);
            Assert.AreEqual(Ressource.Commandes.ERREUR_AUTH_HUISSIER, réponse.Message);
        }

        [TestMethod]
        public void TestAjouterFichierConstatCommande_ajouterUnFichierEchoueSiHuissierMaisFichierEnErreur()
        {
            ConfigurationTest.initialiserLaSessionHTTP(TypeUtilisateur.Huissier);
            Constat constat = ConfigurationTest.créerUnConstatDeTest();
            IAjouterFichierConstatMessage message = new AjouterFichierConstatMessageTest();
            message.IdConstat = constat.Id.ToString();
            message.Fichier = null;
            message.Nom = "Poulou.mp3";
            ReponseCommande réponse = BusCommande.exécuter(message);

            Assert.IsFalse(réponse.Réussite);
            Assert.AreEqual(Ressource.Commandes.ERREUR_AJOUT_FICHIER, réponse.Message);
        }
    }

    public class AjouterFichierConstatMessageTest : IAjouterFichierConstatMessage
    {
        public string IdConstat { get; set; }
        public MemoryStream Fichier { get; set; }
        public string Nom { get; set; }
    }
}
