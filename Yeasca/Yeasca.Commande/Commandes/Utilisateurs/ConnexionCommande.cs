using Yeasca.Metier;
using Yeasca.Mongo;

namespace Yeasca.Commande
{
    public class ConnexionCommande : Commande<IConnexionMessage>
    {
        public override ReponseCommande exécuter(IConnexionMessage message)
        {
            IEntrepotUtilisateur entrepot = EntrepotMongo.fabriquerEntrepot<IEntrepotUtilisateur>();
            Email email = new Email(message.Email);
            if (email.estValide())
                return authentifier(message, entrepot, email);
            return ReponseCommande.générerUnEchec(email.obtenirLErreur());
        }

        private static ReponseCommande authentifier(IConnexionMessage message, IEntrepotUtilisateur entrepot, Email email)
        {
            MotDePasse motDePasse = new MotDePasse(message.MotDePasse);
            Utilisateur utilisateur = entrepot.authentifier(email, motDePasse);
            if (utilisateur != null)
            {
                utilisateur.mettreEnSession();
                return ReponseCommande.générerUnSuccès();
            }
            return ReponseCommande.générerUnEchec(Ressource.Requetes.AUTH_ECHOUE);
        }
    }
}
