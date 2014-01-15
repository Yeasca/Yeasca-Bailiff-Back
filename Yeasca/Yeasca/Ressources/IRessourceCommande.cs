
namespace Yeasca.Metier
{
    public interface IRessourceCommande
    {
        //Bus
        string ERREUR_TYPE_MESSAGE { get; }
        string ERREUR_EXCEPTION_BUS_COMMANDE { get; }

        //Authentification
        string ERREUR_AUTH_ADMIN { get; }
        string ERREUR_AUTH_HUISSIER { get; }
        string ERREUR_AUTH { get; }
        string ERREUR_DÉCO { get; }

        //Commandes constat
        string REF_CONSTAT_INVALIDE { get; }
        string ERREUR_CRÉATION_CONSTAT { get; }
        string ERREUR_AJOUT_FICHIER { get; }
        string ERREUR_ENREG_FICHIER { get; }
        string ERREUR_VALIDATION_CONSTAT { get; }

        //Commandes client
        string REF_CLIENT_INVALIDE { get; }
        string ERREUR_CRÉATION_CLIENT { get; }
        string ERREUR_MODIFICATION_CLIENT { get; }

        //Commandes huissier
        string REF_HUISSIER_INVALIDE { get; }
        string ERREUR_CRÉATION_HUISSIER { get; }
        string ERREUR_CRÉATION_COMPTE_HUISSIER { get; }
        string ERREUR_MODIFICATION_HUISSIER { get; }
        string ERREUR_MODIFICATION_COMPTE_HUISSIER { get; }

        //Commandes secrétaire
        string REF_SECRETAIRE_INVALIDE { get; }
        string ERREUR_CRÉATION_SECRETAIRE { get; }
        string ERREUR_CRÉATION_COMPTE_SECRETAIRE { get; }
        string ERREUR_MODIFICATION_SECRETAIRE { get; }
        string ERREUR_MODIFICATION_COMPTE_SECRETAIRE { get; }

        //Commandes administrateur
        string JETON_INVALIDE { get; }
        string ERREUR_CRÉATION_ADMIN { get; }
        string ERREUR_CRÉATION_COMPTE_ADMIN { get; }
    }
}
