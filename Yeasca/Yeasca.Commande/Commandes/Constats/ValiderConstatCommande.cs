using System;
using Yeasca.Metier;
using Yeasca.Mongo;

namespace Yeasca.Commande
{
    public class ValiderConstatCommande : Commande<IValiderConstatMessage>
    {
        public override ReponseCommande exécuter(IValiderConstatMessage message)
        {
            if (estAuthentifié())
                return validerEtModifierLeConstat(message);
            return ReponseCommande.générerUnEchec(Ressource.Commandes.ERREUR_AUTH);
        }

        private static ReponseCommande validerEtModifierLeConstat(IValiderConstatMessage message)
        {
            Guid idConstat;
            if (Guid.TryParse(message.IdConstat, out idConstat))
                return récupérerEtModifierLeConstat(message, idConstat);
            return ReponseCommande.générerUnEchec(Ressource.Commandes.REF_CONSTAT_INVALIDE);
        }

        private static ReponseCommande récupérerEtModifierLeConstat(IValiderConstatMessage message, Guid idConstat)
        {
            IEntrepotConstat entrepot = EntrepotMongo.fabriquerEntrepot<IEntrepotConstat>();
            Constat constat = entrepot.récupérerLeConstat(idConstat);
            if (constat != null && !constat.EstValidé)
                return ajouterLeDocumentWordAuConstat(message, constat, entrepot);
            return ReponseCommande.générerUnEchec(Ressource.Commandes.REF_CONSTAT_INVALIDE);
        }

        private static ReponseCommande ajouterLeDocumentWordAuConstat(IValiderConstatMessage message, Constat constat,
            IEntrepotConstat entrepot)
        {
            Fichier wordDuConstat = Fichier.enregistrerLeDocumentWord(message.Fichier, message.Nom);
            if (wordDuConstat != null)
                return modifierLeConstat(constat, wordDuConstat, entrepot);
            return ReponseCommande.générerUnEchec(Ressource.Commandes.ERREUR_AJOUT_FICHIER);
        }

        private static ReponseCommande modifierLeConstat(Constat constat, Fichier wordDuConstat, IEntrepotConstat entrepot)
        {
            constat.Fichiers.Add(wordDuConstat);
            if (entrepot.modifier(constat))
                return ReponseCommande.générerUnSuccès();
            return ReponseCommande.générerUnEchec(Ressource.Commandes.ERREUR_VALIDATION_CONSTAT);
        }
    }
}
