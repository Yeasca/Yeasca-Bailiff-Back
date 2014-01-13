using System;
using System.Collections.Generic;
using System.Linq;
using Yeasca.Metier;
using Yeasca.Mongo;

namespace Yeasca.Requete
{
    public class DetailClientRequete : Requete<IDetailClientMessage>
    {
        public override ReponseRequete exécuter(IDetailClientMessage message)
        {
            if (estAuthentifié())
                return lancerLaRequete(message);
            return ReponseRequete.générerUnEchec(Ressource.Requetes.AUTH_REQUISE);
        }

        private ReponseRequete lancerLaRequete(IDetailClientMessage message)
        {
            Guid idClient;
            if (!Guid.TryParse(message.IdClient, out idClient))
                return ReponseRequete.générerUnEchec(Ressource.Requetes.REF_CLIENT_INVALIDE);
            IEntrepotProfile entrepotProfile = EntrepotMongo.fabriquerEntrepot<IEntrepotProfile>();
            Client client = entrepotProfile.récupérerLeClient(idClient);
            if (client != null)
                return formaterLaRéponse(client);
            return ReponseRequete.générerUnEchec(Ressource.Requetes.REF_CLIENT_INVALIDE);
        }

        private ReponseRequete formaterLaRéponse(Client client)
        {
            DetailClientReponse résultat = initialiserLeRésultat(client);
            ajouterLesConstats(client, résultat);
            return ReponseRequete.générerUnSuccès(résultat);
        }

        private DetailClientReponse initialiserLeRésultat(Client client)
        {
            DetailClientReponse résultat = new DetailClientReponse();
            résultat.IdClient = client.Id.ToString();
            résultat.Civilité = (int) client.Abréviation;
            résultat.Nom = client.Nom;
            résultat.Prénom = client.Prénom;
            résultat.Société = client.DénominationEntreprise;
            résultat.NuméroVoie = client.Adresse.Voie.NuméroVoie.Numéro;
            résultat.RépétitionVoie = client.Adresse.Voie.NuméroVoie.Répétition;
            résultat.TypeVoie = client.Adresse.Voie.TypeVoie;
            résultat.NomVoie = client.Adresse.Voie.NomVoie;
            résultat.ComplémentVoie = client.Adresse.Voie.ComplémentVoie;
            résultat.CodePostal = client.Adresse.Ville.CodePostal.Code;
            résultat.Ville = client.Adresse.Ville.LibelléCommune;
            return résultat;
        }

        private void ajouterLesConstats(Client client, DetailClientReponse résultat)
        {
            IEntrepotConstat entrepotConstat = EntrepotMongo.fabriquerEntrepot<IEntrepotConstat>();
            IList<Constat> constatsDuClient = entrepotConstat.récupérerLaListeDesConstatsDuClient(client.Id);
            résultat.Constats = constatsDuClient.Select(x => new ResumeConstatClientReponse()
            {
                IdConstat = x.Id.ToString(),
                Date = x.Date.ToString(Ressource.Paramètres.FORMAT_DATE),
                NomHuissier = x.Huissier.Nom,
                PrénomHuissier = x.Huissier.Prénom
            }).ToList();
        }
    }

    public class DetailClientReponse
    {
        public string IdClient { get; set; }
        public int Civilité { get; set; }
        public string Nom { get; set; }
        public string Prénom { get; set; }
        public string Société { get; set; }
        public string NuméroVoie { get; set; }
        public string RépétitionVoie { get; set; }
        public string TypeVoie { get; set; }
        public string NomVoie { get; set; }
        public string ComplémentVoie { get; set; }
        public string CodePostal { get; set; }
        public string Ville { get; set; }
        public List<ResumeConstatClientReponse> Constats { get; set; } 
    }

    public class ResumeConstatClientReponse
    {
        public string IdConstat { get; set; }
        public string Date { get; set; }
        public string NomHuissier { get; set; }
        public string PrénomHuissier { get; set; }
    }
}
