using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yeasca.Metier;
using Yeasca.Requete;

namespace Yeasca.TestsUnitaires.Requete.Utilisateurs
{
    [TestClass]
    public class TestGenererJetonRequete
    {
        [TestInitialize]
        public void initialiser()
        {
            ConfigurationTest.initialiserLesEntrepotsMongo();
            ConfigurationTest.initialiserLaSessionHTTP(TypeUtilisateur.Superviseur);
        }

        [TestMethod]
        public void TestGenererJetonRequete_peutGénérerUnJeton()
        {
            string clé = "pouet@poulou.com";
            IGenererJetonMessage message = new GenererJetonMessageTest();
            message.Email = clé;
            message.Login = ConstantesTest.EMAIL_VALIDE.Valeur;
            message.MotDePasse = ConstantesTest.MOT_DE_PASSE_VALIDE.ValeurDéchiffrée;
            ReponseRequete réponse = BusRequete.exécuter(message);

            Assert.IsTrue(réponse.Réussite);

            GenererJetonReponse résultat = réponse.Résultat as GenererJetonReponse;

            Assert.IsNotNull(résultat);

            Jeton jeton = new Jeton(résultat.Jeton);

            Assert.IsTrue(jeton.estValide(clé));
        }
    }

    public class GenererJetonMessageTest : IGenererJetonMessage
    {
        public string Email { get; set; }
        public string Login { get; set; }
        public string MotDePasse { get; set; }
    }
}
