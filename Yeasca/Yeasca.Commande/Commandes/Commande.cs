using System;
using System.Collections.Generic;
using Yeasca.Metier;

namespace Yeasca.Commande
{
    public abstract class Commande<T> : ICommande<T>, ICommande where T : IMessageCommande
    {
        public abstract ReponseCommande exécuter(T message);

        public Type Message
        {
            get
            {
                return typeof (T);
            }
        }

        protected bool estUnHuissier()
        {
            Utilisateur utilisateurEnCours = Utilisateur.chargerDepuisLaSession();
            return utilisateurEnCours != null 
                && (utilisateurEnCours.TypeUtilisateur == TypeUtilisateur.Huissier
                    || utilisateurEnCours.TypeUtilisateur == TypeUtilisateur.Administrateur
                    || utilisateurEnCours.TypeUtilisateur == TypeUtilisateur.Superviseur);
        }

        protected bool estAdministrateur()
        {
            Utilisateur utilisateurEnCours = Utilisateur.chargerDepuisLaSession();
            return utilisateurEnCours != null 
                && (utilisateurEnCours.TypeUtilisateur == TypeUtilisateur.Administrateur
                    || utilisateurEnCours.TypeUtilisateur == TypeUtilisateur.Superviseur);
        }

        protected bool estSuperviseur()
        {
            Utilisateur utilisateurEnCours = Utilisateur.chargerDepuisLaSession();
            return utilisateurEnCours != null && utilisateurEnCours.TypeUtilisateur == TypeUtilisateur.Superviseur;
        }

        protected bool estAuthentifié()
        {
            Utilisateur utilisateurEnCours = Utilisateur.chargerDepuisLaSession();
            return utilisateurEnCours != null && utilisateurEnCours.TypeUtilisateur != TypeUtilisateur.Inconnu;
        }

        protected string convertirLesErreursAuFormatHTML(List<string> erreurs)
        {
            string erreursHTML = "<ul>";
            foreach (string erreur in erreurs)
                erreursHTML = string.Concat(erreursHTML, "<li>", erreur, "</li>");
            erreursHTML = string.Concat(erreursHTML, "</ul>");
            return erreursHTML;
        }
    }
}
