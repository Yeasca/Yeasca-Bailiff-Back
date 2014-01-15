using Yeasca.Metier;

namespace Yeasca.Commande
{
    public class DeconnexionCommande : Commande<IDeconnexionMessage>
    {
        public override ReponseCommande exécuter(IDeconnexionMessage message)
        {
            Utilisateur.déconnecter();
            return ReponseCommande.générerUnSuccès();
        }
    }
}
