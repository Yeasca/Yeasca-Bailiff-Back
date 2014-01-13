using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yeasca.Metier;

namespace Yeasca.TestsUnitaires.Metier.Constats
{
    [TestClass]
    public class TestFichier
    {
        private string _cheminDuFichierASupprimer;

        [TestInitialize]
        public void initialiser()
        {
            _cheminDuFichierASupprimer = null;
            ConfigurationTest.initialiserLaSessionHTTP(TypeUtilisateur.Huissier);
        }

        [TestCleanup]
        public void nettoyer()
        {
            if (!string.IsNullOrEmpty(_cheminDuFichierASupprimer) && File.Exists(_cheminDuFichierASupprimer))
                File.Delete(_cheminDuFichierASupprimer);
        }

        [TestMethod]
        public void TestFichier_peutEnregistrerUnFichier()
        {
            string texteDuFichier = "Pouet";
            byte[] buffer = Encoding.Default.GetBytes(texteDuFichier);
            MemoryStream stream = new MemoryStream(buffer);
            Fichier enregistrement = Fichier.enregistrerLeFichier(stream, "Pouet", ".txt");

            Assert.IsNotNull(enregistrement);

            string cheminFichier = enregistrement.PathFichier;

            Assert.IsTrue(File.Exists(cheminFichier));
            _cheminDuFichierASupprimer = cheminFichier;

            StreamReader lecteur = new StreamReader(cheminFichier);
            string texteLu = lecteur.ReadToEnd();
            lecteur.Close();

            Assert.AreEqual(texteDuFichier, texteLu);
        }
    }
}
