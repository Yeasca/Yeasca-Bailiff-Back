using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yeasca.Metier;

namespace Yeasca.TestsUnitaires.Constats
{
    [TestClass]
    public class TestSIRET
    {
        [TestMethod]
        public void TestSIRET_unNuméroSIRETVideEstInvalide()
        {
            SIRET numéroSIRET = new SIRET();

            Assert.IsFalse(numéroSIRET.estValide());
        }

        [TestMethod]
        public void TestSIRET_unNuméroSIRETInvalideEstInvalide()
        {
            SIRET numéroSIRETInvalide = new SIRET("6518611700078");

            Assert.IsFalse(numéroSIRETInvalide.estValide());

            numéroSIRETInvalide = new SIRET("6511700078");

            Assert.IsFalse(numéroSIRETInvalide.estValide());

            numéroSIRETInvalide = new SIRET("6518611abc00078");

            Assert.IsFalse(numéroSIRETInvalide.estValide());
        }

        [TestMethod]
        public void TestSIRET_unNuméroSIRETValideEstValide()
        {
            SIRET numéroSIRETValide = ConstantesTest.SIRET_VALIDE;

            Assert.IsTrue(numéroSIRETValide.estValide());
        }

        [TestMethod]
        public void TestSIRET_unNuméroSIRETVideEstVide()
        {
            SIRET numéroSIRETVide = new SIRET();

            Assert.IsTrue(numéroSIRETVide.estVide());
        }

        [TestMethod]
        public void TestSIRET_unNuméroSIRETAvecUneValeurNEstPasVide()
        {
            SIRET numéroSIRETAvecValeur = new SIRET("123");

            Assert.IsFalse(numéroSIRETAvecValeur.estVide());
        }
    }
}
