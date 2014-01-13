using System.Collections.Generic;

namespace Yeasca.Metier
{
    public class NumeroVoie
    {
        public string Numéro { get; set; }
        public string Répétition { get; set; }

        public bool estValide()
        {
            return aUnNuméroDeVoieCorrect()
                && aUneRépétitionDeVoieCorrect()
                && aUnNuméroDeVoieSiRequis();
        }

        public bool aUnNuméroDeVoie()
        {
            return !string.IsNullOrEmpty(Numéro);
        }

        private bool aUnNuméroDeVoieCorrect()
        {
            return !aUnNuméroDeVoie()
                || leNuméroALaBonneLongueur() && leNuméroEstConstituéDeChiffres();
        }

        private bool leNuméroALaBonneLongueur()
        {
            return Numéro.Length <= Ressource.Validation.LONGUEUR_MAX_NUMÉRO_VOIE;
        }

        private bool leNuméroEstConstituéDeChiffres()
        {
            int numéroConverti;
            return int.TryParse(Numéro, out numéroConverti);
        }

        private bool aUneRépétitionDeVoieCorrect()
        {
            return Répétition == null
                || Répétition.Length <= Ressource.Validation.LONGUEUR_MAX_RÉPÉTITION;
        }

        private bool aUnNuméroDeVoieSiRequis()
        {
            return !aUnNuméroDeVoieRequis() 
                ||  aUnNuméroDeVoieRequis() && aUnNuméroDeVoie();
        }

        private bool aUnNuméroDeVoieRequis()
        {
            return !string.IsNullOrEmpty(Répétition);
        }

        public Erreur obtenirLesErreurs()
        {
            Erreur message = new Erreur();
            validerLeNuméroDeVoieSiIncorrect(message);
            validerLaRépétitionDeVoie(message);
            return message;
        }

        private void validerLeNuméroDeVoieSiIncorrect(Erreur message)
        {
            if (!aUnNuméroDeVoieCorrect())
                validerLeNuméroDeVoie(message);
        }

        private void validerLeNuméroDeVoie(Erreur message)
        {
            if (!leNuméroEstConstituéDeChiffres())
                message.ajouterUneErreur(Ressource.Validation.NUMÉRO_VOIE_INVALIDE);
            else if (!leNuméroALaBonneLongueur())
                message.ajouterUneErreur(Ressource.Validation.NUMÉRO_VOIE_LONGUEUR_MAX);
        }

        private void validerLaRépétitionDeVoie(Erreur message)
        {
            if (!aUneRépétitionDeVoieCorrect())
                message.ajouterUneErreur(Ressource.Validation.RÉPÉTITION_VOIE_LONGUEUR_MAX);
            else if (!aUnNuméroDeVoieSiRequis())
                message.ajouterUneErreur(Ressource.Validation.NUMÉRO_VOIE_REQUIS);
        }

        public string enChaine()
        {
            if (!aUnNuméroDeVoie())
                return string.Empty;
            string numéroDeVoieEnString = Numéro;
            if (string.IsNullOrEmpty(Répétition))
                return numéroDeVoieEnString;
            return string.Concat(numéroDeVoieEnString, " ", Répétition);
        }
    }
}
