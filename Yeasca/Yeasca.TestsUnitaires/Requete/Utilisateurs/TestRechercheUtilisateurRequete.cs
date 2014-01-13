using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yeasca.Metier;
using Yeasca.Mongo;
using Yeasca.Requete;

namespace Yeasca.TestsUnitaires.Requete.Utilisateurs
{
    [TestClass]
    public class TestRechercheUtilisateurRequete
    {
        [TestInitialize]
        public void initialiser()
        {
            ConfigurationTest.initialiserLesEntrepotsMongo();
            ConfigurationTest.initialiserLaSessionHTTP(TypeUtilisateur.Administrateur);
            IEntrepotUtilisateur entrepot = EntrepotMongo.fabriquerEntrepot<IEntrepotUtilisateur>();
            entrepot.ajouter(new Utilisateur() { Profile = new Huissier(), TypeUtilisateur = TypeUtilisateur.Huissier});
            entrepot.ajouter(new Utilisateur() { Profile = new Secretaire(), TypeUtilisateur = TypeUtilisateur.Secrétaire });
        }

        [TestMethod]
        public void TestRechercheUtilisateurRequete_peutRécupérerLeRésultatDeLaRecherche()
        {
            IRechercheUtilisateurMessage message = new RechercheUtilisateurMessageTest();
            message.Type = TypeUtilisateur.Huissier;
            message.NuméroPage = 1;
            message.NombreDElémentsParPage = 10;
            ReponseRequete réponse = BusRequete.exécuter(message);

            Assert.IsTrue(réponse.Réussite);

            List<UtilisateurReponse> résultat = réponse.Résultat as List<UtilisateurReponse>;

            Assert.IsNotNull(résultat);
            Assert.AreEqual(1, résultat.Count);
        }
    }

    public class RechercheUtilisateurMessageTest : IRechercheUtilisateurMessage
    {
        public int NuméroPage { get; set; }
        public int NombreDElémentsParPage { get; set; }
        public string NomUtilisateur { get; set; }
        public TypeUtilisateur Type { get; set; }
    }
}
