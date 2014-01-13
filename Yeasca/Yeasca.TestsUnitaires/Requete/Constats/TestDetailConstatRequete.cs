using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yeasca.Metier;
using Yeasca.Mongo;
using Yeasca.Requete;

namespace Yeasca.TestsUnitaires.Requete.Constats
{
    [TestClass]
    public class TestDetailConstatRequete
    {
        private Constat _constatAChercher;
        private string _nomClient = "Pouet";

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
            _constatAChercher = new Constat()
            {
                Client = new Client() {Nom = _nomClient},
                Huissier = new Huissier()
            };
            entrepot.ajouter(new Constat() {Client = new Client(), Huissier = new Huissier()});
            entrepot.ajouter(_constatAChercher);
        }

        [TestMethod]
        public void TestDetailConstatRequete_peutTrouverLeDétailDuConstat()
        {
            IDetailConstatMessage message = new DetailConstatMessageTest();
            message.IdConstat = _constatAChercher.Id.ToString();
            ReponseRequete réponse = BusRequete.exécuter(message);

            Assert.IsTrue(réponse.Réussite);

            DetailConstatReponse résultat = réponse.Résultat as DetailConstatReponse;

            Assert.IsNotNull(résultat);
            Assert.AreEqual(_nomClient, résultat.NomClient);
        }
    }

    public class DetailConstatMessageTest : IDetailConstatMessage
    {
        public string IdConstat { get; set; }
    }
}
