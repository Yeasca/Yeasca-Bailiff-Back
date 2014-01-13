using System.Collections.Generic;
using System.Linq;
using Yeasca.Metier;
using Yeasca.Mongo;

namespace Yeasca.Requete
{
    public class RechercheAvanceConstatRequete : Requete<IRechercheAvanceConstatMessage>
    {
        public override ReponseRequete exécuter(IRechercheAvanceConstatMessage message)
        {
            if (estAuthentifié())
            {
                IEntrepotConstat entrepot = EntrepotMongo.fabriquerEntrepot<IEntrepotConstat>();
                IList<Constat> recherche = entrepot.récupérerLaListeDesConstats(message);
                List<ConstatReponse> résultat = recherche.Select(x => new ConstatReponse()
                {
                    IdConstat = x.Id.ToString(),
                    Date = x.Date.ToString(Ressource.Paramètres.FORMAT_DATE),
                    NomClient = x.Client.Nom,
                    PrénomClient = x.Client.Prénom,
                    NomHuissier = x.Huissier.Nom,
                    PrénomHuissier = x.Huissier.Prénom,
                    Traité = x.EstValidé
                }) .ToList();
                return ReponseRequete.générerUnSuccès(résultat);
            }
            return ReponseRequete.générerUnEchec(Ressource.Requetes.AUTH_REQUISE);
        }
    }
}