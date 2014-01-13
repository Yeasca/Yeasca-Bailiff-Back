using System;
using System.Linq;
using Yeasca.Metier;

namespace Yeasca.Mongo
{
    public abstract class Entrepot
    {
        protected bool faitUneRechercheSurLeParamètre(string nom)
        {
            return !string.IsNullOrEmpty(nom);
        }

        protected bool faitUneRechercheSurLaDate(DateTime? uneDate)
        {
            return uneDate != null && uneDate.HasValue;
        }

        protected IQueryable<T> paginerLaRecherche<T>(IRecherche recherche, IQueryable<T> résultats) where T : IAgregat
        {
            int nombreDElémentsAPasser = (recherche.NuméroPage - 1) * recherche.NombreDElémentsParPage;
            résultats = résultats.Skip(nombreDElémentsAPasser);
            résultats = résultats.Take(recherche.NombreDElémentsParPage);
            return résultats;
        }
    }
}
