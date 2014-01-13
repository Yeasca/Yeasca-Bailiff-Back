using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yeasca.Metier;
using Yeasca.Mongo;
using Yeasca.Requete;

namespace Yeasca.TestsUnitaires.Requete.Constats
{
    [TestClass]
    public class TestRechercheAvanceConstatRequete
    {
        private string _nomClient = "Pouet";

        [TestInitialize]
        public void initialiser()
        {
            ConfigurationTest.initialiserLesEntrepotsMongo();
            ConfigurationTest.initialiserLaSessionHTTP(TypeUtilisateur.Huissier);
            IEntrepotConstat entrepot = EntrepotMongo.fabriquerEntrepot<IEntrepotConstat>();
            entrepot.ajouter(new Constat() { Client = new Client(), Huissier = new Huissier() });
            entrepot.ajouter(new Constat() { Client = new Client() {Nom = _nomClient}, Huissier = new Huissier() });
        }

        [TestMethod]
        public void TestRechercheAvanceConstatRequete_peutTrouverLesRésultatsCorrespondants()
        {
            IRechercheAvanceConstatMessage message = new RechercheAvanceConstatMessageTest();
            message.NomClient = _nomClient;
            message.NuméroPage = 1;
            message.NombreDElémentsParPage = 10;
            ReponseRequete réponse = BusRequete.exécuter(message);

            Assert.IsTrue(réponse.Réussite);

            List<ConstatReponse> résultat = réponse.Résultat as List<ConstatReponse>;

            Assert.IsNotNull(résultat);
            Assert.AreEqual(1, résultat.Count);
        }
    }

    public class RechercheAvanceConstatMessageTest : IRechercheAvanceConstatMessage
    {
        public int NuméroPage { get; set; }
        public int NombreDElémentsParPage { get; set; }
        public string NomClient { get; set; }
        public DateTime? DateDébut { get; set; }
        public DateTime? DateFin { get; set; }
    }
}
