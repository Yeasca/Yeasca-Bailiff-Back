using System;
using Yeasca.Metier;
using Yeasca.Mongo;

namespace Yeasca.Commande
{
    public class AjouterFichierConstatCommande : Commande<IAjouterFichierConstatMessage>
    {
        public override ReponseCommande exécuter(IAjouterFichierConstatMessage message)
        {
            if (estUnHuissier())
                return validerEtEnregistrer(message);
            return ReponseCommande.générerUnEchec(Ressource.Commandes.ERREUR_AUTH_HUISSIER);
        }

        private static ReponseCommande validerEtEnregistrer(IAjouterFichierConstatMessage message)
        {
            Guid idConstat;
            if (Guid.TryParse(message.IdConstat, out idConstat))
                return procéderALenregistrement(message, idConstat);
            return ReponseCommande.générerUnEchec(Ressource.Commandes.REF_CONSTAT_INVALIDE);
        }

        private static ReponseCommande procéderALenregistrement(IAjouterFichierConstatMessage message, Guid idConstat)
        {
            IEntrepotConstat entrepot = EntrepotMongo.fabriquerEntrepot<IEntrepotConstat>();
            Constat constat = entrepot.récupérerLeConstat(idConstat);
            if (constat != null)
                return enregistrerLeFichier(message, constat, entrepot);
            return ReponseCommande.générerUnEchec(Ressource.Commandes.REF_CONSTAT_INVALIDE);
        }

        private static ReponseCommande enregistrerLeFichier(IAjouterFichierConstatMessage message, Constat constat,
            IEntrepotConstat entrepot)
        {
            Fichier fichierAAjouter = Fichier.enregistrerLeFichier(message.Fichier, message.Nom, message.Extension);
            if (fichierAAjouter != null)
                return modifierLeConstat(constat, fichierAAjouter, entrepot);
            return ReponseCommande.générerUnEchec(Ressource.Commandes.ERREUR_AJOUT_FICHIER);
        }

        private static ReponseCommande modifierLeConstat(Constat constat, Fichier fichierAAjouter, IEntrepotConstat entrepot)
        {
            constat.Fichiers.Add(fichierAAjouter);
            if (entrepot.modifier(constat))
                return ReponseCommande.générerUnSuccès();
            return ReponseCommande.générerUnEchec(Ressource.Commandes.ERREUR_ENREG_FICHIER);
        }
    }
}
