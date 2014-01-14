using Nancy;
using Nancy.Json;
using Yeasca.Commande;
using Yeasca.Requete;

namespace Yeasca.Api
{
    public class UtilisateursModule : NancyModule
    {
        private JavaScriptSerializer _json = new JavaScriptSerializer();

        public UtilisateursModule()
        {
            Get["/Authentifier"] = _ =>
            {
                IAuthentificationMessage message = new AuthentifierMessage();
                message.Email = this.Request.Query["Email"];
                message.MotDePasse = this.Request.Query["MotDePasse"];
                ReponseRequete réponse = BusRequete.exécuter(message);
                return _json.Serialize(réponse);
            };

            Get["/Admin/Jeton"] = _ =>
            {
                IGenererJetonMessage message = new GenererJetonMessage();
                message.Email = this.Request.Query["Email"];
                message.Login = this.Request.Query["Email"];
                message.MotDePasse = this.Request.Query["MotDePasse"];
                ReponseRequete réponse = BusRequete.exécuter(message);
                return _json.Serialize(réponse);
            };

            Post["/Admin/Creer"] = _ =>
            {
                ICreerAdministrateurMessage message = new CreerAdministrateurMessage();
                message.Jeton = this.Request.Form["Jeton"];
                message.Email = this.Request.Form["Email"];
                message.MotDePasse = this.Request.Form["MotDePasse"];
                message.Civilité = this.Request.Form["Civilité"];
                message.Nom = this.Request.Form["Nom"];
                message.Prénom = this.Request.Form["Prénom"];
                message.NomCabinet = this.Request.Form["NomCabinet"];
                message.NuméroVoie = this.Request.Form["NuméroVoie"];
                message.RépétitionVoie = this.Request.Form["RépétitionVoie"];
                message.TypeVoie = this.Request.Form["TypeVoie"];
                message.NomVoie = this.Request.Form["NomVoie"];
                message.ComplémentVoie = this.Request.Form["ComplémentVoie"];
                message.CodePostal = this.Request.Form["CodePostal"];
                message.Ville = this.Request.Form["Ville"];
                ReponseCommande réponse = BusCommande.exécuter(message);
                return _json.Serialize(réponse);
            };

            Get["/Utilisateur/Rechercher"] = _ =>
            {
                IRechercheUtilisateurMessage message = new RechercheUtilisateurMessage();
                message.NomUtilisateur = this.Request.Query["NomUtilisateur"];
                message.Type = this.Request.Query["Type"];
                message.NuméroPage = this.Request.Query["NuméroPage"];
                message.NombreDElémentsParPage = this.Request.Query["NombreDElémentsParPage"];
                ReponseRequete réponse = BusRequete.exécuter(message);
                return _json.Serialize(réponse);
            };

            Get["/Utilisateur"] = _ =>
            {
                IDetailUtilisateurMessage message = new DetailUtilisateurMessage();
                message.IdUtilisateur = this.Request.Query["IdUtilisateur"];
                ReponseRequete réponse = BusRequete.exécuter(message);
                return _json.Serialize(réponse);
            };

            Post["/Admin/Creer/Huissier"] = _ =>
            {
                ICreerHuissierMessage message = new CreerHuissierMessage();
                message.Email = this.Request.Form["Email"];
                message.MotDePasse = this.Request.Form["MotDePasse"];
                message.Civilité = this.Request.Form["Civilité"];
                message.Nom = this.Request.Form["Nom"];
                message.Prénom = this.Request.Form["Prénom"];
                ReponseCommande réponse = BusCommande.exécuter(message);
                return _json.Serialize(réponse);
            };

            Post["/Admin/Modifier/Huissier"] = _ =>
            {
                IModifierHuissierMessage message = new ModifierHuissierMessage();
                message.IdHuissier = this.Request.Form["IdHuissier"];
                message.Email = this.Request.Form["Email"];
                message.MotDePasse = this.Request.Form["MotDePasse"];
                message.Civilité = this.Request.Form["Civilité"];
                message.Nom = this.Request.Form["Nom"];
                message.Prénom = this.Request.Form["Prénom"];
                ReponseCommande réponse = BusCommande.exécuter(message);
                return _json.Serialize(réponse);
            };

            Post["/Admin/Creer/Secretaire"] = _ =>
            {
                ICreerSecretaireMessage message = new CreerSecretaireMessage();
                message.Email = this.Request.Form["Email"];
                message.MotDePasse = this.Request.Form["MotDePasse"];
                message.Civilité = this.Request.Form["Civilité"];
                message.Nom = this.Request.Form["Nom"];
                message.Prénom = this.Request.Form["Prénom"];
                ReponseCommande réponse = BusCommande.exécuter(message);
                return _json.Serialize(réponse);
            };

            Post["/Admin/Modifier/Secretaire"] = _ =>
            {
                IModifierSecretaireMessage message = new ModifierSecretaireMessage();
                message.IdSecrétaire = this.Request.Form["IdSecrétaire"];
                message.Email = this.Request.Form["Email"];
                message.MotDePasse = this.Request.Form["MotDePasse"];
                message.Civilité = this.Request.Form["Civilité"];
                message.Nom = this.Request.Form["Nom"];
                message.Prénom = this.Request.Form["Prénom"];
                ReponseCommande réponse = BusCommande.exécuter(message);
                return _json.Serialize(réponse);
            };

            Get["/Parametrage"] = _ =>
            {
                IParametrageMessage message = new ParametrageMessage();
                ReponseRequete réponse = BusRequete.exécuter(message);
                return _json.Serialize(réponse);
            };
        }
    }
}