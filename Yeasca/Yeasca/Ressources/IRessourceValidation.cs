
namespace Yeasca.Metier
{
    public interface IRessourceValidation
    {
        //Adresse
        int LONGUEUR_MAX_NUMÉRO_VOIE { get; }
        int LONGUEUR_MAX_RÉPÉTITION { get; }
        
        string NUMÉRO_VOIE_REQUIS { get; }
        string NUMÉRO_VOIE_INVALIDE { get; }
        string NUMÉRO_VOIE_LONGUEUR_MAX { get; }
        string RÉPÉTITION_VOIE_LONGUEUR_MAX { get; }

        int LONGUEUR_MAX_TYPE_VOIE { get; }
        int LONGUEUR_MAX_NOM_VOIE  { get; }
        int LONGUEUR_MAX_COMPLÉMENT_VOIE { get; }
        
        string TYPE_VOIE_REQUIS  { get; }
        string TYPE_VOIE_LONGUEUR_MAX { get; }
        string NOM_VOIE_REQUIS  { get; }
        string NOM_VOIE_LONGUEUR_MAX  { get; }
        string COMPLÉMENT_VOIE_LONGUEUR_MAX { get; }
        
        int LONGUEUR_MAX_COMMUNE  { get; }
        
        string CODE_POSTAL_REQUIS  { get; }
        string CODE_POSTAL_INVALIDE { get; }
        string LIBELLÉ_COMMUNE_REQUIS  { get; }
        string LIBELLÉ_COMMUNE_LONGUEUR_MAX  { get; }

        //Société
        int LONGUEUR_MAX_DÉNOMINATION_SOCIÉTÉ  { get; }
        
        string DÉNOMINATION_SOCIÉTÉ_REQUISE  { get; }
        string DÉNOMINATION_SOCIÉTÉ_LONGUEUR_MAX  { get; }
        string NUMÉRO_SIRET_INVALIDE  { get; }

        //Partie
        int LONGUEUR_MAX_NOM { get; }
        int LONGUEUR_MAX_PRÉNOM { get; }
        
        string NOM_REQUIS  { get; }
        string PRÉNOM_REQUIS  { get; }
        string NOM_LONGUEUR_MAX { get; }
        string PRÉNOM_LONGUEUR_MAX { get; }

        //Utilisateur
        int LONGUEUR_MAX_LOGIN { get; }
        int LONGUEUR_MIN_MOT_DE_PASSE { get; }
        int LONGUEUR_MAX_MOT_DE_PASSE { get; }
        int LONGUEUR_MAX_EMAIL { get; }
        
        string MOT_DE_PASSE_REQUIS { get; }
        string MOT_DE_PASSE_LONGUEUR_MIN { get; }
        string MOT_DE_PASSE_LONGUEUR_MAX { get; }
        string MOT_DE_PASSE_COMPLEXE { get; }
        string EMAIL_REQUIS { get; }
        string EMAIL_LONGUEUR_MAX { get; }
        string EMAIL_INVALIDE { get; }

    }
}
