using System;
using Yeasca.Metier;

namespace Yeasca.Requete
{
    public abstract class Requete<T> : IRequete<T>, IRequete where T : IMessageRequete
    {
        public abstract ReponseRequete exécuter(T message);

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

        public Type Message { get { return typeof (T); } }
    }
}
