using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yeasca.Metier;
using Yeasca.Mongo;

namespace Yeasca.TestsUnitaires.Persistance
{
    [TestClass]
    public class TestEntrepotMongo
    {
        [TestMethod]
        public void TestEntrepotMongo_peutCréerUnEntrepotAvecUnFournisseurDeTest()
        {
            ModuleInjection paramètresDeTest = EntrepotMongo.obtenirLesParamètresParDéfaut();
            paramètresDeTest.lier<IFournisseur>().à<FournisseurTest>();
            EntrepotMongo.paramétrer(paramètresDeTest);
            IEntrepotConstat entrepotDemandé = EntrepotMongo.fabriquerEntrepot<IEntrepotConstat>();

            Assert.IsNotNull(entrepotDemandé);
            
            Constat unConstat = new Constat();
            entrepotDemandé.ajouter(unConstat);

            Assert.IsNotNull(unConstat.Id);
        }

        [TestMethod]
        public void TestEntrepotMongo_tousLesEntrepotsSontFabricables()
        {
            ModuleInjection paramètresDeTest = EntrepotMongo.obtenirLesParamètresParDéfaut();
            paramètresDeTest.lier<IFournisseur>().à<FournisseurTest>();
            EntrepotMongo.paramétrer(paramètresDeTest);
            IEntrepotConstat entrepotConstat = EntrepotMongo.fabriquerEntrepot<IEntrepotConstat>();
            IEntrepotProfile entrepotPartie = EntrepotMongo.fabriquerEntrepot<IEntrepotProfile>();
            IEntrepotUtilisateur entrepotUtilisateur = EntrepotMongo.fabriquerEntrepot<IEntrepotUtilisateur>();
            IEntrepotParametrage entrepotParametrage = EntrepotMongo.fabriquerEntrepot<IEntrepotParametrage>();
            IEntrepotJeton entrepotJeton = EntrepotMongo.fabriquerEntrepot<IEntrepotJeton>();

            Assert.IsNotNull(entrepotConstat);
            Assert.IsNotNull(entrepotPartie);
            Assert.IsNotNull(entrepotUtilisateur);
            Assert.IsNotNull(entrepotParametrage);
            Assert.IsNotNull(entrepotJeton);
        }
    }
}
