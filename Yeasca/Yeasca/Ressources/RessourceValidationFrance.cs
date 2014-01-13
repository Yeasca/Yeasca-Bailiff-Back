
namespace Yeasca.Metier
{
    public class RessourceValidationFrance : IRessourceValidation
    {

        public int LONGUEUR_MAX_NUMÉRO_VOIE { get { return  5; } }
        public int LONGUEUR_MAX_RÉPÉTITION { get { return  9; } }

        public string NUMÉRO_VOIE_REQUIS { get { return  "Numéro de voie requis"; } }
        public string NUMÉRO_VOIE_INVALIDE { get { return  "Numéro de voie invalide"; } }
        public string NUMÉRO_VOIE_LONGUEUR_MAX { get { return  "Numéro de voie trop long"; } }
        public string RÉPÉTITION_VOIE_LONGUEUR_MAX { get { return  "Répétition de voie trop longue"; } }

        public int LONGUEUR_MAX_TYPE_VOIE { get { return  255; } }
        public int LONGUEUR_MAX_NOM_VOIE { get { return  255; } }
        public int LONGUEUR_MAX_COMPLÉMENT_VOIE { get { return  255; } }

        public string TYPE_VOIE_REQUIS { get { return  "Type de voie requis"; } }
        public string TYPE_VOIE_LONGUEUR_MAX { get { return  "Type de voie trop long"; } }
        public string NOM_VOIE_REQUIS { get { return  "Nom de voie requis"; } }
        public string NOM_VOIE_LONGUEUR_MAX { get { return  "Nom de voie trop long"; } }
        public string COMPLÉMENT_VOIE_LONGUEUR_MAX { get { return  "Complément de voie trop long"; } }

        public int LONGUEUR_MAX_COMMUNE { get { return  255; } }

        public string CODE_POSTAL_REQUIS { get { return  "Code postal requis"; } }
        public string CODE_POSTAL_INVALIDE { get { return  "Code postal invalide"; } }
        public string LIBELLÉ_COMMUNE_REQUIS { get { return  "Nom commune requis"; } }
        public string LIBELLÉ_COMMUNE_LONGUEUR_MAX { get { return  "Nom commune trop long"; } }

        public int LONGUEUR_MAX_DÉNOMINATION_SOCIÉTÉ { get { return  255; } }

        public string DÉNOMINATION_SOCIÉTÉ_REQUISE { get { return  "Dénomination société requise"; } }
        public string DÉNOMINATION_SOCIÉTÉ_LONGUEUR_MAX { get { return  "Dénomination société trop longue"; } }
        public string NUMÉRO_SIRET_INVALIDE { get { return  "Numéro SIRET invalide"; } }

        public int LONGUEUR_MAX_NOM { get { return  255; } }
        public int LONGUEUR_MAX_PRÉNOM { get { return  255; } }

        public string NOM_REQUIS { get { return  "Nom requis"; } }
        public string PRÉNOM_REQUIS { get { return  "Prénom requis"; } }
        public string NOM_LONGUEUR_MAX { get { return  "Nom trop long"; } }
        public string PRÉNOM_LONGUEUR_MAX { get { return  "Prénom trop long"; } }

        public int LONGUEUR_MAX_LOGIN { get { return 255; } }
        public int LONGUEUR_MIN_MOT_DE_PASSE { get { return 8; } }
        public int LONGUEUR_MAX_MOT_DE_PASSE { get { return 255; } }
        public int LONGUEUR_MAX_EMAIL { get { return 255; } }

        public string MOT_DE_PASSE_REQUIS { get { return "Mot de passe requis"; } }
        public string MOT_DE_PASSE_LONGUEUR_MIN { get { return "Mot de passe trop court"; } }
        public string MOT_DE_PASSE_LONGUEUR_MAX { get { return "Mot de passe trop long"; } }
        public string MOT_DE_PASSE_COMPLEXE { get { return "Mot de passe pas assez complexe"; } }
        public string EMAIL_REQUIS { get { return "E-mail requis"; } }
        public string EMAIL_LONGUEUR_MAX { get { return "E-mail trop long"; } }
        public string EMAIL_INVALIDE { get { return "E-mail invalide"; } }
    }
}
