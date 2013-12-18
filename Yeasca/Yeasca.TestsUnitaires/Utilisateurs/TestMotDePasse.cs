using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yeasca.Metier;

namespace Yeasca.TestsUnitaires.Utilisateurs
{
    [TestClass]
    public class TestMotDePasse
    {
        [TestMethod]
        public void TestMotDePasse_unMotDePasseVideEstInvalide()
        {
            MotDePasse motDePasseVide = new MotDePasse();

            Assert.IsFalse(motDePasseVide.estValide());
        }

        [TestMethod]
        public void TestMotDePasse_unMotDePasseIncorrectEstInvalide()
        {
            MotDePasse motDePasseIncorrect = new MotDePasse("pouet");

            Assert.IsFalse(motDePasseIncorrect.estValide());
        }

        [TestMethod]
        public void TestMotDePasse_unMotDePasseValideEstValide()
        {
            MotDePasse motDePasseValide = ConstantesTest.MOT_DE_PASSE_VALIDE;

            Assert.IsTrue(motDePasseValide.estValide());
        }

        [TestMethod]
        public void TestMotDePasse_unMotDePasseVideRenvoieLeBonMessageDErreur()
        {
            MotDePasse motDePasseVide = new MotDePasse();
            string messageDErreur = motDePasseVide.obtenirLErreur();

            Assert.AreEqual(Ressource.Validation.MOT_DE_PASSE_REQUIS, messageDErreur);
        }

        [TestMethod]
        public void TestMotDePasse_unMotDePasseTropCourtRenvoieLeBonMessageDErreur()
        {
            MotDePasse motDePasseTropCourt = new MotDePasse(ConstantesTest.CHAINE_DE_6);
            string messageDErreur = motDePasseTropCourt.obtenirLErreur();

            Assert.AreEqual(Ressource.Validation.MOT_DE_PASSE_LONGUEUR_MIN, messageDErreur);
        }

        [TestMethod]
        public void TestMotDePasse_unMotDePasseTropLongRenvoieLeBonMessageDErreur()
        {
            MotDePasse motDePasseTropLong = new MotDePasse(ConstantesTest.CHAINE_DE_256);
            string messageDErreur = motDePasseTropLong.obtenirLErreur();

            Assert.AreEqual(Ressource.Validation.MOT_DE_PASSE_LONGUEUR_MAX, messageDErreur);
        }

        [TestMethod]
        public void TestMotDePasse_unMotDePassePasAssezComplexeRenvoieLeBonMessageDErreur()
        {
            MotDePasse motDePassePeuComplexe = new MotDePasse("admin123");
            string messageDErreur = motDePassePeuComplexe.obtenirLErreur();

            Assert.AreEqual(Ressource.Validation.MOT_DE_PASSE_COMPLEXE, messageDErreur);
        }
    }
}
