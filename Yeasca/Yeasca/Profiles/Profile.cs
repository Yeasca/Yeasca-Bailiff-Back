using System;
using System.Collections.Generic;

namespace Yeasca.Metier
{
    public abstract class Profile : IAgregat
    {
        protected Dictionary<Abreviation, string> _chaineParAbréviation = new Dictionary<Abreviation, string>()
        {
            {Abreviation.Mademoiselle, "Mlle"},
            {Abreviation.Madame, "Mme"},
            {Abreviation.Monsieur, "M."}
        }; 

        public Profile()
        {
        }

        public Profile(string nom, string prénom)
        {
            Nom = nom;
            Prénom = prénom;
        }

        public Profile(string nom, string prénom, string dénominationEntreprise)
        {
            Nom = nom;
            Prénom = prénom;
            DénominationEntreprise = dénominationEntreprise;
        }

        public Guid Id { get; set; }
        public Abreviation Abréviation { get; set; }
        public string Nom { get; set; }
        public string Prénom { get; set; }
        public string DénominationEntreprise { get; set; }
        public Adresse Adresse { get; set; }

        public virtual bool estValide()
        {
            return aUnNomCorrect()
                && aUnPrénomCorrect();
        }

        protected bool aUnNomCorrect()
        {
            return !string.IsNullOrEmpty(Nom)
                && Nom.Length <= Ressource.Validation.LONGUEUR_MAX_NOM;
        }

        protected bool aUnPrénomCorrect()
        {
            return !string.IsNullOrEmpty(Prénom)
                && Prénom.Length <= Ressource.Validation.LONGUEUR_MAX_PRÉNOM;
        }

        public virtual Erreur obtenirLesErreurs()
        {
            Erreur message = new Erreur();
            validerLeNom(message);
            validerLePrénom(message);
            return message;
        }

        private void validerLeNom(Erreur message)
        {
            if (string.IsNullOrEmpty(Nom))
                message.ajouterUneErreur(Ressource.Validation.NOM_REQUIS);
            else if (Nom.Length > Ressource.Validation.LONGUEUR_MAX_NOM)
                message.ajouterUneErreur(Ressource.Validation.NOM_LONGUEUR_MAX);
        }

        private void validerLePrénom(Erreur message)
        {
            if (string.IsNullOrEmpty(Prénom))
                message.ajouterUneErreur(Ressource.Validation.PRÉNOM_REQUIS);
            else if (Prénom.Length > Ressource.Validation.LONGUEUR_MAX_PRÉNOM)
                message.ajouterUneErreur(Ressource.Validation.PRÉNOM_LONGUEUR_MAX);
        }

        protected bool aUneEntreprise()
        {
            return !string.IsNullOrEmpty(DénominationEntreprise);
        }

        protected virtual bool nAPasLesInformationsNécessaires()
        {
            return string.IsNullOrEmpty(Nom)
                || string.IsNullOrEmpty(Prénom)
                || string.IsNullOrEmpty(Adresse.enChaine());
        }

        public abstract string obtenirLaDescription();
    }

    public enum Abreviation
    {
        Mademoiselle = 0,
        Madame = 1,
        Monsieur = 2
    }
}
