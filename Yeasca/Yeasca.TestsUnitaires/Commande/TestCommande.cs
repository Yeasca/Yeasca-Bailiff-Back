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
        public void initialiserLaSessionHTTP()
        {
            HttpContext.Current = ConstantesTest.CONTEXTE_HTTP;
            CacheUtilisateur.réinitialiserLeCacheUtilisateur();
        }

        [TestMethod]
        public void TestCommande_exécuterUneCommandePourHuissierEchoueSiPasHuissier()
        {
            Utilisateur unUtilisateurInconnu = new Utilisateur();
            unUtilisateurInconnu.mettreEnSession();
            ReponseCommande réponse = new CommandeTestHuissier().exécuter(null);

            Assert.IsFalse(réponse.Réussite);
        }

        [TestMethod]
        public void TestCommande_exécuterUneCommandePourHuissierRéussitSiHuissier()
        {
            Utilisateur unUtilisateurHuissier = new Utilisateur();
            unUtilisateurHuissier.TypeUtilisateur = TypeUtilisateur.Huissier;
            unUtilisateurHuissier.mettreEnSession();
            ReponseCommande réponse = new CommandeTestHuissier().exécuter(null);

            Assert.IsTrue(réponse.Réussite);
        }

        [TestMethod]
        public void TestCommande_exécuterUneCommandePourAdministrateurEchoueSiPasAdministrateur()
        {
            Utilisateur unUtilisateurInconnu = new Utilisateur();
            unUtilisateurInconnu.mettreEnSession();
            ReponseCommande réponse = new CommandeTestAdministrateur().exécuter(null);

            Assert.IsFalse(réponse.Réussite);
        }

        [TestMethod]
        public void TestCommande_exécuterUneCommandePourAdministrateurRéussitSiAdministrateur()
        {
            Utilisateur unUtilisateurHuissier = new Utilisateur();
            unUtilisateurHuissier.TypeUtilisateur = TypeUtilisateur.Administrateur;
            unUtilisateurHuissier.mettreEnSession();
            ReponseCommande réponse = new CommandeTestAdministrateur().exécuter(null);

            Assert.IsTrue(réponse.Réussite);
        }
        [TestMethod]
        public void TestCommande_exécuterUneCommandePourSuperviseurEchoueSiPasSuperviseur()
        {
            Utilisateur unUtilisateurInconnu = new Utilisateur();
            unUtilisateurInconnu.mettreEnSession();
            ReponseCommande réponse = new CommandeTestSuperviseur().exécuter(null);

            Assert.IsFalse(réponse.Réussite);
        }

        [TestMethod]
        public void TestCommande_exécuterUneCommandePourSuperviseurRéussitSiSuperviseur()
        {
            Utilisateur unUtilisateurHuissier = new Utilisateur();
            unUtilisateurHuissier.TypeUtilisateur = TypeUtilisateur.Superviseur;
            unUtilisateurHuissier.mettreEnSession();
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
