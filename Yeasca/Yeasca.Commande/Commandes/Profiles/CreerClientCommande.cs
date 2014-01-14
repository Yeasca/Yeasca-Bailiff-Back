using System;
using System.Collections.Generic;
using System.Linq;
using Yeasca.Metier;
using Yeasca.Mongo;

namespace Yeasca.Commande
{
    public class CreerClientCommande : Commande<ICreerClientMessage>
    {
        public override ReponseCommande exécuter(ICreerClientMessage message)
        {
            if (estAuthentifié())
            {
                Adresse nouvelleAdresse = initialiserLAdresse(message);
                return enregistrerUnClientParticulier(message, nouvelleAdresse);
            }
            return ReponseCommande.générerUnEchec(Ressource.Commandes.ERREUR_AUTH);
        }

        private Adresse initialiserLAdresse(ICreerClientMessage message)
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

        private ReponseCommande enregistrerUnClientParticulier(ICreerClientMessage message, Adresse nouvelleAdresse)
        {
            Client client = new Client();
            client.Abréviation = (Abreviation) message.Civilité;
            client.Nom = message.Nom;
            client.Prénom = message.Prénom;
            client.Adresse = nouvelleAdresse;
            client.DénominationEntreprise = message.DénominationSociété;
            Erreur erreurs = client.obtenirLesErreurs();
            return validerEtEnregistrerLeProfile(client, erreurs);
        }

        private ReponseCommande validerEtEnregistrerLeProfile(Profile partie, Erreur erreurs )
        {
            if (erreurs.Nombre == 0)
            {
                IEntrepotProfile entrepot = EntrepotMongo.fabriquerEntrepot<IEntrepotProfile>();
                return enregistrerLeProfile(partie, entrepot);
            }
            return ReponseCommande.générerUnEchec(erreurs.donnerLaListeEnHTML());
        }

        private static ReponseCommande enregistrerLeProfile(Profile partie, IEntrepotProfile entrepot)
        {
            if (entrepot.ajouter(partie))
                return ReponseCommande.générerUnSuccès(partie.Id.ToString());
            return ReponseCommande.générerUnEchec(Ressource.Commandes.ERREUR_CRÉATION_CLIENT);
        }
    }
}
