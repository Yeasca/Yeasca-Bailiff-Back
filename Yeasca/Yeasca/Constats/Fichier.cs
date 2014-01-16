using System;
using System.Configuration;
using System.IO;

namespace Yeasca.Metier
{
    public class Fichier
    {
        public const string DOSSIER_MAITRE_INDEX = "DossierMaitre";
        public const string DOSSIER_FICHIER_INDEX = "DossierFichiers";
        public const string DOSSIER_MAITRE_DEFAUT = "Yeasca.Api";
        public const string DOSSIER_FICHIER_DEFAUT = "\\Fichiers";

        public Fichier(string nom)
        {
            Id = Guid.NewGuid();
            Nom = nom;
            Date = DateTime.Now;
            Utilisateur utilisateurEnCours = Utilisateur.chargerDepuisLaSession();
            if(utilisateurEnCours != null)
                Propriétaire = utilisateurEnCours.Profile;
        }

        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Nom { get; set; }
        public Profile Propriétaire { get; set; }

        public string PathFichier
        {
            get
            {
                string dossierFichier = trouverLeDossierFichiers();
                if (dossierFichier != null)
                    return string.Concat(dossierFichier, "\\", Id.ToString(), obtenirLExtension());
                return null;
            }
        }

        public bool EstUnDocumentWord
        {
            get
            {
                string extension = obtenirLExtension();
                return extension == ".doc" || extension == ".docx";
            }
        }

        public bool EstUnFichierAudio
        {
            get
            {
                string extension = obtenirLExtension();
                return extension == ".wav" || extension == ".mp3" || extension == ".mp4";
            }
        }

        public bool EstUnFichierImage
        {
            get
            {
                string extension = obtenirLExtension();
                return extension == ".png" || extension == ".jpg" || extension == ".jpeg" || extension == ".gif";
            }
        }

        public string TypeDuFichier
        {
            get
            {
                if (EstUnDocumentWord)
                    return "Word";
                if (EstUnFichierAudio)
                    return "Audio";
                if (EstUnFichierImage)
                    return "Image";
                return "Autre";
            }
        }

        public string TypeMIMEDuFichier
        {
            get
            {
                string extension = obtenirLExtension();
                if (extension == ".doc")
                    return "application/msword";
                if (extension == ".docx")
                    return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                if (extension == ".wav")
                    return "audio/x-wav";
                if (extension == ".mp3" || extension == ".mp4")
                    return "audio/mpeg";
                if (extension == ".png")
                    return "image/png";
                if (extension == ".jpg" || extension == ".jpeg")
                    return "image/jpeg";
                if (extension == ".gif")
                    return "image/gif";
               return "text/html";
            }
        }

        private string obtenirLExtension()
        {
            return Nom.Substring(Nom.LastIndexOf('.'));
        }

        public void supprimer()
        {
            string cheminDuFichier = PathFichier;
            if(PathFichier != null)
                File.Delete(cheminDuFichier);
        }

        public static Fichier enregistrerLeFichier(MemoryStream fichierAEnregistrer, string nom)
        {
            try
            {
                Fichier fichier = new Fichier(nom);
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
            catch(Exception e)
            {
                Log.loguer("Erreur lors de l'enregistrement sur le disque d'un fichier", e);
                return null;   
            }
        }

        public static Fichier enregistrerLeDocumentWord(MemoryStream fichierAEnregistrer, string nom)
        {
            Fichier fichier = new Fichier(nom);
            if (fichier.EstUnDocumentWord)
                return enregistrerLeFichier(fichierAEnregistrer, nom);
            return null;
        }

        public static Fichier enregistrerLeFichierImageOuAudio(MemoryStream fichierAEnregistrer, string nom)
        {
            Fichier fichier = new Fichier(nom);
            if (fichier.EstUnFichierAudio || fichier.EstUnFichierImage)
                return enregistrerLeFichier(fichierAEnregistrer, nom);
            return null;
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
                string dossierCourant = AppDomain.CurrentDomain.BaseDirectory;
                for (int i = 0; i < 4 && !dossierCourant.EndsWith(dossierMaitre); i++)
                    dossierCourant = Directory.GetParent(dossierCourant).FullName;
                if (dossierCourant.EndsWith(dossierMaitre))
                    return récupérerLeDossierFichier(dossierFichier, dossierCourant);
                throw new DirectoryNotFoundException("Dossier maître introuvable après 4 itérations");
            }
            catch (Exception e)
            {
                Log.loguer("Impossible de trouer le dossier des fichiers téléchargés", e);
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
