using Yeasca.Metier;
using Yeasca.Mongo;

namespace Yeasca.Requete
{
    public class GenererJetonRequete : Requete<IGenererJetonMessage>
    {
        public override ReponseRequete exécuter(IGenererJetonMessage message)
        {
            IEntrepotUtilisateur entrepotUtilisateur = EntrepotMongo.fabriquerEntrepot<IEntrepotUtilisateur>();
            Utilisateur utilisateur = entrepotUtilisateur.authentifier(new Email(message.Email), new MotDePasse(message.MotDePasse));
            if (utilisateur != null && utilisateur.TypeUtilisateur == TypeUtilisateur.Superviseur)
            {
                GenererJetonReponse résultat = new GenererJetonReponse();
                résultat.Jeton = Jeton.générerUnJeton(message.Email);
                return ReponseRequete.générerUnSuccès(résultat);
            }
            return ReponseRequete.générerUnEchec(Ressource.Requetes.AUTH_ECHOUE);
        }
    }

    public class GenererJetonReponse
    {
        public string Jeton { get; set; }
    }
}
