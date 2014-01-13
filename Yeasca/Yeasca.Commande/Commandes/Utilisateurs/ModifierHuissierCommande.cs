using System;
using Yeasca.Metier;
using Yeasca.Mongo;

namespace Yeasca.Commande
{
    public class ModifierHuissierCommande : Commande<IModifierHuissierMessage>
    {
        public override ReponseCommande exécuter(IModifierHuissierMessage message)
        {
            if (estAdministrateur())
                return procéderALaModification(message);
            return ReponseCommande.générerUnEchec(Ressource.Commandes.ERREUR_AUTH_ADMIN);
        }

        private ReponseCommande procéderALaModification(IModifierHuissierMessage message)
        {
            Guid idCompte;
            if (Guid.TryParse(message.IdHuissier, out idCompte))
                return validerEtModifierLHuissier(message, idCompte);
            return ReponseCommande.générerUnEchec(Ressource.Commandes.REF_HUISSIER_INVALIDE);
        }

        private ReponseCommande validerEtModifierLHuissier(IModifierHuissierMessage message, Guid idCompte)
        {
            IEntrepotUtilisateur entrepotUtilisateur = EntrepotMongo.fabriquerEntrepot<IEntrepotUtilisateur>();
            Utilisateur compteHuissier = entrepotUtilisateur.récupérer(idCompte);
            if (compteHuissier != null && compteHuissier.TypeUtilisateur == TypeUtilisateur.Huissier)
                return modifierLHuissier(message, compteHuissier, entrepotUtilisateur);
            return ReponseCommande.générerUnEchec(Ressource.Commandes.REF_HUISSIER_INVALIDE);
        }

        private ReponseCommande modifierLHuissier(IModifierHuissierMessage message, Utilisateur compteHuissier,
            IEntrepotUtilisateur entrepotUtilisateur)
        {
            IEntrepotProfile entrepotProfile = EntrepotMongo.fabriquerEntrepot<IEntrepotProfile>();
            Huissier huissier = entrepotProfile.récupérerLHuissier(compteHuissier.Profile.Id);
            huissier.Abréviation = (Abreviation) message.Civilité;
            huissier.Nom = message.Nom;
            huissier.Prénom = message.Prénom;
            Erreur erreursDuProfile = huissier.obtenirLesErreurs();
            if (erreursDuProfile.Nombre > 0)
                return ReponseCommande.générerUnEchec(erreursDuProfile.donnerLaListeEnHTML());
            if (entrepotProfile.modifier(huissier))
                return modifierLeCompteHuissier(message, compteHuissier, entrepotUtilisateur);
            return ReponseCommande.générerUnEchec(Ressource.Commandes.ERREUR_MODIFICATION_HUISSIER);
        }

        private ReponseCommande modifierLeCompteHuissier(IModifierHuissierMessage message, Utilisateur compteHuissier,
            IEntrepotUtilisateur entrepotUtilisateur)
        {
            if (modifieLeMotDePasse(message))
                compteHuissier.MotDePasse.ValeurDéchiffrée = message.MotDePasse;
            compteHuissier.Email.Valeur = message.Email;
            Erreur erreursDuCompte = compteHuissier.obtenirLesErreurs();
            if (erreursDuCompte.Nombre > 0)
                return ReponseCommande.générerUnEchec(erreursDuCompte.donnerLaListeEnHTML());
            if (entrepotUtilisateur.modifier(compteHuissier))
                return ReponseCommande.générerUnSuccès();
            return ReponseCommande.générerUnEchec(Ressource.Commandes.ERREUR_MODIFICATION_COMPTE_HUISSIER);
        }

        private bool modifieLeMotDePasse(IModifierHuissierMessage message)
        {
            return !string.IsNullOrEmpty(message.MotDePasse);
        }
    }
}
