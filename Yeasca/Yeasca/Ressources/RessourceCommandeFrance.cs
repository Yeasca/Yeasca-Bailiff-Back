
namespace Yeasca.Metier
{
    public class RessourceCommandeFrance : IRessourceCommande
    {
        public string ERREUR_TYPE_MESSAGE { get { return "Impossible d'exécuter la commande"; } }
        public string ERREUR_EXCEPTION_BUS_COMMANDE { get { return "Erreur lors de l'exécution de la commande"; } }

        public string ERREUR_AUTH_ADMIN { get { return "Vous devez être un administrateur authentifié"; } }
        public string ERREUR_AUTH_HUISSIER { get { return "Vous devez être un huissier authentifié"; } }
        public string ERREUR_AUTH { get { return "Vous devez être authentifié"; } }
        public string ERREUR_DÉCO { get { return "Erreur à la déconnexion"; } }

        public string REF_CONSTAT_INVALIDE { get { return "Référence constat invalide"; } }
        public string ERREUR_CRÉATION_CONSTAT { get { return "Erreur lors de la création du constat"; } }
        public string ERREUR_AJOUT_FICHIER { get { return "Erreur lors de l'ajout du fichier"; } }
        public string ERREUR_ENREG_FICHIER { get { return "Erreur lors de l'enregistrement du fichier"; } }
        public string ERREUR_VALIDATION_CONSTAT { get { return "Erreur lors de la validation du constat"; } }

        public string REF_CLIENT_INVALIDE { get { return "Référence client invalide"; } }
        public string ERREUR_CRÉATION_CLIENT { get { return "Erreur lors de la création du client"; } }
        public string ERREUR_MODIFICATION_CLIENT { get { return "Erreur lors de la modification du client"; } }

        public string REF_HUISSIER_INVALIDE { get { return "Référence huissier invalide"; } }
        public string ERREUR_CRÉATION_HUISSIER { get { return "Erreur lors de la création du profile de l'huissier"; } }
        public string ERREUR_CRÉATION_COMPTE_HUISSIER { get { return "Erreur lors de la création du compte de l'huissier"; } }
        public string ERREUR_MODIFICATION_HUISSIER { get { return "Erreur lors de la modification du profile de l'huissier"; } }
        public string ERREUR_MODIFICATION_COMPTE_HUISSIER { get { return "Erreur lors de la modification du compte de l'huissier"; } }

        public string REF_SECRETAIRE_INVALIDE { get { return "Référence secrétaire invalide"; } }
        public string ERREUR_CRÉATION_SECRETAIRE { get { return "Erreur lors de la création du profile de la secrétaire"; } }
        public string ERREUR_CRÉATION_COMPTE_SECRETAIRE { get { return "Erreur lors de la création du compte de la secrétaire"; } }
        public string ERREUR_MODIFICATION_SECRETAIRE { get { return "Erreur lors de la modification du profile de la secrétaire"; } }
        public string ERREUR_MODIFICATION_COMPTE_SECRETAIRE { get { return "Erreur lors de la modification du compte de la secrétaire"; } }

        public string JETON_INVALIDE { get { return "Impossible de créer le compte administrateur"; } }
        public string ERREUR_CRÉATION_ADMIN { get { return "Erreur lors de la création du profile de l'administrateur"; } }
        public string ERREUR_CRÉATION_COMPTE_ADMIN { get { return "Erreur lors de la création du compte de l'administrateur"; } }
    }
}
