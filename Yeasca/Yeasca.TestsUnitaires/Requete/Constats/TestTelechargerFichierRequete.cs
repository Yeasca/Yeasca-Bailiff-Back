using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yeasca.Metier;
using Yeasca.Mongo;
using Yeasca.Requete;

namespace Yeasca.TestsUnitaires.Requete.Constats
{
    [TestClass]
    public class TestTelechargerFichierRequete
    {
        private Constat _constat;
        private Fichier _fichierDuConstat;
        private string _nomFichier = "Pouet.txt";

        [TestInitialize]
        public void initialiser()
        {
            ConfigurationTest.initialiserLesEntrepotsMongo();
            ConfigurationTest.initialiserLaSessionHTTP(TypeUtilisateur.Huissier);
            initialiserLesDonnées();
        }

        private void initialiserLesDonnées()
        {
            IEntrepotConstat entrepot = EntrepotMongo.fabriquerEntrepot<IEntrepotConstat>();
            string texteDuFichier = "Pouet";
            byte[] buffer = Encoding.Default.GetBytes(texteDuFichier);
            MemoryStream stream = new MemoryStream(buffer);
            _fichierDuConstat = Fichier.enregistrerLeFichier(stream, _nomFichier);
            _constat = new Constat()
            {
                Client = new Client(),
                Huissier = new Huissier()
            };
            _constat.Fichiers.Add(_fichierDuConstat);
            entrepot.ajouter(_constat);
        }

        [TestMethod]
        public void TestTelechargerFichierRequete_peutTéléchargerUnFichier()
        {
            ITelechargerFichierMessage message = new TelechargerFichierMessageTest();
            message.IdConstat = _constat.Id.ToString();
            message.IdFichier = _fichierDuConstat.Id.ToString();
            ReponseRequete réponse = BusRequete.exécuter(message);

            Assert.IsTrue(réponse.Réussite);

            FichierReponse résultat = réponse.Résultat as FichierReponse;

            Assert.IsNotNull(résultat);
            Assert.AreEqual(_nomFichier, résultat.Nom);
            Assert.AreEqual(_fichierDuConstat.PathFichier, résultat.Chemin);
        }
    }

    public class TelechargerFichierMessageTest : ITelechargerFichierMessage
    {
        public string IdConstat { get; set; }
        public string IdFichier { get; set; }
    }
}
