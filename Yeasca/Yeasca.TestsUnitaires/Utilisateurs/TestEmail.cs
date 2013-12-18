using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yeasca.Metier;

namespace Yeasca.TestsUnitaires.Utilisateurs
{
    [TestClass]
    public class TestEmail
    {
        [TestMethod]
        public void TestEmail_unEmailVideEstInvalide()
        {
            Email unEmailVide = new Email();

            Assert.IsFalse(unEmailVide.estValide());
        }

        [TestMethod]
        public void TestEmail_unEmailValideEstValide()
        {
            Email unEmailValide = ConstantesTest.EMAIL_VALIDE;

            Assert.IsTrue(unEmailValide.estValide());
        }

        [TestMethod]
        public void TestEmail_unEmailVideRenvoieLeBonMessageDErreur()
        {
            Email unEmailVide = new Email();
            string messageDErreur = unEmailVide.obtenirLErreur();

            Assert.AreEqual(Ressource.Validation.EMAIL_REQUIS, messageDErreur);
        }

        [TestMethod]
        public void TestEmail_unEmailTropLongRenvoieLeBonMessageDErreur()
        {
            Email unEmailTropLong = new Email(ConstantesTest.CHAINE_DE_256);
            string messageDErreur = unEmailTropLong.obtenirLErreur();

            Assert.AreEqual(Ressource.Validation.EMAIL_LONGUEUR_MAX, messageDErreur);
        }

        [TestMethod]
        public void TestEmail_unEmailInvalideRenvoieLeBonMessageDErreur()
        {
            Email unEmailInvalide = new Email("pouetpoulouloucom");
            string messageDErreur = unEmailInvalide.obtenirLErreur();

            Assert.AreEqual(Ressource.Validation.EMAIL_INVALIDE, messageDErreur);
        }
    }
}
