using Nancy;
using Nancy.Json;
using Yeasca.Commande;
using Yeasca.Requete;

namespace Yeasca.Api
{
    public class ProfilesModule : NancyModule
    {
        private JavaScriptSerializer _json = new JavaScriptSerializer();

        public ProfilesModule()
        {
            Get["/Client/Recherche"] = _ =>
            {
                IRechercheClientMessage message = new RechercheClientMessage();
                message.Texte = this.Request.Query["Texte"];
                message.NuméroPage = this.Request.Query["NuméroPage"];
                message.NombreDElémentsParPage = this.Request.Query["NombreDElémentsParPage"];
                ReponseRequete réponse = BusRequete.exécuter(message);
                return _json.Serialize(réponse);
            };

            Get["/Client"] = _ =>
            {
                IDetailClientMessage message = new DetailClientMessage();
                message.IdClient = this.Request.Query["IdClient"];
                ReponseRequete réponse = BusRequete.exécuter(message);
                return _json.Serialize(réponse);
            };

            Post["/Client/Creer"] = _ =>
            {
                ICreerClientMessage message = new CreerClientMessage();
                message.Civilité = this.Request.Form["Civilité"];
                message.Nom = this.Request.Form["Nom"];
                message.Prénom = this.Request.Form["Prénom"];
                message.DénominationSociété = this.Request.Form["DénominationSociété"];
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

            Post["/Client/Modifier"] = _ =>
            {
                IModifierClientMessage message = new ModifierClientMessage();
                message.IdClient = this.Request.Form["IdClient"];
                message.Civilité = this.Request.Form["Civilité"];
                message.Nom = this.Request.Form["Nom"];
                message.Prénom = this.Request.Form["Prénom"];
                message.DénominationSociété = this.Request.Form["DénominationSociété"];
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
        }
    }
}