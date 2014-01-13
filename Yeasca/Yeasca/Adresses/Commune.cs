using System.Collections.Generic;

namespace Yeasca.Metier
{
    public class Commune
    {
        public Commune()
        {
            CodePostal = new CodePostal();
        }

        public CodePostal CodePostal { get; set; }
        public string LibelléCommune { get; set; }

        public bool estValide()
        {
                return CodePostal != null
                    && CodePostal.estValide()
                    && aUneCommuneRenseignée();
        }

        private bool aUneCommuneRenseignée()
        {
            return !string.IsNullOrEmpty(LibelléCommune);
        }

        public Erreur obtenirLesErreurs()
        {
            Erreur message = new Erreur();
            validerLeCodePostal(message);
            validerLeLibelléCommune(message);
            return message;
        }

        private void validerLeCodePostal(Erreur message)
        {
            if (!CodePostal.estValide())
                message.ajouterUneErreur(CodePostal.obtenirLesErreurs());
        }

        private void validerLeLibelléCommune(Erreur message)
        {
            if (!aUneCommuneRenseignée())
                message.ajouterUneErreur(Ressource.Validation.LIBELLÉ_COMMUNE_REQUIS);
            else if (LibelléCommune.Length > Ressource.Validation.LONGUEUR_MAX_COMMUNE)
                message.ajouterUneErreur(Ressource.Validation.LIBELLÉ_COMMUNE_LONGUEUR_MAX);
        }

        public string enChaine()
        {
            return string.Concat(CodePostal.enChaine(), " ", LibelléCommune);
        }

    }
}
