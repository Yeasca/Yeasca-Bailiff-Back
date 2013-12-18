using System;
using System.Collections.Generic;

namespace Yeasca.Metier
{
    public abstract class Partie
    {
        protected Dictionary<Abreviation, string> _chaineParAbréviation = new Dictionary<Abreviation, string>()
        {
            {Abreviation.Mademoiselle, "Mlle"},
            {Abreviation.Madame, "Mme"},
            {Abreviation.Monsieur, "M."}
        }; 

        public Partie()
        {
        }

        public Partie(string nom, string prénom)
        {
            Nom = nom;
            Prénom = prénom;
        }

        public Guid ID { get; set; }
        public Abreviation Abréviation { get; set; }
        public string Nom { get; set; }
        public string Prénom { get; set; }

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

        public virtual List<string> obtenirLesErreurs()
        {
            List<string> message = new List<string>();
            validerLeNom(message);
            validerLePrénom(message);
            return message;
        }

        private void validerLeNom(List<string> message)
        {
            if (string.IsNullOrEmpty(Nom))
                message.Add(Ressource.Validation.NOM_REQUIS);
            else if (Nom.Length > Ressource.Validation.LONGUEUR_MAX_NOM)
                message.Add(Ressource.Validation.NOM_LONGUEUR_MAX);
        }

        private void validerLePrénom(List<string> message)
        {
            if (string.IsNullOrEmpty(Prénom))
                message.Add(Ressource.Validation.PRÉNOM_REQUIS);
            else if (Prénom.Length > Ressource.Validation.LONGUEUR_MAX_PRÉNOM)
                message.Add(Ressource.Validation.PRÉNOM_LONGUEUR_MAX);
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
