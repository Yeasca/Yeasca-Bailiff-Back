using System;
using System.Collections.Generic;
using System.Runtime.Caching;

namespace Yeasca.Metier
{
    public class Utilisateur : IAgregat
    {
        public Utilisateur()
        {
            MotDePasse = new MotDePasse();
            Email = new Email();
            Profile = new Huissier();
            TypeUtilisateur = TypeUtilisateur.Inconnu;
            EstActivé = true;
        }

        public Guid Id { get; set; }
        public MotDePasse MotDePasse { get; set; }
        public Email Email { get; set; }
        public TypeUtilisateur TypeUtilisateur { get; set; }
        public bool EstActivé { get; set; }
        public Profile Profile { get; set; }

        public bool estValide()
        {
            return aUnEmail()
                   && Email.estValide()
                   && MotDePasse.estValide();
        }

        private bool aUnEmail()
        {
            return Email != null;
        }

        public Erreur obtenirLesErreurs()
        {
            Erreur messages = new Erreur();
            validerLeMotDePasse(messages);
            validerLEmail(messages);
            return messages;
        }

        private void validerLeMotDePasse(Erreur messages)
        {
            if (!MotDePasse.estValide())
                messages.ajouterUneErreur(MotDePasse.obtenirLErreur());
        }

        private void validerLEmail(Erreur messages)
        {
            if (!Email.estValide())
                messages.ajouterUneErreur(Email.obtenirLErreur());
        }

        public static Utilisateur chargerDepuisLaSession()
        {
            CacheUtilisateur.initialiserLeCacheUtilisateur();
            SessionUtilisateur session = CacheUtilisateur.Sessions.récupérerLaSession();
            if (session != null)
            {
                Utilisateur utilisateur = new Utilisateur();
                utilisateur.Id = session.Utilisateur.Id;
                utilisateur.MotDePasse = session.Utilisateur.MotDePasse;
                utilisateur.Email = session.Utilisateur.Email;
                utilisateur.TypeUtilisateur = session.Utilisateur.TypeUtilisateur;
                return utilisateur;
            }
            return null;
        }

        public void mettreEnSession()
        {
            CacheUtilisateur.initialiserLeCacheUtilisateur();
            SessionUtilisateur nouvelleSession = new SessionUtilisateur();
            nouvelleSession.Utilisateur = this;
            CacheUtilisateur.Sessions.ajouterUneSession(nouvelleSession);
        }
    }
}
