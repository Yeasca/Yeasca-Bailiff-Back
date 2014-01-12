using System;
using System.Collections.Generic;

namespace Yeasca.Metier
{
    public class Utilisateur : IAgregat
    {
        public Utilisateur()
        {
            MotDePasse = new MotDePasse();
            Email = new Email();
            TypeUtilisateur = TypeUtilisateur.Inconnu;
        }

        public Guid Id { get; set; }
        public string Login { get; set; }
        public MotDePasse MotDePasse { get; set; }
        public Email Email { get; set; }
        public TypeUtilisateur TypeUtilisateur { get; set; }
        public Partie Profile { get; set; }

        public bool estValide()
        {
            return aUnLogin()
                && aUnLoginDeLaBonneLongueur()
                && MotDePasse.estValide()
                && Email.estValide();
        }

        private bool aUnLogin()
        {
            return !string.IsNullOrEmpty(Login);
        }

        private bool aUnLoginDeLaBonneLongueur()
        {
            return Login != null && Login.Length <= Ressource.Validation.LONGUEUR_MAX_LOGIN;
        }

        public List<string> obtenirLesErreurs()
        {
            List<string> messages = new List<string>();
            validerLeLogin(messages);
            validerLeMotDePasse(messages);
            validerLEmail(messages);
            return messages;
        }

        private void validerLeLogin(List<string> messages)
        {
            if (!aUnLogin())
                messages.Add(Ressource.Validation.LOGIN_REQUIS);
            else if (!aUnLoginDeLaBonneLongueur())
                messages.Add(Ressource.Validation.LOGIN_LONGUEUR_MAX);
        }

        private void validerLeMotDePasse(List<string> messages)
        {
            if (!MotDePasse.estValide())
                messages.Add(MotDePasse.obtenirLErreur());
        }

        private void validerLEmail(List<string> messages)
        {
            if (!Email.estValide())
                messages.Add(Email.obtenirLErreur());
        }

        public void chargerDepuisLaSession(string idSession)
        {
            SessionUtilisateur session = CacheUtilisateur.Sessions.récupérerLaSession();
            if (session != null)
            {
                Id = session.Utilisateur.Id;
                Login = session.Utilisateur.Login;
                MotDePasse = session.Utilisateur.MotDePasse;
                Email = session.Utilisateur.Email;
                TypeUtilisateur = session.Utilisateur.TypeUtilisateur;
            }
        }

        public void mettreEnSession()
        {
            SessionUtilisateur nouvelleSession = new SessionUtilisateur();
            nouvelleSession.Utilisateur = this;
            CacheUtilisateur.Sessions.ajouterUneSession(nouvelleSession);
        }
    }
}
