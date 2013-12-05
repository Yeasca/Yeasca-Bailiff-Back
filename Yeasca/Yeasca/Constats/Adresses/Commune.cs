﻿using System.Collections.Generic;
using Yeasca.Ressources;

namespace Yeasca
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

        public List<string> obtenirLesErreurs()
        {
            List<string> message = new List<string>();
            validerLeCodePostal(message);
            validerLeLibelléCommune(message);
            return message;
        }

        private void validerLeCodePostal(List<string> message)
        {
            if (!CodePostal.estValide())
                message.AddRange(CodePostal.obtenirLesErreurs());
        }

        private void validerLeLibelléCommune(List<string> message)
        {
            if (!aUneCommuneRenseignée())
                message.Add(RessourceValidation.LIBELLÉ_COMMUNE_REQUIS);
            else if (LibelléCommune.Length > RessourceValidation.LONGUEUR_MAX_COMMUNE)
                message.Add(RessourceValidation.LIBELLÉ_COMMUNE_LONGUEUR_MAX);
        }

        public string enChaine()
        {
            return string.Concat(CodePostal.enChaine(), " ", LibelléCommune);
        }

    }
}