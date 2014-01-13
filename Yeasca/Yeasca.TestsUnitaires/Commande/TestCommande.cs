using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yeasca.Commande;
using Yeasca.Metier;

namespace Yeasca.TestsUnitaires.Commande
{
    [TestClass]
    public class TestCommande
    {
        [TestInitialize]
        public void initialiser()
        {
            ConfigurationTest.initialiserLesEntrepotsMongo();    
        }

        [TestMethod]
        public void TestCommande_exécuterUneCommandePourHuissierEchoueSiPasHuissier()
        {
            ConfigurationTest.initialiserLaSessionHTTP(TypeUtilisateur.Inconnu);
            ReponseCommande réponse = new CommandeTestHuissier().exécuter(null);

            Assert.IsFalse(réponse.Réussite);
        }

        [TestMethod]
        public void TestCommande_exécuterUneCommandePourHuissierRéussitSiHuissier()
        {
            ConfigurationTest.initialiserLaSessionHTTP(TypeUtilisateur.Huissier);
            ReponseCommande réponse = new CommandeTestHuissier().exécuter(null);

            Assert.IsTrue(réponse.Réussite);
        }

        [TestMethod]
        public void TestCommande_exécuterUneCommandePourAdministrateurEchoueSiPasAdministrateur()
        {
            ConfigurationTest.initialiserLaSessionHTTP(TypeUtilisateur.Inconnu);
            ReponseCommande réponse = new CommandeTestAdministrateur().exécuter(null);

            Assert.IsFalse(réponse.Réussite);
        }

        [TestMethod]
        public void TestCommande_exécuterUneCommandePourAdministrateurRéussitSiAdministrateur()
        {
            ConfigurationTest.initialiserLaSessionHTTP(TypeUtilisateur.Administrateur);
            ReponseCommande réponse = new CommandeTestAdministrateur().exécuter(null);

            Assert.IsTrue(réponse.Réussite);
        }
        [TestMethod]
        public void TestCommande_exécuterUneCommandePourSuperviseurEchoueSiPasSuperviseur()
        {
            ConfigurationTest.initialiserLaSessionHTTP(TypeUtilisateur.Inconnu);
            ReponseCommande réponse = new CommandeTestSuperviseur().exécuter(null);

            Assert.IsFalse(réponse.Réussite);
        }

        [TestMethod]
        public void TestCommande_exécuterUneCommandePourSuperviseurRéussitSiSuperviseur()
        {
            ConfigurationTest.initialiserLaSessionHTTP(TypeUtilisateur.Superviseur);
            ReponseCommande réponse = new CommandeTestSuperviseur().exécuter(null);

            Assert.IsTrue(réponse.Réussite);
        }
    }

    public class CommandeTestHuissier : Commande<IMessageCommande>
    {
        public override ReponseCommande exécuter(IMessageCommande message)
        {
            if (estUnHuissier())
                return ReponseCommande.générerUnSuccès();
            return ReponseCommande.générerUnEchec(null);
        }
    }

    public class CommandeTestAdministrateur : Commande<IMessageCommande>
    {
        public override ReponseCommande exécuter(IMessageCommande message)
        {
            if (estAdministrateur())
                return ReponseCommande.générerUnSuccès();
            return ReponseCommande.générerUnEchec(null);
        }
    }

    public class CommandeTestSuperviseur : Commande<IMessageCommande>
    {
        public override ReponseCommande exécuter(IMessageCommande message)
        {
            if (estSuperviseur())
                return ReponseCommande.générerUnSuccès();
            return ReponseCommande.générerUnEchec(null);
        }
    }
}
