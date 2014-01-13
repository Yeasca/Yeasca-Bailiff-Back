using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yeasca.Metier;
using Yeasca.Mongo;
using Yeasca.Requete;

namespace Yeasca.TestsUnitaires.Requete.Utilisateurs
{
    [TestClass]
    public class TestDetailUtilisateurRequete
    {
        private Utilisateur _compte = new Utilisateur() { Profile = new Huissier(), TypeUtilisateur = TypeUtilisateur.Huissier};

        [TestInitialize]
        public void initialiser()
        {
            ConfigurationTest.initialiserLesEntrepotsMongo();
            ConfigurationTest.initialiserLaSessionHTTP(TypeUtilisateur.Huissier);
            IEntrepotUtilisateur entrepot = EntrepotMongo.fabriquerEntrepot<IEntrepotUtilisateur>();
            entrepot.ajouter(_compte);
        }

        [TestMethod]
        public void TestDetailUtilisateurRequete_peutRécupérerLesDétailsDuCompte()
        {
            IDetailUtilisateurMessage message = new DetailUtilisateurMessageTest();
            message.IdUtilisateur = _compte.Id.ToString();
            ReponseRequete réponse = BusRequete.exécuter(message);

            Assert.IsTrue(réponse.Réussite);

            DetailUtilisateurReponse résultat = réponse.Résultat as DetailUtilisateurReponse;

            Assert.IsNotNull(résultat);
        }
    }

    public class DetailUtilisateurMessageTest : IDetailUtilisateurMessage
    {
        public string IdUtilisateur { get; set; }
    }
}
