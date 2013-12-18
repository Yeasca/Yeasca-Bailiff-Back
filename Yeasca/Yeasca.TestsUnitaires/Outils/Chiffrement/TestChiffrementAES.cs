using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yeasca.Metier;

namespace Yeasca.TestsUnitaires.Outils.Chiffrement
{
    [TestClass]
    public class TestChiffrementAES
    {
        [TestMethod]
        public void TestChiffrementAES_peutChiffrerEtDéchifferUneChaine()
        {
            string chaineInitiale = "pouet0@1";
            ChiffrementAES decrypteur = new ChiffrementAES();
            byte[] chaineChiffrée = decrypteur.crypter(chaineInitiale);

            Assert.IsNotNull(chaineChiffrée);
            Assert.AreNotEqual(chaineInitiale, Encoding.Default.GetString(chaineChiffrée));

            string chaineDéchiffrée = decrypteur.décrypter(chaineChiffrée);
            Assert.AreEqual(chaineInitiale, chaineDéchiffrée);
        }
    }
}
