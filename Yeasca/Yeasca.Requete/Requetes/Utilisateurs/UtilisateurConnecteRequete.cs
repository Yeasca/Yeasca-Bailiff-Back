
using Yeasca.Metier;
using Yeasca.Mongo;

namespace Yeasca.Requete
{
    public class UtilisateurConnecteRequete : Requete<IUtilisateurConnecteMessage>
    {
        public override ReponseRequete exécuter(IUtilisateurConnecteMessage message)
        {
            Utilisateur utilisateurEnCours = Utilisateur.chargerDepuisLaSession();
            if (utilisateurEnCours != null)
            {
                IEntrepotParametrage paramètres = EntrepotMongo.fabriquerEntrepot<IEntrepotParametrage>();
                UtilisateurConnecteReponse résultat = new UtilisateurConnecteReponse();
                résultat.Civilité = paramètres.récupérerLesCivilités()[utilisateurEnCours.Profile.Abréviation];
                résultat.Nom = utilisateurEnCours.Profile.Nom;
                résultat.Prénom = utilisateurEnCours.Profile.Prénom;
                return ReponseRequete.générerUnSuccès(résultat);
            }
            return ReponseRequete.générerUnSuccès(null);
        }
    }

    public class UtilisateurConnecteReponse
    {
        public string Civilité { get; set; }
        public string Nom { get; set; }
        public string Prénom { get; set; }
    }
}
