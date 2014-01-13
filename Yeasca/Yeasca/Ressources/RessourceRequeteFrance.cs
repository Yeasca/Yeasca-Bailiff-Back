namespace Yeasca.Metier
{
    public class RessourceRequeteFrance : IRessourceRequete
    {
        public string AUTH_REQUISE { get { return "Authentification requise"; } }
        public string AUTH_ADMIN_REQUISE { get { return "Authentification administrateur requise"; } }

        public string AUTH_ECHOUE { get { return "Adresse e-mail ou mot de passe incorrect"; } }
        public string REF_UTILISATEUR_INVALIDE { get { return "Référence utilisateur invalide"; } }

        public string REF_CONSTAT_INVALIDE { get { return "Référence constat invalide"; } }

        public string REF_CLIENT_INVALIDE { get { return "Référence client invalide"; } }
    }
}
