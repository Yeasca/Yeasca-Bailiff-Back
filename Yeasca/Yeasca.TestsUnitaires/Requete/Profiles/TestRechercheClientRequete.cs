using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yeasca.Metier;
using Yeasca.Mongo;
using Yeasca.Requete;

namespace Yeasca.TestsUnitaires.Requete.Profiles
{
    [TestClass]
    public class TestRechercheClientRequete
    {
        private string _nomClient = "Pouet";

        [TestInitialize]
        public void initialiser()
        {
            ConfigurationTest.initialiserLesEntrepotsMongo();
            ConfigurationTest.initialiserLaSessionHTTP(TypeUtilisateur.Huissier);
            IEntrepotProfile entrepot = EntrepotMongo.fabriquerEntrepot<IEntrepotProfile>();
            entrepot.ajouter(new Client());
            entrepot.ajouter(new Client() { Nom = _nomClient });
        }

        [TestMethod]
        public void TestRechercheClientRequete_peutTrouverLesClients()
        {
            IRechercheClientMessage message = new RechercheClientMessageTest();
            message.Texte = _nomClient;
            message.NuméroPage = 1;
            message.NombreDElémentsParPage = 10;
            ReponseRequete réponse = BusRequete.exécuter(message);

            Assert.IsTrue(réponse.Réussite);

            List<ClientReponse> résultat = réponse.Résultat as List<ClientReponse>;

            Assert.IsNotNull(résultat);
            Assert.AreEqual(1, résultat.Count);
        }
    }

    public class RechercheClientMessageTest : IRechercheClientMessage
    {
        public int NuméroPage { get; set; }
        public int NombreDElémentsParPage { get; set; }
        public string Texte { get; set; }
    }
}
