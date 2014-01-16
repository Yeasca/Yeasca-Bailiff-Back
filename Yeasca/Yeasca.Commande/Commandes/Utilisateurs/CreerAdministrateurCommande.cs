using Yeasca.Metier;
using Yeasca.Mongo;

namespace Yeasca.Commande
{
    public class CreerAdministrateurCommande : Commande<ICreerAdministrateurMessage>
    {
        public override ReponseCommande exécuter(ICreerAdministrateurMessage message)
        {
            IEntrepotUtilisateur entrepotUtilisateur = EntrepotMongo.fabriquerEntrepot<IEntrepotUtilisateur>();
            Utilisateur adminExistant = entrepotUtilisateur.récupérerLAdministrateur();
            if (peutCréerUnAdmin(adminExistant))
                return créerLeProfileEtLeCompte(message);
            return ReponseCommande.générerUnEchec(Ressource.Commandes.COMPTE_ADMIN_EXIST);
        }

        private bool peutCréerUnAdmin(Utilisateur adminExistant)
        {
            return adminExistant == null;
        }

        private ReponseCommande créerLeProfileEtLeCompte(ICreerAdministrateurMessage message)
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
                return créerLeCompte(message, huissier);
            return ReponseCommande.générerUnEchec(Ressource.Commandes.ERREUR_CRÉATION_ADMIN);
        }

        private ReponseCommande créerLeCompte(ICreerAdministrateurMessage message, Huissier huissier)
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
                return ReponseCommande.générerUnSuccès(compte.Id.ToString());
            return ReponseCommande.générerUnEchec(Ressource.Commandes.ERREUR_CRÉATION_COMPTE_ADMIN);
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
