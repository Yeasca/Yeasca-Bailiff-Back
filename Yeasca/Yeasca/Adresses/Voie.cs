using System.Collections.Generic;

namespace Yeasca.Metier
{
    public class Voie
    {
        public  Voie()
        {
            NuméroVoie = new NumeroVoie();
        }

        public NumeroVoie NuméroVoie { get; set; }
        public string TypeVoie { get; set; }
        public string NomVoie { get; set; }
        public string ComplémentVoie { get; set; }

        public bool estValide()
        {
            return NuméroVoie.estValide()
                && aUnTypeDeVoieCorrect()
                && aUnTypeDeVoieSiRequis()
                && aUnNomDeVoieRenseigné()
                && aUnNomDeVoieCorrect()
                && aUnComplémentDeVoieCorrect();
        }

        private bool aUnTypeDeVoieCorrect()
        {
            return TypeVoie == null
                || TypeVoie.Length <= Ressource.Validation.LONGUEUR_MAX_TYPE_VOIE;
        }

        private bool aUnTypeDeVoieSiRequis()
        {
            return !aUnNuméroDeVoieRenseigné() 
                || aUnTypeDeVoieRenseigné();

        }

        private bool aUnTypeDeVoieRenseigné()
        {
            return !string.IsNullOrEmpty(TypeVoie);
        }

        private bool aUnNuméroDeVoieRenseigné()
        {
            return !string.IsNullOrEmpty(NuméroVoie.enChaine());
        }

        private bool aUnNomDeVoieRenseigné()
        {
            return !string.IsNullOrEmpty(NomVoie);
        }

        private bool aUnNomDeVoieCorrect()
        {
            return NomVoie.Length <= Ressource.Validation.LONGUEUR_MAX_NOM_VOIE;
        }

        private bool aUnComplémentDeVoieCorrect()
        {
            return ComplémentVoie == null 
                || ComplémentVoie.Length <= Ressource.Validation.LONGUEUR_MAX_COMPLÉMENT_VOIE;
        }

        private bool aUnComplémentDeVoieRenseigné()
        {
            return !string.IsNullOrEmpty(ComplémentVoie);
        }

        public Erreur obtenirLesErreurs()
        {
            Erreur message = new Erreur();
            validerLeNuméroDeVoie(message);
            validerLeTypeDeVoie(message);
            validerLeNomDeVoie(message);
            validerLeComplémentDeVoie(message);
            return message;
        }

        private void validerLeNuméroDeVoie(Erreur message)
        {
            if (!NuméroVoie.estValide())
                message.ajouterUneErreur(NuméroVoie.obtenirLesErreurs());
        }

        private void validerLeTypeDeVoie(Erreur message)
        {
            if (!aUnTypeDeVoieCorrect())
                message.ajouterUneErreur(Ressource.Validation.TYPE_VOIE_LONGUEUR_MAX);
            else if (!aUnTypeDeVoieSiRequis())
                message.ajouterUneErreur(Ressource.Validation.TYPE_VOIE_REQUIS);
        }

        private void validerLeNomDeVoie(Erreur message)
        {
            if (!aUnNomDeVoieRenseigné())
                message.ajouterUneErreur(Ressource.Validation.NOM_VOIE_REQUIS);
            else if (!aUnNomDeVoieCorrect())
                message.ajouterUneErreur(Ressource.Validation.NOM_VOIE_LONGUEUR_MAX);
        }

        private void validerLeComplémentDeVoie(Erreur message)
        {
            if (!aUnComplémentDeVoieCorrect())
                message.ajouterUneErreur(Ressource.Validation.COMPLÉMENT_VOIE_LONGUEUR_MAX);
        }

        public string enChaine()
        {
            string voieEnString = NuméroVoie.enChaine();
            concaténerLeTypeDeVoie(ref voieEnString);
            concaténerAvecUnEspace(ref voieEnString, NomVoie);
            concaténerLeComplémentDeVoie(ref voieEnString);
            return voieEnString;
        }

        private void concaténerLeTypeDeVoie(ref string voieEnString)
        {
            if (aUnTypeDeVoieRenseigné())
                concaténerAvecUnEspace(ref voieEnString, TypeVoie);
        }

        private void concaténerLeComplémentDeVoie(ref string voieEnString)
        {
            if (aUnComplémentDeVoieRenseigné())
                concaténerAvecUnEspace(ref voieEnString, ComplémentVoie);
        }

        private void concaténerAvecUnEspace(ref string chainePrincipale, string chaineAAjouter)
        {
            chainePrincipale = string.Concat(chainePrincipale, " ", chaineAAjouter);
        }
    }
}
