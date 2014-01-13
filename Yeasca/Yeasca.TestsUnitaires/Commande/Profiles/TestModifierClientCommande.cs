using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yeasca.Commande;
using Yeasca.Metier;

namespace Yeasca.TestsUnitaires.Commande.Profiles
{
    [TestClass]
    public class TestModifierClientCommande
    {
        [TestInitialize]
        public void initialiser()
        {
            ConfigurationTest.initialiserLesEntrepotsMongo();    
        }

        [TestMethod]
        public void TestModifierClientCommande_modifierUnClientAvecLesBonsParamètresEtAuthentifiéFonctionne()
        {
            ConfigurationTest.initialiserLaSessionHTTP(TypeUtilisateur.Secrétaire);
            Client client = ConfigurationTest.créerUnClient();
            IModifierClientMessage message = créerUnMessageValide(client.Id.ToString());
            ReponseCommande réponse = BusCommande.exécuter(message);

            Assert.IsTrue(réponse.Réussite);
        }

        [TestMethod]
        public void TestModifierClientCommande_modifierUnClientAvecLesBonsParamètresSansEtreAuthentifiéEchoue()
        {
            ConfigurationTest.initialiserLaSessionHTTP(TypeUtilisateur.Inconnu);
            Client client = ConfigurationTest.créerUnClient();
            IModifierClientMessage message = créerUnMessageValide(client.Id.ToString());
            ReponseCommande réponse = BusCommande.exécuter(message);

            Assert.IsFalse(réponse.Réussite);
            Assert.AreEqual(Ressource.Commandes.ERREUR_AUTH, réponse.Message);
        }

        [TestMethod]
        public void TestModifierClientCommande_modifierUnClientSansLesBonsParamètresEtAuthentifiéEchoue()
        {
            ConfigurationTest.initialiserLaSessionHTTP(TypeUtilisateur.Huissier);
            Client client = ConfigurationTest.créerUnClient();
            IModifierClientMessage message = créerUnMessageValide(client.Id.ToString());
            message.NomVoie = null;
            ReponseCommande réponse = BusCommande.exécuter(message);

            Assert.IsFalse(réponse.Réussite);
            Assert.IsTrue(réponse.Message.Contains(Ressource.Validation.NOM_VOIE_REQUIS));
        }

        private IModifierClientMessage créerUnMessageValide(string idClient)
        {
            IModifierClientMessage message = new ModifierClientMessageTest();
            message.idClient = idClient;
            message.Civilité = 0;
            message.Nom = "Johanson";
            message.Prénom = "Scarlett";
            message.NomVoie = "Pouet";
            message.CodePostal = "33520";
            message.Ville = "Bruges";
            return message;
        }
    }

    public class ModifierClientMessageTest : IModifierClientMessage
    {
        public string idClient { get; set; }
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
