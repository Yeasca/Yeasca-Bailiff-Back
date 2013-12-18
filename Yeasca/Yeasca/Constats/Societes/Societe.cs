using System;
using System.Collections.Generic;

namespace Yeasca.Metier
{
    public class Societe
    {
        public Societe()
        {
            initialisation();
        }

        public Societe(string dénomination)
        {
            initialisation();
            Dénomination = dénomination;
        }

        private void initialisation()
        {
            NuméroSIRET = new SIRET();
            Adresse = new Adresse();
        }

        public Guid ID { get; set; }
        public string Dénomination { get; set; }
        public SIRET NuméroSIRET { get; set; }
        public Adresse Adresse { get; set; }

        public bool estValide()
        {
            return aUneDénominationCorrect()
                && aUnNuméroSIRETCorrect()
                && Adresse.estValide();
        }

        private bool aUneDénominationCorrect()
        {
            return !string.IsNullOrEmpty(Dénomination)
                && Dénomination.Length <= Ressource.Validation.LONGUEUR_MAX_DÉNOMINATION_SOCIÉTÉ;
        }

        private bool aUnNuméroSIRETCorrect()
        {
            return NuméroSIRET.estVide()
                   || NuméroSIRET.estValide();
        }

        public List<string> obtenirLesErreurs()
        {
            List<string> message = new List<string>();
            validerLaDénomination(message);
            validerLeNuméroSIRET(message);
            validerLAdresse(message);
            return message;
        }

        private void validerLaDénomination(List<string> message)
        {
            if (string.IsNullOrEmpty(Dénomination))
                message.Add(Ressource.Validation.DÉNOMINATION_SOCIÉTÉ_REQUISE);
            else if(Dénomination.Length > Ressource.Validation.LONGUEUR_MAX_DÉNOMINATION_SOCIÉTÉ)
                message.Add(Ressource.Validation.DÉNOMINATION_SOCIÉTÉ_LONGUEUR_MAX);
        }

        private void validerLeNuméroSIRET(List<string> message)
        {
            if (!aUnNuméroSIRETCorrect())
                message.Add(Ressource.Validation.NUMÉRO_SIRET_INVALIDE);
        }

        private void validerLAdresse(List<string> message)
        {
            if (!Adresse.estValide())
                message.AddRange(Adresse.obtenirLesErreurs());
        }
    }
}
