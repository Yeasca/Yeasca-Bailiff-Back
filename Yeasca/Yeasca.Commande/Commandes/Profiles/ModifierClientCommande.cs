
using System;
using Yeasca.Metier;
using Yeasca.Mongo;

namespace Yeasca.Commande
{
    public class ModifierClientCommande : Commande<IModifierClientMessage>
    {
        public override ReponseCommande exécuter(IModifierClientMessage message)
        {
            if (estAuthentifié())
                return procéderALaModificationDuClient(message);
            return ReponseCommande.générerUnEchec(Ressource.Commandes.ERREUR_AUTH);
        }

        private ReponseCommande procéderALaModificationDuClient(IModifierClientMessage message)
        {
            Guid idClient;
            if (Guid.TryParse(message.idClient, out idClient))
                return validerLaRéférenceClientEtModifier(message, idClient);
            return ReponseCommande.générerUnEchec(Ressource.Commandes.REF_CLIENT_INVALIDE);
        }

        private ReponseCommande validerLaRéférenceClientEtModifier(IModifierClientMessage message, Guid idClient)
        {
            IEntrepotProfile entrepot = EntrepotMongo.fabriquerEntrepot<IEntrepotProfile>();
            Profile client = entrepot.récupérerLeClient(idClient);
            if (client != null)
                return validerEtModifierLeClient(message, client, entrepot);
            return ReponseCommande.générerUnEchec(Ressource.Commandes.REF_CLIENT_INVALIDE);
        }

        private ReponseCommande validerEtModifierLeClient(IModifierClientMessage message, Profile client,
            IEntrepotProfile entrepot)
        {
            client.Abréviation = (Abreviation) message.Civilité;
            client.Nom = message.Nom;
            client.Prénom = message.Prénom;
            client.DénominationEntreprise = message.DénominationSociété;
            client.Adresse = actualiserLAdresse(message);
            Erreur erreurs = client.obtenirLesErreurs();
            if (erreurs.Nombre == 0)
                return modifierLeClient(entrepot, client);
            return ReponseCommande.générerUnEchec(erreurs.donnerLaListeEnHTML());
        }

        private static ReponseCommande modifierLeClient(IEntrepotProfile entrepot, Profile client)
        {
            if (entrepot.modifier(client))
                return ReponseCommande.générerUnSuccès();
            return ReponseCommande.générerUnEchec(Ressource.Commandes.ERREUR_MODIFICATION_CLIENT);
        }

        private Adresse actualiserLAdresse(IModifierClientMessage message)
        {
            Adresse nouvelleAdresse = new Adresse();
            nouvelleAdresse.Voie.NuméroVoie.Numéro = message.NuméroVoie;
            nouvelleAdresse.Voie.NuméroVoie.Répétition = message.RépétitionVoie;
            nouvelleAdresse.Voie.NomVoie = message.NomVoie;
            nouvelleAdresse.Voie.ComplémentVoie = message.ComplémentVoie;
            nouvelleAdresse.Ville.CodePostal.Code = message.CodePostal;
            nouvelleAdresse.Ville.LibelléCommune = message.Ville;
            return nouvelleAdresse;
        }
    }
}