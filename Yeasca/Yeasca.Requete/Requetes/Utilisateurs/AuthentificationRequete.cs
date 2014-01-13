using Yeasca.Metier;
using Yeasca.Mongo;

namespace Yeasca.Requete
{
    public class AuthentificationRequete : Requete<IAuthentificationMessage>
    {
        public override ReponseRequete exécuter(IAuthentificationMessage message)
        {
            IEntrepotUtilisateur entrepot = EntrepotMongo.fabriquerEntrepot<IEntrepotUtilisateur>();
            Email email = new Email(message.Email);
            if (email.estValide())
                return authentifier(message, entrepot, email);
            return ReponseRequete.générerUnEchec(email.obtenirLErreur());
        }

        private static ReponseRequete authentifier(IAuthentificationMessage message, IEntrepotUtilisateur entrepot, Email email)
        {
            MotDePasse motDePasse = new MotDePasse(message.MotDePasse);
            Utilisateur utilisateur = entrepot.authentifier(email, motDePasse);
            if (utilisateur != null)
            {
                utilisateur.mettreEnSession();
                return formaterLeRésultat(utilisateur);
            }
            return ReponseRequete.générerUnEchec(Ressource.Requetes.AUTH_ECHOUE);
        }

        private static ReponseRequete formaterLeRésultat(Utilisateur utilisateur)
        {
            IEntrepotParametrage paramètres = EntrepotMongo.fabriquerEntrepot<IEntrepotParametrage>();
            AuthentificationReponse résultat = new AuthentificationReponse()
            {
                Civilité = paramètres.récupérerLesCivilités()[utilisateur.Profile.Abréviation],
                Nom = utilisateur.Profile.Nom,
                Prénom = utilisateur.Profile.Prénom,
                TypeUtilisateur = (int) utilisateur.TypeUtilisateur
            };
            return ReponseRequete.générerUnSuccès(résultat);
        }
    }

    public class AuthentificationReponse
    {
        public string Civilité { get; set; }
        public string Nom { get; set; }
        public string Prénom { get; set; }
        public int TypeUtilisateur { get; set; }
    }
}
