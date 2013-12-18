using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yeasca.Metier;

namespace Yeasca.TestsUnitaires.Outils.Injection
{
    [TestClass]
    public class TestModuleInjection
    {
        [TestMethod]
        public void TestModuleInjection_unModuleVideAUneListeDeDépendanceVide()
        {
            ModuleInjection module = new ModuleInjection();

            Assert.AreEqual(0, module.NombreDeDépendances);
        }

        [TestMethod]
        public void TestModuleInjection_peutAjouterDesInjectionsALaListeDesDépendances()
        {
            ModuleInjection module = new ModuleInjection();
            module.lier<Interface1>().à<Implementation1>();
            module.lier<Interface2>().à<Implementation2>();

            Assert.AreEqual(2, module.NombreDeDépendances);
        }

        [TestMethod]
        public void TestModuleInjection_peutLierUneClasseAElleMême()
        {
            ModuleInjection module = new ModuleInjection();
            module.lier<Implementation1>().àLuiMême();

            Assert.AreEqual(1, module.NombreDeDépendances);
        }

        [TestMethod]
        public void TestModuleInjection_lierDeuxInterfacesLèveUneException()
        {
            try
            {
                ModuleInjection module = new ModuleInjection();
                module.lier<Interface1>().à<Interface1>();
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(InjectionException));
            }
        }

        [TestMethod]
        public void TestModuleInjection_lierUneInterfaceAUneClasseQuiNeLImplémentePasLèveUneException()
        {
            try
            {
                ModuleInjection module = new ModuleInjection();
                module.lier<Interface1>().à<Implementation2>();
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(InjectionException));
            }
        }

        [TestMethod]
        public void TestModuleInjection_lierUneInterfaceAElleMêmeLèveUneException()
        {
            try
            {
                ModuleInjection module = new ModuleInjection();
                module.lier<Interface1>().àLuiMême();
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(InjectionException));
            }
        }

        [TestMethod]
        public void TestModuleInjection_configurerUnModuleChargeLesDépendancesConfiguréesÀLaConstruction()
        {
            ModuleInjection module = new ConfigInjection();

            Assert.AreEqual(1, module.NombreDeDépendances);
        }

        interface Interface1
        {
        }

        interface Interface2
        {
        }

        class Implementation1 : Interface1 { }

        class Implementation2 : Interface2 { }

        class ConfigInjection : ModuleInjection
        {
            public ConfigInjection()
            {
                lier<Interface1>().à<Implementation1>();
            }
        }
    }
}
