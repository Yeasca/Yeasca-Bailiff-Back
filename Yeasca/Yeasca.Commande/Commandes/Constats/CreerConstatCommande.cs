using System;
using Yeasca.Metier;
using Yeasca.Mongo;

namespace Yeasca.Commande
{
    public class CreerConstatCommande : Commande<ICreerConstatMessage>
    {
        public override ReponseCommande exécuter(ICreerConstatMessage message)
        {
            if (estUnHuissier())
                return créerLeConstat(message);
            return ReponseCommande.générerUnEchec(Ressource.Commandes.ERREUR_AUTH_HUISSIER);
        }

        private ReponseCommande créerLeConstat(ICreerConstatMessage message)
        {
            Guid idClient;
            if (Guid.TryParse(message.IdClient, out idClient))
                return créerLeConstatAvecLaBonneRéférenceClient(idClient);
            return ReponseCommande.générerUnEchec(Ressource.Commandes.REF_CLIENT_INVALIDE);
        }

        private ReponseCommande créerLeConstatAvecLaBonneRéférenceClient(Guid idClient)
        {
            Constat nouveauConstat = initialiserLeNouveauConstat(idClient);
            IEntrepotConstat entrepotConstat = EntrepotMongo.fabriquerEntrepot<IEntrepotConstat>();
            if (entrepotConstat.ajouter(nouveauConstat))
                return ReponseCommande.générerUnSuccès(nouveauConstat.Id.ToString());
            return ReponseCommande.générerUnEchec(Ressource.Commandes.ERREUR_CRÉATION_CONSTAT);
        }

        private Constat initialiserLeNouveauConstat(Guid idClient)
        {
            IEntrepotProfile entrepotParties = EntrepotMongo.fabriquerEntrepot<IEntrepotProfile>();
            Profile client = entrepotParties.récupérerLeClient(idClient);
            Profile huissier = Utilisateur.chargerDepuisLaSession().Profile;
            Constat nouveauConstat = new Constat();
            nouveauConstat.Client = client;
            nouveauConstat.Huissier = huissier;
            return nouveauConstat;
        }
    }
}
