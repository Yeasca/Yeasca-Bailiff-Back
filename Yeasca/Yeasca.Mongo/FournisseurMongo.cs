using System;
using System.Configuration;
using System.Linq;
using System.Reflection;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Yeasca.Metier;

namespace Yeasca.Mongo
{
    public class FournisseurMongo : IFournisseur
    {
        private const string CHAINE_CONNEXION = "connexionBDD";
        private const string NOM_BASE = "nomBDD";

        private static MongoServer _serveur;
        private static MongoDatabase _baseDeDonnées;

        private string _chaineDeConnexion;
        private string _nomBase;

        public FournisseurMongo()
        {
            _chaineDeConnexion = ConfigurationManager.AppSettings[CHAINE_CONNEXION];
            _nomBase = ConfigurationManager.AppSettings[NOM_BASE];
        }

        public void seConnecter()
        {
            try
            {
                if (!EstConnecté)
                {
                    new MappingMongo().enregistrer();
                    MongoClient client = new MongoClient(_chaineDeConnexion);
                    _serveur = client.GetServer();
                    _baseDeDonnées = _serveur.GetDatabase(_nomBase);

                    ajouterUnSuperviseur();

                }
            }
            catch(Exception e)
            {
                Log.loguer("Erreur à la connexion de la base de données", e);
            }
        }

        private void ajouterUnSuperviseur()
        {
            IQueryable<Utilisateur> utilisateurs = obtenirLaCollection<Utilisateur>();
            if (!utilisateurs.Any(x => x.TypeUtilisateur == TypeUtilisateur.Superviseur))
            {
                Utilisateur superviseur = new Utilisateur();
                superviseur.Email.Valeur = "egaichet@gmail.com";
                superviseur.MotDePasse.ValeurDéchiffrée = "Ye@sc@##";
                superviseur.TypeUtilisateur = TypeUtilisateur.Superviseur;
                superviseur.Profile = new Client()
                {
                    Abréviation = Abreviation.Monsieur,
                    Nom = "Gaichet",
                    Prénom = "Emeric",
                    DénominationEntreprise = "Yeasca"
                };
                insérer<Utilisateur>(superviseur);
            }
        }

        public void seDéconnecter()
        {
            if (EstConnecté)
            {
                _serveur.Disconnect();
            }
            _serveur = null;
            _baseDeDonnées = null;
        }

        public bool EstConnecté 
        {
            get
            {
                return _serveur != null 
                    && _baseDeDonnées != null
                    && _baseDeDonnées.Name == _nomBase;
            }
        }

        public IQueryable<T> obtenirLaCollection<T>() where T : IAgregat
        {
            seConnecter();
            string nomDeLaCollection = trouverLeNomDeLaCollectionCorrespondante<T>();
            if(EstConnecté)
                return _baseDeDonnées.GetCollection<T>(nomDeLaCollection).AsQueryable<T>();
            return null;
        }

        private string trouverLeNomDeLaCollectionCorrespondante<T>()
        {
            Type typeDeLAggregat = typeof(T);
            return typeDeLAggregat.GetTypeInfo().Name;
        }

        public bool insérer<T>(IAgregat agrégat) where T : IAgregat
        {
            try
            {
                string nomDeLaCollection = trouverLeNomDeLaCollectionCorrespondante<T>();
                MongoCollection collection = _baseDeDonnées.GetCollection(nomDeLaCollection);
                WriteConcernResult résultat = collection.Save(agrégat);
                return résultat.Ok;
            }
            catch
            {
                return false;
            }
        }

        public bool modifier<T>(IAgregat agrégat) where T : IAgregat
        {
            try
            {
                string nomDeLaCollection = trouverLeNomDeLaCollectionCorrespondante<T>();
                MongoCollection collection = _baseDeDonnées.GetCollection(nomDeLaCollection);
                WriteConcernResult résultat = collection.Save(agrégat);
                return résultat.Ok;
            }
            catch
            {
                return false;
            }
        }
    }
}
