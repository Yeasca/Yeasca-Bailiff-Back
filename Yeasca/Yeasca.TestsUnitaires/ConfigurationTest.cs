using System;
using System.Web;
using Yeasca.Metier;
using Yeasca.Mongo;

namespace Yeasca.TestsUnitaires
{
    public class ConfigurationTest
    {
        public static void initialiserLesEntrepotsMongo()
        {
            ModuleInjection module = EntrepotMongo.obtenirLesParamètresParDéfaut();
            module.lier<IFournisseur>().à<FournisseurTest>().enSingleton();
            EntrepotMongo.paramétrer(module);
        }

        public static void initialiserLaSessionHTTP(TypeUtilisateur typeUtilisateur)
        {
            HttpContext.Current = ConstantesTest.CONTEXTE_HTTP;
            CacheUtilisateur.réinitialiserLeCacheUtilisateur();
            initialiserLUtilisateur(typeUtilisateur);
        }

        private static void initialiserLUtilisateur(TypeUtilisateur typeUtilisateur)
        {
            Utilisateur huissier = new Utilisateur() { TypeUtilisateur = typeUtilisateur };
            IEntrepotUtilisateur entrepotUtilisateur = EntrepotMongo.fabriquerEntrepot<IEntrepotUtilisateur>();
            entrepotUtilisateur.ajouter(huissier);
            Huissier profileHuissier = new Huissier();
            profileHuissier.Id = huissier.Id;
            IEntrepotProfile entrepotPartie = EntrepotMongo.fabriquerEntrepot<IEntrepotProfile>();
            entrepotPartie.ajouter(profileHuissier);
            huissier.Profile = profileHuissier;
            entrepotUtilisateur.modifier(huissier);
            huissier.mettreEnSession();
        }

        public static Constat créerUnConstatDeTest()
        {
            Constat constat = new Constat();
            Huissier huissier = new Huissier();
            Client client = new Client();
            IEntrepotProfile entrepotPartie = EntrepotMongo.fabriquerEntrepot<IEntrepotProfile>();
            IEntrepotConstat entrepotConstat = EntrepotMongo.fabriquerEntrepot<IEntrepotConstat>();
            entrepotPartie.ajouter(huissier);
            entrepotPartie.ajouter(client);
            constat.Huissier = huissier;
            constat.Client = client;
            entrepotConstat.ajouter(constat);
            return constat;
        }

        public static Client créerUnClient()
        {
            Client client = new Client();
            IEntrepotProfile entrepot = EntrepotMongo.fabriquerEntrepot<IEntrepotProfile>();
            entrepot.ajouter(client);
            return client;
        }

        public static Utilisateur créerUnHuissier()
        {
            Huissier huissier = new Huissier();
            IEntrepotProfile entrepotProfile = EntrepotMongo.fabriquerEntrepot<IEntrepotProfile>();
            entrepotProfile.ajouter(huissier);
            Utilisateur compteHuissier = new Utilisateur();
            compteHuissier.TypeUtilisateur = TypeUtilisateur.Huissier;
            compteHuissier.Profile = huissier;
            IEntrepotUtilisateur entrepotUtilisateur = EntrepotMongo.fabriquerEntrepot<IEntrepotUtilisateur>();
            entrepotUtilisateur.ajouter(compteHuissier);
            return compteHuissier;
        }

        public static Utilisateur créerUneSecrétaire()
        {
            Secretaire secrétaire = new Secretaire();
            IEntrepotProfile entrepotProfile = EntrepotMongo.fabriquerEntrepot<IEntrepotProfile>();
            entrepotProfile.ajouter(secrétaire);
            Utilisateur compteSecrétaire = new Utilisateur();
            compteSecrétaire.TypeUtilisateur = TypeUtilisateur.Secrétaire;
            compteSecrétaire.Profile = secrétaire;
            IEntrepotUtilisateur entrepotUtilisateur = EntrepotMongo.fabriquerEntrepot<IEntrepotUtilisateur>();
            entrepotUtilisateur.ajouter(compteSecrétaire);
            return compteSecrétaire;
        }
    }
}
