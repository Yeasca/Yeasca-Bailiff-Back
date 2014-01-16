using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using Nancy;
using Nancy.Json;
using Nancy.Responses;
using Yeasca.Api;
using Yeasca.Commande;
using Yeasca.Requete;

namespace Yeasca.Web.Api
{
    public class ConstatApiModule : NancyModule
    {
        private JavaScriptSerializer _json = new JavaScriptSerializer();

        public ConstatApiModule()
        {
            Get["/Api/Constat/Recherche"] = _ =>
            {
                IRechercheGeneraleConstatMessage message = new RechercheGeneraleConstatMessage();
                message.Texte = this.Request.Query["Texte"];
                message.NuméroPage = this.Request.Query["NuméroPage"];
                message.NombreDElémentsParPage = this.Request.Query["NombreDElémentsParPage"];
                ReponseRequete réponse = BusRequete.exécuter(message);
                return _json.Serialize(réponse);
            };
            
            Get["/Api/Constat/Recherche/Avance"] = _ =>
            {
                IRechercheAvanceConstatMessage message = new RechercheAvanceConstatMessage();
                message.NomClient = this.Request.Query["NomClient"];
                message.DateDébut = convertirEnDate(this.Request.Query["DateDébut"]);
                message.DateFin = convertirEnDate(this.Request.Query["DateFin"]);
                message.NuméroPage = this.Request.Query["NuméroPage"];
                message.NombreDElémentsParPage = this.Request.Query["NombreDElémentsParPage"];
                ReponseRequete réponse = BusRequete.exécuter(message);
                return _json.Serialize(réponse);
            };

            Get["/Api/Constat"] = _ =>
            {
                IDetailConstatMessage message = new DetailConstatMessage();
                message.IdConstat = this.Request.Query["IdConstat"];
                ReponseRequete réponse = BusRequete.exécuter(message);
                return _json.Serialize(réponse);
            };

            Post["/Api/Constat/Creer"] = _ =>
            {
                ICreerConstatMessage message = new CreerConstatMessage();
                message.IdClient = this.Request.Form["IdClient"];
                ReponseCommande réponse = BusCommande.exécuter(message);
                return _json.Serialize(réponse);
            };

            Post["/Api/Constat/Ajouter/Fichier"] = _ =>
            {
                HttpFile fichier = récupérerLeFichier(this.Request.Files);
                IAjouterFichierConstatMessage message = new AjouterFichierConstatMessage();
                message.IdConstat = this.Request.Form["IdConstat"];
                message.Nom = fichier.Name;
                message.Fichier = transformerLeFichierEnMemoryStream(fichier);
                ReponseCommande réponse = BusCommande.exécuter(message);
                return _json.Serialize(réponse);
            };

            Post["/Api/Constat/Valider"] = _ =>
            {
                HttpFile fichier = récupérerLeFichier(this.Request.Files);
                IValiderConstatMessage message = new ValiderConstatMessage();
                message.IdConstat = this.Request.Form["IdConstat"];
                message.Nom = fichier.Name;
                message.Fichier = transformerLeFichierEnMemoryStream(fichier);
                ReponseCommande réponse = BusCommande.exécuter(message);
                return _json.Serialize(réponse);
            };

            Get["/Api/Constat/Telecharger"] = _ =>
            {
                ITelechargerFichierMessage message = new TelechargerFichierMessage();
                message.IdConstat = this.Request.Query["IdConstat"];
                message.IdFichier = this.Request.Query["IdFichier"];
                ReponseRequete réponse = BusRequete.exécuter(message);
                if (réponse.Réussite)
                {
                    FichierReponse résultat = réponse.Résultat as FichierReponse;
                    HttpResponse response = HttpContext.Current.Response;
                    response.ClearContent();
                    response.Clear();
                    response.ContentType = résultat.TypeMIME;
                    response.AddHeader("Content-Disposition", "attachment; filename=" + résultat.Nom + ";");
                    response.TransmitFile(résultat.Chemin);
                    response.End();
                    résultat.Chemin = string.Empty;
                    réponse.Résultat = résultat;
                }
                return _json.Serialize(réponse);
            };
        }

        private DateTime? convertirEnDate(string date)
        {
            DateTime résultat;
            if (DateTime.TryParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out résultat))
                return résultat;
            return null;
        }

        private HttpFile récupérerLeFichier(IEnumerable<HttpFile> fichiers)
        {
            return fichiers.FirstOrDefault(x => x.Key == "Fichier");
        }

        private MemoryStream transformerLeFichierEnMemoryStream(HttpFile fichier)
        {
            if (fichier != null)
            {
                MemoryStream fichierTransformé = new MemoryStream();
                fichier.Value.CopyTo(fichierTransformé);
                return fichierTransformé;
            }
            return null;
        }
    }
}