using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yeasca.Metier;
using Yeasca.Requete;

namespace Yeasca.TestsUnitaires.Requete
{
    [TestClass]
    public class TestRequete
    {
        [TestInitialize]
        public void initialiser()
        {
            ConfigurationTest.initialiserLesEntrepotsMongo();
        }

        [TestMethod]
        public void TestRequete_exécuterUneRequetePourHuissierEchoueSiPasHuissier()
        {
            ConfigurationTest.initialiserLaSessionHTTP(TypeUtilisateur.Inconnu);
            ReponseRequete réponse = new RequeteTestHuissier().exécuter(null);

            Assert.IsFalse(réponse.Réussite);
        }

        [TestMethod]
        public void TestRequete_exécuterUneRequetePourHuissierRéussitSiHuissier()
        {
            ConfigurationTest.initialiserLaSessionHTTP(TypeUtilisateur.Huissier);
            ReponseRequete réponse = new RequeteTestHuissier().exécuter(null);

            Assert.IsTrue(réponse.Réussite);
        }

        [TestMethod]
        public void TestRequete_exécuterUneRequetePourAdministrateurEchoueSiPasAdministrateur()
        {
            ConfigurationTest.initialiserLaSessionHTTP(TypeUtilisateur.Inconnu);
            ReponseRequete réponse = new RequeteTestAdministrateur().exécuter(null);

            Assert.IsFalse(réponse.Réussite);
        }

        [TestMethod]
        public void TestRequete_exécuterUneRequetePourAdministrateurRéussitSiAdministrateur()
        {
            ConfigurationTest.initialiserLaSessionHTTP(TypeUtilisateur.Administrateur);
            ReponseRequete réponse = new RequeteTestAdministrateur().exécuter(null);

            Assert.IsTrue(réponse.Réussite);
        }
        [TestMethod]
        public void TestRequete_exécuterUneRequetePourSuperviseurEchoueSiPasSuperviseur()
        {
            ConfigurationTest.initialiserLaSessionHTTP(TypeUtilisateur.Inconnu);
            ReponseRequete réponse = new RequeteTestSuperviseur().exécuter(null);

            Assert.IsFalse(réponse.Réussite);
        }

        [TestMethod]
        public void TestRequete_exécuterUneRequetePourSuperviseurRéussitSiSuperviseur()
        {
            ConfigurationTest.initialiserLaSessionHTTP(TypeUtilisateur.Superviseur);
            ReponseRequete réponse = new RequeteTestSuperviseur().exécuter(null);

            Assert.IsTrue(réponse.Réussite);
        }
    }

    public class RequeteTestHuissier : Requete<IMessageRequete>
    {
        public override ReponseRequete exécuter(IMessageRequete message)
        {
            if (estUnHuissier())
                return ReponseRequete.générerUnSuccès(null);
            return ReponseRequete.générerUnEchec(null);
        }
    }

    public class RequeteTestAdministrateur : Requete<IMessageRequete>
    {
        public override ReponseRequete exécuter(IMessageRequete message)
        {
            if (estAdministrateur())
                return ReponseRequete.générerUnSuccès(null);
            return ReponseRequete.générerUnEchec(null);
        }
    }

    public class RequeteTestSuperviseur : Requete<IMessageRequete>
    {
        public override ReponseRequete exécuter(IMessageRequete message)
        {
            if (estSuperviseur())
                return ReponseRequete.générerUnSuccès(null);
            return ReponseRequete.générerUnEchec(null);
        }
    }
}
