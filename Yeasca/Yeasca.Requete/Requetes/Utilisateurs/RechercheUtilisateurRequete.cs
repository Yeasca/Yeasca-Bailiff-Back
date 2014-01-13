using System.Collections.Generic;
using System.Linq;
using Yeasca.Metier;
using Yeasca.Mongo;

namespace Yeasca.Requete
{
    public class RechercheUtilisateurRequete : Requete<IRechercheUtilisateurMessage>
    {
        public override ReponseRequete exécuter(IRechercheUtilisateurMessage message)
        {
            if (estAdministrateur())
            {
                IEntrepotUtilisateur entrepot = EntrepotMongo.fabriquerEntrepot<IEntrepotUtilisateur>();
                IEntrepotParametrage paramètres = EntrepotMongo.fabriquerEntrepot<IEntrepotParametrage>();
                IList<Utilisateur> recherche = entrepot.récupérerLaListeDesUtilisateurs(message);
                List<UtilisateurReponse> résultat = recherche.Select(x => new UtilisateurReponse()
                {
                    IdUtilisateur = x.Id.ToString(),
                    Nom = x.Profile.Nom,
                    Prénom = x.Profile.Prénom,
                    Type = paramètres.récupérerLesTypesUtilisateurs()[x.TypeUtilisateur]
                }).ToList();
                return ReponseRequete.générerUnSuccès(résultat);
            }
            return ReponseRequete.générerUnEchec(Ressource.Requetes.AUTH_ADMIN_REQUISE);
        }
    }

    public class UtilisateurReponse
    {
        public string IdUtilisateur { get; set; }
        public string Nom { get; set; }
        public string Prénom { get; set; }
        public string Type { get; set; }
    }
}
