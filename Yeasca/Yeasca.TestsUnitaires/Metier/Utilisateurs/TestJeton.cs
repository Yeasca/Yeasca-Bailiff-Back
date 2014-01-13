using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yeasca.Metier;

namespace Yeasca.TestsUnitaires.Metier.Utilisateurs
{
    [TestClass]
    public class TestJeton
    {
        [TestMethod]
        public void TestJeton_peutGénérerUnJeton()
        {
            string jeton = Jeton.générerUnJeton("pouet");

            Assert.IsNotNull(jeton);
        }

        [TestMethod]
        public void TestJeton_peutVérifierLeJeton()
        {
            string clé = "Pouet";
            string jeton = Jeton.générerUnJeton(clé);
            Jeton jetonAValider = new Jeton(jeton);

            Assert.IsTrue(jetonAValider.estValide(clé));
        }
    }
}
