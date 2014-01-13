using System;
using System.Collections.Generic;
using System.Linq;
using Yeasca.Metier;

namespace Yeasca.Mongo
{
    public class EntrepotConstat : Entrepot, IEntrepotConstat
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
            if (faitUneRechercheSurLeParamètre(recherche.NomClient))
                résultats =
                    résultats.Where(x =>
                        x.Client.Nom != null && x.Client.Nom.IndexOf(recherche.NomClient, StringComparison.OrdinalIgnoreCase) > -1 ||
                        x.Client.Prénom != null && x.Client.Prénom.IndexOf(recherche.NomClient, StringComparison.OrdinalIgnoreCase) > -1);
            if (faitUneRechercheSurLaDate(recherche.DateDébut))
                résultats = résultats.Where(x => x.Date >= recherche.DateDébut.Value);
            if (faitUneRechercheSurLaDate(recherche.DateFin))
                résultats = résultats.Where(x => x.Date <= recherche.DateFin.Value);
            return résultats;
        }

        public IList<Constat> récupérerLaListeDesConstats(IRechercheGlobale recherche)
        {
            IQueryable<Constat> résultats = _fournisseur.obtenirLaCollection<Constat>().OrderByDescending(x => x.Date);
            résultats = filtrerLaRecherche(recherche, résultats);
            résultats = paginerLaRecherche(recherche, résultats);
            return résultats.ToList();
        }

        private IQueryable<Constat> filtrerLaRecherche(IRechercheGlobale recherche, IQueryable<Constat> résultats)
        {
            if (faitUneRechercheSurLeParamètre(recherche.Texte))
                résultats =
                    résultats.Where(x =>
                        x.Client.Nom != null && x.Client.Nom.IndexOf(recherche.Texte, StringComparison.OrdinalIgnoreCase) > -1 ||
                        x.Client.Prénom != null && x.Client.Prénom.IndexOf(recherche.Texte, StringComparison.OrdinalIgnoreCase) > -1);
            return résultats;
        }

        public IList<Constat> récupérerLaListeDesConstatsDuClient(Guid idClient)
        {
            return _fournisseur.obtenirLaCollection<Constat>().Where(x => x.Client.Id == idClient).ToList();
        }

        public int nombreDeConstatPourLeClient(Guid idClient)
        {
            return _fournisseur.obtenirLaCollection<Constat>().Where(x => x.Client.Id == idClient).Count();
        }
    }
}
