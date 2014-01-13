namespace Yeasca.Metier
{
    public interface IRessourceRequete
    {
        //Authentification
        string AUTH_REQUISE { get; }
        string AUTH_ADMIN_REQUISE { get; }

        //Requetes utilisateur
        string AUTH_ECHOUE { get; }
        string REF_UTILISATEUR_INVALIDE { get; }

        //Requetes constat
        string REF_CONSTAT_INVALIDE { get; }

        //Requetes profile
        string REF_CLIENT_INVALIDE { get; }
    }
}
