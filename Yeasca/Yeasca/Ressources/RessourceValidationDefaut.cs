
namespace Yeasca.Metier
{
    public class RessourceValidationDefaut : IRessourceValidation
    {

        public int LONGUEUR_MAX_NUMÉRO_VOIE { get { return  5; } }
        public int LONGUEUR_MAX_RÉPÉTITION { get { return  9; } }

        public string NUMÉRO_VOIE_REQUIS { get { return  "NUMÉRO_VOIE_REQUIS"; } }
        public string NUMÉRO_VOIE_INVALIDE { get { return  "NUMÉRO_VOIE_INVALIDE"; } }
        public string NUMÉRO_VOIE_LONGUEUR_MAX { get { return  "NUMÉRO_VOIE_LONGUEUR_MAX"; } }
        public string RÉPÉTITION_VOIE_LONGUEUR_MAX { get { return  "RÉPÉTITION_VOIE_LONGUEUR_MAX"; } }

        public int LONGUEUR_MAX_TYPE_VOIE { get { return  255; } }
        public int LONGUEUR_MAX_NOM_VOIE { get { return  255; } }
        public int LONGUEUR_MAX_COMPLÉMENT_VOIE { get { return  255; } }

        public string TYPE_VOIE_REQUIS { get { return  "TYPE_VOIE_REQUIS"; } }
        public string TYPE_VOIE_LONGUEUR_MAX { get { return  "TYPE_VOIE_LONGUEUR_MAX"; } }
        public string NOM_VOIE_REQUIS { get { return  "NOM_VOIE_REQUIS"; } }
        public string NOM_VOIE_LONGUEUR_MAX { get { return  "NOM_VOIE_LONGUEUR_MAX"; } }
        public string COMPLÉMENT_VOIE_LONGUEUR_MAX { get { return  "COMPLÉMENT_VOIE_LONGUEUR_MAX"; } }

        public int LONGUEUR_MAX_COMMUNE { get { return  255; } }

        public string CODE_POSTAL_REQUIS { get { return  "CODE_POSTAL_REQUIS"; } }
        public string CODE_POSTAL_INVALIDE { get { return  "CODE_POSTAL_INVALIDE"; } }
        public string LIBELLÉ_COMMUNE_REQUIS { get { return  "LIBELLÉ_COMMUNE_REQUIS"; } }
        public string LIBELLÉ_COMMUNE_LONGUEUR_MAX { get { return  "LIBELLÉ_COMMUNE_LONGUEUR_MAX"; } }

        public int LONGUEUR_MAX_DÉNOMINATION_SOCIÉTÉ { get { return  255; } }

        public string DÉNOMINATION_SOCIÉTÉ_REQUISE { get { return  "DÉNOMINATION_SOCIÉTÉ_REQUISE"; } }
        public string DÉNOMINATION_SOCIÉTÉ_LONGUEUR_MAX { get { return  "DÉNOMINATION_SOCIÉTÉ_LONGUEUR_MAX"; } }
        public string NUMÉRO_SIRET_INVALIDE { get { return  "NUMÉRO_SIRET_INVALIDE"; } }

        public int LONGUEUR_MAX_NOM { get { return  255; } }
        public int LONGUEUR_MAX_PRÉNOM { get { return  255; } }

        public string NOM_REQUIS { get { return  "NOM_REQUIS"; } }
        public string PRÉNOM_REQUIS { get { return  "PRÉNOM_REQUIS"; } }
        public string NOM_LONGUEUR_MAX { get { return  "NOM_LONGUEUR_MAX"; } }
        public string PRÉNOM_LONGUEUR_MAX { get { return  "PRÉNOM_LONGUEUR_MAX"; } }

        public int LONGUEUR_MAX_LOGIN { get { return 255; } }
        public int LONGUEUR_MIN_MOT_DE_PASSE { get { return 8; } }
        public int LONGUEUR_MAX_MOT_DE_PASSE { get { return 255; } }
        public int LONGUEUR_MAX_EMAIL { get { return 255; } }

        public string LOGIN_REQUIS { get { return "LOGIN_REQUIS"; } }
        public string LOGIN_LONGUEUR_MAX { get { return "LOGIN_LONGUEUR_MAX"; } }
        public string MOT_DE_PASSE_REQUIS { get { return "MOT_DE_PASSE_REQUIS"; } }
        public string MOT_DE_PASSE_LONGUEUR_MIN { get { return "MOT_DE_PASSE_LONGUEUR_MIN"; } }
        public string MOT_DE_PASSE_LONGUEUR_MAX { get { return "MOT_DE_PASSE_LONGUEUR_MAX"; } }
        public string MOT_DE_PASSE_COMPLEXE { get { return "MOT_DE_PASSE_COMPLEXE"; } }
        public string EMAIL_REQUIS { get { return "EMAIL_REQUIS"; } }
        public string EMAIL_LONGUEUR_MAX { get { return "EMAIL_LONGUEUR_MAX"; } }
        public string EMAIL_INVALIDE { get { return "EMAIL_INVALIDE"; } }
    }
}
