using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yeasca.Metier;

namespace Yeasca.TestsUnitaires.Metier.Outils.Logs
{
    [TestClass]
    public class TestLog
    {
        [TestMethod]
        public void TestLog_peutLoggerUnMessage()
        {
            Log.loguer("pouet", new Exception());
            string cheminLog = Log.cheminDuLog();

            Assert.IsTrue(File.Exists(cheminLog));

            string texte = File.ReadAllText(cheminLog);
            Assert.IsTrue(texte.Contains("pouet"));

            File.Delete(cheminLog);
        }
    }
}
