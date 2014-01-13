using System;
using Yeasca.Metier;
using Yeasca.Mongo;

namespace Yeasca.Commande
{
    public class ModifierSecretaireCommande : Commande<IModifierSecretaireMessage>
    {
        public override ReponseCommande exécuter(IModifierSecretaireMessage message)
        {
            if (estAdministrateur())
                return procéderALaModification(message);
            return ReponseCommande.générerUnEchec(Ressource.Commandes.ERREUR_AUTH_ADMIN);
        }

        private ReponseCommande procéderALaModification(IModifierSecretaireMessage message)
        {
            Guid idCompte;
            if (Guid.TryParse(message.IdSecrétaire, out idCompte))
                return validerEtModifierLaSecrétaire(message, idCompte);
            return ReponseCommande.générerUnEchec(Ressource.Commandes.REF_SECRETAIRE_INVALIDE);
        }

        private ReponseCommande validerEtModifierLaSecrétaire(IModifierSecretaireMessage message, Guid idCompte)
        {
            IEntrepotUtilisateur entrepotUtilisateur = EntrepotMongo.fabriquerEntrepot<IEntrepotUtilisateur>();
            Utilisateur compteSecrétaire = entrepotUtilisateur.récupérer(idCompte);
            if (compteSecrétaire != null && compteSecrétaire.TypeUtilisateur == TypeUtilisateur.Secrétaire)
                return modifierLaSecrétaire(message, compteSecrétaire, entrepotUtilisateur);
            return ReponseCommande.générerUnEchec(Ressource.Commandes.REF_SECRETAIRE_INVALIDE);
        }

        private ReponseCommande modifierLaSecrétaire(IModifierSecretaireMessage message, Utilisateur compte,
            IEntrepotUtilisateur entrepotUtilisateur)
        {
            IEntrepotProfile entrepotProfile = EntrepotMongo.fabriquerEntrepot<IEntrepotProfile>();
            Secretaire secrétaire = entrepotProfile.récupérerLaSecrétaire(compte.Profile.Id);
            secrétaire.Abréviation = (Abreviation)message.Civilité;
            secrétaire.Nom = message.Nom;
            secrétaire.Prénom = message.Prénom;
            Erreur erreursDuProfile = secrétaire.obtenirLesErreurs();
            if (erreursDuProfile.Nombre > 0)
                return ReponseCommande.générerUnEchec(erreursDuProfile.donnerLaListeEnHTML());
            if (entrepotProfile.modifier(secrétaire))
                return modifierLeCompteSecrétaire(message, compte, entrepotUtilisateur);
            return ReponseCommande.générerUnEchec(Ressource.Commandes.ERREUR_MODIFICATION_SECRETAIRE);
        }

        private ReponseCommande modifierLeCompteSecrétaire(IModifierSecretaireMessage message, Utilisateur compte,
            IEntrepotUtilisateur entrepotUtilisateur)
        {
            if (modifieLeMotDePasse(message))
                compte.MotDePasse.ValeurDéchiffrée = message.MotDePasse;
            compte.Email.Valeur = message.Email;
            Erreur erreursDuCompte = compte.obtenirLesErreurs();
            if (erreursDuCompte.Nombre > 0)
                return ReponseCommande.générerUnEchec(erreursDuCompte.donnerLaListeEnHTML());
            if (entrepotUtilisateur.modifier(compte))
                return ReponseCommande.générerUnSuccès();
            return ReponseCommande.générerUnEchec(Ressource.Commandes.ERREUR_MODIFICATION_COMPTE_SECRETAIRE);
        }

        private bool modifieLeMotDePasse(IModifierSecretaireMessage message)
        {
            return !string.IsNullOrEmpty(message.MotDePasse);
        }
    }
}
