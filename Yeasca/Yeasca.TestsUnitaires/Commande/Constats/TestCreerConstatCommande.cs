using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yeasca.Commande;
using Yeasca.Metier;
using Yeasca.Mongo;

namespace Yeasca.TestsUnitaires.Commande.Constats
{
    [TestClass]
    public class TestCreerConstatCommande
    {
        [TestInitialize]
        public void initialiser()
        {
            ConfigurationTest.initialiserLesEntrepotsMongo();
        }

        [TestMethod]
        public void TestCreerConstatCommande_exécuterAvecUnBonMessageEnTantQuHuissierFonctionne()
        {
            ConfigurationTest.initialiserLaSessionHTTP(TypeUtilisateur.Huissier);
            string idClient = ajouterUnClient();
            ICreerConstatMessage message = new CreerConstatMessageTest() { IdClient = idClient };
            ReponseCommande réponse = BusCommande.exécuter(message);

            Assert.IsTrue(réponse.Réussite);
            Assert.IsTrue(!string.IsNullOrEmpty(réponse.Message));
        }

        [TestMethod]
        public void TestCreerConstatCommande_exécuterAvecUnBonMessageEnTantQuAdministrateurFonctionne()
        {
            ConfigurationTest.initialiserLaSessionHTTP(TypeUtilisateur.Administrateur);
            string idClient = ajouterUnClient();
            ICreerConstatMessage message = new CreerConstatMessageTest() { IdClient = idClient };
            ReponseCommande réponse = BusCommande.exécuter(message);

            Assert.IsTrue(réponse.Réussite);
            Assert.IsTrue(!string.IsNullOrEmpty(réponse.Message));
        }

        [TestMethod]
        public void TestCreerConstatCommande_exécuterAvecUnMauvaisMessageEnTantQuHuissierEchoue()
        {
            ConfigurationTest.initialiserLaSessionHTTP(TypeUtilisateur.Huissier);
            string idClient = string.Empty;
            ICreerConstatMessage message = new CreerConstatMessageTest() { IdClient = idClient };
            ReponseCommande réponse = BusCommande.exécuter(message);

            Assert.IsFalse(réponse.Réussite);
            Assert.AreEqual(Ressource.Commandes.REF_CLIENT_INVALIDE, réponse.Message);
        }

        [TestMethod]
        public void TestCreerConstatCommande_exécuterAvecUnBonMessageSansEtreHuissierEchoue()
        {
            ConfigurationTest.initialiserLaSessionHTTP(TypeUtilisateur.Inconnu);
            string idClient = ajouterUnClient();
            ICreerConstatMessage message = new CreerConstatMessageTest() { IdClient = idClient };
            ReponseCommande réponse = BusCommande.exécuter(message);

            Assert.IsFalse(réponse.Réussite);
            Assert.AreEqual(Ressource.Commandes.ERREUR_AUTH_HUISSIER, réponse.Message);
        }

        private string ajouterUnClient()
        {
            Client client = new Client();
            IEntrepotProfile entrepot = EntrepotMongo.fabriquerEntrepot<IEntrepotProfile>();
            entrepot.ajouter(client);
            return client.Id.ToString();
        }
    }

    public class CreerConstatMessageTest : ICreerConstatMessage
    {
        public string IdClient { get; set; }
    }
}
