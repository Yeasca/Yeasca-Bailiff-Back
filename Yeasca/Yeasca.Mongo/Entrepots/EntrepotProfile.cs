using System;
using System.Collections.Generic;
using System.Linq;
using Yeasca.Metier;

namespace Yeasca.Mongo
{
    public class EntrepotProfile : Entrepot, IEntrepotProfile
    {
        private IFournisseur _fournisseur;

        public EntrepotProfile(IFournisseur fournisseur)
        {
            _fournisseur = fournisseur;
        }

        public bool ajouter(IAgregat agrégat)
        {
            return _fournisseur.insérer<Profile>(agrégat);
        }

        public bool modifier(IAgregat agrégat)
        {
            return _fournisseur.modifier<Profile>(agrégat);
        }

        public Client récupérerLeClient(Guid id)
        {
            return _fournisseur.obtenirLaCollection<Profile>().SingleOrDefault(x => x is Client && x.Id == id) as Client;
        }

        public Huissier récupérerLHuissier(Guid id)
        {
            return _fournisseur.obtenirLaCollection<Profile>().SingleOrDefault(x => x is Huissier && x.Id == id) as Huissier;
        }

        public Secretaire récupérerLaSecrétaire(Guid id)
        {
            return _fournisseur.obtenirLaCollection<Profile>().SingleOrDefault(x => x is Secretaire && x.Id == id) as Secretaire;
        }

        public IList<Client> récupérerLaListeDesClients(IRechercheGlobale recherche)
        {
            IQueryable<Client> résultats = _fournisseur.obtenirLaCollection<Profile>().Where(x => x is Client).OrderBy(x => x.Nom).Cast<Client>();
            résultats = filtrerLaRecherche(recherche, résultats);
            résultats = paginerLaRecherche(recherche, résultats);
            return résultats.ToList();
        }

        private IQueryable<Client> filtrerLaRecherche(IRechercheGlobale recherche, IQueryable<Client> résultats)
        {
            if (faitUneRechercheSurLeParamètre(recherche.Texte))
            {
                résultats =
                    résultats.Where(
                        x =>
                            x.Nom != null && x.Nom.IndexOf(recherche.Texte, StringComparison.OrdinalIgnoreCase) > -1 ||
                            x.Prénom != null && x.Prénom.IndexOf(recherche.Texte, StringComparison.OrdinalIgnoreCase) > -1);
            }
            return résultats;
        }

        public IList<Huissier> récupérerLaListeDesHuissier(IRechercheGlobale recherche)
        {
            IQueryable<Huissier> résultats = _fournisseur.obtenirLaCollection<Profile>().Where(x => x is Huissier).OrderBy(x => x.Nom).Cast<Huissier>();
            résultats = filtrerLaRecherche(recherche, résultats);
            résultats = paginerLaRecherche(recherche, résultats);
            return résultats.ToList();
        }

        private IQueryable<Huissier> filtrerLaRecherche(IRechercheGlobale recherche, IQueryable<Huissier> résultats)
        {
            if (faitUneRechercheSurLeParamètre(recherche.Texte))
            {
                résultats =
                    résultats.Where(
                        x =>
                            x.Nom != null && x.Nom.IndexOf(recherche.Texte, StringComparison.OrdinalIgnoreCase) > -1 ||
                            x.Prénom != null && x.Prénom.IndexOf(recherche.Texte, StringComparison.OrdinalIgnoreCase) > -1);
            }
            return résultats;
        }

        public IList<Secretaire> récupérerLaListeDesSecrétaires(IRechercheGlobale recherche)
        {
            IQueryable<Secretaire> résultats = _fournisseur.obtenirLaCollection<Profile>().Where(x => x is Secretaire).OrderBy(x => x.Nom).Cast<Secretaire>();
            résultats = filtrerLaRecherche(recherche, résultats);
            résultats = paginerLaRecherche(recherche, résultats);
            return résultats.ToList();
        }

        private IQueryable<Secretaire> filtrerLaRecherche(IRechercheGlobale recherche, IQueryable<Secretaire> résultats)
        {
            if (faitUneRechercheSurLeParamètre(recherche.Texte))
            {
                résultats =
                    résultats.Where(
                        x =>
                            x.Nom != null && x.Nom.IndexOf(recherche.Texte, StringComparison.OrdinalIgnoreCase) > -1 ||
                            x.Prénom != null && x.Prénom.IndexOf(recherche.Texte, StringComparison.OrdinalIgnoreCase) > -1);
            }
            return résultats;
        }
    }
}
