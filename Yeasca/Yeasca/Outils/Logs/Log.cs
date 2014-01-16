using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace Yeasca.Metier
{
    public class Log
    {
        public const string DOSSIER_LOG_INDEX = "DossierLogs";
        public const string DOSSIER_LOG_DEFAUT = "\\Logs";
        public const string NOM_LOG = "\\Log.txt";

        public static void loguer(string message, Exception exception)
        {
            try
            {
                string cheminLog = cheminDuLog();
                string log = string.Concat(DateTime.Now.ToString(Ressource.Paramètres.FORMAT_DATE), " : ", message, Environment.NewLine, exception.Message, Environment.NewLine, exception.StackTrace, Environment.NewLine);
                File.AppendAllText(cheminLog, log);
            }
            catch
            {
                //TODO : là, on est mal
            }
        }

        public static string cheminDuLog()
        {
            string dossierFichier = ConfigurationManager.AppSettings[DOSSIER_LOG_INDEX] ?? DOSSIER_LOG_DEFAUT;
            string dossierCourant = AppDomain.CurrentDomain.BaseDirectory;
            dossierCourant = Directory.GetParent(dossierCourant).FullName;
            dossierFichier = string.Concat(dossierCourant, dossierFichier);
            if (!Directory.Exists(dossierFichier))
                Directory.CreateDirectory(dossierFichier);
            string cheminLog = string.Concat(dossierFichier, NOM_LOG);
            return cheminLog;
        }
    }
}
