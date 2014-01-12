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

            IEntrepotConstat entrepotDemandé = new EntrepotMongo().fabriquerEntrepot<IEntrepotConstat>();

            Assert.IsNotNull(entrepotDemandé);
            
            Constat unConstat = new Constat();
            entrepotDemandé.ajouter(unConstat);

            Assert.IsNotNull(unConstat.Id);
        }
    }
}
