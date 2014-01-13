using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yeasca.Metier;
using Yeasca.Mongo;
using Yeasca.Requete;

namespace Yeasca.TestsUnitaires.Requete.Constats
{
    [TestClass]
    public class TestRechercheGeneraleConstatRequete
    {
        [TestInitialize]
        public void initialiser()
        {
            ConfigurationTest.initialiserLesEntrepotsMongo();
            ConfigurationTest.initialiserLaSessionHTTP(TypeUtilisateur.Huissier);
            IEntrepotConstat entrepot = EntrepotMongo.fabriquerEntrepot<IEntrepotConstat>();
            entrepot.ajouter(new Constat() { Client = new Client(), Huissier = new Huissier() });
            entrepot.ajouter(new Constat() { Client = new Client(), Huissier = new Huissier() });
        }

        [TestMethod]
        public void TestRechercheGeneraleConstatRequete_peutTrouverLeRésultat()
        {
            IRechercheGeneraleConstatMessage message = new RechercheGeneraleConstatMessageTest();
            message.NuméroPage = 1;
            message.NombreDElémentsParPage = 10;
            ReponseRequete réponse = BusRequete.exécuter(message);

            Assert.IsTrue(réponse.Réussite);

            List<ConstatReponse> résultat = réponse.Résultat as List<ConstatReponse>;

            Assert.IsNotNull(résultat);
            Assert.AreEqual(2, résultat.Count);
        }
    }

    public class RechercheGeneraleConstatMessageTest : IRechercheGeneraleConstatMessage
    {
        public int NuméroPage { get; set; }
        public int NombreDElémentsParPage { get; set; }
        public string Texte { get; set; }
    }
}
