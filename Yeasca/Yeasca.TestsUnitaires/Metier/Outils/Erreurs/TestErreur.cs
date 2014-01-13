using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yeasca.Metier;

namespace Yeasca.TestsUnitaires.Metier.Erreurs
{
    [TestClass]
    public class TestErreur
    {
        [TestMethod]
        public void TestErreur_uneErreurVideRenvoieUnHTMLVide()
        {
            Erreur erreur = new Erreur();

            Assert.IsTrue(string.IsNullOrEmpty(erreur.donnerLaListeEnHTML()));
        }

        [TestMethod]
        public void TestErreur_uneErreurRemplieRenvoieLeBonHTML()
        {
            Erreur erreur = new Erreur();
            erreur.ajouterUneErreur("Pouet");
            erreur.ajouterUneErreur("Poulou");
            string HTMLAttendu = "<ul><li>Pouet</li><li>Poulou</li></ul>";

            Assert.AreEqual(HTMLAttendu, erreur.donnerLaListeEnHTML());
        }
    }
}
