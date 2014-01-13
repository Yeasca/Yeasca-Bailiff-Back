
using System;
using Yeasca.Metier;
using Yeasca.Mongo;

namespace Yeasca.Requete
{
    public class DetailUtilisateurRequete : Requete<IDetailUtilisateurMessage>
    {
        public override ReponseRequete exécuter(IDetailUtilisateurMessage message)
        {
            if (estAuthentifié())
                return lancerLaRequete(message);
            return ReponseRequete.générerUnEchec(Ressource.Requetes.AUTH_REQUISE);
        }

        private ReponseRequete lancerLaRequete(IDetailUtilisateurMessage message)
        {
            Guid idUtilisateur;
            if (!Guid.TryParse(message.IdUtilisateur, out idUtilisateur))
                return ReponseRequete.générerUnEchec(Ressource.Requetes.REF_UTILISATEUR_INVALIDE);
            IEntrepotUtilisateur entrepot = EntrepotMongo.fabriquerEntrepot<IEntrepotUtilisateur>();
            Utilisateur utilisateur = entrepot.récupérer(idUtilisateur);
            if (utilisateur != null)
            {
                DetailUtilisateurReponse résultat = initialiserLeRésultat(utilisateur);
                return ReponseRequete.générerUnSuccès(résultat);
            }
            return ReponseRequete.générerUnEchec(Ressource.Requetes.REF_UTILISATEUR_INVALIDE);
        }

        private DetailUtilisateurReponse initialiserLeRésultat(Utilisateur utilisateur)
        {
            IEntrepotParametrage paramètres = EntrepotMongo.fabriquerEntrepot<IEntrepotParametrage>();
            DetailUtilisateurReponse résultat = new DetailUtilisateurReponse();
            résultat.IdUtilisateur = utilisateur.Id.ToString();
            résultat.Civilité = (int) utilisateur.Profile.Abréviation;
            résultat.Nom = utilisateur.Profile.Nom;
            résultat.Prénom = utilisateur.Profile.Prénom;
            résultat.LibelléTypeCompte = paramètres.récupérerLesTypesUtilisateurs()[utilisateur.TypeUtilisateur];
            ajouterLesInfosDeLAdmin(résultat, utilisateur);
            return résultat;
        }

        private void ajouterLesInfosDeLAdmin(DetailUtilisateurReponse résultat, Utilisateur utilisateur)
        {
            if (estAdministrateur())
            {
                résultat.Email = utilisateur.Email.Valeur;
                résultat.MotDePasse = utilisateur.MotDePasse.ValeurDéchiffrée;
                résultat.TypeCompte = (int) utilisateur.TypeUtilisateur;
                résultat.NomCabinet = utilisateur.Profile.DénominationEntreprise;
                résultat.NuméroVoie = utilisateur.Profile.Adresse.Voie.NuméroVoie.Numéro;
                résultat.RépétitionVoie = utilisateur.Profile.Adresse.Voie.NuméroVoie.Répétition;
                résultat.TypeVoie = utilisateur.Profile.Adresse.Voie.TypeVoie;
                résultat.NomVoie = utilisateur.Profile.Adresse.Voie.NomVoie;
                résultat.ComplémentVoie = utilisateur.Profile.Adresse.Voie.ComplémentVoie;
                résultat.CodePostal = utilisateur.Profile.Adresse.Ville.CodePostal.Code;
                résultat.Ville = utilisateur.Profile.Adresse.Ville.LibelléCommune;
            }
        }
    }

    public class DetailUtilisateurReponse
    {
        public string IdUtilisateur { get; set; }
        public string Email { get; set; }
        public string MotDePasse { get; set; }
        public int TypeCompte { get; set; }
        public string LibelléTypeCompte { get; set; }
        public int Civilité { get; set; }
        public string Nom { get; set; }
        public string Prénom { get; set; }
        public string NomCabinet { get; set; }
        public string NuméroVoie { get; set; }
        public string RépétitionVoie { get; set; }
        public string TypeVoie { get; set; }
        public string NomVoie { get; set; }
        public string ComplémentVoie { get; set; }
        public string CodePostal { get; set; }
        public string Ville { get; set; }
    }
}
