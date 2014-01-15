using System.Collections.Generic;
using System.Linq;
using Yeasca.Metier;
using Yeasca.Mongo;

namespace Yeasca.Requete
{
    public class RechercheClientRequete : Requete<IRechercheClientMessage>
    {
        public override ReponseRequete exécuter(IRechercheClientMessage message)
        {
            if (estAuthentifié())
            {
                IEntrepotProfile entrepotProfile = EntrepotMongo.fabriquerEntrepot<IEntrepotProfile>();
                IEntrepotConstat entrepotConstat = EntrepotMongo.fabriquerEntrepot<IEntrepotConstat>();
                IList<Client> recherche = entrepotProfile.récupérerLaListeDesClients(message);
                List<ClientReponse> résultat = recherche.Select(x => new ClientReponse()
                {
                    IdClient = x.Id.ToString(),
                    NomClient = x.Nom,
                    PrénomClient = x.Prénom,
                    Constats = entrepotConstat.récupérerLaListeDesConstatsDuClient(x.Id).Select(y =>  new ConstatResume()
                    {
                        IdConstat = y.Id.ToString(),
                        Date = y.Date.ToString(Ressource.Paramètres.FORMAT_DATE)
                    }).ToList()
                }).ToList();
                return ReponseRequete.générerUnSuccès(résultat);
            }
            return ReponseRequete.générerUnEchec(Ressource.Requetes.AUTH_REQUISE);
        }
    }

    public class ClientReponse
    {
        public string IdClient { get; set; }
        public string NomClient { get; set; }
        public string PrénomClient { get; set; }
        public List<ConstatResume> Constats { get; set; }
    }

    public class ConstatResume
    {
        public string IdConstat { get; set; }
        public string Date { get; set; }
    }
}
