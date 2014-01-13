using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yeasca.Metier;
using Yeasca.Mongo;
using Yeasca.Requete;

namespace Yeasca.TestsUnitaires.Requete.Profiles
{
    [TestClass]
    public class TestDetailClientRequete
    {
        private Client _client = new Client() {Nom = "Pouet", Adresse = ConstantesTest.ADRESSE_VALIDE};

        [TestInitialize]
        public void initialiser()
        {
            ConfigurationTest.initialiserLesEntrepotsMongo();
            ConfigurationTest.initialiserLaSessionHTTP(TypeUtilisateur.Huissier);
            initialiserLesDonnées();
        }

        private void initialiserLesDonnées()
        {
            IEntrepotProfile entrepotProfile = EntrepotMongo.fabriquerEntrepot<IEntrepotProfile>();
            entrepotProfile.ajouter(_client);
            IEntrepotConstat entrepotConstat = EntrepotMongo.fabriquerEntrepot<IEntrepotConstat>();
            entrepotConstat.ajouter(new Constat() { Client = _client, Huissier = new Huissier() });
            entrepotConstat.ajouter(new Constat() { Client = _client, Huissier = new Huissier() });
        }

        [TestMethod]
        public void TestDetailClientRequete_peutRécupérerLeDétailDuClient()
        {
            IDetailClientMessage message = new DetailClientMessageTest();
            message.IdClient = _client.Id.ToString();
            ReponseRequete réponse = BusRequete.exécuter(message);

            Assert.IsTrue(réponse.Réussite);

            DetailClientReponse résultat = réponse.Résultat as DetailClientReponse;

            Assert.IsNotNull(résultat);
            Assert.AreEqual(2, résultat.Constats.Count);
        }
    }

    public class DetailClientMessageTest : IDetailClientMessage
    {
        public string IdClient { get; set; }
    }
}
