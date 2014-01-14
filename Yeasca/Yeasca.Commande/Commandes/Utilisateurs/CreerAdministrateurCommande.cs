using Yeasca.Metier;
using Yeasca.Mongo;

namespace Yeasca.Commande
{
    public class CreerAdministrateurCommande : Commande<ICreerAdministrateurMessage>
    {
        public override ReponseCommande exécuter(ICreerAdministrateurMessage message)
        {
            IEntrepotJeton entrepotJeton = EntrepotMongo.fabriquerEntrepot<IEntrepotJeton>();
            Jeton jeton = new Jeton(message.Jeton);
            if (peutCréerUnAdmin(message, jeton, entrepotJeton))
                return créerLeProfileEtLeCompte(message, entrepotJeton, jeton);
            return ReponseCommande.générerUnEchec(Ressource.Commandes.JETON_INVALIDE);
        }

        private bool peutCréerUnAdmin(ICreerAdministrateurMessage message, Jeton jeton, IEntrepotJeton entrepotJeton)
        {
            return jeton.estValide(message.Email) && !entrepotJeton.aEtéUtilisé(message.Jeton)
                || estSuperviseur();
        }

        private ReponseCommande créerLeProfileEtLeCompte(ICreerAdministrateurMessage message, IEntrepotJeton entrepotJeton,
            Jeton jeton)
        {
            Huissier huissier = new Huissier();
            huissier.Abréviation = (Abreviation) message.Civilité;
            huissier.Nom = message.Nom;
            huissier.Prénom = message.Prénom;
            huissier.DénominationEntreprise = message.NomCabinet;
            huissier.Adresse = initialiserLAdresse(message);
            Erreur erreursDuProfile = huissier.obtenirLesErreurs();
            if (erreursDuProfile.Nombre > 0)
                return ReponseCommande.générerUnEchec(erreursDuProfile.donnerLaListeEnHTML());
            IEntrepotProfile entrepotProfile = EntrepotMongo.fabriquerEntrepot<IEntrepotProfile>();
            if (entrepotProfile.ajouter(huissier))
                return créerLeCompte(message, huissier, entrepotJeton, jeton);
            return ReponseCommande.générerUnEchec(Ressource.Commandes.ERREUR_CRÉATION_ADMIN);
        }

        private ReponseCommande créerLeCompte(ICreerAdministrateurMessage message, Huissier huissier,
            IEntrepotJeton entrepotJeton, Jeton jeton)
        {
            Utilisateur compte = new Utilisateur();
            compte.Email.Valeur = message.Email;
            compte.MotDePasse.ValeurDéchiffrée = message.MotDePasse;
            compte.TypeUtilisateur = TypeUtilisateur.Administrateur;
            compte.Profile = huissier;
            Erreur erreursDuCompte = compte.obtenirLesErreurs();
            if (erreursDuCompte.Nombre > 0)
                return ReponseCommande.générerUnEchec(erreursDuCompte.donnerLaListeEnHTML());
            IEntrepotUtilisateur entrepotUtilisateur = EntrepotMongo.fabriquerEntrepot<IEntrepotUtilisateur>();
            if (entrepotUtilisateur.ajouter(compte))
                return créer(entrepotJeton, jeton, compte);
            return ReponseCommande.générerUnEchec(Ressource.Commandes.ERREUR_CRÉATION_COMPTE_ADMIN);
        }

        private ReponseCommande créer(IEntrepotJeton entrepotJeton, Jeton jeton, Utilisateur compte)
        {
            if (!string.IsNullOrEmpty(jeton.Valeur))
                entrepotJeton.ajouter(jeton);
            return ReponseCommande.générerUnSuccès(compte.Id.ToString());
        }

        private Adresse initialiserLAdresse(ICreerAdministrateurMessage message)
        {
            Adresse nouvelleAdresse = new Adresse();
            nouvelleAdresse.Voie.NuméroVoie.Numéro = message.NuméroVoie;
            nouvelleAdresse.Voie.NuméroVoie.Répétition = message.RépétitionVoie;
            nouvelleAdresse.Voie.TypeVoie = message.TypeVoie;
            nouvelleAdresse.Voie.NomVoie = message.NomVoie;
            nouvelleAdresse.Voie.ComplémentVoie = message.ComplémentVoie;
            nouvelleAdresse.Ville.CodePostal.Code = message.CodePostal;
            nouvelleAdresse.Ville.LibelléCommune = message.Ville;
            return nouvelleAdresse;
        }
    }
}
