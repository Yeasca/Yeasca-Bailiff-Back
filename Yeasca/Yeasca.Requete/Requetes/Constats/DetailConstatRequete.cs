using System;
using System.Collections.Generic;
using Yeasca.Metier;
using Yeasca.Mongo;

namespace Yeasca.Requete
{
    public class DetailConstatRequete : Requete<IDetailConstatMessage>
    {
        public override ReponseRequete exécuter(IDetailConstatMessage message)
        {
            if (estAuthentifié())
                return lancerLaRequete(message);
            return ReponseRequete.générerUnEchec(Ressource.Requetes.AUTH_REQUISE);
        }

        private ReponseRequete lancerLaRequete(IDetailConstatMessage message)
        {
            Guid idConstat;
            if (!Guid.TryParse(message.IdConstat, out idConstat))
                return ReponseRequete.générerUnEchec(Ressource.Requetes.REF_CONSTAT_INVALIDE);
            IEntrepotConstat entrepot = EntrepotMongo.fabriquerEntrepot<IEntrepotConstat>();
            Constat constat = entrepot.récupérerLeConstat(idConstat);
            if (constat != null)
                return obtenirLaRéponse(constat);
            return ReponseRequete.générerUnEchec(Ressource.Requetes.REF_CONSTAT_INVALIDE);
        }

        private ReponseRequete obtenirLaRéponse(Constat constat)
        {
            DetailConstatReponse résultat = initialiserLaRéponse(constat);
            ajouterLesFichiers(résultat, constat);
            return ReponseRequete.générerUnSuccès(résultat);
        }

        private DetailConstatReponse initialiserLaRéponse(Constat constat)
        {
            IEntrepotParametrage paramètres = EntrepotMongo.fabriquerEntrepot<IEntrepotParametrage>();
            DetailConstatReponse résultat = new DetailConstatReponse();
            résultat.IdConstat = constat.Id.ToString();
            résultat.Date = constat.Date.ToString(Ressource.Paramètres.FORMAT_DATE);
            résultat.CivilitéClient = paramètres.récupérerLesCivilités()[constat.Client.Abréviation];
            résultat.NomClient = constat.Client.Nom;
            résultat.PrénomClient = constat.Client.Prénom;
            résultat.CivilitéHuissier = paramètres.récupérerLesCivilités()[constat.Huissier.Abréviation];
            résultat.NomHuissier = constat.Huissier.Nom;
            résultat.PrénomHuissier = constat.Huissier.Prénom;
            résultat.Traité = constat.EstValidé;
            return résultat;
        }

        private void ajouterLesFichiers(DetailConstatReponse résultat, Constat constat)
        {
            résultat.Fichiers = new List<DetailFichierReponse>();
            foreach (Fichier fichier in constat.Fichiers)
            {
                résultat.Fichiers.Add(new DetailFichierReponse()
                {
                    URL = fichier.URLFichier,
                    Nom = fichier.NomComplet
                });
            }
        }
    }

    public class DetailConstatReponse
    {
        public string IdConstat { get; set; }
        public string Date { get; set; }
        public string CivilitéClient { get; set; }
        public string NomClient { get; set; }
        public string PrénomClient { get; set; }
        public string CivilitéHuissier { get; set; }
        public string NomHuissier { get; set; }
        public string PrénomHuissier { get; set; }
        public bool Traité { get; set; }
        public List<DetailFichierReponse> Fichiers { get; set; } 
    }

    public class DetailFichierReponse
    {
        public string URL { get; set; }
        public string Nom { get; set; }
    }
}
