
using Yeasca.Metier;
using Yeasca.Mongo;

namespace Yeasca.Requete
{
    public class AdminEstCreeRequete : Requete<IAdminEstCreeMessage>
    {
        public override ReponseRequete exécuter(IAdminEstCreeMessage message)
        {
            IEntrepotUtilisateur entrepot = EntrepotMongo.fabriquerEntrepot<IEntrepotUtilisateur>();
            Utilisateur admin = entrepot.récupérerLAdministrateur();
            return ReponseRequete.générerUnSuccès(admin != null);
        }
    }
}
