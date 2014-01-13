using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yeasca.Commande;
using Yeasca.Metier;
using Yeasca.Mongo;

namespace Yeasca.TestsUnitaires.Commande.Profiles
{
    [TestClass]
    public class TestCreerClientCommande
    {
        [TestInitialize]
        public void initialiser()
        {
            ConfigurationTest.initialiserLesEntrepotsMongo();    
        }

        [TestMethod]
        public void TestCreerClientCommande_créerUnClientParticulierAuthentifiéAvecDesDonnéesValideRéussit()
        {
            ConfigurationTest.initialiserLaSessionHTTP(TypeUtilisateur.Secrétaire);
            ICreerClientMessage message = créerUnMessageValide();
            ReponseCommande réponse = BusCommande.exécuter(message);

            Assert.IsTrue(réponse.Réussite);

            string idClient = réponse.Message;
            IEntrepotProfile entrepot = EntrepotMongo.fabriquerEntrepot<IEntrepotProfile>();
            Profile client = entrepot.récupérerLeClient(new Guid(idClient));

            Assert.IsNotNull(client);
            Assert.IsTrue(client is Client);
        }

        [TestMethod]
        public void TestCreerClientCommande_créerUnClientSociétéAuthentifiéAvecDesDonnéesValideRéussit()
        {
            ConfigurationTest.initialiserLaSessionHTTP(TypeUtilisateur.Secrétaire);
            ICreerClientMessage message = créerUnMessageValide();
            message.DénominationSociété = "Bonjour inc";
            ReponseCommande réponse = BusCommande.exécuter(message);

            Assert.IsTrue(réponse.Réussite);

            string idClient = réponse.Message;
            IEntrepotProfile entrepot = EntrepotMongo.fabriquerEntrepot<IEntrepotProfile>();
            Profile client = entrepot.récupérerLeClient(new Guid(idClient));

            Assert.IsNotNull(client);
            Assert.IsTrue(client is Client);
        }

        [TestMethod]
        public void TestCreerClientCommande_créerUnClientSansEtreAuthentifiéEchoue()
        {
            ConfigurationTest.initialiserLaSessionHTTP(TypeUtilisateur.Inconnu);
            ICreerClientMessage message = créerUnMessageValide();
            ReponseCommande réponse = BusCommande.exécuter(message);

            Assert.IsFalse(réponse.Réussite);
            Assert.AreEqual(Ressource.Commandes.ERREUR_AUTH, réponse.Message);
        }

        [TestMethod]
        public void TestCreerClientCommande_créerUnClientAvecParamètresManquantsEchoue()
        {
            ConfigurationTest.initialiserLaSessionHTTP(TypeUtilisateur.Huissier);
            ICreerClientMessage message = créerUnMessageValide();
            message.NomVoie = null;
            ReponseCommande réponse = BusCommande.exécuter(message);

            Assert.IsFalse(réponse.Réussite);
            Assert.IsTrue(réponse.Message.Contains(Ressource.Validation.NOM_VOIE_REQUIS));
        }

        private ICreerClientMessage créerUnMessageValide()
        {
            ICreerClientMessage message = new CreerClientMessageTest();
            message.Civilité = 0;
            message.Nom = "Johanson";
            message.Prénom = "Scarlett";
            message.NomVoie = "pouet";
            message.CodePostal = "33520";
            message.Ville = "Bruges";
            return message;
        }
    }

    public class CreerClientMessageTest : ICreerClientMessage
    {
        public int Civilité { get; set; }
        public string Nom { get; set; }
        public string Prénom { get; set; }
        public string DénominationSociété { get; set; }
        public string NuméroVoie { get; set; }
        public string RépétitionVoie { get; set; }
        public string TypeVoie { get; set; }
        public string NomVoie { get; set; }
        public string ComplémentVoie { get; set; }
        public string CodePostal { get; set; }
        public string Ville { get; set; }
    }
}
