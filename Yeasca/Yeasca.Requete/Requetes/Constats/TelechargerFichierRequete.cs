using System;
using System.Linq;
using Yeasca.Metier;
using Yeasca.Mongo;

namespace Yeasca.Requete
{
    public class TelechargerFichierRequete : Requete<ITelechargerFichierMessage>
    {
        public override ReponseRequete exécuter(ITelechargerFichierMessage message)
        {
            if (estAuthentifié())
                return trouverLeConstat(message);
            return ReponseRequete.générerUnEchec(Ressource.Requetes.AUTH_REQUISE);
        }

        private ReponseRequete trouverLeConstat(ITelechargerFichierMessage message)
        {
            Guid idConstat, idFichier;
            if (!Guid.TryParse(message.IdConstat, out idConstat) || !Guid.TryParse(message.IdFichier, out idFichier))
                return ReponseRequete.générerUnEchec(Ressource.Requetes.REF_CONSTAT_INVALIDE);
            IEntrepotConstat entrepot = EntrepotMongo.fabriquerEntrepot<IEntrepotConstat>();
            Constat constat = entrepot.récupérerLeConstat(idConstat);
            if (constat != null)
                return trouverLeFichier(constat, idFichier);
            return ReponseRequete.générerUnEchec(Ressource.Requetes.REF_CONSTAT_INVALIDE);
        }

        private ReponseRequete trouverLeFichier(Constat constat, Guid idFichier)
        {
            Fichier fichier = constat.Fichiers.SingleOrDefault(x => x.Id == idFichier);
            if (fichier != null)
            {
                FichierReponse résultat = new FichierReponse();
                résultat.Chemin = fichier.PathFichier;
                résultat.Nom = fichier.Nom;
                résultat.TypeMIME = fichier.TypeMIMEDuFichier;
                return ReponseRequete.générerUnSuccès(résultat);
            }
            return ReponseRequete.générerUnEchec(Ressource.Requetes.REF_CONSTAT_INVALIDE);
        }
    }

    public class FichierReponse
    {
        public string Chemin { get; set; }
        public string Nom { get; set; }
        public string TypeMIME { get; set; }
    }
}
