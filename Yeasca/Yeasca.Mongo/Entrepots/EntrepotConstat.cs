using System;
using System.Collections.Generic;
using System.Linq;
using Yeasca.Metier;

namespace Yeasca.Mongo
{
    public class EntrepotConstat : IEntrepotConstat
    {
        private IFournisseur _fournisseur;

        public EntrepotConstat(IFournisseur fournisseur)
        {
            _fournisseur = fournisseur;
        }

        public bool ajouter(IAgregat agrégat)
        {
            return _fournisseur.insérer<Constat>(agrégat);
        }

        public bool modifier(IAgregat agrégat)
        {
            return _fournisseur.modifier<Constat>(agrégat);
        }

        public Constat récupérerLeConstat(Guid id)
        {
            return _fournisseur.obtenirLaCollection<Constat>().SingleOrDefault(x => x.Id == id);
        }

        public IList<Constat> récupérerLaListeDesConstats(IRechercheConstat recherche)
        {
            IQueryable<Constat> résultats = _fournisseur.obtenirLaCollection<Constat>().OrderByDescending(x => x.Date);
            résultats = filtrerLaRecherche(recherche, résultats);
            résultats = paginerLaRecherche(recherche, résultats);
            return résultats.ToList();
        }

        private IQueryable<Constat> filtrerLaRecherche(IRechercheConstat recherche, IQueryable<Constat> résultats)
        {
            if (faitUneRechercheSurLeParamètre(recherche.nomClient))
                résultats =
                    résultats.Where(
                        x => x.Client.Nom.Contains(recherche.nomClient) || x.Client.Prénom.Contains(recherche.nomClient));
            if (faitUneRechercheSurLaDate(recherche.dateDébut))
                résultats = résultats.Where(x => x.Date >= recherche.dateDébut.Value);
            if (faitUneRechercheSurLaDate(recherche.dateFin))
                résultats = résultats.Where(x => x.Date <= recherche.dateFin.Value);
            return résultats;
        }

        private static IQueryable<Constat> paginerLaRecherche(IRechercheConstat recherche, IQueryable<Constat> résultats)
        {
            int nombreDElémentsAPasser = (recherche.NuméroPage - 1) * recherche.NombreDElémentsParPage;
            résultats = résultats.Skip(nombreDElémentsAPasser);
            résultats = résultats.Take(recherche.NombreDElémentsParPage);
            return résultats;
        }

        private bool faitUneRechercheSurLeParamètre(string nom)
        {
            return !string.IsNullOrEmpty(nom);
        }

        private bool faitUneRechercheSurLaDate(DateTime? uneDate)
        {
            return uneDate != null && uneDate.HasValue;
        }
    }
}
