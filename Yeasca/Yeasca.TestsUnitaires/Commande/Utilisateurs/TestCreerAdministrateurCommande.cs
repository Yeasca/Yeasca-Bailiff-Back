using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yeasca.Commande;
using Yeasca.Metier;
using Yeasca.Mongo;

namespace Yeasca.TestsUnitaires.Commande.Utilisateurs
{
    [TestClass]
    public class TestCreerAdministrateurCommande
    {
        private string _email = ConstantesTest.EMAIL_VALIDE.Valeur;

        [TestInitialize]
        public void initialiser()
        {
            ConfigurationTest.initialiserLesEntrepotsMongo();
        }

        [TestMethod]
        public void TestCreerAdministrateurCommande_créerUnAdminAvecLesBonParamètresFonctionne()
        {
            ICreerAdministrateurMessage message = initialiserUnMessageValide();
            ReponseCommande réponse = BusCommande.exécuter(message);

            Assert.IsTrue(réponse.Réussite);

            Guid idCompte;

            Assert.IsTrue(Guid.TryParse(réponse.Message, out idCompte));

            IEntrepotUtilisateur entrepotUtilisateur = EntrepotMongo.fabriquerEntrepot<IEntrepotUtilisateur>();
            Utilisateur admin = entrepotUtilisateur.récupérer(idCompte);

            Assert.IsNotNull(admin);
            Assert.AreEqual(TypeUtilisateur.Administrateur, admin.TypeUtilisateur);

            IEntrepotProfile entrepotProfile = EntrepotMongo.fabriquerEntrepot<IEntrepotProfile>();
            Huissier huissier = entrepotProfile.récupérerLHuissier(admin.Profile.Id);

            Assert.IsNotNull(huissier);
        }

        [TestMethod]
        public void TestCreerAdministrateurCommande_utilisateurDeuxFoisLeMêmeJetonFaitEchouerLaCommande()
        {
            ICreerAdministrateurMessage message = initialiserUnMessageValide();
            BusCommande.exécuter(message);
            ReponseCommande réponse = BusCommande.exécuter(message);

            Assert.IsFalse(réponse.Réussite);
            Assert.AreEqual(Ressource.Commandes.JETON_INVALIDE, réponse.Message);
        }

        private ICreerAdministrateurMessage initialiserUnMessageValide()
        {
            ICreerAdministrateurMessage message = new CreerAdministrateurMessageTest();
            message.Jeton = Jeton.générerUnJeton(_email);
            message.Email = _email;
            message.MotDePasse = ConstantesTest.MOT_DE_PASSE_VALIDE.ValeurDéchiffrée;
            message.Civilité = 0;
            message.Nom = "Morane";
            message.Prénom = "Bob";
            message.NomCabinet = "Pouet";
            message.NomVoie = "Poulou";
            message.CodePostal = "33520";
            message.Ville = "Bruges";
            return message;
        }
    }

    public class CreerAdministrateurMessageTest : ICreerAdministrateurMessage
    {
        public string Jeton { get; set; }
        public string Email { get; set; }
        public string MotDePasse { get; set; }
        public int Civilité { get; set; }
        public string Nom { get; set; }
        public string Prénom { get; set; }
        public string NomCabinet { get; set; }
        public string NuméroVoie { get; set; }
        public string RépétitionVoie { get; set; }
        public string NomVoie { get; set; }
        public string ComplémentVoie { get; set; }
        public string CodePostal { get; set; }
        public string Ville { get; set; }
    }
}
