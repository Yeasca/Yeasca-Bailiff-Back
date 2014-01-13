using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yeasca.Metier;

namespace Yeasca.TestsUnitaires.Outils.Injection
{
    [TestClass]
    public class TestInjecteur
    {
        [TestMethod]
        public void TestInjecteur_peutRésoudreUneInjection()
        {
            Injecteur injecteur = new Injecteur(new TestModuleInjection());
            Interface1 implémentation = injecteur.construire<Interface1>();

            Assert.IsNotNull(implémentation);
            Assert.IsInstanceOfType(implémentation, typeof(Implementation1));
        }

        [TestMethod]
        public void TestInjecteur_peutRésoudreUneChaineDInjection()
        {
            Injecteur injecteur = new Injecteur(new TestModuleInjection());
            Interface2 implémentation = injecteur.construire<Interface2>();

            Assert.IsNotNull(implémentation);
            Assert.IsInstanceOfType(implémentation, typeof(Implementation2));
            Assert.IsNotNull(implémentation.Service);
            Assert.IsInstanceOfType(implémentation.Service, typeof(Implementation1));
        }

        [TestMethod]
        public void TestInjecteur_peutRésoudreUneClasseLiéeAElleMême()
        {
            Injecteur injecteur = new Injecteur(new TestModuleInjection());
            Implementation1 objet = injecteur.construire<Implementation1>();

            Assert.IsNotNull(objet);
        }

        [TestMethod]
        public void TestInjecteur_peutRésoudreLeConstructeurSouhaité()
        {
            Injecteur injecteur = new Injecteur(new TestModuleInjection());
            Implementation3 objet = injecteur.construire<Implementation3>();

            Assert.IsNotNull(objet);
            Assert.IsNotNull(objet.Service);
            Assert.IsInstanceOfType(objet.Service, typeof(Implementation1));
        }

        [TestMethod]
        public void TestInjecteur_peutInjecterEnSingleton()
        {
            string chaineDeTest = "Pouet";
            ModuleInjection module = new ModuleInjection();
            module.lier<Interface1>().à<Implementation1>().enSingleton();
            Injecteur injecteur = new Injecteur(module);
            Interface1 objet = injecteur.construire<Interface1>();
            objet.Test = chaineDeTest;
            Interface1 objetRappelé = injecteur.construire<Interface1>();

            Assert.AreEqual(chaineDeTest, objetRappelé.Test);
        }

        interface Interface1
        {
            string Test { get; set; }
        }

        interface Interface2
        {
            Interface1 Service { get; }
        }

        class TestModuleInjection : ModuleInjection
        {
            public TestModuleInjection()
            {
                lier<Interface1>().à<Implementation1>();
                lier<Interface2>().à<Implementation2>();

                lier<Implementation1>().àLuiMême();
                lier<Implementation3>().àLuiMême();
            }
        }

        class Implementation1 : Interface1
        {
            public string Test { get; set; }
        }

        class Implementation2 : Interface2
        {
            public Implementation2(Interface1 service)
            {
                Service = service;
            }

            public Interface1 Service { get; private set; }
        }

        class Implementation3
        {
            public Implementation3() { }

            [Injecter]
            public Implementation3(Interface1 service)
            {
                Service = service;
            }

            public Interface1 Service { get; private set; }
        }
    }
}
