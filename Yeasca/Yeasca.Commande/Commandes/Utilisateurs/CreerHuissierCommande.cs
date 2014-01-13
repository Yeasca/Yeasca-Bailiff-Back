using Yeasca.Metier;
using Yeasca.Mongo;

namespace Yeasca.Commande
{
    public class CreerHuissierCommande : Commande<ICreerHuissierMessage>
    {
        public override ReponseCommande exécuter(ICreerHuissierMessage message)
        {
            if (estAdministrateur())
                return validerEtCréerLeProfile(message);
            return ReponseCommande.générerUnEchec(Ressource.Commandes.ERREUR_AUTH_ADMIN);
        }

        private static ReponseCommande validerEtCréerLeProfile(ICreerHuissierMessage message)
        {
            Utilisateur créateur = Utilisateur.chargerDepuisLaSession();
            Huissier profile = new Huissier();
            profile.Abréviation = (Abreviation) message.Civilité;
            profile.Nom = message.Nom;
            profile.Prénom = message.Prénom;
            profile.Adresse = créateur.Profile.Adresse;
            profile.DénominationEntreprise = créateur.Profile.DénominationEntreprise;
            Erreur erreursDuProfile = profile.obtenirLesErreurs();
            if (erreursDuProfile.Nombre == 0)
                return validerEtCréerLeCompte(message, profile);
            return ReponseCommande.générerUnEchec(erreursDuProfile.donnerLaListeEnHTML());
        }

        private static ReponseCommande validerEtCréerLeCompte(ICreerHuissierMessage message, Huissier profile)
        {
            IEntrepotProfile entrepotProfile = EntrepotMongo.fabriquerEntrepot<IEntrepotProfile>();
            if (entrepotProfile.ajouter(profile))
                return validerLeCompte(message, profile);
            return ReponseCommande.générerUnEchec(Ressource.Commandes.ERREUR_CRÉATION_HUISSIER);
        }

        private static ReponseCommande validerLeCompte(ICreerHuissierMessage message, Huissier profile)
        {
            Utilisateur nouveauCompte = new Utilisateur();
            nouveauCompte.Id = profile.Id;
            nouveauCompte.Email.Valeur = message.Email;
            nouveauCompte.MotDePasse.ValeurDéchiffrée = message.MotDePasse;
            nouveauCompte.TypeUtilisateur = TypeUtilisateur.Huissier;
            nouveauCompte.Profile = profile;
            Erreur erreursDuCompte = nouveauCompte.obtenirLesErreurs();
            if (erreursDuCompte.Nombre == 0)
                return créerLeCompte(nouveauCompte);
            return ReponseCommande.générerUnEchec(erreursDuCompte.donnerLaListeEnHTML());
        }

        private static ReponseCommande créerLeCompte(Utilisateur nouveauCompte)
        {
            IEntrepotUtilisateur entrepotUtilisateur =
                EntrepotMongo.fabriquerEntrepot<IEntrepotUtilisateur>();
            if (entrepotUtilisateur.ajouter(nouveauCompte))
                return ReponseCommande.générerUnSuccès(nouveauCompte.Id.ToString());
            return ReponseCommande.générerUnEchec(Ressource.Commandes.ERREUR_CRÉATION_COMPTE_HUISSIER);
        }
    }
}
