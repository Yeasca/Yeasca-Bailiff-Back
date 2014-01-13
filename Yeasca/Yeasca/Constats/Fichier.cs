using System;
using System.Configuration;
using System.IO;
using System.Reflection;

namespace Yeasca.Metier
{
    public class Fichier
    {
        public const string DOSSIER_MAITRE_INDEX = "DossierMaitre";
        public const string DOSSIER_FICHIER_INDEX = "DossierFichiers";
        public const string DOSSIER_MAITRE_DEFAUT = "Yeasca";
        public const string DOSSIER_FICHIER_DEFAUT = "\\Fichiers";
        public const string RACINE = "RacineWeb";

        public Fichier(string nom, string extension)
        {
            Id = Guid.NewGuid();
            Nom = nom;
            Extension = extension;
            Utilisateur utilisateurEnCours = Utilisateur.chargerDepuisLaSession();
            if(utilisateurEnCours != null)
                Propriétaire = utilisateurEnCours.Profile;
        }

        public Guid Id { get; set; }
        public string Nom { get; set; }
        public string Extension { get; set; }
        public Profile Propriétaire { get; set; }

        public string PathFichier
        {
            get
            {
                string dossierFichier = trouverLeDossierFichiers();
                if (dossierFichier != null)
                {
                    return string.Concat(dossierFichier, "\\", Id.ToString(), Extension);
                }
                return null;
            }
        }

        public string URLFichier
        {
            get
            {
                string racine = ConfigurationManager.AppSettings[RACINE];
                return string.Concat(racine, Nom, Extension);
            }
        }

        public string NomComplet
        {
            get
            {
                return string.Concat(Nom, Extension);
            }
        }

        public bool EstUnDocumentWord
        {
            get
            {
                return Extension == ".doc" || Extension == ".docx";
            }
        }

        public void supprimer()
        {
            string cheminDuFichier = PathFichier;
            if(PathFichier != null)
                File.Delete(cheminDuFichier);
        }

        public static Fichier enregistrerLeFichier(MemoryStream fichierAEnregistrer, string nom, string extension)
        {
            try
            {
                Fichier fichier = new Fichier(nom, extension);
                string cheminFichier = fichier.PathFichier;
                if (cheminFichier != null)
                {
                    byte[] buffer = fichierAEnregistrer.ToArray();
                    FileStream flux = File.OpenWrite(cheminFichier);
                    flux.Write(buffer, 0, buffer.Length);
                    fermerLeFlux(flux);
                    fermerLeFlux(fichierAEnregistrer);
                    return fichier;
                }
                return null;
            }
            catch
            {
                //TODO : log
                return null;   
            }
        }

        private static void fermerLeFlux(Stream flux)
        {
            flux.Close();
            flux.Dispose();
        }

        public static string trouverLeDossierFichiers()
        {
            try
            {
                string dossierMaitre = ConfigurationManager.AppSettings[DOSSIER_MAITRE_INDEX] ?? DOSSIER_MAITRE_DEFAUT;
                string dossierFichier = ConfigurationManager.AppSettings[DOSSIER_FICHIER_INDEX] ?? DOSSIER_FICHIER_DEFAUT;
                string dossierCourant = Environment.CurrentDirectory;
                for (int i = 0; i < 4 && !dossierCourant.EndsWith(dossierMaitre); i++)
                    dossierCourant = Directory.GetParent(dossierCourant).FullName;
                if (dossierCourant.EndsWith(dossierMaitre))
                    return récupérerLeDossierFichier(dossierFichier, dossierCourant);
                throw new DirectoryNotFoundException("Dossier maître introuvable après 4 itérations");
            }
            catch (Exception)
            {
                //TODO : log
                return null;
            }
        }

        private static string récupérerLeDossierFichier(string dossierFichier, string dossierCourant)
        {
            dossierFichier = string.Concat(dossierCourant, dossierFichier);
            if (!Directory.Exists(dossierFichier))
            {
                Directory.CreateDirectory(dossierFichier);
            }
            return dossierFichier;
        }
    }
}
